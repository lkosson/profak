using Microsoft.EntityFrameworkCore;
using ProFak.DB;

namespace ProFak.UI;

class WplataEdytor : EdytorDwieKolumny<Wplata>
{
	private readonly NumericUpDown numericUpDownKwota;
	private readonly TextBox textBoxUwagi;
	private readonly CheckBox checkBoxCzyRozliczenie;

	public WplataEdytor()
	{
		DodajDatePicker(wplata => wplata.Data, "Data wpływu");
		numericUpDownKwota = DodajNumericUpDown(wplata => wplata.Kwota, "Kwota");
		numericUpDownKwota.Minimum = -numericUpDownKwota.Maximum;
		textBoxUwagi = DodajTextBox(wplata => wplata.Uwagi, "Uwagi");
		checkBoxCzyRozliczenie = DodajCheckBox(wplata => wplata.CzyRozliczenie, "Uwzględnij w rozliczeniu");
		Walidacja(textBoxUwagi, WalidacjaUwag, false);
		UstawRozmiar();
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
			checkBoxCzyRozliczenie.Visible = faktura.CzySprzedaz;
		}
	}

	private string?	 WalidacjaUwag(string uwagi)
	{
		if (Rekord.CzyRozliczenie && String.IsNullOrEmpty(uwagi)) return "Należy podać opis rozliczenia";
		return null;
	}
}
