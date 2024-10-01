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
	partial class SkladkaZusEdytor : SkladkaZusEdytorBase
	{
		public SkladkaZusEdytor()
		{
			InitializeComponent();

			kontroler.Powiazanie(dateTimePickerMiesiac, skladka => skladka.Miesiac);

			kontroler.Powiazanie(numericUpDownPodstawaSpoleczne, skladka => skladka.PodstawaSpoleczne);
			kontroler.Powiazanie(numericUpDownPodstawaZdrowotne, skladka => skladka.PodstawaZdrowotne);
			kontroler.Powiazanie(numericUpDownSkladkaEmerytalna, skladka => skladka.SkladkaEmerytalna);
			kontroler.Powiazanie(numericUpDownSkladkaRentowa, skladka => skladka.SkladkaRentowa);
			kontroler.Powiazanie(numericUpDownSkladkaWypadkowa, skladka => skladka.SkladkaWypadkowa);
			kontroler.Powiazanie(numericUpDownSkladkaSpoleczna, skladka => skladka.SkladkaSpoleczna);
			kontroler.Powiazanie(numericUpDownSkladkaZdrowotna, skladka => skladka.SkladkaZdrowotna);
			kontroler.Powiazanie(numericUpDownRozliczenieRoczneSkladkiZdrowotnej, skladka => skladka.RozliczenieRoczneSkladkiZdrowotnej);
			kontroler.Powiazanie(numericUpDownFunduszPracy, skladka => skladka.SkladkaFunduszPracy);
			kontroler.Powiazanie(numericUpDownSumaSkladek, skladka => skladka.SumaSkladek);
			kontroler.Powiazanie(numericUpDownOdliczenieOdDochodu, skladka => skladka.OdliczenieOdDochodu);
		}

		private void buttonPrzelicz_Click(object sender, EventArgs e)
		{
			try
			{
				Przelicz();
			}
			catch (Exception exc)
			{
				OknoBledu.Pokaz(exc);
			}
		}

		private void Przelicz()
		{
			var podmiot = Kontekst.Baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.CzyPodmiot);
			if (podmiot == null || !podmiot.FormaOpodatkowania.HasValue) throw new ApplicationException("Przed wyliczeniem składek ZUS należy uzupełnić formę opodatkowania firmy.");

			var minimalneWynagrodzenie = 3000.00m;

			var przychod = 0m;
			var koszty = 0m;
			var poczatekRoku = new DateTime(Rekord.Miesiac.Year, 1, 1);
			var dataKoncowa = Rekord.Miesiac.Date.AddDays(1 - Rekord.Miesiac.Day).AddMonths(1);

			var faktury = Kontekst.Baza.Faktury.Where(faktura => faktura.DataSprzedazy >= poczatekRoku && faktura.DataSprzedazy < dataKoncowa).ToList();
			foreach (var faktura in faktury)
			{
				if (faktura.CzyZakup) koszty += faktura.Koszty;
				if (faktura.CzySprzedaz) przychod += faktura.RazemNetto;
			}

			if (Rekord.Miesiac.Month != 1)
			{
				var poprzedniMiesiac = Kontekst.Baza.SkladkiZus.Where(skladka => skladka.Miesiac < Rekord.Miesiac).OrderByDescending(skladka => skladka.Miesiac).FirstOrDefault();
				if (poprzedniMiesiac != null && poprzedniMiesiac.Miesiac.Year == Rekord.Miesiac.Year)
				{
					if (Rekord.PodstawaSpoleczne == 0) Rekord.PodstawaSpoleczne = poprzedniMiesiac.PodstawaSpoleczne;
					if (Rekord.PodstawaZdrowotne == 0) Rekord.PodstawaZdrowotne = poprzedniMiesiac.PodstawaZdrowotne;
				}
			}

			Rekord.SkladkaEmerytalna = (Rekord.PodstawaSpoleczne * (0.0976m + 0.0976m)).Zaokragl();
			Rekord.SkladkaRentowa = (Rekord.PodstawaSpoleczne * (0.015m + 0.065m)).Zaokragl();
			Rekord.SkladkaWypadkowa = (Rekord.PodstawaSpoleczne * 0.0167m).Zaokragl();
			Rekord.SkladkaSpoleczna = Rekord.SkladkaEmerytalna + Rekord.SkladkaRentowa + Rekord.SkladkaWypadkowa;
			Rekord.OdliczenieOdDochodu = Rekord.SkladkaSpoleczna;
			Rekord.SkladkaFunduszPracy = (Rekord.PodstawaSpoleczne * 0.0245m).Zaokragl();

			przychod -= Rekord.SkladkaSpoleczna;
			var dochod = przychod - koszty;

			if (Rekord.Miesiac.Year < 2022)
			{
				Rekord.SkladkaZdrowotna = (Rekord.PodstawaZdrowotne * 0.09m).Zaokragl();
			}
			else if (podmiot.FormaOpodatkowania == FormaOpodatkowania.Liniowy)
			{
				Rekord.PodstawaZdrowotne = Math.Max(dochod, minimalneWynagrodzenie);
				Rekord.SkladkaZdrowotna = (Rekord.PodstawaZdrowotne * 0.049m).Zaokragl();
				Rekord.SkladkaZdrowotna += Rekord.RozliczenieRoczneSkladkiZdrowotnej;
				Rekord.OdliczenieOdDochodu += Rekord.SkladkaZdrowotna;
				var sumaOdliczen = Kontekst.Baza.SkladkiZus.Where(skladka => skladka.Miesiac >= new DateTime(Rekord.Miesiac.Year, 1, 1) && skladka.Miesiac < skladka.Miesiac).Sum(skladka => skladka.OdliczenieOdDochodu);
				Rekord.OdliczenieOdDochodu = Math.Min(Rekord.OdliczenieOdDochodu, 8700m - sumaOdliczen);
			}
			else if (podmiot.FormaOpodatkowania == FormaOpodatkowania.Ryczałt)
			{
				Rekord.SkladkaZdrowotna = (Rekord.PodstawaZdrowotne * 0.09m).Zaokragl();
				Rekord.SkladkaZdrowotna += Rekord.RozliczenieRoczneSkladkiZdrowotnej;
				Rekord.OdliczenieOdDochodu += (Rekord.SkladkaZdrowotna * 0.5m).Zaokragl();
			}
			else if (podmiot.FormaOpodatkowania == FormaOpodatkowania.Skala)
			{
				Rekord.PodstawaZdrowotne = Math.Max(dochod, minimalneWynagrodzenie);
				Rekord.SkladkaZdrowotna = (Rekord.PodstawaZdrowotne * 0.09m).Zaokragl();
			}

			Rekord.SumaSkladek = Rekord.SkladkaSpoleczna + Rekord.SkladkaZdrowotna + Rekord.SkladkaFunduszPracy;

			kontroler.AktualizujKontrolki();
		}
	}

	class SkladkaZusEdytorBase : Edytor<SkladkaZus>
	{
	}
}
