namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeCunsumedAndCurrentAmountNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Supplements", "ConsumedAmount", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Supplements", "ConsumedAmount", c => c.Int(nullable: false));
        }
    }
}
