using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using AdSalesBrowser.Configuration;
using AdSalesBrowser.Helpers;
using AdSalesBrowser.PowerPoint;
using AdSalesBrowser.SalesLibraryExtensions;
using Asa.Common.GUI.ToolForms;
using DevExpress.Utils;
using DevExpress.XtraTab;
using EO.WebBrowser;
using EO.WebBrowser.WinForm;
using Padding = System.Windows.Forms.Padding;

namespace AdSalesBrowser.WebPage
{
	public sealed partial class WebKitPage : XtraTabPage
	//public sealed partial class WebKitPage : UserControl
	{
		private readonly WebControl _webKit;
		private readonly string _startUrl;
		private bool _initialLoadComplete;

		public event EventHandler<NewPageEventArgs> OnNavigateNewPage;
		public event EventHandler<ClosePageEventArgs> OnClosePage;

		public string CurrentUrl => _webKit.WebView.Url;

		public WebKitPage()
		{
			InitializeComponent();
			_webKit = new WebControl();
			_webKit.Dock = DockStyle.Fill;
			Controls.Add(_webKit);
			_webKit.WebView = new WebView();
			_webKit.WebView.CustomUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Essential Objects Chrome/41.0.2272.16 Safari/537.36";
			Text = _webKit.WebView.Title;

			pbProgressLogo.Image = AppSettingsManager.Instance.SplashLogo;

			InitSiteLoading();
			InitDownloading();
			InitExternalBrowsersCommandIds();
			InitSalesLibraryExtensions();
		}

		public WebKitPage(string url) : this()
		{
			_startUrl = url;
		}

		#region Site Loading
		public void Navigate()
		{
			ResizeProgressBar();
			pnProgress.BringToFront();
			circularProgress.IsRunning = true;
			Application.DoEvents();
			_webKit.WebView.LoadUrl(_startUrl);
		}

		private void InitSiteLoading()
		{
			_webKit.WebView.TitleChanged += OnWebViewTitleChanged;
			_webKit.WebView.LoadCompleted += OnWebViewLoadCompleted;
			_webKit.WebView.LoadFailed += OnWebViewLoadFailed;
			_webKit.WebView.UrlChanged += OnWebViewUrlChanged;
			_webKit.WebView.BeforeContextMenu += OnWebViewBeforeContextMenu;
			_webKit.WebView.NewWindow += OnWebViewNewWindow;
			_webKit.WebView.LaunchUrl += OnWebViewLaunchUrl;
			_webKit.WebView.CanGoBackChanged += OnWebViewNavigationStateChaged;
			_webKit.WebView.CanGoForwardChanged += OnWebViewNavigationStateChaged;
			_webKit.WebView.BeforeContextMenu += OnWebViewBeforeContextMenu;
			_webKit.WebView.Command += OnWebViewExternalBrowserOpenCommand;
		}

		public void Release()
		{
			_webKit.WebView.Close(true);
		}

		private void OnWebViewTitleChanged(object sender, EventArgs eventArgs)
		{
			Text = ((WebView)sender).Title;
		}

		private void OnWebViewBeforeContextMenu(object sender, BeforeContextMenuEventArgs e)
		{
			if (AppSettingsManager.Instance.EnableMenu)
			{
				if (!String.IsNullOrEmpty(e.MenuInfo.LinkUrl) &&
					_extensionManager.IsExtensionsActive &&
					_extensionManager.IsUrlExternal(e.MenuInfo.LinkUrl))
				{
					e.Menu.Items.Clear();
					foreach (var commandId in _externalBrowsersCommandIds)
						e.Menu.Items.Add(new EO.WebBrowser.MenuItem(commandId.Key, commandId.Value));
				}
			}
			else
				e.Menu.Items.Clear();
		}

		private void OnWebViewLoadFailed(object sender, LoadFailedEventArgs e)
		{
			if (!_initialLoadComplete && ShowCloseButton != DefaultBoolean.False && OnClosePage != null)
				OnClosePage(this, new ClosePageEventArgs { Page = this, NeedReleasePage = e.ErrorCode != ErrorCode.ProceedAsDownload });
			else
			{
				circularProgress.IsRunning = false;
				pnProgress.SendToBack();
				_webKit.BringToFront();
			}
		}

		private void OnWebViewLoadCompleted(object sender, LoadCompletedEventArgs e)
		{
			circularProgress.IsRunning = false;
			pnProgress.SendToBack();
			_webKit.BringToFront();
			UpdateYouTubeState();
			_initialLoadComplete = true;
		}

		private void OnWebViewUrlChanged(object sender, EventArgs e)
		{
			UpdateYouTubeState();
		}

		private void OnWebViewNewWindow(object sender, NewWindowEventArgs e)
		{
			if (OnNavigateNewPage != null)
				OnNavigateNewPage(this, new NewPageEventArgs() { Url = e.TargetUrl });
			e.Accepted = false;
		}

		private void OnWebViewLaunchUrl(object sender, LaunchUrlEventArgs e)
		{
			e.UseOSHandler = true;
		}
		#endregion

		#region Download handling
		private void InitDownloading()
		{
			_webKit.WebView.FileDialog += OnProcessFileDialog;
			_webKit.WebView.BeforeDownload += OnWebViewBeforeDownload;
			_webKit.WebView.DownloadUpdated += OnWebViewDownloadUpdated;
			_webKit.WebView.DownloadCompleted += OnWebViewDownloadCompleted;
			_webKit.WebView.DownloadCanceled += OnWebViewDownloadCanceled;
		}

		private void OnProcessFileDialog(Object sender, FileDialogEventArgs e)
		{
			switch (e.Mode)
			{
				case FileDialogMode.Save:
					using (var saveDialog = new SaveFileDialog())
					{
						saveDialog.Title = e.Title;
						saveDialog.Filter = e.Filter;
						saveDialog.FileName = e.DefaultFileName;
						saveDialog.InitialDirectory = Path.Combine(
							Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
							"Downloads");
						if (saveDialog.ShowDialog(FormMain.Instance) != DialogResult.Cancel)
						{
							FormDownloadProgress.ShowProgress(FormMain.Instance);
							FormDownloadProgress.SetTitle("Downloading…");
							FormDownloadProgress.SetDetails(Path.GetFileName(saveDialog.FileName));
							FormMain.Instance.SuspendPages();
							Application.DoEvents();
							e.Continue(saveDialog.FileName);
						}
						else
							e.Cancel();
					}
					break;
			}
			e.Handled = true;
		}

		private void OnWebViewBeforeDownload(object sender, BeforeDownloadEventArgs e)
		{
			e.FilePath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
				"Downloads",
				Path.GetFileName(e.FilePath));
		}

