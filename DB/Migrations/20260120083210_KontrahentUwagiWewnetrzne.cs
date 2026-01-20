using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    /// <inheritdoc />
    public partial class KontrahentUwagiWewnetrzne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uwagi",
                table: "Kontrahent",
                newName: "UwagiWewnetrzne");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UwagiWewnetrzne",
                table: "Kontrahent",
                newName: "Uwagi");
        }
    }
}
