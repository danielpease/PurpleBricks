namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMaxLengthOnPropertyModel : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE dbo.Properties DROP CONSTRAINT DF__Propertie__Prope__1FCDBCEB");
            Sql("ALTER TABLE dbo.Properties DROP CONSTRAINT DF__Propertie__Stree__20C1E124");
            Sql("ALTER TABLE dbo.Properties DROP CONSTRAINT DF__Propertie__Descr__21B6055D");
            AlterColumn("dbo.Properties", "PropertyType", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Properties", "StreetName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Properties", "Description", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Properties", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Properties", "StreetName", c => c.String(nullable: false));
            AlterColumn("dbo.Properties", "PropertyType", c => c.String(nullable: false));
        }
    }
}
