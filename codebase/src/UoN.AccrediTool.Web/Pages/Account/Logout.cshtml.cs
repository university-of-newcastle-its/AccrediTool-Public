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
    public class LogoutModel : PageModel
    {
        private readonly ILogger<LogoutModel> _logger;
        private readonly IConfiguration _Configuration;

        public LogoutModel(ILogger<LogoutModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _Configuration = configuration;
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(_Configuration["Okta:PostLogoutRedirectUri"]))
                throw new Exception("Post Logout URI is a required field. It can be a full or relative path");

            var IdClaimType = _Configuration["Okta:IdClaimType"];
            string loginId = User.Claims.FirstOrDefault(x => x.Type.Equals(IdClaimType, StringComparison.OrdinalIgnoreCase))?.Value;
            _logger.LogInformation("{0} - User {1} logging out, login id {2}", Activity.Current?.Id ?? HttpContext.TraceIdentifier, User.Identity.Name, loginId);

            return SignOut
            (
                new AuthenticationProperties { RedirectUri = _Configuration["Okta:PostLogoutRedirectUri"] },
                new[]
                {
                    OpenIdConnectDefaults.AuthenticationScheme,
                    CookieAuthenticationDefaults.AuthenticationScheme
                }
            );
        }
    }
}
