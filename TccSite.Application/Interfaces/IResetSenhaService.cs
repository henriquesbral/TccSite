using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Domain.Entities;

namespace TccSite.Application.Interfaces
{
    public interface IResetSenhaService
    {
        void SalvarNovaSolicitacao(string email, string novaSenha, bool autorizado);
    }
}
