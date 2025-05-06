using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models.Entities
{
    public class Yorum : Interface
    {
        public int Id { get; set; }

       
        public  int? SiparisDetayId { get; set; }

        public SiparisDetay? SiparisDetay { get; set; }

        public int? KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }

        
        [Range(1, 5)]
        public int? Puan { get; set; }

        [MaxLength(1000)]
        public string? YorumMetni { get; set; }

        public DateTime? Tarih { get; set; } = DateTime.Now;


        public int? UrunId { get; set; }
        public Urun? Urun { get; set; }

    }
}
