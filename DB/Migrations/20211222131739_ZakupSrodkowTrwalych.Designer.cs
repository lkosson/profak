﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProFak.DB;

namespace ProFak.DB.Migrations
{
    [DbContext(typeof(Baza))]
    [Migration("20211222131739_ZakupSrodkowTrwalych")]
    partial class ZakupSrodkowTrwalych
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("ProFak.DB.Faktura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CzyTP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<bool>("CzyWartosciReczne")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<bool>("CzyZakupSrodkowTrwalych")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<string>("DaneNabywcy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("DaneSprzedawcy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<DateTime>("DataSprzedazy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataWprowadzenia")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataWystawienia")
                        .HasColumnType("TEXT");

                    b.Property<int?>("FakturaKorygowanaId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FakturaKorygujacaId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("KursWaluty")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(1m);

                    b.Property<string>("NIPNabywcy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("NIPSprzedawcy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<int?>("NabywcaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NazwaNabywcy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("NazwaSprzedawcy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("Numer")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("OpisSposobuPlatnosci")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<decimal>("ProcentKosztow")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(100m);

                    b.Property<decimal>("ProcentVatNaliczonego")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(100m);

                    b.Property<string>("RachunekBankowy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<decimal>("RazemBrutto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("RazemNetto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("RazemVat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(0m);

                    b.Property<int>("Rodzaj")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.Property<int?>("SposobPlatnosciId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SprzedawcaId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TerminPlatnosci")
                        .HasColumnType("TEXT");

                    b.Property<string>("UwagiPubliczne")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("UwagiWewnetrzne")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<int?>("WalutaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FakturaKorygowanaId");

                    b.HasIndex("FakturaKorygujacaId");

                    b.HasIndex("NabywcaId");

                    b.HasIndex("SposobPlatnosciId");

                    b.HasIndex("SprzedawcaId");

                    b.HasIndex("WalutaId");

                    b.ToTable("Faktura");
                });

            modelBuilder.Entity("ProFak.DB.JednostkaMiary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CzyDomyslna")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<int>("LiczbaMiescPoPrzecinku")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("Skrot")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.HasKey("Id");

                    b.ToTable("JednostkaMiary");
                });

            modelBuilder.Entity("ProFak.DB.Kontrahent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AdresKorespondencyjny")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("AdresRejestrowy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<bool>("CzyArchiwalny")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<bool>("CzyPodmiot")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<bool>("CzyTP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<string>("EMail")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("NIP")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("PelnaNazwa")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("RachunekBankowy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("Telefon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("Uwagi")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.HasKey("Id");

                    b.ToTable("Kontrahent");
                });

            modelBuilder.Entity("ProFak.DB.Numerator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Format")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<int>("Przeznaczenie")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Numerator");
                });

