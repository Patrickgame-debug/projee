using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models.Entities
{
    public class Urun : Interface
    {
        // Urun sınıfı, Interface sınıfından türetilmiştir.
        public int Id { get; set; }
        [Display(Name = "Ürün Adı")]
        public required string UrunAdi { get; set; }
        [Display(Name = "Ürün Açıklaması")]
        public string? Aciklama { get; set; }

        [Display(Name = "Resim")]
        public string? Resim { get; set; }
        [Display(Name = "Fiyat")]
        public decimal Fiyat { get; set; }
        [Display(Name = "İndirimli Fiyat")]
        public decimal? IndirimliFiyat { get; set; } = null; // bunu burada mı tutsam yoksa  kanpanya sınıfında mı tutsam bilemedim suanlık null ileride düşünürüz
        [Display(Name = "Aktif?")] // bunu burada mı tutsam yoksa  kanpanya sınıfında mı tutsam bilemedim suanlık null ileride düşünürüz

        public bool AktifMi { get; set; } = true;
        //ürünlerde sıralama yapabilmek için bir sıra numarası ekliyoruz
        [Display(Name = "Sıra Numarası")]
        public int Sırano { get; set; }
        [Display(Name = "Kayıt Tarihi")]
        public DateTime? KayitTarihi { get; set; } = DateTime.Now;
        [Display(Name = "Güncelleme Tarihi")]
        public DateTime? GuncellemeTarihi { get; set; } = DateTime.Now;
        [Display(Name = "Ana Sayfada Gözüksün mü?")]
        public bool AnasayfadaGozuksunMu { get; set; } = true;
        [Display(Name = "Stok Adeti")]
        public int StokAdedi { get; set; } = 0;
        [Display(Name = "Ürün Kodu")]
        public string? UrunKodu { get; set; } = null;

        // Ürün Kategorisi ve Markası referansları

        public int? KategoriId { get; set; } = null;
        public  Kategori?  Kategori { get; set; }

        public int? MarkaId { get; set; } = null;
        public Marka? Marka { get; set; }

        public List<Yorum>? Yorumlar { get; set; }
    }
}
