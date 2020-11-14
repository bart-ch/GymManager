namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateSupplementFlavors : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Flavors (Id,Name) VALUES (1,'Vanilla')");
            Sql("INSERT INTO Flavors (Id,Name) VALUES (2,'Chocolate')");
            Sql("INSERT INTO Flavors (Id,Name) VALUES (3,'Banana')");
            Sql("INSERT INTO Flavors (Id,Name) VALUES (4,'Strawberry')");
            Sql("INSERT INTO Flavors (Id,Name) VALUES (5,'Raspberry')");
            Sql("INSERT INTO Flavors (Id,Name) VALUES (6,'Cookie')");
            Sql("INSERT INTO Flavors (Id,Name) VALUES (7,'Salted Caramel')");
        }
        
        public override void Down()
        {
        }
    }
}
