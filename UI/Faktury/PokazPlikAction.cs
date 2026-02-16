using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class PokazPlikAction : AkcjaNaSpisie<Plik>
	{
		public override string Nazwa => "Otwórz plik [ENTER]";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Plik> zaznaczoneRekordy) => zaznaczoneRekordy.Any();
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.None && klawisz == Keys.Enter;

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<Plik> zaznaczoneRekordy)
		{
			var plik = zaznaczoneRekordy.Single();
			using var nowyKontekst = new Kontekst(kontekst);
			nowyKontekst.Dodaj(plik);
			using var transakcja = nowyKontekst.Transakcja();
			var zawartosc = nowyKontekst.Baza.Znajdz<Zawartosc>(plik.ZawartoscId);
			var sciezka = Path.GetTempFileName();
			var pelnaSciezka = sciezka + Path.GetExtension(plik.Nazwa);
			File.WriteAllBytes(sciezka, zawartosc.Dane);
			File.Move(sciezka, pelnaSciezka);

			var proces = new Process() { StartInfo = new ProcessStartInfo { FileName = pelnaSciezka, UseShellExecute = true }, EnableRaisingEvents = true };
			proces.Start();
			Thread.Sleep(100);
			if (proces == null || proces.HasExited)
			{
				AppDomain.CurrentDomain.ProcessExit += delegate
				{
					try
					{
						File.Delete(pelnaSciezka);
					}
					catch
					{
					}
				};
			}
			else
			{
				proces.Exited += delegate
				{
					try
					{
						File.Delete(pelnaSciezka);
					}
					catch
					{
						AppDomain.CurrentDomain.ProcessExit += delegate
						{
							try
							{
								File.Delete(pelnaSciezka);
							}
							catch
							{
							}
						};
					}
				};
			}
		}
	}
}
