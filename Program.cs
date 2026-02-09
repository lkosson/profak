using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using ProFak.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak
{
	public static class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			try
			{
				Wyglad.DostosujDoWine();
				Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
				Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("pl-PL");
				Baza.UstalSciezkeBazy();
#if !SQLSERVER
				if (!PierwszyStartBaza.Uruchom()) return;
#endif
				Baza.Przygotuj();

				if (args.Length > 0)
				{
					if (args[0] == "xsd") Wydruki.GeneratorXSD.Utworz();
					if (args[0] == "sql")
					{
						using var kontekst = new UI.Kontekst();
						using var dialog = new UI.Dialog("ProFak", new UI.EkranSQL() { Kontekst = kontekst }, kontekst) { CzyPrzyciskiWidoczne = false };
						dialog.ShowDialog();
					}
					if (args[0] == "db")
					{
						using var kontekst = new UI.Kontekst();
						using var dialog = new UI.Dialog("ProFak", new UI.BazyDanych() { Kontekst = kontekst }, kontekst) { CzyPrzyciskiWidoczne = false };
						dialog.ShowDialog();
					}
					return;
				}

				using (var kontekst = new UI.Kontekst())
				{
					var podmiot = kontekst.Baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.CzyPodmiot);
					if (podmiot == null)
					{
						MessageBox.Show("Przed rozpoczêciem korzystania z programu nale¿y uzupe³niæ dane firmy.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
						var _ = Enumerable.Empty<DB.Kontrahent>();
						new UI.MojaFirmaAkcja().Uruchom(kontekst, ref _);
					}
				}

				Application.Run(new UI.GlowneOkno());
			}
			catch (Exception exc)
			{
				OknoBledu.Pokaz(exc);
			}
		}
	}
}
