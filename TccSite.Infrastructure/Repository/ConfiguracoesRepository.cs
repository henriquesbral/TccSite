using Microsoft.EntityFrameworkCore;
using TccSite.Data.Context;
using TccSite.Models.Entities;
using TccSite.Models.Interfaces;

namespace TccSite.Infrastructure.Repository
{
    public class ConfiguracoesRepository : IConfiguracoesRepository
    {
        private readonly DataContext _context;

        public ConfiguracoesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Configuracoes> GetConfiguracaoAsync()
        {
            return await _context.Configuracoes.FirstOrDefaultAsync();
        }

        public async Task AtualizarConfiguracaoAsync(Configuracoes config)
        {
            _context.Configuracoes.Update(config);
            await _context.SaveChangesAsync();
        }
    }

}
