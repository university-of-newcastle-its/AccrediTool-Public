using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace UoN.AccrediTool.Core.Security
{
    public class UoDebugClaimProvider : IUoClaimProvider
    {
        IConfiguration _Configuration;
        public UoDebugClaimProvider(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }

        public List<Claim> GetClaims(ClaimsPrincipal principal)
        {
            var claims = new List<Claim>();

            if (principal == null)
                return claims;

            var roleMappings = _Configuration.GetSection("RoleMapping:Mappings").GetChildren();
            var IdClaimType = _Configuration["Okta:IdClaimType"];
            foreach (var roleMapping in roleMappings)
            {
                string roles = string.Empty;
                if (roleMapping.Key.Equals(principal.Claims.First(x => x.Type.Equals(IdClaimType, StringComparison.OrdinalIgnoreCase)).Value, StringComparison.OrdinalIgnoreCase))
                    roles = roleMapping.Value;

                var roleList = roles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach(var role in roleList)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Trim()));
                }
            }
            return claims;
        }
    }
}
