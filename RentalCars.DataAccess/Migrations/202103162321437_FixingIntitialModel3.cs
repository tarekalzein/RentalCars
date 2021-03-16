namespace RentalCars.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingIntitialModel3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Bookings", name: "RentedCarRegNr", newName: "RentedCar_RegNr");
            RenameIndex(table: "dbo.Bookings", name: "IX_RentedCarRegNr", newName: "IX_RentedCar_RegNr");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Bookings", name: "IX_RentedCar_RegNr", newName: "IX_RentedCarRegNr");
            RenameColumn(table: "dbo.Bookings", name: "RentedCar_RegNr", newName: "RentedCarRegNr");
        }
    }
}
