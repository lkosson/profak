using Microsoft.EntityFrameworkCore;
using ProFak.DB;
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
	partial class BazyDanych : UserControl, IKontrolkaZKontekstem
	{
		public Kontekst Kontekst { get; set; }
		private bool gotowy;

		public BazyDanych()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			Wypelnij();
			base.OnLoad(e);
		}

		private void Wypelnij()
		{
			if (String.IsNullOrEmpty(Baza.Sciezka))
			{
				comboBoxPlik.Text = "(baza tymczasowa)";
				comboBoxPlik.Enabled = false;
				buttonUtworzKopie.Enabled = false;
				buttonPrzywrocKopie.Enabled = false;
				return;
			}
			gotowy = false;
			var plik = new FileInfo(Baza.Sciezka);
			comboBoxPlik.Text = plik.FullName;
			textBoxRozmiar.Text = plik.Length.ToString("#,##0");
			textBoxDataModyfikacji.Text = plik.LastWriteTime.ToString("d MMMM yyyy, H:mm:ss");
			comboBoxPlik.Items.Clear();
			comboBoxPlik.Items.Add(Baza.PublicznaSciezka);
			comboBoxPlik.Items.Add(Baza.PrywatnaSciezka);
			comboBoxPlik.Items.Add(Baza.LokalnaSciezka);
			if (Baza.Sciezka == Baza.PublicznaSciezka) comboBoxPlik.SelectedIndex = 0;
			if (Baza.Sciezka == Baza.PrywatnaSciezka) comboBoxPlik.SelectedIndex = 1;
			if (Baza.Sciezka == Baza.LokalnaSciezka) comboBoxPlik.SelectedIndex = 2;
			gotowy = true;
		}

		private void buttonPrzenies_Click(object sender, EventArgs e)
		{
			if (!gotowy) return;
			var staryPlik = Baza.Sciezka;
			var nowyPlik = comboBoxPlik.Text;
			if (File.Exists(nowyPlik))
			{
				MessageBox.Show($"Plik {nowyPlik} już istnieje. Przed przeniesieniem bazy przenieś go w inne miejsce.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (MessageBox.Show("Czy na pewno chcesz przenieść bazę danych do nowego miejsca?", "ProFak", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
			{
				gotowy = false;
				comboBoxPlik.Text = staryPlik;
				gotowy = true;
				return;
			}
			var katalog = Path.GetDirectoryName(nowyPlik);
			Directory.CreateDirectory(katalog);
			File.Move(staryPlik, nowyPlik);
			Baza.Sciezka = nowyPlik;
			Baza.ZapiszOdnosnikDoBazy();
			Wypelnij();
		}

		private void buttonUtworzKopie_Click(object sender, EventArgs e)
		{
			saveFileDialogBackup.FileName = $"profak-{DateTime.Now:yyyyMMdd}.probak";
			if (saveFileDialogBackup.ShowDialog() != DialogResult.OK) return;
			try
			{
				File.Copy(Baza.Sciezka, saveFileDialogBackup.FileName);
				MessageBox.Show("Kopia bazy danych została zapisana.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception exc)
			{
				using var okno = new OknoBledu(exc);
				okno.ShowDialog();
			}
		}

		private void buttonPrzywrocKopie_Click(object sender, EventArgs e)
		{
			if (openFileDialogBackup.ShowDialog() != DialogResult.OK) return;

			if (MessageBox.Show("Dotychczasowe dane zostaną nadpisane. Czy na pewno chcesz kontynuować?", "ProFak", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
			var bazaRatunkowa = Baza.Sciezka + "-bak";
			if (File.Exists(bazaRatunkowa)) File.Delete(bazaRatunkowa);
			try
			{
				File.Move(Baza.Sciezka, bazaRatunkowa);
				File.Copy(openFileDialogBackup.FileName, Baza.Sciezka);
				using var baza = new DB.Baza();
				baza.Database.Migrate();
			}
			catch (Exception exc)
			{
				if (File.Exists(bazaRatunkowa))
				{
					if (File.Exists(Baza.Sciezka)) File.Delete(Baza.Sciezka);
					File.Move(bazaRatunkowa, Baza.Sciezka);
				}

				using var okno = new OknoBledu(exc);
				okno.ShowDialog();
				return;
			}
			MessageBox.Show("Baza danych została odtworzona.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
			Wypelnij();
		}
	}
}
