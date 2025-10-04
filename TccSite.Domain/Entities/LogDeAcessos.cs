using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TccSite.Models.Entities
{
    public class LogDeAcessos
    {
        [Key]
        public int CodLogAcesso { get; set; }

        [ForeignKey("Usuario")]
        public int CodUsuario { get; set; }

        public DateTime DataUltimoAcesso { get; set; }
    }
}
