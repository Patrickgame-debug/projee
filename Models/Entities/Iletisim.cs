using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models.Entities
{
    public class Iletisim : Interface
    {
        // Bu sayfa genel olarak kullanıcıdan gelen mesajları iletisim formu gibi   mesela  ürün hatalı geldi
        // yada ürün hakkında bilgi almak istiyorum gibi mesajları tutmak için kullanılacak.

        // Iletisim sınıfı, Interface sınıfından türetilmiştir.
        public int Id { get; set; }

        [Display(Name = "Adı"),Required(ErrorMessage ="{0} Alanı Boş Geçilemez")]
        public required string Adi { get; set; }

        [Display(Name = "Soyadı") ,Required(ErrorMessage ="{0} Alanı Boş Geçilemez")]
        public required string Soyadi { get; set; }
        [Display(Name = "E-Mail"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez")]
        public required string Email { get; set; }
        [Display(Name = "Mesaj"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez")]
        public required string Mesaj { get; set; }

       
        public string? KullaniciAdi { get; set; }
        [Display(Name = "Adres")]
        public string? Adres { get; set; }
        [Display(Name = "Telefon")]
        public string? Telefon { get; set; }
        [Display(Name = "Aktif?")]
        public bool AktifMi { get; set; } = true;
        [Display(Name = "Siparis Numarası")]
        public int? SiparisNo { get; set; }

        [Display(Name = "Kayıt Tarihi")]
        public DateTime? KayitTarihi { get; set; } = DateTime.Now;
        [Display(Name = "Başlangıc Tarihi")]
        public DateTime? GuncellemeTarihi { get; set; } = DateTime.Now;
    
    }
}
