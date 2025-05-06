using E_Ticaret.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Ticaret.DataContext.Configurations

{
    public class KullaniciConfiguration : IEntityTypeConfiguration<Kullanici>

    {
        public void Configure(EntityTypeBuilder<Kullanici> builder)
        {
           builder.Property(x => x.Adi)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);
            builder.Property(x => x.Soyadi)
                .IsRequired()
                 .HasColumnType("varchar(50)")
                .HasMaxLength(50);
            builder.Property(x => x.Email)
                .IsRequired()
                  .HasColumnType("varchar(50)")
                .HasMaxLength(100);
            builder.Property(x => x.KullaniciAdi)
                .IsRequired()
                  .HasColumnType("varchar(50)")
                .HasMaxLength(50);
            
            builder.Property(x => x.Sifre)
                .IsRequired()
                  .HasColumnType("varchar(50)")
                .HasMaxLength(100);
            builder.Property(x => x.Telefon)
                
                    .HasColumnType("nvarchar(50)")
                    .HasMaxLength(50);
            builder.Property(x => x.Adres)
                
                    .HasColumnType("varchar(50)")
                    .HasMaxLength(50);
            // bir tane admin kaydı yapalım  iceriye girerken başlangıc lazım


            builder.HasData(
                new Kullanici
                {
                    Id = 1,
                    Adi = "ÜmitAdmin",
                    Soyadi = "Admin",
                    Email = "email@hotmail.com",
                    KullaniciAdi = "admin",
                    Sifre="123",
                    Telefon = "123456789",
                    Adres = "İstanbul KaynarcaMah",
                    AdminMi = true,
                    AktifMi = true,


                }
                );



        }
    }
}
