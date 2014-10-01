using System;
using System.Drawing;
using System.Windows.Forms;
using NewBizWiz.Core.Common;

namespace NewBizWiz.CommonGUI.Floater
{
	public class FloaterManager
	{
		public const string FloatedMarker = "Floated";
		private int _floaterPositionX = int.MinValue;
		private int _floaterPositionY = int.MinValue;

		public void ShowFloater(Form sender, Image logo, Action afterShow, Action afterHide, Action afterBack)
		{
			var x = _floaterPositionX == Int32.MinValue ? sender.Left + sender.Width - 40 : _floaterPositionX;
			var y = _floaterPositionY == Int32.MinValue ? (sender.Top + (sender.Height - 65) / 2) : _floaterPositionY;

			using (var form = new FormFloater(x, y, logo))
			{
				if (afterShow != null)
					form.Shown += (o, e) => afterShow();
				var formStyle = sender.FormBorderStyle;
				var minSize = sender.MinimumSize;
				var size = sender.Size;
				sender.Tag = FloatedMarker;
				sender.FormBorderStyle = FormBorderStyle.None;
				sender.MinimumSize = new Size(0, 0);
				sender.Size = new Size(0, 0);
				sender.Opacity = 0;
				var result = form.ShowDialog();
				_floaterPositionY = form.Top;
				_floaterPositionX = form.Left + form.Width;
				if (result != DialogResult.Yes)
					Utilities.Instance.MinimizeForm(sender.Handle);
				sender.Tag = null;
				sender.FormBorderStyle = formStyle;
				sender.MinimumSize = minSize;
				sender.Size = size;
				if (result == DialogResult.Yes)
				{
					if (afterBack != null)
						afterBack();
				}
				else
				{
					if (afterHide != null)
						afterHide();
					Utilities.Instance.ActivateTaskbar();
				}
			}
		}
	}
}
