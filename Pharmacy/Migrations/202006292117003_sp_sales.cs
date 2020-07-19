namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sp_sales : DbMigration
    {
        public override void Up()
        {
            Sql(ResourceSQL.CreateBill);
            Sql(ResourceSQL.CreateSaleDetails);
        }
        
        public override void Down()
        {
        }
    }
}
