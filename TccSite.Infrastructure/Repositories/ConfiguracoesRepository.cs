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
            return _context.Configuracoes.Where(x => x.DataConfiguracao <= DateTime.Now)
                .OrderByDescending(x => x.DataConfiguracao).FirstOrDefault();
        }

        public void AtualizarConfiguracao(Configuracoes config)
        {
            _context.Configuracoes.Update(config);
            _context.SaveChangesAsync();
        }

        public List<Configuracoes> GetConfiguracoes()
        {
            var configuracoes = _context.Configuracoes.OrderByDescending(x => x.CodConfiguracoes).ToList();
            return configuracoes;
        }
    }

}
