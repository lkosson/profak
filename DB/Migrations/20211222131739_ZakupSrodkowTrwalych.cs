using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class ZakupSrodkowTrwalych : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CzyZakupSrodkowTrwalych",
                table: "Faktura",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzyZakupSrodkowTrwalych",
                table: "Faktura");
        }
    }
}
