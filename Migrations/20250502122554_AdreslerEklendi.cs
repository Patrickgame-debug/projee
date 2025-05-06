using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret.Migrations
{
    /// <inheritdoc />
    public partial class AdreslerEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdresBasligi = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Sehir = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Ilce = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    AcikAdres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaturaAdres = table.Column<bool>(type: "bit", nullable: false),
                    TeslimatAdres = table.Column<bool>(type: "bit", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdressGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KullaniciId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adresler_Kullancilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullancilar",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Adresler_KullaniciId",
                table: "Adresler",
                column: "KullaniciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adresler");

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 4, 30, 23, 7, 58, 613, DateTimeKind.Local).AddTicks(1648), new DateTime(2025, 4, 30, 23, 7, 58, 611, DateTimeKind.Local).AddTicks(6080) });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 4, 30, 23, 7, 58, 613, DateTimeKind.Local).AddTicks(2950), new DateTime(2025, 4, 30, 23, 7, 58, 613, DateTimeKind.Local).AddTicks(2948) });

            migrationBuilder.UpdateData(
                table: "Kullancilar",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi", "KullaniciGuid" },
                values: new object[] { new DateTime(2025, 4, 30, 23, 7, 58, 614, DateTimeKind.Local).AddTicks(7040), new DateTime(2025, 4, 30, 23, 7, 58, 614, DateTimeKind.Local).AddTicks(7037), new Guid("b588e3d9-f3e0-459d-aa24-941f0ad50b74") });
        }
    }
}
