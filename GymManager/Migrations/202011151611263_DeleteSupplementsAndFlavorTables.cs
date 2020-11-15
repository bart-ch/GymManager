namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteSupplementsAndFlavorTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Supplements", "FlavorId", "dbo.Flavors");
            DropForeignKey("dbo.Supplements", "SupplementTypeId", "dbo.SupplementTypes");
            DropIndex("dbo.Supplements", new[] { "FlavorId" });
            DropIndex("dbo.Supplements", new[] { "SupplementTypeId" });
            DropTable("dbo.Flavors");
            DropTable("dbo.Supplements");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Flavors",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Supplements", "SupplementTypeId");
            CreateIndex("dbo.Supplements", "FlavorId");
            AddForeignKey("dbo.Supplements", "SupplementTypeId", "dbo.SupplementTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Supplements", "FlavorId", "dbo.Flavors", "Id", cascadeDelete: true);
        }
    }
}
