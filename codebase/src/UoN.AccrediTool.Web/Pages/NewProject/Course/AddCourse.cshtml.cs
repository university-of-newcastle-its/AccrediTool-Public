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
   // [Authorize]
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

        [BindProperty(SupportsGet = true)]
        public bool loadCourse {get; set;}= false;

        [BindProperty(SupportsGet = true)]
        public int loadedCourseId {get;set;}
                
        [BindProperty(SupportsGet =true)]
        public int programId {get; set;}
        
        [BindProperty(SupportsGet =true)]
        public int projectId {get; set;}

        [BindProperty(SupportsGet = true)]
        public string LevelCategoryJson {get; set;}

        [BindProperty]
        public UoCourseModel CourseModel { get; set; }


        [BindProperty]
        public String CourseCode { get; set; }

        [BindProperty]
        public int NumOfComp { get; set; }

        [BindProperty]
        public int NumOfAssessments {get; set;}
        [BindProperty]
        public int NumOfLearningOutcome {get; set;}

        [BindProperty]
        public List<UoLevelCoursesJoin> LevelCoursesJoins { get; } = new();

        [BindProperty]
        public UoCourseInstanceModel CourseInstanceModel {get; set;} = new();

        public List<UoAssessmentTypeModel> AssessmentTypeModels {get; set;} = new();

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

            if(loadCourse)
            {
                UoCourseModel loadedCourseModel = JsonConvert.DeserializeObject<UoCourseModel>(API.API.GetJSON("courses/" + loadedCourseId, _Configuration));

                if(loadedCourseModel != null)
                {
                    CourseModel = loadedCourseModel;
                    CourseCode = CourseModel.Subject + CourseModel.CatalogNumber;
                }
                else
                {
                    loadCourse = false;
                    loadedCourseId = 0;
                }


            }
            else
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


  
        }

        public IActionResult OnPostSubmit(List<CourseMappings> courseMappings, List<UoAssessmentModel> assessmentModels, List<UoLearningOutcomeModel> learningOutcomeModels)
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

            int index = ((Year - 1) * 8) + ((Semester - 1) * 4) + Pos; // calculate index of course

            CourseModel.Position = index; // set pos


            // create course object to be posted
            string jsonStr = new JObject(new JProperty("subject", CourseModel.Subject),
                                        new JProperty("catalogNumber", CourseModel.CatalogNumber),
                                        new JProperty("name", CourseModel.Name),
                                        new JProperty("description", CourseModel.Description),
                                        new JProperty("units", CourseModel.Units),
                                        new JProperty("position", CourseModel.Position),
                                        new JProperty("academicOrgId", CourseModel.AcademicOrgId), 
                                        new JProperty("fieldOfEducationId", CourseModel.FieldOfEducationId)).ToString();

            
            if (LevelCategoryJson != "NaN")
            {
                List<int> LevelCatIds = JsonConvert.DeserializeObject<List<int>>(LevelCategoryJson);


                for (int i = 0; i < LevelCatIds.Count; i++)
                {
                    LevelCategoryModels.Add(JsonConvert.DeserializeObject<UoLevelCategoryModel>(API.API.GetJSON("level-categories/" + LevelCatIds[i], _Configuration)));

                }

            }


            if(loadCourse) // edit a existing course
            {

                //Update course via put request
                CourseModel = JsonConvert.DeserializeObject<UoCourseModel>(API.API.Update(jsonStr, "courses/" + loadedCourseId, _Configuration));
                CourseModel = JsonConvert.DeserializeObject<UoCourseModel>(API.API.GetJSON("courses/" + loadedCourseId, _Configuration));

                //remove existing course mappings
                API.API.Delete("level-courses/" + loadedCourseId, _Configuration);

                //remove existing course instance
                API.API.Delete("course-instances/course/" + loadedCourseId, _Configuration);

            }
            else // create a new course
            {
                // Post course
                CourseModel = JsonConvert.DeserializeObject<UoCourseModel>(API.API.PostJSON(jsonStr, "courses", _Configuration));


            }
            
                // save index in course list
                CourseModels[index] = CourseModel;
                courseIds[index] = CourseModel.CourseId;

            // create course comp mappings || TODO: this needs to have better supoort for templates.
            if (courseMappings != null)
            {
                for (int i = 0; i < courseMappings.Count; i++)
                {
                    if(courseMappings[i].NodeId != -1 || courseMappings[i].NodeId != -2)
                    {
                        if (courseMappings[i].Taught) // if Comp is taught
                        {
                            UoLevelCoursesJoin taughtCourse = new(); // create new join
                                                                    // fill join with form data
                            taughtCourse.CourseId = CourseModel.CourseId;
                            taughtCourse.NodeId = courseMappings[i].NodeId;


                            foreach (UoLevelModel levelModel in LevelCategoryModels.Where(levelCats => levelCats.Name == "Implied Pedagogy").First().Levels)
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


                            foreach (UoLevelModel levelModel in LevelCategoryModels.Where(levelCats => levelCats.Name == "Implied Pedagogy").First().Levels)
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


                            foreach (UoLevelModel levelModel in LevelCategoryModels.Where(levelCats => levelCats.Name == "Implied Pedagogy").First().Levels)
                            {
                                if (levelModel.Name == "Assessed")
                                {
                                    assessedCourse.LevelId = levelModel.LevelId;
                                    break;
                                }
                            }

                            LevelCoursesJoins.Add(assessedCourse);
                        }

                        //Blooms Level course joins

                        var levelCats = LevelCategoryModels.Where(levelCats => levelCats.Name == "Level of Capability").First().Levels;

                        for(int j = 0; j < levelCats.Count(); j++)
                        {
                            if(levelCats[j].Position == courseMappings[i].Level)
                            {
                                UoLevelCoursesJoin capability = new();
                                capability.CourseId = CourseModel.CourseId;
                                capability.NodeId = courseMappings[i].NodeId;
                                capability.LevelId = levelCats[j].LevelId;

                                LevelCoursesJoins.Add(capability);

                            }
                        }


                    }


                }

                // join course and comp and levels in joining table
                API.Joiner.CourseToCompToLevels(LevelCoursesJoins, _Configuration);

            }

            CourseListJson = JsonConvert.SerializeObject(courseIds);



            //===============================================================================
            //Create Course Instance
            //===============================================================================

            int termCode = 0;

            for(int i = 0; i < assessmentModels.Count; i++)
            {
                if(assessmentModels[i].Name == "remove")
                {
                    assessmentModels.RemoveAt(i);
                    i--;
                }
            }            
            
            
            for(int i = 0; i < learningOutcomeModels.Count; i++)
            {
                if(learningOutcomeModels[i].Name == "remove")
                {
                    learningOutcomeModels.RemoveAt(i);
                    i--;
                }
            }

        
            for(int i = 0; i < assessmentModels.Count; i++)
            {
                assessmentModels[i].Position = i +1;
            }            
            
            
            for(int i = 0; i < learningOutcomeModels.Count; i++)
            {
                learningOutcomeModels[i].Position = i +1;
            }


            if(Semester == 1)
            {
                termCode = int.Parse(UoCoreModels.GetYearCode(Year) + "40");
            }
            else
            {
                termCode = int.Parse(UoCoreModels.GetYearCode(Year) + "80");
            }

            JObject json = new(new JProperty("termCode", termCode),
                            new JProperty("year", Year),
                            new JProperty("assessments", 
                            new JArray(
                                    from a in assessmentModels
                                    select new JObject(
                                        new JProperty("position", a.Position),
                                        new JProperty("name", a.Name),
                                        new JProperty("weight", a.Weight),
                                        new JProperty("assessmentTypeId", a.AssessmentTypeId)))),
                            new JProperty("learningOutcomes",
                            new JArray(
                                from l in learningOutcomeModels
                                select new JObject(
                                        new JProperty("position", l.Position),
                                        new JProperty("name", l.Name)))),
                            new JProperty("campusId", programModel.Campus.CampusId),
                            new JProperty("courseId", CourseModel.CourseId));
                        



            string tmp = API.API.PostJSON(json.ToString(), "course-instances", _Configuration);

            return RedirectToPage("CourseList", new{programId = programId, projectId = projectId, CourseListJson = CourseListJson, LevelCategoryJson = LevelCategoryJson});

        }


        public ActionResult OnPostLoadMapping()
        {
            int courseId;
            
            MemoryStream memoryStream = new();
            Request.Body.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            using (StreamReader r = new StreamReader(memoryStream))
            {


                string tmp = r.ReadToEnd();
                tmp = tmp.Trim();


                loadCourse = JObject.Parse(tmp).Value<bool>("loadCourse");
                courseId = JObject.Parse(tmp).Value<int>("courseId");

            }

            if(loadCourse)
            {
                JArray json = JsonConvert.DeserializeObject<JArray>(API.API.GetJSON("level-courses/" + courseId, _Configuration));

                for(int i = 0; i < json.Count; i++)
                {
                    int levelId = json[i].Last.Previous.Last.Value<int>();

                    UoLevelModel level = JsonConvert.DeserializeObject<UoLevelModel>(API.API.GetJSON("levels/" + levelId, _Configuration));


                    json[i].Last.AddAfterSelf(JToken.Parse("{ \"levelCatName\" : \"" + level.LevelCategory.Name + "\"}").First);
                    json[i].Last.AddAfterSelf(JToken.Parse("{ \"levelName\" : \"" + level.Name + "\"}").First);


                }

                return Content(json.ToString());
            }
            else
            {
                return null;
            }
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

        public IActionResult OnPostAddAssessment()
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

                }

            }

            return AssesmentView(num);

        }

        public IActionResult OnPostAddLearningOutcome()
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

                }

            }

            return LearningOutcomeView(num);
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
        public virtual PartialViewResult LearningOutcomeView(int index)
        {

            NumOfLearningOutcome = index;

            var resultData = new ViewDataDictionary<AddCourseModel>(ViewData);

           

            return new PartialViewResult()
            {
                ViewName = "AddLearningOutcome",
                ViewData = resultData
             };
        }

        [NonAction]
        public virtual PartialViewResult AssesmentView(int index)
        {
            AssessmentTypeModels = JsonConvert.DeserializeObject<List<UoAssessmentTypeModel>>(API.API.GetJSON("assessment-types", _Configuration));

             NumOfAssessments = index;

            var resultData = new ViewDataDictionary<AddCourseModel>(ViewData);

           

            return new PartialViewResult()
            {
                ViewName = "AddAssessment",
                ViewData = resultData
             };
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
