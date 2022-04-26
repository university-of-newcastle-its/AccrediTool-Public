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
using UoN.AccrediTool.Core.Data;

//JSON.net
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace UoN.AccrediTool.Web.Pages
{
   // [Authorize]
    public class ViewProjectModel : PageModel
    {
        private readonly IConfiguration _Configuration;

        public UoProjectModel projectModel {get; set;}
        
        public List<UoProgramModel> programModels = new();

        public string levelCategoryJson {get; set;}

        public ViewProjectModel(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public void OnGet(int projectId)
        {
            projectModel = JsonConvert.DeserializeObject<UoProjectModel>(API.API.GetJSON("projects/" + projectId, _Configuration));

            for(int i = 0; i < projectModel.ProjectPrograms.Count; i++)
            {
                programModels.Add(JsonConvert.DeserializeObject<UoProgramModel>(API.API.GetJSON("programs/" + projectModel.ProjectPrograms[i].ProgramId, _Configuration)));
            }

            UoFrameworkModel frameworkModel = JsonConvert.DeserializeObject<UoFrameworkModel>(API.API.GetJSON("frameworks/" + projectModel.Framework.FrameworkId, _Configuration));

            List<UoLevelCategoryNodesJoin> allLevelCategoryNodes = JsonConvert.DeserializeObject<List<UoLevelCategoryNodesJoin>>(API.API.GetJSON("level-category-nodes", _Configuration));
            List<int> levelCategories = new();

            //get level cats linked to our nodes
            for (int i = 0; i < allLevelCategoryNodes.Count; i++)
            {
                if(frameworkModel.Nodes != null)
                    if(frameworkModel.Nodes[0].ChildNodes !=null)
                        if(allLevelCategoryNodes[i].NodeId == frameworkModel.Nodes[0].ChildNodes[0].NodeId) // faster then loop through all nodes and child node, but assumes all nodes will have the same level category. this is true for now but could change
                        {

                            levelCategories.Add(allLevelCategoryNodes[i].LevelCategoryId);
                        }
                
            }

            //remove duplicate levelCategory ids
            levelCategories.Distinct().ToList();


            levelCategoryJson = JsonConvert.SerializeObject(levelCategories, Formatting.None).ToString();

        }

        public IActionResult OnGetCourseList(int id)
        {
            List<UoCourseModel> courseModels = new();
            var courseListJson = JsonConvert.DeserializeObject<JObject>(API.API.GetJSON("course-lists/" + id, _Configuration)).GetValue("courses");
            bool getCourses = true;


            if(courseListJson.First != null)
            {
                while(getCourses)
                {

                    courseModels.Add(JsonConvert.DeserializeObject<UoCourseModel>(API.API.GetJSON("courses/" + courseListJson.First.First.First.ToString(), _Configuration)));

                    if(courseListJson.Next == null)
                    {
                        getCourses = false;
                    }
                    else
                    {
                        courseListJson = courseListJson.Next;
                    }
                }
            } 


            return Content(JsonConvert.SerializeObject(courseListJson));
        }


    }
}
