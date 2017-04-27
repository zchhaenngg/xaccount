using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace x.account.Utility
{
    public class MyOwinConfig
    {
        public static string CookieName => ConfigurationManager.AppSettings["Cookie"];
        public static string IdentityProvider => ConfigurationManager.AppSettings["IdentityProvider"];
        public static string AuthenticationType => ConfigurationManager.AppSettings["AuthenticationType"];
        public static TimeSpan LockoutTimeSpan   => TimeSpan.FromMinutes(30);
    }
}