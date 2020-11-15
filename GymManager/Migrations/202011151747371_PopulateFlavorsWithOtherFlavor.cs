namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateFlavorsWithOtherFlavor : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Flavors (Name) VALUES ('Other')");
        }
        
        public override void Down()
        {
        }
    }
}
