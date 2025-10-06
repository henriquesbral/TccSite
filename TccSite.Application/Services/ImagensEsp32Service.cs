using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Application.Interfaces;
using TccSite.Domain.Entities;
using TccSite.Infrastructure.Repository;

namespace TccSite.Application.Services
{
    public class ImagensEsp32Service : IImagensEsp32Service
    {
        private readonly ImagensEsp32Repository _repo;

        public ImagensEsp32Service(ImagensEsp32Repository repo)
        {
            _repo = repo;
        }

        public List<ImagensEsp32> GetImagens()
            => _repo.GetImagens();
    }
}
