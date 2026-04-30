using Jint;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ProFak.IO.KSEFPDF;

public class Generator
{
	private static Engine? engine;

	public static byte[] ZbudujPDF(string ksefXml, string ksefNumer, string ksefUrl, CancellationToken cancellationToken)
	{
		if (engine == null)
		{
			engine = new Engine();
			engine.SetValue("logImpl", new Action<object>(v => { Console.WriteLine(v); }));

			var asm = Assembly.GetExecutingAssembly();

			void Wczytaj(string plikSkryptu)
			{
				using var skrypt = asm.GetManifestResourceStream(plikSkryptu) ?? throw new ApplicationException($"Błąd wewnętrzny: nie znaleziono skryptu {plikSkryptu}.");
				using var streamReader = new StreamReader(skrypt);
				var trescSkryptu = streamReader.ReadToEnd();
				engine!.Evaluate(trescSkryptu);
			}

			Wczytaj("ProFak.IO.KSEFPDF.ksef-fe-invoice-converter-wrapper.cjs");
			Wczytaj("ProFak.IO.KSEFPDF.ksef-fe-invoice-converter.umd.cjs");
		}
		engine.SetValue("xml", ksefXml);
		engine.SetValue("ksefNumer", ksefNumer);
		engine.SetValue("ksefUrl", ksefUrl);
		var wynik = engine.Evaluate("exports.generateInvoice(xml, { \"nrKSeF\": ksefNumer, \"qrCode\": ksefUrl }, \"base64\");");
		var pdfb64 = wynik.UnwrapIfPromise(cancellationToken);
		var pdf = Convert.FromBase64String(pdfb64.AsString());
		return pdf;
	}
}
