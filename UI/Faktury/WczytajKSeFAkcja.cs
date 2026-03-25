using ProFak.DB;
using System.Text.RegularExpressions;

namespace ProFak.UI;

class WczytajKSeFAkcja : AkcjaNaSpisie<Faktura>
{
	public override string Nazwa => "➕ Wczytaj XML KSeF [CTRL-K]";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => true;
	public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => klawisz == Keys.K && modyfikatory == Keys.Control;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
	{
		using var dialog = new OpenFileDialog();
		dialog.Filter = "e-Faktura XML (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*";
		dialog.Title = "Wybierz e-Fakturę do załadowania";
		dialog.RestoreDirectory = true;
		dialog.Multiselect = true;
		if (dialog.ShowDialog() != DialogResult.OK) return;

		var rekordy = new List<Faktura>();
		var pliki = dialog.FileNames;
		var pominOkno = false;
		if (pliki.Length > 1)
		{
			var odp = MessageBox.Show("Wybrano więcej niż jeden plik do importu. Czy wczytać faktury w ciemno, bez wyświetlania formularza edycji dla każdej z nich?", "ProFak", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (odp == DialogResult.Cancel) return;
			if (odp == DialogResult.Yes) pominOkno = true;
		}

		for (var i = 0; i < pliki.Length; i++)
		{
			var plik = pliki[i];
			using var nowyKontekst = new Kontekst(kontekst);
			using var transakcja = nowyKontekst.Transakcja();
			var faktura = DodajFakture(nowyKontekst, plik, pominOkno);
			if (faktura == null)
			{
				if (i < pliki.Length - 1 && MessageBox.Show("Kontynuować dodawanie faktur ze wskazanych plików?", "ProFak", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
					break;
				continue;
			}
			transakcja.Zatwierdz();
			rekordy.Add(faktura);
		}
		zaznaczoneRekordy = rekordy;
	}

	private Faktura? DodajFakture(Kontekst kontekst, string plik, bool pominOkno)
	{
		var xml = File.ReadAllText(plik);
		var faktura = IO.FA_3.Generator.ZbudujDB(kontekst.Baza, xml);
		faktura.DataKSeF = DateTime.Now;
		var numerKsef = Path.GetFileNameWithoutExtension(plik);
		if (Regex.IsMatch(numerKsef, @"\d{10}-\d{8}-[0-9A-Fa-f]{12}-[0-9A-Fa-f]{2}"))
		{
			var istniejaca = kontekst.Baza.Faktury.FirstOrDefault(e => e.NumerKSeF == numerKsef);
			if (istniejaca != null && MessageBox.Show($"Faktura {istniejaca.Numer} ({istniejaca.NumerKSeF}) już istnieje w bazie. Czy mimo to chcesz ją dodać ponownie?", "ProFak", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes) 
				return null;
			faktura.NumerKSeF = numerKsef;
		}
		kontekst.Baza.Zapisz(faktura);

		if (!pominOkno)
		{
			kontekst.Dodaj(faktura);
			using var edytor = new FakturaEdytor();
			using var okno = new Dialog("Nowa pozycja", edytor, kontekst);
			edytor.Przygotuj(kontekst, faktura);
			if (okno.ShowDialog() != DialogResult.OK) return null;
			edytor.KoniecEdycji();
		}
		kontekst.Baza.Zapisz(faktura);
		return faktura;
	}
}
