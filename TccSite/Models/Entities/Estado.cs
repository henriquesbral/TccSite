using System.ComponentModel.DataAnnotations;

namespace TccSite.Models.Entities
{
    public class Estado
    {
        [Key]
        public int CodEstado { get; set; }

        public string NomeEstado { get; set; }

        public string SiglaEstado { get; set; }
    }
}
