using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models.Entities
{
    public class SiparisDetay : Interface
    {
        public int Id { get; set; }
        [Display(Name = "Sipariş Numarası")]
        public int SiparisId { get; set; } // Sipariş ID'si
        [Display(Name = "Sipariş")]
        public Siparis? Siparis { get; set; } // İlişkili sipariş
        [Display(Name = "Ürün Numarası")]
        public int UrunId { get; set; } // Ürün ID'si
        [Display(Name = "Ürün")]
        public Urun? Urun { get; set; } // İlişkili ürün

       
        public int? Miktar { get; set; } // Ürün adedi
        [Display(Name = "Birim Fiyatı")]
        public decimal BirimFiyati { get; set; } // Ürün fiyatı  





    }


    
}
