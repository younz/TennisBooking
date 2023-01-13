using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TennisBooking.Models;
using TennisBooking.Interface;

namespace TennisBooking.Pages.Bookings
{
    public class NewBookingModel : PageModel
    {
        private IBooking _booking;
        private IMembers _members;
        private ILane _lane;
        [BindProperty] public Booking NewBooking { get; set; }
        public List<SelectListItem> LaneItems { get; set; }
        public List<SelectListItem> MemberItems { get; set; }

        public NewBookingModel(IBooking bookings, IMembers member, ILane bane)
        {
            _booking = bookings;
            _lane = bane;
            _members = member;
            NewBooking = new Booking();
        }

        public async Task<IActionResult> OnGet()
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

            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Bookings.confirmation = true;
            await _booking.AddBooking(NewBooking);
            return RedirectToPage("Index");
        }
    }
}
