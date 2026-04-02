using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.MigrationsSqlServer
{
    /// <inheritdoc />
    public partial class KontrahentKraj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kraj",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kraj",
                table: "Kontrahent");
        }
    }
}
