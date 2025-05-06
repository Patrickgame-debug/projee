using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models.Entities
{
    public class Favori : Interface
    {
        public int Id { get; set; }

        [Required]
        public int KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }

        [Required]
        public int UrunId { get; set; }
        public Urun? Urun { get; set; }

        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
    }
}
