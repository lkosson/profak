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
	partial class NumeratorEdytor : UserControl, IEdytor<Numerator>
	{
		public Numerator Rekord { get => kontroler.Model; private set { kontroler.Model = value; } }
		public Kontekst Kontekst { get; private set; }
		private readonly SpisZAkcjami<StanNumeratora, StanNumeratoraSpis> stanyNumeratora;
		private readonly Kontroler<Numerator> kontroler;

		public NumeratorEdytor()
		{
			InitializeComponent();

			kontroler = new Kontroler<Numerator>();

			kontroler.Slownik<PrzeznaczenieNumeratora>(comboBoxPrzeznaczenie);

			kontroler.Powiazanie(comboBoxPrzeznaczenie, numerator => numerator.Przeznaczenie);
			kontroler.Powiazanie(textBoxFormat, numerator => numerator.Format);

			stanyNumeratora = Spis.StanyNumeratorow(Kontekst);
			panelStan.Controls.Add(stanyNumeratora);

			MinimumSize = new Size(800, 200);
		}

		public void Przygotuj(Kontekst kontekst, Numerator rekord)
		{
			Kontekst = kontekst;
			Rekord = rekord;
			stanyNumeratora.Spis.NumeratorRef = rekord;
			stanyNumeratora.Spis.Kontekst = kontekst;
		}
	}
}
