namespace ProFak.UI;

class ButtonDPI : Button
{
	public ButtonDPI()
	{
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();
		Height = 23 * DeviceDpi / 96;
		if (Text == "..." || Text == "➕")
		{
			AutoSize = false;
			Width = Height + 3;
		}
		if (Text == "➕" && !Wyglad.IkonyAkcji) Text = "+";
	}
}
