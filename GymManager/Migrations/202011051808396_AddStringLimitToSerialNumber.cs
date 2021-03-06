namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStringLimitToSerialNumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Equipment", "SerialNumber", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Equipment", "SerialNumber", c => c.String());
        }
    }
}
