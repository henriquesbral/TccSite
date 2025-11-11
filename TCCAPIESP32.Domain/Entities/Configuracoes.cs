using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCAPIESP32.Domain.Entities
{
    public class Configuracoes
    {
        [Key]
        public int CodConfiguracoes { get; set; }
        public decimal LimiteAlertaBaixo    { get; set;}
        public decimal LimiteAlertaMedio    { get; set;}
        public decimal LimiteAlertaAlto     { get; set;}
        public decimal LimiteAlertaCritico  { get; set;}
        public int FrequenciaCaptura    { get; set;}
        public bool NotificarEmail       { get; set;}
        public bool NotificacaoWhatsapp  { get; set;}
        public DateTime DataConfiguracao { get; set; }
    }
}
