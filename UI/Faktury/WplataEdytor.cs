using Microsoft.EntityFrameworkCore;
using ProFak.DB;

namespace ProFak.UI;

class WplataEdytor : EdytorDwieKolumny<Wplata>
{
	private readonly NumericUpDown numericUpDownKwota;
	private readonly TextBox textBoxUwagi;

	public WplataEdytor()
	{
		DodajDatePicker(wplata => wplata.Data, "Data wpływu");
		numericUpDownKwota = DodajNumericUpDown(wplata => wplata.Kwota, "Kwota");
		numericUpDownKwota.Minimum = -numericUpDownKwota.Maximum;
		textBoxUwagi = DodajTextBox(wplata => wplata.Uwagi, "Uwagi");
		DodajCheckBox(wplata => wplata.CzyRozliczenie, "Uwzględnij w rozliczeniu");
		Walidacja(textBoxUwagi, WalidacjaUwag, false);
		UstawRozmiar();
	}

	protected override void PrzygotujRekord(Wplata rekord)
	{
		base.PrzygotujRekord(rekord);
		var faktura = Kontekst.Znajdz<Faktura>();
		if (rekord.Kwota == 0 && faktura != null)
		{
			var fakturaPlusWplaty = Kontekst.Baza.Faktury
				.Include(e => e.Wplaty)
				.FirstOrDefault(e => e.Id == faktura.Id);
			rekord.Kwota = fakturaPlusWplaty!.PozostaloDoZaplaty;
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
	}

	private string?	 WalidacjaUwag(string uwagi)
	{
		if (Rekord.CzyRozliczenie && String.IsNullOrEmpty(uwagi)) return "Należy podać opis rozliczenia";
		return null;
	}
}
