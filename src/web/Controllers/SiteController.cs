using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using x.account.Models;
using x.domain;
using x.domain.Entities;

namespace x.account.Controllers
{
    [Authorize]
    public class SiteController : BaseController
    {
        // GET: Site
        public ActionResult AddSite()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSite(SiteViewModel model)
        {
            return RedirectToLocal(model, () =>
            {
                using (var context = new XAccountContext { LoginId = LoginId })
                {
                    var entity = new xt_web
                    {
                        id = Guid.NewGuid().ToString(),
                        site = model.Site,
                        web_name = model.WebName,
                        xt_user = context.xt_user.Find(LoginId)
                    };
                    context.Add(entity);
                    context.SaveChanges();
                }
            }, "/Home/Index");
        }
    }
}