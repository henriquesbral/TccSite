using Microsoft.EntityFrameworkCore;
using TccSite.Data.Context;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;

namespace TccSite.Infrastructure.Repository
{
    public class ConfiguracoesRepository : IConfiguracoesRepository
    {
        private readonly DataContext _context;

        public ConfiguracoesRepository(DataContext context)
        {
            _context = context;
        }

        public Configuracoes GetConfiguracao()
        {
            var configuracoes = _context.Configuracoes.LastOrDefault();
            return configuracoes;
        }

        public void AtualizarConfiguracao(Configuracoes config)
        {
            _context.Configuracoes.Update(config);
            _context.SaveChangesAsync();
        }
    }

}
