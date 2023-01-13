using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisBooking.Interface;
using TennisBooking.Models;

namespace TennisBooking.Pages.members
{
    public class IndexModel : PageModel
    {
        private IMembers _repo;
        public List<Member> Members { get; private set; }

        public IndexModel(IMembers repos)
        {
            _repo = repos;
        }
        public async Task OnGet()
        {
            Members = await _repo.GetAllMembers();
        }
    }

    }

