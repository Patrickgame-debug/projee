using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret.Migrations
{
    /// <inheritdoc />
    public partial class KullaniciEngellemeEkle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HesapEngelliMi",
                table: "Kullancilar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 7, 15, 45, 29, 298, DateTimeKind.Local).AddTicks(9473), new DateTime(2025, 5, 7, 15, 45, 29, 297, DateTimeKind.Local).AddTicks(4896) });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 7, 15, 45, 29, 299, DateTimeKind.Local).AddTicks(665), new DateTime(2025, 5, 7, 15, 45, 29, 299, DateTimeKind.Local).AddTicks(663) });

            migrationBuilder.UpdateData(
                table: "Kullancilar",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "HesapEngelliMi", "KayitTarihi", "KullaniciGuid" },
                values: new object[] { new DateTime(2025, 5, 7, 15, 45, 29, 300, DateTimeKind.Local).AddTicks(4427), false, new DateTime(2025, 5, 7, 15, 45, 29, 300, DateTimeKind.Local).AddTicks(4424), new Guid("edcaa4ef-e634-42eb-a991-aca109df762c") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HesapEngelliMi",
                table: "Kullancilar");

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 7, 1, 23, 3, 491, DateTimeKind.Local).AddTicks(6915), new DateTime(2025, 5, 7, 1, 23, 3, 489, DateTimeKind.Local).AddTicks(7528) });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 7, 1, 23, 3, 491, DateTimeKind.Local).AddTicks(8588), new DateTime(2025, 5, 7, 1, 23, 3, 491, DateTimeKind.Local).AddTicks(8586) });

            migrationBuilder.UpdateData(
                table: "Kullancilar",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi", "KullaniciGuid" },
                values: new object[] { new DateTime(2025, 5, 7, 1, 23, 3, 493, DateTimeKind.Local).AddTicks(6045), new DateTime(2025, 5, 7, 1, 23, 3, 493, DateTimeKind.Local).AddTicks(6042), new Guid("0e3a9716-25f3-4e27-8008-afd1d47c6480") });
        }
    }
}
