using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCCAPIESP32.Domain.Entities;
using TCCAPIESP32.Domain.Enums;
using TCCAPIESP32.Infrastructure.Data;

namespace TCCAPIESP32.Application.Services
{
    public class AlertaService
    {
        private readonly ILogger<AlertaService> _logger;
        private readonly AppDbContext _context;

        const string _mensagemNivelRioBaixo = "ligeiramente acima do normal.";
        const string _mensagemNivelRioMedio = "aumento rápido do nível.";
        const string _mensagemNivelRioAlto = "risco de transbordamento.";
        const string _mensagemNivelRioCritico = "evacuação recomendada.";


        public AlertaService(IConfiguration configuration, ILogger<AlertaService> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> SalvarAlertaAsync(decimal nivelRio)
        {
            bool result;

            try
            {
                var config = _context.Configuracoes.FirstOrDefault();
                string msgAlerta = string.Empty;

                if (config is not null)
                {
                    msgAlerta = GerarAlertaNivelRio(nivelRio,
                        config.LimiteAlertaBaixo,
                        config.LimiteAlertaMedio,
                        config.LimiteAlertaAlto,
                        config.LimiteAlertaCritico);
                }

                int codStatusAlerta = GerarStatusAlerta(msgAlerta);

                var alerta = new Alerta()
                {
                    NomeAlerta = "Alerta Gerado por API + IA",
                    CodStatusAlerta = codStatusAlerta,
                    Ativo = "S",
                    DataCadastro = DateTime.Now,
                    DataDesativacao = null,
                    Descricao = msgAlerta.ToString(),
                    NivelRio = nivelRio
                };

                if (alerta is not null)
                {
                    _context.Add(alerta);
                    await _context.SaveChangesAsync();
                    result = true;
                }
                else
                {
                    result = false;
                }

            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        private string GerarAlertaNivelRio(decimal nivelAtual, decimal baixo, decimal medio, decimal alto, decimal critico)
        {
            string mensagem =
                nivelAtual <= baixo ? _mensagemNivelRioBaixo :
                nivelAtual <= medio ? _mensagemNivelRioMedio :
                nivelAtual <= alto ? _mensagemNivelRioAlto :
                                      _mensagemNivelRioCritico;

            return $"Nível do rio é {nivelAtual}cm, {mensagem}";
        }

        private int GerarStatusAlerta(string msgAlerta)
        {
            int codStatusAlerta = 0;

            if (msgAlerta.Contains(_mensagemNivelRioBaixo, StringComparison.OrdinalIgnoreCase))
                codStatusAlerta = (int)StatusAlertaEnum.Baixo;

            else if (msgAlerta.Contains(_mensagemNivelRioMedio, StringComparison.OrdinalIgnoreCase))
                codStatusAlerta = (int)StatusAlertaEnum.Médio;

            else if (msgAlerta.Contains(_mensagemNivelRioAlto, StringComparison.OrdinalIgnoreCase))
                codStatusAlerta = (int)StatusAlertaEnum.Alto;

            else if (msgAlerta.Contains(_mensagemNivelRioCritico, StringComparison.OrdinalIgnoreCase))
                codStatusAlerta = (int)StatusAlertaEnum.Crítico;

            return codStatusAlerta;
        }
    }
}