		private void OnWebViewDownloadUpdated(Object sender, DownloadEventArgs e)
		{
			FormDownloadProgress.SetDetails(String.Format("{0} - {1}%", Path.GetFileName(e.Item.FullPath), e.Item.PercentageComplete));
			Application.DoEvents();
		}

		private void OnWebViewDownloadCompleted(Object sender, DownloadEventArgs e)
		{
			FormMain.Instance.ResumePages();
			FormDownloadProgress.CloseProgress();
			if (".MP4".Equals(Path.GetExtension(e.Item.FullPath), StringComparison.OrdinalIgnoreCase))
			{
				PowerPointSingleton.Instance.Connect();
				var activePresentation = PowerPointSingleton.Instance.GetActivePresentation();
				var allowVideoInsert = activePresentation != null && File.Exists(activePresentation.FullName);
				if (allowVideoInsert)
				{
					using (var formComplete = new FormVideoDownloadComplete(e.Item.FullPath))
					{
						var result = formComplete.ShowDialog(FormMain.Instance);
						if (result == DialogResult.Abort)
							AppManager.Instance.ShowFloater(() =>
							{
								FormProgress.ShowProgress();
								FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
								PowerPointSingleton.Instance.InsertVideoIntoActivePresentation(e.Item.FullPath);
								FormProgress.CloseProgress();
							}, null);
						return;
					}
				}
			}
			using (var formComplete = new FormFileDownloadComplete(e.Item.FullPath))
			{
				formComplete.ShowDialog(FormMain.Instance);
			}
		}

		private void OnWebViewDownloadCanceled(Object sender, DownloadEventArgs e)
		{
			FormMain.Instance.ResumePages();
			FormDownloadProgress.CloseProgress();
		}
		#endregion

		#region Navigation
		public void UpdateNavigationButtonsState()
		{
			FormMain.Instance.ButtonNavigationBack.Enabled = _webKit.WebView.CanGoBack;
			FormMain.Instance.ButtonNavigationForward.Enabled = _webKit.WebView.CanGoForward;
			FormMain.Instance.barMain.RecalcLayout();
		}

