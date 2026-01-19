@echo off
rem https://github.com/mganss/XmlSchemaClassGenerator
rem https://www.gov.pl/web/kas/struktury-jpk
..\XmlSchemaClassGenerator\XmlSchemaClassGenerator.Console.exe ^
--nullable ^
--namespace http://jpk.mf.gov.pl/wzor/2022/02/17/02171/=ProFak.IO.JPK_FA ^
--namespace http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2018/08/24/eD/DefinicjeTypy/=ProFak.IO.JPK_FA.DefinicjeTypy ^
--namingScheme=Direct ^
Schemat_JPK_FA(4)_v1-0.xsd