using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

using UoN.AccrediTool.Core.Security;

namespace UoN.AccrediTool.Tests.Core
{
    [ExcludeFromCodeCoverage]
    public class UoDebugClaimProviderTest
    {
        readonly string IdClaimTypeKey = "Okta:IdClaimType";
        readonly string IdClaimType = "preferred_username";
        readonly string RoleMappingKey = "RoleMapping:Mappings";
        readonly string testUser = "TEST123";
        readonly string testRoles = "TestRole1,TestRole2";

        [Fact]
        public void GetClaimsTest()
        {
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.Setup(x=>x.GetChildren()).Returns(MockRoleMappings);

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x[It.Is<string>(s => s.Equals(IdClaimTypeKey, StringComparison.Ordinal))]).Returns(IdClaimType);
            mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s.Equals(RoleMappingKey, StringComparison.Ordinal)))).Returns(mockConfSection.Object);

            var mockClaim = new Claim(IdClaimType, testUser);
            var mockClaims = new List<Claim>();
            mockClaims.Add(mockClaim);

            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.SetupGet(x => x.Claims).Returns(mockClaims);

            var provider = new UoDebugClaimProvider(mockConfiguration.Object);
            var claims = provider.GetClaims(mockPrincipal.Object);

            var roles = claims.Where(x => x.Type.Equals(ClaimTypes.Role, StringComparison.Ordinal)).Select(x => x.Value).Aggregate((first, second) => first + "," + second);
            Assert.Equal(testRoles, roles);
        }

        private IEnumerable<IConfigurationSection> MockRoleMappings()
        {
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(x => x.Key).Returns(testUser);
            mockConfSection.SetupGet(x => x.Value).Returns(testRoles);
            List<IConfigurationSection> roles = new List<IConfigurationSection>();
            roles.Add(mockConfSection.Object);

            return roles;
        }
    }
}
