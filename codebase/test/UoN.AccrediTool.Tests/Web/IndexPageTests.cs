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
        [Fact]
        public void IndexModelTest()
        {
            var logger = Mock.Of<ILogger<IndexModel>>();
            var config = Mock.Of<IConfiguration>();
            var pageModel = new IndexModel(logger, config);
            pageModel.OnGet();
        }
    }
}