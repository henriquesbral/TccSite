using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TccSite.Domain.Entities
{
    public class Relatorios
    {
        public int CodStatusAlerta { get; set; }

        public string NomeAlerta { get; set; }

        public string NomeStatusAlerta { get; set; }

        public string Ativo { get; set; }

        public DateTime? DataCadastroAlerta { get; set; }
    }
}
