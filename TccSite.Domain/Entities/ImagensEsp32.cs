using System.ComponentModel.DataAnnotations;

namespace TccSite.Domain.Entities
{
    public class ImagensEsp32
    {
        [Key]
        public int CodEventoImagem { get; set; }

        public string NomeArquivo { get; set; }

        public string CaminhoArquivo { get; set; }

        public DateTime DataEnvio { get; set; }
    }
}
