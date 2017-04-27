using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using x.account.Utility;

[assembly: OwinStartup(typeof(x.account.App_Start.Startup))]

namespace x.account.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = MyOwinConfig.AuthenticationType,
                LoginPath = new PathString("/Account/Login"),
                CookieName = MyOwinConfig.CookieName,
                //Provider = new CookieAuthenticationProvider
                //{
                //    // 当用户登录时使应用程序可以验证安全戳。
                //    // 这是一项安全功能，当你更改密码或者向帐户添加外部登录名时，将使用此功能。
                //    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                //        validateInterval: TimeSpan.FromMinutes(30),
                //        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                //}
                CookieSecure = CookieSecureOption.SameAsRequest,
                //tested
                ExpireTimeSpan = TimeSpan.FromHours(4)
            });
        }
    }
}
