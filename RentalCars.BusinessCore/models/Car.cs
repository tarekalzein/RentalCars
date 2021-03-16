using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCars.BusinessCore.models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RegNr { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        public virtual CarCategory Category { get; set; }

        public int Milage { get; set; }

        public bool IsRented { get; set; }

        private Car()
        {

        }
        public Car(string regNr, CarCategory category, int milage)
        {
            RegNr = regNr.ToUpper();
            Category = category;
            Milage = milage;
            IsRented = false;
        }
    }
}
