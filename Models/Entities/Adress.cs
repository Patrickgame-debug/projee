using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models.Entities
{
    public class Adress : Interface
    {
        public int Id { get; set; }

        [Display(Name = "Adres Başlığı"), StringLength(60),Required(ErrorMessage = "{0} Alanı Zorunludur!")]
        public required string AdresBasligi { get; set; }// Ev, İş, Yazlık, Yazlık

        [Display(Name = "Şehir"), StringLength(60), Required(ErrorMessage = "{0} Alanı Zorunludur!")]
        public  required string Sehir { get; set; } // Şehir

        [Display(Name = "İlçe"), StringLength(60), Required(ErrorMessage = "{0} Alanı Zorunludur!")]
        public required string Ilce { get; set; } // İlçe

        [Display(Name = "Açık Adres"),DataType(DataType.MultilineText), Required(ErrorMessage = "{0} Alanı Zorunludur!")]
        public required string AcikAdres { get; set; } // Açık Adres

        [Display(Name = "Fatura Adresi")]
        public bool FaturaAdres { get; set; } // Fatura Adresi

        [Display(Name = "Teslimat Adresi")]
        public bool TeslimatAdres { get; set; } // Teslimat Adresi

        public bool AktifMi { get; set; } = true;


        [Display(Name = "Kayıt Tarihi"),ScaffoldColumn(false)]
        public DateTime? KayitTarihi { get; set; } = DateTime.Now;

        public Guid? AdressGuid { get; set; } = Guid.NewGuid();
        public int? KullaniciId { get; set; } // Kullanıcı ID

        public Kullanici? Kullanici { get; set; } // Kullanıcı


    }
}
