using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    /// <inheritdoc />
    public partial class SposobLiczeniaCenyTowaruNazwa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CzyWedlugCenBrutto",
                table: "Towar",
                newName: "SposobLiczeniaCeny");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SposobLiczeniaCeny",
                table: "Towar",
                newName: "CzyWedlugCenBrutto");
        }
    }
}
