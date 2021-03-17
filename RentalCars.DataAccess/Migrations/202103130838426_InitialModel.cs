namespace RentalCars.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                {
                    BookingNr = c.Int(nullable: false, identity: true),
                    RentalDateTime = c.DateTime(nullable: false),
                    ReturnDateTime = c.DateTime(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                    CustomerBirthdate = c.DateTime(nullable: false),
                    RentedCar_RegNr = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.BookingNr)
                .ForeignKey("dbo.Cars", t => t.RentedCar_RegNr)
                .Index(t => t.RentedCar_RegNr);

            CreateTable(
                "dbo.Cars",
                c => new
                {
                    RegNr = c.String(nullable: false, maxLength: 128),
                    Milage = c.Int(nullable: false),
                    IsRented = c.Boolean(nullable: false),
                    Category_Id = c.Int(),
                })
                .PrimaryKey(t => t.RegNr)
                .ForeignKey("dbo.CarCategories", t => t.Category_Id)
                .Index(t => t.Category_Id);

            CreateTable(
                "dbo.CarCategories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Category = c.String(),
                    BaseRentalMultiplier = c.Double(nullable: false),
                    KilometerPriceMultiplier = c.Double(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Customers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Personnummer = c.Int(nullable: false),
                    Birthdate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "RentedCar_RegNr", "dbo.Cars");
            DropForeignKey("dbo.Cars", "Category_Id", "dbo.CarCategories");
            DropIndex("dbo.Cars", new[] { "Category_Id" });
            DropIndex("dbo.Bookings", new[] { "RentedCar_RegNr" });
            DropTable("dbo.Customers");
            DropTable("dbo.CarCategories");
            DropTable("dbo.Cars");
            DropTable("dbo.Bookings");
        }
    }
}
