using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandyModel.Entity.Abstracts;

namespace x.domain.Entities
{
    public class xt_web_account : hy_Entity
    {
        [Required]
        [StringLength(255)]
        public string username { get; set; }

        [StringLength(255)]
        public string password { get; set; }

        public xt_web xt_web { get; set; }
        
    }
}
