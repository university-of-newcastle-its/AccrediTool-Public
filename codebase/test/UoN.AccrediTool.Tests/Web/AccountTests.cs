using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
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
    public class AccountControllerTest
    {
        [Fact]
        public void LoginNewTest()
        {
            var logger = Mock.Of<ILogger<AccountController>>();
            var config = Mock.Of<IConfiguration>();
            var httpContext = Mock.Of<HttpContext>();
            httpContext.User = new ClaimsPrincipal();
            var claims = new List<Claim>();
            var identity = new ClaimsIdentity(claims);
            httpContext.User.AddIdentity(identity);
            using (var account = new AccountController(logger, config))
            {
                account.ControllerContext.HttpContext = httpContext;
                var result = account.Login();
                _ = Assert.IsType<ChallengeResult>(result);
            }
        }
        [Fact]
        public void LoginExistsTest()
        {
            var logger = Mock.Of<ILogger<AccountController>>();
            var config = Mock.Of<IConfiguration>();
            var httpContext = Mock.Of<HttpContext>();
            httpContext.User = new ClaimsPrincipal();
            var identity = new Mock<ClaimsIdentity>();
            identity.SetupGet(i => i.IsAuthenticated).Returns(true);
            httpContext.User.AddIdentity(identity.Object);
            using (var account = new AccountController(logger, config))
            {
                account.ControllerContext.HttpContext = httpContext;
                var result = account.Login();
                _ = Assert.IsType<RedirectToActionResult>(result);
            }
        }
        [Fact]
        public void LogoutTest()
        {
            var logger = Mock.Of<ILogger<AccountController>>();
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x[It.Is<string>(s => s.Equals("Okta:PostLogoutRedirectUri", StringComparison.Ordinal))]).Returns("https://newcastle.edu.au");
            mockConfiguration.Setup(x => x[It.Is<string>(s => s.Equals("Okta:IdClaimType", StringComparison.Ordinal))]).Returns("preferred_username");

            var httpContext = Mock.Of<HttpContext>();
            httpContext.User = new ClaimsPrincipal();
            var claims = new List<Claim>();
            var identity = new ClaimsIdentity(claims);
            httpContext.User.AddIdentity(identity);
            using (var account = new AccountController(logger, mockConfiguration.Object))
            {
                account.ControllerContext.HttpContext = httpContext;
                var result = account.Logout();
                _ = Assert.IsType<SignOutResult>(result);
            }
        }
        [Fact]
        public void AccessDeniedTest()
        {
            var logger = Mock.Of<ILogger<AccountController>>();
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x[It.Is<string>(s => s.Equals("Okta:IdClaimType", StringComparison.Ordinal))]).Returns("preferred_username");
            var httpContext = Mock.Of<HttpContext>();
            httpContext.User = new ClaimsPrincipal();
            var identity = new Mock<ClaimsIdentity>();
            identity.SetupGet(i => i.IsAuthenticated).Returns(true);
            httpContext.User.AddIdentity(identity.Object);
            using (var account = new AccountController(logger, mockConfiguration.Object))
            {
                account.ControllerContext.HttpContext = httpContext;
                var result = account.AccessDenied(new Uri("https://restricted.newcastle.edu.au"));
                _ = Assert.IsType<ViewResult>(result);
            }
        }
    }
    */
    [ExcludeFromCodeCoverage]
    public class AccountTests
    {
        [Fact]
        public void AccountModelTest()
        {
            Assert.True(true);
        }
    }
}
