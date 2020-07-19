namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sp_sale : DbMigration
    {
        public override void Up()
        {
            Sql(ResourceSQL.CreateSale);
        }
        
        public override void Down()
        {
        }
    }
}
