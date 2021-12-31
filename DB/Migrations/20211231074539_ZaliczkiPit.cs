using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class ZaliczkiPit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZaliczkaPit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Miesiac = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Przychody = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    Koszty = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    Przeniesiony = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    DoPrzeniesienia = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    DoWplaty = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZaliczkaPit", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZaliczkaPit");
        }
    }
}
