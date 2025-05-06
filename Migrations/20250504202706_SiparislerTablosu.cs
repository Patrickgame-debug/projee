using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret.Migrations
{
    /// <inheritdoc />
    public partial class SiparislerTablosu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Siparisler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisNo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ToplamTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KullaniciId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MusteriNumarasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaturaAdresi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeslimatAdresi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiparisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siparisler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiparisDetaylar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisId = table.Column<int>(type: "int", nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    Miktar = table.Column<int>(type: "int", nullable: true),
                    BirimFiyati = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiparisDetaylar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiparisDetaylar_Siparisler_SiparisId",
                        column: x => x.SiparisId,
                        principalTable: "Siparisler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiparisDetaylar_Urunler_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urunler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 4, 23, 27, 5, 578, DateTimeKind.Local).AddTicks(5481), new DateTime(2025, 5, 4, 23, 27, 5, 577, DateTimeKind.Local).AddTicks(304) });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 4, 23, 27, 5, 578, DateTimeKind.Local).AddTicks(6689), new DateTime(2025, 5, 4, 23, 27, 5, 578, DateTimeKind.Local).AddTicks(6687) });

            migrationBuilder.UpdateData(
                table: "Kullancilar",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi", "KullaniciGuid" },
                values: new object[] { new DateTime(2025, 5, 4, 23, 27, 5, 580, DateTimeKind.Local).AddTicks(695), new DateTime(2025, 5, 4, 23, 27, 5, 580, DateTimeKind.Local).AddTicks(692), new Guid("da3ad907-4cb3-47c6-849b-20b765a6baa4") });

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetaylar_SiparisId",
                table: "SiparisDetaylar",
                column: "SiparisId");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetaylar_UrunId",
                table: "SiparisDetaylar",
                column: "UrunId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiparisDetaylar");

            migrationBuilder.DropTable(
                name: "Siparisler");

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 2, 15, 25, 53, 747, DateTimeKind.Local).AddTicks(4132), new DateTime(2025, 5, 2, 15, 25, 53, 745, DateTimeKind.Local).AddTicks(8443) });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 2, 15, 25, 53, 747, DateTimeKind.Local).AddTicks(5373), new DateTime(2025, 5, 2, 15, 25, 53, 747, DateTimeKind.Local).AddTicks(5371) });

            migrationBuilder.UpdateData(
                table: "Kullancilar",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi", "KullaniciGuid" },
                values: new object[] { new DateTime(2025, 5, 2, 15, 25, 53, 748, DateTimeKind.Local).AddTicks(9237), new DateTime(2025, 5, 2, 15, 25, 53, 748, DateTimeKind.Local).AddTicks(9233), new Guid("54d4117b-92ef-4e84-9034-b6904e73e0b1") });
        }
    }
}
