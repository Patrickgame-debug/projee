using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Ticaret.Models.Entities
{
    public class Marka : Interface
    {
        // Markalar sınıfı, Interface sınıfından türetilmiştir.
        public int Id { get; set; }
        [Display(Name = "Marka Adı")]
        public  required string MarkaAdi { get; set; }
        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }
        [Display(Name = "Resim")]
        public string? Resim { get; set; }
        [Display(Name = "Aktif?")]
        public bool AktifMi { get; set; } = true;
        [Display(Name = "Sipariş Numarası")]
        public int? SiparisNo { get; set; }

        //markaya ait ürünleri listelemek için kullanacağım bire çok ilişki
        // marka sayfamda  o markanın ürünleri listelemek için kullanacağım
        public IList<Urun>? Urunler { get; set; }

        


        public DateTime? KayitTarihi { get; set; } = DateTime.Now;
        public DateTime? GuncellemeTarihi { get; set; } = DateTime.Now;

        

    }
}
