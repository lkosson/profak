@echo off
rem https://github.com/mganss/XmlSchemaClassGenerator
rem https://www.gov.pl/web/kas/struktury-jpk
..\XmlSchemaClassGenerator\XmlSchemaClassGenerator.Console.exe ^
--nullable ^
--namespace http://jpk.mf.gov.pl/wzor/2024/10/30/10301/=ProFak.IO.JPK_EWP ^
--namespace http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2022/01/05/eD/DefinicjeTypy/=ProFak.IO.JPK_EWP.DefinicjeTypy ^
--namespace http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2022/01/05/eD/KodyUrzedowSkarbowych/=ProFak.IO.JPK_EWP.KodyUrzedowSkarbowych ^
--namingScheme=Direct ^
Struktura_Schemat_JPK_EWP(4)_v1-0.xsd