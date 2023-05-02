using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    public partial class SkladkaZdrowotnaRozliczenieRoczne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RozliczenieRoczneSkladkiZdrowotnej",
                table: "SkladkaZus",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RozliczenieRoczneSkladkiZdrowotnej",
                table: "SkladkaZus");
        }
    }
}
