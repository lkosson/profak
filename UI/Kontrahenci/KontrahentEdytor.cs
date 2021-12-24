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
	partial class KontrahentEdytor : KontrahentEdytorBase
	{
		private readonly SpisZAkcjami<Faktura, FakturaSprzedazySpis> fakturySprzedazy;
		private readonly SpisZAkcjami<Faktura, FakturaZakupuSpis> fakturyZakupu;

		public KontrahentEdytor()
		{
			InitializeComponent();

			kontroler.Slownik(comboBoxStan, "archiwalny", "aktywny");

			kontroler.Powiazanie(textBoxNazwa, kontrahent => kontrahent.Nazwa);
			kontroler.Powiazanie(textBoxPelnaNazwa, kontrahent => kontrahent.PelnaNazwa);
			kontroler.Powiazanie(textBoxNIP, kontrahent => kontrahent.NIP);
			kontroler.Powiazanie(textBoxAdresRejestrowy, kontrahent => kontrahent.AdresRejestrowy);
			kontroler.Powiazanie(textBoxAdresKorespondencyjny, kontrahent => kontrahent.AdresKorespondencyjny);
			kontroler.Powiazanie(textBoxTelefon, kontrahent => kontrahent.Telefon);
			kontroler.Powiazanie(textBoxEMail, kontrahent => kontrahent.EMail);
			kontroler.Powiazanie(textBoxRachunekBankowy, kontrahent => kontrahent.RachunekBankowy);
			kontroler.Powiazanie(textBoxUwagi, kontrahent => kontrahent.Uwagi);
			kontroler.Powiazanie(comboBoxStan, kontrahent => kontrahent.CzyArchiwalny);
			kontroler.Powiazanie(checkBoxTP, kontrahent => kontrahent.CzyTP);

			kontroler.Powiazanie(comboBoxKodUrzedu, kontrahent => kontrahent.KodUrzedu);
			kontroler.Powiazanie(textBoxOsobaFizycznaImie, kontrahent => kontrahent.OsobaFizycznaImie);
			kontroler.Powiazanie(textBoxOsobaFizycznaNazwisko, kontrahent => kontrahent.OsobaFizycznaNazwisko);
			kontroler.Powiazanie(dateTimePickerOsobaFizycznaDataUrodzenia, kontrahent => kontrahent.OsobaFizycznaDataUrodzenia);

			Wymagane(textBoxNazwa);

			tabPageFakturySprzedazy.Controls.Add(fakturySprzedazy = Spisy.FakturySprzedazy());
			tabPageFakturyZakupu.Controls.Add(fakturyZakupu = Spisy.FakturyZakupu());
		}

		private void textBoxNazwa_TextChanged(object sender, EventArgs e)
		{
			textBoxPelnaNazwa.PlaceholderText = textBoxNazwa.Text;
		}

		private void textBoxAdresRejestrowy_TextChanged(object sender, EventArgs e)
		{
			textBoxAdresKorespondencyjny.PlaceholderText = textBoxAdresRejestrowy.Text;
		}

		protected override void KontekstGotowy()
		{
			base.KontekstGotowy();

			new Slownik<UrzadSkarbowy>(
				Kontekst, comboBoxKodUrzedu, buttonUrzadSkarbowy,
				Kontekst.Baza.UrzedySkarbowe.OrderBy(urzad => urzad.Kod).ToList,
				urzad => urzad.Kod,
				urzad => { if (Rekord.KodUrzedu == urzad.Kod) return; Rekord.KodUrzedu = urzad.Kod; kontroler.AktualizujKontrolki(); },
				Spisy.UrzedySkarbowe)
				.Zainstaluj();
		}

		protected override void RekordGotowy()
		{
			base.RekordGotowy();
			checkBoxTP.Visible = !Rekord.CzyPodmiot;

			fakturySprzedazy.Spis.NabywcaRef = Rekord;
			fakturySprzedazy.Spis.Kontekst = Kontekst;
			fakturyZakupu.Spis.SprzedawcaRef = Rekord;
			fakturyZakupu.Spis.Kontekst = Kontekst;

			if (Rekord.CzyPodmiot)
			{
				tabControl.TabPages.Remove(tabPageFakturySprzedazy);
				tabControl.TabPages.Remove(tabPageFakturyZakupu);
			}
			else
			{
				tabControl.TabPages.Remove(tabPagePodatki);
			}
		}

		public override void KoniecEdycji()
		{
			base.KoniecEdycji();
			if (String.IsNullOrEmpty(Rekord.PelnaNazwa)) Rekord.PelnaNazwa = Rekord.Nazwa;
			if (String.IsNullOrEmpty(Rekord.AdresKorespondencyjny)) Rekord.AdresKorespondencyjny = Rekord.AdresRejestrowy;
		}
	}

	class KontrahentEdytorBase : Edytor<Kontrahent>
	{
	}
}
