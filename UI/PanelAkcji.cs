using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class PanelAkcji : Panel
	{
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CzyGlownySpis { get; set; }

		private readonly List<(Button przycisk, AdapterAkcji adapter)> przyciski;

		public PanelAkcji()
		{
			przyciski = new List<(Button przycisk, AdapterAkcji adapter)>();
			MinimumSize = new Size(200 * DeviceDpi / 96, 50);
			TabIndex = 100;
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			AktualizujUklad();
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			AktualizujUklad();
		}

		public void AktualizujUklad()
		{
			SuspendLayout();
			int y = 0;
			y += Padding.Top;
			foreach (Control kontrolka in Controls)
			{
				kontrolka.Location = new Point(Math.Max(Padding.Left, kontrolka.Margin.Left), y);
				kontrolka.Width = ClientSize.Width - Math.Max(Padding.Left, kontrolka.Margin.Left) - Math.Max(Padding.Right, kontrolka.Margin.Right);
				y += kontrolka.Height;
				y += kontrolka.Margin.Bottom;
			}
			Height = y;
			ResumeLayout();
		}

		public void DodajKontrolke(Control kontrolka)
		{
			Controls.Add(kontrolka);
		}

		public void DodajAkcje(AdapterAkcji adapter)
		{
			var przycisk = new ButtonDropDown();
			przycisk.AutoSize = true;
			przycisk.TabIndex = TabIndex + Controls.Count;
			przycisk.Click += delegate { adapter.Uruchom(); };
			if (adapter.Podrzedne.Count > 0)
			{
				var menu = new ContextMenuStrip();
				menu.ShowImageMargin = false;
				foreach (var podrzedna in adapter.Podrzedne)
				{
					var pozycja = new ToolStripButton();
					pozycja.Text = Wyglad.NazwaAkcji(podrzedna);
					pozycja.Dock = DockStyle.Fill;
					pozycja.TextAlign = ContentAlignment.MiddleLeft;
					pozycja.Click += delegate { podrzedna.Uruchom(); };
					menu.Items.Add(pozycja);
				}
				przycisk.Menu = menu;
			}
			AktualizujPrzycisk(przycisk, adapter);
			Controls.Add(przycisk);
			przyciski.Add((przycisk, adapter));
		}

		public void Aktualizuj()
		{
			foreach ((var przycisk, var adapter) in przyciski)
			{
				AktualizujPrzycisk(przycisk, adapter);
			}
			AktualizujUklad();
		}

		private void AktualizujPrzycisk(Button przycisk, AdapterAkcji adapter)
		{
			przycisk.Text = Wyglad.NazwaAkcji(adapter);
			przycisk.Enabled = adapter.CzyDostepna;
		}

		public bool ObsluzKlawisz(Keys klawisz, Keys modyfikatory)
		{
			foreach ((_, var adapter) in przyciski)
			{
				if (adapter.CzyKlawiszSkrotu(klawisz, modyfikatory))
				{
					adapter.Uruchom();
					return true;
				}
			}
			return false;
		}
	}
}
