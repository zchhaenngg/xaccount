using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandyModel.Entity.Abstracts;

namespace x.domain.Entities
{
    public class xt_user : hy_Creator
    {
        public xt_user()
        {
            xt_webs = new HashSet<xt_web>();
        }

        [Required]
        [StringLength(255)]
        public string username { get; set; }

        [Required]
        [StringLength(255)]
        public string password { get; set; }

        public virtual ICollection<xt_web> xt_webs { get; set; }
    }
}
