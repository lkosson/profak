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
	partial class KonfiguracjaEdytor : KonfiguracjaEdytorBase
	{
		public KonfiguracjaEdytor()
		{
			InitializeComponent();

			kontroler.Powiazanie(textBoxSMTPSerwer, konfiguracja => konfiguracja.SMTPSerwer);
			kontroler.Powiazanie(numericUpDownSMTPort, konfiguracja => konfiguracja.SMTPPort);
			kontroler.Powiazanie(textBoxSMTPLogin, konfiguracja => konfiguracja.SMTPLogin);
			kontroler.Powiazanie(textBoxSMTPHaslo, konfiguracja => konfiguracja.SMTPHaslo);
			kontroler.Powiazanie(textBoxEMailNadawca, konfiguracja => konfiguracja.EMailNadawca);
			kontroler.Powiazanie(textBoxEMailTemat, konfiguracja => konfiguracja.EMailTemat);
			kontroler.Powiazanie(textBoxEMailTresc, konfiguracja => konfiguracja.EMailTresc);
		}

		public static void Wyswietl()
		{
			using var nowyKontekst = new Kontekst();
			using var transakcja = nowyKontekst.Transakcja();
			var rekord = nowyKontekst.Baza.Konfiguracja.FirstOrDefault();
			if (rekord == null) rekord = Konfiguracja.Domyslna;
			nowyKontekst.Dodaj(rekord);
			using var edytor = new KonfiguracjaEdytor();
			using var okno = new Dialog("Edycja konfiguracji", edytor, nowyKontekst);
			edytor.Przygotuj(nowyKontekst, rekord);
			if (okno.ShowDialog() != DialogResult.OK) return;
			nowyKontekst.Baza.Zapisz(rekord);
			transakcja.Zatwierdz();
		}
	}

	class KonfiguracjaEdytorBase : Edytor<Konfiguracja>
	{
	}
}
