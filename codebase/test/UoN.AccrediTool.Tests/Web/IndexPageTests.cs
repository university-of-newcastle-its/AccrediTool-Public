using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

using UoN.AccrediTool.Web.Pages;

namespace UoN.AccrediTool.Tests.Web
{
    [ExcludeFromCodeCoverage]
    public class IndexPageTests
    {
        public static IConfiguration createConfig()
        {
            IConfigurationBuilder config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.Development.json");
            return config.Build();
        }



        [Fact]
        public void IndexModelTest()
        {
            var config = createConfig();
            var pageModel = new IndexModel(config);
            
           // pageModel.OnGet();
        }
    }
}