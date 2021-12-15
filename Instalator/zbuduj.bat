@echo off

cd ..

dotnet publish -r win-x86 -c release -o bin\Publish
if errorlevel 1 goto :blad

cd bin\Publish

attrib +r *.dll
attrib +r *.json
attrib +r ProFak.exe
attrib +r *.probak
attrib +r pl\*.dll
del /s /q *.* 2> nul >nul
attrib /s -r *.*

cd ..\..\Instalator

heat dir ..\bin\Publish -sreg -srd -sfrag -gg -template fragment -cg Pliki -var var.Zrodlo -dr KatalogProgramu -out ProFak-pliki.wxs
if errorlevel 1 goto :blad

candle.exe ProFak.wxs ProFak-pliki.wxs -dZrodlo=..\bin\Publish\
if errorlevel 1 goto :blad

light.exe ProFak.wixobj ProFak-pliki.wixobj -out ..\bin\Install\ProFak.msi
if errorlevel 1 goto :blad

goto :koniec

:blad
pause
:koniec
