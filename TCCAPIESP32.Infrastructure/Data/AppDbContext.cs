using Microsoft.EntityFrameworkCore;
using TCCAPIESP32.Domain.Entities;

namespace TCCAPIESP32.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ImagensEsp32> ImagensEsp32 { get; set; }
        public DbSet<LogImagensEsp32> LogImagensEsp32 { get; set; }
        public DbSet<Alerta> Alerta { get; set; }
        public DbSet<StatusAlerta> StatusAlerta { get; set; }
        public DbSet<Configuracoes> Configuracoes { get; set; }
    }
}
