ProFak udostępnia interfejs programistyczny do automatyzacji niektórych czynności i do dostępu do bazy danych z poziomu innych aplikacji i skryptów.

Na ten moment możliwa jest integracja z innymi aplikacjami .NETowymi przez dodanie w swoim projekcie referencji do pliku `ProFak.dll` oraz referencji do pakietu `Microsoft.EntityFrameworkCore.Sqlite` i ewentualnie `ReportViewerCore.WinForms` / `KSeF.Client`.

Głównym punktem dostępu do API jest klasa `ProFak.API`, która umożliwia pracę w kontekście wskazanej w konstruktorze bazy. Za jej pośrednictwem dostępne są wszystkie encje z bazy (faktury, kontrahenci, towary itp) oraz operacje biznesowe (np. `Faktura.WystawPodobna()`).

Dostęp do danych odbywa się za pośrednictwem [Entity Framework Core](https://learn.microsoft.com/en-gb/ef/core/) poprzez kolekcje eksportowane przez `ProFak.API.Baza`. Odczyt danych nie wymaga jawnego otwierania transakcji bazodanowej. Zapis odbywa się za pośrednictwem metody `ProFak.API.Baza.Zapisz()` i powinien być wykonywany wewnętrz transakcji otwartej wywołaniem `ProFak.API.Transakcja()` i zamykanej wywołaniem `Zatwierdz()`.

Poniżej znajduje się kilka przykładów jak korzystać z interfejsu. API należy traktować jako rozwiązanie eksperymentalne.

### Pobranie bazy kontrahentów jako JSON
```
using var api = new ProFak.API(@"e:\Firma\profak.sqlite3");
var json = JsonSerializer.Serialize(api.Baza.Kontrahenci.Select(kontrahent => new { kontrahent.Id, kontrahent.NIP, kontrahent.Nazwa, kontrahent.AdresRejestrowyFmt }));
```

### Wystawienie faktury według danych słownikowych
```
using var api = new ProFak.API(@"e:\Firma\profak.sqlite3");
using var tx = api.Transakcja();
var nabywca = api.Baza.Kontrahenci.First(kh => kh.Nazwa == "ProTech");
var towar = api.Baza.Towary.First(towar => towar.Nazwa == "Zestaw promocyjny");
var usluga = api.Baza.Towary.First(towar => towar.Nazwa == "Usługa kurierska");
var faktura = api.PrzygotujFakture(ProFak.DB.RodzajFaktury.Sprzedaż, nabywca, [(towar, 5), (usluga, 1)]);
tx.Zatwierdz();
```

### Wystawienie faktury opłaconej z góry, razem z rabatem i dodatkowym opisem

```
using var api = new ProFak.API(@"e:\Firma\profak.sqlite3");
using var tx = api.Transakcja();
var nabywca = api.Baza.Kontrahenci.First(kh => kh.Nazwa == "ProTech");
var towar = api.Baza.Towary.First(towar => towar.Nazwa == "Zestaw promocyjny");
var usluga = api.Baza.Towary.First(towar => towar.Nazwa == "Usługa kurierska");
var faktura = api.PrzygotujFakture(ProFak.DB.RodzajFaktury.Sprzedaż, nabywca, [(towar, 5), (usluga, 1)]);
var pozycjaTowaru = api.Baza.PozycjeFaktur.First(pozycja => pozycja.FakturaId == faktura.Id && pozycja.TowarId == towar.Id);
pozycjaTowaru.RabatProcent = 5;
pozycjaTowaru.PrzeliczCeny(api.Baza);
api.Baza.Zapisz(pozycjaTowaru);
faktura.UwagiPubliczne = "Dziękujemy za zakupy!";
faktura.PrzeliczRazem(api.Baza);
var wplata = new ProFak.DB.Wplata { Data = DateTime.Now, FakturaRef = faktura, Kwota = faktura.PozostaloDoZaplaty, Uwagi = "PayU" };
api.Baza.Zapisz(wplata);
api.Baza.Zapisz(faktura);
tx.Zatwierdz();
```

### Wygenerowanie PDFów dla wszystkich dzisiejszych faktur

```
using var api = new ProFak.API(@"e:\Firma\profak.sqlite3");
var fvs = api.Baza.Faktury.Where(fv => fv.DataWystawienia >= DateTime.Now.Date).ToList();
var wydruk = new ProFak.Wydruki.Faktura(api.Baza, fvs);
var pdf = wydruk.ZapiszJako("PDF");
```