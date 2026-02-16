using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class ZaliczkaPit : Rekord<ZaliczkaPit>
	{
		public DateTime Miesiac { get; set; } = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day).AddMonths(-1);

		public decimal Przychody { get; set; }
		public decimal Koszty { get; set; }
		public decimal SkladkiZus { get; set; }
		public decimal Podatek { get; set; }
		public decimal Przeniesiony { get; set; }
		public decimal DoPrzeniesienia { get; set; }
		public decimal DoWplaty { get; set; }

		public string MiesiacFmt => Miesiac.ToString("MM/yyyy");

		public List<Faktura> Faktury { get; set; } = default!;

		public void WybierzFaktury(Baza baza)
		{
			var nieaktualneFaktury = baza.Faktury.Where(faktura => faktura.ZaliczkaPitId == Id).ToDictionary(faktura => faktura.Ref);
			var zmienioneFaktury = new List<Faktura>();

			var faktury = baza.Faktury
				.Where(faktura => faktura.DataSprzedazy < Miesiac.Date.AddMonths(1) && (faktura.ZaliczkaPitId == null || faktura.ZaliczkaPitId == Id))
				.ToList();

			foreach (var faktura in faktury)
			{
				if (!nieaktualneFaktury.Remove(faktura))
				{
					faktura.ZaliczkaPitRef = this;
					zmienioneFaktury.Add(faktura);
				}
			}

			foreach (var faktura in nieaktualneFaktury.Values)
			{
				faktura.ZaliczkaPitRef = default;
				zmienioneFaktury.Add(faktura);
			}

			baza.Zapisz(zmienioneFaktury);
		}

		public void Przelicz(Baza baza)
		{
			var podmiot = baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.CzyPodmiot);
			if (podmiot == null || !podmiot.FormaOpodatkowania.HasValue) throw new ApplicationException("Przed wyliczeniem zaliczki PIT należy uzupełnić formę opodatkowania firmy.");

			Przychody = 0;
			Koszty = 0;
			SkladkiZus = 0;
			Podatek = 0;
			Przeniesiony = 0;
			DoPrzeniesienia = 0;
			DoWplaty = 0;

			var poczatekRoku = new DateTime(Miesiac.Year, 1, 1);
			var dataKoncowa = Miesiac.Date.AddDays(1 - Miesiac.Day).AddMonths(1);

			var poprzednieZaliczki = baza.ZaliczkiPit
				.Where(zaliczka => zaliczka.Miesiac < Miesiac && zaliczka.Miesiac >= poczatekRoku)
				.OrderBy(zaliczka => zaliczka.Miesiac)
				.ToList();

			var skladkiZus = baza.SkladkiZus
				.Where(skladka => skladka.Miesiac >= poczatekRoku.AddMonths(-1) && skladka.Miesiac < dataKoncowa.AddMonths(-1))
				.ToList();

			var faktury = baza.Faktury
				.Where(faktura => faktura.DataSprzedazy >= poczatekRoku && faktura.DataSprzedazy < dataKoncowa)
				.Include(faktura => faktura.Pozycje)
				.ToList();

			var podstawaZdrowotna = 0m;

			foreach (var skladkaZus in skladkiZus)
			{
				SkladkiZus += skladkaZus.OdliczenieOdDochodu;
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
						Przychody += pozycja.WartoscNetto;
						Podatek += (pozycja.WartoscNetto * pozycja.StawkaRyczaltu.Value / 100m).Zaokragl(2);
					}
				}
				Podatek = Podatek.Zaokragl(0);
				if (Podatek > 0)
				{
					var sredniaStawkaRyczaltu = Podatek / Przychody;
					var odliczenieSkladekZus = (SkladkiZus * sredniaStawkaRyczaltu).Zaokragl(0);
					Podatek -= odliczenieSkladekZus;
				}
			}
			else
			{
				foreach (var faktura in faktury)
				{
					if (faktura.CzyZakup) Koszty += faktura.Koszty;
					if (faktura.CzySprzedaz) Przychody += faktura.RazemNetto;
				}

				if (podmiot.FormaOpodatkowania == FormaOpodatkowania.Liniowy)
				{
					var podstawa = Przychody - Koszty - SkladkiZus;
					Podatek = (podstawa * 0.19m).Zaokragl(0);
				}
				else if (podmiot.FormaOpodatkowania == FormaOpodatkowania.Skala)
				{
					var podstawa = Przychody - Koszty - SkladkiZus;

					if (podstawa <= 30000) Podatek = 0;
					else if (podstawa <= 120000) Podatek = (podstawa * 0.12m - 3600.00m).Zaokragl(0);
					else if (podstawa <= 1000000) Podatek = 10800.00m + ((podstawa - 120000) * 0.32m).Zaokragl(0);
					else Podatek = 10800.00m + ((podstawa - 120000) * 0.32m + (podstawa - 1000000) * 0.04m).Zaokragl(0);
				}

				if (Miesiac.Year < 2022) Podatek -= (podstawaZdrowotna * 0.0775m).Zaokragl(0);
			}

			foreach (var poprzedniaZaliczka in poprzednieZaliczki)
			{
				Przychody -= poprzedniaZaliczka.Przychody;
				Koszty -= poprzedniaZaliczka.Koszty;
				SkladkiZus -= poprzedniaZaliczka.SkladkiZus;
				Podatek -= poprzedniaZaliczka.Podatek;
				Przeniesiony = poprzedniaZaliczka.DoPrzeniesienia;
			}

			if (Podatek < 0)
			{
				DoPrzeniesienia = -Podatek;
				Podatek = 0;
			}
			else
			{
				DoWplaty = Podatek;
			}


		}
	}
}
