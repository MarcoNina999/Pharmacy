namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class spImage : DbMigration
    {
        public override void Up()
        {
            Sql(ResourceSQL.RandomImage);
        }
        
        public override void Down()
        {
        }
    }
}
