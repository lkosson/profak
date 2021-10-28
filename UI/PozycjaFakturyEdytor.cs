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
		public PozycjaFaktury Rekord { get => bindingSource.DataSource as PozycjaFaktury; private set => bindingSource.DataSource = value; }
		public Kontekst Kontekst { get; private set; }

		public PozycjaFakturyEdytor()
		{
			InitializeComponent();
		}

		public void Przygotuj(Kontekst kontekst, PozycjaFaktury rekord)
		{
			Kontekst = kontekst;
			WypelnijSpisy();
			Rekord = rekord;
		}

		private void WypelnijSpisy()
		{
		}
	}
}
