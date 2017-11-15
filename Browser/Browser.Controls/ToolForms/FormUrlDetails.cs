using System;
using System.Diagnostics;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;

namespace Asa.Browser.Controls.ToolForms
{
	public partial class FormUrlDetails : MetroForm
	{
		private readonly string _url;

		public FormUrlDetails(string url)
		{
			InitializeComponent();
			_url = url;
			simpleLabelItemWebAddressValue.Text = _url;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MaxSize, scaleFactor);
			layoutControlItemLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MinSize, scaleFactor);
			layoutControlItemEmail.MaxSize = RectangleHelper.ScaleSize(layoutControlItemEmail.MaxSize, scaleFactor);
			layoutControlItemEmail.MinSize = RectangleHelper.ScaleSize(layoutControlItemEmail.MinSize, scaleFactor);
			layoutControlItemCopy.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCopy.MaxSize, scaleFactor);
			layoutControlItemCopy.MinSize = RectangleHelper.ScaleSize(layoutControlItemCopy.MinSize, scaleFactor);
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, scaleFactor);
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, scaleFactor);
		}

		private void OnEmailClick(object sender, System.EventArgs e)
		{
			try
			{
				Process.Start(Uri.EscapeUriString(String.Format("mailto:{0}?Body={1}", String.Empty, _url)));
			}
			catch { }
			Close();
		}

		private void OnCopyClick(object sender, EventArgs e)
		{
			Clipboard.SetText(_url);
			Close();
		}
	}
}
