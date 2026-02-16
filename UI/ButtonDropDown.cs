using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace ProFak.UI;

class ButtonDropDown : Button
{
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public ContextMenuStrip? Menu { get; set; }

	public ButtonDropDown()
	{
	}

	protected override void OnMouseDown(MouseEventArgs mevent)
	{
		if (Menu != null && mevent.Button == MouseButtons.Left && mevent.X >= Width - Height)
		{
			Menu.Show(this, 0, Height);
			return;
		}
		base.OnMouseDown(mevent);
	}

	protected override void OnPaint(PaintEventArgs pevent)
	{
		base.OnPaint(pevent);

		if (Menu == null) return;

		// Draw the arrow glyph on the right side of the button
		int arrowX = ClientRectangle.Width - 14;
		int arrowY = ClientRectangle.Height / 2 - 1;

		var arrowBrush = Enabled ? SystemBrushes.ControlText : SystemBrushes.ButtonShadow;
		var arrows = new[] { new Point(arrowX, arrowY), new Point(arrowX + 7, arrowY), new Point(arrowX + 3, arrowY + 4) };
		pevent.Graphics.FillPolygon(arrowBrush, arrows);

		// Draw a dashed separator on the left of the arrow
		int lineX = ClientRectangle.Width - Height;
		int lineYFrom = arrowY - 4;
		int lineYTo = arrowY + 8;
		using (var separatorPen = new Pen(Brushes.DarkGray) { DashStyle = DashStyle.Dot })
		{
			pevent.Graphics.DrawLine(separatorPen, lineX, lineYFrom, lineX, lineYTo);
		}
	}
}
