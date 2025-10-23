using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TccSite.Domain.DTOs
{
    public class UsuarioDTO
    {
        public int CodUsuario { get; set; }

        public int CodPessoaCadastro { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }
    }
}
