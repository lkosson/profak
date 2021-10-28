using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class PozycjaFakturySpis : Spis<PozycjaFaktury>
	{
		public PozycjaFakturySpis()
		{
			DodajKolumne(nameof(PozycjaFaktury.Opis), "Opis");
			DodajKolumne(nameof(PozycjaFaktury.Ilosc), "Ilość");
			DodajKolumne(nameof(PozycjaFaktury.CenaNetto), "Cena netto", wyrownajDoPrawej: true, format: "#,##0.00");
			DodajKolumne(nameof(PozycjaFaktury.CenaBrutto), "Cena brutto", wyrownajDoPrawej: true, format: "#,##0.00");
			DodajKolumne(nameof(PozycjaFaktury.WartoscNetto), "Wartość netto", wyrownajDoPrawej: true, format: "#,##0.00");
			DodajKolumne(nameof(PozycjaFaktury.WartoscVat), "VAT", wyrownajDoPrawej: true, format: "#,##0.00");
			DodajKolumne(nameof(PozycjaFaktury.WartoscBrutto), "Wartość brutto", wyrownajDoPrawej: true, format: "#,##0.00");
			DodajKolumne(nameof(PozycjaFaktury.Id), "Id", wyrownajDoPrawej: true);
		}

		public override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.PozycjeFaktur
				.ToList();
		}
	}
}
