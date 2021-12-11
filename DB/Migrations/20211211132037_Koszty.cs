using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class Koszty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GTU",
                table: "Towar",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GTU",
                table: "PozycjaFaktury",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "CzyTP",
                table: "Kontrahent",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CzyTP",
                table: "Faktura",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ProcentKosztow",
                table: "Faktura",
                type: "TEXT",
                nullable: false,
                defaultValue: 100m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProcentVatNaliczonego",
                table: "Faktura",
                type: "TEXT",
                nullable: false,
                defaultValue: 100m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GTU",
                table: "Towar");

            migrationBuilder.DropColumn(
                name: "GTU",
                table: "PozycjaFaktury");

            migrationBuilder.DropColumn(
                name: "CzyTP",
                table: "Kontrahent");

            migrationBuilder.DropColumn(
                name: "CzyTP",
                table: "Faktura");

            migrationBuilder.DropColumn(
                name: "ProcentKosztow",
                table: "Faktura");

            migrationBuilder.DropColumn(
                name: "ProcentVatNaliczonego",
                table: "Faktura");
        }
    }
}
