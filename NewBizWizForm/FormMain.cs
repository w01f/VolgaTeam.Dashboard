﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NewBizWizForm.BusinessClasses;
using NewBizWizForm.ConfigurationClasses;
using NewBizWizForm.InteropClasses;
using NewBizWizForm.TabCableForms;
using NewBizWizForm.TabCalendarForms;
using NewBizWizForm.TabHomeForms;
using NewBizWizForm.TabMobileForms;
using NewBizWizForm.TabNewspaperForms;
using NewBizWizForm.TabOnlineForms;
using NewBizWizForm.TabRadioForms;
using NewBizWizForm.TabTVForms;
using NewBizWizForm.TabiPadForms;

namespace NewBizWizForm
{
	public partial class FormMain : Form
	{
		private static FormMain _instance;

		private int _floaterPositionX = int.MinValue;
		private int _floaterPositionY = int.MinValue;

		private FormMain()
		{
			_instance = this;
			InitializeComponent();
			AppManager.Instance.SetClickEventHandler(ribbonControl);
			AppManager.Instance.SetClickEventHandler(panelExMain);
			if ((base.CreateGraphics()).DpiX > 96)
			{
				ribbonControl.Font = new Font(ribbonControl.Font.FontFamily, ribbonControl.Font.Size - 1, ribbonControl.Font.Style);
			}
		}

		public AppManager.EmptyParametersDelegate OutputClick { get; set; }
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
			Instance.Text = AppManager.FormCaption;
			Image masterWizardLogo = MasterWizardManager.Instance.DefaultLogo;
			buttonItemHomeOverview.Image = masterWizardLogo;
			buttonItemOnlineLogo.Image = masterWizardLogo;
			buttonItemMobileLogo.Image = masterWizardLogo;
			buttonItemNewspaperLogo.Image = masterWizardLogo;
			buttonItemTVLogo.Image = masterWizardLogo;
			buttonItemCableLogo.Image = masterWizardLogo;
			buttonItemRadioLogo.Image = masterWizardLogo;
			buttonItemStarLogo.Image = masterWizardLogo;
			buttonItemDigitalLogo.Image = masterWizardLogo;
			buttonItemCalendarLogo.Image = masterWizardLogo;
			buttonItemiPadLogo.Image = masterWizardLogo;
			ribbonBarHomeOverview.RecalcLayout();
			ribbonPanelHome.PerformLayout();
		}

