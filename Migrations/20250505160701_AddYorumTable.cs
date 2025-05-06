using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret.Migrations
{
    /// <inheritdoc />
    public partial class AddYorumTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Yorumlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisDetayId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    Puan = table.Column<int>(type: "int", nullable: false),
                    YorumMetni = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yorumlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Yorumlar_Kullancilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullancilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Yorumlar_SiparisDetaylar_SiparisDetayId",
                        column: x => x.SiparisDetayId,
                        principalTable: "SiparisDetaylar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Yorumlar_KullaniciId",
                table: "Yorumlar",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Yorumlar_SiparisDetayId",
                table: "Yorumlar",
                column: "SiparisDetayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Yorumlar");

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
    }
}
