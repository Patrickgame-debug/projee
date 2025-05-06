using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Ticaret.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnaSayfaSliderlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resim = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Baslik = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnaSayfaSliderlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Iletisimler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Soyadi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mesaj = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    SiparisNo = table.Column<int>(type: "int", nullable: true),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iletisimler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kampanyalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KampanyaAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Resim = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    SiparisNo = table.Column<int>(type: "int", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kampanyalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAdi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resim = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    UstKategoriId = table.Column<int>(type: "int", nullable: true),
                    UstMenudeGozuksunMu = table.Column<bool>(type: "bit", nullable: false),
                    SiparisNo = table.Column<int>(type: "int", nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kullancilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Soyadi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 100, nullable: false),
                    KullaniciAdi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Sifre = table.Column<string>(type: "varchar(50)", maxLength: 100, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Adres = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    AdminMi = table.Column<bool>(type: "bit", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KullaniciGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullancilar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Markalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarkaAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resim = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    SiparisNo = table.Column<int>(type: "int", nullable: true),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunAdi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resim = table.Column<string>(type: "varchar(200)", maxLength: 50, nullable: true),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndirimliFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    Sırano = table.Column<int>(type: "int", nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GuncellemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnasayfadaGozuksunMu = table.Column<bool>(type: "bit", nullable: false),
                    StokAdedi = table.Column<int>(type: "int", nullable: false),
                    UrunKodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KategoriId = table.Column<int>(type: "int", nullable: true),
                    MarkaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Urunler_Kategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Urunler_Markalar_MarkaId",
                        column: x => x.MarkaId,
                        principalTable: "Markalar",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Kategoriler",
                columns: new[] { "Id", "Aciklama", "AktifMi", "GuncellemeTarihi", "KategoriAdi", "KayitTarihi", "Resim", "SiparisNo", "UstKategoriId", "UstMenudeGozuksunMu" },
                values: new object[,]
                {
                    { 1, "Bilgisayar kategorisi", true, new DateTime(2025, 4, 23, 13, 56, 0, 497, DateTimeKind.Local).AddTicks(2060), "Bilgisayar", new DateTime(2025, 4, 23, 13, 56, 0, 495, DateTimeKind.Local).AddTicks(5924), "bilgisayar.png", 1, 0, true },
                    { 2, "Telefon kategorisi", true, new DateTime(2025, 4, 23, 13, 56, 0, 497, DateTimeKind.Local).AddTicks(3238), "Telefon", new DateTime(2025, 4, 23, 13, 56, 0, 497, DateTimeKind.Local).AddTicks(3236), "telefon.png", 2, 0, true }
                });

            migrationBuilder.InsertData(
                table: "Kullancilar",
                columns: new[] { "Id", "Adi", "AdminMi", "Adres", "AktifMi", "Email", "GuncellemeTarihi", "KayitTarihi", "KullaniciAdi", "KullaniciGuid", "Sifre", "Soyadi", "Telefon" },
                values: new object[] { 1, "ÜmitAdmin", true, "İstanbul KaynarcaMah", true, "email@hotmail.com", new DateTime(2025, 4, 23, 13, 56, 0, 498, DateTimeKind.Local).AddTicks(7198), new DateTime(2025, 4, 23, 13, 56, 0, 498, DateTimeKind.Local).AddTicks(7195), "admin", new Guid("443aefcb-c5ec-40f3-9f2f-a338e37905b6"), "123", "Admin", "123456789" });

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_KategoriId",
                table: "Urunler",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_MarkaId",
                table: "Urunler",
                column: "MarkaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnaSayfaSliderlar");

            migrationBuilder.DropTable(
                name: "Iletisimler");

            migrationBuilder.DropTable(
                name: "Kampanyalar");

            migrationBuilder.DropTable(
                name: "Kullancilar");

            migrationBuilder.DropTable(
                name: "Urunler");

            migrationBuilder.DropTable(
                name: "Kategoriler");

            migrationBuilder.DropTable(
                name: "Markalar");
        }
    }
}
