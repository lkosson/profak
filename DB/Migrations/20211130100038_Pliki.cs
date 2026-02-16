using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class Pliki : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FakturaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false, defaultValue: ""),
                    Rozmiar = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    ZawartoscId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plik_Faktura_FakturaId",
                        column: x => x.FakturaId,
                        principalTable: "Faktura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zawartosc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Dane = table.Column<byte[]>(type: "BLOB", nullable: false),
                    PlikId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zawartosc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zawartosc_Plik_PlikId",
                        column: x => x.PlikId,
                        principalTable: "Plik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plik_FakturaId",
                table: "Plik",
                column: "FakturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Zawartosc_PlikId",
                table: "Zawartosc",
                column: "PlikId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zawartosc");

            migrationBuilder.DropTable(
                name: "Plik");
        }
    }
}
