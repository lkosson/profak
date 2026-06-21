using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    /// <inheritdoc />
    public partial class FakturaRozliczeniowa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FakturaRozliczeniowaId",
                table: "Faktura",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_FakturaRozliczeniowaId",
                table: "Faktura",
                column: "FakturaRozliczeniowaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faktura_Faktura_FakturaRozliczeniowaId",
                table: "Faktura",
                column: "FakturaRozliczeniowaId",
                principalTable: "Faktura",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faktura_Faktura_FakturaRozliczeniowaId",
                table: "Faktura");

            migrationBuilder.DropIndex(
                name: "IX_Faktura_FakturaRozliczeniowaId",
                table: "Faktura");

            migrationBuilder.DropColumn(
                name: "FakturaRozliczeniowaId",
                table: "Faktura");
        }
    }
}
