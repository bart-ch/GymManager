namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeliveryDateToSupplement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supplements", "DeliveryDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Supplements", "DeliveryDate");
        }
    }
}
