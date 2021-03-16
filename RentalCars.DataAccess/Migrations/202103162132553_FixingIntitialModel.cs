namespace RentalCars.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingIntitialModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "Category_Id", "dbo.CarCategories");
            DropIndex("dbo.Cars", new[] { "Category_Id" });
            RenameColumn(table: "dbo.Cars", name: "Category_Id", newName: "CategoryID");
            AlterColumn("dbo.Cars", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "CategoryID");
            AddForeignKey("dbo.Cars", "CategoryID", "dbo.CarCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "CategoryID", "dbo.CarCategories");
            DropIndex("dbo.Cars", new[] { "CategoryID" });
            AlterColumn("dbo.Cars", "CategoryID", c => c.Int());
            RenameColumn(table: "dbo.Cars", name: "CategoryID", newName: "Category_Id");
            CreateIndex("dbo.Cars", "Category_Id");
            AddForeignKey("dbo.Cars", "Category_Id", "dbo.CarCategories", "Id");
        }
    }
}
