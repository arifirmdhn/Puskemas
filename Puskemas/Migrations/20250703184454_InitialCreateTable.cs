using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Puskemas.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dokters",
                columns: table => new
                {
                    IdDokter = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JenisKelamin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alamat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Spesialis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokters", x => x.IdDokter);
                });

            migrationBuilder.CreateTable(
                name: "Pasiens",
                columns: table => new
                {
                    IdPasien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alamat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TanggalLahir = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NomorHp = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasiens", x => x.IdPasien);
                });

            migrationBuilder.CreateTable(
                name: "Jadwals",
                columns: table => new
                {
                    IdJadwal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDokter = table.Column<int>(type: "int", nullable: false),
                    Hari = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JamMulai = table.Column<TimeSpan>(type: "time", nullable: false),
                    JamSelesai = table.Column<TimeSpan>(type: "time", nullable: false),
                    Tanggal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusJadwal = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jadwals", x => x.IdJadwal);
                    table.ForeignKey(
                        name: "FK_Jadwals_Dokters_IdDokter",
                        column: x => x.IdDokter,
                        principalTable: "Dokters",
                        principalColumn: "IdDokter",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RekamMedis",
                columns: table => new
                {
                    IdRekam = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPasien = table.Column<int>(type: "int", nullable: false),
                    TanggalCheckup = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diagnosa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pengobatan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Obat = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RekamMedis", x => x.IdRekam);
                    table.ForeignKey(
                        name: "FK_RekamMedis_Pasiens_IdPasien",
                        column: x => x.IdPasien,
                        principalTable: "Pasiens",
                        principalColumn: "IdPasien",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservasis",
                columns: table => new
                {
                    IdReservasi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPasien = table.Column<int>(type: "int", nullable: false),
                    IdJadwal = table.Column<int>(type: "int", nullable: false),
                    StatusReservasi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservasis", x => x.IdReservasi);
                    table.ForeignKey(
                        name: "FK_Reservasis_Jadwals_IdJadwal",
                        column: x => x.IdJadwal,
                        principalTable: "Jadwals",
                        principalColumn: "IdJadwal",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservasis_Pasiens_IdPasien",
                        column: x => x.IdPasien,
                        principalTable: "Pasiens",
                        principalColumn: "IdPasien",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jadwals_IdDokter",
                table: "Jadwals",
                column: "IdDokter");

            migrationBuilder.CreateIndex(
                name: "IX_RekamMedis_IdPasien",
                table: "RekamMedis",
                column: "IdPasien");

            migrationBuilder.CreateIndex(
                name: "IX_Reservasis_IdJadwal",
                table: "Reservasis",
                column: "IdJadwal",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservasis_IdPasien",
                table: "Reservasis",
                column: "IdPasien");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RekamMedis");

            migrationBuilder.DropTable(
                name: "Reservasis");

            migrationBuilder.DropTable(
                name: "Jadwals");

            migrationBuilder.DropTable(
                name: "Pasiens");

            migrationBuilder.DropTable(
                name: "Dokters");
        }
    }
}
