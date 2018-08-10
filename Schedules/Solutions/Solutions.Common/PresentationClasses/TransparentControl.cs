using System;
using System.Drawing;
using System.Windows.Forms;

namespace Asa.Solutions.Common.PresentationClasses
{
	public class TransparentControl: UserControl
	{
		public bool _drag = false;
		public bool _enabled = false;
		private int _opacity = 100;

		private int alpha;

		public TransparentControl()
		{
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			SetStyle(ControlStyles.Opaque, true);
			BackColor = Color.Transparent;
		}

		public int Opacity
		{
			get
			{
				if (_opacity > 100)
				{
					_opacity = 100;
				}
				else if (_opacity < 1)
				{
					_opacity = 1;
				}
				return _opacity;
			}
			set
			{
				_opacity = value;
				Parent?.Invalidate(Bounds, true);
			}
		}

		protected override CreateParams CreateParams
		{
			get
			{
				var cp = base.CreateParams;
				cp.ExStyle = cp.ExStyle | 0x20;
				return cp;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			var bounds = new Rectangle(0, 0, Width - 1, Height - 1);

			var formColor = Parent.BackColor;
			Brush backColor;

			alpha = (_opacity * 255) / 100;

			if (_drag)
			{
				Color dragBckColor;

				if (BackColor != Color.Transparent)
				{
					var rb = BackColor.R * alpha / 255 + formColor.R * (255 - alpha) / 255;
					var gb = BackColor.G * alpha / 255 + formColor.G * (255 - alpha) / 255;
					var bb = BackColor.B * alpha / 255 + formColor.B * (255 - alpha) / 255;
					dragBckColor = Color.FromArgb(rb, gb, bb);
				}
				else
				{
					dragBckColor = formColor;
				}

				alpha = 255;
				backColor = new SolidBrush(Color.FromArgb(alpha, dragBckColor));
			}
			else
			{
				backColor = new SolidBrush(Color.FromArgb(alpha, BackColor));
			}

			if (BackColor != Color.Transparent | _drag)
			{
				g.FillRectangle(backColor, bounds);
			}

			backColor.Dispose();
			g.Dispose();
			base.OnPaint(e);
		}

		protected override void OnBackColorChanged(EventArgs e)
		{
			Parent?.Invalidate(Bounds, true);
			base.OnBackColorChanged(e);
		}

		protected override void OnParentBackColorChanged(EventArgs e)
		{
			Invalidate();
			base.OnParentBackColorChanged(e);
		}
	}
}
