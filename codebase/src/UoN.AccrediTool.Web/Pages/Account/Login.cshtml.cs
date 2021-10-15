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

namespace UoN.AccrediTool.Web.Pages.Account
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Project implementation may use a resource manager and remove this supression")]
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IConfiguration _Configuration;

        public LoginModel(ILogger<LoginModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _Configuration = configuration;
        }

        public IActionResult OnGet()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Challenge(OpenIdConnectDefaults.AuthenticationScheme);
            }

            var IdClaimType = _Configuration["Okta:IdClaimType"];
            string loginId = User.Claims.FirstOrDefault(x => x.Type.Equals(IdClaimType, StringComparison.OrdinalIgnoreCase))?.Value;
            _logger.LogInformation("{0} - User {1} logged in, login id {2}", Activity.Current?.Id ?? HttpContext.TraceIdentifier, User.Identity.Name, loginId);
            
            return RedirectToPage("/Index");
        }
    }
}
