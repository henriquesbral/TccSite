using Microsoft.EntityFrameworkCore;
using TCCAPIESP32.Models;

namespace TCCAPIESP32.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ImagensEsp32> ImagensEsp32 { get; set; }
    }
}
