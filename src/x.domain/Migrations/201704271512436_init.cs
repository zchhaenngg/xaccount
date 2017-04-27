namespace x.domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.hy_data_history",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 40, storeType: "nvarchar"),
                        entity_name = c.String(maxLength: 50, storeType: "nvarchar"),
                        unique_key = c.String(nullable: false, maxLength: 40, storeType: "nvarchar"),
                        operation = c.String(maxLength: 50, storeType: "nvarchar"),
                        description = c.String(nullable: false, unicode: false),
                        context_id = c.String(nullable: false, maxLength: 40, storeType: "nvarchar"),
                        created_by_id = c.String(maxLength: 40, storeType: "nvarchar"),
                        created_time = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.xt_user",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 40, storeType: "nvarchar"),
                        username = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        password = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        created_by_id = c.String(maxLength: 40, storeType: "nvarchar"),
                        created_time = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.xt_web",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 40, storeType: "nvarchar"),
                        site = c.String(maxLength: 255, storeType: "nvarchar"),
                        username = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        password = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        web_name = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        created_by_id = c.String(maxLength: 40, storeType: "nvarchar"),
                        created_time = c.DateTime(nullable: false, precision: 0),
                        xt_user_id = c.String(maxLength: 40, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.xt_user", t => t.xt_user_id)
                .Index(t => t.xt_user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.xt_web", "xt_user_id", "dbo.xt_user");
            DropIndex("dbo.xt_web", new[] { "xt_user_id" });
            DropTable("dbo.xt_web");
            DropTable("dbo.xt_user");
            DropTable("dbo.hy_data_history");
        }
    }
}
