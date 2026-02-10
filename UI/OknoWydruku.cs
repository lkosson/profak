using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class OknoWydruku : Form
	{
		private readonly Microsoft.Reporting.WinForms.ReportViewer reportViewer;
		private readonly Wydruki.Wydruk wydruk;

		public OknoWydruku(Wydruki.Wydruk wydruk)
		{
			Icon = GlowneOkno.Ikona;
			WindowState = FormWindowState.Maximized;
			ShowInTaskbar = false;
			Text = "ProFak - Wydruk";
			reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
			reportViewer.Dock = DockStyle.Fill;
			if (Wyglad.DomyslnyPodgladStrony) reportViewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
			reportViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
			Controls.Add(reportViewer);
			this.wydruk = wydruk;
		}

		protected override void OnLoad(EventArgs e)
		{
			wydruk.Przygotuj(reportViewer.LocalReport);
			reportViewer.RefreshReport();
			base.OnLoad(e);
		}

		public static void ZaladujWstepnieReportViewer()
		{
			_ = Task.Run(async delegate
			{
				try
				{
					await Task.Delay(TimeSpan.FromSeconds(3));
					using var kontekst = new Kontekst();
					var faktura = kontekst.Baza.Faktury.Where(e => e.Rodzaj == DB.RodzajFaktury.Sprzedaż).OrderByDescending(e => e.Id).FirstOrDefault();
					if (faktura == null) return;
					var wydruk = new Wydruki.Faktura(kontekst.Baza, [faktura.Ref]);
					using var okno = new OknoWydruku(wydruk);
					wydruk.Przygotuj(okno.reportViewer.LocalReport);
					okno.reportViewer.RefreshReport();
				}
				catch
				{
					// ignored
				}
			});
		}
	}
}
