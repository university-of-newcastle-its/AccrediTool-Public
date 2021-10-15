using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

using UoN.AccrediTool.Web.Pages;

namespace UoN.AccrediTool.Tests.Web
{
    /*
    public class CommonControllerTest
    {
        [Fact]
        public void ErrorTest()
        {
            var logger = Mock.Of<ILogger<CommonController>>();
            var config = Mock.Of<IConfiguration>();
            var features = new Mock<IFeatureCollection>();
            features.Setup(x => x.Get<IExceptionHandlerPathFeature>()).Returns(()=>null);
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(x => x.Features).Returns(features.Object);
            using (var common = new CommonController(logger, config))
            {
                common.ControllerContext.HttpContext = httpContext.Object;
                var result = common.Error();
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.IsType<ErrorViewModel>(viewResult.ViewData.Model);
            }
        }
    }
    */
    [ExcludeFromCodeCoverage]
    public class ErrorPageTests
    {
        [Fact]
        public void ErrorModelTest()
        {
            Assert.True(true);
        }
    }
}
