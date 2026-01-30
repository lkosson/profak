using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class PozycjaFaktury : Rekord<PozycjaFaktury>
	{
		public int FakturaId { get; set; }
		public int? TowarId { get; set; }
		public string Opis { get; set; } = "";
		public decimal CenaNetto { get; set; }
		public decimal CenaVat { get; set; }
		public decimal CenaBrutto { get; set; }
		public decimal Ilosc { get; set; }
		public decimal WartoscNetto { get; set; }
		public decimal WartoscVat { get; set; }
		public decimal WartoscBrutto { get; set; }
		public bool CzyWedlugCenBrutto { get; set; }
		public bool CzyWartosciReczne { get; set; }
		public int? StawkaVatId { get; set; }
		public int? JednostkaMiaryId { get; set; }
		public int LP { get; set; }
		public bool CzyPrzedKorekta { get; set; }
		public int GTU { get; set; }
		public decimal? StawkaRyczaltu { get; set; }
		public decimal RabatProcent { get; set; }
		public decimal RabatCena { get; set; }
		public decimal RabatWartosc { get; set; }
		public decimal CenaZakupuDlaMarzy { get; set; }
		public string RabatFmt => (RabatProcent > 0 ? "-" + (RabatProcent / 1.0000000m) + "%" : "") + (RabatCena > 0 || RabatWartosc > 0 ? (RabatProcent > 0 ? ", " : "") + "-" + (RabatCena * Math.Abs(Ilosc) + RabatWartosc).ToString("n2") : "");
		public decimal RabatRazem => -(Ilosc * CenaNetto * RabatProcent / 100m + RabatCena * Ilosc + RabatWartosc).Zaokragl();

		public Ref<Faktura> FakturaRef { get => FakturaId; set => FakturaId = value; }
		public Ref<Towar> TowarRef { get => TowarId; set => TowarId = value; }
		public Ref<StawkaVat> StawkaVatRef { get => StawkaVatId; set => StawkaVatId = value; }
		public Ref<JednostkaMiary> JednostkaMiaryRef { get => JednostkaMiaryId; set => JednostkaMiaryId = value; }

		public Faktura Faktura { get; set; }
		public Towar Towar { get; set; }
		public StawkaVat StawkaVat { get; set; }
		public JednostkaMiary JednostkaMiary { get; set; }

		public decimal Cena => CzyWedlugCenBrutto ? CenaBrutto : CenaNetto;
		public decimal IloscAbs => Math.Abs(Ilosc);
		public decimal WartoscNettoAbs => Math.Abs(WartoscNetto);
		public decimal WartoscVatAbs => Math.Abs(WartoscVat);
		public decimal WartoscBruttoAbs => Math.Abs(WartoscBrutto);

		public override bool CzyPasuje(string fraza)
			=> base.CzyPasuje(fraza)
			|| CzyPasuje(Opis, fraza)
			|| CzyPasuje(CenaNetto, fraza)
			|| CzyPasuje(CenaVat, fraza)
			|| CzyPasuje(CenaBrutto, fraza)
			|| CzyPasuje(Ilosc, fraza)
			|| CzyPasuje(WartoscNetto, fraza)
			|| CzyPasuje(WartoscVat, fraza)
			|| CzyPasuje(WartoscBrutto, fraza)
			|| CzyPasuje(CenaZakupuDlaMarzy, fraza)
			|| CzyPasuje(CzyWartosciReczne ? "Ręczne" : "", fraza)
			|| CzyPasuje(CzyWedlugCenBrutto ? "Brutto" : "Netto", fraza);

		public void PrzeliczCeny(Baza baza)
		{
			if (CzyWartosciReczne) return;
			var stawkaVat = baza.Znajdz(StawkaVatRef);
			var procentVat = stawkaVat?.Wartosc ?? 0;

			if (CenaZakupuDlaMarzy > 0)
			{
				var marzaBrutto = CenaBrutto - CenaZakupuDlaMarzy;
				var marzaNetto = (marzaBrutto * 100m / (100 + procentVat)).Zaokragl();
				CenaNetto = marzaNetto;
				CenaVat = (marzaBrutto - marzaNetto).Zaokragl();
				WartoscBrutto = (Ilosc * CenaBrutto).Zaokragl();
				WartoscNetto = (Ilosc * CenaNetto).Zaokragl();
				WartoscVat = (Ilosc * CenaVat).Zaokragl();
			}
			else if (CzyWedlugCenBrutto)
			{
				CenaNetto = (CenaBrutto * 100m / (100 + procentVat)).Zaokragl();
				CenaVat = (CenaBrutto - CenaNetto).Zaokragl();
				WartoscBrutto = (Ilosc * CenaBrutto * (100 - RabatProcent) / 100m - RabatCena * Ilosc - RabatWartosc).Zaokragl();
				WartoscNetto = (WartoscBrutto * 100m / (100 + procentVat)).Zaokragl();
				WartoscVat = (WartoscBrutto - WartoscNetto).Zaokragl();
			}
			else
			{
				CenaVat = (CenaNetto * procentVat / 100).Zaokragl();
				CenaBrutto = (CenaNetto + CenaVat).Zaokragl();
				WartoscNetto = (Ilosc * CenaNetto * (100 - RabatProcent) / 100m - RabatCena * Ilosc - RabatWartosc).Zaokragl();
				WartoscVat = (WartoscNetto * procentVat / 100).Zaokragl();
				WartoscBrutto = (WartoscNetto + WartoscVat).Zaokragl();
			}
		}
	}
}
