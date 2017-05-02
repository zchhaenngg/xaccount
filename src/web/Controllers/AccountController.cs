using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using x.account.Models;
using x.account.Utility;
using x.domain;
using x.domain.Entities;

namespace x.account.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToAction("index", "home");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
            var result = OwinService.SignIn(model.UserName, model.Password, model.RememberMe);
            switch (result)
            {
                case SignInResult.Success:
                    return RedirectToLocal(returnUrl);
                case SignInResult.LockedOut:
                    return View("Lockout");
                case SignInResult.UserNameError:
                    ModelState.AddModelError("", "用户名不存在");
                    return View(model);
                case SignInResult.PasswordError:
                    ModelState.AddModelError("", "密码不正确");
                    return View(model);
                default:
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            return RedirectToLocal(model, () =>
            {
                using (var context = new XAccountContext())
                {
                    var entity = new xt_user
                    {
                        id = Guid.NewGuid().ToString(),
                        access_failed_times = 0,
                        username = model.UserName
                    };
                    entity.password = new PasswordHasher().HashPassword(model.Password);
                    context.Add(entity);
                    context.SaveChanges();
                }
            }, "/Account/Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            OwinService.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}