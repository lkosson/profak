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
		public Faktura Rekord { get => bindingSource.DataSource as Faktura; private set => bindingSource.DataSource = value; }
		public Kontekst Kontekst { get; private set; }
		private readonly SpisZAkcjami<Wplata, WplataSpis> wplaty;
		private readonly SpisZAkcjami<PozycjaFaktury, PozycjaFakturySpis> pozycjeFaktury;

		public FakturaEdytor()
		{
			InitializeComponent();
			comboBoxRodzaj.DataSource = Enum.GetValues(typeof(RodzajFaktury)).Cast<RodzajFaktury>().Select(r => new PozycjaListy<RodzajFaktury> { Wartosc = r, Opis = r.ToString() }).ToArray();
			comboBoxRodzaj.DisplayMember = "Opis";
			comboBoxRodzaj.ValueMember = "Wartosc";

			wplaty = Spis.Wplaty(Kontekst);
			tabPageWplaty.Controls.Add(wplaty);

			pozycjeFaktury = Spis.PozycjeFaktur(Kontekst);
			tabPagePozycje.Controls.Add(pozycjeFaktury);
		}

		public void Przygotuj(Kontekst kontekst, Faktura rekord)
		{
			Kontekst = kontekst;
			WypelnijSpisy();
			Rekord = rekord;
			wplaty.Spis.FakturaRef = rekord;
			wplaty.Spis.Kontekst = kontekst;
			pozycjeFaktury.Spis.FakturaRef = rekord;
			pozycjeFaktury.Spis.Kontekst = kontekst;
		}

		private void WypelnijSpisy()
		{
			new SwobodnySlownik<Waluta>(
				Kontekst, comboBoxWaluta, buttonWaluta,
				Kontekst.Baza.Waluty.ToList,
				waluta => waluta.Skrot,
				waluta => { },
				Spis.Waluty)
				.Zainstaluj();

			new SwobodnySlownik<SposobPlatnosci>(
				Kontekst, comboBoxSposobPlatnosci, buttonSposobPlatnosci,
				Kontekst.Baza.SposobyPlatnosci.ToList,
				sposobPlatnosci => sposobPlatnosci.Nazwa,
				sposobPlatnosci => { if (sposobPlatnosci == null || Rekord.SposobPlatnosciRef == sposobPlatnosci.Ref) return; Rekord.SposobPlatnosciRef = sposobPlatnosci; Rekord.OpisSposobuPlatnosci = sposobPlatnosci.Nazwa; Rekord.TerminPlatnosci = Rekord.DataWystawienia.AddDays(sposobPlatnosci.LiczbaDni); bindingSource.ResetCurrentItem(); },
				Spis.SposobyPlatnosci)
				.Zainstaluj();

			new SwobodnySlownik<Kontrahent>(
				Kontekst, comboBoxNIPNabywcy, buttonNabywca,
				Kontekst.Baza.Kontrahenci.ToList,
				kontrahent => kontrahent.NIP,
				kontrahent => { if (kontrahent == null || Rekord.NabywcaRef == kontrahent.Ref) return; Rekord.NabywcaRef = kontrahent; Rekord.NIPNabywcy = kontrahent.NIP; Rekord.NazwaNabywcy = kontrahent.PelnaNazwa; Rekord.DaneNabywcy = kontrahent.AdresRejestrowy; bindingSource.ResetCurrentItem(); },
				Spis.Kontrahenci)
				.Zainstaluj();

			new SwobodnySlownik<Kontrahent>(
				Kontekst, comboBoxNazwaNabywcy, null,
				Kontekst.Baza.Kontrahenci.ToList,
				kontrahent => kontrahent.PelnaNazwa,
				kontrahent => { if (kontrahent == null || Rekord.NabywcaRef == kontrahent.Ref) return; Rekord.NabywcaRef = kontrahent; Rekord.NIPNabywcy = kontrahent.NIP; Rekord.NazwaNabywcy = kontrahent.PelnaNazwa; Rekord.DaneNabywcy = kontrahent.AdresRejestrowy; bindingSource.ResetCurrentItem(); },
				Spis.Kontrahenci)
				.Zainstaluj();

			new SwobodnySlownik<Kontrahent>(
				Kontekst, comboBoxNIPSprzedawcy, buttonSprzedawca,
				Kontekst.Baza.Kontrahenci.ToList,
				kontrahent => kontrahent.NIP,
				kontrahent => { if (kontrahent == null || Rekord.SprzedawcaRef == kontrahent.Ref) return; Rekord.SprzedawcaRef = kontrahent; Rekord.NIPSprzedawcy = kontrahent.NIP; Rekord.NazwaSprzedawcy = kontrahent.PelnaNazwa; Rekord.DaneSprzedawcy = kontrahent.AdresRejestrowy; bindingSource.ResetCurrentItem(); },
				Spis.Kontrahenci)
				.Zainstaluj();

			new SwobodnySlownik<Kontrahent>(
				Kontekst, comboBoxNazwaSprzedawcy, null,
				Kontekst.Baza.Kontrahenci.ToList,
				kontrahent => kontrahent.PelnaNazwa,
				kontrahent => { if (kontrahent == null || Rekord.SprzedawcaRef == kontrahent.Ref) return; Rekord.SprzedawcaRef = kontrahent; Rekord.NIPSprzedawcy = kontrahent.NIP; Rekord.NazwaSprzedawcy = kontrahent.PelnaNazwa; Rekord.DaneSprzedawcy = kontrahent.AdresRejestrowy; bindingSource.ResetCurrentItem(); },
				Spis.Kontrahenci)
				.Zainstaluj();
		}
	}
}
