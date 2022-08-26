using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

using UoN.AccrediTool.Core.Models;


//JSON.net
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace UoN.AccrediTool.Web.Pages
{
    // [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _Configuration;

        public List<UoProjectModel> ProjectModels { get; set;  } 

        public IndexModel(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        // public string StaticPath { get; set; }
        public UoProgramModel Program { get; set; }


        public void OnGet()
        {
            ProjectModels = JsonConvert.DeserializeObject<List<UoProjectModel>>(API.API.GetJSON("projects/", _Configuration));


            if (ProjectModels != null)
            {
                ProjectModels.Reverse(); 
            }


        }

    }
}
