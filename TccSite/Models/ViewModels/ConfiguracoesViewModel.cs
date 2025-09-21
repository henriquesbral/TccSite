namespace TccSite.Models.ViewModels
{
    public class ConfiguracoesViewModel
    {
        public decimal NivelAtencao { get; set; }
        public decimal NivelCritico { get; set; }
        public int FrequenciaCaptura { get; set; } // em minutos
        public string EmailsAlerta { get; set; } = string.Empty;
    }
}
