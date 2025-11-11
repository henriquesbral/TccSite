using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCCAPIESP32.Domain.Entities;
using TCCAPIESP32.Infrastructure.Data;

namespace TCCAPIESP32.Application.Services
{
    public class AlertaService
    {
        private readonly ILogger<AlertaService> _logger;
        private readonly AppDbContext _context;

        public AlertaService(IConfiguration configuration, ILogger<AlertaService> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> SalvarAlertaAsync(decimal nivelRio)
        {
            bool result = false;

            try
            {
                var config = _context.Configuracoes.FirstOrDefault();
                var alerta = new Alerta();

                if (config is not null)
                {
                    GerarAlertaNivelRio(config.LimiteAlertaBaixo, config.LimiteAlertaMedio, config.LimiteAlertaAlto, config.LimiteAlertaCritico);
                }

                _context.Add(alerta);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex) 
            {
                result = false;
            }

            return result;
        }

        private void GerarAlertaNivelRio(decimal baixo, decimal medio, decimal alto, decimal critico)
        {
            var nivelBaixo = baixo;
            var nivelMedio = medio;
            var nivelAlto = alto;
            var nivelCritico = critico;
        }
    }
}
