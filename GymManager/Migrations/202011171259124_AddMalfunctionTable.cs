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
                        Equipment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipment", t => t.Equipment_Id)
                .Index(t => t.Equipment_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Malfunctions", "Equipment_Id", "dbo.Equipment");
            DropIndex("dbo.Malfunctions", new[] { "Equipment_Id" });
            DropTable("dbo.Malfunctions");
        }
    }
}
