using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace UoN.AccrediTool.Web.Pages
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Project implementation may use a resource manager and remove this supression")]
    public class AccessDeniedModel : PageModel
    {
        private readonly ILogger<AccessDeniedModel> _logger;
        private readonly IConfiguration _Configuration;

        public AccessDeniedModel(ILogger<AccessDeniedModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _Configuration = configuration;
        }

        public void OnGet(Uri returnUrl)
        {
            var IdClaimType = _Configuration["Okta:IdClaimType"];
            string loginId = User.Claims.FirstOrDefault(x => x.Type.Equals(IdClaimType, StringComparison.OrdinalIgnoreCase))?.Value;
            _logger.LogError("{0} - Unauthorised access to {1} by {2}, login id {3}", Activity.Current?.Id ?? HttpContext.TraceIdentifier, returnUrl, User.Identity.Name, loginId);
        }
    }
}
