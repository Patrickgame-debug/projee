using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret.Migrations
{
    /// <inheritdoc />
    public partial class FixKullaniciIdInSiparis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "KullaniciId",
                table: "Siparisler",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_KullaniciId",
                table: "Siparisler",
                column: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Siparisler_Kullancilar_KullaniciId",
                table: "Siparisler",
                column: "KullaniciId",
                principalTable: "Kullancilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Siparisler_Kullancilar_KullaniciId",
                table: "Siparisler");

            migrationBuilder.DropIndex(
                name: "IX_Siparisler_KullaniciId",
                table: "Siparisler");

            migrationBuilder.AlterColumn<string>(
                name: "KullaniciId",
                table: "Siparisler",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
