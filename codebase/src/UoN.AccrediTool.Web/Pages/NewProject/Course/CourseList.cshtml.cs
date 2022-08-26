using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;


using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Web.Pages.NewProject.Course
{
    // [Authorize]
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

        public List<UoCourseModel> existingCourses {get; set;}

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
                throw new Exception("programId not given");
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

            /**=======================
            * *       INFO
            *  Load all courses, 
            *  used to add an existing course
            *========================**/

            existingCourses = JsonConvert.DeserializeObject<List<UoCourseModel>>(API.API.GetJSON("courses", _Configuration));


            /*---- END OF SECTION ----*/
            
            


        }


        /**========================================================================
        **                           OnPostCourseInfo
        *?  Gets course infomation via its id and returns the name and course code as json
        *@param existingCourseId int   
        *@return IActionResult - returns json via Content(obj)
        *========================================================================**/
        public IActionResult OnGetCourseInfo(int existingCourseId)
        {
            UoCourseModel existingCourseModel = JsonConvert.DeserializeObject<UoCourseModel>(API.API.GetJSON("courses/" + existingCourseId, _Configuration)); // get course

            if(existingCourseModel != null)
            {
                    // get need info and add to list of strings
                List<string> courseInfo = new();
                courseInfo.Add(existingCourseModel.Name);
                courseInfo.Add(existingCourseModel.Subject + existingCourseModel.CatalogNumber);

                return Content(JsonConvert.SerializeObject(courseInfo)); // return list as json
            }
            else
            {
                return null;
            }

        }
        /*---------------------------- END OF OnPostCourseInfo ----------------------------*/
        
        /**========================================================================
        **                           OnPostAddExistingCourse
        *?  Adds a course from the database into the courselist for the given project.
        *@param existingCourseId int - the courseId of the course being added  
        *@param pos int - the position of the course within a semester  
        *@param sem int - the semester of the course within a year  
        *@param year int - the year the course is in  
        *@return IActionResult
        *========================================================================**/
        public IActionResult OnPostAddExistingCourse(int existingCourseId, int pos, int sem, int year)
        {
            //get program
            if(programId != 0)
            {
                ProgramModel = JsonConvert.DeserializeObject<UoProgramModel>(API.API.GetJSON("programs/" + programId, _Configuration));
            }
            else
            {
                throw new Exception("programId not given");
            }

            if(ProgramModel != null)
            {
                int[] courseIds = new int[(int)ProgramModel.Duration * 8];

                if(CourseListJson != null && CourseListJson != "NaN")
                {
                    courseIds = JsonConvert.DeserializeObject<int[]>(CourseListJson); // get courses
                }



                int index = ((year - 1) * 8) + ((sem - 1) * 4) + pos; // calculate index of course

                //Add course to course list
                if(courseIds[index] == 0)
                {
                    courseIds[index] = existingCourseId;
                }
                else
                {
                    throw new Exception("courses slot not empty");
                }


                string url = "?CourseListJson=" + JsonConvert.SerializeObject(courseIds) + "&LevelCategoryJson="+ LevelCategoryJson + "&projectId="+ projectId + "&programId=" + programId;
                return Content(url);
            }
            else
            {
                return null;
            }



        }
        /*---------------------------- END OF OnPostAddExistingCourse ----------------------------*/
          
          


    }
}
