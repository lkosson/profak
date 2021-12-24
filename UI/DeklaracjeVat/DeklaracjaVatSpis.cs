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
			DodajKolumneKwota(nameof(DeklaracjaVat.NettoRazem), "Podstawa");
			DodajKolumneKwota(nameof(DeklaracjaVat.NaleznyRazem), "Należny");
			DodajKolumneKwota(nameof(DeklaracjaVat.NaliczonyRazem), "Naliczony");
			DodajKolumneKwota(nameof(DeklaracjaVat.DoWplaty), "Do wpłaty");
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.DeklaracjeVat.OrderBy(deklaracja => deklaracja.Miesiac).ToList();
		}
	}
}
