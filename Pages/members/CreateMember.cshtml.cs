using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisBooking.Interface;
using TennisBooking.Models;

namespace TennisBooking.Pages.members
{
    public class CreateMemberModel : PageModel
    {
        [BindProperty]
        public Member Member  { get; set; }
        private IMembers _repo;

        public CreateMemberModel(IMembers memberRepo)
        {
            _repo = memberRepo;
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
            await _repo.AddMember(Member);
            return RedirectToPage("Index");
        }

    }
}
