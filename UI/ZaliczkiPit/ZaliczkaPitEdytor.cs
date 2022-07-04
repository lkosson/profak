using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	partial class ZaliczkaPitEdytor : ZaliczkaPitEdytorBase
	{
		private readonly SpisZAkcjami<Faktura, FakturaSprzedazySpis> fakturySprzedazy;
		private readonly SpisZAkcjami<Faktura, FakturaZakupuSpis> fakturyZakupu;

		public ZaliczkaPitEdytor()
		{
			InitializeComponent();

			kontroler.Powiazanie(dateTimePickerMiesiac, deklaracja => deklaracja.Miesiac);
			kontroler.Powiazanie(numericUpDownPrzychody, deklaracja => deklaracja.Przychody);
			kontroler.Powiazanie(numericUpDownKoszty, deklaracja => deklaracja.Koszty);
			kontroler.Powiazanie(numericUpDownSkladkiZus, deklaracja => deklaracja.SkladkiZus);
			kontroler.Powiazanie(numericUpDownPodatek, deklaracja => deklaracja.Podatek);
			kontroler.Powiazanie(numericUpDownPrzeniesiony, deklaracja => deklaracja.Przeniesiony);
			kontroler.Powiazanie(numericUpDownDoPrzeniesienia, deklaracja => deklaracja.DoPrzeniesienia);
			kontroler.Powiazanie(numericUpDownDoWplaty, deklaracja => deklaracja.DoWplaty);

			var dodajSprzedazDoZaliczki = new DynamicznaAkcja<Faktura>("➕ Dodaj do zaliczki [INS]", kontekst =>
			{
				using var spis = new SpisZAkcjami<Faktura, FakturaSprzedazySpis>(new FakturaSprzedazySpis { CzyBezZaliczkiPit = true });
				var faktura = Spisy.Wybierz(kontekst, spis, "Wybierz fakturę", default);
				if (faktura == null) return;
				faktura.ZaliczkaPitRef = Rekord;
				kontekst.Baza.Zapisz(faktura);
				Przelicz();
			}, Keys.Insert, Keys.None);

			var dodajZakupDoZaliczki = new DynamicznaAkcja<Faktura>("➕ Dodaj do zaliczki [INS]", kontekst =>
			{
				using var spis = new SpisZAkcjami<Faktura, FakturaZakupuSpis>(new FakturaZakupuSpis { CzyBezZaliczkiPit = true });
				var faktura = Spisy.Wybierz(kontekst, spis, "Wybierz fakturę", default);
				if (faktura == null) return;
				faktura.ZaliczkaPitRef = Rekord;
				kontekst.Baza.Zapisz(faktura);
				Przelicz();
			}, Keys.Insert, Keys.None);


			var usunZZaliczki = new DynamicznaAkcja<Faktura>("❌ Usuń z zaliczki [DEL]", (kontekst, rekordy) =>
			{
				foreach (var rekord in rekordy)
				{
					rekord.ZaliczkaPitRef = default;
				}
				kontekst.Baza.Zapisz(rekordy);
				Przelicz();
			}, Keys.Delete, Keys.None);

			fakturySprzedazy = new SpisZAkcjami<Faktura, FakturaSprzedazySpis>(new FakturaSprzedazySpis(), new AkcjaNaSpisie<Faktura>[] { dodajSprzedazDoZaliczki, new EdytujRekordAkcja<Faktura, FakturaEdytor>(), usunZZaliczki, new WydrukFakturyAkcja(), new PrzeladujAkcja<Faktura>()});
			fakturyZakupu = new SpisZAkcjami<Faktura, FakturaZakupuSpis>(new FakturaZakupuSpis(), new AkcjaNaSpisie<Faktura>[] { dodajZakupDoZaliczki, new EdytujRekordAkcja<Faktura, FakturaEdytor>(), usunZZaliczki, new PrzeladujAkcja<Faktura>()});

			tabPageFakturySprzedazy.Controls.Add(fakturySprzedazy);
			tabPageFakturyZakupu.Controls.Add(fakturyZakupu);
		}

		protected override void RekordGotowy()
		{
			base.RekordGotowy();

			fakturySprzedazy.Spis.ZaliczkaPitRef = Rekord;
			fakturySprzedazy.Spis.Kontekst = Kontekst;
			fakturyZakupu.Spis.ZaliczkaPitRef = Rekord;
			fakturyZakupu.Spis.Kontekst = Kontekst;
		}

		private void buttonPrzelicz_Click(object sender, EventArgs e)
		{
			WybierzFaktury();
			Przelicz();
		}

		private void WybierzFaktury()
		{
			var nieaktualneFaktury = Kontekst.Baza.Faktury.Where(faktura => faktura.ZaliczkaPitId == Rekord.Id).ToDictionary(faktura => faktura.Ref);
			var zmienioneFaktury = new List<Faktura>();

			var faktury = Kontekst.Baza.Faktury
				.Where(faktura => faktura.DataSprzedazy < Rekord.Miesiac.Date.AddMonths(1) && (faktura.ZaliczkaPitId == null || faktura.ZaliczkaPitId == Rekord.Id))
				.ToList();

			foreach (var faktura in faktury)
			{
				if (!nieaktualneFaktury.Remove(faktura))
				{
					faktura.ZaliczkaPitRef = Rekord;
					zmienioneFaktury.Add(faktura);
				}
			}

			foreach (var faktura in nieaktualneFaktury.Values)
			{
				faktura.ZaliczkaPitRef = default;
				zmienioneFaktury.Add(faktura);
			}

			Kontekst.Baza.Zapisz(zmienioneFaktury);

			fakturySprzedazy.Spis.PrzeladujBezpiecznie();
			fakturyZakupu.Spis.PrzeladujBezpiecznie();
		}

		private void Przelicz()
		{
			var podmiot = Kontekst.Baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.CzyPodmiot);
			if (podmiot == null || !podmiot.FormaOpodatkowania.HasValue) throw new ApplicationException("Przed wyliczeniem zaliczki PIT należy uzupełnić formę opodatkowania firmy.");
			
			Rekord.Przychody = 0;
			Rekord.Koszty = 0;
			Rekord.SkladkiZus = 0;
			Rekord.Podatek = 0;
			Rekord.Przeniesiony = 0;
			Rekord.DoPrzeniesienia = 0;
			Rekord.DoWplaty = 0;

			var poczatekRoku = new DateTime(Rekord.Miesiac.Year, 1, 1);
			var dataKoncowa = Rekord.Miesiac.Date.AddDays(1 - Rekord.Miesiac.Day).AddMonths(1);

			var poprzednieZaliczki = Kontekst.Baza.ZaliczkiPit
				.Where(zaliczka => zaliczka.Miesiac < Rekord.Miesiac && zaliczka.Miesiac >= poczatekRoku)
				.OrderBy(zaliczka => zaliczka.Miesiac)
				.ToList();

			var skladkiZus = Kontekst.Baza.SkladkiZus
				.Where(skladka => skladka.Miesiac >= poczatekRoku && skladka.Miesiac < dataKoncowa)
				.ToList();

			if (!skladkiZus.Any() || skladkiZus.Last().Miesiac != Rekord.Miesiac) throw new ApplicationException($"Przed wyliczeniem podatku należy wyliczyć składku ZUS za miesiąc {Rekord.Miesiac:MMMM yyyy}.");

			var faktury = Kontekst.Baza.Faktury
				.Where(faktura => faktura.DataSprzedazy >= poczatekRoku && faktura.DataSprzedazy < dataKoncowa)
				.Include(faktura => faktura.Pozycje)
				.ToList();

			var podstawaZdrowotna = 0m;

			foreach (var skladkaZus in skladkiZus)
			{
				Rekord.SkladkiZus += skladkaZus.SkladkaSpoleczna;
				podstawaZdrowotna += skladkaZus.PodstawaZdrowotne;
			}

			if (podmiot.FormaOpodatkowania == FormaOpodatkowania.Ryczałt)
			{
				foreach (var faktura in faktury)
				{
					if (!faktura.CzySprzedaz) continue;
					foreach (var pozycja in faktura.Pozycje)
					{
						if (!pozycja.StawkaRyczaltu.HasValue) continue;
						Rekord.Przychody += pozycja.WartoscNetto;
						Rekord.Podatek += Decimal.Round(pozycja.WartoscNetto * pozycja.StawkaRyczaltu.Value / 100m, 0, MidpointRounding.AwayFromZero);
					}
				}
				if (Rekord.Podatek > 0)
				{
					var sredniaStawkaRyczaltu = Rekord.Podatek / Rekord.Przychody;
					var odliczenieSkladekZus = Decimal.Round(Rekord.SkladkiZus * sredniaStawkaRyczaltu, 0, MidpointRounding.AwayFromZero);
					Rekord.Podatek -= odliczenieSkladekZus;
				}
			}
			else
			{
				foreach (var faktura in faktury)
				{
					if (faktura.CzyZakup) Rekord.Koszty += faktura.Koszty;
					if (faktura.CzySprzedaz) Rekord.Przychody += faktura.RazemNetto;
				}

				if (podmiot.FormaOpodatkowania == FormaOpodatkowania.Liniowy)
				{
					var podstawa = Rekord.Przychody - Rekord.Koszty - Rekord.SkladkiZus;
					Rekord.Podatek = Decimal.Round(podstawa * 0.19m, 0, MidpointRounding.AwayFromZero);
				}
				else if (podmiot.FormaOpodatkowania == FormaOpodatkowania.Skala)
				{
					var przychodDlaUlgi = Rekord.Przychody - Rekord.Koszty;
					decimal ulga;
					if (przychodDlaUlgi < 68412) ulga = 0;
					else if (przychodDlaUlgi < 102588) ulga = Decimal.Round((przychodDlaUlgi * 0.0688m - 4566) / 0.17m, 2, MidpointRounding.AwayFromZero);
					else if (przychodDlaUlgi < 133692) ulga = Decimal.Round((przychodDlaUlgi * -7.35m + 9829) / 0.17m, 2, MidpointRounding.AwayFromZero);
					else ulga = 0;
					var podstawa = Rekord.Przychody - Rekord.Koszty - Rekord.SkladkiZus - ulga;
					var zmniejszenie = 0m;

					if (podstawa <= 8001.49m) zmniejszenie = podstawa * 0.17m;
					else if (podstawa <= 13000.49m) zmniejszenie = 1360 - 834.88m * (podstawa - 8000) / 5000;
					else if (podstawa <= 85528.49m) zmniejszenie = 525.12m;
					else if (podstawa <= 127000.59m) zmniejszenie = 525.12m - 525.12m * (podstawa - 85528) / 41472;

					if (podstawa <= 85528) Rekord.Podatek = Decimal.Round(podstawa * 0.17m - zmniejszenie, 0, MidpointRounding.AwayFromZero);
					else if (podstawa <= 1000000) Rekord.Podatek = 14539.76m + Decimal.Round((podstawa - 85528) * 0.32m - zmniejszenie, 0, MidpointRounding.AwayFromZero);
					else Rekord.Podatek = 14539.76m + Decimal.Round((podstawa - 85528) * 0.32m + (podstawa - 1000000) * 0.04m, 0, MidpointRounding.AwayFromZero);
				}

				if (Rekord.Miesiac.Year < 2022) Rekord.Podatek -= Decimal.Round(podstawaZdrowotna * 0.0775m, 0, MidpointRounding.AwayFromZero);
			}

			foreach (var poprzedniaZaliczka in poprzednieZaliczki)
			{
				Rekord.Przychody -= poprzedniaZaliczka.Przychody;
				Rekord.Koszty -= poprzedniaZaliczka.Koszty;
				Rekord.SkladkiZus -= poprzedniaZaliczka.SkladkiZus;
				Rekord.Podatek -= poprzedniaZaliczka.Podatek;
				Rekord.Przeniesiony = poprzedniaZaliczka.DoPrzeniesienia;
			}

			if (Rekord.Podatek < 0)
			{
				Rekord.DoPrzeniesienia = -Rekord.Podatek;
				Rekord.Podatek = 0;
			}
			else
			{
				Rekord.DoWplaty = Rekord.Podatek;
			}

			kontroler.AktualizujKontrolki();
		}
	}

	class ZaliczkaPitEdytorBase : Edytor<ZaliczkaPit>
	{
	}
}
