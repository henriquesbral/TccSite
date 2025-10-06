using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TccSite.Domain.Entities
{
    public class Cidade
    {
        [Key]
        public int CodCidade { get; set; }

        public string NomeCidade { get; set; }

        [ForeignKey("Estado")]
        public int CodEstado { get; set; }
    }
}
