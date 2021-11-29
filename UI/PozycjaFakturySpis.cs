using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class PozycjaFakturySpis : Spis<PozycjaFaktury>
	{
		public Ref<Faktura> FakturaRef { get; set; }

		public PozycjaFakturySpis()
		{
			DodajKolumne(nameof(PozycjaFaktury.Opis), "Opis", rozciagnij: true);
			DodajKolumne(nameof(PozycjaFaktury.Ilosc), "Ilość", wyrownajDoPrawej: true, szerokosc: 80);
			DodajKolumneKwota(nameof(PozycjaFaktury.Cena), "Cena");
			DodajKolumneKwota(nameof(PozycjaFaktury.WartoscNetto), "Netto");
			DodajKolumneKwota(nameof(PozycjaFaktury.WartoscVat), "VAT");
			DodajKolumneKwota(nameof(PozycjaFaktury.WartoscBrutto), "Brutto");
			DodajKolumneId();
		}

		public override void Przeladuj()
		{
			IQueryable<PozycjaFaktury> q = Kontekst.Baza.PozycjeFaktur;
			if (FakturaRef.IsNotNull) q = q.Where(pozycja => pozycja.FakturaId == FakturaRef.Id);
			Rekordy = q.ToList();
		}

		protected override void UstawStylWiersza(PozycjaFaktury rekord, string kolumna, DataGridViewCellStyle styl)
		{
			base.UstawStylWiersza(rekord, kolumna, styl);
			if (rekord.Ilosc < 0) styl.ForeColor = Color.LightGray;
		}
	}
}
