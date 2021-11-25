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
	partial class TowarEdytor : TowarEdytorBase
	{
		public TowarEdytor()
		{
			InitializeComponent();

			kontroler.Slownik<RodzajTowaru>(comboBoxRodzaj);
			kontroler.Slownik(comboBoxSposobLiczenia, "według brutto", "według netto");
			kontroler.Slownik(comboBoxWidocznosc, "ukryty", "widoczny");

			kontroler.Powiazanie(textBoxNazwa, towar => towar.Nazwa);
			kontroler.Powiazanie(comboBoxRodzaj, towar => towar.Rodzaj);
			kontroler.Powiazanie(comboBoxSposobLiczenia, towar => towar.CzyWedlugCenBrutto);
			kontroler.Powiazanie(comboBoxStawkaVat, towar => towar.StawkaVatRef);
			kontroler.Powiazanie(numericUpDownCenaNetto, towar => towar.CenaNetto);
			kontroler.Powiazanie(numericUpDownCenaBrutto, towar => towar.CenaBrutto);
			kontroler.Powiazanie(comboBoxJednostkaMiary, towar => towar.JednostkaMiaryRef);
			kontroler.Powiazanie(comboBoxWidocznosc, towar => towar.CzyArchiwalny);
		}

		protected override void KontekstGotowy()
		{
			base.KontekstGotowy();

			new Slownik<JednostkaMiary>(
				Kontekst, comboBoxJednostkaMiary, buttonJednostkaMiary,
				Kontekst.Baza.JednostkiMiar.ToList,
				jednostka => jednostka.Skrot,
				jednostka => { if (jednostka == null) return; Rekord.JednostkaMiary = jednostka; },
				Spis.JednostkiMiar)
				.Zainstaluj();

			new Slownik<StawkaVat>(
				Kontekst, comboBoxStawkaVat, buttonStawkaVat,
				Kontekst.Baza.StawkiVat.ToList,
				stawka => stawka.Skrot,
				stawka => { if (stawka == null) return; Rekord.StawkaVat = stawka; PrzeliczCeny(); },
				Spis.StawkiVat)
				.Zainstaluj();
		}

		protected override void RekordGotowy()
		{
			base.RekordGotowy();

			Rekord.StawkaVat = Kontekst.Baza.StawkiVat.Single(stawka => stawka.Id == Rekord.StawkaVatId);
			Rekord.JednostkaMiary = Kontekst.Baza.JednostkiMiar.Single(jednostka => jednostka.Id == Rekord.JednostkaMiaryId);
			PrzeliczCeny();
		}

		private void PrzeliczCeny()
		{
			if (Rekord == null) return;
			if (Rekord.CzyWedlugCenBrutto) numericUpDownCenaNetto.Value = Decimal.Round(Rekord.CenaBrutto * 100m / (100 + Rekord.StawkaVat.Wartosc), 2, MidpointRounding.AwayFromZero);
			else numericUpDownCenaBrutto.Value = Decimal.Round(Rekord.CenaNetto * (100 + Rekord.StawkaVat.Wartosc) / 100m, 2, MidpointRounding.AwayFromZero);
		}

		private void comboBoxSposobLiczenia_SelectedIndexChanged(object sender, EventArgs e)
		{
			PrzeliczCeny();
			if (comboBoxSposobLiczenia.SelectedValue != null)
			{
				numericUpDownCenaBrutto.Enabled = (bool)comboBoxSposobLiczenia.SelectedValue;
				numericUpDownCenaNetto.Enabled = !(bool)comboBoxSposobLiczenia.SelectedValue;
			}
		}

		private void numericUpDownCenaNetto_ValueChanged(object sender, EventArgs e)
		{
			if (Rekord.CzyWedlugCenBrutto) return;
			numericUpDownCenaBrutto.Value = Decimal.Round(numericUpDownCenaNetto.Value * (100 + Rekord.StawkaVat.Wartosc) / 100m, 2, MidpointRounding.AwayFromZero);
		}

		private void numericUpDownCenaBrutto_ValueChanged(object sender, EventArgs e)
		{
			if (!Rekord.CzyWedlugCenBrutto) return;
			numericUpDownCenaNetto.Value = Decimal.Round(numericUpDownCenaBrutto.Value * 100m / (100 + Rekord.StawkaVat.Wartosc), 2, MidpointRounding.AwayFromZero);
		}
	}

	class TowarEdytorBase : Edytor<Towar>
	{
	}
}
