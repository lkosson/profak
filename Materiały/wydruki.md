# Zmiana szablonów wydruków

ProFak umożliwia podmianę standardowych szablonów wydruków na własne bez potrzeby rekompilacji programu. Wydruki są zaprojektowane przy użyciu SQL Server Reporting Services w wersji klienckiej (ReportViewer). Aby je zmodyfikować:

1. Pobierz i zainstaluj [Microsoft Report Builder](https://download.microsoft.com/download/5/e/b/5eb40744-dc0a-47c0-8b0a-1830e74d3c23/ReportBuilder.msi). Ewentualnie, jeśli masz zainstalowane Visual Studio, pobierz i zainstaluj rozszerzenie [Microsoft RDLC Report Designer](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftRdlcReportDesignerforVisualStudio-18001).

2. Pobierz aktualne szablony wydruków (pliki `.rdlc`) z katalogu [Wydruki](/Wydruki).

3. Jeśli używasz *Report Buildera*, zmień rozszerzenia plików z `.rdlc` na `.rdl`. Jeśli używasz *Visual Studio*, nie zmieniaj rozszerzeń.

4. Po wprowadzeniu i zapisaniu zmian, przywróć rozszerzenie `.rdlc` i skopiuj zmodyfikowane szablony do katalogu, w którym jest zainstalowany ProFak. Pliki muszą mieć takie same nazwy jak w repozytorium. Nie musisz restartować programu by zmiany zaczęły obowiązywać.

Korzystając z powyższej procedury możesz zmienić dowolny wydruk w programie:
 * `EwidencjaPrzychodow.rdlc` - Formularz miesięcznej ewidencji przychodów (ryczałt)
 * `Faktura.rdlc` - Faktura - część nagłówkowa i stopka
 * `FakturaPozycje.rdlc` - Specyfikacja faktury nieVATowskiej
 * `FakturaPozycjeRabat.rdlc` - Specyfikacja faktury nieVATowskiej z pozycjami z rabatem
 * `FakturaPozycjeVat.rdlc` - Specyfikacja faktury VAT
 * `FakturaPozycjeVatRabat.rdlc` - Specyfikacja faktury VAT z pozycjami z rabatem
 * `PKPiR.rdlc` - Formularz miesięcznej podatkowej księgi przychodów i rozchodów (liniowy lub skala)

Jeśli korzystasz z własnych szablonów wydruków, zwróć szczególną uwagę w momencie wgrywania aktualizacji programu, jeśli w liście zmian jest mowa o zmodyfikowanych wydrukach. Twoje zmiany powinny nadal działać, ale możesz chcieć uwzględnić na nich nowości z oficjalnej wersji.