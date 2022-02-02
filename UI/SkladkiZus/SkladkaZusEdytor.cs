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
			kontroler.Powiazanie(numericUpDownFunduszPracy, skladka => skladka.SkladkaFunduszPracy);
			kontroler.Powiazanie(numericUpDownSumaSkladek, skladka => skladka.SumaSkladek);
			kontroler.Powiazanie(numericUpDownOdliczenieOdDochodu, skladka => skladka.OdliczenieOdDochodu);
		}

		private void buttonPrzelicz_Click(object sender, EventArgs e)
		{
			Przelicz();
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

			Rekord.SkladkaEmerytalna = Decimal.Round(Rekord.PodstawaSpoleczne * (0.0976m + 0.0976m), 2, MidpointRounding.AwayFromZero);
			Rekord.SkladkaRentowa = Decimal.Round(Rekord.PodstawaSpoleczne * (0.015m + 0.065m), 2, MidpointRounding.AwayFromZero);
			Rekord.SkladkaWypadkowa = Decimal.Round(Rekord.PodstawaSpoleczne * 0.0167m, 2, MidpointRounding.AwayFromZero);
			Rekord.SkladkaSpoleczna = Rekord.SkladkaEmerytalna + Rekord.SkladkaRentowa + Rekord.SkladkaWypadkowa;
			Rekord.OdliczenieOdDochodu = Rekord.SkladkaSpoleczna;
			Rekord.SkladkaFunduszPracy = Decimal.Round(Rekord.PodstawaSpoleczne * 0.0245m, 2, MidpointRounding.AwayFromZero);

			przychod -= Rekord.SkladkaSpoleczna;
			var dochod = przychod - koszty;

			if (Rekord.Miesiac.Year < 2022)
			{
				Rekord.SkladkaZdrowotna = Decimal.Round(Rekord.PodstawaZdrowotne * 0.09m, 2, MidpointRounding.AwayFromZero);
			}
			else if (podmiot.FormaOpodatkowania == FormaOpodatkowania.Liniowy)
			{
				Rekord.PodstawaZdrowotne = Math.Max(dochod, minimalneWynagrodzenie);
				Rekord.SkladkaZdrowotna = Decimal.Round(Rekord.PodstawaZdrowotne * 0.049m, 2, MidpointRounding.AwayFromZero);
			}
			else if (podmiot.FormaOpodatkowania == FormaOpodatkowania.Ryczałt)
			{
				Rekord.SkladkaZdrowotna = Decimal.Round(Rekord.PodstawaZdrowotne * 0.09m, 2, MidpointRounding.AwayFromZero);
			}
			else if (podmiot.FormaOpodatkowania == FormaOpodatkowania.Skala)
			{
				Rekord.PodstawaZdrowotne = Math.Max(dochod, minimalneWynagrodzenie);
				Rekord.SkladkaZdrowotna = Decimal.Round(Rekord.PodstawaZdrowotne * 0.09m, 2, MidpointRounding.AwayFromZero);
			}

			Rekord.SumaSkladek = Rekord.SkladkaSpoleczna + Rekord.SkladkaZdrowotna + Rekord.SkladkaFunduszPracy;

			kontroler.AktualizujKontrolki();
		}
	}

	class SkladkaZusEdytorBase : Edytor<SkladkaZus>
	{
	}
}
