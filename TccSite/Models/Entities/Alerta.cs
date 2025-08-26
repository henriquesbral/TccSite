using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TccSite.Models.Enums;

namespace TccSite.Models.Entities
{
    public class Alerta
    {
        [Key]
        public int CodAlerta { get; set; }

        public string NomeAlerta { get; set; }

        [ForeignKey("StatusAlerta")]
        public int CodStatusAlerta { get; set; }

        [NotMapped] 
        public StatusAlertaEnum StatusAlerta
        {
            get => (StatusAlertaEnum)CodStatusAlerta;
            set => CodStatusAlerta = (int)value;
        }

        public string Ativo { get; set; }

        public DateTime? DataCadastro { get; set; }

        public DateTime? DataDesativacao { get; set; }

        public string Descricao { get; set; }

        public int? NivelRio { get; set; }
    }
}
