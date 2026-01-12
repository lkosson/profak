using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    /// <inheritdoc />
    public partial class SposobPlatnosciKontrahenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SposobPlatnosciId",
                table: "Kontrahent",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kontrahent_SposobPlatnosciId",
                table: "Kontrahent",
                column: "SposobPlatnosciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kontrahent_SposobPlatnosci_SposobPlatnosciId",
                table: "Kontrahent",
                column: "SposobPlatnosciId",
                principalTable: "SposobPlatnosci",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kontrahent_SposobPlatnosci_SposobPlatnosciId",
                table: "Kontrahent");

            migrationBuilder.DropIndex(
                name: "IX_Kontrahent_SposobPlatnosciId",
                table: "Kontrahent");

            migrationBuilder.DropColumn(
                name: "SposobPlatnosciId",
                table: "Kontrahent");
        }
    }
}
