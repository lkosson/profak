using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class FakturaPodobnaSprzedazAkcja : FakturaPodobnaAkcja
	{
		public override string Nazwa => "➕ Wystaw podobną [SHIFT-INS]";

		protected override Faktura UtworzRekord(Kontekst kontekst, IEnumerable<Faktura> zaznaczoneRekordy)
		{
			var faktura = base.UtworzRekord(kontekst, zaznaczoneRekordy);
			if (faktura.Rodzaj == RodzajFaktury.Proforma)
			{
				var wynik = MessageBox.Show("Czy chcesz wystawić zwykłą fakturę na podstawie zaznaczonej faktury pro forma?", "ProFak", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (wynik == DialogResult.Yes) faktura.Rodzaj = faktura.ProceduraMarzy == ProceduraMarży.NieDotyczy ? RodzajFaktury.Sprzedaż : RodzajFaktury.VatMarża;
				else if (wynik == DialogResult.Cancel) return null;
			}
			return faktura;
		}
	}
}
