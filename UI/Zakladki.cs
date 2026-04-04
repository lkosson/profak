using System;
using System.Collections.Generic;
using System.Text;

namespace ProFak.UI;

class Zakladki : TabControl
{
	public void Dodaj(string etykieta, Control zawartosc)
	{
		var tabPage = new TabPage();
		tabPage.Text = etykieta;
		tabPage.Padding = new Padding(3);
		tabPage.UseVisualStyleBackColor = true;
		tabPage.Controls.Add(zawartosc);
		zawartosc.Dock = DockStyle.Fill;
		TabPages.Add(tabPage);
	}
}
