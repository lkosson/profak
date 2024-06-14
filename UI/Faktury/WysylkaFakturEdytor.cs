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

namespace ProFak.UI.Faktury;

partial class WysylkaFakturEdytor : UserControl
{
	public IEnumerable<Faktura> Faktury { get; set; }
	public Kontekst Kontekst { get; set; }

	private string szablonAdresat;
	private string szablonTemat;
	private string szablonTresc;

	public WysylkaFakturEdytor()
	{
		InitializeComponent();
	}

	protected override void OnLoad(EventArgs e)
	{
		var konfiguracja = Kontekst.Baza.Konfiguracja.First();
		szablonAdresat = "[NABYWCA-NAZWA] <[NABYWCA-EMAIL]>";
		szablonTemat = konfiguracja.EMailTemat;
		szablonTresc = konfiguracja.EMailTresc;
		var pozycje = new List<Faktura>();
		pozycje.Add(new Faktura { Numer = "(wszystkie)", Id = 0 });
		pozycje.AddRange(Faktury.OrderBy(e => e.DataWystawienia).ThenBy(e => e.Id));
		comboBoxFaktura.DataSource = pozycje;
		base.OnLoad(e);
	}

	private void comboBoxFaktura_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (comboBoxFaktura.SelectedIndex == 0)
		{
			textBoxAdresat.Text = szablonAdresat;
			textBoxTemat.Text = szablonTemat;
			textBoxTresc.Text = szablonTresc;
		}
		else
		{
			var faktura = (Faktura)comboBoxFaktura.SelectedItem;
			textBoxAdresat.Text = faktura.PodstawPolaWysylki(szablonAdresat);
			textBoxTemat.Text = faktura.PodstawPolaWysylki(szablonTemat);
			textBoxTresc.Text = faktura.PodstawPolaWysylki(szablonTresc);
		}
	}

	private void buttonPoprzednia_Click(object sender, EventArgs e)
	{
		if (comboBoxFaktura.SelectedIndex == 0) return;
		comboBoxFaktura.SelectedIndex--;
	}

	private void buttonNastepna_Click(object sender, EventArgs e)
	{
		if (comboBoxFaktura.SelectedIndex == comboBoxFaktura.Items.Count - 1) return;
		comboBoxFaktura.SelectedIndex++;
	}

	private void buttonWyslij_Click(object sender, EventArgs e)
	{
		var idx = comboBoxFaktura.SelectedIndex;
		if (idx == 0) WyslijWszystkie();
		else WyslijBiezaca();
	}

	private void WyslijBiezaca()
	{
		var idx = comboBoxFaktura.SelectedIndex;
		var faktura = (Faktura)comboBoxFaktura.SelectedItem;
		OknoPostepu.Uruchom(async delegate
		{
			await Wyslij(faktura);
		});
		var faktury = (List<Faktura>)comboBoxFaktura.DataSource;
		faktury.Remove(faktura);
		comboBoxFaktura.BeginUpdate();
		comboBoxFaktura.DataSource = Array.Empty<Faktura>();
		comboBoxFaktura.DataSource = faktury;
		comboBoxFaktura.DisplayMember = "Numer";
		comboBoxFaktura.SelectedIndex = Math.Min(idx, faktury.Count - 1);
		comboBoxFaktura.EndUpdate();

		if (faktury.Count == 1)
		{
			ParentForm.DialogResult = DialogResult.OK;
			ParentForm.Close();
		}
	}

	private void WyslijWszystkie()
	{
		OknoPostepu.Uruchom(async delegate
		{
			var faktury = (List<Faktura>)comboBoxFaktura.DataSource;
			foreach (var faktura in faktury)
			{
				if (faktura.Id == 0) continue;
				await Wyslij(faktura);
			}
		});
		ParentForm.DialogResult = DialogResult.OK;
		ParentForm.Close();
	}

	private async Task Wyslij(Faktura faktura)
	{
		await Task.Delay(1000);
	}
}
