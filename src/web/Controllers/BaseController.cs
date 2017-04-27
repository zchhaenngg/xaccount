using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using x.account.Utility;

namespace x.account.Controllers
{
    public class BaseController : Controller
    {
        private IOWinService _oWinService;
        protected IOWinService OwinService => _oWinService ?? (_oWinService = new OWinService());

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (returnUrl != null)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        protected ActionResult RedirectToLocal<T>(T model, Action action, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                action();
                return RedirectToLocal(returnUrl);
            }
            return View(model);
        }

        private string GetModelStateString()
        {
            return string.Join("<br/>", ModelState.Select(o => o.Value).SelectMany(o => o.Errors).Select(o => o.ErrorMessage + o.Exception?.Message));
        }
    }
}