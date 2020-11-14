namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBrandToSupplement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supplements", "Brand", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Supplements", "Brand");
        }
    }
}
