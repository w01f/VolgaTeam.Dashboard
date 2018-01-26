using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Bar.App.BarItems;
using Asa.Bar.App.Common;
using Asa.Bar.App.Configuration;
using Asa.Bar.App.ExternalProcesses;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro.ColorTables;

namespace Asa.Bar.App.Forms
{
	public partial class FormMain : Form
	{
		private readonly List<RibbonBar> _browsersPanels = new List<RibbonBar>();
		private readonly ButtonItem _buttonItemDock;
		private readonly ButtonItem _buttonItemUndock;
		private readonly ButtonItem _buttonItemCollapse;
		private bool _floaterOnTop = false;

		public FormMain()
		{
			InitializeComponent();

			_buttonItemDock = new ButtonItem();
			_buttonItemDock.Visible = false;
			_buttonItemDock.ItemAlignment = eItemAlignment.Far;
			_buttonItemDock.Tooltip = "Dock";
			_buttonItemDock.Click += OnDockButtonClick;

			_buttonItemUndock = new ButtonItem();
			_buttonItemUndock.Visible = false;
			_buttonItemUndock.ItemAlignment = eItemAlignment.Far;
			_buttonItemUndock.Tooltip = "Undock";
			_buttonItemUndock.Click += OnUndockButtonClick;

			_buttonItemCollapse = new ButtonItem();
			_buttonItemCollapse.Visible = false;
			_buttonItemCollapse.ItemAlignment = eItemAlignment.Far;
			_buttonItemCollapse.Tooltip = "Mini-Floater";
			_buttonItemCollapse.Click += OnCollapseClick;
		}

		#region Windows Management
		private void InitFromConfig()
		{
			if (ResourceManager.Instance.IconFile.ExistsLocal())
				Icon = notifyIcon.Icon = new Icon(ResourceManager.Instance.IconFile.LocalPath);

			if (ResourceManager.Instance.DockRegularImageFile.ExistsLocal())
				_buttonItemDock.Image = Image.FromFile(ResourceManager.Instance.DockRegularImageFile.LocalPath);

			if (ResourceManager.Instance.UndockFormImageFile.ExistsLocal())
				_buttonItemUndock.Image = Image.FromFile(ResourceManager.Instance.UndockFormImageFile.LocalPath);

			if (ResourceManager.Instance.CollapseFormImageFile.ExistsLocal())
				_buttonItemCollapse.Image = Image.FromFile(ResourceManager.Instance.CollapseFormImageFile.LocalPath);

			Font = new Font(Font.FontFamily, AppManager.Instance.Settings.Config.FontSize, Font.Style);
			superTabControlMain.TabFont = new Font(superTabControlMain.TabFont.FontFamily, AppManager.Instance.Settings.Config.FontSize, superTabControlMain.TabFont.Style);
			superTabControlMain.SelectedTabFont = new Font(superTabControlMain.SelectedTabFont.FontFamily, AppManager.Instance.Settings.Config.FontSize, superTabControlMain.SelectedTabFont.Style);

			Width = AppManager.Instance.Settings.Config.Width;
			Height = AppManager.Instance.Settings.Config.Height;
			superTabControlMain.TabStyle = AppManager.Instance.Settings.Config.TabStyle;
			styleManager.ManagerStyle = AppManager.Instance.Settings.Config.ManagerStyle;
			ApplyAccentColor(AppManager.Instance.Settings.UserSettings.AccentColor, false);
			ApplyTextColor(AppManager.Instance.Settings.UserSettings.TextColor, false);
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
			if (!AppManager.Instance.Settings.UserSettings.UseDockedStyle && AppManager.Instance.Settings.UserSettings.AlwaysExpanded) return;
			superTabControlMain.SelectedTab = null;
			ChangeWindowHeight(AppManager.Instance.Settings.Config.CollapsedHeight);
		}

		private void ChangeWindowHeight(int height)
		{
			Height = height;
			AdjustWindowPosition();
		}

		public void UncollapseWindow()
		{
			ChangeWindowHeight(AppManager.Instance.Settings.Config.Height);
		}

