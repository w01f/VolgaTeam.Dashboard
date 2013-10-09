using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.CommonGUI.Slides;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.MiniBar.BusinessClasses;
using NewBizWiz.MiniBar.InteropClasses;
using NewBizWiz.MiniBar.SettingsForms;
using NewBizWiz.MiniBar.ToolForms;
using Application = System.Windows.Forms.Application;
using Font = System.Drawing.Font;
using Point = System.Drawing.Point;
using SettingsManager = NewBizWiz.MiniBar.BusinessClasses.SettingsManager;
using TabPage = NewBizWiz.MiniBar.BusinessClasses.TabPage;

namespace NewBizWiz.MiniBar
{
	public partial class FormMainExpanded : Form
	{
		private static FormMainExpanded _instance;
		private readonly Timer _formMouseLeaveTimer;
		private int _mouseLeaveAdditionalTime;
		private readonly Timer _hideTimer;
		private bool _allowToSave;
		private bool _comboOpened;
		private bool _expanded;

		private FormMainExpanded()
		{
			InitializeComponent();

			ribbonControl.Height = 130;

			_formMouseLeaveTimer = new Timer();
			_formMouseLeaveTimer.Interval = 2000;
			_formMouseLeaveTimer.Tick += FormMouseLeaveTimer_Tick;

			_hideTimer = new Timer();
			_hideTimer.Interval = 30;
			_hideTimer.Tick += _hideTimer_Tick;
			_hideTimer.Start();

			if ((base.CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
				comboBoxEditPowerPointStyle.Font = font;
				ribbonControl.Font = new Font(ribbonControl.Font.FontFamily, ribbonControl.Font.Size - 1, ribbonControl.Font.Style);
				ribbonBarApps1.RecalcLayout();
				ribbonBarApps1Exit.RecalcLayout();
				ribbonBarApps1Help.RecalcLayout();
				ribbonBarDashboard.RecalcLayout();
				ribbonBarDashboardExit.RecalcLayout();
				ribbonBarDashboardHelp.RecalcLayout();
				ribbonBarPowerPointExit.RecalcLayout();
				ribbonBarPowerPointHelp.RecalcLayout();
				ribbonBarPowerPointLaunch.RecalcLayout();
				ribbonBarPowerPointPresentationSettings.RecalcLayout();
				ribbonBarSalesDepot.RecalcLayout();
				ribbonBarSalesDepotRemote.RecalcLayout();
				ribbonBarSalesDepotHelp.RecalcLayout();
				ribbonBarSetingsHelp.RecalcLayout();
				ribbonBarSettingsExit.RecalcLayout();
				ribbonBarSettingsMinibar.RecalcLayout();
				ribbonBarSalesDepotExit.RecalcLayout();
				ribbonBarSyncExit.RecalcLayout();
				ribbonBarSyncHelp.RecalcLayout();
				ribbonBarSyncHourly.RecalcLayout();
				ribbonBarSyncStart.RecalcLayout();
				ribbonBarSyncStatus.RecalcLayout();
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

			#region Init Activity Recording Events

			buttonItemApps1Help.Click += ribbonTabItem_Click;
			buttonItemApps1Clipart.Click += ribbonTabItem_Click;
			buttonItemDashboard.Click += ribbonTabItem_Click;
			buttonItemDashboardHelp.Click += ribbonTabItem_Click;
			buttonItemPowerPointHelp.Click += ribbonTabItem_Click;
			buttonItemPowerPointLaunch.Click += ribbonTabItem_Click;
			buttonItemPowerPointSize1.Click += ribbonTabItem_Click;
			buttonItemPowerPointSize2.Click += ribbonTabItem_Click;
			buttonItemPowerPointSize3.Click += ribbonTabItem_Click;
			buttonItemPowerPointSize4.Click += ribbonTabItem_Click;
			buttonItemPowerPointSize5.Click += ribbonTabItem_Click;
			buttonItemSalesDepotHelp.Click += ribbonTabItem_Click;
			buttonItemSettingsDesktop.Click += ribbonTabItem_Click;
			buttonItemSettingsHelp.Click += ribbonTabItem_Click;
			buttonItemSettingsKillExcel.Click += ribbonTabItem_Click;
			buttonItemSettingsKilPowerPoint.Click += ribbonTabItem_Click;
			buttonItemSettingsMinibar.Click += ribbonTabItem_Click;
			buttonItemSettingsMonitor1.Click += ribbonTabItem_Click;
			buttonItemSettingsMonitor2.Click += ribbonTabItem_Click;
			buttonItemSettingsReset.Click += ribbonTabItem_Click;
			buttonItemSettingsPresentationOrganizer.Click += ribbonTabItem_Click;
			buttonItemSyncHelp.Click += ribbonTabItem_Click;
			buttonItemSyncHourlyOff.Click += ribbonTabItem_Click;
			buttonItemSyncHourlyOn.Click += ribbonTabItem_Click;
			buttonItemSyncStart.Click += ribbonTabItem_Click;

			#endregion
		}

		#region Form Event Handlers

		private void FormMain_Deactivate(object sender, EventArgs e)
		{
			ActivateRetractedForm();
		}

		private void comboBoxEdit_Popup(object sender, EventArgs e)
		{
			_comboOpened = true;
		}

		private void comboBoxEdit_Closed(object sender, ClosedEventArgs e)
		{
			_comboOpened = false;
		}
		#endregion

		#region Timer Ticks

		private void FormMouseLeaveTimer_Tick(object sender, EventArgs e)
		{
			var pos = MousePosition;
			bool inForm = pos.X >= Left && pos.Y >= Top && pos.X < Right && pos.Y < Bottom;

			if (!inForm && _expanded && !_comboOpened)
			{
				if (_mouseLeaveAdditionalTime > 0)
				{
					_mouseLeaveAdditionalTime--;
					return;
				}
				ActivateRetractedForm();
			}
		}

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
			_expanded = true;
			_formMouseLeaveTimer.Start();
			((Timer)sender).Enabled = false;
			((Timer)sender).Dispose();
		}

		private void FadeOutTimer_Tick(object sender, EventArgs e)
		{
			_expanded = false;
			if (Opacity > 0 && !SettingsManager.Instance.QuickRetraction)
			{
				Opacity -= 0.07;
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
				Screen screen = Screen.PrimaryScreen;
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
			}
		}

		#endregion

		#region Buttons Clicks

		private void ribbonTabItem_Click(object sender, EventArgs e)
		{
			ServiceDataManager.Instance.WriteActivity();
		}

		private void buttonItemExit_Click(object sender, EventArgs e)
		{
			ServiceDataManager.Instance.WriteActivity();
			if (SettingsManager.Instance.CloseFloat)
			{
				ShowFloater();
			}
			else if (AppManager.Instance.ShowWarningQuestion("Are you Sure You want to close the Minibar?") == DialogResult.Yes)
			{
				AppManager.Instance.ShowInformation("To open Minibar in the future, double-click desktop shortcut for Minibar...");
				if (SettingsManager.Instance.CloseHide)
					lock (AppManager.Locker)
					{
						RegistryHelper.ShowHidden = true;
					}
				else if (SettingsManager.Instance.CloseShutdown)
					Application.Exit();
			}
		}

		private void buttonItemHelp_Click(object sender, EventArgs e)
		{
			AppManager.Instance.HelpManager.OpenHelpLink(ribbonControl.SelectedRibbonTabItem.Tag.ToString());
		}

		#endregion

		#region PowerPoint Methods

		private void RunPowerPoint()
		{
			AppManager.Instance.RunPowerPointLoader();
		}

		private void SetPresentationSettings()
		{
			if (Core.Common.SettingsManager.Instance.Orientation.Equals("Landscape"))
			{
				if (Core.Common.SettingsManager.Instance.SizeWidth == 10 && Core.Common.SettingsManager.Instance.SizeHeght == 7.5)
				{
					buttonItemPowerPointSize1.Checked = true;
					buttonItemPowerPointSize2.Checked = false;
					buttonItemPowerPointSize3.Checked = false;
					buttonItemPowerPointSize4.Checked = false;
					buttonItemPowerPointSize5.Checked = false;
				}
				else if (Core.Common.SettingsManager.Instance.SizeWidth == 10.75 && Core.Common.SettingsManager.Instance.SizeHeght == 8.25)
				{
					buttonItemPowerPointSize1.Checked = false;
					buttonItemPowerPointSize2.Checked = true;
					buttonItemPowerPointSize3.Checked = false;
					buttonItemPowerPointSize4.Checked = false;
					buttonItemPowerPointSize5.Checked = false;
				}
				else if (Core.Common.SettingsManager.Instance.SizeWidth == 10 && Core.Common.SettingsManager.Instance.SizeHeght == 5.63)
				{
					buttonItemPowerPointSize1.Checked = false;
					buttonItemPowerPointSize2.Checked = false;
					buttonItemPowerPointSize3.Checked = true;
					buttonItemPowerPointSize4.Checked = false;
					buttonItemPowerPointSize5.Checked = false;
				}
			}
			else
			{
				if (Core.Common.SettingsManager.Instance.SizeWidth == 10 && Core.Common.SettingsManager.Instance.SizeHeght == 7.5)
				{
					buttonItemPowerPointSize1.Checked = false;
					buttonItemPowerPointSize2.Checked = false;
					buttonItemPowerPointSize3.Checked = false;
					buttonItemPowerPointSize4.Checked = true;
					buttonItemPowerPointSize5.Checked = false;
				}
				else if (Core.Common.SettingsManager.Instance.SizeWidth == 10.75 && Core.Common.SettingsManager.Instance.SizeHeght == 8.25)
				{
					buttonItemPowerPointSize1.Checked = false;
					buttonItemPowerPointSize2.Checked = false;
					buttonItemPowerPointSize3.Checked = false;
					buttonItemPowerPointSize4.Checked = false;
					buttonItemPowerPointSize5.Checked = true;
				}
			}
		}

		private void SelectMasterWizard(string name)
		{
			MasterWizard masterWizard = null;
			MasterWizardManager.Instance.MasterWizards.TryGetValue(name, out masterWizard);
			MasterWizardManager.Instance.SelectedWizard = masterWizard;
			if (MasterWizardManager.Instance.SelectedWizard == null) return;
			Core.Common.SettingsManager.Instance.SelectedWizard = masterWizard.Name;

			UpdateSlideSize();

			UpdateSlideMasters();

			UpdateApplicationsStatus();

			Core.Common.SettingsManager.Instance.SaveSharedSettings();
		}

		private void UpdateSlideSize()
		{
			buttonItemPowerPointSize1.Enabled = MasterWizardManager.Instance.SelectedWizard.Has43;
			if (buttonItemPowerPointSize1.Checked && !buttonItemPowerPointSize1.Enabled)
				buttonItemPowerPointSize1.Checked = false;
			buttonItemPowerPointSize2.Enabled = MasterWizardManager.Instance.SelectedWizard.Has54;
			if (buttonItemPowerPointSize2.Checked && !buttonItemPowerPointSize2.Enabled)
				buttonItemPowerPointSize2.Checked = false;
			buttonItemPowerPointSize3.Enabled = MasterWizardManager.Instance.SelectedWizard.Has169;
			if (buttonItemPowerPointSize3.Checked && !buttonItemPowerPointSize3.Enabled)
				buttonItemPowerPointSize3.Checked = false;
			buttonItemPowerPointSize4.Enabled = MasterWizardManager.Instance.SelectedWizard.Has34;
			if (buttonItemPowerPointSize4.Checked && !buttonItemPowerPointSize4.Enabled)
				buttonItemPowerPointSize4.Checked = false;
			buttonItemPowerPointSize5.Enabled = MasterWizardManager.Instance.SelectedWizard.Has45;
			if (buttonItemPowerPointSize5.Checked && !buttonItemPowerPointSize5.Enabled)
				buttonItemPowerPointSize5.Checked = false;

			if (!buttonItemPowerPointSize1.Checked && !buttonItemPowerPointSize2.Checked && !buttonItemPowerPointSize3.Checked && !buttonItemPowerPointSize4.Checked && !buttonItemPowerPointSize5.Checked)
			{
				if (buttonItemPowerPointSize1.Enabled)
					buttonItemPowerPointSize1.Checked = true;
				else if (buttonItemPowerPointSize2.Enabled)
					buttonItemPowerPointSize2.Checked = true;
				else if (buttonItemPowerPointSize3.Enabled)
					buttonItemPowerPointSize3.Checked = true;
				else if (buttonItemPowerPointSize4.Enabled)
					buttonItemPowerPointSize4.Checked = true;
				else if (buttonItemPowerPointSize5.Enabled)
					buttonItemPowerPointSize5.Checked = true;
			}
		}

		private void UpdateSlideMasters()
		{
			var availableSlide = AppManager.Instance.SlideManager.Slides.Where(s => s.SizeWidth == Core.Common.SettingsManager.Instance.SizeWidth && s.SizeHeght == Core.Common.SettingsManager.Instance.SizeHeght);
			if (availableSlide.Any())
			{
				var selectedSlide = availableSlide.FirstOrDefault(s => s.Group.Equals(SettingsManager.Instance.SelectedSlideGroup) && s.Name.Equals(SettingsManager.Instance.SelectedSlideMaster));
				if (selectedSlide == null)
				{
					selectedSlide = availableSlide.FirstOrDefault();
					SettingsManager.Instance.SelectedSlideGroup = selectedSlide.Group;
					SettingsManager.Instance.SelectedSlideMaster = selectedSlide.Name;
					SettingsManager.Instance.SaveMinibarSettings();
				}
				buttonItemPowerPointOutput.Enabled = true;
				buttonItemPowerPointSlideMaster.Visible = true;
				buttonItemPowerPointSlideMaster.Image = selectedSlide.RibbonLogo;
				ribbonBarPowerPointSlideMaster.Text = String.Format("{0}", selectedSlide.Name);
				buttonItemPowerPointSlideMaster.Tag = selectedSlide;
			}
			else
			{
				buttonItemPowerPointOutput.Enabled = false;
				buttonItemPowerPointSlideMaster.Visible = false;
				ribbonBarPowerPointSlideMaster.Text = "No Slides";
			}
			ribbonBarPowerPointSlideMaster.RecalcLayout();
			ribbonPanelPowerPoint.PerformLayout();
		}

		private void AppendSlideMaster(SlideMaster slideMaster)
		{
			if (slideMaster == null) return;
			if (!MinibarPowerPointHelper.Instance.PowerPointDetected())
			{
				if (AppManager.Instance.ShowWarningQuestion("You need to first open PowerPoint\nDo you want to do that now?") == DialogResult.Yes)
					RunPowerPoint();
				else
					return;
			}
			MinibarPowerPointHelper.Instance.Connect(false);
			MinibarPowerPointHelper.Instance.AppendSlideMaster(slideMaster.MasterPath);
			Utilities.Instance.ActivateForm(Handle, false, true);
		}

		private void buttonItemSize_Click(object sender, EventArgs e)
		{
			if (AppManager.Instance.AplicationDetected())
			{
				if (AppManager.Instance.ShowWarningQuestion("All active applications will be closed before you change PowerPoint settings.\nDo you want to continue?") == DialogResult.Yes)
					AppManager.Instance.CloseActiveApplications();
				else
					return;
			}
			if (MinibarPowerPointHelper.Instance.PowerPointDetected())
			{
				using (var form = new FormFormatChangeNotification())
				{
					var buttonPressed = (sender as ButtonItem);
					string currentFormatText = Core.Common.SettingsManager.Instance.SlideSize;
					string futureFormatText = string.Empty;
					if (buttonPressed == buttonItemPowerPointSize1)
						futureFormatText = "Landscape 4 x 3";
					else if (buttonPressed == buttonItemPowerPointSize2)
						futureFormatText = "Landscape 5 x 4";
					else if (buttonPressed == buttonItemPowerPointSize3)
						futureFormatText = "Landscape 16 x 9";
					else if (buttonPressed == buttonItemPowerPointSize4)
						futureFormatText = "Portrait 3 x 4";
					else if (buttonPressed == buttonItemPowerPointSize5)
						futureFormatText = "Portrait 4 x 5";
					form.labelControlCurrentState.Text = "Your curent presentation is: " + currentFormatText;
					form.labelControlFutureState.Text = "You want to change your presentation to: " + futureFormatText;
					if (form.ShowDialog() != DialogResult.Yes)
						return;
				}
			}
			buttonItemPowerPointSize1.Checked = false;
			buttonItemPowerPointSize2.Checked = false;
			buttonItemPowerPointSize3.Checked = false;
			buttonItemPowerPointSize4.Checked = false;
			buttonItemPowerPointSize5.Checked = false;
			(sender as ButtonItem).Checked = true;
			UpdateSlideMasters();
			Core.Common.SettingsManager.Instance.SaveSharedSettings();
			MinibarPowerPointHelper.Instance.Connect(false);
			MinibarPowerPointHelper.Instance.SetPresentationSettings();
		}

		private void buttonItemSize_CheckedChanged(object sender, EventArgs e)
		{
			if ((sender as ButtonItem).Checked)
			{
				if (buttonItemPowerPointSize1.Checked)
				{
					Core.Common.SettingsManager.Instance.SizeWidth = 10;
					Core.Common.SettingsManager.Instance.SizeHeght = 7.5;
					Core.Common.SettingsManager.Instance.Orientation = "Landscape";
				}
				else if (buttonItemPowerPointSize2.Checked)
				{
					Core.Common.SettingsManager.Instance.SizeWidth = 10.75;
					Core.Common.SettingsManager.Instance.SizeHeght = 8.25;
					Core.Common.SettingsManager.Instance.Orientation = "Landscape";
				}
				else if (buttonItemPowerPointSize3.Checked)
				{
					Core.Common.SettingsManager.Instance.SizeWidth = 13;
					Core.Common.SettingsManager.Instance.SizeHeght = 7.32;
					Core.Common.SettingsManager.Instance.Orientation = "Landscape";
				}
				else if (buttonItemPowerPointSize4.Checked)
				{
					Core.Common.SettingsManager.Instance.SizeWidth = 7.5;
					Core.Common.SettingsManager.Instance.SizeHeght = 10;
					Core.Common.SettingsManager.Instance.Orientation = "Portrait";
				}
				else if (buttonItemPowerPointSize5.Checked)
				{
					Core.Common.SettingsManager.Instance.SizeWidth = 8.25;
					Core.Common.SettingsManager.Instance.SizeHeght = 10.75;
					Core.Common.SettingsManager.Instance.Orientation = "Portrait";
				}
				UpdateApplicationsStatus();
			}
		}

		private void comboBoxEditPowerPointStyle_EditValueChanging(object sender, ChangingEventArgs e)
		{
			e.Cancel = false;
			if (AppManager.Instance.AplicationDetected())
			{
				if (AppManager.Instance.ShowWarningQuestion("All active applications will be closed before you change PowerPoint settings.\nDo you want to continue?") == DialogResult.Yes)
				{
					AppManager.Instance.CloseActiveApplications();
					e.Cancel = false;
				}
				else
				{
					e.Cancel = true;
					return;
				}
			}
			if (MinibarPowerPointHelper.Instance.PowerPointDetected())
			{
				using (var form = new FormFormatChangeNotification())
				{
					string currentFormatText = Core.Common.SettingsManager.Instance.SelectedWizard;
					string futureFormatText = e.NewValue != null ? e.NewValue.ToString() : string.Empty;
					form.labelControlCurrentState.Text = "Your curent wizard is: " + currentFormatText;
					form.labelControlFutureState.Text = "You want to change your wizard to: " + futureFormatText;
					if (form.ShowDialog() == DialogResult.Yes)
						e.Cancel = false;
					else
						e.Cancel = true;
				}
			}
		}

		private void comboBoxEditPowerPointStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxEditPowerPointStyle.SelectedIndex >= 0)
			{
				SelectMasterWizard(comboBoxEditPowerPointStyle.EditValue.ToString());
			}
		}

