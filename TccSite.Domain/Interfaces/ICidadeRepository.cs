using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Domain.Entities;

namespace TccSite.Domain.Interfaces
{
    public interface ICidadeRepository
    {
        List<Cidade> GetCidades();

        Cidade GetCidadePorId(int id);

        Cidade GetCidadePorNome(string nomeCidade);
    }
}
