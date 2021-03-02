using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck.Models;
using Microsoft.Data.SqlClient;


namespace Truck.Entity
{
    partial class TruckContext : DbContext
    {

        public string ConnectionString;
        public TruckContext(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}
