using ProFak.DB;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Text.Unicode;

namespace ProFak.IO.Eksport;

public class Generator
{
	private static readonly JsonSerializerOptions options;

	static Generator()
	{
		options = new JsonSerializerOptions
		{
			MaxDepth = 10,
			WriteIndented = true,
			Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.LatinExtendedA),
			TypeInfoResolver = new DefaultJsonTypeInfoResolver { Modifiers = { IgnorujNadmiarowePola } }
		};
	}

	public static string Zbuduj(Baza baza)
	{
		var dane = new Dane
		{
			DeklaracjeVat = baza.DeklaracjeVat.ToList(),
			DodatkowePodmioty = baza.DodatkowePodmioty.ToList(),
			Faktury = baza.Faktury.ToList(),
			JednostkiMiar = baza.JednostkiMiar.ToList(),
			KolumnySpisow = baza.KolumnySpisow.ToList(),
			Konfiguracja = baza.Konfiguracja.ToList(),
			Kontrahenci = baza.Kontrahenci.ToList(),
			Numeratory = baza.Numeratory.ToList(),
			Pliki = baza.Pliki.ToList(),
			PozycjeFaktur = baza.PozycjeFaktur.ToList(),
			SkladkiZus = baza.SkladkiZus.ToList(),
			SposobyPlatnosci = baza.SposobyPlatnosci.ToList(),
			StanyMenu = baza.StanyMenu.ToList(),
			StanyNumeratorow = baza.StanyNumeratorow.ToList(),
			StawkiVat = baza.StawkiVat.ToList(),
			Towary = baza.Towary.ToList(),
			UrzedySkarbowe = baza.UrzedySkarbowe.ToList(),
			Waluty = baza.Waluty.ToList(),
			Wplaty = baza.Wplaty.ToList(),
			ZaliczkiPit = baza.ZaliczkiPit.ToList(),
			Zawartosci = baza.Zawartosci.ToList()
		};

		return JsonSerializer.Serialize(dane, options);
	}

	public static void Wczytaj(Baza baza, string json)
	{
		var dane = JsonSerializer.Deserialize<Dane>(json) ?? throw new ArgumentOutOfRangeException(nameof(json));
		var fakturyDoPoprawy = new Dictionary<Faktura, (int? fakturaKorygowana, int? fakturaKorygujaca)>();
		var zawartosciDoPoprawy = new Dictionary<Zawartosc, int>();
		foreach (var faktura in dane.Faktury)
		{
			if (!faktura.FakturaKorygowanaId.HasValue && !faktura.FakturaKorygujacaId.HasValue) continue;
			fakturyDoPoprawy[faktura] = (faktura.FakturaKorygowanaId, faktura.FakturaKorygujacaId);
			faktura.FakturaKorygowanaId = null;
			faktura.FakturaKorygujacaId = null;
		}
		foreach (var zawartosc in dane.Zawartosci)
		{
			if (!zawartosc.PlikId.HasValue) continue;
			zawartosciDoPoprawy[zawartosc] = zawartosc.PlikId.Value;
			zawartosc.PlikId = null;
		}

		var fakturyDoOdpiecia = baza.Faktury.Where(faktura => faktura.FakturaKorygowanaId != null || faktura.FakturaKorygujacaId != null).ToList();
		foreach (var faktura in fakturyDoOdpiecia)
		{
			faktura.FakturaKorygowana = null;
			faktura.FakturaKorygujaca = null;
			faktura.FakturaKorygowanaId = null;
			faktura.FakturaKorygujacaId = null;
		}
		baza.Zapisz(fakturyDoOdpiecia);
		baza.Usun<Zawartosc>();
		baza.Usun<Wplata>();
		baza.Usun<PozycjaFaktury>();
		baza.Usun<Plik>();
		baza.Usun<DodatkowyPodmiot>();
		baza.Usun<Faktura>();
		baza.Usun<Towar>();
		baza.Usun<Kontrahent>();
		baza.Usun<ZaliczkaPit>();
		baza.Usun<Waluta>();
		baza.Usun<UrzadSkarbowy>();
		baza.Usun<StawkaVat>();
		baza.Usun<StanNumeratora>();
		baza.Usun<StanMenu>();
		baza.Usun<SposobPlatnosci>();
		baza.Usun<SkladkaZus>();
		baza.Usun<Numerator>();
		baza.Usun<Konfiguracja>();
		baza.Usun<KolumnaSpisu>();
		baza.Usun<JednostkaMiary>();
		baza.Usun<DeklaracjaVat>();

		baza.Dodaj(dane.DeklaracjeVat);
		baza.Dodaj(dane.JednostkiMiar);
		baza.Dodaj(dane.KolumnySpisow);
		baza.Dodaj(dane.Konfiguracja);
		baza.Dodaj(dane.Numeratory);
		baza.Dodaj(dane.SkladkiZus);
		baza.Dodaj(dane.SposobyPlatnosci);
		baza.Dodaj(dane.StanyMenu);
		baza.Dodaj(dane.StanyNumeratorow);
		baza.Dodaj(dane.StawkiVat);
		baza.Dodaj(dane.UrzedySkarbowe);
		baza.Dodaj(dane.Waluty);
		baza.Dodaj(dane.ZaliczkiPit);
		baza.Dodaj(dane.Kontrahenci); // SposobPlatnosci, Waluta
		baza.Dodaj(dane.Towary); // StawkaVat, JednostkaMiary
		baza.Dodaj(dane.Faktury); // Faktura, Kontrahent, Waluta, SposobPlatnosci, DeklaracjaVat, ZaliczkaPit
		baza.Dodaj(dane.DodatkowePodmioty); // Faktura
		baza.Dodaj(dane.Pliki); // Faktura, Zawartosc
		baza.Dodaj(dane.PozycjeFaktur); // Faktura, Towar, StawkaVat, JednostkaMiary
		baza.Dodaj(dane.Wplaty); // Faktura
		baza.Dodaj(dane.Zawartosci); // Plik

		foreach (var kv in fakturyDoPoprawy)
		{
			var faktura = kv.Key;
			faktura.FakturaKorygowanaId = kv.Value.fakturaKorygowana;
			faktura.FakturaKorygujacaId = kv.Value.fakturaKorygujaca;
		}
		foreach (var kv in zawartosciDoPoprawy)
		{
			var zawartosc = kv.Key;
			zawartosc.PlikId = kv.Value;
		}

		baza.Zapisz(fakturyDoPoprawy.Keys);
		baza.Zapisz(zawartosciDoPoprawy.Keys);
	}

	private static void IgnorujNadmiarowePola(JsonTypeInfo typeInfo)
	{
		if (!typeInfo.Type.IsAssignableTo(typeof(Rekord))) return;

		static bool Nie(object obj, object? val) => false;

		foreach (var wlasciwosc in typeInfo.Properties)
		{
			if (wlasciwosc.PropertyType.IsGenericType && wlasciwosc.PropertyType.GetGenericTypeDefinition() == typeof(Ref<>)) wlasciwosc.ShouldSerialize = Nie;
			if (wlasciwosc.PropertyType.IsAssignableTo(typeof(IEnumerable<Rekord>))) wlasciwosc.ShouldSerialize = Nie;
			if (wlasciwosc.PropertyType.IsAssignableTo(typeof(Rekord))) wlasciwosc.ShouldSerialize = Nie;
		}
	}

	class Dane
	{
		public required List<DeklaracjaVat> DeklaracjeVat { get; init; }
		public required List<DodatkowyPodmiot> DodatkowePodmioty { get; init; }
		public required List<Faktura> Faktury { get; init; }
		public required List<JednostkaMiary> JednostkiMiar { get; init; }
		public required List<KolumnaSpisu> KolumnySpisow { get; init; }
		public required List<Konfiguracja> Konfiguracja { get; init; }
		public required List<Kontrahent> Kontrahenci { get; init; }
		public required List<Numerator> Numeratory { get; init; }
		public required List<Plik> Pliki { get; init; }
		public required List<PozycjaFaktury> PozycjeFaktur { get; init; }
		public required List<SkladkaZus> SkladkiZus { get; init; }
		public required List<SposobPlatnosci> SposobyPlatnosci { get; init; }
		public required List<StanMenu> StanyMenu { get; init; }
		public required List<StanNumeratora> StanyNumeratorow { get; init; }
		public required List<StawkaVat> StawkiVat { get; init; }
		public required List<Towar> Towary { get; init; }
		public required List<UrzadSkarbowy> UrzedySkarbowe { get; init; }
		public required List<Waluta> Waluty { get; init; }
		public required List<Wplata> Wplaty { get; init; }
		public required List<ZaliczkaPit> ZaliczkiPit { get; init; }
		public required List<Zawartosc> Zawartosci { get; init; }
	}
}
