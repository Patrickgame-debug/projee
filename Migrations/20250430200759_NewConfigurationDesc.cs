using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret.Migrations
{
    /// <inheritdoc />
    public partial class NewConfigurationDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "Kampanyalar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "Kampanyalar",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 4, 23, 13, 56, 0, 497, DateTimeKind.Local).AddTicks(2060), new DateTime(2025, 4, 23, 13, 56, 0, 495, DateTimeKind.Local).AddTicks(5924) });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi" },
                values: new object[] { new DateTime(2025, 4, 23, 13, 56, 0, 497, DateTimeKind.Local).AddTicks(3238), new DateTime(2025, 4, 23, 13, 56, 0, 497, DateTimeKind.Local).AddTicks(3236) });

            migrationBuilder.UpdateData(
                table: "Kullancilar",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "GuncellemeTarihi", "KayitTarihi", "KullaniciGuid" },
                values: new object[] { new DateTime(2025, 4, 23, 13, 56, 0, 498, DateTimeKind.Local).AddTicks(7198), new DateTime(2025, 4, 23, 13, 56, 0, 498, DateTimeKind.Local).AddTicks(7195), new Guid("443aefcb-c5ec-40f3-9f2f-a338e37905b6") });
        }
    }
}
