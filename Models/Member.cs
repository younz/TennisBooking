using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisBooking.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public Member(){}
        public Member(int memberId, string firstName, string lastName, string mail, string address, string phoneNumber, string password)
        {
            MemberId = memberId;
            FirstName = firstName;
            LastName = lastName;
            Mail = mail;
            Address = address;
            PhoneNumber = phoneNumber;
            Password = password;
        }
    }
}
