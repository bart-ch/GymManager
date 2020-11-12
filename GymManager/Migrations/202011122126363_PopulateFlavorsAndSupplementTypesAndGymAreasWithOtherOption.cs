namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateFlavorsAndSupplementTypesAndGymAreasWithOtherOption : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Flavors (Id, Name) VALUES (8,'Other')");
            Sql("INSERT INTO SupplementTypes (Id, Name) VALUES (6,'Other')");
            Sql("INSERT INTO Areas (Id, Name) VALUES (9,'Other')");
        }
        
        public override void Down()
        {
        }
    }
}
