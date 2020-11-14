namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateSuplementTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO SupplementTypes (Id,Name) VALUES (1,'Whey protein')");
            Sql("INSERT INTO SupplementTypes (Id,Name) VALUES (2,'Isolate protein')");
            Sql("INSERT INTO SupplementTypes (Id,Name) VALUES (3,'Mass gainer')");
            Sql("INSERT INTO SupplementTypes (Id,Name) VALUES (4,'Creatine monohydrate')");
            Sql("INSERT INTO SupplementTypes (Id,Name) VALUES (5,'BCAA')");
        }
        
        public override void Down()
        {
        }
    }
}
