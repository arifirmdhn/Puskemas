using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Puskemas.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnRekamMedisAndPasien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Anamnesis",
                table: "RekamMedis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PemeriksaanFisik",
                table: "RekamMedis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Penatalaksanaan",
                table: "RekamMedis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomorKeluarga",
                table: "Pasiens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusPernikahan",
                table: "Pasiens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TanggalTerdaftar",
                table: "Pasiens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anamnesis",
                table: "RekamMedis");

            migrationBuilder.DropColumn(
                name: "PemeriksaanFisik",
                table: "RekamMedis");

            migrationBuilder.DropColumn(
                name: "Penatalaksanaan",
                table: "RekamMedis");

            migrationBuilder.DropColumn(
                name: "NomorKeluarga",
                table: "Pasiens");

            migrationBuilder.DropColumn(
                name: "StatusPernikahan",
                table: "Pasiens");

            migrationBuilder.DropColumn(
                name: "TanggalTerdaftar",
                table: "Pasiens");
        }
    }
}
