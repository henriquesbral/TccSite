using TccSite.Domain.Entities;

namespace TccSite.Application.Interfaces
{
    public interface IPessoaCadastroService
    {
        PessoaCadastro Obter(int id);

        void AtualizarPessoaCadastro(PessoaCadastro pessoa);

        void AdicionarPessoaCadastro(PessoaCadastro pessoa);
    }
}
