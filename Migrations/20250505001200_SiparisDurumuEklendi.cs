using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret.Migrations
{
    /// <inheritdoc />
    public partial class SiparisDurumuEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SiparisDurumu",
                table: "Siparisler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 5, 3, 11, 59, 620, DateTimeKind.Local).AddTicks(8123), new DateTime(2025, 5, 5, 3, 11, 59, 619, DateTimeKind.Local).AddTicks(2423) });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 5, 3, 11, 59, 620, DateTimeKind.Local).AddTicks(9430), new DateTime(2025, 5, 5, 3, 11, 59, 620, DateTimeKind.Local).AddTicks(9428) });

            migrationBuilder.UpdateData(
                table: "Kullancilar",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi", "KullaniciGuid" },
                values: new object[] { new DateTime(2025, 5, 5, 3, 11, 59, 622, DateTimeKind.Local).AddTicks(3400), new DateTime(2025, 5, 5, 3, 11, 59, 622, DateTimeKind.Local).AddTicks(3397), new Guid("6a958104-2a4f-4734-bc4a-aa8545aa5cec") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiparisDurumu",
                table: "Siparisler");

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 5, 2, 41, 2, 579, DateTimeKind.Local).AddTicks(7748), new DateTime(2025, 5, 5, 2, 41, 2, 577, DateTimeKind.Local).AddTicks(6233) });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 5, 2, 41, 2, 579, DateTimeKind.Local).AddTicks(9120), new DateTime(2025, 5, 5, 2, 41, 2, 579, DateTimeKind.Local).AddTicks(9118) });

            migrationBuilder.UpdateData(
                table: "Kullancilar",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi", "KullaniciGuid" },
                values: new object[] { new DateTime(2025, 5, 5, 2, 41, 2, 581, DateTimeKind.Local).AddTicks(5338), new DateTime(2025, 5, 5, 2, 41, 2, 581, DateTimeKind.Local).AddTicks(5332), new Guid("05b64fac-84ee-4be9-aa6d-077082831057") });
        }
    }
}
