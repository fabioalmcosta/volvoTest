using Microsoft.EntityFrameworkCore;
using TruckWebApi.Models;

namespace TruckWebApi.DataAccess
{
    public class TruckContext : DbContext
    {
        public TruckContext(DbContextOptions<TruckContext> options)
            : base(options)
        { }

        public DbSet<Truck> Truck { get; set; }
    }
}
