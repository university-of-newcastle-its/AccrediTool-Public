using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using UoN.AccrediTool.Service.Models;

namespace UoN.AccrediTool.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly ILogger<SecurityController> _logger;
        private readonly IConfiguration _configuration;

        public SecurityController(ILogger<SecurityController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
        public IActionResult Login([FromBody] UoLoginModel model)
        {
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - login attempted by {1}", traceId, model?.Username);
            var userSecret = _configuration["Security:UserMappings:" + model.Username + ":Secret"];
            var userRoles = _configuration["Security:UserMappings:" + model.Username + ":Secret"];
            if (!string.IsNullOrEmpty(userSecret) && !string.IsNullOrEmpty(userRoles) && userSecret.Equals(model.Secret, StringComparison.InvariantCulture))
            {
                var roleList = userRoles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in roleList)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Security:JWT:Secret"]));
                double expireHours;
                if (!double.TryParse(_configuration["Security:JWT:ExpireHours"], out expireHours))
                {
                    expireHours = 1;
                }
                var token = new JwtSecurityToken(
                    issuer: _configuration["Security:JWT:ValidIssuer"],
                    audience: _configuration["Security:JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(expireHours),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                _logger.LogInformation("{0} - Web API - login successful for {1}", traceId, model.Username);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            _logger.LogInformation("{0} - Web API - login failed for {1}", traceId, model.Username);
            return Unauthorized();
        }
    }
}
