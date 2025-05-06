using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret.Migrations
{
    /// <inheritdoc />
    public partial class FavorilerEkle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favoriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoriler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favoriler_Kullancilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullancilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favoriler_Urunler_UrunId",
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
                values: new object[] { new DateTime(2025, 5, 6, 4, 50, 3, 469, DateTimeKind.Local).AddTicks(3217), new DateTime(2025, 5, 6, 4, 50, 3, 467, DateTimeKind.Local).AddTicks(7276) });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 6, 4, 50, 3, 469, DateTimeKind.Local).AddTicks(4440), new DateTime(2025, 5, 6, 4, 50, 3, 469, DateTimeKind.Local).AddTicks(4437) });

            migrationBuilder.UpdateData(
                table: "Kullancilar",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi", "KullaniciGuid" },
                values: new object[] { new DateTime(2025, 5, 6, 4, 50, 3, 470, DateTimeKind.Local).AddTicks(8535), new DateTime(2025, 5, 6, 4, 50, 3, 470, DateTimeKind.Local).AddTicks(8533), new Guid("52e2f0c0-67d3-4d0c-9fde-a472706d513e") });

            migrationBuilder.CreateIndex(
                name: "IX_Favoriler_KullaniciId",
                table: "Favoriler",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Favoriler_UrunId",
                table: "Favoriler",
                column: "UrunId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favoriler");

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 5, 21, 13, 16, 770, DateTimeKind.Local).AddTicks(8392), new DateTime(2025, 5, 5, 21, 13, 16, 769, DateTimeKind.Local).AddTicks(3400) });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 5, 21, 13, 16, 770, DateTimeKind.Local).AddTicks(9707), new DateTime(2025, 5, 5, 21, 13, 16, 770, DateTimeKind.Local).AddTicks(9706) });

            migrationBuilder.UpdateData(
                table: "Kullancilar",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi", "KullaniciGuid" },
                values: new object[] { new DateTime(2025, 5, 5, 21, 13, 16, 772, DateTimeKind.Local).AddTicks(4146), new DateTime(2025, 5, 5, 21, 13, 16, 772, DateTimeKind.Local).AddTicks(4143), new Guid("c4f6db7b-9937-4189-a7f6-49c4d16d4dc1") });
        }
    }
}
