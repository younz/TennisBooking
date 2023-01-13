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
    public class BookingServices : Connection,IBooking
    {
        private string _getAllBookins = "select * from Booking";
        private string _getBooking = "select * from Booking where BookingId = @ID";
        private string _AddBooking = "insert into Booking values(@ID, @LaneID, @MemberID, @BookingDate, @partnerId)";
        private string _RemoveBooking = "delete from Booking where BookingId = @ID";
        private string _EditBooking = "update Booking set bookingId = @ID, LaneId = @LaneID, MemberId = @MemberID, BookingTime = @BookingDate, PartnerId = @partnerID " +
            "where bookinId = @ID";

        //public LinkedList<string,int> userInfo;
        public BookingServices(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<Booking>> GetAllBookings()
        {
            List<Booking>  currentBookings = new List<Booking>();
            await using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await using(SqlCommand command = new SqlCommand(_getAllBookins,connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            Booking booking = new Booking();
                            booking.BookingId = reader.GetInt32(0);
                            booking.LaneId= reader.GetInt32(1);
                            booking.FirstMemberId= reader.GetInt32(2);
                            booking.BookingTime=reader.GetDateTime(3);
                            booking.PartnerId=reader.GetInt32(4);
                            currentBookings.Add(booking);

                        }
                    }
                    catch (Exception e)
                    {

                        throw new Exception("der sekte en fejl");
                    }
                }
            }
            return currentBookings.ToList();
        }

        public async Task<bool> AddBooking(Booking booking)
        {
            
            List<Booking> currentBookings = await GetAllBookings() as List<Booking>;
            List<int> bookingId = new List<int>();
            foreach (var VARIABLE in currentBookings)
            {
                bookingId.Add(VARIABLE.BookingId);
            }

            if (bookingId.Count != 0)
            {
                int start = bookingId.Max();
                booking.BookingId = start + 1;
            }
            else
            {
                booking.BookingId = 1;
            }
            if (booking.FirstMemberId == booking.PartnerId)
            {
                throw new Exception("Vælg en ny partner");
            }
            await using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await using (SqlCommand command = new SqlCommand(_AddBooking, connection))
                {
                    command.Parameters.AddWithValue("@ID",booking.BookingId);
                    command.Parameters.AddWithValue("@LaneID",booking.LaneId);
                    command.Parameters.AddWithValue("@MemberID", booking.FirstMemberId);
                    command.Parameters.AddWithValue("@BookingDate", booking.BookingTime);
                    command.Parameters.AddWithValue("@partnerId",booking.PartnerId);
                    await command.Connection.OpenAsync();
                    int noOfRows = command.ExecuteNonQuery();
                    if (noOfRows == 1)
                    {
                        return true;
                    }
                }
            }

            return false;
            
            throw new NotImplementedException();
        }

        public async Task<Booking> GetBooking(int id)
        {
            await using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await using(SqlCommand command = new SqlCommand(_getBooking, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        Booking booking = new Booking();
                        booking.BookingId = reader.GetInt32(0);
                        booking.LaneId = reader.GetInt32(1);
                        booking.FirstMemberId = reader.GetInt32(2);
                        booking.BookingTime = reader.GetDateTime(3);
                        booking.PartnerId = reader.GetInt32(4);
                        return booking;
                    }
                }
            }
            return null;
        }

        public async Task<Booking> RemoveBooking(Booking booking)
        {
           
            if (booking != null)
            {
                await using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await using (SqlCommand command = new SqlCommand(_RemoveBooking, connection))
                    {
                        command.Parameters.AddWithValue("@ID", booking.BookingId);
                        await command.Connection.OpenAsync();
                        var NoOfRows = command.ExecuteNonQuery();
                        if (NoOfRows ==1)
                        {
                            return booking;
                        }

                        return null;
                    }
                }
            }

            return null;

        }

        public async Task<bool> EditBooking(Booking booking)
        {
            await using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await using (SqlCommand command = new SqlCommand(_EditBooking, connection))
                {
                    command.Parameters.AddWithValue("@ID", booking.BookingId);
                    command.Parameters.AddWithValue("@MemberID", booking.FirstMemberId);
                    command.Parameters.AddWithValue("@LaneID", booking.LaneId);
                    command.Parameters.AddWithValue("@BookingDate", booking.BookingTime);
                    command.Parameters.AddWithValue("@PartnerID",booking.PartnerId);
                    await command.Connection.OpenAsync();
                    var NoOfRow = command.ExecuteNonQuery();
                    if (NoOfRow ==1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}

