using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Puskemas.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReservasiJadwalRelationship_AddJamSelesaiNomorAntrian : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservasis_IdJadwal",
                table: "Reservasis");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "JamSelesai",
                table: "Reservasis",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "NomorAntrian",
                table: "Reservasis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservasis_IdJadwal",
                table: "Reservasis",
                column: "IdJadwal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservasis_IdJadwal",
                table: "Reservasis");

            migrationBuilder.DropColumn(
                name: "JamSelesai",
                table: "Reservasis");

            migrationBuilder.DropColumn(
                name: "NomorAntrian",
                table: "Reservasis");

            migrationBuilder.CreateIndex(
                name: "IX_Reservasis_IdJadwal",
                table: "Reservasis",
                column: "IdJadwal",
                unique: true);
        }
    }
}
