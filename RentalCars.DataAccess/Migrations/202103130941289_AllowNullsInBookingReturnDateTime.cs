namespace RentalCars.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowNullsInBookingReturnDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "ReturnDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "ReturnDateTime", c => c.DateTime(nullable: false));
        }
    }
}
