using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic; 
using System.Text.RegularExpressions;  

//OpenXML
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

//JSON.net
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;
using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Web.Pages
{
    public class UploadModel : PageModel
    {
        [BindProperty]
        public IFormFile fileUpload {get; set;}

        private ZipArchive zip;

        private String errorString = "";

        private Boolean uploaded = false;

        private String zipRoot = ""; 

        //=== vars from zip file===
        public UoProjectModel _Project;
        public String _ProjectName {get; set;}
        public String _ProjectDescription {get; set;}
        public UoProgramModel _Program;

        List<UoCourseListModel> _CourseListModels = new List<UoCourseListModel>();
        List<String[]> _CourseListIds = new List<String[]>();
        UoFrameworkModel _Framework = new UoFrameworkModel();
        
        List<UoCourseModel> _CourseModels = new List<UoCourseModel>();
        List<UoLevelCategoryModel> _LevelCategories = new List<UoLevelCategoryModel>();
        List<List<UoLevelCoursesJoin>> _LevelCourseJoin = new  List<List<UoLevelCoursesJoin>>();



        public async Task OnPostAsync()
        {
            if(fileUpload !=null && fileUpload.ContentType.Equals("application/x-zip-compressed"))
            {
                zip = new ZipArchive(fileUpload.OpenReadStream());
                proccessZip();
                uploaded = true;
            }
            else    
            {
                errorString = "Please upload a .zip file";
            }
                
        }


        //returns a string containing any errors that might have occured
        public String getErrorString()
        {
            return errorString;
        }

        public Boolean hasUpload()
        {
            return uploaded;
        }

        //proccess the uploaded zips into the api.
        private void proccessZip()
        {

  

            getModels();
            postModels();
            linkTables();


   
        }

        private void getModels()
        {

            //===Get FrameworkData===

            
            ZipArchiveEntry _FrameworkData = zip.GetEntry(zipRoot + "TemplateInfo/TemplateData.xlsx");
            SpreadsheetDocument _FrameworkDataXLSX = getSpreadSheetFromZip(_FrameworkData);

            List<UoNodeModel> _Nodes = new List<UoNodeModel>();
            
            int j = 1;

            foreach(Sheet sheet in _FrameworkDataXLSX.WorkbookPart.Workbook.Sheets)
            {
                if(sheet.Name.Equals("FRAMEWORK"))
                {
                    _Framework.Name = getCellValue(_FrameworkDataXLSX, "FRAMEWORK", "A2");                      // set framework name
                    _Framework.Description = getCellValue(_FrameworkDataXLSX, "FRAMEWORK", "B2");               // set framework description
                    _Framework.TemplateName =  getCellValue(_FrameworkDataXLSX, "FRAMEWORK", "C2");             // set framework template name
                    _Framework.CustomNodeNoun = getCellValue(_FrameworkDataXLSX, "FRAMEWORK", "D2");            // set framework Custom node noun
                    _Framework.CustomNodePluralNoun = getCellValue(_FrameworkDataXLSX, "FRAMEWORK", "E2");      // set framework Custom node plural noun
                    _Framework.CustomFrameworkNoun = getCellValue(_FrameworkDataXLSX, "FRAMEWORK", "F2");       // set framework custom framework noun
                }
                else
                {

                    _Nodes.Add(new UoNodeModel());

                    _Nodes[_Nodes.Count-1].Name = sheet.Name;

                    _Nodes[_Nodes.Count-1].Position = j;
                    j++;

                    int i = 2;
                    List<UoNodeModel> _ChildNodes = new List<UoNodeModel>();

                    while(getCellValue(_FrameworkDataXLSX, sheet.Name, "A" + i) != "null")
                    {
                        

                        _ChildNodes.Add(new UoNodeModel());

                        _ChildNodes[_ChildNodes.Count-1].NodeCode = getCellValue(_FrameworkDataXLSX, sheet.Name, "A" + i);
                        _ChildNodes[_ChildNodes.Count-1].NodeCode = string.Format("{0:0.0}", double.Parse(_ChildNodes[_ChildNodes.Count-1].NodeCode));

                        _ChildNodes[_ChildNodes.Count-1].Name = getCellValue(_FrameworkDataXLSX, sheet.Name, "B" + i);
                        _ChildNodes[_ChildNodes.Count-1].Position = i-1;
                       // _ChildNodes[_ChildNodes.Count-1].ParentNode = _Nodes[_Nodes.Count-1];
                        i++;
                    }

                    _Nodes[_Nodes.Count-1].ChildNodes = _ChildNodes;

                
                }

            }
            
            _Framework.Nodes = _Nodes;

            //===Get Level Categories===

            ZipArchiveEntry _LevelCategoryData = zip.GetEntry(zipRoot + "TemplateInfo/LevelCategories.xlsx");
            SpreadsheetDocument _LevelCateogoryXLSX = getSpreadSheetFromZip(_LevelCategoryData);
    

            foreach(Sheet sheet in _LevelCateogoryXLSX.WorkbookPart.Workbook.Sheets)
            {
                List<UoLevelModel> _LevelModels = new List<UoLevelModel>();

                j = 2;
                _LevelCategories.Add(new UoLevelCategoryModel());
                _LevelCategories[_LevelCategories.Count-1].Name = sheet.Name;

                while(getCellValue(_LevelCateogoryXLSX, sheet.Name, "A" + j) != "null")
                {
                    _LevelModels.Add(new UoLevelModel());

                    _LevelModels[_LevelModels.Count-1].Position = int.Parse(getCellValue(_LevelCateogoryXLSX, sheet.Name, "A" + j));
                    _LevelModels[_LevelModels.Count-1].Name = getCellValue(_LevelCateogoryXLSX, sheet.Name, "B" + j);
                    j++;
                }

                _LevelCategories[_LevelCategories.Count-1].Levels = _LevelModels;
            }

            //===Get ProjectData===

            ZipArchiveEntry _ProjectData = zip.GetEntry(zipRoot + "ProjectData.xlsx"); // open ProjectData.xlsx


            SpreadsheetDocument _ProjectDataXLSX = getSpreadSheetFromZip(_ProjectData);

            // get data from ProjectData
            _ProjectName = getCellValue(_ProjectDataXLSX, "ProjectData", "A2");

            _ProjectDescription = getCellValue(_ProjectDataXLSX, "ProjectData", "B2");

            _ProjectDataXLSX.Close();

            //===Get ProgramData===

            _Program = new UoProgramModel();

            ZipArchiveEntry _ProgramData = zip.GetEntry(zipRoot + "Programs/ProgramData.xlsx"); 

            SpreadsheetDocument _ProgramDataXLSX = getSpreadSheetFromZip(_ProgramData);


            _Program.ProgramCode = int.Parse(getCellValue(_ProgramDataXLSX, "ProgramInfo", "A2"));      // set program code
            _Program.Name = getCellValue(_ProgramDataXLSX, "ProgramInfo", "B2");                        // set program name
            _Program.FirstTermCode = int.Parse(getCellValue(_ProgramDataXLSX, "ProgramInfo", "C2"));    // set firstTermCode
            _Program.MinUnits = int.Parse(getCellValue(_ProgramDataXLSX, "ProgramInfo", "D2"));         // set minUnits
            _Program.Duration = int.Parse(getCellValue(_ProgramDataXLSX, "ProgramInfo", "E2"));         // set duration
            _Program.MaxYears = int.Parse(getCellValue(_ProgramDataXLSX, "ProgramInfo", "F2"));         // set maxYears
            
        
            // get all campuses from API
            List<UoCampusModel> campusModels = JsonConvert.DeserializeObject<List<UoCampusModel>>(getJsonFromAPI("campuses"));

            String campusName = getCellValue(_ProgramDataXLSX, "ProgramInfo", "G2"); // get name of the campus

            //search for campus
            for(int i = 0; i < campusModels.Count; i++)
            {
                if(campusModels[i].Name.Equals(campusName))
                {
                    _Program.Campus = campusModels[i];
                    _Program.CampusId = campusModels[i].CampusId;
                    break;
                }
                  
            }

            // TODO: these could probs be put into their own function to clean things up abit
            List<UoProgramCareerModel> programCareerModels = JsonConvert.DeserializeObject<List<UoProgramCareerModel>>(getJsonFromAPI("program-careers"));

            String careerName = getCellValue(_ProgramDataXLSX, "ProgramInfo", "H2");
            
            for(int i = 0; i < programCareerModels.Count; i++)
            {
                if(programCareerModels[i].Name.Equals(careerName))
                {
                    _Program.ProgramCareer = programCareerModels[i];
                    _Program.ProgramCareerId = programCareerModels[i].ProgramCareerId;
                    break;
                }
                  
            }

            List<UoFieldOfEducationModel> fieldOfEducationModels = JsonConvert.DeserializeObject<List<UoFieldOfEducationModel>>(getJsonFromAPI("fields-of-education"));

            String fieldOfEducation = getCellValue(_ProgramDataXLSX, "ProgramInfo", "I2");

            for(int i = 0; i < fieldOfEducationModels.Count; i++)
            {
                if(fieldOfEducationModels[i].Name.Equals(fieldOfEducation))
                {
                    _Program.FieldOfEducation = fieldOfEducationModels[i];
                    _Program.FieldOfEducationId = fieldOfEducationModels[i].FieldOfEducationId;
                    break;
                }
                  
            }

            //===Get Course list===
            ZipArchiveEntry _CourseListData = zip.GetEntry(zipRoot + "CourseLists/ProgramCourseLists.xlsx"); 

            SpreadsheetDocument _CourseListXLSX = getSpreadSheetFromZip(_CourseListData);



            j=2;

            while(getCellValue(_CourseListXLSX, "CourseListInfo", "A" + j) != "null")
            {
                _CourseListModels.Add(new UoCourseListModel());

                _CourseListModels[_CourseListModels.Count-1].Position = int.Parse(getCellValue(_CourseListXLSX, "CourseListInfo", "A" + j));
                _CourseListModels[_CourseListModels.Count-1].Name = getCellValue(_CourseListXLSX, "CourseListInfo", "B" + j);
                _CourseListModels[_CourseListModels.Count-1].Major = getCellValue(_CourseListXLSX, "CourseListInfo", "C" + j);

                _CourseListIds.Add(getCellValue(_CourseListXLSX, "CourseListInfo", "D" + j).Split(","));
                j++;



                
            }

            // ===Get Course data===
            
            List<String> courseNames = new List<String>();

            foreach (ZipArchiveEntry entry in zip.Entries)
            {
                Regex regex = new Regex(@"^"+zipRoot+"Courses/[A-Z]{4}[0-9]{4}[A-Z]?/$");


                if(regex.IsMatch(entry.FullName))
                {
                    courseNames.Add(entry.FullName.Substring(8).Remove(entry.FullName.Substring(8).Length -1));
                }
                
            }


            foreach(String name in courseNames)
            {
                ZipArchiveEntry _CourseData = zip.GetEntry(zipRoot +"Courses/" +name+ "/CourseData.xlsx"); // open CourseData.xlsx
                SpreadsheetDocument _CourseDataXLSX = getSpreadSheetFromZip(_CourseData);
                UoCourseModel _Course = new UoCourseModel();

                _Course.Subject = name.Remove(4);                                                   // set course subject
                _Course.CatalogNumber = name.Substring(4);                                          // set course catalogNumber
                _Course.Position = int.Parse(getCellValue(_CourseDataXLSX, "CourseInfo", "A2"));    // set course position
                _Course.Name = getCellValue(_CourseDataXLSX, "CourseInfo", "B2");                   // set course name
                _Course.Description = getCellValue(_CourseDataXLSX, "CourseInfo", "C2");            // set course description
                _Course.Units = int.Parse( getCellValue(_CourseDataXLSX, "CourseInfo", "D2"));      // set course units
                
                List<UoAcademicOrgModel> academicOrgModels =  JsonConvert.DeserializeObject<List<UoAcademicOrgModel>>(getJsonFromAPI("academic-orgs"));

                String academicOrgName = getCellValue(_CourseDataXLSX, "CourseInfo", "E2");

                for(int i = 0; i < academicOrgModels.Count; i++)
                {
                    if(academicOrgName.Equals(academicOrgModels[i].Name))
                    {
                        _Course.AcademicOrg = academicOrgModels[i];
                        _Course.AcademicOrgId = academicOrgModels[i].AcademicOrgId;
                        break;
                    }
                }

            
                fieldOfEducation = getCellValue(_CourseDataXLSX, "CourseInfo", "F2");

                for(int i = 0; i < fieldOfEducationModels.Count; i++)
                {
                    if(fieldOfEducationModels[i].Name.Equals(fieldOfEducation))
                    {
                        _Course.FieldOfEducation = fieldOfEducationModels[i];
                        _Course.FieldOfEducationId = fieldOfEducationModels[i].FieldOfEducationId;
                        break;
                    }
                    
                }

                j = 2;
                
                

                while(getCellValue(_CourseDataXLSX, "CompetencyMapping", "A" + j) != "null")
                {
                    
                    UoNodeModel selectedNode = new UoNodeModel();
                    String nodeCode = getCellValue(_CourseDataXLSX, "CompetencyMapping", "A" + j);

                    nodeCode = string.Format("{0:0.0}", double.Parse(nodeCode));

                    foreach(UoNodeModel node in _Framework.Nodes)
                    {
                        foreach(UoNodeModel childNode in node.ChildNodes)
                        {
                            
                            if(childNode.NodeCode.Equals(nodeCode))
                            {
                                selectedNode = childNode;
                                
                            }
                        }
                    }

                    int levelOfCapability = int.Parse(getCellValue(_CourseDataXLSX, "CompetencyMapping", "B" + j));


                    foreach(UoLevelModel level in _LevelCategories[0].Levels)
                    {
                        if(levelOfCapability == level.Position)
                        {
                            UoLevelCoursesJoin courseJoin = new UoLevelCoursesJoin();
                            courseJoin.Level = level;
                            courseJoin.LevelId = level.LevelId;
                            courseJoin.Node = selectedNode;
                            courseJoin.NodeId = selectedNode.NodeId;
                            courseJoin.Course = _Course;

                            _Course.LevelCourses.Add(courseJoin);
                        }
                    }
                    

                    String[] impliedPedagogyArray = getCellValue(_CourseDataXLSX, "CompetencyMapping", "C" + j).Split(",");

                    foreach(String impliedPedagogy in impliedPedagogyArray)
                    {
                        foreach(UoLevelModel level in _LevelCategories[1].Levels)
                        {
                            if(int.Parse(impliedPedagogy) == level.Position)
                            {
                                UoLevelCoursesJoin courseJoin = new UoLevelCoursesJoin();
                                courseJoin.Level = level;
                                courseJoin.LevelId = level.LevelId;
                                courseJoin.Node = selectedNode;
                                courseJoin.NodeId = selectedNode.NodeId;
                                courseJoin.Course = _Course;

                                _Course.LevelCourses.Add(courseJoin);
                            }
                        }
                    }

                    

                    j++;
                }
                

                

                _CourseModels.Add(_Course);
                _LevelCourseJoin.Add(_Course.LevelCourses);

            }

        }


        private void postModels()
        {
            //===Post to api===

            String json = "";

            //Post framework
            json = JsonConvert.SerializeObject(_Framework, Formatting.Indented);
            _Framework = JsonConvert.DeserializeObject<UoFrameworkModel>(postJSON(json, "frameworks"));

            //Post project
            json = new JObject(new JProperty("name", _ProjectName), new JProperty("description", _ProjectDescription), new JProperty("frameworkId", _Framework.FrameworkId)).ToString();
            _Project = JsonConvert.DeserializeObject<UoProjectModel>(postJSON(json, "projects"));

            //Post program
            json = new JObject(new JProperty("programCode", _Program.ProgramCode), 
                                new JProperty("name", _Program.Name),
                                new JProperty("firstTermCode", _Program.FirstTermCode), 
                                new JProperty("minUnits", _Program.MinUnits),
                                new JProperty("duration", _Program.Duration), 
                                new JProperty("maxYears", _Program.MaxYears),
                                new JProperty("campusId", _Program.CampusId), 
                                new JProperty("programCareerId", _Program.ProgramCareerId),
                                new JProperty("fieldOfEducationId", _Program.FieldOfEducationId)).ToString();


            _Program = JsonConvert.DeserializeObject<UoProgramModel>(postJSON(json, "programs"));

            //Post course-list

            for(int i = 0; i < _CourseListModels.Count; i++)
            {
                _CourseListModels[i].ProgramId = _Program.ProgramId;

                json = new JObject(new JProperty("position", _CourseListModels[i].Position), 
                                new JProperty("name", _CourseListModels[i].Name),
                                new JProperty("major", _CourseListModels[i].Major), 
                                new JProperty("programId",_CourseListModels[i].ProgramId)).ToString();

                _CourseListModels[i] = JsonConvert.DeserializeObject<UoCourseListModel>(postJSON(json, "course-lists"));

            }

            //Post course

            for(int i = 0; i < _CourseModels.Count; i++)
            {
                json = new JObject(new JProperty("subject", _CourseModels[i].Subject), 
                    new JProperty("catalogNumber", _CourseModels[i].CatalogNumber),
                    new JProperty("name", _CourseModels[i].Name), 
                    new JProperty("description",_CourseModels[i].Description),
                    new JProperty("units", _CourseModels[i].Units), 
                    new JProperty("position", _CourseModels[i].Position),
                    new JProperty("academicOrgId", _CourseModels[i].AcademicOrgId), 
                    new JProperty("fieldOfEducationId",_CourseModels[i].FieldOfEducationId)).ToString();

                _CourseModels[i] = JsonConvert.DeserializeObject<UoCourseModel>(postJSON(json, "courses"));
                
            }

            //post level Categories

            for(int i = 0; i < _LevelCategories.Count; i++)
            {
                _LevelCategories[i] =  JsonConvert.DeserializeObject<UoLevelCategoryModel>(postJSON(JsonConvert.SerializeObject(_LevelCategories[i], Formatting.Indented), "level-categories"));
                
                for(int j = 0; j < _LevelCategories[i].Levels.Count; j++)
                {
                    json = new JObject(new JProperty("position", _LevelCategories[i].Levels[j].Position), 
                        new JProperty("name", _LevelCategories[i].Levels[j].Name),
                        new JProperty("description", _LevelCategories[i].Levels[j].Description),  
                        new JProperty("levelCategoryId",_LevelCategories[i].LevelCategoryId)).ToString();

                    _LevelCategories[i].Levels[j] = JsonConvert.DeserializeObject<UoLevelModel>(postJSON(json, "levels"));
                }

            }


        }

        
        private void linkTables()
        {
            //===link via linking tables===
            String json = "";
            //course to course-list

            foreach(UoCourseModel course in _CourseModels)
            {
                for(int i = 0; i < _CourseListIds.Count; i++)
                {
                    foreach(String courseID in _CourseListIds[i])
                    {
                        if(courseID.Equals(course.Subject + course.CatalogNumber))
                        {
                            json = new JObject(new JProperty("courseListId", _CourseListModels[i].CourseListId),
                                                new JProperty("courseId", course.CourseId)).ToString();
                        }

                    }
                }

                postJSON(json, "course-list-courses");
                
            }

            //competencies to levels

            foreach(UoLevelCategoryModel categoryModel in _LevelCategories)
            {
                foreach(UoNodeModel node in _Framework.Nodes)
                {
                    json = new JObject(new JProperty("levelCategoryId", categoryModel.LevelCategoryId),
                                        new JProperty("nodeId", node.NodeId)).ToString();

                    postJSON(json, "level-category-nodes");

                    foreach(UoNodeModel childNode in node.ChildNodes)
                    {
                        json = new JObject(new JProperty("levelCategoryId", categoryModel.LevelCategoryId),
                                        new JProperty("nodeId", childNode.NodeId)).ToString();

                        postJSON(json, "level-category-nodes");
                    }
                }


            }

            //courses to competencies and levels
            //level-courses

            int levelId = -1;
            int nodeId = -1;
            int courseId = -1;

            foreach(List<UoLevelCoursesJoin> courseJoinList in _LevelCourseJoin)
            {
                foreach(UoLevelCoursesJoin courseJoin in courseJoinList) // for each course to join
                {

                    //get level ID
                    foreach(UoLevelCategoryModel categoryModel in _LevelCategories)
                    {
                        foreach(UoLevelModel levelModel in categoryModel.Levels)
                        {
                            if(levelModel.Name.Equals(courseJoin.Level.Name))
                            {
                                levelId = levelModel.LevelId;
                            }
                        }
                    }

                    //get node
                    foreach(UoNodeModel node in _Framework.Nodes)
                    {
                        foreach(UoNodeModel childNode in node.ChildNodes)
                        {
                            if(childNode.NodeCode.Equals(courseJoin.Node.NodeCode))
                            {
                                nodeId = childNode.NodeId;
                            }
                        }
                    }

                    //get course
                    foreach(UoCourseModel courseModel in _CourseModels)
                    {
                        if (courseModel.Name.Equals(courseJoin.Course.Name))
                        {
                            courseId = courseModel.CourseId;
                        }
                    }



                    json = new JObject(new JProperty("courseId", courseId),
                        new JProperty("levelId", levelId),
                        new JProperty("nodeId", nodeId)).ToString(); 
                
                    postJSON(json, "level-courses");

                }

                


            }


            //Programs to Nodes to Levels

              json = new JObject(new JProperty("programId", _Program.ProgramId),
                    new JProperty("projectId", _Project.ProjectId)).ToString();

            postJSON(json, "project-programs");

        }

        private String postJSON(string json, string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:53924/api/" + url);

            request.Method = "POST";
            request.ContentType = "application/json";


           // Console.WriteLine(json);

            using (StreamWriter APIWriter = new StreamWriter(request.GetRequestStream()))
            {
                APIWriter.Write(json);
            }

  
           
            
           StreamReader APIReader = new StreamReader(request.GetResponse().GetResponseStream());

            return APIReader.ReadToEnd();

        }

        private String getJsonFromAPI(String url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:53924/api/" + url);

            request.Accept = "application/json";
            

            StreamReader APIReader = new StreamReader(request.GetResponse().GetResponseStream());

            return APIReader.ReadToEnd();
        }

        private SpreadsheetDocument getSpreadSheetFromZip(ZipArchiveEntry zipEntry)
        {
            //Convert the DeflateStream into a memory stream, the stream given by zip.GetEntry does not have write permisions therfore it needs to be changed


            MemoryStream ms = new MemoryStream();

            zipEntry.Open().CopyTo(ms);

            SpreadsheetDocument _XLSX = SpreadsheetDocument.Open(ms, true);

            return _XLSX;
        }

        private string getCellValue(SpreadsheetDocument excel, string sheetName, string cellLocation)
        {
            //https://docs.microsoft.com/en-us/office/open-xml/how-to-retrieve-the-values-of-cells-in-a-spreadsheet for more info on this


            Sheet sheet = excel.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName).FirstOrDefault(); // get the sheet from the excel document

            WorksheetPart sheetPart = (WorksheetPart)(excel.WorkbookPart.GetPartById(sheet.Id)); // get the sheet part

            Cell cell = sheetPart.Worksheet.Descendants<Cell>().Where(c => c.CellReference == cellLocation).FirstOrDefault(); // get the cell from the sheet part


            if(cell != null)
            {
                if(cell.DataType == null)
                {
                    return cell.InnerText;
                }
                else
                {
                    //only strings are handled here atm, other types can be added as needed
                    switch(cell.DataType.Value)
                    {
                        case CellValues.SharedString:
                            return excel.WorkbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault().SharedStringTable.ElementAt(int.Parse(cell.InnerText)).InnerText; // return the text stored in the cell
                        default:
                            return "Cell type not found";
                    }
                }


            }
            else
            {
                return "null";
                //cell not found error
            }

        }

        public String getLink()
        {
            return "http://localhost:53924/api/programs/" +_Program.ProgramId + "/view?projectId="+_Project.ProjectId +"&template=" + _Framework.TemplateName;
        }
    }
}
