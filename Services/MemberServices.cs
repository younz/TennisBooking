using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TennisBooking.Helpers;
using TennisBooking.Interface;
using TennisBooking.Models;

namespace TennisBooking.Services
{
    public class MemberServices : Connection, IMembers
    {
        private string _getAllMembers = "select * from Member";
        private string _getById = "select * from Member where MemberNumber = @ID";
        private string _addMember = "insert into Member Values (@ID, @FirstName, @LastName, @Address, @Email, @Number, @Password)";
        private string _removeMember = "delete from Member where MemberNumber = @ID";
        private string _editMember = "Update Member " +
                                     "set MemberNumber = @ID, FirstName= @FirstName, LastName = @LastName, Address = @Address," +
                                     " Email = @Mail, PhoneNumber = @Number, USERPassword = @Password" +
                                     " where MemberNumber = @ID";

        public async Task<List<Member>> GetAllMembers()
        {


            List < Member > members = new List<Member>();
            await using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                await using (SqlCommand command = new SqlCommand(_getAllMembers, sqlConnection))
                {
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Member member = new Member();
                        member.MemberId = reader.GetInt32(0);
                        member.FirstName = reader.GetString(1);
                        member.LastName = reader.GetString(2);
                        member.Address = reader.GetString(3);
                        member.Mail = reader.GetString(4);
                        member.PhoneNumber = reader.GetString(5);
                        member.Password = reader.GetString(6);
                        members.Add(member);
                    }
                }
                             
                
                
            }

            return members.ToList();
        }

        public async Task<bool> AddMember(Member member)
        {
            
            List<Member> currenMembers = await GetAllMembers();
            List<int> memberInts = new List<int>();
            foreach (var variable in currenMembers)
            {
                memberInts.Add(variable.MemberId);
            }

            if (memberInts.Count != 0)
            {
                int start = memberInts.Max();
                member.MemberId = start + 1;
            }
            else
            {
                member.MemberId = 1;
            }

            await using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await using (SqlCommand command = new SqlCommand(_addMember, connection))
                {
                    command.Parameters.AddWithValue("@ID", member.MemberId);
                    command.Parameters.AddWithValue("@FirstName", member.FirstName);
                    command.Parameters.AddWithValue("@LastName", member.LastName);
                    command.Parameters.AddWithValue("@Address", member.Address);
                    command.Parameters.AddWithValue("@Email", member.Mail);
                    command.Parameters.AddWithValue("@Number", member.PhoneNumber);
                    command.Parameters.AddWithValue("@Password", member.Password);
                    await command.Connection.OpenAsync();
                    var noOfRow = command.ExecuteNonQuery();
                    if (noOfRow == 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<Member> GetMember(int id)
        {
            
            await using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await using (SqlCommand command = new SqlCommand(_getById, connection))
                {
                    command.Parameters.AddWithValue("@ID",id);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        int memberId = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string lastName = reader.GetString(2);
                        string email = reader.GetString(4);
                        string adress = reader.GetString(3);
                        string phone = reader.GetString(5);
                        string pass = reader.GetString(6);
                        Member member = new Member(memberId, name, lastName, email, adress, phone,pass);
                        return member;
                    }
                }
            }
            return null;

        }

        public async Task<Member> RemoveMember(Member member)
        {
            if (member != null)
            {
                await using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await using (SqlCommand command = new SqlCommand(_removeMember, connection))
                    {
                        command.Parameters.AddWithValue("@ID",member.MemberId);
                        await command.Connection.OpenAsync();
                        var noOfRows = command.ExecuteNonQuery();
                        if (noOfRows==1)
                        {
                            return member;
                        }

                        return null;
                    }
                }
            }

            return null;
        }

        public async Task<bool> EditMember(Member member)
        {

            await using(SqlConnection connection = new SqlConnection(ConnectionString))
            await using (SqlCommand command = new SqlCommand(_editMember,connection))
            {
                command.Parameters.AddWithValue("@ID", member.MemberId);
                command.Parameters.AddWithValue("@FirstName", member.FirstName);
                command.Parameters.AddWithValue("@LastName", member.LastName);
                command.Parameters.AddWithValue("@Address", member.Address);
                command.Parameters.AddWithValue("@Mail", member.Mail);
                command.Parameters.AddWithValue("@Number", member.PhoneNumber);
                command.Parameters.AddWithValue("@Password", member.Password);
                await command.Connection.OpenAsync();
                var noOfRow = command.ExecuteNonQuery();
                if (noOfRow ==1)
                {
                    return true;
                }

                return false;
            }
        }
        

         
        public MemberServices(IConfiguration configuration) : base(configuration)
        {
        }
    
        }

}
