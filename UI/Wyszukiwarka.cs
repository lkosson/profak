using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class Wyszukiwarka : TextBox
	{
		public Wyszukiwarka()
		{
			var opis = "";
			if (Wyglad.IkonyAkcji) opis += "🔍 ";
			opis += "Wyszukaj";
			if (Wyglad.SkrotyKlawiaturoweAkcji) opis += " [F3]";
			PlaceholderText = opis;
			TextAlign = HorizontalAlignment.Center;
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			TextAlign = String.IsNullOrEmpty(Text) ? HorizontalAlignment.Center : HorizontalAlignment.Left;
		}
	}
}
