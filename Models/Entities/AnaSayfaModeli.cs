
namespace E_Ticaret.Models.Entities
{
    public class AnaSayfaModeli
    {
        public List<AnaSayfaSlider>? Sliders { get; set; }
        public List<Urun>? Urunler { get; set; }
        public List<Kampanya>? Kampanyalar { get; set; }


        // yeni ekledim 7.05.2025
        public List<Urun>? OnerilenUrunler { get; set; }
    }
}
