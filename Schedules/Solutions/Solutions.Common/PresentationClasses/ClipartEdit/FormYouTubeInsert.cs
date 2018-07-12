using System;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using EO.WebBrowser;

namespace Asa.Solutions.Common.PresentationClasses.ClipartEdit
{
	public partial class FormYouTubeInsert : MetroForm
	{
		public YouTubeClipartObject YouTubeInfo { get; private set; }

		public FormYouTubeInsert()
		{
			InitializeComponent();

			webBrowser.WebView = new WebView();
			webBrowser.WebView.NewWindow += OnNewWindow;
			webBrowser.WebView.CustomUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Essential Objects Chrome/41.0.2272.16 Safari/537.36";
			
			Shown += OnFormShown;

			buttonEditUrl.EnableSelectAll();

			layoutControlItemOK.Enabled = false;

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void OnFormShown(Object sender, EventArgs e)
		{
			webBrowser.WebView.LoadHtmlAndWait("<html><body><h3 style\"text-align: center;\">Type url to load video...</h3></body></html>");
		}

		private void OnApplyUrlButtonClick(object sender, ButtonPressedEventArgs e)
		{
			try
			{
				webBrowser.WebView.LoadHtmlAndWait("<html><body></body></html>");
				layoutControlItemProgress.Visibility = LayoutVisibility.Always;
				circularProgress.IsRunning = true;
				Application.DoEvents();

				YouTubeInfo = YouTubeClipartObject.FromUrl(buttonEditUrl.EditValue as String);
				webBrowser.WebView.LoadUrlAndWait(YouTubeInfo.EmbeddedUrl);

				circularProgress.IsRunning = false;
				layoutControlItemProgress.Visibility = LayoutVisibility.Never;
				Application.DoEvents();

				layoutControlItemOK.Enabled = true;
			}
			catch (Exception exception)
			{
				layoutControlItemOK.Enabled = false;
				YouTubeInfo = null;
				Application.DoEvents();
				webBrowser.WebView.LoadHtmlAndWait(String.Format("<html><body><h3 style\"text-align: center;\">{0}</h3></body></html>", exception.Message));
			}
		}

		private void OnUrlKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				OnApplyUrlButtonClick(sender, new ButtonPressedEventArgs(buttonEditUrl.Properties.Buttons[0]));
		}

		private void OnUrlEditValueChanged(object sender, EventArgs e)
		{
			layoutControlItemOK.Enabled = false;
			YouTubeInfo = null;
		}

		private void OnNewWindow(object sender, NewWindowEventArgs e)
		{
		}
	}
}