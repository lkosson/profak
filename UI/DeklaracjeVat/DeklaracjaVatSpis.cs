using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class DeklaracjaVatSpis : Spis<DeklaracjaVat>
	{
		public DeklaracjaVatSpis()
		{
			DodajKolumne(nameof(DeklaracjaVat.MiesiacFmt), "Miesiąc");
			DodajKolumne(nameof(DeklaracjaVat.NettoRazem), "Podstawa", wyrownajDoPrawej: true, format: "#,##0", szerokosc: 80);
			DodajKolumne(nameof(DeklaracjaVat.NaleznyRazem), "Należny", wyrownajDoPrawej: true, format: "#,##0", szerokosc: 80);
			DodajKolumne(nameof(DeklaracjaVat.NaliczonyRazem), "Naliczony", wyrownajDoPrawej: true, format: "#,##0", szerokosc: 80);
			DodajKolumne(nameof(DeklaracjaVat.DoWplaty), "Do wpłaty", wyrownajDoPrawej: true, format: "#,##0", szerokosc: 80);
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.DeklaracjeVat.OrderBy(deklaracja => deklaracja.Miesiac).ToList();
		}
	}
}
