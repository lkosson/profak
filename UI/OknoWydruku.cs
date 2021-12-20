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
			reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
			reportViewer.Dock = DockStyle.Fill;
			reportViewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
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
	}
}
