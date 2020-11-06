namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Types (Id,Name) VALUES (1,'Free weight')");
            Sql("INSERT INTO Types (Id,Name) VALUES (2,'Strength machine')");
            Sql("INSERT INTO Types (Id,Name) VALUES (3,'Treadmill')");
            Sql("INSERT INTO Types (Id,Name) VALUES (4,'Elliptical trainer')");
            Sql("INSERT INTO Types (Id,Name) VALUES (5,'Rowing Machine')");
            Sql("INSERT INTO Types (Id,Name) VALUES (6,'Stepper')");
            Sql("INSERT INTO Types (Id,Name) VALUES (7,'Stationary bicycle')");
            Sql("INSERT INTO Types (Id,Name) VALUES (8,'Resistance band and tubing')");
            Sql("INSERT INTO Types (Id,Name) VALUES (9,'Handle')");
            Sql("INSERT INTO Types (Id,Name) VALUES (10,'Other')");
        }
        
        public override void Down()
        {
        }
    }
}
