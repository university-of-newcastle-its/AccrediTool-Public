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
    public class CompetenciesModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int frameworkId { get; set; }

        public List<UoNodeModel> NodeModels { get; set; } = new();

        private readonly IConfiguration _Configuration;

        public CompetenciesModel(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public void OnGet()
        {

            UoFrameworkModel frameworkModel = JsonConvert.DeserializeObject<UoFrameworkModel>(API.API.GetJSON("frameworks/" + frameworkId, _Configuration ));


            NodeModels = frameworkModel.Nodes;

            for(int i = 0; i < NodeModels.Count; i++)
            {
                NodeModels[i] = JsonConvert.DeserializeObject<UoNodeModel>(API.API.GetJSON("nodes/" + NodeModels[i].NodeId, _Configuration));
            }

            
        }
    }
}
