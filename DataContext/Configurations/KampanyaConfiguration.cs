using E_Ticaret.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace E_Ticaret.DataContext.Configurations
{
    public class KampanyaConfiguration : IEntityTypeConfiguration<Kampanya>
    {
        public void Configure(EntityTypeBuilder<Kampanya> builder)
        {
            // Kampanya sınıfının özelliklerini yapılandırıyoruz.
           
            builder.Property(k => k.KampanyaAdi).IsRequired().HasMaxLength(100);
            builder.Property(k => k.Aciklama);
            builder.Property(k => k.Resim).HasMaxLength(50);
            
        }
    }
    
}
