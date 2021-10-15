using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class PanelAkcji : Panel
	{
		public PanelAkcji()
		{
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

		private void AktualizujUklad()
		{
			SuspendLayout();
			int y = 0;
			y += Padding.Top;
			foreach (Button przycisk in Controls)
			{
				przycisk.Location = new Point(Math.Max(Padding.Left, przycisk.Margin.Left), y);
				przycisk.Width = ClientSize.Width - Math.Max(Padding.Left, przycisk.Margin.Left) - Math.Max(Padding.Right, przycisk.Margin.Right);
				przycisk.Height = przycisk.GetPreferredSize(new Size(przycisk.Width, 10)).Height;
				y += przycisk.Height;
				y += przycisk.Margin.Bottom;
			}
			ResumeLayout();
		}

		public void DodajAkcje(AdapterAkcji adapter)
		{
			var przycisk = new Button();
			przycisk.Tag = adapter;
			przycisk.Click += Przycisk_Click;
			AktualizujPrzycisk(przycisk);
			Controls.Add(przycisk);
		}

		public void Aktualizuj()
		{
			foreach (Button przycisk in Controls)
			{
				AktualizujPrzycisk(przycisk);
			}
		}

		private void AktualizujPrzycisk(Button przycisk)
		{
			var adapter = (AdapterAkcji)przycisk.Tag;
			przycisk.Text = adapter.Nazwa;
			przycisk.Enabled = adapter.CzyDostepna;
		}

		private void Przycisk_Click(object sender, EventArgs e)
		{
			var przycisk = (Button)sender;
			var adapter = (AdapterAkcji)przycisk.Tag;
			if (!adapter.CzyDostepna) return;
			adapter.Uruchom();
		}
	}
}
