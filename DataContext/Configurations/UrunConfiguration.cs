using E_Ticaret.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace E_Ticaret.DataContext.Configurations
{
    public class UrunConfiguration : IEntityTypeConfiguration<Urun>

    {
        public void Configure(EntityTypeBuilder<Urun> builder)
        {
            builder.Property(x => x.UrunAdi)
             .IsRequired()
             .HasColumnType("varchar(50)")
             .HasMaxLength(50);
            builder.Property(x => x.Resim)
               
                .HasColumnType("varchar(200)")
                .HasMaxLength(50);


        }

    }  
}
