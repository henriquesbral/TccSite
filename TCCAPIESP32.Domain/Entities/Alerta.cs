using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCAPIESP32.Domain.Entities
{
    public class Alerta
    {
        [Key]
        public int CodAlerta { get; set; }

        public string NomeAlerta { get; set; }

        [ForeignKey("StatusAlerta")]
        public int CodStatusAlerta { get; set; }

        public string Ativo { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataDesativacao { get; set; }

        public string Descricao { get; set; }

        public decimal NivelRio { get; set; }
    }
}
