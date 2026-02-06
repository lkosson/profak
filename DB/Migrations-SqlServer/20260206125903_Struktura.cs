using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProFak.DB.MigrationsSqlServer
{
    /// <inheritdoc />
    public partial class Struktura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeklaracjaVat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Miesiac = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NettoZW = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Netto0 = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Netto5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Netto8 = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Netto23 = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    NettoWDT = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    NettoWNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Nalezny5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Nalezny8 = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Nalezny23 = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    NaleznyWNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    NettoSrodkiTrwale = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    NettoPozostale = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    NaliczonyPrzeniesiony = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    NaliczonySrodkiTrwale = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    NaliczonyPozostale = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeklaracjaVat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JednostkaMiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Skrot = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    CzyDomyslna = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LiczbaMiescPoPrzecinku = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JednostkaMiary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KolumnaSpisu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Spis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kolumna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kolejnosc = table.Column<int>(type: "int", nullable: false),
                    Szerokosc = table.Column<int>(type: "int", nullable: false),
                    PoziomSortowania = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KolumnaSpisu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Konfiguracja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SMTPSerwer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTPLogin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTPHaslo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTPPort = table.Column<int>(type: "int", nullable: false),
                    EMailNadawca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMailTemat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMailTresc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konfiguracja", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Numerator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Przeznaczenie = table.Column<int>(type: "int", nullable: false),
                    Format = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Numerator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkladkaZus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Miesiac = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PodstawaSpoleczne = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    PodstawaZdrowotne = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    SkladkaEmerytalna = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    SkladkaRentowa = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    SkladkaWypadkowa = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    SkladkaSpoleczna = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    SkladkaZdrowotna = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    RozliczenieRoczneSkladkiZdrowotnej = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    SkladkaFunduszPracy = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    SumaSkladek = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    OdliczenieOdDochodu = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkladkaZus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SposobPlatnosci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    LiczbaDni = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CzyDomyslny = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SposobPlatnosci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StanMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pozycja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CzyZwinieta = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CzyUkryta = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CzyAktywna = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StawkaVat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Skrot = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Wartosc = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CzyDomyslna = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StawkaVat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UrzadSkarbowy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrzadSkarbowy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Waluta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Skrot = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    CzyDomyslna = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waluta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZaliczkaPit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Miesiac = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Przychody = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Koszty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    SkladkiZus = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Podatek = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Przeniesiony = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    DoPrzeniesienia = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    DoWplaty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZaliczkaPit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StanNumeratora",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeratorId = table.Column<int>(type: "int", nullable: false),
                    Parametry = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    OstatniaWartosc = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanNumeratora", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StanNumeratora_Numerator_NumeratorId",
                        column: x => x.NumeratorId,
                        principalTable: "Numerator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kontrahent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    PelnaNazwa = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    NIP = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    AdresRejestrowy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    AdresKorespondencyjny = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    RachunekBankowy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    NazwaBanku = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    UwagiWewnetrzne = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    UwagiPubliczne = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    CzyArchiwalny = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CzyPodmiot = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CzyTP = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SposobPlatnosciId = table.Column<int>(type: "int", nullable: true),
                    KodUrzedu = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    OsobaFizycznaImie = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    OsobaFizycznaNazwisko = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    OsobaFizycznaDataUrodzenia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FormaOpodatkowania = table.Column<int>(type: "int", nullable: true),
                    TokenKSeF = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    SrodowiskoKSeF = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontrahent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kontrahent_SposobPlatnosci_SposobPlatnosciId",
                        column: x => x.SposobPlatnosciId,
                        principalTable: "SposobPlatnosci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Towar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Rodzaj = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CenaNetto = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CenaBrutto = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CzyWedlugCenBrutto = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CzyArchiwalny = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    GTU = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    StawkaRyczaltu = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StawkaVatId = table.Column<int>(type: "int", nullable: true),
                    JednostkaMiaryId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numer = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    DataWystawienia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataSprzedazy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataWprowadzenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerminPlatnosci = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataWyslania = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataUsuniecia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NIPSprzedawcy = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    NazwaSprzedawcy = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    DaneSprzedawcy = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    NIPNabywcy = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    NazwaNabywcy = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    DaneNabywcy = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    RachunekBankowy = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    NazwaBanku = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    UwagiPubliczne = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    UwagiWewnetrzne = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    RazemNetto = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    RazemVat = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    RazemBrutto = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    KursWaluty = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 1m),
                    OpisSposobuPlatnosci = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Rodzaj = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CzyWartosciReczne = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ProcentVatNaliczonego = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 100m),
                    ProcentKosztow = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 100m),
                    CzyTP = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CzyZakupSrodkowTrwalych = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CzyWDT = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CzyWNT = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ProceduraMarzy = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    OpisZdarzenia = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    NumerKSeF = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    XMLKSeF = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    URLKSeF = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    DataKSeF = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SprzedawcaId = table.Column<int>(type: "int", nullable: true),
                    NabywcaId = table.Column<int>(type: "int", nullable: true),
                    FakturaKorygowanaId = table.Column<int>(type: "int", nullable: true),
                    FakturaKorygujacaId = table.Column<int>(type: "int", nullable: true),
                    FakturaPierwotnaId = table.Column<int>(type: "int", nullable: true),
                    WalutaId = table.Column<int>(type: "int", nullable: true),
                    SposobPlatnosciId = table.Column<int>(type: "int", nullable: true),
                    DeklaracjaVatId = table.Column<int>(type: "int", nullable: true),
                    ZaliczkaPitId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faktura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faktura_DeklaracjaVat_DeklaracjaVatId",
                        column: x => x.DeklaracjaVatId,
                        principalTable: "DeklaracjaVat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                        name: "FK_Faktura_Faktura_FakturaPierwotnaId",
                        column: x => x.FakturaPierwotnaId,
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
                        name: "FK_Faktura_Kontrahent_SprzedawcaId",
                        column: x => x.SprzedawcaId,
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
                    table.ForeignKey(
                        name: "FK_Faktura_ZaliczkaPit_ZaliczkaPitId",
                        column: x => x.ZaliczkaPitId,
                        principalTable: "ZaliczkaPit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DodatkowyPodmiot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FakturaId = table.Column<int>(type: "int", nullable: false),
                    Rodzaj = table.Column<int>(type: "int", nullable: false),
                    IDwew = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    NIP = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    VatUE = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Udzial = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Plik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FakturaId = table.Column<int>(type: "int", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    Rozmiar = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ZawartoscId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plik_Faktura_FakturaId",
                        column: x => x.FakturaId,
                        principalTable: "Faktura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PozycjaFaktury",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FakturaId = table.Column<int>(type: "int", nullable: false),
                    TowarId = table.Column<int>(type: "int", nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    CenaNetto = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CenaVat = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CenaBrutto = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Ilosc = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    WartoscNetto = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    WartoscVat = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    WartoscBrutto = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CzyWedlugCenBrutto = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CzyWartosciReczne = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    StawkaVatId = table.Column<int>(type: "int", nullable: true),
                    JednostkaMiaryId = table.Column<int>(type: "int", nullable: true),
                    LP = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CzyPrzedKorekta = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    GTU = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    StawkaRyczaltu = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RabatProcent = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    RabatCena = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    RabatWartosc = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CenaZakupuDlaMarzy = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
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
                        name: "FK_PozycjaFaktury_JednostkaMiary_JednostkaMiaryId",
                        column: x => x.JednostkaMiaryId,
                        principalTable: "JednostkaMiary",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PozycjaFaktury_StawkaVat_StawkaVatId",
                        column: x => x.StawkaVatId,
                        principalTable: "StawkaVat",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PozycjaFaktury_Towar_TowarId",
                        column: x => x.TowarId,
                        principalTable: "Towar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Wplata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FakturaId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kwota = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Uwagi = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wplata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wplata_Faktura_FakturaId",
                        column: x => x.FakturaId,
                        principalTable: "Faktura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zawartosc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dane = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PlikId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zawartosc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zawartosc_Plik_PlikId",
                        column: x => x.PlikId,
                        principalTable: "Plik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Konfiguracja",
                columns: new[] { "Id", "EMailNadawca", "EMailTemat", "EMailTresc", "SMTPHaslo", "SMTPLogin", "SMTPPort", "SMTPSerwer" },
                values: new object[] { 1, "[SPRZEDAWCA-NAZWA] <[SPRZEDAWCA-EMAIL]>", "Faktura - [NUMER]", "Dzień dobry,\r\n\r\nw załączniku znajduje się faktura numer [NUMER] z dnia [DATA-SPRZEDAZY] na kwotę [KWOTA-BRUTTO] [WALUTA].\r\n\r\nWiadomość wygenerowana automatycznie.\r\n\r\n-- \r\n[SPRZEDAWCA-NAZWA]\r\n[SPRZEDAWCA-ADRES]", "tajnehaslo", "biuro", 465, "smtp.example.com" });

            migrationBuilder.CreateIndex(
                name: "IX_DodatkowyPodmiot_FakturaId",
                table: "DodatkowyPodmiot",
                column: "FakturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_DeklaracjaVatId",
                table: "Faktura",
                column: "DeklaracjaVatId");

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_FakturaKorygowanaId",
                table: "Faktura",
                column: "FakturaKorygowanaId");

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_FakturaKorygujacaId",
                table: "Faktura",
                column: "FakturaKorygujacaId");

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_FakturaPierwotnaId",
                table: "Faktura",
                column: "FakturaPierwotnaId");

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_NabywcaId",
                table: "Faktura",
                column: "NabywcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_SposobPlatnosciId",
                table: "Faktura",
                column: "SposobPlatnosciId");

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_SprzedawcaId",
                table: "Faktura",
                column: "SprzedawcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_WalutaId",
                table: "Faktura",
                column: "WalutaId");

            migrationBuilder.CreateIndex(
                name: "IX_Faktura_ZaliczkaPitId",
                table: "Faktura",
                column: "ZaliczkaPitId");

            migrationBuilder.CreateIndex(
                name: "IX_Kontrahent_SposobPlatnosciId",
                table: "Kontrahent",
                column: "SposobPlatnosciId");

            migrationBuilder.CreateIndex(
                name: "IX_Plik_FakturaId",
                table: "Plik",
                column: "FakturaId");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjaFaktury_FakturaId",
                table: "PozycjaFaktury",
                column: "FakturaId");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjaFaktury_JednostkaMiaryId",
                table: "PozycjaFaktury",
                column: "JednostkaMiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjaFaktury_StawkaVatId",
                table: "PozycjaFaktury",
                column: "StawkaVatId");

            migrationBuilder.CreateIndex(
                name: "IX_PozycjaFaktury_TowarId",
                table: "PozycjaFaktury",
                column: "TowarId");

            migrationBuilder.CreateIndex(
                name: "IX_StanNumeratora_NumeratorId",
                table: "StanNumeratora",
                column: "NumeratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Towar_JednostkaMiaryId",
                table: "Towar",
                column: "JednostkaMiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Towar_StawkaVatId",
                table: "Towar",
                column: "StawkaVatId");

            migrationBuilder.CreateIndex(
                name: "IX_Wplata_FakturaId",
                table: "Wplata",
                column: "FakturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Zawartosc_PlikId",
                table: "Zawartosc",
                column: "PlikId",
                unique: true,
                filter: "[PlikId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DodatkowyPodmiot");

            migrationBuilder.DropTable(
                name: "KolumnaSpisu");

            migrationBuilder.DropTable(
                name: "Konfiguracja");

            migrationBuilder.DropTable(
                name: "PozycjaFaktury");

            migrationBuilder.DropTable(
                name: "SkladkaZus");

            migrationBuilder.DropTable(
                name: "StanMenu");

            migrationBuilder.DropTable(
                name: "StanNumeratora");

            migrationBuilder.DropTable(
                name: "UrzadSkarbowy");

            migrationBuilder.DropTable(
                name: "Wplata");

            migrationBuilder.DropTable(
                name: "Zawartosc");

            migrationBuilder.DropTable(
                name: "Towar");

            migrationBuilder.DropTable(
                name: "Numerator");

            migrationBuilder.DropTable(
                name: "Plik");

            migrationBuilder.DropTable(
                name: "JednostkaMiary");

            migrationBuilder.DropTable(
                name: "StawkaVat");

            migrationBuilder.DropTable(
                name: "Faktura");

            migrationBuilder.DropTable(
                name: "DeklaracjaVat");

            migrationBuilder.DropTable(
                name: "Kontrahent");

            migrationBuilder.DropTable(
                name: "Waluta");

            migrationBuilder.DropTable(
                name: "ZaliczkaPit");

            migrationBuilder.DropTable(
                name: "SposobPlatnosci");
        }
    }
}
