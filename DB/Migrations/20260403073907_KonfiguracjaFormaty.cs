using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    /// <inheritdoc />
    public partial class KonfiguracjaFormaty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormatCzasu",
                table: "Konfiguracja",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormatDaty",
                table: "Konfiguracja",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormatKwoty",
                table: "Konfiguracja",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormatCzasu",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "FormatDaty",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "FormatKwoty",
                table: "Konfiguracja");
        }
    }
}
