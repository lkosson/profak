using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class Numerator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Numerator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Przeznaczenie = table.Column<int>(type: "INTEGER", nullable: false),
                    Format = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Numerator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StanNumeratora",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeratorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Parametry = table.Column<string>(type: "TEXT", nullable: false, defaultValue: ""),
                    OstatniaWartosc = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanNumeratora", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StanNumeratora_Numerator_NumeratorId",
                        column: x => x.NumeratorId,
                        principalTable: "Numerator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StanNumeratora_NumeratorId",
                table: "StanNumeratora",
                column: "NumeratorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StanNumeratora");

            migrationBuilder.DropTable(
                name: "Numerator");
        }
    }
}
