using Bookify.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookify.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>  options) : base (options) {}
        public DbSet<Client> Clients { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}