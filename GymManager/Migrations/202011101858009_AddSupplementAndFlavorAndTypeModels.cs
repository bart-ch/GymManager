namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSupplementAndFlavorAndTypeModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flavors",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Supplements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InitialAmount = c.Int(nullable: false),
                        ConsumedAmount = c.Int(nullable: false),
                        FlavorId = c.Byte(nullable: false),
                        SupplementTypeId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Flavors", t => t.FlavorId, cascadeDelete: true)
                .ForeignKey("dbo.SupplementTypes", t => t.SupplementTypeId, cascadeDelete: true)
                .Index(t => t.FlavorId)
                .Index(t => t.SupplementTypeId);
            
            CreateTable(
                "dbo.SupplementTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supplements", "SupplementTypeId", "dbo.SupplementTypes");
            DropForeignKey("dbo.Supplements", "FlavorId", "dbo.Flavors");
            DropIndex("dbo.Supplements", new[] { "SupplementTypeId" });
            DropIndex("dbo.Supplements", new[] { "FlavorId" });
            DropTable("dbo.SupplementTypes");
            DropTable("dbo.Supplements");
            DropTable("dbo.Flavors");
        }
    }
}
