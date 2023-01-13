using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisBooking.Models;

namespace TennisBooking.Interface
{
    public interface ILane
    {
        public Task<List<Lane>> GetallLanes();
        public Task<Lane> GetLane(int id);
        public Task<bool> EditLane(Lane lane);
        public Task<bool> AddLane(Lane lane);
        public Task<Lane> RemoveLane(Lane lane);
    }
}
