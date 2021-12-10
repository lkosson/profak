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
	partial class FakturaEdytor : FakturaEdytorBase
	{
		private readonly SpisZAkcjami<Wplata, WplataSpis> wplaty;
		private readonly SpisZAkcjami<PozycjaFaktury, PozycjaFakturySpis> pozycjeFaktury;
		private readonly SpisZAkcjami<Plik, PlikSpis> pliki;

		public virtual bool CzySprzedaz => true;

		public FakturaEdytor()
		{
			InitializeComponent();

			kontroler.Powiazanie(textBoxNumer, faktura => faktura.Numer);
			kontroler.Powiazanie(comboBoxWaluta, faktura => faktura.WalutaRef);

			kontroler.Powiazanie(comboBoxNIPSprzedawcy, faktura => faktura.NIPSprzedawcy);
			kontroler.Powiazanie(comboBoxNazwaSprzedawcy, faktura => faktura.NazwaSprzedawcy);
			kontroler.Powiazanie(textBoxDaneSprzedawcy, faktura => faktura.DaneSprzedawcy);

			kontroler.Powiazanie(comboBoxNIPNabywcy, faktura => faktura.NIPNabywcy);
			kontroler.Powiazanie(comboBoxNazwaNabywcy, faktura => faktura.NazwaNabywcy);
			kontroler.Powiazanie(textBoxDaneNabywcy, faktura => faktura.DaneNabywcy);

			kontroler.Powiazanie(dateTimePickerDataWystawienia, faktura => faktura.DataWystawienia);
			kontroler.Powiazanie(dateTimePickerDataSprzedazy, faktura => faktura.DataSprzedazy);
			kontroler.Powiazanie(dateTimePickerDataWprowadzenia, faktura => faktura.DataWprowadzenia);
			kontroler.Powiazanie(dateTimePickerTerminPlatnosci, faktura => faktura.TerminPlatnosci);

			kontroler.Powiazanie(comboBoxSposobPlatnosci, faktura => faktura.OpisSposobuPlatnosci);
			kontroler.Powiazanie(textBoxRachunekBankowy, faktura => faktura.RachunekBankowy);

			kontroler.Powiazanie(numericUpDownNetto, faktura => faktura.RazemNetto);
			kontroler.Powiazanie(numericUpDownVat, faktura => faktura.RazemVat);
			kontroler.Powiazanie(numericUpDownBrutto, faktura => faktura.RazemBrutto);

			kontroler.Powiazanie(textBoxUwagiPubliczne, faktura => faktura.UwagiPubliczne);
			kontroler.Powiazanie(textBoxUwagiWewnetrzne, faktura => faktura.UwagiWewnetrzne);

			Wymagane(textBoxDaneNabywcy);
			Wymagane(textBoxDaneSprzedawcy);
			Wymagane(comboBoxNazwaNabywcy);
			Wymagane(comboBoxNazwaSprzedawcy);
			Wymagane(comboBoxSposobPlatnosci);
			Wymagane(comboBoxWaluta);

			tabPageWplaty.Controls.Add(wplaty = Spisy.Wplaty());
			tabPagePozycje.Controls.Add(pozycjeFaktury = Spisy.PozycjeFaktur());
			tabPagePliki.Controls.Add(pliki = Spisy.Pliki());
			pozycjeFaktury.Spis.RekordyZmienione += pozycjeFakturySpis_RekordyZmienione;
		}

		private void pozycjeFakturySpis_RekordyZmienione()
		{
			Rekord.PrzeliczRazem(Kontekst.Baza);
			kontroler.AktualizujKontrolki();
		}

		protected override void KontekstGotowy()
		{
			base.KontekstGotowy();

			new Slownik<Waluta>(
				Kontekst, comboBoxWaluta, buttonWaluta,
				Kontekst.Baza.Waluty.ToList,
				waluta => waluta.Skrot,
				waluta => { },
				Spisy.Waluty)
				.Zainstaluj();

			new Slownik<SposobPlatnosci>(
				Kontekst, comboBoxSposobPlatnosci, buttonSposobPlatnosci,
				Kontekst.Baza.SposobyPlatnosci.ToList,
				sposobPlatnosci => sposobPlatnosci.Nazwa,
				sposobPlatnosci => { if (UstawSposobPlatnosci(Rekord, sposobPlatnosci)) kontroler.AktualizujKontrolki(); },
				Spisy.SposobyPlatnosci)
				.Zainstaluj();

			new Slownik<Kontrahent>(
				Kontekst, comboBoxNIPNabywcy, buttonNabywca,
				Kontekst.Baza.Kontrahenci.Where(kontrahent => !kontrahent.CzyArchiwalny && kontrahent.CzyPodmiot == !CzySprzedaz).ToList,
				kontrahent => kontrahent.NIP,
				kontrahent => { if (UstawNabywce(Rekord, kontrahent)) kontroler.AktualizujKontrolki(); },
				Spisy.Kontrahenci)
				.Zainstaluj();

			new Slownik<Kontrahent>(
				Kontekst, comboBoxNazwaNabywcy, null,
				Kontekst.Baza.Kontrahenci.Where(kontrahent => !kontrahent.CzyArchiwalny && kontrahent.CzyPodmiot == !CzySprzedaz).ToList,
				kontrahent => kontrahent.PelnaNazwa,
				kontrahent => { if (UstawNabywce(Rekord, kontrahent)) kontroler.AktualizujKontrolki(); },
				Spisy.Kontrahenci)
				.Zainstaluj();

			new Slownik<Kontrahent>(
				Kontekst, comboBoxNIPSprzedawcy, buttonSprzedawca,
				Kontekst.Baza.Kontrahenci.Where(kontrahent => !kontrahent.CzyArchiwalny && kontrahent.CzyPodmiot == CzySprzedaz).ToList,
				kontrahent => kontrahent.NIP,
				kontrahent => { if (UstawSprzedawce(Rekord, kontrahent)) kontroler.AktualizujKontrolki(); },
				Spisy.Kontrahenci)
				.Zainstaluj();

			new Slownik<Kontrahent>(
				Kontekst, comboBoxNazwaSprzedawcy, null,
				Kontekst.Baza.Kontrahenci.Where(kontrahent => !kontrahent.CzyArchiwalny && kontrahent.CzyPodmiot == CzySprzedaz).ToList,
				kontrahent => kontrahent.PelnaNazwa,
				kontrahent => { if (UstawSprzedawce(Rekord, kontrahent)) kontroler.AktualizujKontrolki(); },
				Spisy.Kontrahenci)
				.Zainstaluj();
		}

		private bool UstawNabywce(Faktura rekord, Kontrahent kontrahent)
		{
			if (kontrahent == null || rekord.NabywcaRef == kontrahent.Ref) return false;
			rekord.NabywcaRef = kontrahent;
			rekord.NIPNabywcy = kontrahent.NIP;
			rekord.NazwaNabywcy = kontrahent.PelnaNazwa;
			rekord.DaneNabywcy = kontrahent.AdresRejestrowy;
			return true;
		}

		private bool UstawSprzedawce(Faktura rekord, Kontrahent kontrahent)
		{
			if (kontrahent == null || rekord.SprzedawcaRef == kontrahent.Ref) return false;
			rekord.SprzedawcaRef = kontrahent;
			rekord.NIPSprzedawcy = kontrahent.NIP;
			rekord.NazwaSprzedawcy = kontrahent.PelnaNazwa;
			rekord.DaneSprzedawcy = kontrahent.AdresRejestrowy;
			rekord.RachunekBankowy = kontrahent.RachunekBankowy;
			return true;
		}

		private bool UstawSposobPlatnosci(Faktura rekord, SposobPlatnosci sposobPlatnosci)
		{
			if (sposobPlatnosci == null || rekord.SposobPlatnosciRef == sposobPlatnosci.Ref) return false;
			rekord.SposobPlatnosciRef = sposobPlatnosci;
			rekord.OpisSposobuPlatnosci = sposobPlatnosci.Nazwa;
			rekord.TerminPlatnosci = rekord.DataWystawienia.AddDays(sposobPlatnosci.LiczbaDni);
			return true;
		}

		protected override void PrzygotujRekord(Faktura rekord)
		{
			base.PrzygotujRekord(rekord);
			if (rekord.WalutaRef.IsNull) rekord.WalutaRef = Kontekst.Baza.Waluty.FirstOrDefault(waluta => waluta.CzyDomyslna);
			if (rekord.SposobPlatnosciRef.IsNull) UstawSposobPlatnosci(rekord, Kontekst.Baza.SposobyPlatnosci.FirstOrDefault(sposobPlatnosci => sposobPlatnosci.CzyDomyslny));
			if (rekord.SprzedawcaRef.IsNull && (rekord.Rodzaj == RodzajFaktury.Sprzedaż || rekord.Rodzaj == RodzajFaktury.Proforma)) UstawSprzedawce(rekord, Kontekst.Baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.CzyPodmiot && !kontrahent.CzyArchiwalny));
			if (rekord.NabywcaRef.IsNull && rekord.Rodzaj == RodzajFaktury.Zakup) UstawNabywce(rekord, Kontekst.Baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.CzyPodmiot && !kontrahent.CzyArchiwalny));
		}

		protected override void RekordGotowy()
		{
			base.RekordGotowy();
			wplaty.Spis.FakturaRef = Rekord;
			wplaty.Spis.Kontekst = Kontekst;
			pozycjeFaktury.Spis.FakturaRef = Rekord;
			pozycjeFaktury.Spis.Kontekst = Kontekst;
			pliki.Spis.FakturaRef = Rekord;
			pliki.Spis.Kontekst = Kontekst;
			if (Rekord.Rodzaj == RodzajFaktury.Sprzedaż) labelRodzaj.Text = "Sprzedaż";
			else if (Rekord.Rodzaj == RodzajFaktury.Zakup) labelRodzaj.Text = "Zakup";
			else if (Rekord.Rodzaj == RodzajFaktury.KorektaSprzedaży) labelRodzaj.Text = "Korekta sprzedaży";
			else if (Rekord.Rodzaj == RodzajFaktury.KorektaZakupu) labelRodzaj.Text = "Korekta zakupu";
			else if (Rekord.Rodzaj == RodzajFaktury.Proforma) labelRodzaj.Text = "Proforma";
			else labelRodzaj.Text = Rekord.Rodzaj.ToString();

			if (String.IsNullOrWhiteSpace(Rekord.Numer) && (Rekord.Rodzaj == RodzajFaktury.Sprzedaż || Rekord.Rodzaj == RodzajFaktury.KorektaSprzedaży || Rekord.Rodzaj == RodzajFaktury.Proforma))
			{
				var numer = Numerator.NadajNumer(Kontekst.Baza, Rekord.Numerator, Rekord.Podstawienie, zwiekszLicznik: false);
				textBoxNumer.PlaceholderText = numer;
				comboBoxNazwaNabywcy.Focus();
				ActiveControl = comboBoxNazwaNabywcy;
			}
			else
			{
				Wymagane(textBoxNumer);
				textBoxNumer.Focus();
				ActiveControl = textBoxNumer;
			}
		}
	}

	class FakturaEdytorBase : Edytor<Faktura>
	{
	}
}
