
using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models.Entities
{
    public class Kullanici : Interface
    {
        // Kullanici sınıfı, Interface sınıfından türetilmiştir.
        public int Id { get; set; }
        [Display(Name ="Adı")]
        public required string Adi { get; set; }
        [Display(Name = "Soyadı")]
        public required string Soyadi { get; set; }
        [Display(Name = "E-Mail")]
        public required string Email { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public required string? KullaniciAdi { get; set; }
        [Display(Name = "Şifre")]
        public required  string Sifre { get; set; }

        [Display(Name = "Telefon")]
        public string? Telefon { get; set; }
        [Display(Name = "Adres")]
        public string? Adres { get; set; }
        [Display(Name = "Admin?")]
        public bool AdminMi { get; set; }
        [Display(Name = "Aktif?")]
        public bool AktifMi { get; set; }
        // burada kayıt tearihi ve güncelleme tarihi için default değerler verildi artık configurtions kısmında bir daha vermeme gerek yok 
        [Display(Name = "Kayıt Tarihi")]
        public DateTime? KayitTarihi { get; set; } = DateTime.Now;
        [Display(Name = "Güncelleme Tarihi")]
        public DateTime? GuncellemeTarihi { get; set; } = DateTime.Now;
        public Guid? KullaniciGuid { get; set; } = Guid.NewGuid();

        public List<Adress>? Adresler { get; set; } //  adres ile  kullanı arasında BAĞ KURMUS OLDUK BÖYLECE 


    }
    
}
