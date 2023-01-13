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
    public class EditMemberModel : PageModel
    {
        [BindProperty]public Member EditMember { get; set; }
        private IMembers members;
        public EditMemberModel (IMembers repo)
        {
            members= repo;
        }
        public async Task OnGet(int id )
        {
            EditMember = await members.GetMember(id);
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await members.EditMember(EditMember);
            return RedirectToPage("Index");
        }
    }
}
