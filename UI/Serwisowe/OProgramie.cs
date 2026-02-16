using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace ProFak.UI
{
	partial class OProgramie : UserControl, IKontrolkaZKontekstem
	{
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Kontekst Kontekst { get; set; } = default!;

		public OProgramie()
		{
			InitializeComponent();
			labelWersja.Text = GetType().Assembly.GetName().Version!.ToString();
			labelSciezka.Text = Environment.ProcessPath;
			labelData.Text = File.GetLastWriteTime(Environment.ProcessPath!).ToString("d MMMM yyyy, H:mm:ss");
		}

		private void linkLabelStrona_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "https://github.com/lkosson/profak/" });
		}

		private void btnSprawdzAktualizacje_Click(object? sender, EventArgs e)
		{
			try
			{
				string? response = null;
				OknoPostepu.Uruchom(async cancellationToken =>
				{
					using var wb = new HttpClient();
					wb.DefaultRequestHeaders.UserAgent.ParseAdd("ProFak (https://github.com/lkosson/profak)");
					response = await wb.GetStringAsync("https://api.github.com/repos/lkosson/profak/releases/latest", cancellationToken);
					cancellationToken.ThrowIfCancellationRequested();
				});

				var json = JsonDocument.Parse(response!);
				Version.TryParse(json.RootElement.GetProperty("tag_name").ToString().Replace("v", ""), out var wersjaGitHub);
				var wersjaAplikacji = GetType().Assembly.GetName().Version;
				if (wersjaGitHub != null && wersjaGitHub > wersjaAplikacji)
				{
					if (MessageBox.Show("Dostępna jest nowa wersja " + wersjaGitHub.ToString() + ".\r\nCzy chcesz przejść do strony pobierania?", "ProFak", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = json.RootElement.GetProperty("html_url").ToString() });
					}
				}
				else
				{
					MessageBox.Show("Nie znaleziono nowej wersji programu", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				OknoBledu.Pokaz(ex);
			}
		}
	}
}
