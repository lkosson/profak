using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    /// <inheritdoc />
    public partial class RachunkiKontrahentow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RachunekKontrahenta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KontrahentId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumerRachunku = table.Column<string>(type: "TEXT", nullable: false, defaultValue: ""),
                    NazwaBanku = table.Column<string>(type: "TEXT", nullable: false, defaultValue: ""),
                    WalutaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RachunekKontrahenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RachunekKontrahenta_Kontrahent_KontrahentId",
                        column: x => x.KontrahentId,
                        principalTable: "Kontrahent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RachunekKontrahenta_Waluta_WalutaId",
                        column: x => x.WalutaId,
                        principalTable: "Waluta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RachunekKontrahenta_KontrahentId",
                table: "RachunekKontrahenta",
                column: "KontrahentId");

            migrationBuilder.CreateIndex(
                name: "IX_RachunekKontrahenta_WalutaId",
                table: "RachunekKontrahenta",
                column: "WalutaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RachunekKontrahenta");
        }
    }
}
