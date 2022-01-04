using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class OpisZdarzenia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OpisZdarzenia",
                table: "Faktura",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpisZdarzenia",
                table: "Faktura");
        }
    }
}
