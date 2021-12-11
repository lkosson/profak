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
		public int LP { get; set; }
		public bool CzyPrzedKorekta { get; set; }
		public int GTU { get; set; }

		public Ref<Faktura> FakturaRef { get => FakturaId; set => FakturaId = value; }
		public Ref<Towar> TowarRef { get => TowarId; set => TowarId = value; }
		public Ref<StawkaVat> StawkaVatRef { get => StawkaVatId; set => StawkaVatId = value; }

		public Faktura Faktura { get; set; }
		public Towar Towar { get; set; }
		public StawkaVat StawkaVat { get; set; }

		public decimal Cena => CzyWedlugCenBrutto ? CenaBrutto : CenaNetto;

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
			|| CzyPasuje(CzyWartosciReczne ? "Ręczne" : "", fraza)
			|| CzyPasuje(CzyWedlugCenBrutto ? "Brutto" : "Netto", fraza);

		public void PrzeliczCeny(Baza baza)
		{
			if (CzyWartosciReczne) return;
			var stawkaVat = baza.Znajdz(StawkaVatRef);
			var procentVat = stawkaVat?.Wartosc ?? 0;

			if (CzyWedlugCenBrutto)
			{
				CenaNetto = Zaokragl(CenaBrutto * 100m / (100 + procentVat));
				CenaVat = Zaokragl(CenaBrutto - CenaNetto);
				WartoscBrutto = Zaokragl(Ilosc * CenaBrutto);
				WartoscNetto = Zaokragl(WartoscBrutto * 100m / (100 + procentVat));
				WartoscVat = Zaokragl(WartoscBrutto - WartoscNetto);
			}
			else
			{
				CenaVat = Zaokragl(CenaNetto * procentVat / 100);
				CenaBrutto = Zaokragl(CenaNetto + CenaVat);
				WartoscNetto = Zaokragl(Ilosc * CenaNetto);
				WartoscVat = Zaokragl(WartoscNetto * procentVat / 100);
				WartoscBrutto = Zaokragl(WartoscNetto + WartoscVat);
			}
		}
	}
}
