using TccSite.Domain.Entities;

namespace TccSite.Application.Interfaces
{
    public interface IConfiguracoesService
    {
        Configuracoes GetConfiguracao();

        List<Configuracoes> GetConfiguracoes();

        void AtualizarConfiguracao(Configuracoes config);
    }

}
