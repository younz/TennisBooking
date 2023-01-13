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
    public class IndexModel : PageModel
    {
        private ILane _reps;
        public List<Lane> Lanes { get; private set; }

        public IndexModel(ILane repo)
        {
            _reps = repo;
        }
        public async Task OnGet()
        {
            Lanes = await _reps.GetallLanes();
        }
    }
}
