namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAreaModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Equipment", "Area_Id", c => c.Byte());
            CreateIndex("dbo.Equipment", "Area_Id");
            AddForeignKey("dbo.Equipment", "Area_Id", "dbo.Areas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipment", "Area_Id", "dbo.Areas");
            DropIndex("dbo.Equipment", new[] { "Area_Id" });
            DropColumn("dbo.Equipment", "Area_Id");
            DropTable("dbo.Areas");
        }
    }
}
