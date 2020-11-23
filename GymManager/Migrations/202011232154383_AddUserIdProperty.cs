namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdProperty : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.EquipmentOrders", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.EquipmentOrders", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.EquipmentOrders", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.EquipmentOrders", name: "UserId", newName: "User_Id");
        }
    }
}
