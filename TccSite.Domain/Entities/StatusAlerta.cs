using System.ComponentModel.DataAnnotations;

namespace TccSite.Domain.Entities
{
    public class StatusAlerta
    {
        [Key]
        public int CodStatusAlerta { get; set; }

        public string NomeStatus { get; set; }
    }
}
