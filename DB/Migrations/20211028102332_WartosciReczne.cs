using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class WartosciReczne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CzyWartosciReczne",
                table: "PozycjaFaktury",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CzyWartosciReczne",
                table: "Faktura",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzyWartosciReczne",
                table: "PozycjaFaktury");

            migrationBuilder.DropColumn(
                name: "CzyWartosciReczne",
                table: "Faktura");
        }
    }
}
