using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Security
{
    public class UoClaimProvider : IUoClaimProvider
    {
        static IConfiguration _Configuration;
        public UoClaimProvider(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }

        public List<Claim> GetClaims(ClaimsPrincipal principal)
        {
            var claims = new List<Claim>();

            if (principal == null)
                return claims;

            var IdClaimType = _Configuration["Okta:IdClaimType"];

            HttpClient client = new HttpClient();
            string uri = "/api/users/by-login/" + principal.Claims.First(x => x.Type.Equals(IdClaimType, StringComparison.OrdinalIgnoreCase)).Value;
            HttpResponseMessage response = client.GetAsync(new Uri(_Configuration["Services:UoN.AccrediTool.Service:BaseUri"] + uri)).Result;
            if (response.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<UoUserModel>(response.Content.ReadAsStringAsync().Result);
                foreach (var userGroup in user.UserGroups)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userGroup.Name));
                }
            }

            client.Dispose();
            return claims;
        }
    }
}