		private void buttonItemPowerPointOutput_Click(object sender, EventArgs e)
		{
			var selectedSlideMaster = buttonItemPowerPointSlideMaster.Tag as SlideMaster;
			AppendSlideMaster(selectedSlideMaster);
		}

		private void buttonItemPowerPointSlideMaster_Click(object sender, EventArgs e)
		{
			using (var form = new FormSlideSelector())
			{
				form.LoadSlides(AppManager.Instance.SlideManager);
				form.Shown += (o, args) =>
				{
					form.SetSelectedSlide(SettingsManager.Instance.SelectedSlideGroup, SettingsManager.Instance.SelectedSlideMaster);
				};

				form.AddSlide += (o, args) => AppendSlideMaster(args.SelectedSlide);
				if (form.ShowDialog() == DialogResult.OK)
				{
					var selectedSlide = form.SelectedSlide;
					if (selectedSlide == null) return;
					buttonItemPowerPointSlideMaster.Image = selectedSlide.RibbonLogo;
					ribbonBarPowerPointSlideMaster.Text = String.Format("{0}", selectedSlide.Name);
					buttonItemPowerPointSlideMaster.Tag = selectedSlide;
					ribbonBarPowerPointSlideMaster.RecalcLayout();
					ribbonPanelPowerPoint.PerformLayout();

					SettingsManager.Instance.SelectedSlideGroup = selectedSlide.Group;
					SettingsManager.Instance.SelectedSlideMaster = selectedSlide.Name;
					SettingsManager.Instance.SaveMinibarSettings();
				}
				_mouseLeaveAdditionalTime = 2;
				FormMain.Instance.ActivateExpandedForm();
			}
		}
		#endregion

