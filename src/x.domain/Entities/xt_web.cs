using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandyModel.Entity.Abstracts;

namespace x.domain.Entities
{
    public class xt_web : hy_Entity
    {
        public xt_web()
        {
            xt_web_accounts = new HashSet<xt_web_account>();
        }

        [StringLength(255)]
        public string site { get; set; }
        
        [Required]
        [StringLength(255)]
        public string web_name { get; set; }

        public xt_user xt_user { get; set; }

        public ICollection<xt_web_account> xt_web_accounts { get; set; }

        public xt_web_group xt_web_group { get; set; }
    }
}
