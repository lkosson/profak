using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    /// <inheritdoc />
    public partial class KontrahentNDG : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CzyNDG",
                table: "Kontrahent",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzyNDG",
                table: "Kontrahent");
        }
    }
}
