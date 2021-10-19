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
	partial class FakturaEdytor : UserControl, IEdytor<Faktura>
	{
		public Faktura Rekord { get { return bindingSource.DataSource as Faktura; } set { bindingSource.DataSource = value; } }
		public Kontekst Kontekst { get; set; }

		public FakturaEdytor()
		{
			InitializeComponent();
		}
	}
}
