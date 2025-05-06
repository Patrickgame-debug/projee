using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models.Entities
{
    public class KayitSayfaModeli
    {
        [Required(ErrorMessage = "Email alanı zorunludur")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Sifre alanı zorunludur.")]
        public required string Sifre { get; set; }

        // kullanıcı adres  cubuğundan  geri döndürme işlemi varsa onu yakalar 
        public string? GeriDonusUrl { get; set; }

        public bool Hatırla { get; set; } = false;


    }
}
