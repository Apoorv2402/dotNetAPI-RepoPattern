
using Microsoft.EntityFrameworkCore;
using StudyMash.API.Models;

namespace StudyMash.API.DataAccess 
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<City> cities { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
