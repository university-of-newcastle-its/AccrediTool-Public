using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

//JSON.net
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Web.Pages.NewProject.Course
{
   // [Authorize]
    public class AddCourseInstanceModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int CourseId { get; set; }

        [BindProperty]
        public UoCourseInstanceModel CourseInstanceModel { get; set; } = new();

        [BindProperty]
        public int Semester { get; set; }

        public List<UoCampusModel> CampusModels { get; }

        public UoCourseModel CourseModel { get; set; }

        public List<UoAssessmentTypeModel> AssessmentTypeModels {get;}

        private readonly IConfiguration _Configuration;

        public AddCourseInstanceModel(IConfiguration configuration)
        {
            _Configuration = configuration;

            CampusModels = JsonConvert.DeserializeObject<List<UoCampusModel>>(API.API.GetJSON("campuses", _Configuration));

            AssessmentTypeModels = JsonConvert.DeserializeObject<List<UoAssessmentTypeModel>>(API.API.GetJSON("assessment-types", _Configuration));
        }





        public void OnGet()
        {
            CourseModel = JsonConvert.DeserializeObject<UoCourseModel>(API.API.GetJSON("courses/" + CourseId, _Configuration));


        }

        public IActionResult OnPost(List<UoAssessmentModel> assessmentModels, List<UoLearningOutcomeModel> learningOutcomeModels)
        {


            string termCode = "" + UoCoreModels.GetYearCode(CourseInstanceModel.Year);

            termCode += Semester;

            CultureInfo cultureInfo = new("en-AU");

            CourseInstanceModel.TermCode = int.Parse(termCode, cultureInfo);

            if(assessmentModels != null)
            {
                for (int i = 0; i < assessmentModels.Count; i++)
                {
                    assessmentModels[i].Position = (i+1);

                    CourseInstanceModel.Assessments.Add(assessmentModels[i]);
                }

            }


            if (learningOutcomeModels != null)
            {
                for (int i = 0; i < learningOutcomeModels.Count; i++)
                {
                    learningOutcomeModels[i].Position = (i+1);

                    CourseInstanceModel.LearningOutcomes.Add(learningOutcomeModels[i]);
                }

            }

            CourseInstanceModel.CourseId = CourseId;



            JObject json = new(new JProperty("termCode", CourseInstanceModel.TermCode),
                                        new JProperty("year", CourseInstanceModel.Year),
                                        new JProperty("assessments", 
                                        new JArray(
                                             from a in CourseInstanceModel.Assessments
                                             select new JObject(
                                                 new JProperty("position", a.Position),
                                                 new JProperty("name", a.Name),
                                                 new JProperty("weight", a.Weight),
                                                 new JProperty("assessmentTypeId", a.AssessmentTypeId)))),
                                        new JProperty("learningOutcomes",
                                        new JArray(
                                            from l in CourseInstanceModel.LearningOutcomes
                                            select new JObject(
                                                 new JProperty("position", l.Position),
                                                 new JProperty("name", l.Name)))),
                                        new JProperty("campusId", CourseInstanceModel.CampusId),
                                        new JProperty("courseId", CourseInstanceModel.CourseId));
                        



            CourseInstanceModel = JsonConvert.DeserializeObject<UoCourseInstanceModel>(API.API.PostJSON(json.ToString(), "course-instances", _Configuration));



            return RedirectToPage("CourseList");
        }

    }
}
