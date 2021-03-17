namespace RentalCars.BusinessCore.models
{
    public class CarCategory
    {
        public int Id { get; set; }

        public string Category { get; set; }

        public double BaseRentalMultiplier { get; set; }

        public double KilometerPriceMultiplier { get; set; }


        private CarCategory()
        {

        }
        public CarCategory(string categoryName, double baseRentalMultiplier, double kilometerPriceMultiplier)
        {
            Category = categoryName;
            BaseRentalMultiplier = baseRentalMultiplier;
            KilometerPriceMultiplier = kilometerPriceMultiplier;
        }
    }
}
