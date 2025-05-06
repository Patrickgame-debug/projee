using E_Ticaret.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace E_Ticaret.DataContext.Configurations
{
    public class MarkaConfiguration : IEntityTypeConfiguration<Marka>
    {
        public void Configure(EntityTypeBuilder<Marka> builder)
        {
            
            builder.Property(x => x.MarkaAdi)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Resim)
                .HasMaxLength(50);



        }    
    }

}
