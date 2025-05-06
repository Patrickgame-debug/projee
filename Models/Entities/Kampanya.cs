using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models.Entities
{
    public class Kampanya : Interface
    {
        // Kampanya VE DUYURU KISMI buraya  ekleme yapabiliriz işte indirim oranı gibi %20 bu kampanya %30 bu kampanya gibi
        // Kampanya sınıfı, Interface sınıfından türetilmiştir.
        public int Id { get; set; }
        [Display(Name = "Kampanya Adı")]
        public required string KampanyaAdi { get; set; }
        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }

        [Display(Name = "Resim")]
        public string? Resim { get; set; }
        [Display(Name = "Aktif?")]
        public bool AktifMi { get; set; } = true;
        [Display(Name = "Sipariş Numarası")]
        public int SiparisNo { get; set; }
        [Display(Name = "Kampanya'nın Başlangıç Tarihi")]
        public DateTime? BaslangicTarihi { get; set; }
        [Display(Name = "Kampanya'nın Bitiş Tarihi")]
        public DateTime? BitisTarihi { get; set; }

    }
}
