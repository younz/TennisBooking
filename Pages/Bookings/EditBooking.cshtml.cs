using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TennisBooking.Interface;
using TennisBooking.Models;

namespace TennisBooking.Pages.Bookings
{
    public class EditBookingModel : PageModel
    {
        [BindProperty]public Booking Booking { get; set; }
        public List<SelectListItem> LaneItems { get; set; }
        public List<SelectListItem> MemberItems { get; set; }
        private IBooking bookingrepo;
        private IMembers _members;
        private ILane _lane;
        public EditBookingModel(IBooking bookings, IMembers member, ILane bane)
        {
            bookingrepo = bookings;
            _lane = bane;
            _members = member;
        }

        public async Task<IActionResult> OnGet(int id)
        {
             List<Member> member = await _members.GetAllMembers();
            List<Lane> lanes = await _lane.GetallLanes();
            LaneItems = new List<SelectListItem>();
            MemberItems = new List<SelectListItem>();
            foreach (var b in lanes)
            {
                SelectListItem s = new SelectListItem(b.BaneNummer.ToString(), b.BaneNummer.ToString());
                LaneItems.Add(s);
            }

            foreach (var m in member)
            {
                SelectListItem t = new SelectListItem(m.FirstName, m.MemberId.ToString());
                MemberItems.Add(t);
            }
            Booking = bookingrepo.GetBooking(id).Result;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            bookingrepo.EditBooking(Booking);
            return RedirectToPage("Index");
        }

    }
}
