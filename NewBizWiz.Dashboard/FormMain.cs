using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Asa.CommonGUI.Common;
using Asa.CommonGUI.Floater;
using Asa.CommonGUI.SlideSettingsEditors;
using Asa.Core.Common;
using Asa.Dashboard.InteropClasses;
using Asa.Dashboard.Properties;
using Asa.Dashboard.TabHomeForms;
using Asa.Dashboard.TabSlides;
using SettingsManager = Asa.Core.Dashboard.SettingsManager;

namespace Asa.Dashboard
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;

		private FormMain()
		{
			_instance = this;
			InitializeComponent();
			AppManager.Instance.SetClickEventHandler(ribbonControl);
			AppManager.Instance.SetClickEventHandler(pnMain);
			if ((CreateGraphics()).DpiX > 96)
			{
				ribbonControl.Font = new Font(ribbonControl.Font.FontFamily, ribbonControl.Font.Size - 1, ribbonControl.Font.Style);
			}
			PowerPointManager.Instance.SettingsChanged += (o, e) =>
			{
				Text = AppManager.FormCaption;
			};
		}

		public AppManager.EmptyParametersDelegate OutputClick { get; set; }
		public AppManager.EmptyParametersDelegate PreviewClick { get; set; }
		public AppManager.EmptyParametersDelegate LoadClick { get; set; }
		public AppManager.EmptyParametersDelegate OutsideClick { get; set; }
		public bool IsDead { get; set; }

		public static FormMain Instance
		{
			get
			{
				if (_instance == null)
					_instance = new FormMain();
				return _instance;
			}
		}

		#region Configuration Methods
		private void ApplyMasterWizard()
		{
			Text = AppManager.FormCaption;

			var userName = Environment.UserName;
			ribbonBarHomeOverview.Text = userName;
			ribbonBarSlidesLogo.Text = userName;

			var masterWizardLogo = Resources.RibbonLogo;
			buttonItemHomeOverview.Image = masterWizardLogo;
			buttonItemSlidesLogo.Image = masterWizardLogo;
			ribbonBarHomeOverview.RecalcLayout();
			ribbonPanelHome.PerformLayout();
		}
		#endregion

		#region GUI Event Handlers
		public void Init()
		{
			FormStateHelper.Init(this, ResourceManager.Instance.AppSettingsFolder, "6ms", false).LoadState();

			ApplyMasterWizard();

			buttonItemSlidesPowerPoint.Click += TabSlidesMainPage.Instance.buttonItemSlidesPowerPoint_Click;
			buttonItemSlidesPreview.Click += TabSlidesMainPage.Instance.buttonItemSlidesPreview_Click;
			ribbonControl_SelectedRibbonTabChanged(ribbonControl, EventArgs.Empty);
			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;

			buttonItemSlideSettings.Visible =
				MasterWizardManager.Instance.MasterWizards.Count > 1 ||
				(MasterWizardManager.Instance.MasterWizards.Count == 1 && SlideSettings.GetAvailableConfigurations().Count(MasterWizardManager.Instance.MasterWizards.First().Value.HasSlideConfiguration) > 1);
		}

		public void HideThemeButtons()
		{
			buttonItemHomeThemeCleanslate.Visible = false;
			buttonItemHomeThemeCover.Visible = false;
			buttonItemHomeThemeLeadoff.Visible = false;
			buttonItemHomeThemeClientGoals.Visible = false;
			buttonItemHomeThemeTargetCustomers.Visible = false;
			buttonItemHomeThemeSimpleSummary.Visible = false;
		}

		public void FormMain_Activated(object sender, EventArgs e)
		{
			if (RegistryHelper.MainFormHandle != Handle)
				AppManager.Instance.ActivateMainForm();
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			Utilities.Instance.ActivatePowerPoint(DashboardPowerPointHelper.Instance.PowerPointObject);
			RegistryHelper.MainFormHandle = Handle;
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

		private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			OutsideClick = null;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
			{
				TabHomeMainPage.Instance.UpdatePageAccordingToggledButton(SlideType.Cleanslate);
				if (!pnMain.Controls.Contains(TabHomeMainPage.Instance))
					pnMain.Controls.Add(TabHomeMainPage.Instance);
				TabHomeMainPage.Instance.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSlides)
			{
				if (!pnMain.Controls.Contains(TabSlidesMainPage.Instance))
					pnMain.Controls.Add(TabSlidesMainPage.Instance);
				TabSlidesMainPage.Instance.BringToFront();
			}
		}

		private void labelItemLogo_Click(object sender, EventArgs e)
		{
			ribbonTabItemHome.Select();
			buttonItemHomeOverview_Click(null, null);
		}

		private void Outside_Click(object sender, EventArgs e)
		{
			if (OutsideClick != null)
				OutsideClick();
		}
		#endregion

		#region Ribbon Buttons's Clicks Event Handlers
		public void buttonItemFloater_Click(object sender, EventArgs e)
		{
			var formSender = sender as Form;
			if (formSender != null && formSender.IsDisposed) return;
			AppManager.Instance.ShowFloater(formSender, new FloaterRequestedEventArgs { Logo = Resources.RibbonLogo });
		}

		public void buttonItemExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonItemPowerPoint_Click(object sender, EventArgs e)
		{
			if (!AppManager.Instance.CheckPowerPointRunning()) return;
			if (OutputClick != null)
				OutputClick();
		}

		private void buttonItemPreview_Click(object sender, EventArgs e)
		{
			if (!AppManager.Instance.CheckPowerPointRunning()) return;
			if (PreviewClick != null)
				PreviewClick();
		}

		private void buttonItemHomeLoad_Click(object sender, EventArgs e)
		{
			if (LoadClick != null)
				LoadClick();
		}

		private void buttonItemHomeTheme_Click(object sender, EventArgs e)
		{
			if (!SettingsManager.Instance.ThemeManager.GetThemes(SlideType.None).Any())
				AppManager.Instance.HelpManager.OpenHelpLink("NoTheme");
		}

		private void buttonItemHelp_Click(object sender, EventArgs e)
		{
			string helpKey = String.Empty;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
				helpKey = TabHomeMainPage.Instance.HelpKey;
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSlides)
				helpKey = "Slides";

			AppManager.Instance.HelpManager.OpenHelpLink(helpKey);
		}

		private void buttonItemSlideSettings_Click(object sender, EventArgs e)
		{
			using (var form = new FormEditSlideSettings())
			{
				form.ShowDialog(this);
			}
		}

		#region Home Tab
		private void UncheckHomeButtons()
		{
			buttonItemHomeOverview.Checked = false;
			buttonItemPowerPoint.Enabled = false;
		}

		private void buttonItemHomeOverview_Click(object sender, EventArgs e)
		{
			UncheckHomeButtons();
			buttonItemHomeOverview.Checked = true;
			TabHomeMainPage.Instance.UpdatePageAccordingToggledButton(SlideType.Cleanslate);
		}
		#endregion

		#endregion
	}
}