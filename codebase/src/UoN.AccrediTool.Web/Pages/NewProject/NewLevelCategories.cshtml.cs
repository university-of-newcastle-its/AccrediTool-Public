using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

//Json.NET
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Web.Pages.NewProject
{
    //[Authorize]
    public class NewLevelCategoriesModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string LevelCategoryJson {get; set;}
        
        [BindProperty]
        public UoLevelCategoryModel LevelCategoryModel { get; set; } = new UoLevelCategoryModel();

        [BindProperty]
        public List<UoLevelModel> LevelModels { get; } = new List<UoLevelModel>();
        [BindProperty]
        public int NumOfLevel { get; set; }

        private readonly IConfiguration _Configuration;

        public NewLevelCategoriesModel(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        [BindProperty(SupportsGet = true)]
        public int programId {get;set;}


        public void OnPostLevels()
        {
            for (int i = 0; i < NumOfLevel; i++)
            {
                LevelModels.Add(new UoLevelModel());
            }
        }

        public ActionResult OnPostSubmit()
        {



            //post level cat
            LevelCategoryModel = JsonConvert.DeserializeObject<UoLevelCategoryModel>(API.API.PostJSON(JsonConvert.SerializeObject(LevelCategoryModel, Formatting.Indented), "level-categories", _Configuration));


            List<int> LevelCatIds = new();



            if (LevelCategoryJson != null)
            {
                LevelCatIds = JsonConvert.DeserializeObject<List<int>>(LevelCategoryJson);
            }

            LevelCatIds.Add(LevelCategoryModel.LevelCategoryId);


            //post levels
            for (int i = 0; i < LevelModels.Count; i++)
            {
                string json = new JObject(new JProperty("position", LevelModels[i].Position),
                          new JProperty("name", LevelModels[i].Name),
                          new JProperty("description", LevelModels[i].Description),
                          new JProperty("levelCategoryId", LevelCategoryModel.LevelCategoryId)).ToString();

                LevelModels[i] = JsonConvert.DeserializeObject<UoLevelModel>(API.API.PostJSON(json, "levels", _Configuration));


            }




            LevelCategoryJson = JsonConvert.SerializeObject(LevelCatIds);

            return RedirectToPage("TemplateInfo", new {LevelCategoryJson = LevelCategoryJson, programId = programId});

        }
    }
}
