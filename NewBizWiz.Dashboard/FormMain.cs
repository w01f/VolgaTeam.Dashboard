using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.Core.Common;
using NewBizWiz.Dashboard.InteropClasses;
using NewBizWiz.Dashboard.Properties;
using NewBizWiz.Dashboard.TabHomeForms;
using NewBizWiz.Dashboard.TabNewspaperForms;
using NewBizWiz.Dashboard.TabOnlineForms;
using NewBizWiz.Dashboard.TabRadioForms;
using NewBizWiz.Dashboard.TabSlides;
using NewBizWiz.Dashboard.TabTVForms;
using NewBizWiz.Dashboard.ToolForms;
using SettingsManager = NewBizWiz.Core.Dashboard.SettingsManager;

namespace NewBizWiz.Dashboard
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
			FormStateHelper.Init(this, Core.Common.SettingsManager.Instance.SettingsPath, "Dashboard", false);
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
			ribbonBarOnlineLogo.Text = userName;
			ribbonBarNewspaperLogo.Text = userName;
			ribbonBarTVLogo.Text = userName;
			ribbonBarRadioLogo.Text = userName;
			ribbonBarSlidesLogo.Text = userName;

			var masterWizardLogo = MasterWizardManager.Instance.DefaultLogo;
			buttonItemHomeOverview.Image = masterWizardLogo;
			buttonItemOnlineLogo.Image = masterWizardLogo;
			buttonItemNewspaperLogo.Image = masterWizardLogo;
			buttonItemTVLogo.Image = masterWizardLogo;
			buttonItemRadioLogo.Image = masterWizardLogo;
			ribbonBarHomeOverview.RecalcLayout();
			ribbonPanelHome.PerformLayout();
		}

		private void SetDashboardCode()
		{
			switch (Core.Common.SettingsManager.Instance.DashboardCode)
			{
				case "newspaper":
					ribbonTabItemNewspaper.Visible = true;
					ribbonTabItemOnline.Visible = true;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = false;
					break;
				case "tv":
					ribbonTabItemNewspaper.Visible = false;
					ribbonTabItemOnline.Visible = true;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = true;
					ribbonTabItemTV.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length > 0;
					buttonItemTVScheduleBuilder.Image = Resources.TVLittle;
					break;
				case "radio":
					ribbonTabItemNewspaper.Visible = false;
					ribbonTabItemOnline.Visible = true;
					ribbonTabItemRadio.Visible = true;
					ribbonTabItemRadio.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder).Length > 0;
					ribbonTabItemTV.Visible = false;
					break;
				case "cable":
					ribbonTabItemNewspaper.Visible = false;
					ribbonTabItemOnline.Visible = true;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = true;
					ribbonTabItemTV.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length > 0;
					buttonItemTVScheduleBuilder.Image = Resources.CableLittle;
					break;
			}
		}
		#endregion

		#region GUI Event Handlers
		public void Init()
		{
			timer.Start();
			Application.DoEvents();
			ApplyMasterWizard();
			Application.DoEvents();
			SetDashboardCode();
			Application.DoEvents();
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

		private void FormMain_Load(object sender, EventArgs e)
		{
			using (var form = new FormLoadSplash())
			{
				form.TopMost = true;
				form.Show();
				var thread = new Thread(() => DashboardPowerPointHelper.Instance.SetPresentationSettings());
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				Init();
				if (File.Exists(SettingsManager.Instance.IconPath))
					Icon = new Icon(SettingsManager.Instance.IconPath);
				buttonItemNewspaperNew.Click += PrintScheduleBuilderControl.Instance.buttonXNewSchedule_Click;
				buttonItemNewspaperOpen.Click += PrintScheduleBuilderControl.Instance.buttonXOpenSchedule_Click;
				buttonItemNewspaperDelete.Click += PrintScheduleBuilderControl.Instance.buttonXDeleteSchedule_Click;
				buttonItemOnlineNew.Click += OnlineScheduleBuilderControl.Instance.buttonXNewSchedule_Click;
				buttonItemOnlineOpen.Click += OnlineScheduleBuilderControl.Instance.buttonXOpenSchedule_Click;
				buttonItemOnlineDelete.Click += OnlineScheduleBuilderControl.Instance.buttonXDeleteSchedule_Click;
				buttonItemTVNew.Click += TVScheduleBuilderControl.Instance.buttonXNewSchedule_Click;
				buttonItemTVOpen.Click += TVScheduleBuilderControl.Instance.buttonXOpenSchedule_Click;
				buttonItemTVDelete.Click += TVScheduleBuilderControl.Instance.buttonXDeleteSchedule_Click;
				buttonItemRadioNew.Click += RadioScheduleBuilderControl.Instance.buttonXNewSchedule_Click;
				buttonItemRadioOpen.Click += RadioScheduleBuilderControl.Instance.buttonXOpenSchedule_Click;
				buttonItemRadioDelete.Click += RadioScheduleBuilderControl.Instance.buttonXDeleteSchedule_Click;
				buttonItemSlidesPowerPoint.Click += TabSlidesMainPage.Instance.buttonItemSlidesPowerPoint_Click;
				buttonItemSlidesPreview.Click += TabSlidesMainPage.Instance.buttonItemSlidesPreview_Click;
				ribbonControl_SelectedRibbonTabChanged(ribbonControl, EventArgs.Empty);
				ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				form.Close();
			}
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			Utilities.Instance.ActivatePowerPoint(DashboardPowerPointHelper.Instance.PowerPointObject);
			RegistryHelper.MainFormHandle = Handle;
			AppManager.Instance.ActivateMainForm();
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		private void FormMain_Resize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState != FormWindowState.Minimized)
				Opacity = 1;
		}

		public void FormScheduleResize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f == null || f.IsDisposed) return;
			f.Opacity = f.WindowState == FormWindowState.Minimized ? 0 : 1;
		}

		public void FormScheduleClosed(object sender, EventArgs e)
		{
			Opacity = 1;
			RegistryHelper.MainFormHandle = Handle;
			RegistryHelper.MaximizeMainForm = false;
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (!DashboardPowerPointHelper.Instance.IsActive)
				Environment.Exit(-1);
		}

		private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			OutsideClick = null;
			AppManager.Instance.ActivityManager.AddActivity(new TabActivity(ribbonControl.SelectedRibbonTabItem.Text));
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
			{
				TabHomeMainPage.Instance.UpdatePageAccordingToggledButton(SlideType.Cleanslate);
				if (!pnMain.Controls.Contains(TabHomeMainPage.Instance))
					pnMain.Controls.Add(TabHomeMainPage.Instance);
				TabHomeMainPage.Instance.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemOnline)
			{
				TabOnlineMainPage.Instance.UpdatePageAccordingToggledButton();
				if (!pnMain.Controls.Contains(TabOnlineMainPage.Instance))
					pnMain.Controls.Add(TabOnlineMainPage.Instance);
				TabOnlineMainPage.Instance.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemNewspaper)
			{
				TabNewspaperMainPage.Instance.UpdatePageAccordingToggledButton();
				if (!pnMain.Controls.Contains(TabNewspaperMainPage.Instance))
					pnMain.Controls.Add(TabNewspaperMainPage.Instance);
				TabNewspaperMainPage.Instance.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemTV)
			{
				TabTVMainPage.Instance.UpdatePageAccordingToggledButton();
				if (!pnMain.Controls.Contains(TabTVMainPage.Instance))
					pnMain.Controls.Add(TabTVMainPage.Instance);
				TabTVMainPage.Instance.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemRadio)
			{
				TabRadioMainPage.Instance.UpdatePageAccordingToggledButton();
				if (!pnMain.Controls.Contains(TabRadioMainPage.Instance))
					pnMain.Controls.Add(TabRadioMainPage.Instance);
				TabRadioMainPage.Instance.BringToFront();
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
		public void buttonItemFloater_Click(object sender, FloaterRequestedEventArgs e)
		{
			var formSender = sender as Form;
			if (formSender.IsDisposed) return;
			AppManager.Instance.ShowFloater(formSender, e.AfterShow);
		}

		public void buttonItemFloater_Click(object sender, EventArgs e)
		{
			var formSender = sender as Form;
			if (formSender != null && formSender.IsDisposed) return;
			AppManager.Instance.ShowFloater(formSender, null);
		}

		public void buttonItemExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void buttonItemPowerPoint_Click(object sender, EventArgs e)
		{
			if (OutputClick != null)
				OutputClick();
		}

		private void buttonItemPreview_Click(object sender, EventArgs e)
		{
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
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemOnline)
				helpKey = "Online";
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemNewspaper)
				helpKey = "Newspaper";
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemTV)
				helpKey = "TV";
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemRadio)
				helpKey = "Radio";
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSlides)
				helpKey = "Slides";

			AppManager.Instance.HelpManager.OpenHelpLink(helpKey);
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

		#region Select All in Editor Handlers
		private bool enter;
		private bool needSelect;

		public void Editor_Enter(object sender, EventArgs e)
		{
			enter = true;
			BeginInvoke(new MethodInvoker(ResetEnterFlag));
		}

		public void Editor_MouseUp(object sender, MouseEventArgs e)
		{
			if (needSelect)
			{
				(sender as BaseEdit).SelectAll();
			}
		}

		public void Editor_MouseDown(object sender, MouseEventArgs e)
		{
			needSelect = enter;
		}

		private void ResetEnterFlag()
		{
			enter = false;
		}
		#endregion
	}
}