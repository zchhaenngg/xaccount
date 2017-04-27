using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace x.account.Utility
{
    /// <summary>
    /// 提供注册、登陆等认证
    /// </summary>
    public interface IOWinService
    {
        SignInResult SignIn(string username, string password, bool isPersistent);
        void SignOut();
    }
    public enum SignInResult
    {
        // Summary:
        //     Sign in was successful
        Success = 1,
        /// <summary>
        /// 账号被锁定*一天内尝试n次登录都失败了
        /// </summary>
        LockedOut = 2,
        /// <summary>
        /// 用户名不正确
        /// </summary>
        UserNameError = 3,
        /// <summary>
        /// 密码不正确
        /// </summary>
        PasswordError = 4
    }
}