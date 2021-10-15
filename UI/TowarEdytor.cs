﻿using ProFak.DB;
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
	partial class TowarEdytor : UserControl, IEdytor<Towar>
	{
		public Towar Rekord { get { return bindingSource.DataSource as Towar; } set { bindingSource.DataSource = value; } }
		public Kontekst Kontekst { get; set; }

		public TowarEdytor()
		{
			InitializeComponent();
			comboBoxRodzaj.DataSource = Enum.GetValues(typeof(RodzajTowaru)).Cast<RodzajTowaru>().Select(r => new PozycjaListy<RodzajTowaru> { Wartosc = r, Opis = r.ToString() }).ToArray();
			comboBoxRodzaj.DisplayMember = "Opis";
			comboBoxRodzaj.ValueMember = "Wartosc";
		}

		protected override void OnCreateControl()
		{
			bindingSourceJednostkaMiary.DataSource = Kontekst.Baza.JednostkiMiar.ToList();
			bindingSourceStawkaVat.DataSource = Kontekst.Baza.StawkiVat.ToList();
			base.OnCreateControl();
		}

		protected override void OnValidating(CancelEventArgs e)
		{
			Rekord.CzyWedlugCenBrutto = comboBoxSposobLiczenia.SelectedIndex == 1;
			Rekord.CzyArchiwalny = comboBoxWidocznosc.SelectedIndex == 1;
			Rekord.Rodzaj = (RodzajTowaru)comboBoxRodzaj.SelectedValue;
			base.OnValidating(e);
		}

		private void bindingSource_DataSourceChanged(object sender, EventArgs e)
		{
			comboBoxRodzaj.SelectedValue = Rekord.Rodzaj;
			comboBoxSposobLiczenia.SelectedIndex = Rekord.CzyWedlugCenBrutto ? 1 : 0;
			comboBoxWidocznosc.SelectedIndex = Rekord.CzyArchiwalny ? 1 : 0;
		}

		private void buttonStawkaVat_Click(object sender, EventArgs e)
		{
			var stawkaVat = Spis.Wybierz(Kontekst, Spis.StawkiVat, "Wybierz stawkę Vat");
			if (stawkaVat == null) return;
			bindingSourceStawkaVat.DataSource = Kontekst.Baza.StawkiVat.ToList();
			Rekord.StawkaVatRef = stawkaVat.Ref;
			bindingSource.ResetCurrentItem();
		}

		private void buttonJednostkaMiary_Click(object sender, EventArgs e)
		{
			var jednostkaMiary = Spis.Wybierz(Kontekst, Spis.JednostkiMiar, "Wybierz jednostkę miary");
			if (jednostkaMiary == null) return;
			bindingSourceJednostkaMiary.DataSource = Kontekst.Baza.JednostkiMiar.ToList();
			Rekord.JednostkaMiaryRef = jednostkaMiary.Ref;
			bindingSource.ResetCurrentItem();
		}
	}
}