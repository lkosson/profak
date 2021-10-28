using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class Cena : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KwotaVat",
                table: "PozycjaFaktury",
                newName: "CenaVat");

            migrationBuilder.RenameColumn(
                name: "KwotaNetto",
                table: "PozycjaFaktury",
                newName: "CenaNetto");

            migrationBuilder.RenameColumn(
                name: "KwotaBrutto",
                table: "PozycjaFaktury",
                newName: "CenaBrutto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CenaVat",
                table: "PozycjaFaktury",
                newName: "KwotaVat");

            migrationBuilder.RenameColumn(
                name: "CenaNetto",
                table: "PozycjaFaktury",
                newName: "KwotaNetto");

            migrationBuilder.RenameColumn(
                name: "CenaBrutto",
                table: "PozycjaFaktury",
                newName: "KwotaBrutto");
        }
    }
}
