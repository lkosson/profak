using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class KSeFSpis : Spis<Faktura>
	{
		private bool pierwszeZaladowanie = true;
		private readonly bool sprzedaz;
		private readonly bool przyrostowo;
		private readonly DateTime odDaty;
		private readonly DataGridViewTextBoxColumn kolumnaNazwaNabywcy;
		private readonly DataGridViewTextBoxColumn kolumnaNIPNabywcy;
		private readonly DataGridViewTextBoxColumn kolumnaNazwaSprzedawcy;
		private readonly DataGridViewTextBoxColumn kolumnaNIPSprzedawcy;
		public override string Podsumowanie
		{
			get
			{
				var podsumowanie = base.Podsumowanie;
				if (WybraneRekordy.Count() > 1)
				{
					podsumowanie += $"\nRazem netto: {WybraneRekordy.Sum(faktura => faktura.RazemNetto).ToString("n2")}";
					podsumowanie += $"\nRazem brutto: {WybraneRekordy.Sum(faktura => faktura.RazemBrutto).ToString("n2")}";
				}
				return podsumowanie;
			}
		}

		public KSeFSpis()
		{
			DodajKolumne(nameof(Faktura.Numer), "Numer");
			DodajKolumne(nameof(Faktura.RodzajFmt), "Rodzaj");
			DodajKolumne(nameof(Faktura.DataSprzedazy), "Data sprzedaży", format: "yyyy-MM-dd", szerokosc: 120);
			DodajKolumne(nameof(Faktura.DataKSeF), "Data wystawienia", format: "yyyy-MM-dd HH:mm:ss", szerokosc: 170);
			kolumnaNazwaNabywcy = DodajKolumne(nameof(Faktura.NazwaNabywcy), "Nabywca", rozciagnij: true);
			kolumnaNIPNabywcy = DodajKolumne(nameof(Faktura.NIPNabywcy), "NIP nabywcy", szerokosc: 120);
			kolumnaNazwaSprzedawcy = DodajKolumne(nameof(Faktura.NazwaSprzedawcy), "Sprzedawca", rozciagnij: true);
			kolumnaNIPSprzedawcy = DodajKolumne(nameof(Faktura.NIPSprzedawcy), "NIP sprzedawcy", szerokosc: 120);
			DodajKolumneKwota(nameof(Faktura.RazemNetto), "Netto");
			DodajKolumneKwota(nameof(Faktura.RazemVat), "VAT");
			DodajKolumneKwota(nameof(Faktura.RazemBrutto), "Brutto");
			DodajKolumne(nameof(Faktura.WalutaFmt), "Waluta", szerokosc: 70);
			DodajKolumne(nameof(Faktura.NumerKSeF), "Id", szerokosc: 230);
		}

		public KSeFSpis(bool sprzedaz, string[] parametry)
			: this()
		{
			this.sprzedaz = sprzedaz;
			if (parametry == null) return;
			odDaty = new DateTime(2022, 1, 1);
			foreach (var parametr in parametry)
			{
				if (parametr == "Przyrostowo") przyrostowo = true;
				else if (parametr == "Dzis") odDaty = DateTime.Now.Date;
				else if (parametr == "Wczoraj") odDaty = DateTime.Now.Date.AddDays(-1);
				else if (parametr == "Miesiac") odDaty = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day);
				else if (parametr == "Poprzedni") odDaty = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day).AddMonths(-1);
				else if (parametr == "Rok") odDaty = new DateTime(DateTime.Now.Year, 1, 1);
			}
		}

		protected override void Przeladuj()
		{
			kolumnaNazwaNabywcy.Visible = sprzedaz;
			kolumnaNIPNabywcy.Visible = sprzedaz;
			kolumnaNazwaSprzedawcy.Visible = !sprzedaz;
			kolumnaNIPSprzedawcy.Visible = !sprzedaz;

			if (pierwszeZaladowanie)
			{
				Rekordy = new[] { new Faktura { NazwaNabywcy = "Przeładuj spis aby pobrać dane z KSeF", NazwaSprzedawcy = "Przeładuj spis aby pobrać dane z KSeF", Id = -1 } };
				pierwszeZaladowanie = false;
				return;
			}

			Rekordy = new[] { new Faktura { NazwaNabywcy = "Pobieranie danych z KSEF", NazwaSprzedawcy = "Pobieranie danych z KSEF", Id = -1 } };

			Task.Run(async delegate
			{
				try
				{
					var odDaty = this.odDaty;
					var podmiot = Kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
					if (String.IsNullOrEmpty(podmiot.TokenKSeF)) throw new ApplicationException("Brak tokena dostępowego do KSeF w danych firmy.");
					if (przyrostowo)
					{
						var ostatnia = Kontekst.Baza.Faktury
							.Where(e => e.Rodzaj == RodzajFaktury.Zakup || e.Rodzaj == RodzajFaktury.KorektaZakupu)
							.Where(e => !String.IsNullOrEmpty(e.NumerKSeF))
							.OrderByDescending(e => e.DataKSeF)
							.FirstOrDefault();
						if (ostatnia != null && ostatnia.DataKSeF.HasValue) odDaty = ostatnia.DataKSeF.Value;
					}
					var istniejace = Kontekst.Baza.Faktury.Where(e => !String.IsNullOrEmpty(e.NumerKSeF)).Select(e => e.NumerKSeF).ToHashSet();
					using var api = new IO.KSEF.API(podmiot.SrodowiskoKSeF);
					var cts = new CancellationTokenSource();
					cts.CancelAfter(TimeSpan.FromSeconds(10));
					await api.AuthenticateAsync(podmiot.NIP, podmiot.TokenKSeF);
					var naglowki = await api.GetInvoicesAsync(przyrostowo ? "incremental" : "range", sprzedaz ? "subject1" : "subject2", odDaty, DateTime.Now);
					await api.Terminate();
					var rekordy = naglowki.Select(IO.KSEF.Generator.Zbuduj).ToList();
					await ThreadSwitcher.ResumeForegroundAsync(this);
					foreach (var rekord in rekordy) if (istniejace.Contains(rekord.NumerKSeF)) rekord.Id = 1;
					Rekordy = rekordy;
				}
				catch (Exception exc)
				{
					OknoBledu.Pokaz(exc);
				}
			});
		}

		protected override void UstawStylWiersza(Faktura rekord, string kolumna, DataGridViewCellStyle styl)
		{
			base.UstawStylWiersza(rekord, kolumna, styl);
			if (rekord.Id != 0) styl.ForeColor = Color.FromArgb(20, 170, 30);
		}
	}
}
