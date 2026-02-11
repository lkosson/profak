using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

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

        private void btnSprawdzAktualizacje_Click(object sender, EventArgs e)
        {
            try
            {
                var wb = new WebClient();
                wb.Headers.Add("User-Agent: Other"); // fix for HTTP 403
                var response = wb.DownloadString("https://api.github.com/repos/lkosson/profak/releases/latest");
                var json = JObject.Parse(response);
                Version wersjaGitHub = Version.Parse(json["tag_name"].ToString().Replace("v", ""));
                Version wersjaAplikacji = GetType().Assembly.GetName().Version;
                if (wersjaGitHub.Major > wersjaAplikacji.Major ||
                    (wersjaGitHub.Major == wersjaAplikacji.Major && wersjaGitHub.Minor > wersjaAplikacji.Minor))
                {
                    if (MessageBox.Show("Dostępna jest nowa wersja " + json["tag_name"] + ". \r\nCzy chcesz przejść do strony pobierania?", "Aktualizacja programu", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("explorer.exe", json["html_url"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Nie znaleziono nowej wersji oprogramowania.", "Aktualizacja programu");
                }
            }
            catch (Exception ex)
            {
                OknoBledu.Pokaz(ex);
            }
        }
    }
}
