namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDeadlineDateToDesiredDeliveryDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EquipmentOrders", "DesiredDeliveryDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.EquipmentOrders", "DeadlineDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EquipmentOrders", "DeadlineDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.EquipmentOrders", "DesiredDeliveryDate");
        }
    }
}
