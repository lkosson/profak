namespace ProFak.UI;

class ButtonDPI : Button
{
	public ButtonDPI()
	{
	}

	internal void PoprawWymiary()
	{
		Height = 23 * DeviceDpi / 96;
		if (Text == "..." || Text == "➕")
		{
			AutoSize = false;
			Width = Height + 3;
		}
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();
		PoprawWymiary();
		if (Text == "➕" && !Wyglad.IkonyAkcji) Text = "+";
	}
}
