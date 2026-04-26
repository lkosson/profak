#if AVALONIA
using Avalonia.Controls;

namespace ProFak.UI;

class PionowoAV : StackPanel
{
	private readonly bool wysrodkowane;

	public PionowoAV(TControl[] kontrolki, bool wysrodkowane = false)
	{
		this.wysrodkowane = wysrodkowane;
		foreach (var kontrolka in kontrolki)
		{
			Children.Add(kontrolka);
		}
	}

	public void DodajWiersz(TControl kontrolka)
	{
		Children.Add(kontrolka);
	}

	public void OgraniczSzerokosc(int szerokosc)
	{
	}

	protected void SuspendLayout() { }
	protected void ResumeLayout() { }
}
#endif