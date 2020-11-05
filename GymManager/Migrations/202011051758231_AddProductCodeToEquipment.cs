namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductCodeToEquipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipment", "ProductCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipment", "ProductCode");
        }
    }
}
