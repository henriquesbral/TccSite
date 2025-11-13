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
            var configExistente = _context.Configuracoes.FirstOrDefault();

            if (configExistente != null)
            {
                configExistente.LimiteAlertaBaixo = config.LimiteAlertaBaixo;
                configExistente.LimiteAlertaMedio = config.LimiteAlertaMedio;
                configExistente.LimiteAlertaAlto = config.LimiteAlertaAlto;
                configExistente.LimiteAlertaCritico = config.LimiteAlertaCritico;
                configExistente.FrequenciaCaptura = config.FrequenciaCaptura;
                configExistente.NotificarEmail = config.NotificarEmail;
                configExistente.NotificacaoWhatsapp = config.NotificacaoWhatsapp;
                configExistente.DataConfiguracao = DateTime.Now;

                _context.Configuracoes.Update(configExistente);
            }
            else
            {
                config.DataConfiguracao = DateTime.Now;
                _context.Configuracoes.Add(config);
            }

            _context.SaveChanges(); // ✅ método síncrono, realmente executa o commit
        }


        public List<Configuracoes> GetConfiguracoes()
        {
            var configuracoes = _context.Configuracoes.OrderByDescending(x => x.CodConfiguracoes).ToList();
            return configuracoes;
        }
    }

}
