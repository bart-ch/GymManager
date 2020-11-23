namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAmountToQuantityInEquipmentOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EquipmentOrders", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.EquipmentOrders", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EquipmentOrders", "Amount", c => c.Int(nullable: false));
            DropColumn("dbo.EquipmentOrders", "Quantity");
        }
    }
}
