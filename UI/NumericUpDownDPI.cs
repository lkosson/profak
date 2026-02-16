namespace ProFak.UI
{
	class NumericUpDownDPI : NumericUpDown
	{
		public NumericUpDownDPI()
		{
		}

		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			AutoScaleMode = AutoScaleMode.None;
		}
	}
}
