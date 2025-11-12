using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCCAPIESP32.Domain.Entities
{
    public class StatusAlerta
    {
        [Key]
        public int CodStatusAlerta { get; set; }

        public string NomeStatus { get; set; }
    }

}
