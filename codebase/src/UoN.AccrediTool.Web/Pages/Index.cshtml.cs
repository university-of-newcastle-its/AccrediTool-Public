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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
    [Authorize]
    public class IndexModel : PageModel, IDisposable
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _Configuration;
        private RestClient _client;
        private bool disposed = false;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
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

        // public string StaticPath { get; set; }
        public UoProgramModel Program { get; set; }


        public void OnGet()
        {
            // StaticPath = _Configuration["Services:UoN.AccrediTool.Service:StaticPath"];
            Program = _client?.Get<UoProgramModel>(new Uri(string.Join("/", _Configuration["Services:UoN.AccrediTool.Service:ProgramPath"], 1), UriKind.Relative));

            // Temporary fix if program is not found
            if (Program is null)
            {
                Program = new UoProgramModel();
            }
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
