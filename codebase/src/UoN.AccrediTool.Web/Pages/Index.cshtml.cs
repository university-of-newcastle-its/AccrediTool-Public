using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

using UoN.AccrediTool.Core.Models;


//JSON.net
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace UoN.AccrediTool.Web.Pages
{
    //[Authorize]
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _Configuration;




        public IndexModel(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        // public string StaticPath { get; set; }
        public UoProgramModel Program { get; set; }


        public void OnGet()
        {

        }

        //returns a list of projects from the database between the upper and lower bounds
        public ActionResult OnGetProjects(int lower, int upper)
        {
            upper++;
            if(upper <= lower || lower < 0 || upper < 1) // prevent index out of bounds
            {
                return null;
            }


            List<UoProjectModel> projectModels = JsonConvert.DeserializeObject<List<UoProjectModel>>(API.API.GetJSON("projects/", _Configuration)); //get projects
            if(projectModels != null)
            {
                projectModels.Reverse(); // reverse list, this will make the latest created project at index 0

                if(projectModels.Count < upper)  // prevent index out of bounds
                {
                    upper = projectModels.Count;
                }

                List<UoProjectModel> requestedProjectModels = new();

                for(int i = lower; i < upper; i++) // Add all items between lower and upper to requestedProjectModels list.
                {
                    requestedProjectModels.Add(JsonConvert.DeserializeObject<UoProjectModel>(API.API.GetJSON("projects/" + projectModels[i].ProjectId, _Configuration)));
                }
    
                return Content(JsonConvert.SerializeObject(requestedProjectModels)); // returns projects.
            }
            else
            {
                return Content(null);
            }

        }

        // returns a specfic program
        public ActionResult OnGetPrograms(int id)
        {
            
            return Content(JsonConvert.SerializeObject((JsonConvert.DeserializeObject<UoProgramModel>(API.API.GetJSON("programs/" + id, _Configuration)))));
            

        }



    }
}
