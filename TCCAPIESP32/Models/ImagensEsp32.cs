using System.ComponentModel.DataAnnotations;

namespace TCCAPIESP32.Models
{
    public class ImagensEsp32
    {
        [Key]
        public int CodEventoImagem { get; set; }

        [Required]
        public string NomeArquivo { get; set; } = string.Empty;

        [Required]
        public string CaminhoArquivo { get; set; } = string.Empty;

        public DateTime DataEnvio { get; set; }
    }
}
