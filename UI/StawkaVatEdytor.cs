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
	partial class StawkaVatEdytor : UserControl
	{
		public StawkaVat Rekord { get { return bindingSource.DataSource as StawkaVat; } set { bindingSource.DataSource = value; } }

		public StawkaVatEdytor()
		{
			InitializeComponent();
		}

		public StawkaVatEdytor(StawkaVat rekord)
			: this()
		{
			Rekord = rekord;
		}
	}
}
