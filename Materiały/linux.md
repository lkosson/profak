# Wersja dla Linuxa

Począwszy od wydania 1.18.2, ProFak jest dostępny także jako natywna aplikacja linuksowa z interfejsem użytkownika zbudowanym w Avalonia UI i wydrukami generowanymi przez Quest PDF, zamiast używanych w wersji windowsowej WinForms i Reporting Services. Funkcjonalnie wersje nie różnią się między sobą, choć mogą występować drobne rozbieżności w interfejsie użytkownika.

Wersja linuksowa powinna działać bez instalowania dodatkowych zależności na zdecydowanej większości desktopowych dystrybucji. Jakby jednak brakowało jakichś bibliotek, [oficjalne wymagania .NETa](https://learn.microsoft.com/en-us/dotnet/core/install/linux-debian?tabs=dotnet10#dependencies).

Ze względu na użycie innego mechanizmu generowania wydruków, [własne szablony RDLC](/Materiały/wydruki.md) nie działają w wersji linuksowej.