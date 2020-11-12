namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePurchaseDateToDeliveryDateInEquipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipment", "DeliveryDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Equipment", "PurchaseDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipment", "PurchaseDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Equipment", "DeliveryDate");
        }
    }
}
