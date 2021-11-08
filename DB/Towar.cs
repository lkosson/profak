using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Towar : Rekord<Towar>
	{
		public string Nazwa { get; set; }
		public RodzajTowaru Rodzaj { get; set; }
		public decimal CenaNetto { get; set; }
		public decimal CenaBrutto { get; set; }
		public bool CzyWedlugCenBrutto { get; set; }
		public bool CzyArchiwalny { get; set; }

		public int StawkaVatId { get; set; }
		public int JednostkaMiaryId { get; set; }

		public Ref<StawkaVat> StawkaVatRef { get => StawkaVatId; set => StawkaVatId = value; }
		public Ref<JednostkaMiary> JednostkaMiaryRef { get => JednostkaMiaryId; set => JednostkaMiaryId = value; }

		public StawkaVat StawkaVat { get; set; }
		public JednostkaMiary JednostkaMiary { get; set; }

		public Towar()
		{
			Nazwa = "";
		}

		public override void WypelnijDomyslnePola(Baza baza)
		{
			base.WypelnijDomyslnePola(baza);
			StawkaVatRef = baza.StawkiVat.OrderByDescending(stawkaVat => stawkaVat.CzyDomyslna).ThenBy(stawkaVat => stawkaVat.Id).FirstOrDefault();
			JednostkaMiaryRef = baza.JednostkiMiar.OrderByDescending(jednostka => jednostka.CzyDomyslna).ThenBy(jednostka => jednostka.Id).FirstOrDefault();
			if (StawkaVatRef.IsNull) throw new ApplicationException("Przed dodaniem towaru należy zdefiniować przynajmniej jedną stawkę VAT.");
			if (JednostkaMiaryRef.IsNull) throw new ApplicationException("Przed dodaniem towaru należy zdefiniować przynajmniej jedną jednostkę miary.");
		}
	}

	enum RodzajTowaru
	{
		Towar,
		Usługa
	}
}
