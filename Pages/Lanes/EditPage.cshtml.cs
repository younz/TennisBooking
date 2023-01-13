using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisBooking.Interface;
using TennisBooking.Models;

namespace TennisBooking.Pages.Lanes
{
    public class EditPageModel : PageModel
    {
        [BindProperty] public Lane Lanes { get; set; }
        private ILane _repos;

        public EditPageModel(ILane lane)
        {
            _repos = lane;
        }
        public async Task OnGet(int id)
        {
            Lanes = await _repos.GetLane(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repos.EditLane(Lanes);
            return RedirectToPage("Index");
        }
    }
}
