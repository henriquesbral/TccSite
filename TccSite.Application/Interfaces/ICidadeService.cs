using TccSite.Domain.Entities;

namespace TccSite.Application.Interfaces
{
    public interface ICidadeService
    {
        List<Cidade> GetCidade();
        Cidade GetCidadePorId(int id);
        Cidade GetCidadePorNome(string nomeCidade);
    }
}