		#region Dashboard Methods
		private void buttonItemDashboard_Click(object sender, EventArgs e)
		{
			AppManager.Instance.RunDashboard();
		}

		private void buttonItemPowerPointLaunch_Click(object sender, EventArgs e)
		{
			RunPowerPoint();
			Utilities.Instance.ActivateForm(Handle, false, true);
		}
		#endregion

		#region SalesDepot
		private void buttonItemSalesDepotRemote_Click(object sender, EventArgs e)
		{
			AppManager.Instance.RunSalesDepotRemote();
		}

		private void LoadSalesDepot()
		{
			if (SettingsManager.Instance.SalesDepotSettings.ShowLocalButton && SettingsManager.Instance.SalesDepotSettings.ShowWebButton)
			{
				var galleryContainer = new GalleryContainer();

				var label = new LabelItem();
				label.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, ((204)));
				label.Text = SettingsManager.Instance.SalesDepotSettings.LocalAppName;
				label.Visible = false;
				label.Click += (sender, args) =>
								   {
									   AppManager.Instance.RunLocalSalesDepot();
									   ServiceDataManager.Instance.WriteActivity();
								   };
				var button = new ButtonItem();
				if (File.Exists(SettingsManager.Instance.SalesDepotSettings.LocalLogoPath) && File.Exists(SettingsManager.Instance.SalesDepotSettings.LocalIconPath))
					button.Image = new Bitmap(SettingsManager.Instance.SalesDepotSettings.LocalLogoPath);
				button.Click += (sender, args) =>
									{
										AppManager.Instance.RunLocalSalesDepot();
										ServiceDataManager.Instance.WriteActivity();
									};
				superTooltip.SetSuperTooltip(button, new SuperTooltipInfo(SettingsManager.Instance.SalesDepotSettings.LocalAppName, string.Empty, "Access PowerPoint slides to create a Client Solution", null, null, eTooltipColor.Default, true, false, new Size(0, 0)));
				var itemContainer = new ItemContainer();
				itemContainer.SubItems.AddRange(new BaseItem[]
				                                {
					                                button,
					                                label
				                                });
				galleryContainer.SubItems.AddRange(new BaseItem[] { itemContainer });


				label = new LabelItem();
				label.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, ((204)));
				label.Text = SettingsManager.Instance.SalesDepotSettings.WebAppName;
				label.Visible = false;
				label.Click += (sender, args) =>
								   {
									   AppManager.Instance.RunWebSalesDepot();
									   ServiceDataManager.Instance.WriteActivity();
								   };
				button = new ButtonItem();
				if (File.Exists(SettingsManager.Instance.SalesDepotSettings.WebLogoPath) && File.Exists(SettingsManager.Instance.SalesDepotSettings.WebIconPath))
					button.Image = new Bitmap(SettingsManager.Instance.SalesDepotSettings.WebLogoPath);
				button.Click += (sender, args) =>
									{
										AppManager.Instance.RunWebSalesDepot();
										ServiceDataManager.Instance.WriteActivity();
									};
				superTooltip.SetSuperTooltip(button, new SuperTooltipInfo(SettingsManager.Instance.SalesDepotSettings.WebAppName, string.Empty, "Access corporate web site to create a Client Solution", null, null, eTooltipColor.Default, true, false, new Size(0, 0)));
				itemContainer = new ItemContainer();
				itemContainer.SubItems.AddRange(new BaseItem[]
				                                {
					                                button,
					                                label
				                                });
				galleryContainer.SubItems.AddRange(new BaseItem[] { itemContainer });

