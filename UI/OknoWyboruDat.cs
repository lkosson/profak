namespace ProFak.UI;

class OknoWyboruDat
{
	public static (DateTime dataPoczatkowa, DateTime dataKoncowa)? Pokaz(Kontekst kontekst, DateTime? dataPoczatkowa = null, DateTime? dataKoncowa = null)
	{
		var datePickerDataPoczatkowa = Kontrolki.DatePicker();
		var datePickerDataKoncowa = Kontrolki.DatePicker();
		if (dataPoczatkowa != null) datePickerDataPoczatkowa.Value = dataPoczatkowa.Value;
		if (dataKoncowa != null) datePickerDataKoncowa.Value = dataKoncowa.Value;
		var uklad = new DwieKolumny();
		uklad.DodajWiersz(datePickerDataPoczatkowa, "Data początkowa");
		uklad.DodajWiersz(datePickerDataKoncowa, "Data końcowa");
		if (!DialogEdycji.Pokaz("ProFak - Wybierz zakres dat", uklad, kontekst)) return default;
		return (datePickerDataPoczatkowa.Value, datePickerDataKoncowa.Value);
	}
}
