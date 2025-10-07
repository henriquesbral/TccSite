﻿using Microsoft.EntityFrameworkCore;
using TccSite.Domain.Entities;

namespace TccSite.Data.Context
{
    public class DataContext : DbContext
    {
        #region Constructor

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        #endregion

        #region Properties

        public DbSet<Alerta> Alerta { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<LogDeAcessos> LogDeAcessos { get; set; }
        public DbSet<PessoaCadastro> PessoaCadastro { get; set; }
        public DbSet<StatusAlerta> StatusAlerta { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<ImagensEsp32> ImagensEsp32 { get; set; }
        public DbSet<Configuracoes> Configuracoes { get; set; }


        #endregion

        #region OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Relatorios>().HasNoKey();
        }

        #endregion

    }
}
