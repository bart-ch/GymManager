namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetIdAsIdentityInFlavor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Supplements", "FlavorId", "dbo.Flavors");
            DropPrimaryKey("dbo.Flavors");
            AlterColumn("dbo.Flavors", "Id", c => c.Byte(nullable: false, identity: true));
            AddPrimaryKey("dbo.Flavors", "Id");
            AddForeignKey("dbo.Supplements", "FlavorId", "dbo.Flavors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supplements", "FlavorId", "dbo.Flavors");
            DropPrimaryKey("dbo.Flavors");
            AlterColumn("dbo.Flavors", "Id", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.Flavors", "Id");
            AddForeignKey("dbo.Supplements", "FlavorId", "dbo.Flavors", "Id", cascadeDelete: true);
        }
    }
}
