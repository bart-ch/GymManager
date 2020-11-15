namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSupplementsAndFlavorTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flavors",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Supplements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false, maxLength: 255),
                        InitialAmount = c.Int(nullable: false),
                        ConsumedAmount = c.Int(),
                        DeliveryDate = c.DateTime(nullable: false),
                        FlavorId = c.Byte(nullable: false),
                        SupplementTypeId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Flavors", t => t.FlavorId, cascadeDelete: true)
                .ForeignKey("dbo.SupplementTypes", t => t.SupplementTypeId, cascadeDelete: true)
                .Index(t => t.FlavorId)
                .Index(t => t.SupplementTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supplements", "SupplementTypeId", "dbo.SupplementTypes");
            DropForeignKey("dbo.Supplements", "FlavorId", "dbo.Flavors");
            DropIndex("dbo.Supplements", new[] { "SupplementTypeId" });
            DropIndex("dbo.Supplements", new[] { "FlavorId" });
            DropTable("dbo.Supplements");
            DropTable("dbo.Flavors");
        }
    }
}
