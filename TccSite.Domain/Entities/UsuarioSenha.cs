using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TccSite.Domain.Entities
{
    public class UsuarioSenha
    {
        [Key]
        public int CodUsuarioSenha { get; set; }

        [ForeignKey("Usuario")]
        public int CodUsuario { get; set; }
        public string SaltSenhaAtual { get; set; }
        public string HashSenhaAtual { get; set; }
        public string SaltSenha1 { get; set; }
        public string HashSenha1 { get; set; }
        public string SaltSenha2 { get; set; }
        public string HashSenha2 { get; set; }
        public DateTime DataCadastroSenha { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }

        //// Relacionamento opcional com Usuario
        //public Usuario? Usuario { get; set; }
    }
}
