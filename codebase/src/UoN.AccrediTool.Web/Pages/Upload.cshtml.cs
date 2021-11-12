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

namespace UoN.AccrediTool.Web.Pages
{
    public class UploadModel : PageModel
    {
        [BindProperty]
        public IFormFile fileUpload {get; set;}

        private ZipArchive zip;

        private String errorString = "";

        private Boolean uploaded = false;

        private String zipRoot = "filestructure/"; 

        //=== vars from zip file===
        public String _ProjectName {get; set;}
        public String _ProjectDescription {get; set;}
        public UoProgramModel _Program;


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

            // ===Get Course data===
            
            List<String> courseNames = new List<String>();

            foreach (ZipArchiveEntry entry in zip.Entries)
            {
                Regex regex = new Regex(@"^"+zipRoot+"Courses/[A-Z]{4}[0-9]{4}[A-Z]?/$");


                if(regex.IsMatch(entry.FullName))
                {
                    courseNames.Add(entry.FullName.Substring(22).Remove(entry.FullName.Substring(22).Length -1));
                }
                
            }

            List<UoCourseModel> _CourseModels = new List<UoCourseModel>();

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

                _CourseModels.Add(_Course);


            }



            //===Post to api===




            
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
                return "Cell not found";
                //cell not found error
            }

        }
    }
}