		private void OnWebViewNavigationStateChaged(Object sender, EventArgs e)
		{
			UpdateNavigationButtonsState();
		}

		public void NavigateBack()
		{
			_webKit.WebView.GoBack();
		}

		public void NavigateForward()
		{
			_webKit.WebView.GoForward();
		}

		public void Refresh()
		{
			pnProgress.BringToFront();
			circularProgress.IsRunning = true;
			_webKit.WebView.Reload();
		}
		#endregion

		#region External Web Browsers
		private readonly Dictionary<string, int> _externalBrowsersCommandIds = new Dictionary<String, Int32>();

		private int _commandIdOpenChrome;
		private int _commandIdOpenFirefox;
		private int _commandIdOpenIE;
		private int _commandIdOpenEdge;

		private void InitExternalBrowsersCommandIds()
		{
			foreach (var browserTag in ExternalBrowserManager.AvailableBrowsers.Keys)
			{
				switch (browserTag)
				{
					case ExternalBrowserManager.BrowserChromeTag:
						_commandIdOpenChrome = CommandIds.RegisterUserCommand(ExternalBrowserManager.BrowserChromeTag);
						_externalBrowsersCommandIds.Add("Open in Chrome", _commandIdOpenChrome);
						break;
					case ExternalBrowserManager.BrowserFirefoxTag:
						_commandIdOpenFirefox = CommandIds.RegisterUserCommand(ExternalBrowserManager.BrowserFirefoxTag);
						_externalBrowsersCommandIds.Add("Open in Firefox", _commandIdOpenFirefox);
						break;
					case ExternalBrowserManager.BrowserIETag:
						_commandIdOpenIE = CommandIds.RegisterUserCommand(ExternalBrowserManager.BrowserIETag);
						_externalBrowsersCommandIds.Add("Open in Internet Explorer", _commandIdOpenIE);
						break;
					case ExternalBrowserManager.BrowserEdgeTag:
						_commandIdOpenEdge = CommandIds.RegisterUserCommand(ExternalBrowserManager.BrowserEdgeTag);
						_externalBrowsersCommandIds.Add("Open in Edge", _commandIdOpenEdge);
						break;
				}
			}
		}

		private void OnWebViewExternalBrowserOpenCommand(Object sender, CommandEventArgs e)
		{
			if (e.CommandId == _commandIdOpenChrome)
				ExternalBrowserManager.OpenUrl(ExternalBrowserManager.BrowserChromeTag, e.MenuInfo.LinkUrl);
			else if (e.CommandId == _commandIdOpenFirefox)
				ExternalBrowserManager.OpenUrl(ExternalBrowserManager.BrowserFirefoxTag, e.MenuInfo.LinkUrl);
			else if (e.CommandId == _commandIdOpenEdge)
				ExternalBrowserManager.OpenUrl(ExternalBrowserManager.BrowserEdgeTag, e.MenuInfo.LinkUrl);
			else if (e.CommandId == _commandIdOpenIE)
				ExternalBrowserManager.OpenUrl(ExternalBrowserManager.BrowserIETag, e.MenuInfo.LinkUrl);
		}
		#endregion

		#region Sales Library Extensions
		private ExtensionManager _extensionManager;
		private WebControl _extensionDownloadView;

		private void InitSalesLibraryExtensions()
		{
			_extensionManager = new ExtensionManager();

			_extensionManager.DataChanged += OnExtensionsDataChanged;

			_webKit.WebView.RegisterJSExtensionFunction(ExtensionManager.ActivateFunctionName, OnJavaScriptCall);
			_webKit.WebView.RegisterJSExtensionFunction(ExtensionManager.SendLinkDataFunctionName, OnJavaScriptCall);
			_webKit.WebView.RegisterJSExtensionFunction(ExtensionManager.ReleaseLinkDataFunctionName, OnJavaScriptCall);
			_webKit.WebView.RegisterJSExtensionFunction(ExtensionManager.SwitchDataFunctionName, OnJavaScriptCall);

			_extensionDownloadView = new WebControl();
			_extensionDownloadView.WebView = new WebView();
			_extensionDownloadView.WebView.ShouldForceDownload += OnExtensionWebViewShouldForceDownload;
			_extensionDownloadView.WebView.BeforeDownload += OnExtensionWebViewBeforeDownload;
			_extensionDownloadView.WebView.DownloadUpdated += OnWebViewDownloadUpdated;
			_extensionDownloadView.WebView.DownloadCompleted += OnExtensionWebViewDownloadCompleted;
			_extensionDownloadView.WebView.DownloadCanceled += OnWebViewDownloadCanceled;
			_extensionDownloadView.WebView.LoadFailed += OnExtensionWebViewLoadFailed;
			Controls.Add(_extensionDownloadView);
		}