		private void SetDashboardCode()
		{
			switch (SettingsManager.Instance.DashboardCode)
			{
				case 1:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = true;
					ribbonTabItemNewspaper.Visible = true;
					ribbonTabItemOnline.Visible = true;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = false;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = false;
					ribbonTabItemCalendar.Visible = false;
					break;
				case 2:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = true;
					ribbonTabItemNewspaper.Visible = false;
					ribbonTabItemOnline.Visible = true;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = true;
					ribbonTabItemTV.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length > 0;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = false;
					ribbonTabItemCalendar.Visible = false;
					break;
				case 3:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = true;
					ribbonTabItemNewspaper.Visible = false;
					ribbonTabItemOnline.Visible = true;
					ribbonTabItemRadio.Visible = true;
					ribbonTabItemRadio.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder).Length > 0;
					ribbonTabItemTV.Visible = false;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = false;
					ribbonTabItemCalendar.Visible = false;
					break;
				case 4:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = true;
					ribbonTabItemNewspaper.Visible = true;
					ribbonTabItemOnline.Visible = true;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = true;
					ribbonTabItemTV.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length > 0;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = false;
					ribbonTabItemCalendar.Visible = false;
					break;
				case 5:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = true;
					ribbonTabItemNewspaper.Visible = false;
					ribbonTabItemOnline.Visible = true;
					ribbonTabItemRadio.Visible = true;
					ribbonTabItemRadio.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder).Length > 0;
					ribbonTabItemTV.Visible = true;
					ribbonTabItemTV.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length > 0;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = false;
					ribbonTabItemCalendar.Visible = false;
					break;
				case 6:
					ribbonTabItemCable.Visible = true;
					ribbonTabItemMobile.Visible = true;
					ribbonTabItemNewspaper.Visible = false;
					ribbonTabItemOnline.Visible = true;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = false;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = false;
					ribbonTabItemCalendar.Visible = false;
					break;
				case 7:
					ribbonTabItemCable.Visible = true;
					ribbonTabItemMobile.Visible = true;
					ribbonTabItemNewspaper.Visible = true;
					ribbonTabItemOnline.Visible = true;
					ribbonTabItemRadio.Visible = true;
					ribbonTabItemRadio.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder).Length > 0;
					ribbonTabItemTV.Visible = true;
					ribbonTabItemTV.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length > 0;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = false;
					ribbonTabItemCalendar.Visible = false;
					break;
				case 8:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = true;
					ribbonTabItemNewspaper.Visible = false;
					ribbonTabItemOnline.Visible = true;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = false;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = false;
					ribbonTabItemCalendar.Visible = false;
					break;
				case 9:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = false;
					ribbonTabItemNewspaper.Visible = true;
					ribbonTabItemOnline.Visible = false;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = false;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = true;
					ribbonTabItemCalendar.Visible = false;
					break;
				case 10:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = false;
					ribbonTabItemNewspaper.Visible = false;
					ribbonTabItemOnline.Visible = false;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = true;
					ribbonTabItemTV.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length > 0;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = true;
					ribbonTabItemCalendar.Visible = false;
					break;
				case 11:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = false;
					ribbonTabItemNewspaper.Visible = false;
					ribbonTabItemOnline.Visible = false;
					ribbonTabItemRadio.Visible = true;
					ribbonTabItemRadio.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder).Length > 0;
					ribbonTabItemTV.Visible = false;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = true;
					ribbonTabItemCalendar.Visible = false;
					break;
				case 12:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = false;
					ribbonTabItemNewspaper.Visible = true;
					ribbonTabItemOnline.Visible = false;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = false;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = true;
					ribbonTabItemCalendar.Visible = true;
					break;
				case 13:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = false;
					ribbonTabItemNewspaper.Visible = false;
					ribbonTabItemOnline.Visible = false;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = true;
					ribbonTabItemTV.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length > 0;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = true;
					ribbonTabItemCalendar.Visible = true;
					break;
				case 14:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = false;
					ribbonTabItemNewspaper.Visible = false;
					ribbonTabItemOnline.Visible = false;
					ribbonTabItemRadio.Visible = true;
					ribbonTabItemRadio.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder).Length > 0;
					ribbonTabItemTV.Visible = false;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = true;
					ribbonTabItemCalendar.Visible = true;
					break;
				case 15:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = false;
					ribbonTabItemNewspaper.Visible = true;
					ribbonTabItemOnline.Visible = false;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = true;
					ribbonTabItemTV.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length > 0;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = true;
					ribbonTabItemCalendar.Visible = false;
					break;
				case 16:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = false;
					ribbonTabItemNewspaper.Visible = true;
					ribbonTabItemOnline.Visible = false;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = true;
					ribbonTabItemTV.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length > 0;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = true;
					ribbonTabItemCalendar.Visible = true;
					break;
				case 17:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = true;
					ribbonTabItemNewspaper.Visible = true;
					ribbonTabItemOnline.Visible = true;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = true;
					ribbonTabItemTV.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length > 0;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = false;
					ribbonTabItemCalendar.Visible = true;
					break;
				case 18:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = false;
					ribbonTabItemNewspaper.Visible = false;
					ribbonTabItemOnline.Visible = false;
					ribbonTabItemRadio.Visible = true;
					ribbonTabItemRadio.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder).Length > 0;
					ribbonTabItemTV.Visible = true;
					ribbonTabItemTV.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length > 0;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = true;
					ribbonTabItemCalendar.Visible = true;
					break;
				case 30:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = false;
					ribbonTabItemNewspaper.Visible = false;
					ribbonTabItemOnline.Visible = false;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = true;
					ribbonTabItemTV.Enabled = Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) && Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length > 0;
					ribbonTabItemStar.Visible = true;
					ribbonTabItemDigital.Visible = true;
					ribbonTabItemCalendar.Visible = false;
					break;
				case 31:
					ribbonTabItemCable.Visible = false;
					ribbonTabItemMobile.Visible = false;
					ribbonTabItemNewspaper.Visible = false;
					ribbonTabItemOnline.Visible = false;
					ribbonTabItemRadio.Visible = false;
					ribbonTabItemTV.Visible = false;
					ribbonTabItemStar.Visible = false;
					ribbonTabItemDigital.Visible = false;
					ribbonTabItemCalendar.Visible = false;
					break;
			}
		}
		#endregion

		#region GUI Event Handlers
		public void Init()
		{
			timer.Start();
			ApplyMasterWizard();
			SetDashboardCode();
		}

		public void FormMain_Activated(object sender, EventArgs e)
		{
			if (RegistryHelper.MainFormHandle != Instance.Handle)
				AppManager.Instance.ActivateMainForm();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			if (File.Exists(SettingsManager.Instance.IconPath))
				Icon = new Icon(SettingsManager.Instance.IconPath);
			buttonItemNewspaperNew.Click += PrintScheduleBuilderControl.Instance.buttonXNewSchedule_Click;
			buttonItemNewspaperOpen.Click += PrintScheduleBuilderControl.Instance.buttonXOpenSchedule_Click;
			buttonItemNewspaperDelete.Click += PrintScheduleBuilderControl.Instance.buttonXDeleteSchedule_Click;
			buttonItemOnlineNew.Click += OnlineScheduleBuilderControl.Instance.buttonXNewSchedule_Click;
			buttonItemOnlineOpen.Click += OnlineScheduleBuilderControl.Instance.buttonXOpenSchedule_Click;
			buttonItemOnlineDelete.Click += OnlineScheduleBuilderControl.Instance.buttonXDeleteSchedule_Click;
			buttonItemDigitalNew.Click += OnlineScheduleBuilderControl.Instance.buttonXNewSchedule_Click;
			buttonItemDigitalOpen.Click += OnlineScheduleBuilderControl.Instance.buttonXOpenSchedule_Click;
			buttonItemDigitalDelete.Click += OnlineScheduleBuilderControl.Instance.buttonXDeleteSchedule_Click;
			buttonItemMobileNew.Click += MobileScheduleBuilderControl.Instance.buttonXNewSchedule_Click;
			buttonItemMobileOpen.Click += MobileScheduleBuilderControl.Instance.buttonXOpenSchedule_Click;
			buttonItemMobileDelete.Click += MobileScheduleBuilderControl.Instance.buttonXDeleteSchedule_Click;
			buttonItemTVNew.Click += TVScheduleBuilderControl.Instance.buttonXNewSchedule_Click;
			buttonItemTVOpen.Click += TVScheduleBuilderControl.Instance.buttonXOpenSchedule_Click;
			buttonItemTVDelete.Click += TVScheduleBuilderControl.Instance.buttonXDeleteSchedule_Click;
			buttonItemCalendarNew.Click += CalendarBuilderControl.Instance.buttonXNewCalendar_Click;
			buttonItemCalendarOpen.Click += CalendarBuilderControl.Instance.buttonXOpenCalendar_Click;
			buttonItemCalendarDelete.Click += CalendarBuilderControl.Instance.buttonXDeleteCalendar_Click;
			buttonItemRadioNew.Click += RadioScheduleBuilderControl.Instance.buttonXNewSchedule_Click;
			buttonItemRadioOpen.Click += RadioScheduleBuilderControl.Instance.buttonXOpenSchedule_Click;
			buttonItemRadioDelete.Click += RadioScheduleBuilderControl.Instance.buttonXDeleteSchedule_Click;
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			AppManager.Instance.ActivatePowerPoint();
			AppManager.Instance.ActivateMainForm();
			if (AppManager.Instance.ShowCover)
			{
				buttonItemHomeCover_Click(null, null);
				AppManager.Instance.ShowCover = false;
			}
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		public void FormAdScheduleResize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState == FormWindowState.Minimized)
			{
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 2, 0, 0);
				f.Opacity = 0;
				AppManager.Instance.ActivateMiniBar();
			}
			else
			{
				f.Opacity = 1;
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 1, 0, 0);
			}
		}

		public void FormMobileScheduleResize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState == FormWindowState.Minimized)
			{
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 4, 0, 0);
				f.Opacity = 0;
				AppManager.Instance.ActivateMiniBar();
			}
			else
			{
				f.Opacity = 1;
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 3, 0, 0);
			}
		}

		public void FormOnlineScheduleResize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState == FormWindowState.Minimized)
			{
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 6, 0, 0);
				f.Opacity = 0;
				AppManager.Instance.ActivateMiniBar();
			}
			else
			{
				f.Opacity = 1;
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 5, 0, 0);
			}
		}

		public void FormTVScheduleResize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState == FormWindowState.Minimized)
			{
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 8, 0, 0);
				f.Opacity = 0;
				AppManager.Instance.ActivateMiniBar();
			}
			else
			{
				f.Opacity = 1;
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 7, 0, 0);
			}
		}

		public void FormCalendarResize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState == FormWindowState.Minimized)
			{
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 10, 0, 0);
				f.Opacity = 0;
				AppManager.Instance.ActivateMiniBar();
			}
			else
			{
				f.Opacity = 1;
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 9, 0, 0);
			}
		}

		public void FormRadioScheduleResize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState == FormWindowState.Minimized)
			{
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 12, 0, 0);
				f.Opacity = 0;
				AppManager.Instance.ActivateMiniBar();
			}
			else
			{
				f.Opacity = 1;
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 11, 0, 0);
			}
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (!PowerPointHelper.Instance.IsActive)
			{
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 2, 0, 0);
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 4, 0, 0);
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 6, 0, 0);
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 8, 0, 0);
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 10, 0, 0);
				WinAPIHelper.PostMessage(RegistryHelper.MinibarHandle, WinAPIHelper.WM_APP + 12, 0, 0);
				AppManager.Instance.ActivateMiniBar();
				Environment.Exit(-1);
			}
		}

		private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			Control parent = panelExMainInternal.Parent;
			panelExMainInternal.Parent = null;
			panelExMainInternal.Controls.Clear();
			OutsideClick = null;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
			{
				TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
				panelExMainInternal.Controls.Add(TabHomeMainPage.Instance);
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemOnline)
			{
				TabOnlineMainPage.Instance.UpdatePageAccordingToggledButton();
				panelExMainInternal.Controls.Add(TabOnlineMainPage.Instance);
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMobile)
			{
				TabMobileMainPage.Instance.UpdatePageAccordingToggledButton();
				panelExMainInternal.Controls.Add(TabMobileMainPage.Instance);
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemNewspaper)
			{
				TabNewspaperMainPage.Instance.UpdatePageAccordingToggledButton();
				panelExMainInternal.Controls.Add(TabNewspaperMainPage.Instance);
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemTV)
			{
				TabTVMainPage.Instance.UpdatePageAccordingToggledButton();
				panelExMainInternal.Controls.Add(TabTVMainPage.Instance);
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemRadio)
			{
				TabRadioMainPage.Instance.UpdatePageAccordingToggledButton();
				panelExMainInternal.Controls.Add(TabRadioMainPage.Instance);
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCable)
			{
				TabCableMainPage.Instance.UpdatePageAccordingToggledButton();
				panelExMainInternal.Controls.Add(TabCableMainPage.Instance);
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemDigital)
			{
				TabOnlineMainPage.Instance.UpdatePageAccordingToggledButton();
				panelExMainInternal.Controls.Add(TabOnlineMainPage.Instance);
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
			{
				TabCalendarMainPage.Instance.UpdatePageAccordingToggledButton();
				panelExMainInternal.Controls.Add(TabCalendarMainPage.Instance);
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemIPad)
			{
				TabiPadMainPage.Instance.UpdatePageAccordingToggledButton();
				panelExMainInternal.Controls.Add(TabiPadMainPage.Instance);
			}
			panelExMainInternal.Parent = parent;
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
			if (formSender == null)
				formSender = Instance;

			formSender.Opacity = 0;
			using (var form = new FormFloater(Left + Width, Top, _floaterPositionX, _floaterPositionY, buttonItemHomeOverview.Image, ribbonBarHomeOverview.Text))
			{
				if (form.ShowDialog() != DialogResult.No)
				{
					_floaterPositionY = form.Top;
					_floaterPositionX = form.Left;

					formSender.Opacity = 1;
					AppManager.Instance.ActivateMainForm();
				}
				else
					Application.Exit();
			}
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

		private void buttonItemHelp_Click(object sender, EventArgs e)
		{
			string helpKey = string.Empty;
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemHome)
			{
				if (buttonItemHomeOverview.Checked)
					helpKey = "Home";
				else if (buttonItemHomeCover.Checked)
					helpKey = "Cover";
				else if (buttonItemLeadoffStatement.Checked)
					helpKey = "Intro";
				else if (buttonItemClientGoals.Checked)
					helpKey = "Needs";
				else if (buttonItemTargetCustomers.Checked)
					helpKey = "Target";
				else if (buttonItemSimpleSummary.Checked)
					helpKey = "Closing";
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemOnline)
				helpKey = "Online";
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMobile)
				helpKey = "Mobile";
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemNewspaper)
				helpKey = "Newspaper";
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemTV)
				helpKey = "TV";
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemRadio)
				helpKey = "Radio";
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCable)
				helpKey = "Cable";
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemDigital)
				helpKey = "Digital";
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendar)
				helpKey = "Calendar";
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemIPad)
				helpKey = "IPad";

			HelpManager.Instance.OpenHelpLink(helpKey);
		}

		#region Home Tab
		private void UncheckHomeButtons()
		{
			buttonItemHomeOverview.Checked = false;
			buttonItemHomeCover.Checked = false;
			buttonItemLeadoffStatement.Checked = false;
			buttonItemClientGoals.Checked = false;
			buttonItemTargetCustomers.Checked = false;
			buttonItemSimpleSummary.Checked = false;
			ribbonBarPowerPoint.Enabled = false;
		}

		private void buttonItemHomeOverview_Click(object sender, EventArgs e)
		{
			UncheckHomeButtons();
			buttonItemHomeOverview.Checked = true;
			TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
		}

		private void buttonItemHomeAdSchedule_Click(object sender, EventArgs e)
		{
			UncheckHomeButtons();
			TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
		}

		private void buttonItemHomeCleanslate_Click(object sender, EventArgs e)
		{
			PowerPointHelper.Instance.AppendCleanslate();
		}

		public void buttonItemHomeCover_Click(object sender, EventArgs e)
		{
			if (buttonItemHomeCover.Checked)
				UncheckHomeButtons();
			else
			{
				UncheckHomeButtons();
				buttonItemHomeCover.Checked = true;
			}
			TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
		}

		private void buttonItemLeadoffStatement_Click(object sender, EventArgs e)
		{
			UncheckHomeButtons();
			buttonItemLeadoffStatement.Checked = true;
			TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
		}

		private void buttonItemClientGoals_Click(object sender, EventArgs e)
		{
			UncheckHomeButtons();
			buttonItemClientGoals.Checked = true;
			TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
		}

		private void buttonItemTargetCustomers_Click(object sender, EventArgs e)
		{
			UncheckHomeButtons();
			buttonItemTargetCustomers.Checked = true;
			TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
		}

		public void buttonItemSimpleSummary_Click(object sender, EventArgs e)
		{
			UncheckHomeButtons();
			buttonItemSimpleSummary.Checked = true;
			TabHomeMainPage.Instance.UpdatePageAccordingToggledButton();
		}
		#endregion

		#region iPad Tab
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