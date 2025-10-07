namespace TccSite.Web.ViewModels
{
    public class RelatorioViewModel
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public decimal NivelMax { get; set; }
        public decimal NivelMin { get; set; }
        public decimal NivelMedio { get; set; }

        public List<DadoRelatorio> Dados { get; set; } = new();
    }

    public class DadoRelatorio
    {
        public DateTime Data { get; set; }
        public decimal NivelMetro { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class EstatisticaRelatorio
    {
        public decimal NivelMax { get; set; }
        public decimal NivelMin { get; set; }
        public decimal NivelMedio { get; set; }
    }


}
