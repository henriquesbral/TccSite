using System.Linq;
using TccSite.Data.Context;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;

namespace TccSite.Infrastructure.Repository
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly DataContext _context;

        public CidadeRepository(DataContext context)
        {
            _context = context;
        }

        public List<Cidade> GetCidades()
        {
            var cidade = _context.Cidade.ToList();
            return cidade;
        }

        public Cidade GetCidadePorId(int id)
        {
            var cidade = _context.Cidade.Where(x => x.CodCidade == id).FirstOrDefault();
            return cidade;
        }

        public Cidade GetCidadePorNome(string nomeCidade)
        {
            var cidade = _context.Cidade.Where(x => x.NomeCidade == nomeCidade).FirstOrDefault();
            return cidade;
        }
    }
}
