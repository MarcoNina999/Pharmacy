namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 50),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Symptoms = c.String(maxLength: 200),
                        Image = c.Binary(),
                        Create_at = c.DateTime(nullable: false),
                        Is_active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
