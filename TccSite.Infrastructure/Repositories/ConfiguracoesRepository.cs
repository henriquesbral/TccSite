using Microsoft.EntityFrameworkCore;
using TccSite.Data.Context;
using TccSite.Domain.Entities;

namespace TccSite.Infrastructure.Repository
{
    public class ConfiguracoesRepository 
    {
        private readonly DataContext _context;

        public ConfiguracoesRepository(DataContext context)
        {
            _context = context;
        }

        public Configuracoes GetConfiguracaoAsync()
        {
            var configuracoes = _context.Configuracoes.LastOrDefault();
            return configuracoes;
        }

        public void AtualizarConfiguracaoAsync(Configuracoes config)
        {
            _context.Configuracoes.Update(config);
            _context.SaveChangesAsync();
        }
    }

}
