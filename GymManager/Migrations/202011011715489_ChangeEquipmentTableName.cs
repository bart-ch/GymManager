namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEquipmentTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Equipment", newName: "Equipment");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Equipment", newName: "Equipment");
        }
    }
}
