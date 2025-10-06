using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCCAPIESP32.Domain.Entities
{
    public class LogImagensEsp32
    {
        [Key]
        public int CodLogImagensEsp32 { get; set; }

        public int CodEventoImagem { get; set; }

        public string MensagemProcessamentoStatus { get; set; }

        public DateTime DataLog { get; set; }
    }
}
