using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.MigrationsSqlServer
{
    /// <inheritdoc />
    public partial class KontrahentKodKraju : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KodKraju",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "PL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KodKraju",
                table: "Kontrahent");
        }
    }
}
