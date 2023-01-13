using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisBooking.Models;

namespace TennisBooking.Interface
{
    public interface IBooking
    {
        Task<List<Booking>> GetAllBookings();
        Task<bool> AddBooking(Booking booking);
        Task<Booking> GetBooking(int id);
        Task<Booking> RemoveBooking(Booking booking);
        Task<bool> EditBooking(Booking booking);
    }
}
