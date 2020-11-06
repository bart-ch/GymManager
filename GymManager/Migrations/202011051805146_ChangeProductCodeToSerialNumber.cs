namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeProductCodeToSerialNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipment", "SerialNumber", c => c.String());
            DropColumn("dbo.Equipment", "ProductCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipment", "ProductCode", c => c.String());
            DropColumn("dbo.Equipment", "SerialNumber");
        }
    }
}
