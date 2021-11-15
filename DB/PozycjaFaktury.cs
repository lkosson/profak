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
		public int TowarId { get; set; }
		public string Opis { get; set; }
		public decimal CenaNetto { get; set; }
		public decimal CenaVat { get; set; }
		public decimal CenaBrutto { get; set; }
		public decimal Ilosc { get; set; }
		public decimal WartoscNetto { get; set; }
		public decimal WartoscVat { get; set; }
		public decimal WartoscBrutto { get; set; }
		public bool CzyWedlugCenBrutto { get; set; }
		public bool CzyWartosciReczne { get; set; }

		public Ref<Faktura> FakturaRef { get => FakturaId; set => FakturaId = value; }
		public Ref<Towar> TowarRef { get => TowarId; set => TowarId = value; }

		public Faktura Faktura { get; set; }
		public Towar Towar { get; set; }

		public PozycjaFaktury()
		{
			Opis = "";
		}

		public override void WypelnijDomyslnePola(Baza baza)
		{
			base.WypelnijDomyslnePola(baza);
			var towar = baza.Towary.OrderBy(towar => towar.CzyArchiwalny).ThenBy(towar => towar.Id).FirstOrDefault();
			TowarRef = towar;
			Opis = towar.Nazwa;
		}

		public void PrzeliczCeny(Baza baza)
		{
			if (CzyWartosciReczne) return;
			var towar = baza.Towary.Include(towar => towar.StawkaVat).FirstOrDefault(towar => towar.Id == TowarId);
			var procentVat = towar?.StawkaVat?.Wartosc ?? 0;

			if (CzyWedlugCenBrutto)
			{
				CenaNetto = Decimal.Round(CenaBrutto * 100m / (100 + procentVat), 2, MidpointRounding.AwayFromZero);
				CenaVat = Decimal.Round(CenaBrutto - CenaNetto, 2, MidpointRounding.AwayFromZero);
				WartoscBrutto = Decimal.Round(Ilosc * CenaBrutto, 2, MidpointRounding.AwayFromZero);
				WartoscNetto = Decimal.Round(WartoscBrutto * 100m / (100 + procentVat), 2, MidpointRounding.AwayFromZero);
				WartoscVat = Decimal.Round(WartoscBrutto - WartoscNetto, 2, MidpointRounding.AwayFromZero);
			}
			else
			{
				CenaVat = Decimal.Round(CenaNetto * procentVat / 100, 2, MidpointRounding.AwayFromZero);
				CenaBrutto = Decimal.Round(CenaNetto + CenaVat, 2, MidpointRounding.AwayFromZero);
				WartoscNetto = Decimal.Round(Ilosc * CenaNetto, 2, MidpointRounding.AwayFromZero);
				WartoscVat = Decimal.Round(WartoscNetto * procentVat / 100, 2, MidpointRounding.AwayFromZero);
				WartoscBrutto = Decimal.Round(WartoscNetto + WartoscVat, 2, MidpointRounding.AwayFromZero);
			}
		}
	}
}
