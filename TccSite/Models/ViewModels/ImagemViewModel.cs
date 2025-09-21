namespace TccSite.Models.ViewModels
{
    public class ImagemViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty; // Caminho da imagem salva
        public decimal NivelMetro { get; set; }
        public DateTime DataCaptura { get; set; }
    }
}
