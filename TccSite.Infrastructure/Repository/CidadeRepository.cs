using TccSite.Data.Context;
using TccSite.Models.Entities;
using TccSite.Models.Interfaces;

namespace TccSite.Infrastructure.Repository
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly DataContext _context;

        public CidadeRepository(DataContext context)
        {
            _context = context;
        }

        public Cidade GetCidade()
        {
            throw new NotImplementedException();
        }
    }
}
