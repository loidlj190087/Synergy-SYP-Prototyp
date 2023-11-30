using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarDbLib
{
    public class Booking
    {

        public int Id { get; set; }
        public DateTime StartDate{ get; set; }
        public DateTime EndDate { get; set; }
        public string Description{ get; set; }
        public Car Car { get; set; }

        public int CarId { get; set; }
        
    }
}
