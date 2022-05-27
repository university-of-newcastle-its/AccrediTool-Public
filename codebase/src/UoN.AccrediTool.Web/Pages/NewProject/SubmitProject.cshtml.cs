using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;

using UoN.AccrediTool.Core.Models;
using UoN.AccrediTool.Core.Data;

using UoN.AccrediTool.Web.API;


//Json.NET
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UoN.AccrediTool.Web.Pages.NewProject
{
    // [Authorize]
    public class SubmitProjectModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string CourseListJson {get;set;}

        [BindProperty(SupportsGet =true)]
        public int programId {get;set;}      
        
        [BindProperty(SupportsGet =true)]
        public int projectId {get;set;}
                
        [BindProperty(SupportsGet = true)]
        public string LevelCategoryJson {get; set;}

        public Boolean displayDownload {get; set;} = false;

        UoFrameworkModel FrameworkModel = new();
        UoProjectModel ProjectModel = new();
        UoProgramModel ProgramModel = new();
        List<UoCourseListModel> CourseListModels { get; } = new();
        List<UoCourseModel> CourseModels { get; } = new();
        List<UoLevelCategoryModel> LevelCategoryModels { get; } = new();

        
        private bool isEdit = false;

        private readonly IConfiguration _Configuration;

        public SubmitProjectModel(IConfiguration configuration)
        {
            _Configuration = configuration;
        }


        public IActionResult OnGetDownload()
        {

           API.API.DownloadProject(projectId, _Configuration);



            return File(System.IO.File.ReadAllBytes("projects/" + projectId + ".zip") , MediaTypeNames.Application.Zip, "project-" + projectId + ".zip");
        }

        public void OnGetAPI()
        {
            GetModels();


  
            if(ProgramModel.ProgramStructure.Count == 0)
            {
                //post courselist
                for (int i = 0; i < ProgramModel.Duration; i++)
                {
                    string json = new JObject(new JProperty("position", i+1),
                                    new JProperty("name", "Year " + (i+1) + " Semester 1"),
                                    new JProperty("major", ProgramModel.Name),
                                    new JProperty("programId", ProgramModel.ProgramId)).ToString();

                    CourseListModels.Add(JsonConvert.DeserializeObject<UoCourseListModel>(API.API.PostJSON(json, "course-lists", _Configuration)));

                    json = new JObject(new JProperty("position", i+1),
                            new JProperty("name", "Year " + (i+1) + " Semester 2"),
                            new JProperty("major", ProgramModel.Name),
                            new JProperty("programId", ProgramModel.ProgramId)).ToString();

                    CourseListModels.Add(JsonConvert.DeserializeObject<UoCourseListModel>(API.API.PostJSON(json, "course-lists", _Configuration)));
                }
            }
            else
            {

                
                for(int i = 0; i < ProgramModel.ProgramStructure.Count; i++)
                {
                    CourseListModels.Add(ProgramModel.ProgramStructure[i]);
                }

                if(ProgramModel.ProgramStructure.Count != ProgramModel.Duration * 2)
                {
                    for(int i = ProgramModel.ProgramStructure.Count; i < ProgramModel.Duration; i++)
                    {
                        string json = new JObject(new JProperty("position", i+1),
                                        new JProperty("name", "Year " + (i+1) + " Semester 1"),
                                        new JProperty("major", ProgramModel.Name),
                                        new JProperty("programId", ProgramModel.ProgramId)).ToString();

                        CourseListModels.Add(JsonConvert.DeserializeObject<UoCourseListModel>(API.API.PostJSON(json, "course-lists", _Configuration)));

                        json = new JObject(new JProperty("position", i+1),
                                new JProperty("name", "Year " + (i+1) + " Semester 2"),
                                new JProperty("major", ProgramModel.Name),
                                new JProperty("programId", ProgramModel.ProgramId)).ToString();

                        CourseListModels.Add(JsonConvert.DeserializeObject<UoCourseListModel>(API.API.PostJSON(json, "course-lists", _Configuration)));
                    }
                }



                isEdit = true;
                List<UoCourseModel> courseModelsFromList = new();
                for (int i = 0; i < ProgramModel.ProgramStructure.Count; i++)
                {
                    
                    var courseListJson = JsonConvert.DeserializeObject<JObject>(API.API.GetJSON("course-lists/" + ProgramModel.ProgramStructure[i].CourseListId, _Configuration)).GetValue("courses");
                    bool getCourses = true;


                    if(courseListJson.First != null)
                    {
                        while(getCourses)
                        {

                            courseModelsFromList.Add(JsonConvert.DeserializeObject<UoCourseModel>(API.API.GetJSON("courses/" + courseListJson.First.First.First.ToString(), _Configuration)));

                            if(courseListJson.Next == null)
                            {
                                getCourses = false;
                            }
                            else
                            {
                                courseListJson = courseListJson.Next;
                            }
                        }
                    }
                }

                List<int> toRemove = new();
                //remove already linked courses
                for(int i = 0; i < CourseModels.Count; i++)
                {
                    for(int j = 0; j < courseModelsFromList.Count; j++)
                    {
 
                        if(CourseModels[i].CourseId ==  courseModelsFromList[j].CourseId)
                        {
                            toRemove.Add(i);
                        }
                        
                    }   
                }

                for(int i = 0; i < toRemove.Count; i++)
                {
                    CourseModels.RemoveAt(toRemove[i]);
                }

            }


            LinkModels();

            displayDownload = true;

        }

        public void LinkModels()
        {


            //link course to course lists
            for (int i = 0; i < CourseModels.Count; i++)
            {
                if(CourseModels[i] != null)
                {
                    for (int j = 0; j < CourseListModels.Count; j++)
                    {
                        if(CourseModels[i].Position < 4 * (j+1) && CourseModels[i].Position >= 4 * j)
                        {
                            Joiner.CourseToCourseList(CourseModels[i], CourseListModels[j], _Configuration);
                        }
                    }
                }
            }



            string json;


            if(!isEdit)
            {
                //Programs to project

                json = new JObject(new JProperty("programId", ProgramModel.ProgramId),
                                new JProperty("projectId", ProjectModel.ProjectId)).ToString();

                API.API.PostJSON(json, "project-programs", _Configuration);
            }




        }

        public void GetModels()
        {


            //get program
            if (programId != 0)
            {
                ProgramModel = JsonConvert.DeserializeObject<UoProgramModel>(API.API.GetJSON("programs/" + programId, _Configuration));
            }

            //get project
            if(projectId != 0)
            {
                ProjectModel = JsonConvert.DeserializeObject<UoProjectModel>(API.API.GetJSON("projects/" + projectId, _Configuration));
                FrameworkModel = ProjectModel.Framework;
            }



            if(CourseListJson != null)
            {
                List<int> courseIds = JsonConvert.DeserializeObject<List<int>>(CourseListJson);

                for (int i = 0; i < courseIds.Count; i++)
                {
                    if(courseIds[i] != 0)
                    {
                        CourseModels.Add(JsonConvert.DeserializeObject<UoCourseModel>(API.API.GetJSON("courses/" + courseIds[i], _Configuration)));
                        CourseModels[CourseModels.Count - 1].Position = i;
                    }
                   
                }
            }

            // get level cat
            if(LevelCategoryJson != null)
            {
                List<int> LevelCatIds = JsonConvert.DeserializeObject<List<int>>(LevelCategoryJson);

                for (int i = 0; i < LevelCatIds.Count; i++)
                {
                    LevelCategoryModels.Add(JsonConvert.DeserializeObject<UoLevelCategoryModel>(API.API.GetJSON("level-categories/" + LevelCatIds[i], _Configuration)));

                }

            }



        }
    }
}
