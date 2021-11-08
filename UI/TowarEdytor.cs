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
	partial class TowarEdytor : UserControl, IEdytor<Towar>
	{
		public Towar Rekord { get => bindingSource.DataSource as Towar; private set { UzupelnijPowiazaneWlasciwosci(value); bindingSource.DataSource = value; PrzeliczCeny(); } }
		public Kontekst Kontekst { get; private set; }

		public TowarEdytor()
		{
			InitializeComponent();
			comboBoxRodzaj.DataSource = Enum.GetValues(typeof(RodzajTowaru)).Cast<RodzajTowaru>().Select(r => new PozycjaListy<RodzajTowaru> { Wartosc = r, Opis = r.ToString() }).ToArray();
			comboBoxRodzaj.DisplayMember = "Opis";
			comboBoxRodzaj.ValueMember = "Wartosc";

			comboBoxSposobLiczenia.DataSource = new[] { new PozycjaListy<bool> { Wartosc = false, Opis = "według netto" }, new PozycjaListy<bool> { Wartosc = true, Opis = "według brutto" } };
			comboBoxSposobLiczenia.DisplayMember = "Opis";
			comboBoxSposobLiczenia.ValueMember = "Wartosc";

			comboBoxWidocznosc.DataSource = new[] { new PozycjaListy<bool> { Wartosc = false, Opis = "widoczny" }, new PozycjaListy<bool> { Wartosc = true, Opis = "ukryty" } };
			comboBoxWidocznosc.DisplayMember = "Opis";
			comboBoxWidocznosc.ValueMember = "Wartosc";
		}

		public void Przygotuj(Kontekst kontekst, Towar rekord)
		{
			Kontekst = kontekst;
			WypelnijSpisy();
			Rekord = rekord;
		}

		private void WypelnijSpisy()
		{
			new SwobodnySlownik<JednostkaMiary>(
				Kontekst, comboBoxJednostkaMiary, buttonJednostkaMiary,
				Kontekst.Baza.JednostkiMiar.ToList,
				jednostka => jednostka.Skrot,
				jednostka => { Rekord.JednostkaMiary = jednostka; },
				Spis.JednostkiMiar)
				.Zainstaluj();

			new SwobodnySlownik<StawkaVat>(
				Kontekst, comboBoxStawkaVat, buttonStawkaVat,
				Kontekst.Baza.StawkiVat.ToList,
				stawka => stawka.Skrot,
				stawka => { Rekord.StawkaVat = stawka; PrzeliczCeny(); },
				Spis.StawkiVat)
				.Zainstaluj();
		}

		private void UzupelnijPowiazaneWlasciwosci(Towar rekord)
		{
			rekord.StawkaVat = Kontekst.Baza.StawkiVat.Single(stawka => stawka.Id == rekord.StawkaVatId);
			rekord.JednostkaMiary = Kontekst.Baza.JednostkiMiar.Single(jednostka => jednostka.Id == rekord.JednostkaMiaryId);
		}

		private void PrzeliczCeny()
		{
			if (Rekord == null) return;
			if (Rekord.CzyWedlugCenBrutto) Rekord.CenaNetto = Decimal.Round(Rekord.CenaBrutto * 100m / (100 + Rekord.StawkaVat.Wartosc), 2, MidpointRounding.AwayFromZero);
			else Rekord.CenaBrutto = Decimal.Round(Rekord.CenaNetto * (100 + Rekord.StawkaVat.Wartosc) / 100m, 2, MidpointRounding.AwayFromZero);
		}

		private void comboBoxSposobLiczenia_SelectedIndexChanged(object sender, EventArgs e)
		{
			PrzeliczCeny();
		}

		private void numericUpDownCenaNetto_ValueChanged(object sender, EventArgs e)
		{
			if (Rekord.CzyWedlugCenBrutto) return;
			Rekord.CenaBrutto = Decimal.Round(numericUpDownCenaNetto.Value * (100 + Rekord.StawkaVat.Wartosc) / 100m, 2, MidpointRounding.AwayFromZero);
		}

		private void numericUpDownCenaBrutto_ValueChanged(object sender, EventArgs e)
		{
			if (!Rekord.CzyWedlugCenBrutto) return;
			Rekord.CenaNetto = Decimal.Round(Rekord.CenaBrutto * 100m / (100 + Rekord.StawkaVat.Wartosc), 2, MidpointRounding.AwayFromZero);
		}
	}
}
