using TccSite.Data.Context;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;

namespace TccSite.Infrastructure.Repository
{
    public class ImagensEsp32Repository : IImagensEsp32Repository
    {
        private readonly DataContext _context;

        public ImagensEsp32Repository(DataContext context)
        {
            _context = context;
        }

        public List<ImagensEsp32> GetImagens()
        {
            var imagens = _context.ImagensEsp32.OrderByDescending(x => x.DataEnvio).ToList();
            return imagens;
        }
    }
}
