using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;

namespace Asa.Common.GUI.Floater
{
	public class FloaterManager
	{
		public const string FloatedMarker = "Floated";
		private int _floaterPositionX = int.MinValue;
		private int _floaterPositionY = int.MinValue;

		public void ShowFloater(IFloaterSupportedForm sender, String title, Image logo, Action afterShow, Action afterHide, Action<bool> afterBack)
		{
			var parentForm = (Form)sender;
			var x = _floaterPositionX == Int32.MinValue ? parentForm.Left + parentForm.Width - 40 : _floaterPositionX;
			var y = _floaterPositionY == Int32.MinValue ? (parentForm.Top + (parentForm.Height - 65) / 2) : _floaterPositionY;

			var isMaximized = parentForm.WindowState == FormWindowState.Maximized;

			var form = new FormFloater(x, y, title, logo);
			form.Shown += (o, e) =>
			{
				parentForm.Tag = FloatedMarker;
				parentForm.Opacity = 0;
				afterShow?.Invoke();
			};
			form.Closing += (o, e) =>
			{
				_floaterPositionY = form.Top;
				_floaterPositionX = form.Left + form.Width;
			};
			form.Closed += (o, e) =>
			{
				var result = form.DialogResult;
				if (result != DialogResult.Yes)
					Utilities.MinimizeForm(parentForm.Handle);
				parentForm.Tag = null;
				sender.ShowAfterFloater();
				if (result == DialogResult.Yes)
				{
					afterBack?.Invoke(isMaximized);
				}
				else
				{
					afterHide?.Invoke();
					Utilities.ActivateTaskbar();
				}
				form.Dispose();
			};
			form.Show();
		}
	}
}
