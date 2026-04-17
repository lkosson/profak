using ProFak.DB;
using ZXing;
using ZXing.Windows.Compatibility;

namespace ProFak.UI;

class DodajWplateAkcja : AkcjaNaSpisie<Faktura>
{
	public override string Nazwa => "💰 Dodaj wpłatę [CTRL-W]";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Any(faktura => !faktura.CzyZaplacona);
	public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => klawisz == Keys.W && modyfikatory == Keys.Control;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
	{
		using var nowyKontekst = new Kontekst(kontekst);
		using var transakcja = nowyKontekst.Transakcja();
		var wplata = new Wplata { Data = DateTime.Now.Date };
		Button? buttonQR = null;
		if (zaznaczoneRekordy.Count() == 1)
		{
			var faktura = zaznaczoneRekordy.Single();
			wplata.FakturaRef = faktura;
			wplata.Kwota = faktura.PozostaloDoZaplaty;
			nowyKontekst.Dodaj(faktura);
			nowyKontekst.Baza.Zapisz(wplata);

			if (faktura.CzyZakup && !String.IsNullOrEmpty(faktura.RachunekBankowy) && !String.IsNullOrEmpty(faktura.NIPSprzedawcy))
			{
				buttonQR = new ButtonDPI();
				buttonQR.AutoSize = true;
				buttonQR.Margin = new Padding(3 * buttonQR.DeviceDpi / 96);
				buttonQR.Text = "Kod QR";
				buttonQR.Click += delegate
				{
					var kodPlatnosci = faktura.NIPSprzedawcy
						+ "|PL|"
						+ faktura.RachunekBankowy.Replace(" ", "").Replace("-", "")
						+ "|" + (int)(wplata.Kwota * 100)
						+ "|" + (faktura.NazwaSprzedawcy.Length > 20 ? faktura.NazwaSprzedawcy.Substring(0, 20) : faktura.NazwaSprzedawcy)
						+ "|" + (faktura.Numer.Length > 32 ? faktura.Numer.Substring(0, 32) : faktura.Numer)
						+ "|" // Identyfikator polecenia zapłaty
						+ "|" // Identyfikator Invobill
						+ "|"; // Rezerwa

					var writer = new BarcodeWriter();
					writer.Options.Margin = 5;
					writer.Options.Width = 500;
					writer.Options.Height = 500;
					writer.Format = BarcodeFormat.QR_CODE;
					using var qr = writer.WriteAsBitmap(kodPlatnosci);

					var pb = new PictureBox();
					pb.Dock = DockStyle.Fill;
					pb.SizeMode = PictureBoxSizeMode.Zoom;
					pb.Image = qr;

					using var form = new Dialog("Kod płatności", pb, nowyKontekst);
					form.CzyPrzyciskiWidoczne = false;
					form.ClientSize = new Size(600, 500);
					form.BackColor = Color.White;

					form.ShowDialog();
				};
			}
		}
		nowyKontekst.Dodaj(wplata);
		using var edytor = new WplataEdytor();
		if (buttonQR != null) okno.Przyciski.Controls.Add(buttonQR);
		edytor.Przygotuj(nowyKontekst, wplata);
		if (!DialogEdycji.Pokaz("Nowa wpłata", edytor, nowyKontekst)) return;
		edytor.KoniecEdycji();
		if (wplata.Id > 0)
		{
			nowyKontekst.Baza.Zapisz(wplata);
		}
		else
		{
			foreach (var faktura in zaznaczoneRekordy)
			{
				if (faktura.PozostaloDoZaplaty <= 0) continue;
				wplata.Id = 0;
				wplata.FakturaRef = faktura;
				wplata.Kwota = faktura.PozostaloDoZaplaty;
				nowyKontekst.Baza.Zapisz(wplata);
			}
		}
		transakcja.Zatwierdz();
	}
}
