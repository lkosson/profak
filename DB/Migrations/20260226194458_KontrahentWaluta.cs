using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    /// <inheritdoc />
    public partial class KontrahentWaluta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DomyslnaWalutaId",
                table: "Kontrahent",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kontrahent_DomyslnaWalutaId",
                table: "Kontrahent",
                column: "DomyslnaWalutaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kontrahent_Waluta_DomyslnaWalutaId",
                table: "Kontrahent",
                column: "DomyslnaWalutaId",
                principalTable: "Waluta",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kontrahent_Waluta_DomyslnaWalutaId",
                table: "Kontrahent");

            migrationBuilder.DropIndex(
                name: "IX_Kontrahent_DomyslnaWalutaId",
                table: "Kontrahent");

            migrationBuilder.DropColumn(
                name: "DomyslnaWalutaId",
                table: "Kontrahent");
        }
    }
}
