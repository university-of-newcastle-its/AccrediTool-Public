using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace UoN.AccrediTool.Core.Security
{
    public interface IUoClaimProvider
    {
        List<Claim> GetClaims(ClaimsPrincipal principal);
    }
}
