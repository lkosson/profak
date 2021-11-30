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
	partial class NumeratorEdytor : NumeratorEdytorBase
	{
		private readonly SpisZAkcjami<StanNumeratora, StanNumeratoraSpis> stanyNumeratora;

		public NumeratorEdytor()
		{
			InitializeComponent();

			kontroler.Slownik<PrzeznaczenieNumeratora>(comboBoxPrzeznaczenie);

			kontroler.Powiazanie(comboBoxPrzeznaczenie, numerator => numerator.Przeznaczenie);
			kontroler.Powiazanie(textBoxFormat, numerator => numerator.Format);

			Wymagane(comboBoxPrzeznaczenie);
			Wymagane(textBoxFormat);

			panelStan.Controls.Add(stanyNumeratora = Spisy.StanyNumeratorow());
		}

		protected override void RekordGotowy()
		{
			base.RekordGotowy();
			stanyNumeratora.Spis.NumeratorRef = Rekord;
			stanyNumeratora.Spis.Kontekst = Kontekst;
		}
	}

	class NumeratorEdytorBase : Edytor<Numerator>
	{
	}
}
