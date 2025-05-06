using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models.Entities
{
    public class Siparis :Interface
    {
        public int Id { get; set; }

        [Display(Name = "Sipariş Numarası"), StringLength(60)]
        public required string SiparisNo { get; set; }

        [Display(Name = "Sipariş Toplamı")]
        public required decimal ToplamTutar { get; set; }

        [Display(Name = "Müşteri Numarası")]
        public required int KullaniciId { get; set; } // Kullanıcı ID'si hangi kullanıcı Sipariş verdi
        [Display(Name = "Müşteri")]
        public required string MusteriNumarasi { get; set; } // KullanıcıId' nin guild değerini tutailiriz 
        [Display(Name = "Fatura Adresi")]
        public required string FaturaAdresi { get; set; } // Teslimat adresi
        [Display(Name = "Teslimat Adresi")]
        public required string TeslimatAdresi { get; set; } // Teslimat adresi
        [Display(Name = "Sipariş Tarihi")]
        public DateTime SiparisTarihi { get; set; }

        [Display(Name = "Müşteri")]
        public Kullanici? Kullanici { get; set; } // İlişkili kullanıcı
        public List<SiparisDetay>? SiparisDetaylari { get; set; }


        public EnumSiparisDurumu SiparisDurumu { get; set; }

    }


    public enum EnumSiparisDurumu
    {
        [Display(Name = "Onay Bekliyor")]
        Waiting,
        [Display(Name = "Onaylandı")]
        Approved,
        [Display(Name = "Kargoya Verildi")]
        Shipped,
        [Display(Name = "Tamamlandı")]
        Completed,
        [Display(Name = "İptal Edildi")]
        Cancelled,
        [Display(Name = "İade Edildi")]
        Returned
    }

}
