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
	partial class StawkaVatSpis : UserControl, ISpis<DB.StawkaVat>
	{
		public Baza Baza { get; set; }
		public IEnumerable<StawkaVat> WybraneRekordy { get; set; }

		public StawkaVatSpis()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			Przeladuj();
			base.OnLoad(e);
		}

		public void Przeladuj()
		{
			bindingSource.DataSource = Baza.StawkiVat.ToList();
		}
	}
}
