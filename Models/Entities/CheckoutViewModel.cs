using System.Net;


namespace E_Ticaret.Models.Entities
{
    public class CheckoutViewModel
    {
        public List<CartLine>? CartProducts { get; set; } // Sepetteki ürünler
        public decimal TotalPrice { get; set; } // Toplam fiyat
        
        public decimal KargoUcreti { get; set; } // Kargo ücreti
        public string KargoTipi { get; set; } // Kargo tipi (Standart, Hızlı)


        public List<Adress>? Addresses { get; set; } // Kullanıcının adresleri
        public int? SelectedAddressId { get; set; } // Kullanıcının seçtiği adres
    }

}
