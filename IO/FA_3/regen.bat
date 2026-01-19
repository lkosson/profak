@echo off
rem https://github.com/mganss/XmlSchemaClassGenerator
..\XmlSchemaClassGenerator\XmlSchemaClassGenerator.Console.exe ^
--nullable ^
--namespace http://crd.gov.pl/wzor/2025/06/25/13775/=ProFak.IO.FA_3 ^
--namespace http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2022/01/05/eD/DefinicjeTypy/=ProFak.IO.FA_3.DefinicjeTypy ^
--namingScheme=Direct http://crd.gov.pl/wzor/2025/06/25/13775/schemat.xsd