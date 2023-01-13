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
    public class DeleteMemberModel : PageModel
    {

        private IMembers repo;
        [BindProperty] public Member RemoveMember { get; set; }

        public DeleteMemberModel(IMembers tempMemberRepo)
        {
            repo = tempMemberRepo;
        }

        public async Task OnGet(int id)
        {
            RemoveMember = await repo.GetMember(id);
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            repo.RemoveMember(RemoveMember);
            return RedirectToPage("Index");
        }



    }
}
