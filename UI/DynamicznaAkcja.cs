using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class DynamicznaAkcja<TRekord> : AkcjaNaSpisie<TRekord>
		where TRekord : Rekord<TRekord>
	{
		private readonly string nazwa;
		private readonly Action<Kontekst, IEnumerable<TRekord>> akcja;
		private readonly Keys klawisz;
		private readonly Keys modyfikatory;
		private readonly bool wymaganyRekord;

		public override string Nazwa => nazwa;

		public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => !wymaganyRekord || zaznaczoneRekordy.Count() >= 1;
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == this.modyfikatory && klawisz == this.klawisz;

		public DynamicznaAkcja(string nazwa, Action<Kontekst, IEnumerable<TRekord>> akcja, Keys klawisz, Keys modyfikatory)
		{
			this.nazwa = nazwa;
			this.akcja = akcja;
			this.klawisz = klawisz;
			this.modyfikatory = modyfikatory;
			this.wymaganyRekord = true;
		}

		public DynamicznaAkcja(string nazwa, Action<Kontekst> akcja, Keys klawisz, Keys modyfikatory)
		{
			this.nazwa = nazwa;
			this.akcja = (kontekst, rekordy) => akcja(kontekst);
			this.klawisz = klawisz;
			this.modyfikatory = modyfikatory;
			this.wymaganyRekord = false;
		}

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<TRekord> zaznaczoneRekordy)
		{
			using var nowyKontekst = new Kontekst(kontekst);
			using var transakcja = nowyKontekst.Transakcja();
			akcja(nowyKontekst, zaznaczoneRekordy);
			transakcja.Zatwierdz();
		}
	}
}
