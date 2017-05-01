namespace x.domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedALot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.xt_web_account",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 40, storeType: "nvarchar"),
                        username = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        password = c.String(maxLength: 255, storeType: "nvarchar"),
                        created_by_id = c.String(maxLength: 40, storeType: "nvarchar"),
                        created_time = c.DateTime(nullable: false, precision: 0),
                        last_modified_by_id = c.String(maxLength: 40, storeType: "nvarchar"),
                        last_modified_time = c.DateTime(nullable: false, precision: 0),
                        xt_web_id = c.String(maxLength: 40, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.xt_web", t => t.xt_web_id)
                .Index(t => t.xt_web_id);
            
            CreateTable(
                "dbo.xt_web_group",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 40, storeType: "nvarchar"),
                        name = c.String(unicode: false),
                        created_by_id = c.String(maxLength: 40, storeType: "nvarchar"),
                        created_time = c.DateTime(nullable: false, precision: 0),
                        last_modified_by_id = c.String(maxLength: 40, storeType: "nvarchar"),
                        last_modified_time = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.xt_user", "last_modified_by_id", c => c.String(maxLength: 40, storeType: "nvarchar"));
            AddColumn("dbo.xt_user", "last_modified_time", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.xt_web", "last_modified_by_id", c => c.String(maxLength: 40, storeType: "nvarchar"));
            AddColumn("dbo.xt_web", "last_modified_time", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.xt_web", "xt_web_group_id", c => c.String(maxLength: 40, storeType: "nvarchar"));
            CreateIndex("dbo.xt_web", "xt_web_group_id");
            AddForeignKey("dbo.xt_web", "xt_web_group_id", "dbo.xt_web_group", "id");
            DropColumn("dbo.xt_web", "username");
            DropColumn("dbo.xt_web", "password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.xt_web", "password", c => c.String(nullable: false, maxLength: 255, storeType: "nvarchar"));
            AddColumn("dbo.xt_web", "username", c => c.String(nullable: false, maxLength: 255, storeType: "nvarchar"));
            DropForeignKey("dbo.xt_web", "xt_web_group_id", "dbo.xt_web_group");
            DropForeignKey("dbo.xt_web_account", "xt_web_id", "dbo.xt_web");
            DropIndex("dbo.xt_web_account", new[] { "xt_web_id" });
            DropIndex("dbo.xt_web", new[] { "xt_web_group_id" });
            DropColumn("dbo.xt_web", "xt_web_group_id");
            DropColumn("dbo.xt_web", "last_modified_time");
            DropColumn("dbo.xt_web", "last_modified_by_id");
            DropColumn("dbo.xt_user", "last_modified_time");
            DropColumn("dbo.xt_user", "last_modified_by_id");
            DropTable("dbo.xt_web_group");
            DropTable("dbo.xt_web_account");
        }
    }
}
