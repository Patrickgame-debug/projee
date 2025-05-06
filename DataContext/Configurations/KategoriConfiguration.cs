using E_Ticaret.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace E_Ticaret.DataContext.Configurations
{
    public class KategoriConfiguration : IEntityTypeConfiguration<Kategori>
    {
        public void Configure(EntityTypeBuilder<Kategori> builder)
        {
            builder.Property(x => x.KategoriAdi)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);
            builder.Property(x => x.Resim)
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);

            builder.HasData(
                new Kategori
                {
                    Id = 1,
                    KategoriAdi = "Bilgisayar",
                    Aciklama = "Bilgisayar kategorisi",
                    Resim = "bilgisayar.png",
                    AktifMi = true,
                    UstKategoriId = 0,
                    UstMenudeGozuksunMu = true,
                    SiparisNo = 1
                },
                new Kategori
                {
                    Id = 2,
                    KategoriAdi = "Telefon",
                    Aciklama = "Telefon kategorisi",
                    Resim = "telefon.png",
                    AktifMi = true,
                    UstKategoriId = 0,
                    UstMenudeGozuksunMu = true,
                    SiparisNo = 2
                }
            );
        }
    }
}
