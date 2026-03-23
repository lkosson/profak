using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.MigrationsSqlServer
{
    /// <inheritdoc />
    public partial class v1_17_0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Konfiguracja",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Uwagi",
                table: "Wplata",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Grupa",
                table: "Numerator",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UwagiWewnetrzne",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UwagiPubliczne",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "TokenKSeF",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RachunekBankowy",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "OsobaFizycznaNazwisko",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "OsobaFizycznaImie",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "NazwaBanku",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "KodUrzedu",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "EMail",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AdresRejestrowy",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AdresKorespondencyjny",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DomyslnaWalutaId",
                table: "Kontrahent",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SMTPSerwer",
                table: "Konfiguracja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SMTPLogin",
                table: "Konfiguracja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SMTPHaslo",
                table: "Konfiguracja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EMailTresc",
                table: "Konfiguracja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EMailTemat",
                table: "Konfiguracja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EMailNadawca",
                table: "Konfiguracja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DomyslnyPodgladStrony",
                table: "Konfiguracja",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IkonyAkcji",
                table: "Konfiguracja",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NazwaCzcionki",
                table: "Konfiguracja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "PotwierdzanieZamknieciaEdytora",
                table: "Konfiguracja",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PotwierdzanieZamknieciaProgramu",
                table: "Konfiguracja",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RozmiarCzcionki",
                table: "Konfiguracja",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "SkrotyKlawiaturoweAkcji",
                table: "Konfiguracja",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SkrotyKlawiaturowePrzyciskow",
                table: "Konfiguracja",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SkrotyKlawiaturoweZakladek",
                table: "Konfiguracja",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SzerokoscMenu",
                table: "Konfiguracja",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wersja",
                table: "Konfiguracja",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "WstepneLadowanieReportingServices",
                table: "Konfiguracja",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Kontrahent_DomyslnaWalutaId",
                table: "Kontrahent",
                column: "DomyslnaWalutaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kontrahent_Waluta_DomyslnaWalutaId",
                table: "Kontrahent",
                column: "DomyslnaWalutaId",
                principalTable: "Waluta",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kontrahent_Waluta_DomyslnaWalutaId",
                table: "Kontrahent");

            migrationBuilder.DropIndex(
                name: "IX_Kontrahent_DomyslnaWalutaId",
                table: "Kontrahent");

            migrationBuilder.DropColumn(
                name: "Grupa",
                table: "Numerator");

            migrationBuilder.DropColumn(
                name: "DomyslnaWalutaId",
                table: "Kontrahent");

            migrationBuilder.DropColumn(
                name: "DomyslnyPodgladStrony",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "IkonyAkcji",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "NazwaCzcionki",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "PotwierdzanieZamknieciaEdytora",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "PotwierdzanieZamknieciaProgramu",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "RozmiarCzcionki",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "SkrotyKlawiaturoweAkcji",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "SkrotyKlawiaturowePrzyciskow",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "SkrotyKlawiaturoweZakladek",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "SzerokoscMenu",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "Wersja",
                table: "Konfiguracja");

            migrationBuilder.DropColumn(
                name: "WstepneLadowanieReportingServices",
                table: "Konfiguracja");

            migrationBuilder.AlterColumn<string>(
                name: "Uwagi",
                table: "Wplata",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UwagiWewnetrzne",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UwagiPubliczne",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "TokenKSeF",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RachunekBankowy",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "OsobaFizycznaNazwisko",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "OsobaFizycznaImie",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "NazwaBanku",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "KodUrzedu",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "EMail",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AdresRejestrowy",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AdresKorespondencyjny",
                table: "Kontrahent",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SMTPSerwer",
                table: "Konfiguracja",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SMTPLogin",
                table: "Konfiguracja",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SMTPHaslo",
                table: "Konfiguracja",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "EMailTresc",
                table: "Konfiguracja",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "EMailTemat",
                table: "Konfiguracja",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "EMailNadawca",
                table: "Konfiguracja",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.InsertData(
                table: "Konfiguracja",
                columns: new[] { "Id", "EMailNadawca", "EMailTemat", "EMailTresc", "SMTPHaslo", "SMTPLogin", "SMTPPort", "SMTPSerwer" },
                values: new object[] { 1, "[SPRZEDAWCA-NAZWA] <[SPRZEDAWCA-EMAIL]>", "Faktura - [NUMER]", "Dzień dobry,\r\n\r\nw załączniku znajduje się faktura numer [NUMER] z dnia [DATA-SPRZEDAZY] na kwotę [KWOTA-BRUTTO] [WALUTA].\r\n\r\nWiadomość wygenerowana automatycznie.\r\n\r\n-- \r\n[SPRZEDAWCA-NAZWA]\r\n[SPRZEDAWCA-ADRES]", "tajnehaslo", "biuro", 465, "smtp.example.com" });
        }
    }
}
