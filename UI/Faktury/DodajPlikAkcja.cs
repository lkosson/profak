using ProFak.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class DodajPlikAkcja : AkcjaNaSpisie<Plik>
	{
		private readonly PlikSpis spis;

		public override string Nazwa => "Dołącz plik";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Plik> zaznaczoneRekordy) => true;

		public DodajPlikAkcja(PlikSpis spis)
		{
			this.spis = spis;
		}

		public override void Uruchom(Kontekst kontekst, IEnumerable<Plik> zaznaczoneRekordy)
		{
			using var dialog = new OpenFileDialog();
			dialog.Title = "Wybierz pliki do dołączenia do faktury";
			dialog.Multiselect = true;
			dialog.RestoreDirectory = true;
			if (dialog.ShowDialog() != DialogResult.OK) return;

			using var nowyKontekst = new Kontekst(kontekst);
			using var transakcja = nowyKontekst.Transakcja();

			foreach (var sciezka in dialog.FileNames)
			{
				var dane = File.ReadAllBytes(sciezka);
				var nazwa = Path.GetFileName(sciezka);
				var zawartosc = new Zawartosc { Dane = dane };
				nowyKontekst.Baza.Zapisz(zawartosc);
				var plik = new Plik { FakturaId = spis.FakturaRef, Nazwa = nazwa, Rozmiar = dane.Length, ZawartoscRef = zawartosc };
				nowyKontekst.Baza.Zapisz(plik);
				zawartosc.PlikRef = plik;
				nowyKontekst.Baza.Zapisz(zawartosc);
			}

			transakcja.Zatwierdz();
		}
	}
}
