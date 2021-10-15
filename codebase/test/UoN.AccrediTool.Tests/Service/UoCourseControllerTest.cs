using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

using UoN.AccrediTool.Core.Repositories;
using UoN.AccrediTool.Core.Models;
using UoN.AccrediTool.Service.Controllers;

namespace UoN.AccrediTool.Tests.Service
{
    [ExcludeFromCodeCoverage]
    public class UoCourseControllerTest
    {
        [Fact]
        public void GetTest()
        {
            var logger = Mock.Of<ILogger<UoCourseController>>();
            var businessObject = new Mock<IUoCourseRepository>();
            var httpContext = Mock.Of<HttpContext>(); 
            httpContext.User = new ClaimsPrincipal();
            var claims = new List<Claim>();
            var identity = new ClaimsIdentity(claims);
            httpContext.User.AddIdentity(identity);
            /*
            businessObject.Setup(x => x.GetCourseById(123)).Returns(CreateMockData);
            var service = new CourseController(logger);
            service.Repository = businessObject.Object;
            service.ControllerContext.HttpContext = httpContext;
            var result = service.Get(123);
            var expected = CreateMockData();
            Assert.Equal(result, expected);
            */
        }

        private UoCourseModel CreateMockData()
        {
            return new UoCourseModel();
        }
    }
}
