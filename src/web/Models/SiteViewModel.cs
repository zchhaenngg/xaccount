using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace x.account.Models
{
    public class SiteViewModel
    {
        [Display(Name = "网址")]
        public string Site { get; set; }

        [Display(Name = "网站名称")]
        public string WebName { get; set; }
    }
    public class SiteEditViewModel : SiteViewModel
    {
        public string Id { get; set; }
    }
}