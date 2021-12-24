using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class WDTWNT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CzyWDT",
                table: "Faktura",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CzyWNT",
                table: "Faktura",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzyWDT",
                table: "Faktura");

            migrationBuilder.DropColumn(
                name: "CzyWNT",
                table: "Faktura");
        }
    }
}
