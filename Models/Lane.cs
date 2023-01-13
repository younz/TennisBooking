using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisBooking.Models
{
    public class Lane
    {
        public Lane()
        {
        }

        public Lane(int id, string type)
        {
            BaneNummer = id;
            Type = type;
        }
        public string Type { get; set; }
        public int BaneNummer { get; set; }

    }
}