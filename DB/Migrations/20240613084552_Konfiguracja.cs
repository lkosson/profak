using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.Migrations
{
    public partial class Konfiguracja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Konfiguracja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SMTPSerwer = table.Column<string>(type: "TEXT", nullable: true),
                    SMTPLogin = table.Column<string>(type: "TEXT", nullable: true),
                    SMTPHaslo = table.Column<string>(type: "TEXT", nullable: true),
                    SMTPPort = table.Column<int>(type: "INTEGER", nullable: false),
                    EMailNadawca = table.Column<string>(type: "TEXT", nullable: true),
                    EMailTemat = table.Column<string>(type: "TEXT", nullable: true),
                    EMailTresc = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konfiguracja", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Konfiguracja",
                columns: new[] { "Id", "EMailNadawca", "EMailTemat", "EMailTresc", "SMTPHaslo", "SMTPLogin", "SMTPPort", "SMTPSerwer" },
                values: new object[] { 1, "[SPRZEDAWCA-NAZWA] <[SPRZEDAWCA-EMAIL]>", "Faktura - [NUMER]", "Dzień dobry,\r\n\r\nw załączniku znajduje się faktura numer [NUMER] z dnia [DATA] na kwotę [KWOTA-BRUTTO].\r\n\r\nWiadomość wygenerowana automatycznie.\r\n\r\n-- \r\n[SPRZEDAWCA-NAZWA]\r\n[SPRZEDAWCA-ADRES]", "tajnehaslo", "biuro", 465, "smtp.example.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Konfiguracja");
        }
    }
}
