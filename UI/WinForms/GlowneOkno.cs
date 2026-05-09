#if WINFORMS
using System.Data;

namespace ProFak.UI;

partial class GlowneOkno : Form
{
	private TableLayoutPanel panelZawartosc;

	public GlowneOkno()
	{
		menu = new Menu(ZbudujMenu);

		panelZawartosc = new TableLayoutPanel();
		panelZawartosc.Margin = new Padding(0);
		panelZawartosc.RowCount = 1;
		panelZawartosc.ColumnCount = 1;
		panelZawartosc.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
		panelZawartosc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

		var uklad = new Siatka([Wyglad.SzerokoscMenu, -1], [-1]);
		uklad.DodajWiersz([menu, panelZawartosc]);

		KeyPreview = true;
		WindowState = FormWindowState.Maximized;
		Text = "ProFak";
		Icon = Ikona;
		uklad.Dock = DockStyle.Fill;
		Controls.Add(uklad);
	}

	protected override void OnFormClosing(FormClosingEventArgs e)
	{
		if (e.CloseReason == CloseReason.UserClosing && Wyglad.PotwierdzanieZamknieciaProgramu)
		{
			if (!OknoKomunikatu.PytanieTakNie("Czy na pewno chcesz zamknąć program?", domyslnie: false))
			{
				e.Cancel = true;
			}
		}
		base.OnFormClosing(e);
	}

	private void Wyswietl<TKontrolka>(TKontrolka kontrolka)
		where TKontrolka : TControl, IKontrolkaZKontekstem
	{
		var doUsuniecia = panelZawartosc.Controls.Cast<Control>().ToList();

		var kontekst = new Kontekst();
		kontrolka.Kontekst = kontekst;
		kontrolka.Disposed += delegate { panelZawartosc.Controls.Remove(kontrolka); menu.Focus(); kontekst.Dispose(); };
		if (kontrolka.MaximumSize == default) kontrolka.Dock = DockStyle.Fill;
		else kontrolka.Anchor = AnchorStyles.None;
		panelZawartosc.Controls.Add(kontrolka);
		kontrolka.BringToFront();

		if (ModifierKeys != Keys.Control)
		{
			foreach (var istniejace in doUsuniecia)
			{
				istniejace.Dispose();
			}
		}

		kontrolka.Focus();
	}

	public static void Pokaz()
	{
		using var okno = new GlowneOkno();
		Application.Run(okno);
	}
}
#endif