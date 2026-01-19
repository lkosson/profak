@echo off
rem https://github.com/mganss/XmlSchemaClassGenerator
..\XmlSchemaClassGenerator\XmlSchemaClassGenerator.Console.exe ^
--nullable ^
--namespace http://crd.gov.pl/wzor/2023/06/29/12648/=ProFak.IO.FA_2 ^
--namespace http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2022/01/05/eD/DefinicjeTypy/=ProFak.IO.FA_2.DefinicjeTypy ^
--namingScheme=Direct http://crd.gov.pl/wzor/2023/06/29/12648/schemat.xsd