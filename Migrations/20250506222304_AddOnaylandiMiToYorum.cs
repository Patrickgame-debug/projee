using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret.Migrations
{
    /// <inheritdoc />
    public partial class AddOnaylandiMiToYorum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OnaylandiMi",
                table: "Yorumlar",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnaylandiMi",
                table: "Yorumlar");

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
        }
    }
}
