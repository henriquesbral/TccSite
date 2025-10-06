namespace TccSite.Domain.ViewModels
{
    public class ConfiguracoesViewModel
    {
        public int Id { get; set; }
        public decimal LimiteAlertaBaixo { get; set; }
        public decimal LimiteAlertaMedio { get; set; }
        public decimal LimiteAlertaAlto { get; set; }
        public decimal LimiteAlertaCritico { get; set; }
        public int FrequenciaCaptura { get; set; }

    }
}