		public void AdjustWindowPosition()
		{
			if (!AppManager.Instance.Settings.UserSettings.UseDockedStyle && AppManager.Instance.Settings.UserSettings.FormLocationLeft.HasValue && AppManager.Instance.Settings.UserSettings.FormLocationTop.HasValue) return;
			var screens = Screen.AllScreens;
			var currentScreenIndex = AppManager.Instance.Settings.UserSettings.PreferedMonitor < screens.Length ?
				AppManager.Instance.Settings.UserSettings.PreferedMonitor :
				0;
			var screen = screens[currentScreenIndex];
			var taskbar = new TaskBarHelper.Taskbar(!Screen.PrimaryScreen.Equals(screen));

			var x = screen.Bounds.X + (screen.Bounds.Width / 2) - (Width / 2);
			int y;
			if (taskbar.Handle != IntPtr.Zero && taskbar.Position == TaskBarHelper.TaskbarPosition.Bottom)
			{
				var taskBarHeight = screen.Bounds.Bottom - taskbar.Location.Y;
				taskBarHeight = taskBarHeight > 0 && taskBarHeight < taskbar.Bounds.Height
					? taskBarHeight
					: taskbar.Bounds.Height;
				y = screen.Bounds.Bottom - Height - taskBarHeight;
			}
			else
				y = screen.Bounds.Bottom - Height;

			Left = x;
			Top = y;
		}

		private void OnUpdateWindowTimerTick(object sender, EventArgs e)
		{
			AdjustWindowPosition();
		}
		#endregion

