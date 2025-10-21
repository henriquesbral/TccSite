using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TccSite.Domain.Entities;

namespace TccSite.Application.Interfaces
{
    public interface IUsuarioSenhaService
    {
        bool ValidarSenha(int codUsuario, string senhaDigitada);
        void AtualizarSenha(int codUsuario, string novaSenha);
        void CriarSenhaInicial(int codUsuario, string senhaInicial);
    }
}
