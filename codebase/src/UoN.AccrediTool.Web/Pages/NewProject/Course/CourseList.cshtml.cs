using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

using Newtonsoft.Json;


using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Web.Pages.NewProject.Course
{
    //[Authorize]
    public class CourseListModel : PageModel
    {
        
        public UoCourseModel[] CourseModels { get; set; }

        public UoProgramModel ProgramModel { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Year { get; set; } = 1;

        [BindProperty(SupportsGet =true)]
        public int programId {get; set;}

        
        [BindProperty(SupportsGet =true)]
        public int projectId {get; set;}
                
        [BindProperty(SupportsGet = true)]
        public string CourseListJson {get;set;}

        [BindProperty(SupportsGet = true)]
        public string LevelCategoryJson {get; set;}

        private readonly IConfiguration _Configuration;

        public CourseListModel(IConfiguration configuration)
        {
            _Configuration = configuration;
        }


        public void OnGet()
        {


            if(LevelCategoryJson == null)
            {
                LevelCategoryJson = "NaN";
            }


            //get program
            if(programId != 0)
            {
                ProgramModel = JsonConvert.DeserializeObject<UoProgramModel>(API.API.GetJSON("programs/" + programId, _Configuration));
            }
            else
            {
                //TODO: Error: program id must be passed to CourseList page.
            }
            
            
            if (Request.Cookies.ContainsKey("nodeIdCookie"))
            {
                Response.Cookies.Delete("nodeIdCookie");
            }




            // get course list
            if(CourseListJson != null)
            {
                int[] courseIds = JsonConvert.DeserializeObject<int[]>(CourseListJson);

                CourseModels = new UoCourseModel[courseIds.Length];

                // get courses
                for (int i = 0; i < courseIds.Length; i++)
                {
                    if(courseIds[i] != 0)
                    {
                        CourseModels[i] = JsonConvert.DeserializeObject<UoCourseModel>(API.API.GetJSON("courses/" + courseIds[i], _Configuration));
                    }
                    

                }
            }
            else
            {
                CourseModels = new UoCourseModel[(int)ProgramModel.Duration * 8];
                CourseListJson = "NaN";
            }




        }


    }
}
