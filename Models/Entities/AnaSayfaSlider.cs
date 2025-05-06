using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models.Entities
{
    public class AnaSayfaSlider: Interface
    {
        public int Id { get; set; }
        [Display(Name = "Resim")]
        public string? Resim { get; set; }
        [Display(Name = "Başlık")]
        public required string Baslik { get; set; }
        [Display(Name = "Açıklama")]
        public required string Aciklama { get; set; }
        [Display(Name = "Link")]
        public string? Link { get; set; }
        [Display(Name = "Aktif?")]
        public bool AktifMi { get; set; } = false;


    }
    
}
