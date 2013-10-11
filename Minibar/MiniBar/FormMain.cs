﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.MiniBar.BusinessClasses;
using SettingsManager = NewBizWiz.MiniBar.BusinessClasses.SettingsManager;
using TabPage = NewBizWiz.MiniBar.BusinessClasses.TabPage;

namespace NewBizWiz.MiniBar
{
	public partial class FormMain : Form
	{
		private static readonly object _locker = new object();
		private static FormMain _instance;
		private readonly Timer _hideTimer;

		private FormMain()
		{
			InitializeComponent();

			RibbonVisible = true;

			RegistryHelper.MinibarHandle = Handle;

			ribbonControl.Height = 24;

			_hideTimer = new Timer();
			_hideTimer.Interval = 1000;
			_hideTimer.Tick += _hideTimer_Tick;
			_hideTimer.Start();

			if ((base.CreateGraphics()).DpiX > 96)
			{
				ribbonControl.Font = new Font(ribbonControl.Font.FontFamily, ribbonControl.Font.Size - 1, ribbonControl.Font.Style);
				ribbonPanelApps1.PerformLayout();
				ribbonPanelApps2.PerformLayout();
				ribbonPanelDashboard.PerformLayout();
				ribbonPanelApps4.PerformLayout();
				ribbonPanelApps3.PerformLayout();
				ribbonPanelPowerPoint.PerformLayout();
				ribbonPanelSalesDepot.PerformLayout();
				ribbonPanelSettings.PerformLayout();
				ribbonPanelSync.PerformLayout();
				ribbonPanelApps5.PerformLayout();
			}
		}

		#region Form Event Handlers
		private void FormMain_Load(object sender, EventArgs e)
		{
			Init();
		}
		#endregion

