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
	class ZapiszPlikAction : AkcjaNaSpisie<Plik>
	{
		public override string Nazwa => "Zapisz plik [CTRL-S]";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Plik> zaznaczoneRekordy) => zaznaczoneRekordy.Any();
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.Control && klawisz == Keys.S;

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<Plik> zaznaczoneRekordy)
		{
			var plik = zaznaczoneRekordy.Single();
			using var nowyKontekst = new Kontekst(kontekst);
			nowyKontekst.Dodaj(plik);
			using var dialog = new SaveFileDialog();
			dialog.Title = "Zapisywanie pliku";
			dialog.RestoreDirectory = true;
			dialog.FileName = plik.Nazwa;
			if (dialog.ShowDialog() != DialogResult.OK) return;
			using var transakcja = nowyKontekst.Transakcja();
			var zawartosc = nowyKontekst.Baza.Znajdz<Zawartosc>(plik.ZawartoscId);
			File.WriteAllBytes(dialog.FileName, zawartosc.Dane);
		}
	}
}
