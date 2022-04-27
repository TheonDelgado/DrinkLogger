using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkLogger.Models
{
    public class DrinkLog
    {
        public  int Id { get; set; }
        public  DateTime Date { get; set; }
        public  int Quantity { get; set; }
    }
}