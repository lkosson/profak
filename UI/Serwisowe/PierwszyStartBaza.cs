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
		private const string ZnacznikPierwszegoUruchomienia = "pierwsze-uruchomienie.txt";
		private const string BazaDemo = "(demo)";
		private string? bazaZrodlowa;
		private string? bazaDocelowa;

		public PierwszyStartBaza()
		{
			InitializeComponent();
			radioButtonNowaPrywatnaBaza.Enabled = CzySciezkaDostepna(DB.Baza.PrywatnaSciezka);
			radioButtonNowaPublicznaBaza.Enabled = CzySciezkaDostepna(DB.Baza.PublicznaSciezka);
			radioButtonNowaLokalnaBaza.Enabled = CzySciezkaDostepna(DB.Baza.LokalnaSciezka);
		}

		private bool CzySciezkaDostepna(string sciezka)
		{
			try
			{
				if (File.Exists(sciezka))
				{
					using var f = new FileStream(sciezka, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
					return true;
				}
				else
				{
					Directory.CreateDirectory(Path.GetDirectoryName(sciezka)!);
					using var f = new FileStream(sciezka, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 4096, FileOptions.DeleteOnClose);
					return true;
				}
			}
			catch
			{
				return false;
			}
		}

		private void buttonDalej_Click(object? sender, EventArgs e)
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

			if (File.Exists(bazaDocelowa) && MessageBox.Show($"Baza {bazaDocelowa} już istnieje. Czy na pewno chcesz ją nadpisać i utworzyć w jej miejsce pustą bazę?\n\nTen proces jest nieodwracalny i STRACISZ WSZYSTKIE ISTNIEJĄCE DANE.", "ProFak", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;

			buttonDalej.Enabled = false;
			progressBar.Visible = true;
			labelStatus.Visible = true;
			labelStatus.Text = "Inicjalizacja";
			backgroundWorker.RunWorkerAsync();
		}

		private void backgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
		{
			backgroundWorker.ReportProgress(0, "Weryfikacja bazy źródłowej");
			var zrodloJestPlikiem = !String.IsNullOrEmpty(bazaZrodlowa) && bazaZrodlowa != BazaDemo;
			if (zrodloJestPlikiem && !File.Exists(bazaZrodlowa)) throw new ApplicationException($"Nie znaleziono pliku \"{bazaZrodlowa}\" z bazą danych.");

			if (String.IsNullOrEmpty(bazaDocelowa))
			{
				if (zrodloJestPlikiem)
				{
					DB.Baza.Sciezka = bazaZrodlowa;
					DB.Baza.ZapiszOdnosnikDoBazy();
				}
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

				var katalogDocelowy = Path.GetDirectoryName(bazaDocelowa)!;
				Directory.CreateDirectory(katalogDocelowy);

				if (zrodloJestPlikiem)
				{
					backgroundWorker.ReportProgress(0, "Kopiowanie bazy");
					// Tu nie potrzeba korzystać z mechanizmów SQLite'a - plik źródłowy nie jest aktywną bazą
					File.Copy(bazaZrodlowa!, bazaDocelowa);
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

		private void backgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error == null)
			{
				DialogResult = DialogResult.OK;
				Close();
				return;
			}

			OknoBledu.Pokaz(e.Error);
			buttonDalej.Enabled = true;
			progressBar.Visible = false;
			labelStatus.Visible = false;
		}

		private void backgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
		{
			labelStatus.Text = (string?)e.UserState ?? "";
		}

		public static bool Uruchom()
		{
			var pierwszeUruchomienieWersjiPrzenosnej = false;
			var plikPierwszegoUruchomienia = Path.Combine(DB.Baza.LokalnyKatalog, ZnacznikPierwszegoUruchomienia);
			if (!File.Exists(plikPierwszegoUruchomienia))
			{
				try
				{
					File.WriteAllText(plikPierwszegoUruchomienia, "Ten plik jest znacznikiem, że program był już uruchamiany z tego katalogu.\r\nUsuń go, jeśli chcesz móc ponownie wybrać lokalizację bazy danych.");
					pierwszeUruchomienieWersjiPrzenosnej = true;
				}
				catch
				{
				}
			}

			if (File.Exists(DB.Baza.Sciezka))
			{
				if (!pierwszeUruchomienieWersjiPrzenosnej) return true;
				var ret = MessageBox.Show($"Została znaleziona istniejąca baza danych: {DB.Baza.Sciezka}\n\nCzy chcesz jej użyć?", "ProFak", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
				if (ret == DialogResult.Yes) return true;
				if (ret == DialogResult.Cancel)
				{
					File.Delete(plikPierwszegoUruchomienia);
					return false;
				}
			}

			using var pierwszyStart = new PierwszyStartBaza();
			var ok = pierwszyStart.ShowDialog() == DialogResult.OK;
			if (pierwszeUruchomienieWersjiPrzenosnej && (!ok || String.IsNullOrEmpty(DB.Baza.Sciezka))) File.Delete(plikPierwszegoUruchomienia);
			return ok;
		}
	}
}
