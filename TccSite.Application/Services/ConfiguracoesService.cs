using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Application.Interfaces;
using TccSite.Domain.Entities;
using TccSite.Infrastructure.Repository;

namespace TccSite.Application.Services
{
    public class ConfiguracoesService : IConfiguracoesService
    {
        private readonly ConfiguracoesRepository _repo;

        public ConfiguracoesService(ConfiguracoesRepository repo)
        {
            _repo = repo;
        }

        public Configuracoes GetConfiguracaoAsync()
            => _repo.GetConfiguracaoAsync();

        public void AtualizarConfiguracaoAsync(Configuracoes configuracoes)
            => _repo.AtualizarConfiguracaoAsync(configuracoes);
    }
}
