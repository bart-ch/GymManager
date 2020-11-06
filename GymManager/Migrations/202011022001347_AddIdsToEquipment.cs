namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdsToEquipment : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Equipment", name: "Area_Id", newName: "AreaId");
            RenameColumn(table: "dbo.Equipment", name: "Type_Id", newName: "TypeId");
            RenameIndex(table: "dbo.Equipment", name: "IX_Area_Id", newName: "IX_AreaId");
            RenameIndex(table: "dbo.Equipment", name: "IX_Type_Id", newName: "IX_TypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Equipment", name: "IX_TypeId", newName: "IX_Type_Id");
            RenameIndex(table: "dbo.Equipment", name: "IX_AreaId", newName: "IX_Area_Id");
            RenameColumn(table: "dbo.Equipment", name: "TypeId", newName: "Type_Id");
            RenameColumn(table: "dbo.Equipment", name: "AreaId", newName: "Area_Id");
        }
    }
}
