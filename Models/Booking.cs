using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisBooking.Models
{
    public class Booking
    {
        public Booking(int firstMemberId, int partnerId, int bookingId, int laneId, DateTime date)
        {
            FirstMemberId = firstMemberId;
            PartnerId = partnerId;
            BookingId = bookingId;
            LaneId = laneId;
            BookingTime = date;
        }
        public Booking(){}

        public DateTime BookingTime { get; set; } = DateTime.Today;
        public int FirstMemberId { get; set; }
        public int PartnerId { get; set; }
        public int BookingId { get; set; }
        public int LaneId { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj is Booking)
            {
                var overbooking = (Booking)obj;
                if (overbooking.BookingId == this.BookingId)
                {
                    return true;
                }

            }

            return false;
        }

    }
}
