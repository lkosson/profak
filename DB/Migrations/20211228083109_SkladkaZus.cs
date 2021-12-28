using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class SkladkaZus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkladkaZus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Miesiac = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PodstawaSpoleczne = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    PodstawaZdrowotne = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    SkladkaEmerytalna = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    SkladkaRentowa = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    SkladkaWypadkowa = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    SkladkaSpoleczna = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    SkladkaZdrowotna = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    SkladkaFunduszPracy = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    SumaSkladek = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    OdliczenieOdDochodu = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkladkaZus", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkladkaZus");
        }
    }
}
