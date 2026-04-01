using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    /// <inheritdoc />
    public partial class KontrahentKSeF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CzyImportKSeF",
                table: "Kontrahent",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzyImportKSeF",
                table: "Kontrahent");
        }
    }
}
