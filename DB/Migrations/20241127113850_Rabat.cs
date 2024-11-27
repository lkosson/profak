using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    public partial class Rabat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RabatCena",
                table: "PozycjaFaktury",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RabatProcent",
                table: "PozycjaFaktury",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RabatWartosc",
                table: "PozycjaFaktury",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RabatCena",
                table: "PozycjaFaktury");

            migrationBuilder.DropColumn(
                name: "RabatProcent",
                table: "PozycjaFaktury");

            migrationBuilder.DropColumn(
                name: "RabatWartosc",
                table: "PozycjaFaktury");
        }
    }
}
