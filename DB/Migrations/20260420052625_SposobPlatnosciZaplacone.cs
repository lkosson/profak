using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    /// <inheritdoc />
    public partial class SposobPlatnosciZaplacone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CzyZaplacone",
                table: "SposobPlatnosci",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzyZaplacone",
                table: "SposobPlatnosci");
        }
    }
}
