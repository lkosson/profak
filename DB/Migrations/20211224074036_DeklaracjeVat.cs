using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class DeklaracjeVat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeklaracjaVat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Miesiac = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NettoZW = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    Netto0 = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    Netto5 = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    Netto8 = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    Netto23 = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    NettoWDT = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    NettoWNT = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    Nalezny5 = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    Nalezny8 = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    Nalezny23 = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    NaleznyWNT = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    NettoSrodkiTrwale = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    NettoPozostale = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    NaliczonyPrzeniesiony = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    NaliczonySrodkiTrwale = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    NaliczonyPozostale = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeklaracjaVat", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeklaracjaVat");
        }
    }
}
