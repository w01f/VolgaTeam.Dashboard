﻿using System.Drawing;
using Asa.Bar.App.BarItems;
using Asa.Bar.App.Common;
using Asa.Bar.App.ExternalProcesses;
using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Asa.Bar.App
{
	public partial class FormMain : Form
	{
		private int _lastYVisible;
		private readonly List<RibbonBar> _browsersPanels = new List<RibbonBar>();

		public FormMain()
		{
			InitializeComponent();
		}

		#region Windows Management

		private void InitFromConfig()
		{
			Width = AppManager.Instance.Settings.Config.Width;
			Height = AppManager.Instance.Settings.Config.UncollapsedHeight;
			superTabControlMain.TabStyle = AppManager.Instance.Settings.Config.TabStyle;
			styleManager.ManagerStyle = AppManager.Instance.Settings.Config.ManagerStyle;
			ApplyAccentColor(AppManager.Instance.Settings.UserSettings.AccentColor);
			timerUpdateWindow.Interval = AppManager.Instance.Settings.Config.UpdateWindowInterval;
		}

		/// <summary>
		/// This optimizes the paint routines over this window, but generates some glitches with some controls
		/// </summary>
		protected override CreateParams CreateParams
		{
			get
			{
				var cp = base.CreateParams;
				var os = Environment.OSVersion;
				if ((os.Platform == PlatformID.Win32NT) && (os.Version.Major >= 6))
					cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED 
				return cp;
			}
		}

		public void CollapseWindow()
		{
			superTabControlMain.SelectedTab = null;
			ChangeWindowHeight(AppManager.Instance.Settings.Config.CollapsedHeight);
		}

		private void ChangeWindowHeight(int height)
		{
			Height = height;
			_lastYVisible--;

			AdjustWindowPosition(true);
		}

		public void UncollapseWindow()
		{
			ChangeWindowHeight(AppManager.Instance.Settings.Config.UncollapsedHeight);
		}

		public void AdjustWindowPosition(bool forced = false)
		{
			var screens = Screen.AllScreens;
			var currentScreenIndex = AppManager.Instance.Settings.UserSettings.PreferedMonitor < screens.Length ?
				AppManager.Instance.Settings.UserSettings.PreferedMonitor :
				(screens.Length - 1);
			var screen = screens[currentScreenIndex];
			var taskbar = new TaskBarHelper.Taskbar(!Screen.PrimaryScreen.Equals(screen));
			var y = taskbar.Handle == IntPtr.Zero ?
				screen.Bounds.Bottom - Height :
				taskbar.Location.Y - Height;

			if (y == _lastYVisible && !forced)
				return;

			Top = y;
			Left = screen.Bounds.X + (screen.Bounds.Width / 2) - (Width / 2);
			_lastYVisible = y;
		}

		private void OnUpdateWindowTimerTick(object sender, EventArgs e)
		{
			// Not optimal but it works
			AdjustWindowPosition();
		}
		#endregion

		#region Bar Content Management
		private void LoadBarContent()
		{
			// Create interface
			superTabControlMain.Tabs.Clear();
			foreach (var tabPage in AppManager.Instance.BarItemsManager.Tabs.Where(tab => tab.Visible))
			{
				var tab = superTabControlMain.CreateTab(tabPage.Name);
				var left = 0;
				var shortItemWidth = (superTabControlMain.Width / AppManager.Instance.Settings.Config.MaxShortButtons) + 1;
				var longItemWidth = shortItemWidth * 2;
				var itemHeight = Height - superTabControlMain.TabStrip.Height - 2;
				tab.Enabled = tabPage.Enabled;

				foreach (var tabGroup in tabPage.Groups)
				{
					switch (tabGroup.Type)
					{
						case TabGroupType.ShortButton:
							if (tabGroup.Items.Any())
							{
								var ribbonBar = CreateGroupBar(tabGroup);
								ribbonBar.Width = shortItemWidth;
								ribbonBar.Height = (Int32)((1f / (0.9f + (0.1f * AppManager.Instance.Settings.Config.VirtualDpi))) * itemHeight);
								ribbonBar.Left = left;
								tab.AttachedControl.Controls.Add(ribbonBar);
								left += shortItemWidth;
							}
							break;
						case TabGroupType.LongButton:
							if (tabGroup.Items.Any())
							{
								var ribbonBar = CreateGroupBar(tabGroup);
								ribbonBar.Width = longItemWidth;
								ribbonBar.Height = (Int32)((1f / (0.9f + (0.1f * AppManager.Instance.Settings.Config.VirtualDpi))) * itemHeight);
								ribbonBar.Left = left;
								tab.AttachedControl.Controls.Add(ribbonBar);
								left += shortItemWidth;
							}
							break;
						case TabGroupType.BrowserPanel:
							{
								var ribbonBar = CreateBrowserBar(tabGroup);
								ribbonBar.Width = shortItemWidth;
								ribbonBar.Height = (Int32)((1f / (0.9f + (0.1f * AppManager.Instance.Settings.Config.VirtualDpi))) * itemHeight);
								ribbonBar.Left = left;
								tab.AttachedControl.Controls.Add(ribbonBar);
								_browsersPanels.Add(ribbonBar);
								left += shortItemWidth;
							}
							break;
						case TabGroupType.SettingsPanel:
							ribbonBarSettings.Text = tabGroup.Name;
							ribbonBarSettings.Top = 0;
							ribbonBarSettings.Height = (Int32)((1f / (0.9f + (0.1f * AppManager.Instance.Settings.Config.VirtualDpi))) * itemHeight);
							ribbonBarSettings.Left = left;
							tab.AttachedControl.Controls.Add(ribbonBarSettings);
							left += longItemWidth;
							break;
						case TabGroupType.CustomControls:
							var pluginName = tabGroup.Tag;
							if (String.IsNullOrEmpty(pluginName)) break;
							var plugin = PluginsManager.Instance.Controls.FirstOrDefault(c => c.ControlName.ToLower().Equals(pluginName));
							if (plugin == null) break;

							foreach (var ribbonBar in plugin.RibbonBars)
							{
								ribbonBar.Name = tabGroup.Name;
								ribbonBar.Height = (Int32)((1f / (0.9f + (0.1f * AppManager.Instance.Settings.Config.VirtualDpi))) * itemHeight);
								ribbonBar.Width = longItemWidth;
								ribbonBar.Left = left;
								ribbonBar.Text = tabGroup.Name;
								ribbonBar.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
								ribbonBar.VerticalItemAlignment = eVerticalItemsAlignment.Middle;
								ribbonBar.AutoOverflowEnabled = false;
								tab.AttachedControl.Controls.Add(ribbonBar);
								left += ribbonBar.Width;
							}
							break;
					}
				}
			}
		}

		private RibbonBar CreateGroupBar(TabGroup groupConfig)
		{
			var ribbonBar = new RibbonBar
			{
				Text = groupConfig.Name,
				HorizontalItemAlignment = eHorizontalItemsAlignment.Center,
				VerticalItemAlignment = eVerticalItemsAlignment.Middle,
				AutoOverflowEnabled = false
			};
			ribbonBar.Items.Add(CreateBarItem(groupConfig.Items));
			return ribbonBar;
		}

		private BaseItem CreateBarItem(IList<TabGroupItem> barItemConfigList)
		{

			if (barItemConfigList.Count == 1)
				return CreateButtonItem(barItemConfigList.First());
			return CreateGalleryItem(barItemConfigList);
		}

		private ButtonItem CreateButtonItem(TabGroupItem barItemConfig)
		{
			var buttonItem = new ButtonItem
			{
				Image = barItemConfig.Image,
				Style = eDotNetBarStyle.Metro,
				ThemeAware = false,
				Enabled = !AppManager.Instance.Settings.Config.DisableNonAvailableButtons || barItemConfig.UserGranted,
				ImagePosition = eImagePosition.Top
			};

			if (!String.IsNullOrEmpty(barItemConfig.Tooltip))
				buttonItem.Tooltip = barItemConfig.Tooltip;

			buttonItem.Click += (o, args) => OpenLink(barItemConfig);
			return buttonItem;
		}

		private GalleryContainer CreateGalleryItem(IEnumerable<TabGroupItem> barItemConfigList)
		{
			var galleryContainer = new GalleryContainer { EnableGalleryPopup = false };
			var resize = true;

			foreach (var barItemConfig in barItemConfigList)
			{
				var buttonItem = CreateButtonItem(barItemConfig);
				galleryContainer.SubItems.Add(buttonItem);
				if (!resize) continue;
				galleryContainer.RecalcSize();
				galleryContainer.DefaultSize = new Size(
					buttonItem.Image.Size.Width + AppManager.Instance.Settings.Config.MultiHorizontalPadding,
					buttonItem.Image.Size.Height + AppManager.Instance.Settings.Config.MultiVerticalPadding);
				resize = false;
			}
			return galleryContainer;
		}

		private RibbonBar CreateBrowserBar(TabGroup groupConfig)
		{
			var ribbonBar = new RibbonBar
			{
				Text = groupConfig.Name,
				HorizontalItemAlignment = eHorizontalItemsAlignment.Center,
				VerticalItemAlignment = eVerticalItemsAlignment.Middle,
				AutoOverflowEnabled = false
			};
			foreach (BaseItem buttonItem in ribbonBarBrowsers.Items)
				ribbonBar.Items.Add((BaseItem)buttonItem.Clone());
			return ribbonBar;
		}

		private void OpenLink(TabGroupItem barItemConfig)
		{
			var isAllowed = barItemConfig.UserGranted;
			if (isAllowed && !String.IsNullOrEmpty(barItemConfig.Password))
			{

				using (var form = new FormPassword(barItemConfig.Password, barItemConfig.Title))
				{
					var result = form.ShowDialog(this);
					if (result == DialogResult.Cancel)
						return;
					isAllowed = result == DialogResult.OK;
				}
			}
			if (!isAllowed)
			{
				MessageBox.Show(this,
					"You are not authorized to access this feature",
					"Not authorized",
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
				return;
			}
			barItemConfig.Open();
		}
		#endregion

		#region Color Settings Management
		private void OnSelectedColorChanged(object sender, EventArgs e)
		{
			ApplyAccentColor(colorPickerDropDownInterface.SelectedColor);
		}

		private void OnColorPreview(object sender, ColorPreviewEventArgs e)
		{
			ApplyAccentColor(e.Color, true);
		}

		private void OnColorPopupClose(object sender, EventArgs e)
		{
			ApplyAccentColor(AppManager.Instance.Settings.UserSettings.AccentColor);
		}

		private void ApplyAccentColor(Color color, bool previewOnly = false)
		{
			styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(
					styleManager.MetroColorParameters.CanvasColor, color);

			if (previewOnly) return;

			AppManager.Instance.Settings.UserSettings.AccentColor = color;
			AppManager.Instance.Settings.UserSettings.Save();
		}
		#endregion

		#region Monitor Settings Management
		private void UpdateMonitorControls()
		{
			var monitorCount = AppManager.Instance.MonitorConfigurationWatcher.MonitorsCount;
			var preferedMonitor = AppManager.Instance.Settings.UserSettings.PreferedMonitor;
			if (monitorCount > 1)
			{
				itemContainerMonitors.Visible = true;
				buttonItemScreen1.Checked = preferedMonitor == 0;
				buttonItemScreen2.Visible = monitorCount > 1;
				buttonItemScreen2.Checked = preferedMonitor == 1;
				buttonItemScreen3.Visible = monitorCount > 2;
				buttonItemScreen3.Checked = preferedMonitor == 2;
				buttonItemScreen4.Visible = monitorCount > 3;
				buttonItemScreen4.Checked = preferedMonitor == 3;
				buttonItemScreen5.Visible = monitorCount > 4;
				buttonItemScreen5.Checked = preferedMonitor == 4;
				buttonItemScreen6.Visible = monitorCount > 5;
				buttonItemScreen6.Checked = preferedMonitor == 5;
			}
			else
				itemContainerMonitors.Visible = false;
		}

		private void OnMonitorSelectorClick(object sender, EventArgs e)
		{
			var button = (ButtonItem)sender;
			if (button.Checked) return;

			buttonItemScreen1.Checked = false;
			buttonItemScreen2.Checked = false;
			buttonItemScreen3.Checked = false;
			buttonItemScreen4.Checked = false;
			buttonItemScreen5.Checked = false;
			buttonItemScreen6.Checked = false;

			button.Checked = true;

			AppManager.Instance.Settings.UserSettings.PreferedMonitor = Int32.Parse(button.Tag as String);
			AppManager.Instance.Settings.UserSettings.Save();

			AdjustWindowPosition(true);
		}

		private void OnMonitorConfigurationChanged(object sender, EventArgs e)
		{
			UpdateMonitorControls();
			AdjustWindowPosition(true);
		}
		#endregion

		#region Externall Process Watching
		private void OnExternalProcessesStatusChanged(object sender, ProcessStatusEventArgs e)
		{
			Invoke(new MethodInvoker(() =>
			{
				switch (e.Status)
				{
					case BarVsProcessStatus.OnTop:
						Opacity = 100;
						TopMost = true;
						Select();
						break;

					case BarVsProcessStatus.Hidden:
						Opacity = 0;
						break;

					case BarVsProcessStatus.NotOnTop:
						Opacity = 100;
						TopMost = false;
						SendToBack();
						break;
				}
				Application.DoEvents();
			}));

		}
		#endregion

		#region Web Browser Management
		private void UpdateBrowserButtons()
		{
			foreach (var panelButton in _browsersPanels.SelectMany(panel => WebBrowserManager.GetBrowserButtons(panel.Items)))
			{
				panelButton.Enabled = AppManager.Instance.WebBrowserManager.AvailableBrowsers.ContainsKey((String)panelButton.Tag);
				panelButton.Checked = String.Compare(panelButton.Tag as String, AppManager.Instance.Settings.UserSettings.SelectedBrowser, StringComparison.OrdinalIgnoreCase) == 0;
			}
		}

		private void OnBrowserSelectorClick(object sender, EventArgs e)
		{
			var button = (ButtonItem)sender;
			if (button.Checked) return;

			AppManager.Instance.Settings.UserSettings.SelectedBrowser = (String)button.Tag;
			AppManager.Instance.Settings.UserSettings.Save();

			AppManager.Instance.ActivityManager.AddActivity(new AdBarActivity(AdBarActivityType.ApplicationOpenLink, AppManager.Instance.Settings.UserSettings.SelectedBrowser));

			UpdateBrowserButtons();
		}
		#endregion

		#region Load at Startup Setting Management
		private void InitLoadAtStartupSettings()
		{
			if (AppManager.Instance.Settings.UserSettings.LoadAtStartup &&
				!LoadAtStartupHelper.IsLoadAtStartupEnabled())
				LoadAtStartupHelper.SetLoadAtStartup();

			if (!AppManager.Instance.Settings.UserSettings.LoadAtStartup &&
				LoadAtStartupHelper.IsLoadAtStartupEnabled())
				LoadAtStartupHelper.UnsetLoadAtStartup();

			checkBoxItemLoadAtStartup.Checked = LoadAtStartupHelper.IsLoadAtStartupEnabled();
			checkBoxItemLoadAtStartup.CheckedChanged += OnLoadAtStartupCheckedChanged;
		}

		private void OnLoadAtStartupCheckedChanged(object sender, CheckBoxChangeEventArgs e)
		{
			if (checkBoxItemLoadAtStartup.Checked)
				LoadAtStartupHelper.SetLoadAtStartup();
			else
				LoadAtStartupHelper.UnsetLoadAtStartup();

			AppManager.Instance.Settings.UserSettings.LoadAtStartup = checkBoxItemLoadAtStartup.Checked;
			AppManager.Instance.Settings.UserSettings.Save();
		}
		#endregion

		#region Form Event Handlers
		private void OnFormLoad(object sender, EventArgs e)
		{
			InitFromConfig();

			AppManager.Instance.ExternalProcessesWatcher.StartWatching();
			AppManager.Instance.ExternalProcessesWatcher.OnStatusChanged += OnExternalProcessesStatusChanged;

			AppManager.Instance.MonitorConfigurationWatcher.StartWatching();
			AppManager.Instance.MonitorConfigurationWatcher.ConfigurationChanged += OnMonitorConfigurationChanged;

			UpdateMonitorControls();

			InitLoadAtStartupSettings();

			LoadBarContent();

			UpdateBrowserButtons();

			CollapseWindow();

			timerUpdateWindow.Start();
		}

		private void OnFormShown(object sender, EventArgs e)
		{
			AppManager.Instance.ActivityManager.AddActivity(new AdBarActivity(AdBarActivityType.ApplicationOpen));
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			AppManager.Instance.ActivityManager.AddActivity(new AdBarActivity(AdBarActivityType.ApplicationClose));
			AppManager.Instance.ExternalProcessesWatcher.StopWatching();
			Application.Exit();
		}

		private void OnFormDeactivate(object sender, EventArgs e)
		{
			CollapseWindow();
		}
		#endregion

		#region Ribbon Event Handlers
		private void OnTabControlSelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
		{
			UncollapseWindow();
			AppManager.Instance.ActivityManager.AddActivity(new AdBarActivity(AdBarActivityType.ApplicationSwitchTab, e.OldValue == null ? e.NewValue.Text : e.OldValue.Text + "->" + e.NewValue.Text));
		}

		private void OnTabControlTabStripMouseMove(object sender, MouseEventArgs e)
		{
			timerUpdateWindow.Stop();
			timerUpdateWindow.Start();
		}

		private void OnTabControlCloseClick(object sender, EventArgs e)
		{
			Close();
		}
		#endregion
	}
}
