@echo off
rem https://github.com/mganss/XmlSchemaClassGenerator
rem https://www.gov.pl/web/kas/struktury-jpk
..\XmlSchemaClassGenerator\XmlSchemaClassGenerator.Console.exe ^
--nullable ^
--namespace http://jpk.mf.gov.pl/wzor/2024/10/30/10302/=ProFak.IO.JPK_PKPIR ^
--namespace http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2022/01/05/eD/DefinicjeTypy/=ProFak.IO.JPK_PKPIR.DefinicjeTypy ^
--namespace http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2022/01/05/eD/KodyUrzedowSkarbowych/=ProFak.IO.JPK_PKPIR.KodyUrzedowSkarbowych ^
--namingScheme=Direct ^
Struktura_Schemat_JPK_PKPIR(3)_v1-0.xsd