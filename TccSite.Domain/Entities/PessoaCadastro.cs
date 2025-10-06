using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TccSite.Domain.Entities
{
    public class PessoaCadastro
    {
        [Key]
        public int CodPessoaCadastro { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        [Required]
        public string CPF { get; set; }

        public string Telefone { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Endereco { get; set; }

        public string CEP { get; set; }

        [ForeignKey("Cidade")]
        public int CodCidade { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