		public void UpdateExtensionsState()
		{
			FormMain.Instance.ButtonExtensionsAddSlide.Visible = false;
			FormMain.Instance.ButtonExtensionsAddSlides.Visible = false;
			FormMain.Instance.ButtonExtensionsAddVideo.Visible = false;
			FormMain.Instance.LabelExtensionsWarning.Text = String.Empty;
			if (_extensionManager.ContentEnabled)
			{
				PowerPointSingleton.Instance.Connect();
				switch (_extensionManager.CurrentLinkData.DataType)
				{
					case LinkDataType.PowerPoint:
						FormMain.Instance.ButtonExtensionsAddSlide.Visible = true;
						FormMain.Instance.ButtonExtensionsAddSlides.Visible = true;

						var slideSettings = PowerPointSingleton.Instance.GetSlideSettings();
						var powerPointData = (PowerPointData)_extensionManager.CurrentContentLinkData;
						if (slideSettings != null && !powerPointData.IsFitToInsert(slideSettings))
							FormMain.Instance.LabelExtensionsWarning.Text = "Slide Size Conflict: The slides may not insert correctly…";
						break;
					case LinkDataType.Video:
						var activePresentation = PowerPointSingleton.Instance.GetActivePresentation();
						var allowVideoInsert = activePresentation != null && File.Exists(activePresentation.FullName);
						FormMain.Instance.ButtonExtensionsAddVideo.Visible = allowVideoInsert;
						if (activePresentation != null && !allowVideoInsert)
							FormMain.Instance.LabelExtensionsWarning.Text = "Save your presentation if you want to add this video…";
						break;
				}
			}
			if (_extensionManager.CurrentLinkData?.DataType == LinkDataType.Lan)
			{
				OpenLanLink(_extensionManager.CurrentLinkData.OriginalFileUrl);
				_extensionManager.ReleaseData();
			}
			if (_extensionManager.CurrentLinkData?.DataType == LinkDataType.App)
			{
				OpenFileLink(((AppLinkData)_extensionManager.CurrentLinkData).GetExecutablePaths());
				_extensionManager.ReleaseData();
			}
			if (_extensionManager.CurrentLinkData?.DataType == LinkDataType.Excel)
			{
				DownloadFile(_extensionManager.CurrentLinkData.OriginalFileUrl);
			}
			FormMain.Instance.barMain.RecalcLayout();
		}

		private void DownloadFile(string url)
		{
			FormDownloadProgress.ShowProgress(FormMain.Instance);
			FormDownloadProgress.SetTitle("Downloading…");
			FormMain.Instance.SuspendPages();
			Application.DoEvents();
			_extensionDownloadView.WebView.LoadUrl(url.Replace(@"SalesLibraries/SalesLibraries", "SalesLibraries"));
		}

		private void OpenLanLink(string linkPath)
		{
			var linkPathAvailable = false;
			FormProgress.ShowProgress(
				"Scanning your network for this location…", () =>
				{
					try
					{
						linkPathAvailable = Directory.Exists(linkPath);
						if (linkPathAvailable)
							Process.Start(linkPath);
					}
					catch { }
				},
				false);
			if (!linkPathAvailable)
				MessageBox.Show("Your Browser does not allow access to this network location…", "Warning", MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
		}

		private void OpenFileLink(IEnumerable<string> executablePaths)
		{
			foreach (var executablePath in executablePaths)
			{
				try
				{
					Process.Start(executablePath);
					break;
				}
				catch { }
			}
		}

		private void OnJavaScriptCall(object sender, JSExtInvokeArgs e)
		{
			switch (e.FunctionName)
			{
				case ExtensionManager.ActivateFunctionName:
					_extensionManager.Activate(_startUrl);
					break;
				case ExtensionManager.SendLinkDataFunctionName:
					_extensionManager.LoadData(e.Arguments);
					break;
				case ExtensionManager.ReleaseLinkDataFunctionName:
					_extensionManager.ReleaseData();
					break;
				case ExtensionManager.SwitchDataFunctionName:
					_extensionManager.SwitchDocumentPage(e.Arguments);
					break;
			}
		}

		private void OnExtensionsDataChanged(Object sender, EventArgs e)
		{
			UpdateExtensionsState();
		}

		private void OnExtensionWebViewBeforeDownload(Object sender, BeforeDownloadEventArgs e)
		{
			e.ShowDialog = false;
			e.FilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(e.FilePath));
		}

