using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandyModel.Entity.Abstracts;

namespace x.domain.Entities
{
    public class xt_web_group : hy_Entity
    {
        public xt_web_group()
        {
            xt_webs = new HashSet<xt_web>();
        }

        public string name { get; set; }

        public ICollection<xt_web> xt_webs { get; set; }
    }
}
