#if AVALONIA
using Avalonia.Controls;

namespace ProFak.UI;

class Pionowo : StackPanel
{
	protected override Type StyleKeyOverride => typeof(StackPanel);

	public Pionowo(TControl[] kontrolki)
	{
		foreach (var kontrolka in kontrolki)
		{
			DodajWiersz(kontrolka);
		}
	}

	public void DodajWiersz(TControl kontrolka)
	{
		Children.Add(kontrolka);
	}

	public void OgraniczSzerokosc(int szerokosc)
	{
		MaxWidth = szerokosc;
	}

	protected void SuspendLayout() { }
	protected void ResumeLayout() { }
}
#endif