		private void OnExtensionWebViewDownloadCompleted(Object sender, DownloadEventArgs e)
		{
			FormMain.Instance.ResumePages();
			FormDownloadProgress.CloseProgress();

			AppManager.Instance.ShowFloater(() =>
			{
				if (_extensionManager.CurrentContentLinkData?.DataType == LinkDataType.Video)
				{
					FormProgress.ShowProgress();
					FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
					PowerPointSingleton.Instance.InsertVideoIntoActivePresentation(e.Item.FullPath);
					FormProgress.CloseProgress();
				}
				else if (_extensionManager.CurrentContentLinkData?.DataType == LinkDataType.PowerPoint)
				{
					FormProgress.ShowProgress();
					FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
					PowerPointSingleton.Instance.AppendSlidesFromFile(e.Item.FullPath);
					FormProgress.CloseProgress();
				}
				else if (_extensionManager.CurrentLinkData?.DataType == LinkDataType.Excel)
				{
					OpenFileLink(new[] {e.Item.FullPath});
					_extensionManager.ReleaseData();
				}
			}, null);
		}

		private void OnExtensionWebViewShouldForceDownload(object sender, ShouldForceDownloadEventArgs e)
		{
			e.ForceDownload = true;
		}

		private void OnExtensionWebViewLoadFailed(object sender, LoadFailedEventArgs e)
		{
			if (e.ErrorCode == ErrorCode.ProceedAsDownload) return;
			FormMain.Instance.ResumePages();
			FormDownloadProgress.CloseProgress();
		}

		public void AddVideo()
		{
			if (!PowerPointManager.Instance.CheckPowerPointRunning(UpdateExtensionsState)) return;
			var activePresentation = PowerPointSingleton.Instance.GetActivePresentation();
			if (activePresentation != null && File.Exists(activePresentation.FullName))
			{
				DownloadFile(_extensionManager.CurrentContentLinkData.GetPartFileUrl());
			}
		}

		public void AddSlide()
		{
			if (!PowerPointManager.Instance.CheckPowerPointRunning(UpdateExtensionsState)) return;
			DownloadFile(_extensionManager.CurrentContentLinkData.GetPartFileUrl());
		}

		public void AddSlides()
		{
			if (!PowerPointManager.Instance.CheckPowerPointRunning(UpdateExtensionsState)) return;
			DownloadFile(_extensionManager.CurrentContentLinkData.OriginalFileUrl);
		}
		#endregion

		#region YouTube extensions
		public void UpdateYouTubeState()
		{
			FormMain.Instance.ButtonExtensionsDownloadYouTube.Visible = YouTubeHelper.IsUrlYouTubeWatch(_webKit.WebView.Url);
			FormMain.Instance.barMain.RecalcLayout();
		}

		public void DownloadYouTube()
		{
			_webKit.WebView.EvalScript(
				"(function(w,d,x){x=new(window.XMLHttpRequest||ActiveXObject)('Microsoft.XMLHTTP');x.onreadystatechange=function(){if(x.readyState==4){if(x.status==200)d.body.appendChild(d.createElement('script')).src=JSON.parse(x.responseText).query.results.url;else console.log('ERR',x.status,x.statusText)}};x.open('GET','//query.yahooapis.com/v1/public/yql?q='+encodeURIComponent('select * from json where url=\"http://noflr.deturl.com/l.asp\"')+'&format=json');x.send()})(window,document);");
		}
		#endregion

		#region Splash Processing
		private void ResizeProgressBar()
		{
			var padding = (Width - 420) / 2;
			pnProgress.Padding = new Padding(padding, 50, padding, 0);
		}

		private void OnWebPageResize(object sender, EventArgs e)
		{
			ResizeProgressBar();
		}
		#endregion
	}
}
