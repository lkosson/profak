using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	abstract class AdapterAkcji
	{
		public abstract string Nazwa { get; }
		public abstract bool CzyDostepna { get; }
		public abstract bool CzyDomyslna { get; }

		public abstract void Uruchom();
	}

	class AdapterAkcji<TRekord> : AdapterAkcji
		where TRekord : Rekord<TRekord>
	{
		private readonly AkcjaNaSpisie<TRekord> akcja;
		private readonly ISpis<TRekord> spis;

		public override string Nazwa => akcja.Nazwa;
		public override bool CzyDostepna => akcja.CzyDostepnaDlaRekordow(spis.WybraneRekordy);
		public override bool CzyDomyslna => akcja.CzyDomyslna;

		public AdapterAkcji(AkcjaNaSpisie<TRekord> akcja, ISpis<TRekord> spis)
		{
			this.akcja = akcja;
			this.spis = spis;
		}

		public override void Uruchom()
		{
			akcja.Uruchom(spis.Kontekst, spis.WybraneRekordy);
			spis.Przeladuj();
		}
	}
}
