using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    public partial class StanMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StanMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Pozycja = table.Column<string>(type: "TEXT", nullable: false),
                    CzyZwinieta = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    CzyUkryta = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    CzyAktywna = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanMenu", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StanMenu");
        }
    }
}
