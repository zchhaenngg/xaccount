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
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            using (var context = new XAccountContext { LoginId = LoginId })
            {
                var list = context.AsNoTracking<xt_web>().Where(o => o.xt_user != null && o.xt_user.id == LoginId).OrderBy(o=>o.created_time).ToList();
                var viewModel = list.Select(o => new SiteEditViewModel
                {
                    Id = o.id,
                    Site = o.site,
                    WebName = o.web_name
                }).ToList();
                return View(viewModel);
            }
                
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}