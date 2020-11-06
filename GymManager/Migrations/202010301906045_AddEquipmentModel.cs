namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEquipmentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfEquipment = c.String(nullable: false, maxLength: 255),
                        Brand = c.String(nullable: false, maxLength: 255),
                        Model = c.String(nullable: false, maxLength: 255),
                        PurchaseDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Equipment");
        }
    }
}
