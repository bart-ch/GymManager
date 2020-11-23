namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEquipmentOrderAndOrderStatusEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquipmentOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false, maxLength: 255),
                        Model = c.String(nullable: false, maxLength: 255),
                        DeadlineDate = c.DateTime(nullable: false),
                        TypeId = c.Byte(nullable: false),
                        Amount = c.Int(nullable: false),
                        OrderStatusId = c.Byte(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderStatuses", t => t.OrderStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Types", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.TypeId)
                .Index(t => t.OrderStatusId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.OrderStatuses",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipmentOrders", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.EquipmentOrders", "TypeId", "dbo.Types");
            DropForeignKey("dbo.EquipmentOrders", "OrderStatusId", "dbo.OrderStatuses");
            DropIndex("dbo.EquipmentOrders", new[] { "User_Id" });
            DropIndex("dbo.EquipmentOrders", new[] { "OrderStatusId" });
            DropIndex("dbo.EquipmentOrders", new[] { "TypeId" });
            DropTable("dbo.OrderStatuses");
            DropTable("dbo.EquipmentOrders");
        }
    }
}
