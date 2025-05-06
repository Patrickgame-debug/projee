using E_Ticaret.DataContext.Configurations;
using E_Ticaret.Models.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace E_Ticaret.DataContext
{
    public class DatabaseContext : DbContext
    {
        // veri tabanı nesnelerimizi tutucağımız yer 
        // . Yani, her biri uygulamadaki bir sınıfın (modelin) veritabanındaki bir tabloyla eşleşmesini sağlıyor. 

        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Marka> Markalar { get; set; }
        public DbSet<Kullanici> Kullancilar { get; set; }
        public DbSet<Adress> Adresler { get; set; }
        public DbSet<Kampanya> Kampanyalar { get; set; }
        public DbSet<Iletisim> Iletisimler { get; set; }

        public DbSet<Siparis> Siparisler  { get; set; }
        public DbSet<SiparisDetay> SiparisDetaylar { get; set; }

        public DbSet<AnaSayfaSlider> AnaSayfaSliderlar { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }
        public DbSet<Favori> Favoriler { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(@"Server=DESKTOP-LH4OOEA\SQLEXPRESS;Database=E_TicaretDB; Trusted_Connection=True ; TrustServerCertificate=True;");
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Burada Fluent API ile model yapılandırmalarını yapıyoruz
            // Örneğin, bir tabloya birincil anahtar eklemek veya ilişkileri tanımlamak gibi işlemleri burada yapabiliriz.
            modelBuilder.ApplyConfiguration(new KategoriConfiguration());
            modelBuilder.ApplyConfiguration(new UrunConfiguration());
            modelBuilder.ApplyConfiguration(new MarkaConfiguration());
            modelBuilder.ApplyConfiguration(new KullaniciConfiguration());
            modelBuilder.ApplyConfiguration(new KampanyaConfiguration());
            modelBuilder.ApplyConfiguration(new IletisimConfiguration());
            modelBuilder.ApplyConfiguration(new AnaSayfaSliderConfiguration());
            // ❗ Yorum tablosunun ilişki ayarları
            modelBuilder.Entity<Yorum>()
                .HasOne(y => y.SiparisDetay)
                .WithMany()
                .HasForeignKey(y => y.SiparisDetayId)
                .OnDelete(DeleteBehavior.Restrict); // Çoklu cascade'i önler

            modelBuilder.Entity<Yorum>()
                .HasOne(y => y.Kullanici)
                .WithMany()
                .HasForeignKey(y => y.KullaniciId)
                .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silinirse yorumu da sil


            base.OnModelCreating(modelBuilder);
        }

    }
}
