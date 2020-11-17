namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMalfunctionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Malfunctions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false, maxLength: 255),
                        MalfunctionDate = c.DateTime(nullable: false),
                        IsRepaired = c.Boolean(nullable: false),
                        EquipmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipment", t => t.EquipmentId, cascadeDelete: true)
                .Index(t => t.EquipmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Malfunctions", "EquipmentId", "dbo.Equipment");
            DropIndex("dbo.Malfunctions", new[] { "EquipmentId" });
            DropTable("dbo.Malfunctions");
        }
    }
}
