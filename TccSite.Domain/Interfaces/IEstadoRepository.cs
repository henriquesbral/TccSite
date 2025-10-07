using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Domain.Entities;

namespace TccSite.Domain.Interfaces
{
    public interface IEstadoRepository
    {
        Estado GetUf(int id);
        List<Estado> GetUfs();
    }
}
