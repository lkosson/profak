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
	}
}
