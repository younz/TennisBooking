using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using TennisBooking.Interface;
using TennisBooking.Models;

namespace TennisBooking.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private IMembers _members;
        private IBooking _bookings;
        public List<Booking> currentbookings { get; set; }
        public IndexModel(IBooking booking, IMembers members)
        {
            _members=members;
            _bookings = booking;
        }
        public async Task OnGet()
        {
            currentbookings = await _bookings.GetAllBookings();
        }
    }
}
