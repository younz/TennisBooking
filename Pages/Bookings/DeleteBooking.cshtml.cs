using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisBooking.Interface;
using TennisBooking.Models;

namespace TennisBooking.Pages.Bookings
{
    public class DeleteBookingModel : PageModel
    {
        private IBooking repo;
        [BindProperty] public Booking currentBooking { get; set; }
        public DeleteBookingModel(IBooking _bookings)
        {
            repo = _bookings;
        }
        public async Task OnGet(int id)
        {
            currentBooking = await repo.GetBooking(id);
        }
       public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            repo.RemoveBooking(currentBooking);
            return RedirectToPage("Index");
        }

    }
}
