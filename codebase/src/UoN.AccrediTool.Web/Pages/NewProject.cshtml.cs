using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Microsoft.Extensions.Configuration;


//JSON.net
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;


namespace UoN.AccrediTool.Web.Pages
{
    // [Authorize]
    public class NewProjectModel : PageModel
    {
        [BindProperty]
        public List<UoProgramModel> ProgramModel { get; } = new();
                
        [BindProperty]
        public UoProjectModel ProjectModel  {get;set;}

        [BindProperty]
        public int Year { get; set; }

        [BindProperty]
        public int Semester { get; set; }

        [BindProperty]
        public int ProgramCount {get; set;}

        public List<UoFrameworkModel> FrameworkModels { get; }
        public List<UoCampusModel> CampusModels { get; }
        public List<UoProgramCareerModel> ProgramCareerModels  { get; }
        public List<UoFieldOfEducationModel> FieldOfEducationModels { get; } 

        public bool IsValid { get; set; } = true;
        private readonly IConfiguration _Configuration;

        public NewProjectModel(IConfiguration configuration)
        {
            _Configuration = configuration;

            FrameworkModels = JsonConvert.DeserializeObject<List<UoFrameworkModel>>(API.API.GetJSON("frameworks", _Configuration));

            CampusModels = JsonConvert.DeserializeObject<List<UoCampusModel>>(API.API.GetJSON("campuses", _Configuration));

            ProgramCareerModels = JsonConvert.DeserializeObject<List<UoProgramCareerModel>>(API.API.GetJSON("program-careers", _Configuration));

            FieldOfEducationModels = JsonConvert.DeserializeObject<List<UoFieldOfEducationModel>>(API.API.GetJSON("fields-of-education", _Configuration));
        }

        public ActionResult OnPostSubmit()
        {

            UoProjectModel.ShouldSerializeFrameworkIdBool = true;

            string termCode = "" + UoCoreModels.GetYearCode(Year);

            termCode += Semester;

            CultureInfo cultureInfo = new("en-AU");

            ProgramModel[0].FirstTermCode = int.Parse(termCode, cultureInfo);

            Response.Cookies.Append("projectModelCookie", JsonConvert.SerializeObject(ProjectModel, Formatting.None).ToString());

            string json = new JObject(new JProperty("programCode", ProgramModel[0].ProgramCode),
                            new JProperty("name", ProgramModel[0].Name),
                            new JProperty("firstTermCode", ProgramModel[0].FirstTermCode),
                            new JProperty("minUnits", ProgramModel[0].MinUnits),
                            new JProperty("duration", ProgramModel[0].Duration),
                            new JProperty("maxYears", ProgramModel[0].MaxYears),
                            new JProperty("campusId", ProgramModel[0].CampusId),
                            new JProperty("programCareerId", ProgramModel[0].ProgramCareerId),
                            new JProperty("fieldOfEducationId", ProgramModel[0].FieldOfEducationId)).ToString();

            //post program
            ProgramModel[0] = JsonConvert.DeserializeObject<UoProgramModel>(API.API.PostJSON(json, "programs", _Configuration));

            //Response.Cookies.Append("programIdCookie", JsonConvert.SerializeObject(ProgramModel[0].ProgramId, Formatting.None).ToString());

            return RedirectToPage("NewProject/TemplateInfo", new {programId = ProgramModel[0].ProgramId});





            


        }

        public void OnGetInvalidSubmit()
        {
            IsValid = false;
        }


        public void OnPostAddProgramCount()
        {
                for(int i = 0; i < ProgramCount; i++)
                {
                        ProgramModel.Add(new UoProgramModel());
                }

                        
        }

        public void OnGet()
        {


            ProgramCount = 1;
            OnPostAddProgramCount();
        }

        public void OnGetAddProgram()
        {
            ProgramCount++;
            OnPostAddProgramCount();
        }

        


    }
    
}