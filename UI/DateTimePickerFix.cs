namespace System.Windows.Forms;

class DateTimePickerFix : DateTimePicker
{
	protected override void OnKeyDown(KeyEventArgs e)
	{
		if (e.Modifiers == Keys.Control)
		{
			if (e.KeyCode == Keys.C)
			{
				Clipboard.SetText(Text);
				e.Handled = true;
				e.SuppressKeyPress = true;
				return;
			}
			if (e.KeyCode == Keys.V)
			{
				try
				{
					Text = Clipboard.GetText();
				}
				catch (FormatException)
				{
				}
				e.Handled = true;
				e.SuppressKeyPress = true;
				return;
			}
		}
		base.OnKeyDown(e);
	}
}
