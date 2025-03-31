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
	class FakturaSprzedazyGrupaAkcji : FakturaSprzedazyAkcja
	{
		public override IReadOnlyCollection<AkcjaNaSpisie<Faktura>> Podrzedne => [new FakturaPodobnaSprzedazAkcja(), new FakturaProformaAkcja(), new KorektaSprzedazyAkcja()];
	}
}
