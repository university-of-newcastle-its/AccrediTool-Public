/**------------------------------------------------------------------------
 * ?                                ABOUT
 * @repo           : AccrediTool
 * @description    : This pagemodel provides a way for the user to download
 *                 : a AccrediTool project
 *------------------------------------------------------------------------**/
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


namespace UoN.AccrediTool.Web.Pages
{
    // [Authorize]
    public class DownloadProjectModel : PageModel
    {
        [BindProperty]
        public int projectId {get; set;}

        [BindProperty]
        public List<UoProjectModel> projectModels {get; set;}

        private readonly IConfiguration _Configuration;

        public DownloadProjectModel(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public void OnGet()
        {
            projectModels = JsonConvert.DeserializeObject<List<UoProjectModel>>(API.API.GetJSON("projects", _Configuration));
        }

        public IActionResult OnPost()
        {
            API.API.DownloadProject(projectId, _Configuration);

            return File(System.IO.File.ReadAllBytes("projects/" + projectId + ".zip") , MediaTypeNames.Application.Zip, "project-" + projectId + ".zip");
        }


    }


}