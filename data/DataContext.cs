using DemoApi.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoApi.data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
        
        }

        public DbSet<User> Users { get; set; }

    }
}
