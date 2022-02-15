using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; 
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

//JSON.net
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Web.Pages
{
    //[Authorize]
    public class NewNodeModel : PageModel
    {
        [BindProperty]
        public int NumOfChildNodes {get; set;}

        [BindProperty]
        public List<UoNodeModel> NodeModels { get; } = new List<UoNodeModel>();

        [BindProperty]
        public UoNodeModel Node { get; set; }

        private readonly IConfiguration _Configuration;

        public NewNodeModel(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        [BindProperty(SupportsGet = true)]
        public int programId {get;set;}

        [BindProperty(SupportsGet = true)]
        public string LevelCategoryJson {get; set;}


        public void OnPostChildNodes()
        {

            if (Request.Cookies.ContainsKey("nodeIdCookie"))
            {
                List<int> nodeIds = new(JsonConvert.DeserializeObject<List<int>>(Request.Cookies["nodeIdCookie"]));

                foreach (int nodeId in nodeIds)
                {
                    NodeModels.Add(JsonConvert.DeserializeObject<UoNodeModel>(API.API.GetJSON("nodes/" + nodeId, _Configuration)));
                }

            }
            else
            {
                NodeModels.Clear();
            }

            for (int i = 0; i < NumOfChildNodes; i++)
            {
                Node.ChildNodes.Add(new UoNodeModel());
            }



        }

        public ActionResult OnPostSubmit()
        {




            Node.FrameworkId = 0;


            List<int> nodeIds = new();

            if (Request.Cookies.ContainsKey("nodeIdCookie"))
            {
                nodeIds = new(JsonConvert.DeserializeObject<List<int>>(Request.Cookies["nodeIdCookie"]));
                Node.Position = nodeIds.Count+1;

            }
            else
            {
                Node.Position = nodeIds.Count+1;
            }

            for(int i = 0; i < Node.ChildNodes.Count; i++)
            {
                Node.ChildNodes[i].Position = i+1;
            }


            Node = JsonConvert.DeserializeObject<UoNodeModel>(API.API.PostJSON(JsonConvert.SerializeObject(Node, Formatting.Indented), "nodes", _Configuration));

            Node.Position = nodeIds.Count;


            nodeIds.Add(Node.NodeId);

            Response.Cookies.Append("nodeIdCookie", JsonConvert.SerializeObject(nodeIds, Formatting.Indented));



            return RedirectToPage("TemplateInfo", new {programId = programId, LevelCategoryJson = LevelCategoryJson});
        }

        public void OnGet()
        {

            if (Request.Cookies.ContainsKey("nodeIdCookie"))
            {
                List<int> nodeIds = new(JsonConvert.DeserializeObject<List<int>>(Request.Cookies["nodeIdCookie"]));

                foreach (int nodeId in nodeIds)
                {
                    NodeModels.Add(JsonConvert.DeserializeObject<UoNodeModel>(API.API.GetJSON("nodes/" + nodeId, _Configuration)));
                }

            }
            else
            {
                NodeModels.Clear();
            }


        }

    }
    
}