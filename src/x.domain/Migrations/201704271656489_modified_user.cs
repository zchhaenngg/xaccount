namespace x.domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modified_user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.xt_user", "access_failed_times", c => c.Int(nullable: false));
            AddColumn("dbo.xt_user", "unlock_time", c => c.DateTime(precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.xt_user", "unlock_time");
            DropColumn("dbo.xt_user", "access_failed_times");
        }
    }
}
