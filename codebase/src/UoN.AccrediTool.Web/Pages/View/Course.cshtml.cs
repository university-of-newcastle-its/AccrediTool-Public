using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

//JSON.net
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Web.Pages.View
{
    public class CourseModel : PageModel
    {


        [BindProperty(SupportsGet = true)]
        public int courseId { get; set; }

        private readonly IConfiguration _Configuration;

        public CourseModel(IConfiguration configuration)
        {
            _Configuration = configuration;
        }


        public IActionResult OnGet()
        {
   

            return Content(API.API.GetJSON("courses/" + courseId + "/view", _Configuration), "text/html");
        }



    }
}
