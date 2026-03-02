using Jint;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ProFak.IO.KSEFPDF;

public class Generator
{
	private static Engine? engine;

	public static byte[] ZbudujPDF(string ksefXml, string ksefNumer, CancellationToken cancellationToken)
	{
		if (engine == null)
		{
			var asm = Assembly.GetCallingAssembly();
			using var skrypt = asm.GetManifestResourceStream("ProFak.IO.KSEFPDF.ksef-fe-invoice-converter.umd.cjs") ?? throw new ApplicationException("Błąd wewnętrzny: nie znaleziono skryptu ksef-pdf-generator.");
			using var streamReader = new StreamReader(skrypt);
			var trescSkryptu = streamReader.ReadToEnd();
			engine = new Engine();
			engine.Evaluate(trescSkryptu);
		}
		engine.SetValue("xml", ksefXml);
		engine.SetValue("ksefNumer", ksefNumer);
		var wynik = engine.Evaluate(@"exports.generateInvoiceFromString(xml, ksefNumer);");
		var pdfb64 = wynik.UnwrapIfPromise(cancellationToken);
		var pdf = Convert.FromBase64String(pdfb64.AsString());
		return pdf;
	}
}
