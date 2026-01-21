using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    /// <inheritdoc />
    public partial class FakturaPierwotna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FakturaPierwotnaId",
                table: "Faktura",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_FakturaPierwotnaId",
                table: "Faktura",
                column: "FakturaPierwotnaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faktura_Faktura_FakturaPierwotnaId",
                table: "Faktura",
                column: "FakturaPierwotnaId",
                principalTable: "Faktura",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faktura_Faktura_FakturaPierwotnaId",
                table: "Faktura");

            migrationBuilder.DropIndex(
                name: "IX_Faktura_FakturaPierwotnaId",
                table: "Faktura");

            migrationBuilder.DropColumn(
                name: "FakturaPierwotnaId",
                table: "Faktura");
        }
    }
}
