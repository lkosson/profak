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
		public PozycjaFaktury Rekord { get => kontroler.Model; private set { kontroler.Model = value; KonfigurujPoleIlosci(); } }
		public Kontekst Kontekst { get; private set; }
		private readonly Kontroler<PozycjaFaktury> kontroler;

		public PozycjaFakturyEdytor()
		{
			InitializeComponent();
			kontroler = new Kontroler<PozycjaFaktury>();

			kontroler.Powiazanie(comboBoxTowar, pozycja => pozycja.Opis);
			kontroler.Powiazanie(numericUpDownIlosc, pozycja => pozycja.Ilosc);
			kontroler.Powiazanie(numericUpDownCenaNetto, pozycja => pozycja.CenaNetto);
			kontroler.Powiazanie(numericUpDownCenaVat, pozycja => pozycja.CenaVat);
			kontroler.Powiazanie(numericUpDownCenaBrutto, pozycja => pozycja.CenaBrutto);
			kontroler.Powiazanie(numericUpDownWartoscNetto, pozycja => pozycja.WartoscNetto);
			kontroler.Powiazanie(numericUpDownWartoscVat, pozycja => pozycja.WartoscVat);
			kontroler.Powiazanie(numericUpDownWartoscBrutto, pozycja => pozycja.WartoscBrutto);
			kontroler.Powiazanie(checkBoxWedlugBrutto, pozycja => pozycja.CzyWedlugCenBrutto);
			kontroler.Powiazanie(checkBoxRecznie, pozycja => pozycja.CzyWartosciReczne);
		}

		public void Przygotuj(Kontekst kontekst, PozycjaFaktury rekord)
		{
			Kontekst = kontekst;
			WypelnijSpisy();
			Rekord = rekord;
		}

		private void WypelnijSpisy()
		{
			new SwobodnySlownik<Towar>(
				Kontekst, comboBoxTowar, buttonTowar,
				Kontekst.Baza.Towary.ToList,
				towar => towar.Nazwa,
				towar => { if (towar == null || Rekord.TowarRef == towar.Ref) return; Rekord.TowarRef = towar; Rekord.Opis = towar.Nazwa; KonfigurujPoleIlosci(); },
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
	}
}
