namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTypeDomainModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipment", "Area_Id", "dbo.Areas");
            DropIndex("dbo.Equipment", new[] { "Area_Id" });
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Equipment", "Type_Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.Equipment", "Area_Id", c => c.Byte(nullable: false));
            CreateIndex("dbo.Equipment", "Area_Id");
            CreateIndex("dbo.Equipment", "Type_Id");
            AddForeignKey("dbo.Equipment", "Type_Id", "dbo.Types", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Equipment", "Area_Id", "dbo.Areas", "Id", cascadeDelete: true);
            DropColumn("dbo.Equipment", "TypeOfEquipment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipments", "TypeOfEquipment", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.Equipment", "Area_Id", "dbo.Areas");
            DropForeignKey("dbo.Equipment", "Type_Id", "dbo.Types");
            DropIndex("dbo.Equipment", new[] { "Type_Id" });
            DropIndex("dbo.Equipment", new[] { "Area_Id" });
            AlterColumn("dbo.Equipment", "Area_Id", c => c.Byte());
            DropColumn("dbo.Equipment", "Type_Id");
            DropTable("dbo.Types");
            CreateIndex("dbo.Equipment", "Area_Id");
            AddForeignKey("dbo.Equipment", "Area_Id", "dbo.Areas", "Id");
        }
    }
}
