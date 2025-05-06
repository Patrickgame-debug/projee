using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret.Migrations
{
    /// <inheritdoc />
    public partial class FixSiparisKullaniciRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KullaniciId1",
                table: "Siparisler",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 5, 2, 6, 9, 748, DateTimeKind.Local).AddTicks(4572), new DateTime(2025, 5, 5, 2, 6, 9, 746, DateTimeKind.Local).AddTicks(4104) });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 5, 2, 6, 9, 748, DateTimeKind.Local).AddTicks(6934), new DateTime(2025, 5, 5, 2, 6, 9, 748, DateTimeKind.Local).AddTicks(6931) });

            migrationBuilder.UpdateData(
                table: "Kullancilar",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi", "KullaniciGuid" },
                values: new object[] { new DateTime(2025, 5, 5, 2, 6, 9, 750, DateTimeKind.Local).AddTicks(5833), new DateTime(2025, 5, 5, 2, 6, 9, 750, DateTimeKind.Local).AddTicks(5829), new Guid("21e95928-3720-4f9e-a46f-bbd68e7c3a76") });

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_KullaniciId1",
                table: "Siparisler",
                column: "KullaniciId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Siparisler_Kullancilar_KullaniciId1",
                table: "Siparisler",
                column: "KullaniciId1",
                principalTable: "Kullancilar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Siparisler_Kullancilar_KullaniciId1",
                table: "Siparisler");

            migrationBuilder.DropIndex(
                name: "IX_Siparisler_KullaniciId1",
                table: "Siparisler");

            migrationBuilder.DropColumn(
                name: "KullaniciId1",
                table: "Siparisler");

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
        }
    }
}
