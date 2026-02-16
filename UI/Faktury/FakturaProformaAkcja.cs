using ProFak.DB;

namespace ProFak.UI
{
	class FakturaProformaAkcja : DodajRekordAkcja<Faktura, FakturaEdytor>
	{
		public override string Nazwa => "➕ Wystaw pro formę";
		public FakturaProformaAkcja()
			: base(faktura => faktura.Rodzaj = RodzajFaktury.Proforma)
		{
		}

		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => false;

		protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
		{
			rekord.NadajNumer(kontekst.Baza);
			base.ZapiszRekord(kontekst, rekord);
		}
	}
}
