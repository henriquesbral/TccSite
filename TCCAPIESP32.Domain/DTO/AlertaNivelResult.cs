using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCCAPIESP32.Domain.Enums;

namespace TCCAPIESP32.Domain.DTO
{
    public class AlertaNivelRioResult
    {
        const string _mensagemNivelRioBaixo = "ligeiramente acima do normal.";
        const string _mensagemNivelRioMedio = "aumento rápido do nível.";
        const string _mensagemNivelRioAlto = "risco de transbordamento.";
        const string _mensagemNivelRioCritico = "evacuação recomendada.";

        public string Mensagem { get; set; } = string.Empty;
        public StatusAlertaEnum Status { get; set; }
        public decimal NivelAtual { get; set; }

        public AlertaNivelRioResult GerarAlertaNivelRio(
        decimal nivelAtual,
        decimal baixo,
        decimal medio,
        decimal alto,
        decimal critico)
        {
            StatusAlertaEnum status;
            string mensagem;

            if (nivelAtual <= baixo)
            {
                status = StatusAlertaEnum.Baixo;
                mensagem = _mensagemNivelRioBaixo;
            }
            else if (nivelAtual <= medio)
            {
                status = StatusAlertaEnum.Médio;
                mensagem = _mensagemNivelRioMedio;
            }
            else if (nivelAtual <= alto)
            {
                status = StatusAlertaEnum.Alto;
                mensagem = _mensagemNivelRioAlto;
            }
            else
            {
                status = StatusAlertaEnum.Crítico;
                mensagem = _mensagemNivelRioCritico;
            }

            return new()
            {
                Mensagem = $"Nível do rio é {nivelAtual} cm, {mensagem}",
                Status = status,
                NivelAtual = nivelAtual
            };
        }
    }
}
