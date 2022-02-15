using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

//JSON.net
using Newtonsoft.Json;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Web.Pages.View
{
    public class CourseModel : PageModel
    {

        public UoCourseModel courseModel { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int courseId { get; set; }

        private readonly IConfiguration _Configuration;

        public CourseModel(IConfiguration configuration)
        {
            _Configuration = configuration;
        }


        public void OnGet()
        {
            courseModel = JsonConvert.DeserializeObject<UoCourseModel>(API.API.GetJSON("courses/" + courseId, _Configuration));

        }
    }
}
