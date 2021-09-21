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
				bazaZrodlowa = DB.ProFakContext.BazaStartowa;
				bazaDocelowa = DB.ProFakContext.PrywatnaSciezka;
			}
			else if (radioButtonNowaPublicznaBaza.Checked)
			{
				bazaZrodlowa = DB.ProFakContext.BazaStartowa;
				bazaDocelowa = DB.ProFakContext.PublicznaSciezka;
			}
			else if (radioButtonNowaLokalnaBaza.Checked)
			{
				bazaZrodlowa = DB.ProFakContext.BazaStartowa;
				bazaDocelowa = DB.ProFakContext.LokalnaSciezka;
			}
			else if (radioButtonZewnetrznaBaza.Checked)
			{
				if (openFileDialogBaza.ShowDialog() != DialogResult.OK) return;
				bazaZrodlowa = openFileDialogBaza.FileName;
				bazaDocelowa = null;
			}
			else if (radioButtonBazaDemo.Checked)
			{
				bazaZrodlowa = DB.ProFakContext.BazaDemo;
				bazaDocelowa = null;
			}
			else if (radioButtonOdtworzKopie.Checked)
			{
				if (openFileDialogBackup.ShowDialog() != DialogResult.OK) return;
				bazaZrodlowa = openFileDialogBackup.FileName;
				bazaDocelowa = DB.ProFakContext.PrywatnaSciezka;
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
			if (!File.Exists(bazaZrodlowa)) throw new ApplicationException($"Nie znaleziono pliku \"{bazaZrodlowa}\" z bazą danych.");

			if (String.IsNullOrEmpty(bazaDocelowa))
			{
				DB.ProFakContext.Sciezka = bazaZrodlowa;
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

				backgroundWorker.ReportProgress(0, "Kopiowanie bazy");
				File.Copy(bazaZrodlowa, bazaDocelowa);

				DB.ProFakContext.Sciezka = bazaDocelowa;
			}

			backgroundWorker.ReportProgress(0, "Weryfikacja bazy");
			using var db = new DB.ProFakContext();
			if (!db.Database.CanConnect()) throw new ApplicationException("Nie udało się otworzyć bazy danych.");

			backgroundWorker.ReportProgress(0, "Aktualizacja struktury");
			db.Database.Migrate();

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
			else MessageBox.Show("W trakcie przygotowywania bazy danych wystąpił nieobsłużony błąd.\n\n" + e.Error.Message, "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
