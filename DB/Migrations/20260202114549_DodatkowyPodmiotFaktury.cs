using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    /// <inheritdoc />
    public partial class DodatkowyPodmiotFaktury : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DodatkowyPodmiot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FakturaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Rodzaj = table.Column<int>(type: "INTEGER", nullable: false),
                    IDwew = table.Column<string>(type: "TEXT", nullable: false, defaultValue: ""),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false, defaultValue: ""),
                    NIP = table.Column<string>(type: "TEXT", nullable: false, defaultValue: ""),
                    VatUE = table.Column<string>(type: "TEXT", nullable: false, defaultValue: ""),
                    Adres = table.Column<string>(type: "TEXT", nullable: false, defaultValue: ""),
                    EMail = table.Column<string>(type: "TEXT", nullable: false, defaultValue: ""),
                    Telefon = table.Column<string>(type: "TEXT", nullable: false, defaultValue: ""),
                    Udzial = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DodatkowyPodmiot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DodatkowyPodmiot_Faktura_FakturaId",
                        column: x => x.FakturaId,
                        principalTable: "Faktura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DodatkowyPodmiot_FakturaId",
                table: "DodatkowyPodmiot",
                column: "FakturaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DodatkowyPodmiot");
        }
    }
}
