using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using System.Security.Claims;
using System.Globalization;

using Microsoft.IdentityModel.Logging;

using UoN.AccrediTool.Core.Security;

namespace UoN.AccrediTool.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            IdentityModelEventSource.ShowPII = true; //TODO:

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = Configuration["Okta:Domain"] + "/oauth2/default";
                options.CallbackPath = new Microsoft.AspNetCore.Http.PathString(Configuration["Okta:CallbackPath"]);
                options.RequireHttpsMetadata = true;
                options.ClientId = Configuration["Okta:ClientId"];
                options.ClientSecret = Configuration["Okta:ClientSecret"];
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.GetClaimsFromUserInfoEndpoint = true;
                var scopes = Configuration["Okta:Scopes"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach(var scope in scopes)
                {
                    options.Scope.Add(scope.Trim());
                }
                options.SaveTokens = true;
                options.Events.OnTokenValidated = TokenValidated;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = Configuration["Okta:NameClaimType"],
                    //this will add OKTA claim (groups) as role if they are present in user attributes
                    RoleClaimType = Configuration["Okta:RoleClaimType"],
                    ValidateIssuer = true
                };


                
                //options.RequireHttpsMetadata = false; // NOTE: FOR DEV ONLY. remove in prod
                
                
                

            });

            services.AddAuthorization();
            services.AddRazorPages();
            services.AddMvc();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
        }

        /// <summary>
        /// This method provides a way to add custom claims (roles)
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task TokenValidated(TokenValidatedContext arg)
        {
            //This section checks if a custom role provider is available in config
            string roleProviderType = Configuration["RoleMapping:Provider"];
            if (!string.IsNullOrWhiteSpace(roleProviderType))
            {
                Type type = System.Type.GetType(roleProviderType);
                if (type != null)
                {
                    IUoClaimProvider roleProvider = (IUoClaimProvider)Activator.CreateInstance(type, Configuration);
                    var claims = roleProvider.GetClaims(arg.Principal);
                    var appIdentity = new ClaimsIdentity(claims);
                    arg.Principal.AddIdentity(appIdentity);
                }
            }
            return Task.CompletedTask;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#pragma warning disable CA1801 // Review unused parameters
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#pragma warning restore CA1801 // Review unused parameters
        {
            app.UseExceptionHandler(Configuration["Defaults:ErrorHandler"]);
            /* This section is commented out in the template but can be uncommented as per need
             if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Account/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }*/
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            
        }
    }
}
