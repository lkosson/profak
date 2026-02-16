using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    /// <inheritdoc />
    public partial class KonfiguracjaWyglad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DomyslnyPodgladStrony",
                table: "Konfiguracja",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IkonyAkcji",
                table: "Konfiguracja",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NazwaCzcionki",
                table: "Konfiguracja",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "PotwierdzanieZamknieciaEdytora",
                table: "Konfiguracja",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PotwierdzanieZamknieciaProgramu",
                table: "Konfiguracja",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RozmiarCzcionki",
                table: "Konfiguracja",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "SkrotyKlawiaturoweAkcji",
                table: "Konfiguracja",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SkrotyKlawiaturowePrzyciskow",
                table: "Konfiguracja",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SkrotyKlawiaturoweZakladek",
                table: "Konfiguracja",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SzerokoscMenu",
                table: "Konfiguracja",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wersja",
                table: "Konfiguracja",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "WstepneLadowanieReportingServices",
                table: "Konfiguracja",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DomyslnyPodgladStrony",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "IkonyAkcji",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "NazwaCzcionki",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "PotwierdzanieZamknieciaEdytora",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "PotwierdzanieZamknieciaProgramu",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "RozmiarCzcionki",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "SkrotyKlawiaturoweAkcji",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "SkrotyKlawiaturowePrzyciskow",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "SkrotyKlawiaturoweZakladek",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "SzerokoscMenu",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "Wersja",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "WstepneLadowanieReportingServices",
                table: "Konfiguracja");
        }
    }
}
