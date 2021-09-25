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
		private List<(AdapterAkcji adapter, Button przycisk)> akcje;

		public PanelAkcji()
		{
			akcje = new List<(AdapterAkcji adapter, Button przycisk)>();
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
			foreach (var (adapter, przycisk) in akcje)
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
			var przycisk = DodajPrzycisk(adapter);
			akcje.Add((adapter, przycisk));
		}

		private Button DodajPrzycisk(AdapterAkcji adapter)
		{
			var przycisk = new Button();
			przycisk.Name = adapter.Nazwa;
			przycisk.Text = adapter.Nazwa;
			przycisk.Tag = adapter;
			przycisk.Click += Przycisk_Click;
			Controls.Add(przycisk);
			return przycisk;
		}

		private void Przycisk_Click(object sender, EventArgs e)
		{
			var przycisk = (Button)sender;
			var adapter = (AdapterAkcji)przycisk.Tag;
			adapter.Uruchom();
		}
	}
}
