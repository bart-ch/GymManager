namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixFlavorsTable : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Flavors SET Name = 'Salted caramel' WHERE Id = 7");
        }
        
        public override void Down()
        {
        }
    }
}
