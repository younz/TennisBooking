using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisBooking.Models;

namespace TennisBooking.Interface
{
    public interface IMembers
    {
        //Gadevang TenneisKlub
        Task<List<Member>> GetAllMembers();
        Task<bool> AddMember(Member member);
        Task<Member> GetMember(int id);
        Task<Member> RemoveMember(Member member);
        Task<bool> EditMember(Member member);
    }
}
