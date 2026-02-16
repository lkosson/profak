namespace System.Windows.Forms
{
	class ComboBoxFix : ComboBox
	{
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (!Focused) Select(0, 0);
		}
	}
}
