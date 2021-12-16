using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	public partial class PierwszyStartBaza : Form
	{
		private const string BazaDemo = "(demo)";
		private string bazaZrodlowa;
		private string bazaDocelowa;

		public PierwszyStartBaza()
		{
			InitializeComponent();
		}

		private void buttonDalej_Click(object sender, EventArgs e)
		{
			if (radioButtonNowaPrywatnaBaza.Checked)
			{
				bazaZrodlowa = null;
				bazaDocelowa = DB.Baza.PrywatnaSciezka;
			}
			else if (radioButtonNowaPublicznaBaza.Checked)
			{
				bazaZrodlowa = null;
				bazaDocelowa = DB.Baza.PublicznaSciezka;
			}
			else if (radioButtonNowaLokalnaBaza.Checked)
			{
				bazaZrodlowa = null;
				bazaDocelowa = DB.Baza.LokalnaSciezka;
			}
			else if (radioButtonZewnetrznaBaza.Checked)
			{
				if (openFileDialogBaza.ShowDialog() != DialogResult.OK) return;
				bazaZrodlowa = openFileDialogBaza.FileName;
				bazaDocelowa = null;
			}
			else if (radioButtonBazaDemo.Checked)
			{
				bazaZrodlowa = BazaDemo;
				bazaDocelowa = null;
			}
			else if (radioButtonOdtworzKopie.Checked)
			{
				if (openFileDialogBackup.ShowDialog() != DialogResult.OK) return;
				bazaZrodlowa = openFileDialogBackup.FileName;
				bazaDocelowa = DB.Baza.PrywatnaSciezka;
			}
			else return;

			buttonDalej.Enabled = false;
			progressBar.Visible = true;
			labelStatus.Visible = true;
			labelStatus.Text = "Inicjalizacja";
			backgroundWorker.RunWorkerAsync();
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			backgroundWorker.ReportProgress(0, "Weryfikacja bazy źródłowej");
			var zrodloJestPlikiem = !String.IsNullOrEmpty(bazaZrodlowa) && bazaZrodlowa != BazaDemo;
			if (zrodloJestPlikiem && !File.Exists(bazaZrodlowa)) throw new ApplicationException($"Nie znaleziono pliku \"{bazaZrodlowa}\" z bazą danych.");

			if (String.IsNullOrEmpty(bazaDocelowa))
			{
				if (zrodloJestPlikiem) DB.Baza.Sciezka = bazaZrodlowa;
				else DB.Baza.Sciezka = null;
			}
			else
			{
				backgroundWorker.ReportProgress(0, "Przygotowanie miejsca na bazę docelową");
				if (File.Exists(bazaDocelowa))
				{
					var kopiaBazyDocelowej = bazaDocelowa + "-bak";
					if (File.Exists(kopiaBazyDocelowej))
					{
						backgroundWorker.ReportProgress(0, "Kasowanie starej kopii zapasowej");
						File.Delete(kopiaBazyDocelowej);
					}
					backgroundWorker.ReportProgress(0, "Tworzenie kopii zapasowej");
					File.Move(bazaDocelowa, kopiaBazyDocelowej);
				}

				if (zrodloJestPlikiem)
				{
					backgroundWorker.ReportProgress(0, "Kopiowanie bazy");
					File.Copy(bazaZrodlowa, bazaDocelowa);
				}

				DB.Baza.Sciezka = bazaDocelowa;
			}

			backgroundWorker.ReportProgress(0, "Podłączanie bazy");
			using var db = new DB.Baza();

			backgroundWorker.ReportProgress(0, "Aktualizacja struktury");
			db.Database.Migrate();

			backgroundWorker.ReportProgress(0, "Tworzenie danych startowych");
			DB.DaneStartowe.Zaladuj(db);

			if (bazaZrodlowa == BazaDemo)
			{
				backgroundWorker.ReportProgress(0, "Tworzenie danych demo");
				DB.DaneDemo.Zaladuj(db);
			}

			backgroundWorker.ReportProgress(0, "Baza gotowa");
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error == null)
			{
				DialogResult = DialogResult.OK;
				Close();
				return;
			}

			if (e.Error is ApplicationException ae) MessageBox.Show(ae.Message, "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			else
			{
				var okno = new OknoBledu(e.Error);
				okno.ShowDialog();
			}
			buttonDalej.Enabled = true;
			progressBar.Visible = false;
			labelStatus.Visible = false;
		}

		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			labelStatus.Text = (string)e.UserState;
		}
	}
}
