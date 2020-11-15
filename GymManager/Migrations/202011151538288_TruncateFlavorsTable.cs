namespace GymManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TruncateFlavorsTable : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM flavors WHERE id = 1");
            Sql("DELETE FROM flavors WHERE id = 2");
            Sql("DELETE FROM flavors WHERE id = 3");
            Sql("DELETE FROM flavors WHERE id = 4");
            Sql("DELETE FROM flavors WHERE id = 5");
            Sql("DELETE FROM flavors WHERE id = 6");
            Sql("DELETE FROM flavors WHERE id = 7");
            Sql("DELETE FROM flavors WHERE id = 8");
        }
        
        public override void Down()
        {
        }
    }
}
