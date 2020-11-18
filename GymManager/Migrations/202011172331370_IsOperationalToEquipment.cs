namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsOperationalToEquipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipment", "IsOperational", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipment", "IsOperational");
        }
    }
}
