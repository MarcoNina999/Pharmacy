namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sp_Creates : DbMigration
    {
        public override void Up()
        {
            Sql(ResourceSQL.CreateSaleDetails);
        }
        
        public override void Down()
        {
        }
    }
}
