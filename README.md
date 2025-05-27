# Pasjans na Gigathon 2025 etap 2

## Wymagania

Do kompilacji i uruchomienia potrzebne jest `.NET SDK 8.0.410` dostępny do pobrania tutaj:  
https://dotnet.microsoft.com/en-us/download/dotnet/8.0

## Uruchamianie

Trzeba upewnić się, że okienko terminala jest wystarczająco duże, by gra była poprawnie wyświetlana (większe niż domyślny rozmiar wierszu polecenia).  

### Windows

Na systemach windows można otworzyć project w Visual Studio i uruchomić grę z tamtąd.  

### Inne systemy

W terminalu będąc w katalogu głównym projektu:  

```bash
dotnet run --project Solitare
```

## Testy jednostkowe

```bash
dotnet test
```

# Nawigacja 

## Menu główne

Przy uruchomienu gry pojawi się ekran startowy.  
Można nawigować strzałkami na klawiaturze prawo `→`, lewo `←`, góra `↑`, dół `↓`,  
Można wybrać poziom trudności gry poprzez podświetlenie napisu `Łatwy` lub `Trudny` i kliknięcie spacji.  
Można zmienić ziarno gry (seed) co sprawi że wygeneruje się inna losowa rozgrywka.  
Domyślne ziarno (`123`) zawiera grę którą da się wygrać (przynajmniej na Łatwym), powidzenia ;)  
Kliknięcie przycisku `Rozpocznij grę` rozpoczyna grę.
Można nacisnąć `Esc` aby wyjść z aplikacji.

## Menu Gry

Obecnie podświetloną kartę można zmieniać strzałkami na klawiaturze prawo `→`, lewo `←`, góra `↑`, dół `↓`.  
Po wybraniu właściwej karty należy nacisnąć `spację` lub `enter` (jak wygodniej) aby ją wybrać.  
Potem należy podświetlić miejsce, gdzie chcemy żeby dana karta wylądowawla.  
Naciskamy znowu `spację` lub `enter` i jeżeli ruch jest prawidłowy, karta się przesunie w dane miejsce.  
Jeżeli ruch był nie prawidłowy, na górze aplikacji pojawi się napis z opisem dlaczego ten ruch był nieprawidłowy.  

TIP: Aby szybko wybrać kartę na szczycie talii, można kliknąć skrót klawiszowy odpowiadający litercę nad talią.

TIP: Na systemach Linux, w Powershell'u i w terminalu Visual Studio (czyli oprócz zwykłego cmd) można klikać na karty myszką i tak je wybierać i poruszać.

Na górnym pasku są widoczne:
- Przycisk `Wyjdż do menu głównego` (skrót klawiszowy `Esc`)
- Przycisk `Cofnij` (skrót klawiszowy `z`)
- Liczna wykonanych ruchów

Po wygranej, czyli zebranie po 13 kart w każdym kolorze (od Asa do Króla) na stosie finałowym, pojawi się okienko komunikujące o wygranej i liczbie ruchów.  
Wynik wygranej jest zapisywany w tabeli najlepszych wyników w menu głównym.

