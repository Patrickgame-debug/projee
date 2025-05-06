using E_Ticaret.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace E_Ticaret.DataContext.Configurations
{
    public class AnaSayfaSliderConfiguration : IEntityTypeConfiguration<AnaSayfaSlider>
    {
        public void Configure(EntityTypeBuilder<AnaSayfaSlider> builder)
        {
            // AnaSayfaSlider sınıfının özelliklerini yapılandırıyoruz.
            builder.Property(a => a.Baslik).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Aciklama).IsRequired().HasMaxLength(500);
            builder.Property(a => a.Resim).HasMaxLength(50);
            builder.Property(a => a.Link).HasMaxLength(200);
        }
    }
    
}
