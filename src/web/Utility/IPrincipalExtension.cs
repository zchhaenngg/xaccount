using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace x.account.Utility
{
    public static class IPrincipalExtension
    {
        public static string GetLoginId(this IPrincipal principal)
        {
            return (principal as ClaimsPrincipal)?.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetLoginUsername(this IPrincipal principal)
        {
            return (principal as ClaimsPrincipal)?.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value;
        }
    }
}