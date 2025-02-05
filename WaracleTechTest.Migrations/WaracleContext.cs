using Microsoft.EntityFrameworkCore;
using WaracleTechTest.Data.Models;

namespace WaracleTechTest.Migrations
{
    public class WaracleContext : DbContext
    {
        public WaracleContext(DbContextOptions<WaracleContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("WaracleContext");

            optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
        }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }
    }
}