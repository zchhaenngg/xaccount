namespace x.domain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using HandyContext;
    using x.domain.Entities;

    public partial class XAccountContext : HistoryDbContext<xt_history>
    {
        public XAccountContext()
            : base("name=XAccountContext")
        {
        }

        public virtual DbSet<xt_user> xt_user { get; set; }

        public virtual DbSet<xt_web> xt_web { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
