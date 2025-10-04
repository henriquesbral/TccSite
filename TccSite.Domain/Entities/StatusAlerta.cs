using System.ComponentModel.DataAnnotations;

namespace TccSite.Models.Entities
{
    public class StatusAlerta
    {
        [Key]
        public int CodStatusAlerta { get; set; }

        public string NomeStatus { get; set; }
    }
}
