using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Application.Interfaces;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;

namespace TccSite.Application.Services
{
    public class ConfiguracoesService : IConfiguracoesService
    {
        private readonly IConfiguracoesRepository _repo;

        public ConfiguracoesService(IConfiguracoesRepository repo)
        {
            _repo = repo;
        }

        public Configuracoes GetConfiguracao()
            => _repo.GetConfiguracao();

        public void AtualizarConfiguracao(Configuracoes configuracoes)
            => _repo.AtualizarConfiguracao(configuracoes);

        public List<Configuracoes> GetConfiguracoes()
            => _repo.GetConfiguracoes();
    }
}
