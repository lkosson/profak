using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Kontrahent : Rekord<Kontrahent>
	{
		public string Nazwa { get; set; } = "";
		public string PelnaNazwa { get; set; } = "";
		public string NIP { get; set; } = "";
		public string AdresRejestrowy { get; set; } = "";
		public string AdresKorespondencyjny { get; set; } = "";
		public string RachunekBankowy { get; set; } = "";
		public string Telefon { get; set; } = "";
		public string EMail { get; set; } = "";
		public string Uwagi { get; set; } = "";
		public bool CzyArchiwalny { get; set; }
		public bool CzyPodmiot { get; set; }
		public bool CzyTP { get; set; }

		public string KodUrzedu { get; set; }
		public string OsobaFizycznaImie { get; set; }
		public string OsobaFizycznaNazwisko { get; set; }
		public DateTime? OsobaFizycznaDataUrodzenia { get; set; }
		public FormaOpodatkowania? FormaOpodatkowania { get; set; }

		public string AdresRejestrowyFmt => String.Join(", ", (AdresRejestrowy ?? "").Split('\r', '\n').Where(linia => !String.IsNullOrWhiteSpace(linia)));

		public override bool CzyPasuje(string fraza)
			=> base.CzyPasuje(fraza)
			|| CzyPasuje(Nazwa, fraza)
			|| CzyPasuje(PelnaNazwa, fraza)
			|| CzyPasuje(NIP, fraza)
			|| CzyPasuje(AdresRejestrowy, fraza)
			|| CzyPasuje(AdresKorespondencyjny, fraza)
			|| CzyPasuje(RachunekBankowy, fraza)
			|| CzyPasuje(Telefon, fraza)
			|| CzyPasuje(EMail, fraza)
			|| CzyPasuje(Uwagi, fraza)
			|| CzyPasuje(CzyArchiwalny ? "Archiwalny" : "", fraza)
			|| CzyPasuje(CzyPodmiot ? "Podmiot" : "", fraza);
	}

	enum FormaOpodatkowania
	{
		Liniowy,
		Skala,
		Ryczałt
	}
}
