using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class DanePodatkowe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KodUrzedu",
                table: "Kontrahent",
                type: "TEXT",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "OsobaFizycznaDataUrodzenia",
                table: "Kontrahent",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OsobaFizycznaImie",
                table: "Kontrahent",
                type: "TEXT",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OsobaFizycznaNazwisko",
                table: "Kontrahent",
                type: "TEXT",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KodUrzedu",
                table: "Kontrahent");

            migrationBuilder.DropColumn(
                name: "OsobaFizycznaDataUrodzenia",
                table: "Kontrahent");

            migrationBuilder.DropColumn(
                name: "OsobaFizycznaImie",
                table: "Kontrahent");

            migrationBuilder.DropColumn(
                name: "OsobaFizycznaNazwisko",
                table: "Kontrahent");
        }
    }
}
