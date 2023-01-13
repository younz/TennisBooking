using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TennisBooking.Helpers
{
    public abstract class Connection
    {
        protected string ConnectionString;
        public IConfiguration Configuration { get; }

        public Connection(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }

    }
}
