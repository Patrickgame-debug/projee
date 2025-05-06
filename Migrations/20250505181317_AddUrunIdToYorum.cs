using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret.Migrations
{
    /// <inheritdoc />
    public partial class AddUrunIdToYorum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Tarih",
                table: "Yorumlar",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "SiparisDetayId",
                table: "Yorumlar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Puan",
                table: "Yorumlar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "KullaniciId",
                table: "Yorumlar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UrunId",
                table: "Yorumlar",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Yorumlar_UrunId",
                table: "Yorumlar",
                column: "UrunId");

            migrationBuilder.AddForeignKey(
                name: "FK_Yorumlar_Urunler_UrunId",
                table: "Yorumlar",
                column: "UrunId",
                principalTable: "Urunler",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Yorumlar_Urunler_UrunId",
                table: "Yorumlar");

            migrationBuilder.DropIndex(
                name: "IX_Yorumlar_UrunId",
                table: "Yorumlar");

            migrationBuilder.DropColumn(
                name: "UrunId",
                table: "Yorumlar");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Tarih",
                table: "Yorumlar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SiparisDetayId",
                table: "Yorumlar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Puan",
                table: "Yorumlar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KullaniciId",
                table: "Yorumlar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 5, 19, 7, 0, 630, DateTimeKind.Local).AddTicks(1409), new DateTime(2025, 5, 5, 19, 7, 0, 628, DateTimeKind.Local).AddTicks(6361) });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 5, 5, 19, 7, 0, 630, DateTimeKind.Local).AddTicks(2656), new DateTime(2025, 5, 5, 19, 7, 0, 630, DateTimeKind.Local).AddTicks(2654) });

            migrationBuilder.UpdateData(
                table: "Kullancilar",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi", "KullaniciGuid" },
                values: new object[] { new DateTime(2025, 5, 5, 19, 7, 0, 631, DateTimeKind.Local).AddTicks(6511), new DateTime(2025, 5, 5, 19, 7, 0, 631, DateTimeKind.Local).AddTicks(6499), new Guid("4c23945d-1f39-4575-818c-056bd309717a") });
        }
    }
}
