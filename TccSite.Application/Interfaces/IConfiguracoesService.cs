using TccSite.Domain.Entities;

namespace TccSite.Application.Interfaces
{
    public interface IConfiguracoesService
    {
        Configuracoes GetConfiguracao();

        void AtualizarConfiguracao(Configuracoes config);
    }

}
