using ProFak.DB;

namespace ProFak.UI;

class DodajJakoZakupAkcja : AkcjaNaSpisie<Faktura>
{
	public override string Nazwa => "➕ Dodaj jako zakup [INS]";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Any();
	public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.None && klawisz == Keys.Insert;
	public override bool PrzeladujPoZakonczeniu => false;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
	{
		var pominOkno = false;
		if (zaznaczoneRekordy.Count() > 1)
		{
			var odp = OknoKomunikatu.PytanieTakNieAnuluj("Wybrano więcej niż jedną fakturę do dodania. Czy zapisać faktury w ciemno, bez wyświetlania formularza edycji dla każdej z nich?", domyslnie: false);
			if (odp is null) return;
			if (odp is true) pominOkno = true;
		}

		var rekordy = new List<Faktura>();
		var podmiot = kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
		foreach (var naglowek in zaznaczoneRekordy)
		{
			using var nowyKontekst = new Kontekst(kontekst);
			using var transakcja = nowyKontekst.Transakcja();
			var istniejaca = nowyKontekst.Baza.Faktury.FirstOrDefault(e => e.NumerKSeF == naglowek.NumerKSeF && e.Rodzaj != RodzajFaktury.Usunięta);
			if (istniejaca != null)
			{
				var odp = OknoKomunikatu.PytanieTakNieAnuluj($"Faktura {istniejaca.Numer} ({istniejaca.NumerKSeF}) już istnieje w bazie. Czy mimo to chcesz ją dodać ponownie?", domyslnie: false);
				if (odp is false) continue;
				if (odp is null) break;
			}

			if (String.IsNullOrEmpty(naglowek.XMLKSeF))
			{
				OknoPostepu.Uruchom(async cancellationToken =>
				{
					using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
					await api.UwierzytelnijAsync(podmiot.NIP, podmiot.TokenKSeF, cancellationToken);
					naglowek.XMLKSeF = await api.PobierzFaktureAsync(naglowek.NumerKSeF, cancellationToken);
					cancellationToken.ThrowIfCancellationRequested();
				});
			}
			var faktura = IO.FA_3.Generator.ZbudujDB(nowyKontekst.Baza, naglowek.XMLKSeF);
			faktura.NumerKSeF = naglowek.NumerKSeF;
			faktura.DataKSeF = naglowek.DataKSeF;
			using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
			faktura.URLKSeF = api.ZbudujUrl(naglowek.XMLKSeF, faktura.NIPSprzedawcy, faktura.DataWystawienia);
			nowyKontekst.Baza.Zapisz(faktura);
			IO.FA_3.Generator.PoprawPowiazaniaPoZapisie(nowyKontekst.Baza, faktura);
			naglowek.Id = faktura.Id;

			if (!pominOkno)
			{
				nowyKontekst.Dodaj(faktura);
				using var edytor = new FakturaEdytor();
				edytor.Przygotuj(nowyKontekst, faktura);
				if (!DialogEdycji.Pokaz("Nowa pozycja", edytor, nowyKontekst))
				{
					if (naglowek != zaznaczoneRekordy.Last() && !OknoKomunikatu.PytanieTakNie("Kontynuować dodawanie faktur ze wskazanych plików?"))
						break;
					continue;
				}
				edytor.KoniecEdycji();
				nowyKontekst.Baza.Zapisz(faktura);
			}
			transakcja.Zatwierdz();
			rekordy.Add(faktura);
		}
		zaznaczoneRekordy = rekordy;
	}
}
