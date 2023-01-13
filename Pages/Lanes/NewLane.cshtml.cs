using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TennisBooking.Interface;
using TennisBooking.Models;

namespace TennisBooking.Pages.Lanes
{
    public class NewLaneModel : PageModel
    {
       // public List<SelectListItem> LaneType { get; set; }

        private ILane _repo;
        [BindProperty] public Lane Lanes { get; set; }

        public NewLaneModel(ILane lane)
        {
            _repo = lane;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repo.AddLane(Lanes);
            return RedirectToPage("Index");
        }
    }
}
