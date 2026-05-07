using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using QRCoder;

namespace ProFak.UI;

class WplataEdytor : EdytorDwieKolumny<Wplata>
{
	private readonly TNumericUpDown numericUpDownKwota;
	private readonly TTextBox textBoxUwagi;

	public WplataEdytor()
	{
		DodajDatePicker(wplata => wplata.Data, "Data wpływu");
		numericUpDownKwota = DodajNumericUpDown(wplata => wplata.Kwota, "Kwota");
		numericUpDownKwota.Minimum = -numericUpDownKwota.Maximum;
		textBoxUwagi = DodajTextBox(wplata => wplata.Uwagi, "Uwagi");
		Walidacja(textBoxUwagi, WalidacjaUwag, false);
	}

	protected override void PrzygotujRekord(Wplata rekord)
	{
		base.PrzygotujRekord(rekord);
		var faktura = Kontekst.Znajdz<Faktura>();
		if (rekord.Kwota == 0 && faktura != null)
		{
			var sumaWplat = Kontekst.Baza.Wplaty.Where(e => e.FakturaId == faktura.Id).Sum(e => e.Kwota);
			rekord.Kwota = faktura.RazemBrutto - sumaWplat;
		}
	}

	protected override void RekordGotowy()
	{
		base.RekordGotowy();
		var faktura = Kontekst.Znajdz<Faktura>();
		if (faktura == null)
		{
			numericUpDownKwota.Enabled = false;
			numericUpDownKwota.Text = "";
		}
		else
		{
			if (faktura.CzySprzedaz)
			{
				DodajCheckBox(wplata => wplata.CzyRozliczenie, "Uwzględnij w rozliczeniu");
			}
			if (faktura.CzyZakup && !String.IsNullOrEmpty(faktura.RachunekBankowy) && !String.IsNullOrEmpty(faktura.NIPSprzedawcy))
			{
				DodajWiersz(Kontrolki.Link("Kod QR płatności", QR), null);
			}
		}
		UstawRozmiar();
	}

	private string? WalidacjaUwag(string uwagi)
	{
		if (Rekord.CzyRozliczenie && String.IsNullOrEmpty(uwagi)) return "Należy podać opis rozliczenia";
		return null;
	}

	private void QR()
	{
		var faktura = Kontekst.Znajdz<Faktura>();
		if (faktura == null) return;
		var kodPlatnosci = faktura.NIPSprzedawcy
			+ "|PL|"
			+ faktura.RachunekBankowy.Replace(" ", "").Replace("-", "")
			+ "|" + (int)(Rekord.Kwota * 100)
			+ "|" + (faktura.NazwaSprzedawcy.Length > 20 ? faktura.NazwaSprzedawcy.Substring(0, 20) : faktura.NazwaSprzedawcy)
			+ "|" + (faktura.Numer.Length > 32 ? faktura.Numer.Substring(0, 32) : faktura.Numer)
			+ "|" // Identyfikator polecenia zapłaty
			+ "|" // Identyfikator Invobill
			+ "|"; // Rezerwa

		using var qrc = QRCodeGenerator.GenerateQrCode(kodPlatnosci, QRCodeGenerator.ECCLevel.Default);
		using var renderer = new PngByteQRCode(qrc);
		var qr = renderer.GetGraphic(20);

#if WINFORMS
		var pb = new PictureBox();
		pb.Dock = DockStyle.Fill;
		pb.SizeMode = PictureBoxSizeMode.Zoom;
		pb.Image = new Bitmap(new MemoryStream(qr));

		using var form = new Dialog("Kod płatności", pb, Kontekst);
		form.ClientSize = new Size(600, 500);
		form.BackColor = Color.White;
		form.Pokaz();
#endif
#if AVALONIA
		var image = new Avalonia.Controls.Image();
		var bitmap = new Avalonia.Media.Imaging.Bitmap(new MemoryStream(qr));
		image.Source = bitmap;
		var form = new Dialog("Kod płatności", image, Kontekst);
		form.Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Colors.White);
		form.Width = 600;
		form.Height = 500;
		form.Pokaz();
#endif
	}
}
