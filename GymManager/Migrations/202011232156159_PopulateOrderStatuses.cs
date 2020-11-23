namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateOrderStatuses : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO OrderStatuses (Id,Name) VALUES (1,'In progress')");
            Sql("INSERT INTO OrderStatuses (Id,Name) VALUES (2,'Shipped')");
            Sql("INSERT INTO OrderStatuses (Id,Name) VALUES (3,'Completed')");
        }
        
        public override void Down()
        {
        }
    }
}
