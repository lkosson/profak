using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class DomyslnaJednoskaMiary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CzyGlowna",
                table: "JednostkaMiary",
                newName: "CzyDomyslna");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CzyDomyslna",
                table: "JednostkaMiary",
                newName: "CzyGlowna");
        }
    }
}