            modelBuilder.Entity("ProFak.DB.Plik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FakturaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<int>("Rozmiar")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.Property<int>("ZawartoscId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FakturaId");

                    b.ToTable("Plik");
                });

            modelBuilder.Entity("ProFak.DB.PozycjaFaktury", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CenaBrutto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("CenaNetto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("CenaVat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(0m);

                    b.Property<bool>("CzyPrzedKorekta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<bool>("CzyWartosciReczne")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<bool>("CzyWedlugCenBrutto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<int>("FakturaId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GTU")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.Property<decimal>("Ilosc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(0m);

                    b.Property<int>("LP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1);

                    b.Property<string>("Opis")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<int?>("StawkaVatId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TowarId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("WartoscBrutto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("WartoscNetto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("WartoscVat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.HasIndex("FakturaId");

                    b.HasIndex("StawkaVatId");

                    b.HasIndex("TowarId");

                    b.ToTable("PozycjaFaktury");
                });

            modelBuilder.Entity("ProFak.DB.SposobPlatnosci", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CzyDomyslny")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<int>("LiczbaDni")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.HasKey("Id");

                    b.ToTable("SposobPlatnosci");
                });

            modelBuilder.Entity("ProFak.DB.StanNumeratora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumeratorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OstatniaWartosc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.Property<string>("Parametry")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.HasKey("Id");

                    b.HasIndex("NumeratorId");

                    b.ToTable("StanNumeratora");
                });

            modelBuilder.Entity("ProFak.DB.StawkaVat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CzyDomyslna")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<string>("Skrot")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<decimal>("Wartosc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.ToTable("StawkaVat");
                });

            modelBuilder.Entity("ProFak.DB.Towar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CenaBrutto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("CenaNetto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(0m);

                    b.Property<bool>("CzyArchiwalny")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<bool>("CzyWedlugCenBrutto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<int>("GTU")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.Property<int?>("JednostkaMiaryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<int>("Rodzaj")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.Property<int?>("StawkaVatId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("JednostkaMiaryId");

                    b.HasIndex("StawkaVatId");

                    b.ToTable("Towar");
                });

            modelBuilder.Entity("ProFak.DB.Waluta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CzyDomyslna")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("Skrot")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.HasKey("Id");

                    b.ToTable("Waluta");
                });

            modelBuilder.Entity("ProFak.DB.Wplata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Data")
                        .HasColumnType("TEXT");

                    b.Property<int>("FakturaId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Kwota")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.HasIndex("FakturaId");

                    b.ToTable("Wplata");
                });

            modelBuilder.Entity("ProFak.DB.Zawartosc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Dane")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<int?>("PlikId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlikId")
                        .IsUnique();

                    b.ToTable("Zawartosc");
                });

            modelBuilder.Entity("ProFak.DB.Faktura", b =>
                {
                    b.HasOne("ProFak.DB.Faktura", "FakturaKorygowana")
                        .WithMany()
                        .HasForeignKey("FakturaKorygowanaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProFak.DB.Faktura", "FakturaKorygujaca")
                        .WithMany()
                        .HasForeignKey("FakturaKorygujacaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProFak.DB.Kontrahent", "Nabywca")
                        .WithMany()
                        .HasForeignKey("NabywcaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProFak.DB.SposobPlatnosci", "SposobPlatnosci")
                        .WithMany()
                        .HasForeignKey("SposobPlatnosciId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProFak.DB.Kontrahent", "Sprzedawca")
                        .WithMany()
                        .HasForeignKey("SprzedawcaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProFak.DB.Waluta", "Waluta")
                        .WithMany()
                        .HasForeignKey("WalutaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("FakturaKorygowana");

                    b.Navigation("FakturaKorygujaca");

                    b.Navigation("Nabywca");

                    b.Navigation("SposobPlatnosci");

                    b.Navigation("Sprzedawca");

                    b.Navigation("Waluta");
                });

            modelBuilder.Entity("ProFak.DB.Plik", b =>
                {
                    b.HasOne("ProFak.DB.Faktura", "Faktura")
                        .WithMany("Pliki")
                        .HasForeignKey("FakturaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faktura");
                });

            modelBuilder.Entity("ProFak.DB.PozycjaFaktury", b =>
                {
                    b.HasOne("ProFak.DB.Faktura", "Faktura")
                        .WithMany("Pozycje")
                        .HasForeignKey("FakturaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProFak.DB.StawkaVat", "StawkaVat")
                        .WithMany()
                        .HasForeignKey("StawkaVatId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ProFak.DB.Towar", "Towar")
                        .WithMany()
                        .HasForeignKey("TowarId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Faktura");

                    b.Navigation("StawkaVat");

                    b.Navigation("Towar");
                });

            modelBuilder.Entity("ProFak.DB.StanNumeratora", b =>
                {
                    b.HasOne("ProFak.DB.Numerator", "Numerator")
                        .WithMany("Stany")
                        .HasForeignKey("NumeratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Numerator");
                });

            modelBuilder.Entity("ProFak.DB.Towar", b =>
                {
                    b.HasOne("ProFak.DB.JednostkaMiary", "JednostkaMiary")
                        .WithMany()
                        .HasForeignKey("JednostkaMiaryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProFak.DB.StawkaVat", "StawkaVat")
                        .WithMany()
                        .HasForeignKey("StawkaVatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("JednostkaMiary");

                    b.Navigation("StawkaVat");
                });

            modelBuilder.Entity("ProFak.DB.Wplata", b =>
                {
                    b.HasOne("ProFak.DB.Faktura", "Faktura")
                        .WithMany("Wplaty")
                        .HasForeignKey("FakturaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faktura");
                });

            modelBuilder.Entity("ProFak.DB.Zawartosc", b =>
                {
                    b.HasOne("ProFak.DB.Plik", "Plik")
                        .WithOne("Zawartosc")
                        .HasForeignKey("ProFak.DB.Zawartosc", "PlikId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Plik");
                });

            modelBuilder.Entity("ProFak.DB.Faktura", b =>
                {
                    b.Navigation("Pliki");

                    b.Navigation("Pozycje");

                    b.Navigation("Wplaty");
                });

            modelBuilder.Entity("ProFak.DB.Numerator", b =>
                {
                    b.Navigation("Stany");
                });

            modelBuilder.Entity("ProFak.DB.Plik", b =>
                {
                    b.Navigation("Zawartosc");
                });
#pragma warning restore 612, 618
        }
    }
}
