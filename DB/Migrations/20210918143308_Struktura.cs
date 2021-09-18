using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProFak.DB.Migrations
{
    public partial class Struktura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JednostkaMiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Skrot = table.Column<string>(type: "TEXT", nullable: false),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    CzyGlowna = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    LiczbaMiescPoPrzecinku = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JednostkaMiary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kontrahent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    PelnaNazwa = table.Column<string>(type: "TEXT", nullable: false),
                    NIP = table.Column<string>(type: "TEXT", nullable: false),
                    AdresRejestrowy = table.Column<string>(type: "TEXT", nullable: true),
                    AdresKorespondencyjny = table.Column<string>(type: "TEXT", nullable: true),
                    RachunekBankowy = table.Column<string>(type: "TEXT", nullable: true),
                    Telefon = table.Column<string>(type: "TEXT", nullable: true),
                    EMail = table.Column<string>(type: "TEXT", nullable: true),
                    Uwagi = table.Column<string>(type: "TEXT", nullable: true),
                    CzyArchiwalny = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    CzyPodmiot = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontrahent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SposobPlatnosci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    LiczbaDni = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    CzyDomyslny = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SposobPlatnosci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StawkaVat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Skrot = table.Column<string>(type: "TEXT", nullable: false),
                    Wartosc = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    CzyDomyslna = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StawkaVat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Waluta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Skrot = table.Column<string>(type: "TEXT", nullable: false),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    CzyDomyslna = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waluta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Towar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    Rodzaj = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    CenaNetto = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    CenaBrutto = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    CzyWedlugCenBrutto = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    CzyArchiwalny = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    StawkaVatId = table.Column<int>(type: "INTEGER", nullable: false),
                    JednostkaMiaryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Towar_JednostkaMiary_JednostkaMiaryId",
                        column: x => x.JednostkaMiaryId,
                        principalTable: "JednostkaMiary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Towar_StawkaVat_StawkaVatId",
                        column: x => x.StawkaVatId,
                        principalTable: "StawkaVat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faktura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Numer = table.Column<string>(type: "TEXT", nullable: false),
                    DataWystawienia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataSprzedazy = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataWprowadzenia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TerminPlatnosci = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NIPSprzedawcy = table.Column<string>(type: "TEXT", nullable: false),
                    NazwaSprzedawcy = table.Column<string>(type: "TEXT", nullable: false),
                    DaneSprzedawcy = table.Column<string>(type: "TEXT", nullable: false),
                    NIPNabywcy = table.Column<string>(type: "TEXT", nullable: false),
                    NazwaNabywcy = table.Column<string>(type: "TEXT", nullable: false),
                    DaneNabywcy = table.Column<string>(type: "TEXT", nullable: false),
                    RachunekBankowy = table.Column<string>(type: "TEXT", nullable: false),
                    Uwagi = table.Column<string>(type: "TEXT", nullable: false),
                    RazemNetto = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    RazemVat = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    RazemBrutto = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    KursWaluty = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 1m),
                    OpisSposobuPlatnosci = table.Column<string>(type: "TEXT", nullable: false),
                    SprzedawcId = table.Column<int>(type: "INTEGER", nullable: false),
                    NabywcaId = table.Column<int>(type: "INTEGER", nullable: false),
                    FakturaKorygowanaId = table.Column<int>(type: "INTEGER", nullable: false),
                    FakturaKorygujacaId = table.Column<int>(type: "INTEGER", nullable: false),
                    WalutaId = table.Column<int>(type: "INTEGER", nullable: false),
                    SposobPlatnosciId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faktura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faktura_Faktura_FakturaKorygowanaId",
                        column: x => x.FakturaKorygowanaId,
                        principalTable: "Faktura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faktura_Faktura_FakturaKorygujacaId",
                        column: x => x.FakturaKorygujacaId,
                        principalTable: "Faktura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faktura_Kontrahent_NabywcaId",
                        column: x => x.NabywcaId,
                        principalTable: "Kontrahent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faktura_Kontrahent_SprzedawcId",
                        column: x => x.SprzedawcId,
                        principalTable: "Kontrahent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faktura_SposobPlatnosci_SposobPlatnosciId",
                        column: x => x.SposobPlatnosciId,
                        principalTable: "SposobPlatnosci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faktura_Waluta_WalutaId",
                        column: x => x.WalutaId,
                        principalTable: "Waluta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PozycjaFaktury",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    FakturaId = table.Column<int>(type: "INTEGER", nullable: false),
                    TowarId = table.Column<int>(type: "INTEGER", nullable: false),
                    Opis = table.Column<string>(type: "TEXT", nullable: false),
                    KwotaNetto = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    KwotaVat = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    KwotaBrutto = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    Ilosc = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    WartoscNetto = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    WartoscVat = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    WartoscBrutto = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0m),
                    CzyWedlugCenBrutto = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PozycjaFaktury", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PozycjaFaktury_Faktura_FakturaId",
                        column: x => x.FakturaId,
                        principalTable: "Faktura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PozycjaFaktury_Towar_TowarId",
                        column: x => x.TowarId,
                        principalTable: "Towar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_FakturaKorygowanaId",
                table: "Faktura",
                column: "FakturaKorygowanaId");

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_FakturaKorygujacaId",
                table: "Faktura",
                column: "FakturaKorygujacaId");

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_NabywcaId",
                table: "Faktura",
                column: "NabywcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_SposobPlatnosciId",
                table: "Faktura",
                column: "SposobPlatnosciId");

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_SprzedawcId",
                table: "Faktura",
                column: "SprzedawcId");

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_WalutaId",
                table: "Faktura",
                column: "WalutaId");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjaFaktury_FakturaId",
                table: "PozycjaFaktury",
                column: "FakturaId");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjaFaktury_TowarId",
                table: "PozycjaFaktury",
                column: "TowarId");

            migrationBuilder.CreateIndex(
                name: "IX_Towar_JednostkaMiaryId",
                table: "Towar",
                column: "JednostkaMiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Towar_StawkaVatId",
                table: "Towar",
                column: "StawkaVatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PozycjaFaktury");

            migrationBuilder.DropTable(
                name: "Faktura");

            migrationBuilder.DropTable(
                name: "Towar");

            migrationBuilder.DropTable(
                name: "Kontrahent");

            migrationBuilder.DropTable(
                name: "SposobPlatnosci");

            migrationBuilder.DropTable(
                name: "Waluta");

            migrationBuilder.DropTable(
                name: "JednostkaMiary");

            migrationBuilder.DropTable(
                name: "StawkaVat");
        }
    }
}
