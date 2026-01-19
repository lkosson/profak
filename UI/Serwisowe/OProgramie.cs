using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	partial class OProgramie : UserControl, IKontrolkaZKontekstem
	{
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Kontekst Kontekst { get; set; }

		public OProgramie()
		{
			InitializeComponent();
			labelWersja.Text = GetType().Assembly.GetName().Version.ToString();
			labelSciezka.Text = Environment.ProcessPath;
			labelData.Text = File.GetLastWriteTime(Environment.ProcessPath).ToString("d MMMM yyyy, H:mm:ss");
		}

		private void linkLabelStrona_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "https://github.com/lkosson/profak/" });
		}
	}
}
