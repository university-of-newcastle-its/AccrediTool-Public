using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using UoN.AccrediTool.Core.Models;
using UoN.AccrediTool.Core.Utility;

namespace UoN.AccrediTool.Web.Pages
{
     // [Authorize]
    public class CourseModel: PageModel, IDisposable
    {
        private readonly ILogger<CourseModel> _logger;
        private readonly IConfiguration _Configuration;
        private RestClient _client;
        private bool disposed = false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public CourseModel(ILogger<CourseModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _Configuration = configuration;
            if (!string.IsNullOrWhiteSpace(_Configuration["Services:UoN.AccrediTool.Service:BaseUri"]))
            {
                Uri baseUri = new Uri(_Configuration["Services:UoN.AccrediTool.Service:BaseUri"], UriKind.Absolute);
                Uri loginPath = new Uri(_Configuration["Services:UoN.AccrediTool.Service:LoginPath"], UriKind.Relative);
                _client = new RestClient(baseUri, loginPath,
                    new
                    {
                        Username = _Configuration["Services:UoN.AccrediTool.Service:LoginId"],
                        Secret = _Configuration["Services:UoN.AccrediTool.Service:LoginSecret"]
                    });
            }
        }

        public UoCourseModel Course { get; set; }

        public void OnGet()
        {
            // _ = _client?.Get<List<UoCourseModel>>(new Uri(_Configuration["Services:UoN.AccrediTool.Service:CoursePath"], UriKind.Relative));


            //TODO: this needs to be updated to fetch all the courses in the DB
            Course = new UoCourseModel
            {
                CourseId = 1,
                Subject = "CHEE",
                CatalogNumber = "1000",
                Name = "Chemical Engineering Principles",
                Description = "A particularly non-descript description.",
                FieldOfEducation = new UoFieldOfEducationModel
                {
                    FieldOfEducationId = 1,
                    Name = "Chemical Engineering"
                },
                Units = 10,
                AcademicOrg = new UoAcademicOrgModel
                {
                    AcademicOrgId = 1,
                    AcadCode = "SOE",
                    Name = "School of Engineering"
                }
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _client.Dispose();
                }
                disposed = true;
            }
        }
    }
}