		#region Bar Content Management
		private void LoadBarContent()
		{
			var heightCoeff = 1f / (0.9f + (0.1f * AppManager.Instance.Settings.Config.VirtualDpi));
			var itemHeight = heightCoeff * (AppManager.Instance.Settings.Config.Height - superTabControlMain.TabStrip.Height - 3);

			var shortItemWidth = (superTabControlMain.Width / AppManager.Instance.Settings.Config.MaxShortButtons) + 1;
			var longItemWidth = shortItemWidth * 2;

			superTabControlMain.Tabs.Clear();
			foreach (var tabPage in AppManager.Instance.BarItemsManager.Tabs.Where(tab => tab.Visible))
			{
				var tab = superTabControlMain.CreateTab(tabPage.Name);
				var left = 0;

				tab.Enabled = tabPage.Enabled;
				tab.Click += OnTabControlPageClick;
				tab.MouseDown += OnTabControlMouseDown;

				foreach (var tabGroup in tabPage.Groups)
				{
					switch (tabGroup.Type)
					{
						case TabGroupType.ShortButton:
							if (tabGroup.Items.Any())
							{
								var ribbonBar = CreateGroupBar(tabGroup);
								ribbonBar.Width = shortItemWidth;
								ribbonBar.Height = (Int32)itemHeight;
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
								ribbonBar.Height = (Int32)itemHeight;
								ribbonBar.Left = left;
								tab.AttachedControl.Controls.Add(ribbonBar);
								left += shortItemWidth;
							}
							break;
						case TabGroupType.BrowserPanel:
							{
								var ribbonBar = CreateBrowserBar(tabGroup);
								ribbonBar.Width = shortItemWidth;
								ribbonBar.Height = (Int32)itemHeight;
								ribbonBar.Left = left;
								ribbonBar.TitleStyle.TextColor = AppManager.Instance.Settings.Config.RibbonBarTextColor;
								ribbonBar.TitleStyleMouseOver.TextColor = AppManager.Instance.Settings.Config.RibbonBarTextHoverColor;
								ribbonBar.TitleStyle.TextAlignment = ribbonBar.TitleStyleMouseOver.TextAlignment = eStyleTextAlignment.Center;
								tab.AttachedControl.Controls.Add(ribbonBar);
								_browsersPanels.Add(ribbonBar);
								left += shortItemWidth;
							}
							break;
						case TabGroupType.SettingsPanel:
							ribbonBarSettings.Text = tabGroup.Name;
							ribbonBarSettings.Top = 0;
							ribbonBarSettings.Width = shortItemWidth;
							ribbonBarSettings.Height = (Int32)itemHeight;
							ribbonBarSettings.Left = left;
							ribbonBarSettings.TitleStyle.TextColor = AppManager.Instance.Settings.Config.RibbonBarTextColor;
							ribbonBarSettings.TitleStyleMouseOver.TextColor = AppManager.Instance.Settings.Config.RibbonBarTextHoverColor;
							ribbonBarSettings.TitleStyle.TextAlignment = ribbonBarSettings.TitleStyleMouseOver.TextAlignment = eStyleTextAlignment.Center;
							tab.AttachedControl.Controls.Add(ribbonBarSettings);
							left += shortItemWidth;
							break;
						case TabGroupType.CustomControls:
							var pluginName = tabGroup.Tag;
							if (String.IsNullOrEmpty(pluginName)) break;
							var plugin = PluginsManager.Instance.Controls.FirstOrDefault(c => c.ControlName.ToLower().Equals(pluginName));
							if (plugin == null) break;

							foreach (var ribbonBar in plugin.RibbonBars)
							{
								ribbonBar.Name = tabGroup.Name;
								ribbonBar.Height = (Int32)itemHeight;
								ribbonBar.Width = longItemWidth;
								ribbonBar.Left = left;
								ribbonBar.Text = tabGroup.Name;
								ribbonBar.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
								ribbonBar.VerticalItemAlignment = eVerticalItemsAlignment.Middle;
								ribbonBar.AutoOverflowEnabled = false;
								ribbonBar.TitleStyle.TextColor = AppManager.Instance.Settings.Config.RibbonBarTextColor;
								ribbonBar.TitleStyleMouseOver.TextColor = AppManager.Instance.Settings.Config.RibbonBarTextHoverColor;
								ribbonBar.TitleStyle.TextAlignment = ribbonBar.TitleStyleMouseOver.TextAlignment = eStyleTextAlignment.Center;
								tab.AttachedControl.Controls.Add(ribbonBar);
								left += ribbonBar.Width;
							}
							break;
					}
				}
			}
			superTabControlMain.Tabs.Add(_buttonItemDock);
			superTabControlMain.Tabs.Add(_buttonItemUndock);
			superTabControlMain.Tabs.Add(_buttonItemCollapse);
			ApplyTextColor(AppManager.Instance.Settings.UserSettings.TextColor, false);
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
			ribbonBar.TitleStyle.TextColor = AppManager.Instance.Settings.Config.RibbonBarTextColor;
			ribbonBar.TitleStyleMouseOver.TextColor = AppManager.Instance.Settings.Config.RibbonBarTextHoverColor;
			ribbonBar.TitleStyle.TextAlignment = ribbonBar.TitleStyleMouseOver.TextAlignment = eStyleTextAlignment.Center;
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
		private void OnSelectedAccentColorChanged(object sender, EventArgs e)
		{
			ApplyAccentColor(colorPickerDropDownAccent.SelectedColor, true);
		}

		private void OnAccentColorPreview(object sender, ColorPreviewEventArgs e)
		{
			ApplyAccentColor(e.Color, false);
		}

		private void OnAccentColorPopupClose(object sender, EventArgs e)
		{
			ApplyAccentColor(AppManager.Instance.Settings.UserSettings.AccentColor, true);
		}

		private void ApplyAccentColor(Color color, bool saveSettings)
		{
			styleManager.MetroColorParameters = new MetroColorGeneratorParameters(
					styleManager.MetroColorParameters.CanvasColor, color);

			if (!saveSettings) return;

			AppManager.Instance.Settings.UserSettings.AccentColor = color;
			AppManager.Instance.Settings.UserSettings.Save();
		}

		private void OnSelectedTextColorChanged(object sender, EventArgs e)
		{
			ApplyTextColor(colorPickerDropDownText.SelectedColor, true);
		}

		private void OnTextColorPreview(object sender, ColorPreviewEventArgs e)
		{
			ApplyTextColor(e.Color, false);
		}

		private void OnTextColorPopupClose(object sender, EventArgs e)
		{
			ApplyTextColor(AppManager.Instance.Settings.UserSettings.TextColor, true);
		}

		private void ApplyTextColor(Color color, bool saveSettings)
		{
			superTabControlMain.TabStripColor.ControlBoxDefault.Image =
				superTabControlMain.TabStripColor.ControlBoxPressed.Image =
					superTabControlMain.TabStripColor.ControlBoxMouseOver.Image = color;

			foreach (var tab in superTabControlMain.Tabs.OfType<SuperTabItem>())
				tab.TabColor.Default.Normal.Text =
					tab.TabColor.Default.Disabled.Text =
						tab.TabColor.Default.MouseOver.Text =
							tab.TabColor.Default.Selected.Text =
								tab.TabColor.Default.SelectedMouseOver.Text = AppManager.Instance.Settings.UserSettings.TextColor;

			if (!saveSettings) return;

			AppManager.Instance.Settings.UserSettings.TextColor = color;
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

			AdjustWindowPosition();
		}

		private void OnMonitorConfigurationChanged(object sender, EventArgs e)
		{
			UpdateMonitorControls();
			AdjustWindowPosition();
		}
		#endregion

		#region Externall Process Watching
		private void OnExternalProcessesStatusChanged(object sender, ProcessStatusEventArgs e)
		{
			Invoke(new MethodInvoker(() =>
			{
				if (_floaterOnTop)
				{
					Opacity = 0;
				}
				else
				{
					switch (e.Status)
					{
						case BarVsProcessStatus.OnTop:
							Opacity = 1;
							TopMost = true;
							Select();
							break;

						case BarVsProcessStatus.Hidden:
							Opacity = 0;
							break;

						case BarVsProcessStatus.NotOnTop:
							Opacity = 1;
							TopMost = false;
							SendToBack();
							break;
					}
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

		#region Docked Style Setting Management
		private void InitDockedStyleSettings()
		{
			if (!AppManager.Instance.Settings.UserSettings.UseDockedStyle)
			{
				Top = AppManager.Instance.Settings.UserSettings.FormLocationTop ?? Top;
				Left = AppManager.Instance.Settings.UserSettings.FormLocationLeft ?? Left;

				var minX = Screen.AllScreens.Min(screen => screen.Bounds.Left) + (Int32)(Width * 0.2);
				var maxX = Screen.AllScreens.Max(screen => screen.Bounds.Right) - (Int32)(Width * 0.2);
				var minY = Screen.AllScreens.Min(screen => screen.Bounds.Top) + (Int32)(Height * 0.2);
				var maxY = Screen.AllScreens.Max(screen => screen.Bounds.Bottom) - (Int32)(Height * 0.2);

				if (Right < minX)
					Left = minX - Width;
				else if (Left > maxX)
					Left = maxX;

				if (Bottom < minY)
					Top = minY - Height;
				else if (Top > maxY)
					Top = maxY;

				AdjustWindowPosition();

				buttonItemScreen1.Enabled = false;
				buttonItemScreen2.Enabled = false;
				buttonItemScreen3.Enabled = false;
				buttonItemScreen4.Enabled = false;
				buttonItemScreen5.Enabled = false;
				buttonItemScreen6.Enabled = false;
			}
			_buttonItemDock.Visible = !AppManager.Instance.Settings.UserSettings.UseDockedStyle && AppManager.Instance.Settings.Config.ShowDockRegularButton;
			_buttonItemUndock.Visible = AppManager.Instance.Settings.UserSettings.UseDockedStyle && AppManager.Instance.Settings.Config.ShowUndockButton;
			_buttonItemCollapse.Visible = !AppManager.Instance.Settings.UserSettings.UseDockedStyle;
			notifyIcon.Visible = !AppManager.Instance.Settings.UserSettings.UseDockedStyle;
			checkBoxItemDocked.Checked = AppManager.Instance.Settings.UserSettings.UseDockedStyle;

			checkBoxItemDocked.CheckedChanged += OnDockedStyleCheckedChanged;
		}

		private void ShowFloaterForm()
		{
			Opacity = 0;
			notifyIcon.Visible = false;
			using (var form = new FormFloater())
			{
				if (AppManager.Instance.Settings.UserSettings.FloaterLocationLeft.HasValue &&
					AppManager.Instance.Settings.UserSettings.FloaterLocationTop.HasValue)
				{
					form.Top = AppManager.Instance.Settings.UserSettings.FloaterLocationTop.Value;
					form.Left = AppManager.Instance.Settings.UserSettings.FloaterLocationLeft.Value;
				}
				else
				{
					form.Top = Screen.PrimaryScreen.Bounds.Top + 50;
					form.Left = Screen.PrimaryScreen.Bounds.Right - form.Width - 50;
				}
				_floaterOnTop = true;
				var result = form.ShowDialog(this);
				_floaterOnTop = false;
				AppManager.Instance.Settings.UserSettings.FloaterLocationLeft = form.Left;
				AppManager.Instance.Settings.UserSettings.FloaterLocationTop = form.Top;
				AppManager.Instance.Settings.UserSettings.Save();

				if (result == DialogResult.Yes || result == DialogResult.Abort)
				{
					Opacity = 1;
					notifyIcon.Visible = true;
					if (result == DialogResult.Yes)
					{
						AppManager.Instance.Settings.UserSettings.ShowFloaterWhenUndock = false;
						AppManager.Instance.Settings.UserSettings.Save();
					}
					else
					{
						checkBoxItemDocked.Checked = true;
					}
				}
				else if (result == DialogResult.No)
					Close();
			}
		}

		private void OnDockedStyleCheckedChanged(object sender, CheckBoxChangeEventArgs e)
		{
			AppManager.Instance.Settings.UserSettings.UseDockedStyle = checkBoxItemDocked.Checked;
			AppManager.Instance.Settings.UserSettings.Save();

			if (checkBoxItemDocked.Checked)
			{
				AppManager.Instance.Settings.UserSettings.AlwaysExpanded = AppManager.Instance.Settings.UserSettings.DefaultAlwaysExpanded;
				AdjustWindowPosition();

				buttonItemScreen1.Enabled = true;
				buttonItemScreen2.Enabled = true;
				buttonItemScreen3.Enabled = true;
				buttonItemScreen4.Enabled = true;
				buttonItemScreen5.Enabled = true;
				buttonItemScreen6.Enabled = true;

				_buttonItemDock.Visible = false;
				_buttonItemUndock.Visible = AppManager.Instance.Settings.Config.ShowUndockButton;
				_buttonItemCollapse.Visible = false;
				notifyIcon.Visible = false;
			}
			else
			{
				buttonItemScreen1.Enabled = false;
				buttonItemScreen2.Enabled = false;
				buttonItemScreen3.Enabled = false;
				buttonItemScreen4.Enabled = false;
				buttonItemScreen5.Enabled = false;
				buttonItemScreen6.Enabled = false;

				_buttonItemDock.Visible = AppManager.Instance.Settings.Config.ShowDockRegularButton;
				_buttonItemUndock.Visible = false;
				_buttonItemCollapse.Visible = true;
				notifyIcon.Visible = true;

				if (AppManager.Instance.Settings.UserSettings.DefaultShowFloaterWhenUndock)
					Opacity = 0;

				Move -= OnFormMainMove;
				Top = AppManager.Instance.Settings.UserSettings.FormLocationTop ?? Top;
				Left = AppManager.Instance.Settings.UserSettings.FormLocationLeft ?? Left;
				Move += OnFormMainMove;

				if (AppManager.Instance.Settings.UserSettings.AlwaysExpanded)
					UncollapseWindow();

				if (AppManager.Instance.Settings.UserSettings.DefaultShowFloaterWhenUndock)
				{
					AppManager.Instance.Settings.UserSettings.ShowFloaterWhenUndock = true;
					AppManager.Instance.Settings.UserSettings.Save();
					ShowFloaterForm();
				}
			}
		}

		private void OnUndockButtonClick(Object sender, EventArgs e)
		{
			checkBoxItemDocked.Checked = false;
			if (Height == AppManager.Instance.Settings.Config.CollapsedHeight)
				Top -= AppManager.Instance.Settings.Config.Height;
		}

		private void OnDockButtonClick(Object sender, EventArgs e)
		{
			checkBoxItemDocked.Checked = true;
		}

		private void OnCollapseClick(object sender, EventArgs e)
		{
			AppManager.Instance.Settings.UserSettings.ShowFloaterWhenUndock = true;
			AppManager.Instance.Settings.UserSettings.Save();
			ShowFloaterForm();
		}

		private void OnToolStripMenuItemDockClick(object sender, EventArgs e)
		{
			checkBoxItemDocked.Checked = true;
		}

		private void OnToolStripMenuItemCenterScreenClick(object sender, EventArgs e)
		{
			var screen = Screen.PrimaryScreen;
			Top = screen.Bounds.Height / 2;
			Left = (screen.Bounds.Width - Width) / 2;
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
			InitDockedStyleSettings();
		}

		private void OnFormShown(object sender, EventArgs e)
		{
			LoadBarContent();
			UpdateBrowserButtons();

			superTabControlMain.RecalcLayout();
			superTabControlMain.PerformLayout();

			if (!AppManager.Instance.Settings.UserSettings.UseDockedStyle && AppManager.Instance.Settings.UserSettings.ShowFloaterWhenUndock)
			{
				CollapseWindow();
				ShowFloaterForm();
			}
			else if (!AppManager.Instance.Settings.UserSettings.UseDockedStyle && AppManager.Instance.Settings.UserSettings.AlwaysExpanded)
			{
				UncollapseWindow();
				Opacity = 1;
				Refresh();
			}
			else
			{
				CollapseWindow();
				Opacity = 1;
				Refresh();
			}

			timerUpdateWindow.Start();

			AppManager.Instance.ActivityManager.AddActivity(new AdBarActivity(AdBarActivityType.ApplicationOpen));
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			if (!AppManager.Instance.Settings.UserSettings.UseDockedStyle)
			{
				AppManager.Instance.Settings.UserSettings.FormLocationLeft = Left;
				AppManager.Instance.Settings.UserSettings.FormLocationTop = Top;
				AppManager.Instance.Settings.UserSettings.Save();
			}

			AppManager.Instance.ActivityManager.AddActivity(new AdBarActivity(AdBarActivityType.ApplicationClose));
			AppManager.Instance.ExternalProcessesWatcher.StopWatching();
			Application.Exit();
		}

		private void OnFormDeactivate(object sender, EventArgs e)
		{
			CollapseWindow();
		}

		private void OnFormMainMove(object sender, EventArgs e)
		{
			if (!AppManager.Instance.Settings.UserSettings.UseDockedStyle)
			{
				AppManager.Instance.Settings.UserSettings.FormLocationLeft = Left;
				AppManager.Instance.Settings.UserSettings.FormLocationTop = Top;
				AppManager.Instance.Settings.UserSettings.Save();
			}
		}
		#endregion

		#region Ribbon Event Handlers
		private void OnTabControlPageClick(object sender, EventArgs e)
		{
			if (AppManager.Instance.Settings.UserSettings.UseDockedStyle)
				UncollapseWindow();
		}

		private void OnTabControlPageChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
		{
			if (!AppManager.Instance.Settings.UserSettings.UseDockedStyle)
				UncollapseWindow();
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

		private void OnTabControlMouseDown(object sender, MouseEventArgs e)
		{
			if (AppManager.Instance.Settings.UserSettings.UseDockedStyle) return;
			if (e.Button != MouseButtons.Left) return;
			if (e.Clicks > 1) return;
			WinAPIHelper.ReleaseCapture();
			WinAPIHelper.SendMessage(Handle, WinAPIHelper.WM_NCLBUTTONDOWN, WinAPIHelper.HTCAPTION, IntPtr.Zero);
		}

		private void OnTabControlDoubleClick(object sender, MouseEventArgs e)
		{
			if (AppManager.Instance.Settings.UserSettings.UseDockedStyle) return;
			AppManager.Instance.Settings.UserSettings.AlwaysExpanded =
				!AppManager.Instance.Settings.UserSettings.AlwaysExpanded;
			CollapseWindow();
		}
		#endregion
	}
}
