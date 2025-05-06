using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models.Entities
{
    public class Kategori : Interface
    {
        // Kategori sınıfı, Interface sınıfından türetilmiştir.
        public int Id { get; set; }
        [Display(Name = "Kategori Adı")]
        public required string KategoriAdi { get; set; }
        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }
        [Display(Name = "Resim")]
        public string? Resim { get; set; }
        [Display(Name = "Aktif?")]
        public bool AktifMi { get; set; } = true;



        // Üst Kategori ID ne olucak diyoruz mesela bu üst kategorinin altında ki kategori mi gibi  0 sa alt 1 ise üst kategori

        [Display(Name = "Üst Kategori")]
        public int? UstKategoriId { get; set; } = null;

        [Display(Name = "Üst Menüde Göster")]
        public bool UstMenudeGozuksunMu { get; set; } = true;
        [Display(Name = "Sıra Numarası")]
        public  int  SiparisNo { get; set; }


        // bir kategorinin birden cok ürünü olduğunu söylüyoruz bire çok ilişki
        // kategori sayfamda  o kategorinin ürünleri listelemek için kullanacağım
        public IList<Urun>? Urunler { get; set; }



        public DateTime? KayitTarihi { get; set; } = DateTime.Now;
        public DateTime? GuncellemeTarihi { get; set; } = DateTime.Now;

    }
}
