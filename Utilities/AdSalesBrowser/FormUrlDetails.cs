using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace AdSalesBrowser
{
	public partial class FormUrlDetails : MetroForm
	{
		private readonly string _url;

		public FormUrlDetails(string url)
		{
			InitializeComponent();
			_url = url;
			laWebAddressValue.Text = _url;
		}

		private void buttonXEmail_Click(object sender, System.EventArgs e)
		{
			try
			{
				Process.Start(Uri.EscapeUriString(String.Format("mailto:{0}?Body={1}", String.Empty, _url)));
			}
			catch { }
			Close();
		}

		private void buttonXCopy_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetText(_url);
			Close();
		}

		private void FormUrlDetails_Shown(object sender, EventArgs e)
		{
			laTitleText.ForeColor = Color.Green;
		}
	}
}
