namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateAreaTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Areas (Id,Name) VALUES (1,'Free weights')");
            Sql("INSERT INTO Areas (Id,Name) VALUES (2,'Cardiovascular')");
            Sql("INSERT INTO Areas (Id,Name) VALUES (3,'Selectorized Strength Equipment')");
            Sql("INSERT INTO Areas (Id,Name) VALUES (4,'Functional')");
            Sql("INSERT INTO Areas (Id,Name) VALUES (5,'Stretching and mobility')");
            Sql("INSERT INTO Areas (Id,Name) VALUES (6,'Indoor cycling')");
            Sql("INSERT INTO Areas (Id,Name) VALUES (7,'Crossfit')");
            Sql("INSERT INTO Areas (Id,Name) VALUES (8,'Group activities studio')");
        }
        
        public override void Down()
        {
        }
    }
}
