using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
			kontroler.Slownik<FormaOpodatkowania>(comboBoxFormaOpodatkowania, dopuscPuste: true);
			kontroler.Slownik<SrodowiskoKSeF>(comboBoxSrodowiskoKSeF);

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
			kontroler.Powiazanie(comboBoxFormaOpodatkowania, kontrahent => kontrahent.FormaOpodatkowania);
			kontroler.Powiazanie(textBoxTokenKSeF, kontrahent => kontrahent.TokenKSeF);
			kontroler.Powiazanie(comboBoxSrodowiskoKSeF, kontrahent => kontrahent.SrodowiskoKSeF);

			Wymagane(textBoxNazwa);

			fakturySprzedazy = new SpisZAkcjami<Faktura, FakturaSprzedazySpis>(new FakturaSprzedazySpis(), new AkcjaNaSpisie<Faktura>[] { new EdytujRekordAkcja<Faktura, FakturaEdytor>(), new WydrukFakturyAkcja(), new PrzeladujAkcja<Faktura>() });
			fakturyZakupu = new SpisZAkcjami<Faktura, FakturaZakupuSpis>(new FakturaZakupuSpis(), new AkcjaNaSpisie<Faktura>[] { new EdytujRekordAkcja<Faktura, FakturaEdytor>(), new PrzeladujAkcja<Faktura>() });

			tabPageFakturySprzedazy.Controls.Add(fakturySprzedazy);
			tabPageFakturyZakupu.Controls.Add(fakturyZakupu);

			dateTimePickerOsobaFizycznaDataUrodzenia.CustomFormat = Format.Data;
			dateTimePickerOsobaFizycznaDataUrodzenia.Format = DateTimePickerFormat.Custom;
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

		private void buttonSprawdzMF_Click(object sender, EventArgs e)
		{
			buttonSprawdzMF.Enabled = false;
			backgroundWorkerSprawdzMF.RunWorkerAsync(new[] { Rekord.NIP, Rekord.RachunekBankowy });
		}

		private void backgroundWorkerSprawdzMF_DoWork(object sender, DoWorkEventArgs e)
		{
			var dane = (string[])e.Argument;
			var nip = dane[0];
			var nrb = dane[1].Replace(" ", "");
			var rid = IO.MF.SprawdzBialaListeVAT(nip, nrb);
			e.Result = $"Rachunek znajduje się na białej liście VAT, RequestId: {rid}";
		}

		private void backgroundWorkerSprawdzMF_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			buttonSprawdzMF.Enabled = true;
			if (e.Error != null) OknoBledu.Pokaz(e.Error);
			else MessageBox.Show((string)e.Result, "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void buttonPobierzGUS_Click(object sender, EventArgs e)
		{
			buttonPobierzGUS.Enabled = false;
			backgroundWorkerPobierzGUS.RunWorkerAsync(Rekord);
		}

		private void backgroundWorkerPobierzGUS_DoWork(object sender, DoWorkEventArgs e)
		{
			var kontrahent = (Kontrahent)e.Argument;
			IO.GUS.PobierzGUS(kontrahent).GetAwaiter().GetResult();
		}

		private void backgroundWorkerPobierzGUS_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			buttonPobierzGUS.Enabled = true;
			if (e.Error != null) OknoBledu.Pokaz(e.Error);
			else kontroler.AktualizujKontrolki();
		}
	}

	class KontrahentEdytorBase : Edytor<Kontrahent>
	{
	}
}
