using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class SkladkaZus : Rekord<SkladkaZus>
	{
		public DateTime Miesiac { get; set; } = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day).AddMonths(-1);

		public decimal PodstawaSpoleczne { get; set; }
		public decimal PodstawaZdrowotne { get; set; }
		public decimal SkladkaEmerytalna { get; set; }
		public decimal SkladkaRentowa { get; set; }
		public decimal SkladkaWypadkowa { get; set; }
		public decimal SkladkaSpoleczna { get; set; }
		public decimal SkladkaZdrowotna { get; set; }
		public decimal RozliczenieRoczneSkladkiZdrowotnej { get; set; }
		public decimal SkladkaFunduszPracy { get; set; }
		public decimal SumaSkladek { get; set; }
		public decimal OdliczenieOdDochodu { get; set; }

		public string MiesiacFmt => Miesiac.ToString("MM/yyyy");

		public void Przelicz(Baza baza)
		{
			var podmiot = baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.CzyPodmiot);
			if (podmiot == null || !podmiot.FormaOpodatkowania.HasValue) throw new ApplicationException("Przed wyliczeniem składek ZUS należy uzupełnić formę opodatkowania firmy.");

			var minimalneWynagrodzenie = Miesiac.Year == 2025 ? 3499.50m : 3000.00m;

			var przychod = 0m;
			var koszty = 0m;
			var poczatekRoku = new DateTime(Miesiac.Year, 1, 1);
			var dataKoncowa = Miesiac.Date.AddDays(1 - Miesiac.Day).AddMonths(1);

			var faktury = baza.Faktury.Where(faktura => faktura.DataSprzedazy >= poczatekRoku && faktura.DataSprzedazy < dataKoncowa).ToList();
			foreach (var faktura in faktury)
			{
				if (faktura.CzyZakup) koszty += faktura.Koszty;
				if (faktura.CzySprzedaz) przychod += faktura.RazemNetto;
			}

			if (Miesiac.Month != 1)
			{
				var poprzedniMiesiac = baza.SkladkiZus.Where(skladka => skladka.Miesiac < Miesiac).OrderByDescending(skladka => skladka.Miesiac).FirstOrDefault();
				if (poprzedniMiesiac != null && poprzedniMiesiac.Miesiac.Year == Miesiac.Year)
				{
					if (PodstawaSpoleczne == 0) PodstawaSpoleczne = poprzedniMiesiac.PodstawaSpoleczne;
					if (PodstawaZdrowotne == 0) PodstawaZdrowotne = poprzedniMiesiac.PodstawaZdrowotne;
				}
			}

			SkladkaEmerytalna = (PodstawaSpoleczne * (0.0976m + 0.0976m)).Zaokragl();
			SkladkaRentowa = (PodstawaSpoleczne * (0.015m + 0.065m)).Zaokragl();
			SkladkaWypadkowa = (PodstawaSpoleczne * 0.0167m).Zaokragl();
			SkladkaSpoleczna = SkladkaEmerytalna + SkladkaRentowa + SkladkaWypadkowa;
			OdliczenieOdDochodu = SkladkaSpoleczna;
			SkladkaFunduszPracy = (PodstawaSpoleczne * 0.0245m).Zaokragl();

			przychod -= SkladkaSpoleczna;
			var dochod = przychod - koszty;

			if (Miesiac.Year < 2022)
			{
				SkladkaZdrowotna = (PodstawaZdrowotne * 0.09m).Zaokragl();
			}
			else if (podmiot.FormaOpodatkowania == FormaOpodatkowania.Liniowy)
			{
				PodstawaZdrowotne = Math.Max(dochod, minimalneWynagrodzenie);
				SkladkaZdrowotna = (PodstawaZdrowotne * 0.049m).Zaokragl();
				SkladkaZdrowotna += RozliczenieRoczneSkladkiZdrowotnej;
				OdliczenieOdDochodu += SkladkaZdrowotna;
				var sumaOdliczen = baza.SkladkiZus.Where(skladka => skladka.Miesiac >= new DateTime(Miesiac.Year, 1, 1) && skladka.Miesiac < skladka.Miesiac).AsEnumerable().Sum(skladka => skladka.OdliczenieOdDochodu);
				OdliczenieOdDochodu = Math.Min(OdliczenieOdDochodu, 8700m - sumaOdliczen);
			}
			else if (podmiot.FormaOpodatkowania == FormaOpodatkowania.Ryczałt)
			{
				SkladkaZdrowotna = (PodstawaZdrowotne * 0.09m).Zaokragl();
				SkladkaZdrowotna += RozliczenieRoczneSkladkiZdrowotnej;
				OdliczenieOdDochodu += (SkladkaZdrowotna * 0.5m).Zaokragl();
			}
			else if (podmiot.FormaOpodatkowania == FormaOpodatkowania.Skala)
			{
				PodstawaZdrowotne = Math.Max(dochod, minimalneWynagrodzenie);
				SkladkaZdrowotna = (PodstawaZdrowotne * 0.09m).Zaokragl();
			}

			SumaSkladek = SkladkaSpoleczna + SkladkaZdrowotna + SkladkaFunduszPracy;
		}
	}
}
