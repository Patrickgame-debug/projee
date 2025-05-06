using E_Ticaret.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace E_Ticaret.DataContext.Configurations
{
    public class IletisimConfiguration : IEntityTypeConfiguration<Iletisim>

    {
        public void Configure(EntityTypeBuilder<Iletisim> builder)
        {
            builder.Property(x => x.Adi)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(x => x.Soyadi)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Mesaj)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(x => x.Telefon)
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);
            builder.Property(x => x.Adres)
                .HasMaxLength(100);
            builder.Property(x => x.KullaniciAdi)
                .HasMaxLength(20);


        }
    }
}
