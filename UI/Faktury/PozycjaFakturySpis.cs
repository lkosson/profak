using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class PozycjaFakturySpis : Spis<PozycjaFaktury>
	{
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Ref<Faktura> FakturaRef { get; set; }

		public PozycjaFakturySpis()
		{
			DodajKolumne(nameof(PozycjaFaktury.LP), "LP", szerokosc: 30);
			DodajKolumne(nameof(PozycjaFaktury.Opis), "Opis", rozciagnij: true);
			DodajKolumne(nameof(PozycjaFaktury.IloscAbs), "Ilość", wyrownajDoPrawej: true, szerokosc: 50);
			DodajKolumneKwota(nameof(PozycjaFaktury.Cena), "Cena", szerokosc: 70);
			DodajKolumneKwota(nameof(PozycjaFaktury.WartoscNettoAbs), "Netto", szerokosc: 70);
			DodajKolumneKwota(nameof(PozycjaFaktury.WartoscVatAbs), "VAT", szerokosc: 70);
			DodajKolumneKwota(nameof(PozycjaFaktury.WartoscBruttoAbs), "Brutto", szerokosc: 70);
			DodajKolumneKwota(nameof(PozycjaFaktury.RabatFmt), "Rabat", szerokosc: 70);
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			IQueryable<PozycjaFaktury> q = Kontekst.Baza.PozycjeFaktur;
			if (FakturaRef.IsNotNull) q = q.Where(pozycja => pozycja.FakturaId == FakturaRef.Id);
			q = q.OrderBy(pozycja => pozycja.LP).ThenByDescending(pozycja => pozycja.CzyPrzedKorekta).ThenBy(pozycja => pozycja.Id);
			Rekordy = q.ToList();
		}

		protected override void UstawStylWiersza(PozycjaFaktury rekord, string kolumna, DataGridViewCellStyle styl)
		{
			base.UstawStylWiersza(rekord, kolumna, styl);
			if (rekord.CzyPrzedKorekta) { styl.ForeColor = Color.LightGray; styl.SelectionForeColor = Color.LightGray; }
		}
	}
}
