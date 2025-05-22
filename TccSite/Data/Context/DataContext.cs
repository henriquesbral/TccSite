using Microsoft.EntityFrameworkCore;
using TccSite.Models.Entities;

namespace TccSite.Data.Context
{
    public class DataContext : DbContext
    {
        #region Constructor

        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Properties
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Usuario> LogDeAcessos { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
