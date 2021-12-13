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
			gotowy = false;
			var plik = new FileInfo(Baza.Sciezka);
			textBoxPlik.Text = plik.FullName;
			textBoxRozmiar.Text = plik.Length.ToString("#,##0");
			textBoxDataModyfikacji.Text = plik.LastWriteTime.ToString("d MMMM yyyy, H:mm:ss");
			comboBoxKatalog.Items.Clear();
			comboBoxKatalog.Items.Add(Baza.PublicznaSciezka);
			comboBoxKatalog.Items.Add(Baza.PrywatnaSciezka);
			comboBoxKatalog.Items.Add(Baza.LokalnaSciezka);
			if (Baza.Sciezka == Baza.PublicznaSciezka) comboBoxKatalog.SelectedIndex = 0;
			if (Baza.Sciezka == Baza.PrywatnaSciezka) comboBoxKatalog.SelectedIndex = 1;
			if (Baza.Sciezka == Baza.LokalnaSciezka) comboBoxKatalog.SelectedIndex = 2;
			gotowy = true;
		}

		private void comboBoxKatalog_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!gotowy) return;
			var staryPlik = Baza.Sciezka;
			var nowyPlik = comboBoxKatalog.Text;
			if (File.Exists(nowyPlik))
			{
				MessageBox.Show($"Plik {nowyPlik} już istnieje. Przed przeniesieniem bazy przenieś go w inne miejsce.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (MessageBox.Show("Czy na pewno chcesz przenieść bazę danych do nowego miejsca?", "ProFak", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
			{
				gotowy = false;
				comboBoxKatalog.Text = staryPlik;
				gotowy = true;
				return;
			}
			var katalog = Path.GetDirectoryName(nowyPlik);
			Directory.CreateDirectory(katalog);
			File.Move(staryPlik, nowyPlik);
			Baza.Sciezka = nowyPlik;
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
				MessageBox.Show($"Wystąpił nieobsłużony błąd. Uruchom ponownie program i spróbuj ponownie wykonać operację.\n\n{exc}", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			catch
			{
				if (File.Exists(bazaRatunkowa))
				{
					if (File.Exists(Baza.Sciezka)) File.Delete(Baza.Sciezka);
					File.Move(bazaRatunkowa, Baza.Sciezka);
				}
			}
			MessageBox.Show("Baza danych została odtworzona.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
			Wypelnij();
		}
	}
}
