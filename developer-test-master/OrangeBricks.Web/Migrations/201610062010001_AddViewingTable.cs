namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewingTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Viewings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PropertyId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        ViewDate = c.DateTime(nullable: false),
                        BuyerUserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Properties", t => t.PropertyId, cascadeDelete: true)
                .Index(t => t.PropertyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Viewings", "PropertyId", "dbo.Properties");
            DropIndex("dbo.Viewings", new[] { "PropertyId" });
            DropTable("dbo.Viewings");
        }
    }
}
