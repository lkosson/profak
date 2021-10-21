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
	partial class FakturaEdytor : UserControl, IEdytor<Faktura>
	{
		public Faktura Rekord { get => bindingSource.DataSource as Faktura; private set { ignorujZmiane = true; bindingSource.DataSource = value; ignorujZmiane = false; } }
		public Kontekst Kontekst { get; private set; }
		private bool ignorujZmiane;

		public FakturaEdytor()
		{
			InitializeComponent();
			comboBoxRodzaj.DataSource = Enum.GetValues(typeof(RodzajFaktury)).Cast<RodzajFaktury>().Select(r => new PozycjaListy<RodzajFaktury> { Wartosc = r, Opis = r.ToString() }).ToArray();
			comboBoxRodzaj.DisplayMember = "Opis";
			comboBoxRodzaj.ValueMember = "Wartosc";
		}

		public void Przygotuj(Kontekst kontekst, Faktura rekord)
		{
			Kontekst = kontekst;
			WypelnijSpisy();
			UstawObowiazkowePola(rekord);
			Rekord = rekord;
		}

		private void WypelnijSpisy()
		{
			bindingSourceSposobPlatnosci.DataSource = Kontekst.Baza.SposobyPlatnosci.ToList();
			bindingSourceSprzedawca.DataSource = Kontekst.Baza.Kontrahenci.ToList();
			bindingSourceWaluta.DataSource = Kontekst.Baza.Waluty.ToList();
			comboBoxNazwaSprzedawcy.SelectedItem = null;
			comboBoxNIPSprzedawcy.SelectedItem = null;
			comboBoxWaluta.SelectedItem = null;
			comboBoxSposobPlatnosci.SelectedItem = null;
		}

		private void UstawObowiazkowePola(Faktura faktura)
		{
			if (faktura.Id == 0)
			{
				faktura.DataSprzedazy = DateTime.Now.Date;
				faktura.DataWystawienia = DateTime.Now.Date;
				faktura.DataWprowadzenia = DateTime.Now.Date;
				faktura.TerminPlatnosci = DateTime.Now.Date;
				faktura.WalutaRef = Kontekst.Baza.Waluty.FirstOrDefault(waluta => waluta.CzyDomyslna);
				faktura.SposobPlatnosciRef = Kontekst.Baza.SposobyPlatnosci.FirstOrDefault(sposob => sposob.CzyDomyslny);
				faktura.Uwagi = "";
			}
		}

		private void comboBoxNIPSprzedawcy_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Rekord == null) return;
			if (ignorujZmiane) return;
			if (comboBoxNIPSprzedawcy.SelectedItem is Kontrahent sprzedawca)
			{
				Rekord.SprzedawcaRef = sprzedawca.Ref;
				Rekord.NIPSprzedawcy = sprzedawca.NIP;
				Rekord.NazwaSprzedawcy = sprzedawca.PelnaNazwa;
				Rekord.DaneSprzedawcy = sprzedawca.AdresRejestrowy;
				bindingSource.ResetCurrentItem();
			}
			else
			{
				Rekord.SprzedawcaRef = default;
			}
		}

		private void comboBoxNIPSprzedawcy_TextChanged(object sender, EventArgs e)
		{
			if (Rekord == null) return;
			if (ignorujZmiane) return;
			Rekord.NIPSprzedawcy = comboBoxNIPSprzedawcy.Text;
		}

		private void comboBoxNazwaSprzedawcy_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Rekord == null) return;
			if (ignorujZmiane) return;
			if (comboBoxNazwaSprzedawcy.SelectedItem is Kontrahent sprzedawca)
			{
				Rekord.SprzedawcaRef = sprzedawca.Ref;
				Rekord.NIPSprzedawcy = sprzedawca.NIP;
				Rekord.NazwaSprzedawcy = sprzedawca.PelnaNazwa;
				Rekord.DaneSprzedawcy = sprzedawca.AdresRejestrowy;
				bindingSource.ResetCurrentItem();
			}
			else
			{
				Rekord.SprzedawcaRef = default;
			}
		}

		private void comboBoxNazwaSprzedawcy_TextChanged(object sender, EventArgs e)
		{
			if (Rekord == null) return;
			if (ignorujZmiane) return;
			Rekord.NazwaSprzedawcy = comboBoxNazwaSprzedawcy.Text;
		}

		private void comboBoxSposobPlatnosci_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Rekord == null) return;
			if (ignorujZmiane) return;
			if (comboBoxSposobPlatnosci.SelectedItem is SposobPlatnosci sposobPlatnosci)
			{
				Rekord.SposobPlatnosciRef = sposobPlatnosci.Ref;
				Rekord.OpisSposobuPlatnosci = sposobPlatnosci.Nazwa;
				Rekord.TerminPlatnosci = Rekord.DataWystawienia.AddDays(sposobPlatnosci.LiczbaDni);
			}
			else
			{
				Rekord.SposobPlatnosciRef = default;
			}
		}

		private void comboBoxSposobPlatnosci_TextChanged(object sender, EventArgs e)
		{
			if (Rekord == null) return;
			if (ignorujZmiane) return;
			Rekord.OpisSposobuPlatnosci = comboBoxSposobPlatnosci.Text;
		}

		private void bindingSource_DataSourceChanged(object sender, EventArgs e)
		{
			comboBoxNazwaSprzedawcy.Text = Rekord.NazwaSprzedawcy;
			comboBoxNIPSprzedawcy.Text = Rekord.NIPSprzedawcy;
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			ignorujZmiane = true;
			base.OnHandleDestroyed(e);
		}
	}
}
