using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TccSite.Models.Entities
{
    public class Alerta
    {
        [Key]
        public int CodAlerta { get; set; }

        public string NomeAlerta { get; set; }

        [ForeignKey("StatusAlerta")]
        public int CodStatusAlerta { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataDesativacao { get; set; }
    }
}
