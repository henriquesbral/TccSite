using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TccSite.Domain.Entities
{
    public class ResetSenha
    {
        [Key]
        public int CodResetSenha { get; set; }

        public string Email { get; set; }

        public string NovaSenha { get; set; }

        public bool Autorizado { get; set; }

        public DateTime DataCadatroSolicitacao { get; set; }
    }
}
