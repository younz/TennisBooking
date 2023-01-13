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
    public class DeleteLaneModel : PageModel
    {
        private ILane repo;
        [BindProperty] public Lane lane { get; set; }
        public DeleteLaneModel(ILane irepo)
        {
            repo = irepo;
        }
        public async Task OnGet(int id)
        {
            lane= await repo.GetLane(id);
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            repo.RemoveLane(lane);
            return RedirectToPage("Index");
        }
    }
}