		#region Ribbon Event Handlers
		private void ribbonTabItem_Click(object sender, EventArgs e)
		{
			ActivateExpandedForm();
			if (ribbonControl.SelectedRibbonTabItem != null)
			{
				if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemApps1)
					FormMainExpanded.Instance.ribbonTabItemApps1.Select();
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemApps2)
					FormMainExpanded.Instance.ribbonTabItemApps2.Select();
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemDashboard)
					FormMainExpanded.Instance.ribbonTabItemDashboard.Select();
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemApps3)
					FormMainExpanded.Instance.ribbonTabItemApps3.Select();
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemPowerPoint)
					FormMainExpanded.Instance.ribbonTabItemPowerPoint.Select();
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSalesDepot)
					FormMainExpanded.Instance.ribbonTabItemSalesDepot.Select();
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSettings)
					FormMainExpanded.Instance.ribbonTabItemSettings.Select();
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSync)
					FormMainExpanded.Instance.ribbonTabItemSync.Select();
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemApps4)
					FormMainExpanded.Instance.ribbonTabItemApps4.Select();
				else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemApps5)
					FormMainExpanded.Instance.ribbonTabItemApps5.Select();
				ribbonControl.SelectedRibbonTabItem.Checked = false;
				ServiceDataManager.Instance.WriteActivity();
			}
		}
		#endregion

		#region Timer Ticks
		private void FadeInTimer_Tick(object sender, EventArgs e)
		{
			if (Opacity < 1 && !SettingsManager.Instance.QuickRetraction)
			{
				Opacity += 0.07;
				Application.DoEvents();
				return;
			}
			Opacity = 1;
			Application.DoEvents();
			((Timer)sender).Enabled = false;
			((Timer)sender).Dispose();
		}

		private void FadeOutTimer_Tick(object sender, EventArgs e)
		{
			if (Opacity > 0 && !SettingsManager.Instance.QuickRetraction)
			{
				Opacity -= 1;
				Application.DoEvents();
				return;
			}
			Opacity = 0;
			Application.DoEvents();
			((Timer)sender).Enabled = false;
			((Timer)sender).Dispose();
		}

		private void _hideTimer_Tick(object sender, EventArgs e)
		{
			lock (AppManager.Locker)
			{
				var screen = Screen.PrimaryScreen;
				int screensCount = Screen.AllScreens.Length;
				bool primaryOnLeftSide = true;
				if (screensCount > 1)
				{
					screen = SettingsManager.Instance.OnPrimaryScreen ? Screen.PrimaryScreen : Screen.AllScreens.Where(x => !x.Primary).FirstOrDefault();
					primaryOnLeftSide = screen.WorkingArea.X >= 0;
				}
				if (screen == null)
					screen = Screen.PrimaryScreen;

				Top = screen.WorkingArea.Bottom - Height;
				if (SettingsManager.Instance.OnPrimaryScreen)
					Left = (screen.WorkingArea.Width - Width) / 2;
				else if (screensCount > 1)
				{
					if (primaryOnLeftSide)
						Left = Screen.PrimaryScreen.WorkingArea.Width + (screen.WorkingArea.Width - Width) / 2;
					else
						Left = (screen.WorkingArea.Width + Width) / 2 * -1;
				}

				bool visible = RibbonVisible;

				visible = visible & !RegistryHelper.ShowHidden;
				visible = visible & !RegistryHelper.ShowFloat;
				visible = visible & !SettingsManager.Instance.VisiblePowerPoint;
				visible = visible & !SettingsManager.Instance.VisiblePowerPointMaximaized;

				if ((SettingsManager.Instance.HideAll || SettingsManager.Instance.HideSpecificProgram || SettingsManager.Instance.HideSpecificProgramMaximized) && visible)
				{
					try
					{
						var activeProcess = AppManager.Instance.GetActiveProcess();
						visible = !activeProcess.MainWindowTitle.ToUpper().Contains(@"\\REMOTE") && !activeProcess.MainWindowTitle.ToUpper().Contains("POWERPOINT SLIDE SHOW");
						if (visible)
						{
							if (SettingsManager.Instance.HideAll)
								visible = !AppManager.Instance.IsProcessWindowMaximized(activeProcess) || activeProcess.ProcessName.ToLower().Contains("powerpnt");
							else if (SettingsManager.Instance.HideSpecificProgram || SettingsManager.Instance.HideSpecificProgramMaximized)
							{
								visible = !(SettingsManager.Instance.PrimaryApplications.Where(x => activeProcess.ProcessName.ToUpper().Contains(x.ToUpper()) && !activeProcess.ProcessName.ToLower().Contains("powerpnt")).Count() > 0);
								if (SettingsManager.Instance.HideSpecificProgramMaximized && !visible)
									visible = !AppManager.Instance.IsProcessWindowMaximized(activeProcess);
							}
						}
					}
					catch {}
				}
				if (!visible && !RegistryHelper.ShowFloat && (SettingsManager.Instance.VisiblePowerPoint || SettingsManager.Instance.VisiblePowerPointMaximaized))
				{
					try
					{
						var activeProcess = AppManager.Instance.GetActiveProcess();
						if (activeProcess.ProcessName.ToLower().Contains("powerpnt"))
							if (!activeProcess.MainWindowTitle.ToUpper().Contains("POWERPOINT SLIDE SHOW"))
								visible = SettingsManager.Instance.VisiblePowerPointMaximaized ? AppManager.Instance.IsProcessWindowMaximized(activeProcess) || activeProcess.ProcessName.ToLower().Contains("minibar") : SettingsManager.Instance.VisiblePowerPoint;
						if (!visible && activeProcess.ProcessName.ToLower().Contains("minibar"))
						{
							Process process = Process.GetProcesses().Where(x => x.ProcessName.ToLower().Contains("powerpnt")).FirstOrDefault();
							if (process != null)
								if (!process.MainWindowTitle.ToUpper().Contains("POWERPOINT SLIDE SHOW"))
									visible = SettingsManager.Instance.VisiblePowerPointMaximaized ? AppManager.Instance.IsProcessWindowMaximized(process) : !WinAPIHelper.IsIconic(process.MainWindowHandle);
						}
					}
					catch {}
				}
				ribbonControl.Visible = visible;
			}
		}
		#endregion

		#region Common Methods
		protected override void WndProc(ref Message m)
		{
			lock (AppManager.Locker)
			{
				switch (m.Msg)
				{
					case (WinAPIHelper.WM_APP + 1):
						lock (_locker)
							RibbonVisible = false;
						break;
					case (WinAPIHelper.WM_APP + 2):
						lock (_locker)
							RibbonVisible = true;
						break;
					case (WinAPIHelper.WM_APP + 3):
						lock (_locker)
							RibbonVisible = false;
						break;
					case (WinAPIHelper.WM_APP + 4):
						lock (_locker)
							RibbonVisible = true;
						break;
					case (WinAPIHelper.WM_APP + 5):
						lock (_locker)
							RibbonVisible = false;
						break;
					case (WinAPIHelper.WM_APP + 6):
						lock (_locker)
							RibbonVisible = true;
						break;
					case (WinAPIHelper.WM_APP + 7):
						lock (_locker)
							RibbonVisible = false;
						break;
					case (WinAPIHelper.WM_APP + 8):
						lock (_locker)
							RibbonVisible = true;
						break;
					case (WinAPIHelper.WM_APP + 9):
						lock (_locker)
							RibbonVisible = false;
						break;
					case (WinAPIHelper.WM_APP + 10):
						lock (_locker)
							RibbonVisible = true;
						break;
					case (WinAPIHelper.WM_APP + 11):
						lock (_locker)
							RibbonVisible = false;
						break;
					case (WinAPIHelper.WM_APP + 12):
						lock (_locker)
							RibbonVisible = true;
						break;
					default:
						base.WndProc(ref m);
						break;
				}
			}
		}

		public void Init()
		{
			InitTabPages();

			if (ribbonControl.SelectedRibbonTabItem != null)
				ribbonControl.SelectedRibbonTabItem.Checked = false;

			FormMainExpanded.Instance.Init();
			FormMainExpanded.Instance.Show();
		}

		private void InitTabPages()
		{
			ribbonControl.Items.Clear();
			var tabPages = new List<BaseItem>();
			foreach (var tabPageConfig in SettingsManager.Instance.TabPageSettings.TabPages)
			{
				switch (tabPageConfig.Id)
				{
					case TabNamesEnum.PowerPoint:
						ribbonTabItemPowerPoint.Text = tabPageConfig.Name;
						ribbonTabItemPowerPoint.Enabled = tabPageConfig.Enabled;
						tabPages.Add(ribbonTabItemPowerPoint);
						break;
					case TabNamesEnum.Dashboard:
						ribbonTabItemDashboard.Text = tabPageConfig.Name;
						ribbonTabItemDashboard.Enabled = tabPageConfig.Enabled;
						tabPages.Add(ribbonTabItemDashboard);
						break;
					case TabNamesEnum.SalesDepot:
						ribbonTabItemSalesDepot.Text = tabPageConfig.Name;
						ribbonTabItemSalesDepot.Enabled = tabPageConfig.Enabled;
						tabPages.Add(ribbonTabItemSalesDepot);
						break;
					case TabNamesEnum.Apps1:
						ribbonTabItemApps1.Text = tabPageConfig.Name;
						ribbonTabItemApps1.Enabled = tabPageConfig.Enabled;
						tabPages.Add(ribbonTabItemApps1);
						break;
					case TabNamesEnum.Apps2:
						ribbonTabItemApps2.Text = tabPageConfig.Name;
						ribbonTabItemApps2.Enabled = tabPageConfig.Enabled;
						tabPages.Add(ribbonTabItemApps2);
						break;
					case TabNamesEnum.Apps3:
						ribbonTabItemApps3.Text = tabPageConfig.Name;
						ribbonTabItemApps3.Enabled = tabPageConfig.Enabled;
						tabPages.Add(ribbonTabItemApps3);
						break;
					case TabNamesEnum.Apps4:
						ribbonTabItemApps4.Text = tabPageConfig.Name;
						ribbonTabItemApps4.Enabled = tabPageConfig.Enabled;
						tabPages.Add(ribbonTabItemApps4);
						break;
					case TabNamesEnum.Apps5:
						ribbonTabItemApps5.Text = tabPageConfig.Name;
						ribbonTabItemApps5.Enabled = tabPageConfig.Enabled;
						tabPages.Add(ribbonTabItemApps5);
						break;
					case TabNamesEnum.Settings:
						ribbonTabItemSettings.Text = tabPageConfig.Name;
						ribbonTabItemSettings.Enabled = tabPageConfig.Enabled;
						tabPages.Add(ribbonTabItemSettings);
						break;
					case TabNamesEnum.Sync:
						ribbonTabItemSync.Text = tabPageConfig.Name;
						ribbonTabItemSync.Enabled = tabPageConfig.Enabled;
						tabPages.Add(ribbonTabItemSync);
						break;
				}
			}
			ribbonControl.Items.AddRange(tabPages.ToArray());
		}

		public void ActivateExpandedForm()
		{
			FadeOut();
			FormMainExpanded.Instance.FadeIn();
			FormMainExpanded.Instance.Activate();
		}

		public void FadeIn()
		{
			var timer = new Timer();
			timer.Interval = 30;
			timer.Tick += FadeInTimer_Tick;
			timer.Start();
		}

		public void FadeOut()
		{
			var timer = new Timer();
			timer.Interval = 30;
			timer.Tick += FadeOutTimer_Tick;
			timer.Start();
		}
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

		public bool RibbonVisible { get; set; }

		public static FormMain Instance
		{
			get
			{
				if (_instance == null)
					_instance = new FormMain();
				return _instance;
			}
		}
	}
}