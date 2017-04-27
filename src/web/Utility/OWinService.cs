using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using x.domain;
using x.domain.Entities;

namespace x.account.Utility
{
    public partial class OWinService : IOWinService
    {
        protected HttpRequest Request => HttpContext.Current.Request;

        /// <summary>
        /// Attempts to sign in the specified <paramref name="userName"/> and <paramref name="password"/>
        /// </summary>
        /// <param name="username">The email to sign in.</param>
        /// <param name="password">The password to attempt to sign in with.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <returns>The task object representing  the <see name="SignInResult"/>
        /// for the sign-in attempt.</returns>
        public SignInResult SignIn(string username, string password, bool isPersistent)
        {
            using (var context = new XAccountContext())
            {
                var entity = context.xt_user.FirstOrDefault(o => o.username == username);
                if (entity == null)
                {
                    return SignInResult.UserNameError;
                }
                if (entity.unlock_time != null && DateTime.Now > entity.unlock_time)
                {
                    return SignInResult.LockedOut;
                }
                var result = VerifyHashedPassword(entity, password);
                switch (result)
                {
                    case PasswordVerificationResult.Failed:
                        entity.access_failed_times++;
                        if (entity.access_failed_times > 3)
                        {
                            entity.unlock_time = DateTime.Now.Add(MyOwinConfig.LockoutTimeSpan);
                        }
                        context.SaveChanges();
                        return SignInResult.PasswordError;
                    case PasswordVerificationResult.Success:
                        entity.unlock_time = null;
                        entity.access_failed_times = 0;
                        SignIn(entity, isPersistent);
                        context.SaveChanges();
                        return SignInResult.Success;
                    case PasswordVerificationResult.SuccessRehashNeeded:
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public void SignOut()
        {
            Request.GetOwinContext().Authentication.SignOut(MyOwinConfig.AuthenticationType);
        }
    }
    public partial class OWinService
    {
        //私有方法
        private PasswordVerificationResult VerifyHashedPassword(xt_user entity, string password)
        {
            if (entity.password == null)
            {
                return PasswordVerificationResult.Failed;
            }
            else
            {
                return new PasswordHasher().VerifyHashedPassword(entity.password, password);
            }
        }
        private void SignIn(xt_user entity, bool isPersistent)
        {
            var claims = GetClaims(entity);
            var identity = new ClaimsIdentity(claims, MyOwinConfig.AuthenticationType);
            var authenticationProperties = new AuthenticationProperties { IsPersistent = isPersistent };
            Request.GetOwinContext().Authentication.SignIn(authenticationProperties, identity);
        }
        private IList<Claim> GetClaims(xt_user entity)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, entity.id));
            claims.Add(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", MyOwinConfig.IdentityProvider));
            claims.Add(new Claim(ClaimTypes.Name, entity.username));
            return claims;
        }
    }
}