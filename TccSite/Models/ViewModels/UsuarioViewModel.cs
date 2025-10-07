using TccSite.Domain.Entities;

namespace TccSite.Web.ViewModels
{
    public class UsuarioViewModel
    {
        public int CodUsuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int CodPerfilUsuario { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }

}
