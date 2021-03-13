using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCars.BusinessCore.models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Personnummer { get; set; }

        public DateTime Birthdate { get; set; }

        public Customer()
        {

        }
    }
}
