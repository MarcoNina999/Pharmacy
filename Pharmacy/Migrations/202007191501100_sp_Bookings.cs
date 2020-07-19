namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sp_Bookings : DbMigration
    {
        public override void Up()
        {
            Sql(ResourceSQL.CreateBookings);
            Sql(ResourceSQL.CreateBookingDetails);
        }
        
        public override void Down()
        {
        }
    }
}
