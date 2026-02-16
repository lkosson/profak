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
			kontroler.Slownik<decimal?>(comboBoxStawkaRyczaltu, null, 17m, 15m, 14m, 12.5m, 12m, 10m, 8.5m, 5.5m, 3m, 2m);

			kontroler.Powiazanie(textBoxNazwa, towar => towar.Nazwa);
			kontroler.Powiazanie(comboBoxRodzaj, towar => towar.Rodzaj);
			kontroler.Powiazanie(comboBoxSposobLiczenia, towar => towar.CzyWedlugCenBrutto);
			kontroler.Powiazanie(comboBoxStawkaVat, towar => towar.StawkaVatRef);
			kontroler.Powiazanie(numericUpDownCenaNetto, towar => towar.CenaNetto);
			kontroler.Powiazanie(numericUpDownCenaBrutto, towar => towar.CenaBrutto);
			kontroler.Powiazanie(comboBoxJednostkaMiary, towar => towar.JednostkaMiaryRef);
			kontroler.Powiazanie(comboBoxWidocznosc, towar => towar.CzyArchiwalny);
			kontroler.Powiazanie(comboBoxGTU, towar => towar.GTU);
			kontroler.Powiazanie(comboBoxStawkaRyczaltu, towar => towar.StawkaRyczaltu);

			Wymagane(textBoxNazwa);
			Wymagane(comboBoxStawkaVat);
		}

		protected override void KontekstGotowy()
		{
			base.KontekstGotowy();

			new Slownik<JednostkaMiary>(
				Kontekst, comboBoxJednostkaMiary, buttonJednostkaMiary,
				Kontekst.Baza.JednostkiMiar.OrderBy(jednostka => jednostka.Skrot).ToList,
				jednostka => jednostka.Skrot,
				jednostka => { if (jednostka == null) return; Rekord.JednostkaMiary = jednostka; },
				Spisy.JednostkiMiar)
				.Zainstaluj();

			new Slownik<StawkaVat>(
				Kontekst, comboBoxStawkaVat, buttonStawkaVat,
				Kontekst.Baza.StawkiVat.OrderBy(stawka => stawka.Skrot).ToList,
				stawka => stawka.Skrot,
				stawka => { if (stawka == null) return; Rekord.StawkaVat = stawka; PrzeliczCeny(); },
				Spisy.StawkiVat)
				.Zainstaluj();
		}

		protected override void PrzygotujRekord(Towar rekord)
		{
			base.PrzygotujRekord(rekord);
			if (rekord.StawkaVatRef.IsNull)
			{
				var domyslna = Kontekst.Baza.StawkiVat.OrderByDescending(stawka => stawka.CzyDomyslna).ThenBy(stawka => stawka.Id).FirstOrDefault();
				if (domyslna != null)
				{
					rekord.StawkaVat = domyslna;
					rekord.StawkaVatRef = domyslna;
				}
			}
			else
			{
				rekord.StawkaVat = Kontekst.Baza.StawkiVat.Single(stawka => stawka.Id == rekord.StawkaVatId);
			}

			if (rekord.JednostkaMiaryRef.IsNull)
			{
				var domyslna = Kontekst.Baza.JednostkiMiar.OrderByDescending(jednostka => jednostka.CzyDomyslna).ThenBy(jednostka => jednostka.Id).FirstOrDefault();
				rekord.JednostkaMiary = domyslna;
				rekord.JednostkaMiaryRef = domyslna;
			}
			else
			{
				rekord.JednostkaMiary = Kontekst.Baza.JednostkiMiar.Single(jednostka => jednostka.Id == rekord.JednostkaMiaryId);
			}
		}

		protected override void RekordGotowy()
		{
			base.RekordGotowy();
			PrzeliczCeny();
		}

		private void PrzeliczCeny()
		{
			if (Rekord == null) return;
			// Ustawione w PrzygotujRekord
			ArgumentNullException.ThrowIfNull(Rekord.StawkaVat);
			if (Rekord.CzyWedlugCenBrutto) numericUpDownCenaNetto.Value = (Rekord.CenaBrutto * 100m / (100 + Rekord.StawkaVat.Wartosc)).Zaokragl();
			else numericUpDownCenaBrutto.Value = (Rekord.CenaNetto * (100 + Rekord.StawkaVat.Wartosc) / 100m).Zaokragl();
		}

		private void comboBoxSposobLiczenia_SelectedIndexChanged(object? sender, EventArgs e)
		{
			PrzeliczCeny();
			if (comboBoxSposobLiczenia.SelectedValue != null)
			{
				numericUpDownCenaBrutto.Enabled = (bool)comboBoxSposobLiczenia.SelectedValue;
				numericUpDownCenaNetto.Enabled = !(bool)comboBoxSposobLiczenia.SelectedValue;
			}
		}

		private void numericUpDownCenaNetto_ValueChanged(object? sender, EventArgs e)
		{
			if (Rekord.CzyWedlugCenBrutto) return;
			// Ustawione w PrzygotujRekord
			ArgumentNullException.ThrowIfNull(Rekord.StawkaVat);
			numericUpDownCenaBrutto.Value = (numericUpDownCenaNetto.Value * (100 + Rekord.StawkaVat.Wartosc) / 100m).Zaokragl();
		}

		private void numericUpDownCenaBrutto_ValueChanged(object? sender, EventArgs e)
		{
			if (!Rekord.CzyWedlugCenBrutto) return;
			// Ustawione w PrzygotujRekord
			ArgumentNullException.ThrowIfNull(Rekord.StawkaVat);
			numericUpDownCenaNetto.Value = (numericUpDownCenaBrutto.Value * 100m / (100 + Rekord.StawkaVat.Wartosc)).Zaokragl();
		}
	}

	class TowarEdytorBase : Edytor<Towar>
	{
	}
}
