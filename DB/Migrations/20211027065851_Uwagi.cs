using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class Uwagi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uwagi",
                table: "Faktura");

            migrationBuilder.AddColumn<string>(
                name: "UwagiPubliczne",
                table: "Faktura",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UwagiWewnetrzne",
                table: "Faktura",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UwagiPubliczne",
                table: "Faktura");

            migrationBuilder.DropColumn(
                name: "UwagiWewnetrzne",
                table: "Faktura");

            migrationBuilder.AddColumn<string>(
                name: "Uwagi",
                table: "Faktura",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
