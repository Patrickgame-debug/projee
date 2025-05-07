using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret.Migrations
{
    /// <inheritdoc />
    public partial class AddSiparisDetayTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 7, 17, 22, 15, 814, DateTimeKind.Local).AddTicks(1577), new DateTime(2025, 5, 7, 17, 22, 15, 812, DateTimeKind.Local).AddTicks(6119) });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 7, 17, 22, 15, 814, DateTimeKind.Local).AddTicks(2816), new DateTime(2025, 5, 7, 17, 22, 15, 814, DateTimeKind.Local).AddTicks(2813) });

            migrationBuilder.UpdateData(
                table: "Kullancilar",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi", "KullaniciGuid" },
                values: new object[] { new DateTime(2025, 5, 7, 17, 22, 15, 815, DateTimeKind.Local).AddTicks(7769), new DateTime(2025, 5, 7, 17, 22, 15, 815, DateTimeKind.Local).AddTicks(7766), new Guid("e0a358bd-b270-419c-a884-656e55247740") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "GuncellemeTarihi", "KayitTarihi", "KullaniciGuid" },
                values: new object[] { new DateTime(2025, 5, 7, 15, 45, 29, 300, DateTimeKind.Local).AddTicks(4427), new DateTime(2025, 5, 7, 15, 45, 29, 300, DateTimeKind.Local).AddTicks(4424), new Guid("edcaa4ef-e634-42eb-a991-aca109df762c") });
        }
    }
}
