# Integracja z KSeF

ProFak umożliwia bezpośrednią integrację z API KSeF w zakresie wystawiania faktur sprzedażowych i pobierania faktur kosztowych. Jeśli nie ufasz programowi lub z innych powodów nie chcesz nadawać mu odpowiednich uprawnień, masz możliwość [ręcznego przesyłania](#ręczne-przesyłanie-faktur) plików XML z fakturami.

Przed rozpoczęciem wymiany danych z KSeF, konieczne jest jednorazowe nadanie programowi odpowiednich uprawnień. Możesz zrobić to ręcznie lub możesz pozwolić ProFakowi zrobić to w sposób automatyczny. Dostępne są następujące warianty:
 * [Automatyczne wygenerowanie tokena dostępowego](#automatyczne-wygenerowanie-tokena-dostępowego)
 * [Ręczne wygenerowanie tokena dostępowego](#ręczne-wygenerowanie-tokena-dostępowego)
 * [Ręczne wygenerowanie certyfikatu dostępowego](#ręczne-wygenerowanie-certyfikatu-dostępowego)

## Automatyczne wygenerowanie tokena dostępowego

1. Z menu wybierz spis "Kontrahenci" i kliknij na przycisk "Moja firma" z prawej strony.

![Spis "Kontrahenci" z podświetlonym przyciskiem "Moja firma"](/Materiały/zrzut-moja-firma.png)

2. W danych podstawowych upewnij się, że w polu "NIP" wpisany jest właściwy numer.
3. W zakładce "Dane urzędowe" z listy rozwijanej "Token KSeF" wskaż do którego środowiska KSeF chcesz uzyskać dostęp. Jednocześnie ProFak może być podłączony tylko do jednego środowiska. Przełączanie się pomiędzy środowiskami nie jest dobrym pomysłem. Jeśli chcesz przetestować integrację z KSeF, użyj osobnej kopii programu, z oddzielną bazą danych. Opis poszczególnych środowisk znajdziesz [na stronie Ministerstwa Finansów](https://ksef.podatki.gov.pl/).

![Ekran "Dane urzędowe"](/Materiały/zrzut-dane-urzedowe.png)

4. Kliknij przycisk "Uzyskaj dostęp". Jeśli wnioskujesz o dostęp do środowiska testowego, nie musisz robić nic więcej - ProFak wygeneruje odpowiedni token i wstawi go do pola tekstowego obok przycisku. Kliknij tylko "Zapisz" by zapisać zmiany.
5. Zapoznaj się z informacjami w wyświetlonym oknie, a następnie kliknij na przycisk "Pobierz XML do podpisu". Zostanie wygenerowany plik XML z wnioskiem o jednorazowy dostęp do KSeF. Zapisz ten plik w wygodnym miejscu, na przykład na pulpicie.

![Ekran uzyskiwania dostępu do KSeF](/Materiały/zrzut-ksef-dostep.png)

6. Kliknij na odnośnik w oknie, aby przejść na [stronę podpisywania wniosku przy użyciu Profilu Zaufanego](https://moj.gov.pl/nforms/signer/upload?xFormsAppName=SIGNER). Nie zamykaj okna ProFaka.

![Ekran podpisywania wniosku o jednorazowy dostęp do KSeF](/Materiały/zrzut-ksef-wniosek-podpisywanie.png)

7. Wskaż na stronie zapisany plik XML z wnioskiem i kliknij "Dalej".

8. Uwierzytelnij się za pomocą Profilu Zaufanego i postępuj zgodnie z instrukcjami na ekranie, by podpisać przesłany plik.

9. Po podpisaniu pliku, kliknij "Pobierz podpisane dokumenty" i zapisz podpisany plik XML w wygodnym miejscu. Możesz zapisać go na pulpicie, w miejsce wygenerowanego wcześniej wniosku.

![Ekran pobierania podpisanego wniosku](/Materiały/zrzut-ksef-wniosek-podpisany.png)

10. Wróć do ProFaka i kliknij "Wczytaj podpisany XML". W tym momencie ProFak użyje podpisanego wniosku o jednorazowy dostęp do zalogowania się do KSeF, a następnie wygenerowania nowego tokenu o nazwie "ProFak" z uprawnieniami do odbierania i wystawiania faktur.

11. Jeśli wszystko przebiegło poprawnie, w polu "Token KSeF" zostanie wstawiony wygenerowany token. Kliknij "Zapisz" by zapisać zmiany. Integracja z KSeF jest gotowa. Skasuj pliki z wnioskami. Nie będą już potrzebne i nie można ich użyć do ponownego uwierzytelnienia.

## Ręczne wygenerowanie tokena dostępowego

Jeśli nie chcesz automatyzować procesu i wolisz samemu nadać odpowiednie uprawnienia ProFakowi, postępuj według następujących kroków:

1. Zaloguj się do [Aplikacji Podatnika](https://ap.ksef.mf.gov.pl/)
2. Wybierz z menu "Tokeny" -> "Generuj token".
3. Wpisz dowolną nazwę tokena i zaznacz pozycje "wystawianie faktur" i "przeglądanie faktur".
![Ekran generowania nowego tokena KSeF](/Materiały/zrzut-ap-token-nowy.png)

4. Kliknij "Generuj token".
5. Skopiuj do schowka wygenerowany token. To jedyny moment, kiedy możesz to zrobić.
![Ekran z wygenerowanym tokenem](/Materiały/zrzut-ap-token-wygenerowany.png)

6. Wprowadź token w ProFaku na zakładce "Dane urzędowe" na ekranie "Moja firma" dostępnym ze spisu kontrahentów. Upewnij się, że z listy rozwijanej jest wybrane to samo środowisko KSeF, w którym został wygenerowany token.
![Ekran "Dane urzędowe"](/Materiały/zrzut-dane-urzedowe.png)

## Ręczne wygenerowanie certyfikatu dostępowego

Alternatywnym mechanizmem uwierzytelniania w KSeF jest użycie certyfikatu. Certyfikat możesz wygenerować samodzielnie z Aplikacji Podatnika.

1. Zaloguj się do [Aplikacji Podatnika](https://ap.ksef.mf.gov.pl/)
2. Wybierz z menu "Certyfikaty" -> "Wnioskuj o certyfikat".
3. Wpisz dowolną nazwę certyfikatu i hasło spełniające podane wymagania. Kliknij "Generuj".
![Ekran wnioskowania o nowy certyfikat](/Materiały/zrzut-ap-certyfikat-nowy.png)

4. Zapisz wygenerowany klucz prywatny w wygodnym miejscu, na przykład na pulpicie. Plik powinien mieć rozszerzenie `.key`.
5. Na ekranie wnioskowania o certyfikat wybierz "Uwierzytelnianie w systemie KSeF". Jako datę ważności wskaż dzisiejszy dzień. Na certyfikacie nie masz możliwości wskazywania uprawnień - posiadacz certyfikatu będzie miał pełne uprawnienia do dostępu do KSeF.
![Ekran generowania certyfikatu](/Materiały/zrzut-ap-certyfikat-nowy-2.png)

6. Pobierz wygenerowany certyfikat i zapisz go w wygodnym miejscu, najlepiej tam, gdzie zapisany został plik z kluczem prywatnym. Certyfikat powinien mieć rozszerzenie `.crt`.
![Ekran pobierania certyfikatu](/Materiały/zrzut-ap-certyfikat-wygenerowany.png)

7. W ProFaku otwórz zakładkę "Dane urzędowe" na ekranie "Moja firma" w spisie kontrahentów, wybierz środowisko KSeF z listy rozwijanej i kliknij "Importuj certyfikat".
8. Wskaż plik klucza prywatnego, plik certyfikatu i użyte hasło.
9. ProFak wprowadzi do pola "Token KSeF" certyfikat wraz z kluczem. Zapisz zmiany. Możesz usunąć wygenerowane pliki lub zachować je w bezpiecznym miejscu. Nadal pozostają ważne i możesz ich użyć w przyszłości do ponownego podłączenia się do KSeF.

## Ręczne przesyłanie faktur

Jeśli nie chcesz, by ProFak bezpośrednio komunikował się z KSeFem, ale nadal chcesz korzystać z niego do wystawiania faktur, masz możliwość generowania w programie plików XML z fakturami sprzedażowymi i ręcznego ładowania ich do KSeF oraz ręcznego pobierania plików XML z fakturami zakupowymi i wczytywania ich do ProFaka.

Z poziomu spisu faktur, z menu akcji wybierz "Generuj XML KSeF" aby wytworzyć plik zawierający dane wybranej faktury. Taki plik możesz wczytać do Aplikacji Podatnika za pomocą polecenia "Faktury" -> "Wczytaj fakturę".

![Ekran wczytywania faktury do KSeF](/Materiały/zrzut-ap-wczytaj-fakture.png)

Przed przesłaniem faktury masz możliwość podejrzenia jej wizualizacji i wprowadzenia ewentualnych zmian.

Pamiętaj, że momentem wystawienia faktury jest moment przesłania jej do KSeF. Jeśli ręcznie przesyłasz pliki XML, rób to tego samego dnia, w którym wprowadzasz fakturę w ProFaku.