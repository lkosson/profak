using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class StawkaRyczaltu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "StawkaRyczaltu",
                table: "Towar",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StawkaRyczaltu",
                table: "PozycjaFaktury",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StawkaRyczaltu",
                table: "Towar");

            migrationBuilder.DropColumn(
                name: "StawkaRyczaltu",
                table: "PozycjaFaktury");
        }
    }
}
