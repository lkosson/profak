@echo off
rem https://github.com/mganss/XmlSchemaClassGenerator
..\XmlSchemaClassGenerator\XmlSchemaClassGenerator.Console.exe ^
--nullable ^
--namespace http://crd.gov.pl/wzor/2025/12/19/14090/=ProFak.IO.JPK_V7M ^
--namespace http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2023/09/06/eD/KodyKrajow/=ProFak.IO.JPK_V7M.KodyKrajow ^
--namespace http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2022/01/05/eD/KodyUrzedowSkarbowych/=ProFak.IO.JPK_V7M.KodyUrzedowSkarbowych ^
--namespace http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2022/09/13/eD/DefinicjeTypy/=ProFak.IO.JPK_V7M.DefinicjeTypy ^
--namingScheme=Direct http://crd.gov.pl/wzor/2025/12/19/14090/schemat.xsd
