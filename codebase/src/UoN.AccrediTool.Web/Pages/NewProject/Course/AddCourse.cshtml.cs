using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;


//JSON.net
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;
using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Web.Pages.Shared.Templates.EngineersAustralia;


namespace UoN.AccrediTool.Web.Pages.NewProject.Course
{
    //[Authorize]
    public class AddCourseModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string CourseListJson {get; set; }

        [BindProperty(SupportsGet = true)]
        public int Pos { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Year { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Semester { get; set; }

        [BindProperty]
        public UoCourseModel CourseModel { get; set; }


        [BindProperty]
        public String CourseCode { get; set; }

        [BindProperty]
        public int NumOfComp { get; set; }

        [BindProperty]
        public List<UoLevelCoursesJoin> LevelCoursesJoins { get; } = new();


        [BindProperty(SupportsGet =true)]
        public int programId {get; set;}
        
        [BindProperty(SupportsGet =true)]
        public int projectId {get; set;}

        [BindProperty(SupportsGet = true)]
        public string LevelCategoryJson {get; set;}



        public List<UoLevelCategoryModel> LevelCategoryModels { get; } = new();

        public List<UoNodeModel> NodeModels { get; } = new();

        public int frameworkId { get; set; }

        private readonly IConfiguration _Configuration;

        public AddCourseModel(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public void OnGet()
        {


            if (LevelCategoryJson != "[NaN]")
            {
                List<int> LevelCatIds = JsonConvert.DeserializeObject<List<int>>(LevelCategoryJson);


                for (int i = 0; i < LevelCatIds.Count; i++)
                {
                    LevelCategoryModels.Add(JsonConvert.DeserializeObject<UoLevelCategoryModel>(API.API.GetJSON("level-categories/" + LevelCatIds[i], _Configuration)));

                }

            }


            // get competencies
            if (projectId != 0)
            {
                frameworkId = (int)JsonConvert.DeserializeObject<UoProjectModel>(API.API.GetJSON("projects/" + projectId, _Configuration)).Framework.FrameworkId;

                List<UoNodeModel> nodes = JsonConvert.DeserializeObject<UoFrameworkModel>(API.API.GetJSON("frameworks/" + frameworkId, _Configuration)).Nodes;

                foreach (UoNodeModel node in nodes)
                {
                    NodeModels.Add(node);
                }

            }
            else
            {
                NodeModels.Clear();
            }
        }

        public IActionResult OnPostSubmit(List<CourseMappings> courseMappings)
        {

            //get program
            UoProgramModel programModel = JsonConvert.DeserializeObject<UoProgramModel>(API.API.GetJSON("programs/" + programId, _Configuration));


            UoCourseModel[] CourseModels = new UoCourseModel[(int)programModel.Duration * 8];

            CourseModel.CatalogNumber = CourseCode[4..];

            CourseModel.Subject = CourseCode.Substring(0, 4);

            // get courses

            int[] courseIds = new int[CourseModels.Length]; 

            // if course list is passed
            if(CourseListJson != "[NaN]")
            {
               courseIds = JsonConvert.DeserializeObject<int[]>(CourseListJson); // load course list into array
            }


            // create course object to be posted
            string jsonStr = new JObject(new JProperty("subject", CourseModel.Subject),
                                        new JProperty("catalogNumber", CourseModel.CatalogNumber),
                                        new JProperty("name", CourseModel.Name),
                                        new JProperty("description", CourseModel.Description),
                                        new JProperty("units", CourseModel.Units),
                                        new JProperty("position", CourseModel.Position),
                                        new JProperty("academicOrgId", CourseModel.AcademicOrgId), 
                                        new JProperty("fieldOfEducationId", CourseModel.FieldOfEducationId)).ToString();


            // Post course
            CourseModel = JsonConvert.DeserializeObject<UoCourseModel>(API.API.PostJSON(jsonStr, "courses", _Configuration));

            int index = ((Year - 1) * 8) + ((Semester - 1) * 4) + Pos; // calculate index of course

            CourseModel.Position = index; // set pos

            // save index in course list
            CourseModels[index] = CourseModel;
            courseIds[index] = CourseModel.CourseId;



            if (LevelCategoryJson != "NaN")
            {
                List<int> LevelCatIds = JsonConvert.DeserializeObject<List<int>>(LevelCategoryJson);


                for (int i = 0; i < LevelCatIds.Count; i++)
                {
                    LevelCategoryModels.Add(JsonConvert.DeserializeObject<UoLevelCategoryModel>(API.API.GetJSON("level-categories/" + LevelCatIds[i], _Configuration)));

                }

            }

            // create course comp mappings
            if (courseMappings != null)
            {
                for (int i = 0; i < courseMappings.Count; i++)
                {
                    if(courseMappings[i].NodeId != -1)
                    {
                        if (courseMappings[i].Taught) // if Comp is taught
                        {
                            UoLevelCoursesJoin taughtCourse = new(); // create new join
                                                                     // fill join with form data
                            taughtCourse.CourseId = CourseModel.CourseId;
                            taughtCourse.NodeId = courseMappings[i].NodeId;


                            foreach (UoLevelModel levelModel in LevelCategoryModels[1].Levels)
                            {
                                if (levelModel.Name == "Taught")
                                {
                                    taughtCourse.LevelId = levelModel.LevelId;
                                    break;
                                }
                            }

                            LevelCoursesJoins.Add(taughtCourse);
                        }

                        if (courseMappings[i].Practiced)
                        {
                            UoLevelCoursesJoin practicedCourse = new(); // create new join
                                                                        // fill join with form data
                            practicedCourse.CourseId = CourseModel.CourseId;
                            practicedCourse.NodeId = courseMappings[i].NodeId;


                            foreach (UoLevelModel levelModel in LevelCategoryModels[1].Levels)
                            {
                                if (levelModel.Name == "Practiced")
                                {
                                    practicedCourse.LevelId = levelModel.LevelId;
                                    break;
                                }
                            }

                            LevelCoursesJoins.Add(practicedCourse);
                        }

                        if (courseMappings[i].Assessed)
                        {
                            UoLevelCoursesJoin assessedCourse = new(); // create new join
                                                                       // fill join with form data
                            assessedCourse.CourseId = CourseModel.CourseId;
                            assessedCourse.NodeId = courseMappings[i].NodeId;


                            foreach (UoLevelModel levelModel in LevelCategoryModels[1].Levels)
                            {
                                if (levelModel.Name == "Assessed")
                                {
                                    assessedCourse.LevelId = levelModel.LevelId;
                                    break;
                                }
                            }

                            LevelCoursesJoins.Add(assessedCourse);
                        }
                    }

                   


                }


                // join course and comp and levels in joining table
                API.Joiner.CourseToCompToLevels(LevelCoursesJoins, _Configuration);

            }
            else // invaild post
            {

            }



            CourseListJson = JsonConvert.SerializeObject(courseIds);

            return RedirectToPage("CourseList", new{programId = programId, projectId = projectId, CourseListJson = CourseListJson, LevelCategoryJson = LevelCategoryJson});


        }
        public ActionResult OnPostGetNodeDescription()
        {
            int NodeId = 0;
            {
                MemoryStream memoryStream = new();
                Request.Body.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                using (StreamReader r = new StreamReader(memoryStream))
                {
                    string tmp = r.ReadToEnd();

                    tmp = tmp.Trim();
                    tmp = tmp.Substring(8, tmp.Length - 10);

                    NodeId = int.Parse(tmp);

                }
            }

            string output = JsonConvert.DeserializeObject<UoNodeModel>(API.API.GetJSON("nodes/" + NodeId, _Configuration)).Name;

            return Content(output);

        }
        public ActionResult OnPostAddMapping()
        {


            int num = 0;
            {
                MemoryStream memoryStream = new();
                Request.Body.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                using (StreamReader r = new StreamReader(memoryStream))
                {


                    string tmp = r.ReadToEnd();
                    tmp = tmp.Trim();


                    num = JObject.Parse(tmp).Value<int>("num");
                    projectId = JObject.Parse(tmp).Value<int>("projectId");

                }

            }

            
            // get competencies
            if (projectId != 0)
            {
                frameworkId = (int)JsonConvert.DeserializeObject<UoProjectModel>(API.API.GetJSON("projects/" + projectId, _Configuration)).Framework.FrameworkId;

                List<UoNodeModel> nodes = JsonConvert.DeserializeObject<UoFrameworkModel>(API.API.GetJSON("frameworks/" + frameworkId, _Configuration)).Nodes;

                foreach (UoNodeModel node in nodes)
                {
                    NodeModels.Add(node);
                }

            }
            else
            {
                NodeModels.Clear();
            }


            return CourseMappingView(num);

        }


        public void OnPostNumOfCompetency()
        {




            if (LevelCategoryJson != "NaN")
            {
                List<int> LevelCatIds = JsonConvert.DeserializeObject<List<int>>(LevelCategoryJson);


                for (int i = 0; i < LevelCatIds.Count; i++)
                {
                    LevelCategoryModels.Add(JsonConvert.DeserializeObject<UoLevelCategoryModel>(API.API.GetJSON("level-categories/" + LevelCatIds[i], _Configuration)));

                }

            }



            // get competencies
            if (projectId != 0)
            {
                frameworkId = (int)JsonConvert.DeserializeObject<UoProjectModel>(API.API.GetJSON("projects/" + projectId, _Configuration)).Framework.FrameworkId;

                List<UoNodeModel> nodes = JsonConvert.DeserializeObject<UoFrameworkModel>(API.API.GetJSON("frameworks/" + frameworkId, _Configuration)).Nodes;

                foreach (UoNodeModel node in nodes)
                {
                    NodeModels.Add(node);
                }

            }
            else
            {
                NodeModels.Clear();
            }

            for (int i = 0; i < NumOfComp; i++)
            {
                LevelCoursesJoins.Add(new());
            }
            

        }

        //returns a list of Academic Orgs in the database as JSON
        public ActionResult OnPostGetAcademicOrg()
        {
            return Content(API.API.GetJSON("academic-orgs", _Configuration));
        }

        public ActionResult OnPostGetFieldOfEdu()
        {
            return Content(API.API.GetJSON("fields-of-education", _Configuration));
        }

        [NonAction]
        public virtual PartialViewResult CourseMappingView(int index)
        {

            frameworkId = (int)JsonConvert.DeserializeObject<UoProjectModel>(API.API.GetJSON("projects/" + projectId, _Configuration)).Framework.FrameworkId;



            string view = "../../Shared/Templates/" + JsonConvert.DeserializeObject<UoFrameworkModel>(API.API.GetJSON("frameworks/" + frameworkId, _Configuration)).TemplateName + "/_AddCourseMapping";

            NumOfComp = index;

            var resultData = new ViewDataDictionary<AddCourseModel>(ViewData);


            return new PartialViewResult()
            {
                ViewName = view,
                ViewData = resultData
        };
        }

    }
}