				ribbonBarSalesDepot.Items.Add(galleryContainer);
			}
			else if (SettingsManager.Instance.SalesDepotSettings.ShowWebButton)
			{
				var label = new LabelItem();
				label.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, ((204)));
				label.Text = SettingsManager.Instance.SalesDepotSettings.WebAppName;
				label.Visible = false;
				label.Click += (sender, args) => AppManager.Instance.RunWebSalesDepot();
				var button = new ButtonItem();
				if (File.Exists(SettingsManager.Instance.SalesDepotSettings.WebLogoPath) && File.Exists(SettingsManager.Instance.SalesDepotSettings.WebIconPath))
					button.Image = new Bitmap(SettingsManager.Instance.SalesDepotSettings.WebLogoPath);
				button.Click += (sender, args) => AppManager.Instance.RunWebSalesDepot();
				superTooltip.SetSuperTooltip(button, new SuperTooltipInfo(SettingsManager.Instance.SalesDepotSettings.WebAppName, string.Empty, "Access corporate web site to create a Client Solution", null, null, eTooltipColor.Default, true, false, new Size(0, 0)));
				var itemContainer = new ItemContainer();
				itemContainer.SubItems.AddRange(new BaseItem[]
				                                {
					                                button,
					                                label
				                                });
				ribbonBarSalesDepot.Items.Add(itemContainer);
			}
			else
			{
				var label = new LabelItem();
				label.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, ((204)));
				label.Text = SettingsManager.Instance.SalesDepotSettings.LocalAppName;
				label.Visible = false;
				label.Click += (sender, args) => AppManager.Instance.RunLocalSalesDepot();
				var button = new ButtonItem();
				if (File.Exists(SettingsManager.Instance.SalesDepotSettings.LocalLogoPath) && File.Exists(SettingsManager.Instance.SalesDepotSettings.LocalIconPath))
					button.Image = new Bitmap(SettingsManager.Instance.SalesDepotSettings.LocalLogoPath);
				button.Click += (sender, args) => AppManager.Instance.RunLocalSalesDepot();
				superTooltip.SetSuperTooltip(button, new SuperTooltipInfo(SettingsManager.Instance.SalesDepotSettings.LocalAppName, string.Empty, "Access PowerPoint slides to create a Client Solution", null, null, eTooltipColor.Default, true, false, new Size(0, 0)));
				var itemContainer = new ItemContainer();
				itemContainer.SubItems.AddRange(new BaseItem[]
				                                {
					                                button,
					                                label
				                                });
				ribbonBarSalesDepot.Items.Add(itemContainer);
			}
			ribbonBarSalesDepot.Text = SettingsManager.Instance.SalesDepotSettings.GroupName;
			ribbonBarSalesDepot.RecalcLayout();

			ribbonBarSalesDepotRemote.Visible = Directory.Exists(SettingsManager.Instance.SalesDepotSettings.RemoteRootPath) & SettingsManager.Instance.SalesDepotSettings.UseRemoteSalesDepot;
			ribbonBarSalesDepotBrowser.Visible = SettingsManager.Instance.SalesDepotSettings.ShowWebButton;

			buttonItemSalesDepotBrowserChrome.Enabled = SettingsManager.Instance.SalesDepotSettings.ShowWebButton && Utilities.Instance.ChromeInstalled;
			buttonItemSalesDepotBrowserChrome.Checked = SettingsManager.Instance.SalesDepotBrowser == BrowserType.Chrome;
			if (buttonItemSalesDepotBrowserChrome.Enabled)
			{
				buttonItemSalesDepotBrowserChrome.Click += buttonItemSalesDepotBrowser_Click;
				buttonItemSalesDepotBrowserChrome.CheckedChanged += buttonItemSalesDepotBrowser_CheckedChanged;
			}

			buttonItemSalesDepotBrowserFirefox.Enabled = SettingsManager.Instance.SalesDepotSettings.ShowWebButton && Utilities.Instance.FirefoxInstalled;
			buttonItemSalesDepotBrowserFirefox.Checked = SettingsManager.Instance.SalesDepotBrowser == BrowserType.Firefox;
			if (buttonItemSalesDepotBrowserFirefox.Enabled)
			{
				buttonItemSalesDepotBrowserFirefox.Click += buttonItemSalesDepotBrowser_Click;
				buttonItemSalesDepotBrowserFirefox.CheckedChanged += buttonItemSalesDepotBrowser_CheckedChanged;
			}

			buttonItemSalesDepotBrowserOpera.Enabled = SettingsManager.Instance.SalesDepotSettings.ShowWebButton && Utilities.Instance.OperaInstalled;
			buttonItemSalesDepotBrowserOpera.Checked = SettingsManager.Instance.SalesDepotBrowser == BrowserType.Opera;
			if (buttonItemSalesDepotBrowserOpera.Enabled)
			{
				buttonItemSalesDepotBrowserOpera.Click += buttonItemSalesDepotBrowser_Click;
				buttonItemSalesDepotBrowserOpera.CheckedChanged += buttonItemSalesDepotBrowser_CheckedChanged;
			}

			buttonItemSalesDepotBrowserIE.Checked = SettingsManager.Instance.SalesDepotBrowser == BrowserType.IE;
			buttonItemSalesDepotBrowserIE.Click += buttonItemSalesDepotBrowser_Click;
			buttonItemSalesDepotBrowserIE.CheckedChanged += buttonItemSalesDepotBrowser_CheckedChanged;

			ribbonPanelSalesDepot.PerformLayout();
		}

		private void buttonItemSalesDepotBrowser_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonItem;
			if (button != null && !button.Checked)
			{
				buttonItemSalesDepotBrowserChrome.Checked = false;
				buttonItemSalesDepotBrowserFirefox.Checked = false;
				buttonItemSalesDepotBrowserOpera.Checked = false;
				buttonItemSalesDepotBrowserIE.Checked = false;
				button.Checked = true;
			}
		}

		private void buttonItemSalesDepotBrowser_CheckedChanged(object sender, EventArgs e)
		{
			var button = sender as ButtonItem;
			if (button != null && button.Checked)
			{
				if (buttonItemSalesDepotBrowserChrome.Checked)
					SettingsManager.Instance.SalesDepotBrowser = BrowserType.Chrome;
				else if (buttonItemSalesDepotBrowserFirefox.Checked)
					SettingsManager.Instance.SalesDepotBrowser = BrowserType.Firefox;
				else if (buttonItemSalesDepotBrowserOpera.Checked)
					SettingsManager.Instance.SalesDepotBrowser = BrowserType.Opera;
				else if (buttonItemSalesDepotBrowserIE.Checked)
					SettingsManager.Instance.SalesDepotBrowser = BrowserType.IE;
				SettingsManager.Instance.SaveMinibarSettings();
			}
		}
		#endregion

		#region Apps Methods
		private void LoadNBWApplication()
		{
			var tab1Applications = NBWApplicationsManager.Instance.Links.Where(a => a.TabOrder == 1);
			if (tab1Applications.Any())
			{
				foreach (var nbwApplication in tab1Applications)
					AddAppDefinition(galleryContainerApps1, nbwApplication);
				ribbonBarApps1.RecalcLayout();
				ribbonPanelApps1.PerformLayout();
			}

			var tab2Applications = NBWApplicationsManager.Instance.Links.Where(a => a.TabOrder == 2);
			if (tab2Applications.Any())
			{
				foreach (var nbwApplication in tab2Applications)
					AddAppDefinition(galleryContainerApps2, nbwApplication);
				ribbonBarApps2.RecalcLayout();
				ribbonPanelApps2.PerformLayout();
			}

			var tab3Applications = NBWApplicationsManager.Instance.Links.Where(a => a.TabOrder == 3);
			if (tab3Applications.Any())
			{
				foreach (var nbwApplication in tab3Applications)
					AddAppDefinition(galleryContainerApps3, nbwApplication);
				ribbonBarApps3.RecalcLayout();
				ribbonPanelApps3.PerformLayout();
			}

			var tab4Applications = NBWApplicationsManager.Instance.Links.Where(a => a.TabOrder == 4);
			if (tab4Applications.Any())
			{
				foreach (var nbwApplication in tab4Applications)
					AddAppDefinition(galleryContainerApps4, nbwApplication);
				ribbonBarApps4.RecalcLayout();
				ribbonPanelApps4.PerformLayout();
			}

			var tab5Applications = NBWApplicationsManager.Instance.Links.Where(a => a.TabOrder == 5);
			if (tab5Applications.Any())
			{
				foreach (var nbwApplication in tab5Applications)
					AddAppDefinition(galleryContainerApps5, nbwApplication);
				ribbonBarApps5.RecalcLayout();
				ribbonPanelApps5.PerformLayout();
			}
		}

		private void UpdateApplicationsStatus()
		{
			foreach (var nbwApplication in NBWApplicationsManager.Instance.Links.OfType<NBWApplication>())
			{
				if (!nbwApplication.UseSlideTemplates || MasterWizardManager.Instance.SelectedWizard == null) continue;
				string slideTemplatesFolderPath;
				if (nbwApplication.UseWizard)
					slideTemplatesFolderPath = Path.Combine(MasterWizardManager.Instance.SelectedWizard.Folder.FullName, Core.Common.SettingsManager.Instance.SlideFolder, nbwApplication.SlideTemplatesPath);
				else
					slideTemplatesFolderPath = Path.Combine(MasterWizardManager.ScheduleBuildersFolder, Core.Common.SettingsManager.Instance.SlideFolder, nbwApplication.SlideTemplatesPath);
				nbwApplication.AppButton.Enabled = Directory.Exists(slideTemplatesFolderPath);
			}
		}

		private void AddAppDefinition(GalleryContainer container, NBWLink nbwLink)
		{
			var itemContainerApp = new ItemContainer();
			itemContainerApp.SubItems.AddRange(nbwLink.Controls.ToArray());
			container.SubItems.AddRange(new BaseItem[] { itemContainerApp });
		}

		private void SetClipartSettings()
		{
			switch (SettingsManager.Instance.ClipartSettings.TabPage)
			{
				case TabNamesEnum.Apps1:
					ribbonBarApps1Clipart.Visible = true;
					ribbonBarApps1Clipart.Text = SettingsManager.Instance.ClipartSettings.Name;
					ribbonBarApps1Clipart.Enabled = SettingsManager.Instance.ClipartSettings.Enabled;
					break;
				case TabNamesEnum.Apps2:
					ribbonBarApps2Clipart.Visible = true;
					ribbonBarApps2Clipart.Text = SettingsManager.Instance.ClipartSettings.Name;
					ribbonBarApps2Clipart.Enabled = SettingsManager.Instance.ClipartSettings.Enabled;
					break;
				case TabNamesEnum.Apps3:
					ribbonBarApps3Clipart.Visible = true;
					ribbonBarApps3Clipart.Text = SettingsManager.Instance.ClipartSettings.Name;
					ribbonBarApps3Clipart.Enabled = SettingsManager.Instance.ClipartSettings.Enabled;
					break;
				case TabNamesEnum.Apps4:
					ribbonBarApps4Clipart.Visible = true;
					ribbonBarApps4Clipart.Text = SettingsManager.Instance.ClipartSettings.Name;
					ribbonBarApps4Clipart.Enabled = SettingsManager.Instance.ClipartSettings.Enabled;
					break;
				case TabNamesEnum.Apps5:
					ribbonBarApps5Clipart.Visible = true;
					ribbonBarApps5Clipart.Text = SettingsManager.Instance.ClipartSettings.Name;
					ribbonBarApps5Clipart.Enabled = SettingsManager.Instance.ClipartSettings.Enabled;
					break;
			}
		}

		private void buttonItemAppClipart_Click(object sender, EventArgs e)
		{
			AppManager.Instance.RunSalesGallery();
		}
		#endregion

		#region Settings Methods
		private void buttonItemSettingsPresentationOrganizer_Click(object sender, EventArgs e)
		{
			if (!MinibarPowerPointHelper.Instance.PowerPointDetected())
			{
				AppManager.Instance.ShowWarning("You have no Active PowerPoint Presentation.");
				return;
			}
			MinibarPowerPointHelper.Instance.Connect(false);
			Utilities.Instance.ActivateForm(Handle, false, true);
			using (var form = new FormPresentationOrganizer())
			{
				form.ShowDialog();
			}
			Utilities.Instance.ActivateForm(Handle, false, true);
		}

		private void buttonItemSettingsTeamViewer_Click(object sender, EventArgs e)
		{
			if (File.Exists(SettingsManager.Instance.TeamViewerQSPath))
			{
				Process.Start(SettingsManager.Instance.TeamViewerQSPath);
				return;
			}
			AppManager.Instance.ShowWarning("TeamViewer was not found");
		}

		private void buttonItemSettingsWebcast_Click(object sender, EventArgs e)
		{
			if (File.Exists(SettingsManager.Instance.TeamViewerQJPath))
			{
				Process.Start(SettingsManager.Instance.TeamViewerQJPath);
				return;
			}
			AppManager.Instance.ShowWarning("TeamViewer was not found");
		}

		private void buttonItemSettingsJoinMe_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("http://saleswebcast.com/");
			}
			catch { }
		}

		private void buttonItemSettingsPowerPoint_Click(object sender, EventArgs e)
		{
			if (AppManager.Instance.ShowWarningQuestion("ARE YOU SURE YOU WANT TO STOP ALL POWERPOINT PROCESSES?\nSave any Active PowerPoint Presentations that may be running...") == DialogResult.Yes)
				AppManager.Instance.KillPowerPoint();
		}

		private void buttonItemSettingsExcel_Click(object sender, EventArgs e)
		{
			if (AppManager.Instance.ShowWarningQuestion("ARE YOU SURE YOU WANT TO STOP ALL EXCEL PROCESSES?\nSave any Active PowerPoint Presentations that may be running...") == DialogResult.Yes)
				AppManager.Instance.KillExcel();
		}

		private void buttonItemSettingsKillFMAutoSync_Click(object sender, EventArgs e)
		{
			if (AppManager.Instance.ShowWarningQuestion("Are you sure you want to terminate FM AutoSync process?") == DialogResult.Yes)
				AppManager.Instance.KillFMAutoSync();
		}

		private void buttonItemSettingsReset_Click(object sender, EventArgs e)
		{
			if (AppManager.Instance.ShowWarningQuestion("THIS ACTION WILL REMOVE ALL SETTINGS!\n\nARE YOU SURE YOU WANT TO DO THIS?") == DialogResult.Yes)
				if (AppManager.Instance.ShowWarningQuestion("OKAY!\n\nARE YOU ABSOLUTELY SURE YOU WANT TO RESET YOUR SOFTWARE!") == DialogResult.Yes)
				{
					AppManager.Instance.WipeSoftware();
				}
		}

		private void buttonItemSettingsDesktop_Click(object sender, EventArgs e)
		{
			using (var form = new FormShortcuts())
			{
				form.ShowDialog();
			}
		}

		private void buttonItemSettingsMinibar_Click(object sender, EventArgs e)
		{
			using (var form = new FormMinibarOptions())
			{
				form.ShowDialog();
			}
		}

		private void buttonItemSyncHourly_Click(object sender, EventArgs e)
		{
			buttonItemSyncHourlyOn.Checked = false;
			buttonItemSyncHourlyOff.Checked = false;
			(sender as ButtonItem).Checked = !(sender as ButtonItem).Checked;
		}

		private void buttonItemSyncHourly_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.SyncHourly = buttonItemSyncHourlyOn.Checked;
			SettingsManager.Instance.SaveMinibarSettings();
			SyncManager.SchedulerSyncInBackground();
		}

		private void buttonItemSettingsMonitor_Click(object sender, EventArgs e)
		{
			buttonItemSettingsMonitor1.Checked = false;
			buttonItemSettingsMonitor2.Checked = false;
			(sender as ButtonItem).Checked = !(sender as ButtonItem).Checked;
		}

		private void buttonItemSettingsMonitor_CheckedChanged(object sender, EventArgs e)
		{
			lock (AppManager.Locker)
			{
				SettingsManager.Instance.OnPrimaryScreen = buttonItemSettingsMonitor1.Checked;
				SettingsManager.Instance.SaveMinibarSettings();
			}
		}
		#endregion

		#region Sync Methods
		private void buttonItemSyncStart_Click(object sender, EventArgs e)
		{
			SyncManager.RegularSynchronize();
		}

		public void DisplayNextSync(object param)
		{
			var dt = (DateTime)param;
			labelItemNextSyncValue.Text = dt.ToString("MM/dd/yy h:mm tt");
			ribbonBarSyncStatus.RecalcLayout();
			ribbonPanelSync.PerformLayout();
		}

		public void DisplayLastSync(object param)
		{
			var dt = (DateTime)param;
			labelItemLastSyncValue.Text = dt.ToString("MM/dd/yy h:mm tt");
			ribbonBarSyncStatus.RecalcLayout();
			ribbonPanelSync.PerformLayout();
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

		#region Common Methods
		public void Init()
		{
			DisplayLastSync(SettingsManager.Instance.LastSync);

			InitTabPages();

			if (ribbonControl.SelectedRibbonTabItem != null)
				ribbonControl.SelectedRibbonTabItem.Checked = false;
			ribbonBarDashboard.Text = Core.Common.SettingsManager.Instance.DashboardName;
			ribbonBarDashboard.RecalcLayout();
			ribbonPanelDashboard.PerformLayout();

			LoadSalesDepot();

			SetPresentationSettings();

			comboBoxEditPowerPointStyle.EditValueChanging -= comboBoxEditPowerPointStyle_EditValueChanging;
			comboBoxEditPowerPointStyle.Properties.Items.Clear();
			foreach (string masterWizard in MasterWizardManager.Instance.MasterWizards.Keys)
				comboBoxEditPowerPointStyle.Properties.Items.Add(masterWizard);

			int selectedIndex = comboBoxEditPowerPointStyle.Properties.Items.IndexOf(Core.Common.SettingsManager.Instance.SelectedWizard);
			if (selectedIndex < 0)
				selectedIndex = 0;

			if (comboBoxEditPowerPointStyle.Properties.Items.Count > 0)
				comboBoxEditPowerPointStyle.SelectedIndex = selectedIndex;
			comboBoxEditPowerPointStyle.EditValueChanging += comboBoxEditPowerPointStyle_EditValueChanging;

			buttonItemSyncHourlyOn.Checked = SettingsManager.Instance.SyncHourly;
			buttonItemSyncHourlyOff.Checked = !SettingsManager.Instance.SyncHourly;

			buttonItemSettingsMonitor1.Checked = SettingsManager.Instance.OnPrimaryScreen;
			buttonItemSettingsMonitor2.Checked = !SettingsManager.Instance.OnPrimaryScreen;

			LoadNBWApplication();

			SetClipartSettings();

			ribbonBarSettingsMonitors.Visible = Screen.AllScreens.Length > 1;

			if (SettingsManager.Instance.AutoRunFloat)
				ShowFloater();
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
						ribbonTabItemPowerPoint.Tag = tabPageConfig.Id.ToString();
						tabPages.Add(ribbonTabItemPowerPoint);
						if (!String.IsNullOrEmpty(tabPageConfig.RibbonGroup1Name))
							ribbonBarPowerPointPresentationSettings.Text = tabPageConfig.RibbonGroup1Name;
						break;
					case TabNamesEnum.Dashboard:
						ribbonTabItemDashboard.Text = tabPageConfig.Name;
						ribbonTabItemDashboard.Enabled = tabPageConfig.Enabled;
						ribbonTabItemDashboard.Tag = tabPageConfig.Id.ToString();
						tabPages.Add(ribbonTabItemDashboard);
						if (!String.IsNullOrEmpty(tabPageConfig.RibbonGroup1Name))
							ribbonBarDashboard.Text = tabPageConfig.RibbonGroup1Name;
						break;
					case TabNamesEnum.SalesDepot:
						ribbonTabItemSalesDepot.Text = tabPageConfig.Name;
						ribbonTabItemSalesDepot.Enabled = tabPageConfig.Enabled;
						ribbonTabItemSalesDepot.Tag = tabPageConfig.Id.ToString();
						tabPages.Add(ribbonTabItemSalesDepot);
						if (!String.IsNullOrEmpty(tabPageConfig.RibbonGroup1Name))
							ribbonBarSalesDepot.Text = tabPageConfig.RibbonGroup1Name;
						break;
					case TabNamesEnum.Apps1:
						ribbonTabItemApps1.Text = tabPageConfig.Name;
						ribbonTabItemApps1.Enabled = tabPageConfig.Enabled;
						ribbonTabItemApps1.Tag = tabPageConfig.Id.ToString();
						tabPages.Add(ribbonTabItemApps1);
						if (!String.IsNullOrEmpty(tabPageConfig.RibbonGroup1Name))
							ribbonBarApps1.Text = tabPageConfig.RibbonGroup1Name;
						break;
					case TabNamesEnum.Apps2:
						ribbonTabItemApps2.Text = tabPageConfig.Name;
						ribbonTabItemApps2.Enabled = tabPageConfig.Enabled;
						ribbonTabItemApps2.Tag = tabPageConfig.Id.ToString();
						tabPages.Add(ribbonTabItemApps2);
						if (!String.IsNullOrEmpty(tabPageConfig.RibbonGroup1Name))
							ribbonBarApps2.Text = tabPageConfig.RibbonGroup1Name;
						break;
					case TabNamesEnum.Apps3:
						ribbonTabItemApps3.Text = tabPageConfig.Name;
						ribbonTabItemApps3.Enabled = tabPageConfig.Enabled;
						ribbonTabItemApps3.Tag = tabPageConfig.Id.ToString();
						tabPages.Add(ribbonTabItemApps3);
						if (!String.IsNullOrEmpty(tabPageConfig.RibbonGroup1Name))
							ribbonBarApps3.Text = tabPageConfig.RibbonGroup1Name;
						break;
					case TabNamesEnum.Apps4:
						ribbonTabItemApps4.Text = tabPageConfig.Name;
						ribbonTabItemApps4.Enabled = tabPageConfig.Enabled;
						ribbonTabItemApps4.Tag = tabPageConfig.Id.ToString();
						tabPages.Add(ribbonTabItemApps4);
						if (!String.IsNullOrEmpty(tabPageConfig.RibbonGroup1Name))
							ribbonBarApps4.Text = tabPageConfig.RibbonGroup1Name;
						break;
					case TabNamesEnum.Apps5:
						ribbonTabItemApps5.Text = tabPageConfig.Name;
						ribbonTabItemApps5.Enabled = tabPageConfig.Enabled;
						ribbonTabItemApps5.Tag = tabPageConfig.Id.ToString();
						tabPages.Add(ribbonTabItemApps5);
						if (!String.IsNullOrEmpty(tabPageConfig.RibbonGroup1Name))
							ribbonBarApps5.Text = tabPageConfig.RibbonGroup1Name;
						break;
					case TabNamesEnum.Settings:
						ribbonTabItemSettings.Text = tabPageConfig.Name;
						ribbonTabItemSettings.Enabled = tabPageConfig.Enabled;
						ribbonTabItemSettings.Tag = tabPageConfig.Id.ToString();
						tabPages.Add(ribbonTabItemSettings);
						if (!String.IsNullOrEmpty(tabPageConfig.RibbonGroup1Name))
							ribbonBarSettingsWebcast.Text = tabPageConfig.RibbonGroup1Name;
						break;
					case TabNamesEnum.Sync:
						ribbonTabItemSync.Text = tabPageConfig.Name;
						ribbonTabItemSync.Enabled = tabPageConfig.Enabled;
						ribbonTabItemSync.Tag = tabPageConfig.Id.ToString();
						tabPages.Add(ribbonTabItemSync);
						if (!String.IsNullOrEmpty(tabPageConfig.RibbonGroup1Name))
							ribbonBarSyncStatus.Text = tabPageConfig.RibbonGroup1Name;
						break;
				}
			}
			ribbonControl.Items.AddRange(tabPages.ToArray());
		}

		private void ShowFloater()
		{
			var form = new FormFloater();
			form.StartPosition = FormStartPosition.Manual;
			int defaultTop = Screen.PrimaryScreen.Bounds.Height - form.Height - 50;
			int defaultLeft = Screen.PrimaryScreen.Bounds.Width - form.Width - 50;
			form.Location = new Point(SettingsManager.Instance.FloaterLeft == 0 || SettingsManager.Instance.FloaterLeft > defaultLeft ? defaultLeft : SettingsManager.Instance.FloaterLeft, SettingsManager.Instance.FloaterTop == 0 || SettingsManager.Instance.FloaterTop > defaultTop ? defaultTop : SettingsManager.Instance.FloaterTop);
			form.Show();
		}

		private void ActivateRetractedForm()
		{
			_formMouseLeaveTimer.Stop();
			FadeOut();
			FormMain.Instance.FadeIn();
		}

		public void FadeIn()
		{
			if (!_expanded)
			{
				var timer = new Timer();
				timer.Interval = 30;
				timer.Tick += FadeInTimer_Tick;
				timer.Start();
			}
		}

		public void FadeOut()
		{
			if (_expanded)
			{
				var timer = new Timer();
				timer.Interval = 30;
				timer.Tick += FadeOutTimer_Tick;
				timer.Start();
			}
		}
		#endregion

		public static FormMainExpanded Instance
		{
			get
			{
				if (_instance == null)
					_instance = new FormMainExpanded();
				return _instance;
			}
		}
	}
}