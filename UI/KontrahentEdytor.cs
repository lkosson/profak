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
	partial class KontrahentEdytor : UserControl, IEdytor<Kontrahent>
	{
		public Kontrahent Rekord { get => bindingSource.DataSource as Kontrahent; private set => bindingSource.DataSource = value; }
		public Kontekst Kontekst { get; private set; }

		public KontrahentEdytor()
		{
			InitializeComponent();
		}

		public void Przygotuj(Kontekst kontekst, Kontrahent rekord)
		{
			Kontekst = kontekst;
			Rekord = rekord;
		}
	}
}
