using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	public partial class OknoBledu : Form
	{
		public OknoBledu()
		{
			InitializeComponent();
			ShowInTaskbar = false;
		}

		public OknoBledu(Exception exc)
			: this()
		{
			textBoxWyjatek.Text = exc.ToString();
		}

		private void linkLabelURL_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "https://github.com/lkosson/profak/issues" });
		}

		public static void Pokaz(Exception exc)
		{
			if (exc.GetType() == typeof(ApplicationException))
			{
				MessageBox.Show(exc.Message, "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else if (exc is OperationCanceledException)
			{
				MessageBox.Show("Operacja została przerwana.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
#if SQLSERVER
			else if (exc is Microsoft.Data.SqlClient.SqlException se && se.Number == 1222)
			{
				MessageBox.Show("Rekord jest modyfikowany na innym stanowisku.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
#else
			else if (exc is Microsoft.Data.Sqlite.SqliteException se && se.SqliteErrorCode == 5)
			{
				MessageBox.Show("Baza danych jest używana na innym stanowisku.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
#endif
			else
			{
				using var okno = new OknoBledu(exc);
				okno.ShowDialog();
			}
		}
	}
}
