using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class PozycjaLP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CzyPrzedKorekta",
                table: "PozycjaFaktury",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LP",
                table: "PozycjaFaktury",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzyPrzedKorekta",
                table: "PozycjaFaktury");

            migrationBuilder.DropColumn(
                name: "LP",
                table: "PozycjaFaktury");
        }
    }
}
