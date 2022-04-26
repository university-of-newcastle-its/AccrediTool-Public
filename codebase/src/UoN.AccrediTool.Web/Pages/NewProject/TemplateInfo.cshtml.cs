using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.Extensions.Configuration;

//JSON.net
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


using UoN.AccrediTool.Core.Models;
using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Web.Pages
{
   // [Authorize]
    public class TemplateInfoModel : PageModel
    {

        public UoProjectModel ProjectModel { get; set; }
        public List<UoNodeModel> NodeModels { get; } = new();

        public List<UoLevelCategoryModel> LevelCategoryModels { get; } = new List<UoLevelCategoryModel>();

        [BindProperty(SupportsGet = true)]
        public string LevelCategoryJson {get; set;}

        [BindProperty]
        public UoFrameworkModel FrameworkModel { get; set; } = new();

        [BindProperty]
        public int NodeCount {get; set;}

        [BindProperty(SupportsGet = true)]
        public int programId {get;set;}

        private readonly IConfiguration _Configuration;

        public TemplateInfoModel(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public IActionResult OnPostSubmit()
        {

            


            if (Request.Cookies.ContainsKey("nodeIdCookie"))
            {
                List<int> nodeIds = new(JsonConvert.DeserializeObject<List<int>>(Request.Cookies["nodeIdCookie"]));

                foreach (int nodeId in nodeIds)
                {
                    NodeModels.Add(JsonConvert.DeserializeObject<UoNodeModel>(API.API.GetJSON("nodes/" + nodeId, _Configuration)));
                }

                FrameworkModel.Nodes = NodeModels;

                for (int i = 0; i < FrameworkModel.Nodes.Count; i++) // remove id from framework and delete old node. The Post request cannot contain node id new nodes MUST be made
                {


                    for (int j = 0; j < FrameworkModel.Nodes[i].ChildNodes.Count; j++) // remove childern
                    {
                        API.API.Delete("nodes/" + FrameworkModel.Nodes[i].ChildNodes[j].NodeId, _Configuration);
                        FrameworkModel.Nodes[i].ChildNodes[j].NodeId = 0;
                    }

                    API.API.Delete("nodes/" + FrameworkModel.Nodes[i].NodeId, _Configuration); // remove parent
                    FrameworkModel.Nodes[i].NodeId = 0;
                }




                FrameworkModel = JsonConvert.DeserializeObject<UoFrameworkModel>(API.API.PostJSON(JsonConvert.SerializeObject(FrameworkModel, Formatting.Indented), "frameworks", _Configuration));

                nodeIds.Clear();

                foreach (UoNodeModel node in FrameworkModel.Nodes)
                {
                    nodeIds.Add(node.NodeId);
                }

                Response.Cookies.Append("nodeIdCookie", JsonConvert.SerializeObject(nodeIds, Formatting.None).ToString());

                

                if (LevelCategoryJson != null)
                {
                    List<int> LevelCatIds = JsonConvert.DeserializeObject<List<int>>(LevelCategoryJson);

                    for (int i = 0; i < LevelCatIds.Count; i++)
                    {
                        LevelCategoryModels.Add(JsonConvert.DeserializeObject<UoLevelCategoryModel>(API.API.GetJSON("level-categories/" + LevelCatIds[i], _Configuration)));
                    }

                }


                string json;

                foreach (UoLevelCategoryModel categoryModel in LevelCategoryModels)
                {
                    foreach (UoNodeModel node in FrameworkModel.Nodes)
                    {
                        json = new JObject(new JProperty("levelCategoryId", categoryModel.LevelCategoryId),
                                            new JProperty("nodeId", node.NodeId)).ToString();

                        API.API.PostJSON(json, "level-category-nodes", _Configuration);

                        foreach (UoNodeModel childNode in node.ChildNodes)
                        {
                            json = new JObject(new JProperty("levelCategoryId", categoryModel.LevelCategoryId),
                                            new JProperty("nodeId", childNode.NodeId)).ToString();
                
                            API.API.PostJSON(json, "level-category-nodes", _Configuration);

                            
                        }
                    }
            }


            }
            else
            {
                //TODO:
                // tell use a framework must have nodes etc
            }

            if (Request.Cookies.ContainsKey("projectModelCookie"))
            {
                ProjectModel = JsonConvert.DeserializeObject<UoProjectModel>(Request.Cookies["projectModelCookie"]);


                //post project
                ProjectModel = JsonConvert.DeserializeObject<UoProjectModel>(API.API.PostJSON(new JObject(new JProperty("name", ProjectModel.Name), new JProperty("description", ProjectModel.Description), new JProperty("frameworkId", FrameworkModel.FrameworkId)).ToString(), "projects", _Configuration));

                //Response.Cookies.Append("projectIdCookie", JsonConvert.SerializeObject(ProjectModel.ProjectId, Formatting.None).ToString());

                Response.Cookies.Delete("projectModelCookie");
            }

            return RedirectToPage("Course/CourseList", new {programId = programId, projectId = ProjectModel.ProjectId, LevelCategoryJson = LevelCategoryJson});
        }


        public void OnGetNew()
        {
            if (Request.Cookies.ContainsKey("projectModelCookie"))
            {
                ProjectModel = JsonConvert.DeserializeObject<UoProjectModel>(Request.Cookies["projectModelCookie"]);
            }

            if (Request.Cookies.ContainsKey("nodeIdCookie"))
            {
                List<int> nodeIds = new(JsonConvert.DeserializeObject<List<int>>(Request.Cookies["nodeIdCookie"]));

                NodeModels.Clear();

                foreach (int nodeId in nodeIds)
                {
                    NodeModels.Add(JsonConvert.DeserializeObject<UoNodeModel>(API.API.GetJSON("nodes/" + nodeId, _Configuration)));
                }
                FrameworkModel.Nodes = NodeModels;
            }




            if (LevelCategoryJson != null)
            {
                List<int> LevelCatIds = JsonConvert.DeserializeObject<List<int>>(LevelCategoryJson);

                for (int i = 0; i < LevelCatIds.Count; i++)
                {
                    LevelCategoryModels.Add(JsonConvert.DeserializeObject<UoLevelCategoryModel>(API.API.GetJSON("level-categories/" + LevelCatIds[i], _Configuration)));
                }

            }
        }
        public IActionResult OnGet()
        {
            
            if (Request.Cookies.ContainsKey("projectModelCookie"))
            {
                ProjectModel = JsonConvert.DeserializeObject<UoProjectModel>(Request.Cookies["projectModelCookie"]);
            }

            if(ProjectModel.FrameworkId != 0)
            {


                FrameworkModel.FrameworkId = (int)ProjectModel.FrameworkId;

                //post project
                ProjectModel = JsonConvert.DeserializeObject<UoProjectModel>(API.API.PostJSON(new JObject(new JProperty("name", ProjectModel.Name), new JProperty("description", ProjectModel.Description), new JProperty("frameworkId", ProjectModel.FrameworkId)).ToString(), "projects", _Configuration));



                Response.Cookies.Delete("projectModelCookie");




                //get level cat ids

                List<UoNodeModel> Nodes = new();

                int frameworkId = (int)JsonConvert.DeserializeObject<UoProjectModel>(API.API.GetJSON("projects/" + ProjectModel.ProjectId, _Configuration)).Framework.FrameworkId;

                if (programId != 0)
                {
                    Nodes = JsonConvert.DeserializeObject<UoFrameworkModel>(API.API.GetJSON("frameworks/" + frameworkId, _Configuration)).Nodes;

                    foreach (UoNodeModel node in Nodes)
                    {
                        NodeModels.Add(node);
                    }

                }
                else
                {
                    NodeModels.Clear();
                }

                List<UoLevelCategoryNodesJoin> allLevelCategoryNodes = JsonConvert.DeserializeObject<List<UoLevelCategoryNodesJoin>>(API.API.GetJSON("level-category-nodes", _Configuration));
                List<int> levelCategories = new();

                //get level cats linked to our nodes
                for (int i = 0; i < allLevelCategoryNodes.Count; i++)
                {
                   if(Nodes != null)
                        if(Nodes[0].ChildNodes !=null)
                            if(allLevelCategoryNodes[i].NodeId == Nodes[0].ChildNodes[0].NodeId) // faster then loop through all nodes and child node, but assumes all nodes will have the same level category. this is true for now but could change
                            {

                                levelCategories.Add(allLevelCategoryNodes[i].LevelCategoryId);
                            }
                    
                }

                //remove duplicate levelCategory ids
                levelCategories.Distinct().ToList();


                LevelCategoryJson = JsonConvert.SerializeObject(levelCategories, Formatting.None).ToString();

                return RedirectToPage("Course/CourseList", new {programId = programId, projectId = ProjectModel.ProjectId, LevelCategoryJson = LevelCategoryJson});
            }



            return RedirectToPage("TemplateInfo", "New", new {programId = programId, projectId = ProjectModel.ProjectId, LevelCategoryJson = LevelCategoryJson});


        }



    }
    
}