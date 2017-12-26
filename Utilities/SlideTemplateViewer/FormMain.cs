using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Activities;
using Asa.Common.Core.Objects.Output;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Floater;
using Asa.Common.GUI.SlideSettingsEditors;
using Asa.SlideTemplateViewer.Properties;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro.ColorTables;

namespace Asa.SlideTemplateViewer
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;

		private FormMain()
		{
			_instance = this;
			InitializeComponent();

			Width = (Int32)(Screen.PrimaryScreen.Bounds.Width * 0.8);
			Height = (Int32)(Screen.PrimaryScreen.Bounds.Height * 0.8);
			Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
			Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;

			SlideSettingsManager.Instance.SettingsChanged += (o, e) =>
			{
				Text = AppManager.Instance.FormCaption;
			};
		}

		public static FormMain Instance
		{
			get
			{
				if (_instance == null)
					_instance = new FormMain();
				return _instance;
			}
		}

		#region GUI Event Handlers
		public void Init()
		{
			FormStateHelper.Init(this, ResourceManager.Instance.AppSettingsFolder, "add slides", false).LoadState();

			Text = AppManager.Instance.FormCaption;
			Icon = AppManager.Instance.ImageResourcesManager.MainAppIcon ?? Icon;

			ribbonTabItemSlides.Text = AppManager.Instance.TextResourcesManager.RibbonTabTitle ?? AppManager.Instance.SlideManager.TabTitle ?? ribbonTabItemSlides.Text;
			ribbonBarSlidesLogo.Text = Environment.UserName;
			labelItemSlidesLogo.Image = AppManager.Instance.ImageResourcesManager.MainAppRibbonLogo ?? AppManager.Instance.SlideManager.RibbonBarLogo ?? Resources.AddSlidesLogo;
			labelItemAppTitle.Text = AppManager.Instance.TextResourcesManager.RibbonTabTitle ?? labelItemAppTitle.Text;
			itemContainerStatusBarMainInfo.RecalcSize();
			barBottom.RecalcLayout();

			buttonItemSlidesPowerPoint.Image =
				AppManager.Instance.ImageResourcesManager.RibbonOutputImage ?? buttonItemSlidesPowerPoint.Image;
			buttonItemSlidesPowerPoint.Click += TabSlidesMainPage.Instance.buttonItemSlidesPowerPoint_Click;

			buttonItemSlidesPreview.Image = AppManager.Instance.ImageResourcesManager.RibbonPreviewImage ?? buttonItemSlidesPreview.Image;
			buttonItemSlidesPreview.Click += TabSlidesMainPage.Instance.buttonItemSlidesPreview_Click;

			buttonItemApplicationMenuSlideSettings.Image = AppManager.Instance.ImageResourcesManager.MainMenuSlideSettingsImage ??
												 buttonItemApplicationMenuSlideSettings.Image;
			buttonItemApplicationMenuHelp.Image = AppManager.Instance.ImageResourcesManager.MainMenuHelpImage ??
												 buttonItemApplicationMenuHelp.Image;
			buttonItemApplicationMenuExit.Image = AppManager.Instance.ImageResourcesManager.MainMenuExitImage ??
												 buttonItemApplicationMenuExit.Image;

			
			buttonItemQatFloater.Image = AppManager.Instance.ImageResourcesManager.QatFloaterImage ??
									  buttonItemQatFloater.Image;
			buttonItemQatHelp.Image = AppManager.Instance.ImageResourcesManager.QatHelpImage ??
									  buttonItemQatHelp.Image;

			buttonItemApplicationMenuSlideSettings.Visible =
				MasterWizardManager.Instance.MasterWizards.Count > 1 ||
				(MasterWizardManager.Instance.MasterWizards.Count == 1 && SlideSettings.GetAvailableConfigurations().Count(MasterWizardManager.Instance.MasterWizards.First().Value.HasSlideConfiguration) > 1);

			if (AppManager.Instance.FormStyleManager.Style.AccentColor.HasValue)
				styleManager.MetroColorParameters = new MetroColorGeneratorParameters(
					styleManager.MetroColorParameters.CanvasColor,
					AppManager.Instance.FormStyleManager.Style.AccentColor.Value);

			if (AppManager.Instance.FormStyleManager.Style.StatusBarTextColor.HasValue)
			{
				labelItemAppTitle.ForeColor = AppManager.Instance.FormStyleManager.Style.StatusBarTextColor.Value;
				labelItemSlideSize.ForeColor = AppManager.Instance.FormStyleManager.Style.StatusBarTextColor.Value;
			}

			SlideSettingsManager.Instance.SettingsChanged += OnSlideSettingsChanged;
			OnSlideSettingsChanged(null, EventArgs.Empty);
		}

		private void OnSlideSettingsChanged(Object sender, EventArgs e)
		{
			labelItemSlideSize.Text = String.Format("Slide Size:  {0}", SlideSettingsManager.Instance.SlideSettings.SizeFormatted);
			itemContainerStatusBarAdditionalInfo.RecalcSize();
			barBottom.RecalcLayout();
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			Utilities.ActivatePowerPoint(AppManager.Instance.PowerPointManager.Processor.PowerPointObject);
			Controls.Add(TabSlidesMainPage.Instance);
			TabSlidesMainPage.Instance.BringToFront();
			AppManager.Instance.ActivateMainForm();
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			AppManager.Instance.ActivityManager.AddActivity(new UserActivity("Application Closed"));
		}

		private void FormMain_Resize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState != FormWindowState.Minimized && f.Tag != FloaterManager.FloatedMarker)
				Opacity = 1;
		}
		#endregion

		#region Ribbon Buttons's Clicks Event Handlers
		public void OnFloaterClick(object sender, EventArgs e)
		{
			var formSender = sender as Form;
			if (formSender != null && formSender.IsDisposed) return;
			AppManager.Instance.ShowFloater(
				formSender,
				new FloaterRequestedEventArgs());
		}

		public void OnExitClick(object sender, EventArgs e)
		{
			Close();
		}

		private void OnHelpClick(object sender, EventArgs e)
		{
			AppManager.Instance.HelpManager.OpenHelpLink("Slides");
		}

		private void OnSlideSettingsClick(object sender, EventArgs e)
		{
			using (var form = new FormEditSlideSettings(AppManager.Instance.PowerPointManager.Processor))
			{
				form.ShowDialog(this);
			}
		}

		private void OnRibbonExpandedChanged(object sender, EventArgs e)
		{
			buttonItemExpand.Visible = !ribbonControl.Expanded;
			buttonItemCollapse.Visible = ribbonControl.Expanded;
			buttonItemPin.Visible = false;
			ribbonControl.RecalcLayout();
		}

		private void OnRibbonAfterPanelPopup(object sender, EventArgs e)
		{
			buttonItemExpand.Visible = false;
			buttonItemCollapse.Visible = false;
			buttonItemPin.Visible = true;
			ribbonControl.RecalcLayout();
		}

		private void OnRibbonAfterPanelPopupClose(object sender, EventArgs e)
		{
			buttonItemExpand.Visible = !ribbonControl.Expanded;
			buttonItemCollapse.Visible = ribbonControl.Expanded;
			buttonItemPin.Visible = false;
			ribbonControl.RecalcLayout();
		}

		private void OnRibbonExpandClick(object sender, EventArgs e)
		{
			ribbonControl.Expanded = true;
		}

		private void OnRibbonCollapseClick(object sender, EventArgs e)
		{
			ribbonControl.Expanded = false;
		}

		private void OnRibbonPinClick(object sender, EventArgs e)
		{
			ribbonControl.Expanded = true;
		}
		#endregion
	}
}