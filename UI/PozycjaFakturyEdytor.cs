using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	partial class PozycjaFakturyEdytor : UserControl, IEdytor<PozycjaFaktury>
	{
		public PozycjaFaktury Rekord { get => kontroler.Model; private set { kontroler.Model = value; KonfigurujPoleIlosci(); KonfigurujCeny(); } }
		public Kontekst Kontekst { get; private set; }
		private readonly Kontroler<PozycjaFaktury> kontroler;

		public PozycjaFakturyEdytor()
		{
			InitializeComponent();
			kontroler = new Kontroler<PozycjaFaktury>();

			kontroler.Powiazanie(comboBoxTowar, pozycja => pozycja.Opis);
			kontroler.Powiazanie(numericUpDownIlosc, pozycja => pozycja.Ilosc, PrzeliczCeny);
			kontroler.Powiazanie(numericUpDownCenaNetto, pozycja => pozycja.CenaNetto, PrzeliczCeny);
			kontroler.Powiazanie(numericUpDownCenaVat, pozycja => pozycja.CenaVat, PrzeliczCeny);
			kontroler.Powiazanie(numericUpDownCenaBrutto, pozycja => pozycja.CenaBrutto, PrzeliczCeny);
			kontroler.Powiazanie(numericUpDownWartoscNetto, pozycja => pozycja.WartoscNetto, PrzeliczCeny);
			kontroler.Powiazanie(numericUpDownWartoscVat, pozycja => pozycja.WartoscVat, PrzeliczCeny);
			kontroler.Powiazanie(numericUpDownWartoscBrutto, pozycja => pozycja.WartoscBrutto, PrzeliczCeny);
			kontroler.Powiazanie(checkBoxWedlugBrutto, pozycja => pozycja.CzyWedlugCenBrutto, KonfigurujCeny);
			kontroler.Powiazanie(checkBoxRecznie, pozycja => pozycja.CzyWartosciReczne, KonfigurujCeny);
		}

		public void Przygotuj(Kontekst kontekst, PozycjaFaktury rekord)
		{
			Kontekst = kontekst;
			WypelnijSpisy();
			Rekord = rekord;
		}

		private void WypelnijSpisy()
		{
			new Slownik<Towar>(
				Kontekst, comboBoxTowar, buttonTowar,
				Kontekst.Baza.Towary.ToList,
				towar => towar.Nazwa,
				towar => { if (towar == null || Rekord.TowarRef == towar.Ref) return; Rekord.TowarRef = towar; Rekord.Opis = towar.Nazwa; Rekord.CzyWedlugCenBrutto = towar.CzyWedlugCenBrutto; KonfigurujPoleIlosci(); KonfigurujCeny(); PrzeliczCeny(); },
				Spis.Towary)
				.Zainstaluj();
		}

		private void KonfigurujPoleIlosci()
		{
			var towar = Kontekst.Baza.Towary.Include(towar => towar.JednostkaMiary).FirstOrDefault(towar => towar.Id == Rekord.TowarId);
			if (towar == null)
			{
				labelJednostka.Text = "";
				numericUpDownIlosc.DecimalPlaces = 0;
			}
			else
			{
				labelJednostka.Text = towar.JednostkaMiary.Nazwa;
				numericUpDownIlosc.DecimalPlaces = towar.JednostkaMiary.LiczbaMiescPoPrzecinku;
			}
		}

		private void KonfigurujCeny()
		{
			numericUpDownCenaNetto.Enabled = Rekord.CzyWartosciReczne || !Rekord.CzyWedlugCenBrutto;
			numericUpDownCenaVat.Enabled = Rekord.CzyWartosciReczne;
			numericUpDownCenaBrutto.Enabled = Rekord.CzyWartosciReczne || Rekord.CzyWedlugCenBrutto;
			numericUpDownWartoscNetto.Enabled = Rekord.CzyWartosciReczne;
			numericUpDownWartoscVat.Enabled = Rekord.CzyWartosciReczne;
			numericUpDownWartoscBrutto.Enabled = Rekord.CzyWartosciReczne;
		}

		private void PrzeliczCeny()
		{
			Rekord.PrzeliczCeny(Kontekst.Baza);
			kontroler.AktualizujKontrolki();
		}
	}
}
