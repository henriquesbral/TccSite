using TccSite.Domain.Entities;

namespace TccSite.Application.Interfaces
{
    public interface ICidadeService
    {
        List<Cidade> GetCidades();

        Cidade GetCidadePorId(int id);
        
        Cidade GetCidadePorNome(string nomeCidade);
    }
}
