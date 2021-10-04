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
	partial class OknoSpisu : Form
	{
		public string Tytul { get { return Text; } set { Text = value; } }
		public Control Zawartosc { get { return panelSpis.Controls.Cast<Control>().FirstOrDefault(); } set { panelSpis.Controls.Clear(); if (value != null) { panelSpis.Controls.Add(value); value.Dock = DockStyle.Fill; } } }

		public OknoSpisu()
		{
			InitializeComponent();
		}

		private OknoSpisu(string tytul, Control zawartosc, IEnumerable<AdapterAkcji> akcje)
			: this()
		{
			Tytul = tytul;
			Zawartosc = zawartosc;
			foreach (var akcja in akcje) panelAkcji.DodajAkcje(akcja);
		}

		public static OknoSpisu Utworz<TRekord, TSpis>(Kontekst kontekst, params AkcjaNaSpisie<TRekord>[] akcje)
			where TRekord : Rekord<TRekord>
			where TSpis : Control, ISpis<TRekord>, new()
		{
			var spis = new TSpis();
			spis.Kontekst = kontekst;
			var adaptery = new List<AdapterAkcji>();
			foreach (var akcja in akcje) adaptery.Add(new AdapterAkcji<TRekord>(akcja, spis));
			var okno = new OknoSpisu(spis.Tytul, spis, adaptery);
			return okno;
		}
	}
}
