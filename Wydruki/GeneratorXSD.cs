using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProFak.Wydruki
{
	class GeneratorXSD
	{
		public static void Utworz()
		{
			var types = new[] { typeof(FakturaDTO) };
			var xri = new XmlReflectionImporter();
			var xss = new XmlSchemas();
			var xse = new XmlSchemaExporter(xss);
			foreach (var type in types)
			{
				var xtm = xri.ImportTypeMapping(type);
				xse.ExportTypeMapping(xtm);
			}
			using var sw = new StreamWriter("../../../Wydruki/Wydruki.xsd", false, Encoding.UTF8);
			for (int i = 0; i < xss.Count; i++)
			{
				var xs = xss[i];
				xs.Id = "Wydruki";
				xs.Write(sw);
			}
		}
	}
}
