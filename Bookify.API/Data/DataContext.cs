using Microsoft.EntityFrameworkCore;

namespace Bookify.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>  options) : base (options) {}

        //DbSet here
    }
}