using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	abstract class AdapterAkcji
	{
		public abstract string Nazwa { get; }
		public abstract bool CzyDostepna { get; }
		public abstract bool CzyDomyslna { get; }

		public abstract void Uruchom();
	}

	class AdapterAkcji<TRekord> : AdapterAkcji
		where TRekord : Rekord<TRekord>
	{
		private readonly AkcjaNaSpisie<TRekord> akcja;
		private readonly ISpis<TRekord> spis;

		public override string Nazwa => akcja.Nazwa;
		public override bool CzyDostepna => akcja.CzyDostepnaDlaRekordow(spis.WybraneRekordy);
		public override bool CzyDomyslna => akcja.CzyDomyslna;

		public AdapterAkcji(AkcjaNaSpisie<TRekord> akcja, ISpis<TRekord> spis)
		{
			this.akcja = akcja;
			this.spis = spis;
		}

		public override void Uruchom()
		{
			try
			{
				akcja.Uruchom(spis.Kontekst, spis.WybraneRekordy);
				if (spis.Kontekst.Dialog == null || spis.Kontekst.Dialog.DialogResult == System.Windows.Forms.DialogResult.None)
				{
					spis.Przeladuj();
				}
			}
			catch (ApplicationException ae) when (ae.GetType() == typeof(ApplicationException))
			{
				MessageBox.Show(ae.Message, "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (Exception exc)
			{
				MessageBox.Show($"Wystąpił nieobsłużony błąd. Uruchom ponownie program i spróbuj ponownie wykonać operację.\n\n{exc}", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
