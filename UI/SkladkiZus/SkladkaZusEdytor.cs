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
			if (Rekord.Miesiac.Month != 1)
			{
				var poprzedniMiesiac = Kontekst.Baza.SkladkiZus.Where(skladka => skladka.Miesiac < Rekord.Miesiac).OrderBy(skladka => skladka.Miesiac).FirstOrDefault();
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
			Rekord.SkladkaZdrowotna = Decimal.Round(Rekord.PodstawaZdrowotne * 0.09m, 2, MidpointRounding.AwayFromZero);
			Rekord.SkladkaFunduszPracy = Decimal.Round(Rekord.PodstawaSpoleczne * 0.0245m, 2, MidpointRounding.AwayFromZero);
			Rekord.SumaSkladek = Rekord.SkladkaSpoleczna + Rekord.SkladkaZdrowotna + Rekord.SkladkaFunduszPracy;

			kontroler.AktualizujKontrolki();
		}
	}

	class SkladkaZusEdytorBase : Edytor<SkladkaZus>
	{
	}
}
