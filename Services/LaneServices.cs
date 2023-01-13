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
    public class LaneServices : Connection, ILane
    {
        private string _getAllLanes = "select * from Lane";
        private string _getLane = "select * from Lane where BaneNummer = @ID";
        private string _laneAdd = "insert into Lane Values (@ID, @Type)";
        private string _removeLane = "delete from Lane where BaneNummer = @ID";
        private string _updateLane = "Update Lane Set BaneNummer = @ID, BaneType = @Type where Banenummer = @ID";
        public LaneServices(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<Lane>> GetallLanes()
        {
            List<Lane> lanes = new List<Lane>();
            await using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await using (SqlCommand command = new SqlCommand(_getAllLanes, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            Lane lane = new Lane();
                            lane.BaneNummer = reader.GetInt32(0);
                            lane.Type = reader.GetString(1);
                            lanes.Add(lane);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("der skete en fejl");
                    }
                }
            }

            return lanes.ToList();
        }

        public async Task<Lane> GetLane(int id)
        {
            await using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await using (SqlCommand command = new SqlCommand(_getLane, connection))
                {
                    command.Parameters.AddWithValue("@ID",id);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        int LaneId = reader.GetInt32(0);
                        string name = reader.GetString(1);
                       
                        Lane lane = new Lane(LaneId, name);
                        return lane;
                    }
                }
            }
            return null;

        }

        public async Task<bool> EditLane(Lane lane)
        {
            await using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await using (SqlCommand command = new SqlCommand(_updateLane, connection))
                {
                    command.Parameters.AddWithValue("@ID", lane.BaneNummer);
                    command.Parameters.AddWithValue("@Type", lane.Type);
                    await command.Connection.OpenAsync();
                    var noOfRows = command.ExecuteNonQuery();
                    if (noOfRows == 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<bool> AddLane(Lane lane)
        {
            List<Lane> lanes = await GetallLanes();
            List<int> idList = new List<int>();
            foreach (var variable in lanes)
            {
                idList.Add(variable.BaneNummer);
            }

            if (idList.Count != 0)
            {
                int start = idList.Max();
                lane.BaneNummer = start + 1;
            }
            else
            {
                lane.BaneNummer = 1;
            }

            await using (SqlConnection  connection = new SqlConnection(ConnectionString))
            {
                await using (SqlCommand command = new SqlCommand(_laneAdd,connection))
                {
                    command.Parameters.AddWithValue("@ID", lane.BaneNummer);
                    command.Parameters.AddWithValue("@Type", lane.Type);
                    await command.Connection.OpenAsync();
                    var rowNumber = command.ExecuteNonQuery();
                    if (rowNumber == 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<Lane> RemoveLane(Lane lane)
        {
            if (lane != null)
            {
                await using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await using (SqlCommand command = new SqlCommand(_removeLane,connection))
                    {
                        command.Parameters.AddWithValue("@ID", lane.BaneNummer);
                        await command.Connection.OpenAsync();
                        var noOfRows = command.ExecuteNonQuery();
                        if (noOfRows == 1)
                        {
                            return lane;
                        }

                        return null;
                    }
                }
            }

            return null;
        }
    }
}
