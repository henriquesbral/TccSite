namespace TccSite.Web.Models.ViewModels
{
    public class PainelControleViewModel
    {
        public string NomeArquivo { get; set; } = string.Empty;
        public string CaminhoAbsoluto { get; set; } = string.Empty;
        public string UrlImagem { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
    }
}
