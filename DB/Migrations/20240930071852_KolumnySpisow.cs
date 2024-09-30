using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    public partial class KolumnySpisow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KolumnaSpisu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Spis = table.Column<string>(type: "TEXT", nullable: false),
                    Kolumna = table.Column<string>(type: "TEXT", nullable: false),
                    Kolejnosc = table.Column<int>(type: "INTEGER", nullable: false),
                    Szerokosc = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KolumnaSpisu", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KolumnaSpisu");
        }
    }
}
