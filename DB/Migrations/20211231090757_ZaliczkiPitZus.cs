using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class ZaliczkiPitZus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Podatek",
                table: "ZaliczkaPit",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SkladkiZus",
                table: "ZaliczkaPit",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Podatek",
                table: "ZaliczkaPit");

            migrationBuilder.DropColumn(
                name: "SkladkiZus",
                table: "ZaliczkaPit");
        }
    }
}
