using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TccSite.Domain.Entities
{
    public class Usuario
    {
        [Key]
        public int CodUsuario { get; set; }

        [ForeignKey("PessoaCadastro")]
        public int CodPessoaCadastro { get; set; }

        public string Email { get; set; }

        public int CodPerfilUsuario { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataCadastro { get; set; }

        public PessoaCadastro PessoaCadastro { get; set; }
    }
}
