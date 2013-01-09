using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Microsoft.Office.Interop.PowerPoint;
using MiniBar.BusinessClasses;
using MiniBar.ConfigurationClasses;
using MiniBar.InteropClasses;
using MiniBar.Properties;
using MiniBar.SettingsForms;
using MiniBar.ToolForms;
using Application = System.Windows.Forms.Application;
using Font = System.Drawing.Font;
using Point = System.Drawing.Point;

namespace MiniBar
{
	public partial class FormMainExpanded : Form
	{
		private static object _locker = new object();
		private static FormMainExpanded _instance;
		private readonly Timer _formMouseLeaveTimer;
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

			ApplyPowerPointLogo();

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
				ribbonBarApps.RecalcLayout();
				ribbonBarAppsExit.RecalcLayout();
				ribbonBarAppsHelp.RecalcLayout();
				ribbonBarClipartClientLogos.RecalcLayout();
				ribbonBarClipartExit.RecalcLayout();
				ribbonBarClipartHelp.RecalcLayout();
				ribbonBarClipartSalesGallery.RecalcLayout();
				ribbonBarClipartWebArt.RecalcLayout();
				ribbonBarDashboard.RecalcLayout();
				ribbonBarDashboardExit.RecalcLayout();
				ribbonBarDashboardHelp.RecalcLayout();
				ribbonBarDashboardStarterSlides.RecalcLayout();
				ribbonBarToolsExit.RecalcLayout();
				ribbonBarToolsHelp.RecalcLayout();
				ribbonBarToolsSave.RecalcLayout();
				ribbonBarPdfEmailPdf.RecalcLayout();
				ribbonBarPdfEmailPpt.RecalcLayout();
				ribbonBarPdfExit.RecalcLayout();
				ribbonBarPdfHelp.RecalcLayout();
				ribbonBarPdfSavePdf.RecalcLayout();
				ribbonBarPowerPointExit.RecalcLayout();
				ribbonBarPowerPointHelp.RecalcLayout();
				ribbonBarPowerPointLaunch.RecalcLayout();
				ribbonBarPowerPointPresentationSettings.RecalcLayout();
				ribbonBarPowerPointSlideTemplate.RecalcLayout();
				ribbonBarSalesDepot.RecalcLayout();
				ribbonBarSalesDepotRemote.RecalcLayout();
				ribbonBarSalesDepotHelp.RecalcLayout();
				ribbonBarSetingsHelp.RecalcLayout();
				ribbonBarSettingsExit.RecalcLayout();
				ribbonBarSettingsMinibar.RecalcLayout();
				ribbonBarSettingsStartup.RecalcLayout();
				ribbonBarSlidesExit.RecalcLayout();
				ribbonBarSyncExit.RecalcLayout();
				ribbonBarSyncHelp.RecalcLayout();
				ribbonBarSyncHourly.RecalcLayout();
				ribbonBarSyncStart.RecalcLayout();
				ribbonBarSyncStatus.RecalcLayout();
				ribbonBarTrainingExit.RecalcLayout();
				ribbonBarTrainingHelp.RecalcLayout();
				ribbonPanelApps.PerformLayout();
				ribbonPanelClipart.PerformLayout();
				ribbonPanelDashboard.PerformLayout();
				ribbonPanelTools.PerformLayout();
				ribbonPanelPDF.PerformLayout();
				ribbonPanelPowerPoint.PerformLayout();
				ribbonPanelSalesDepot.PerformLayout();
				ribbonPanelSettings.PerformLayout();
				ribbonPanelSync.PerformLayout();
				ribbonPanelIPad.PerformLayout();
			}

			#region Init Activity Recording Events
			buttonItemAppsHelp.Click += ribbonTabItem_Click;
			buttonItemClipartClientLogos.Click += ribbonTabItem_Click;
			buttonItemClipartHelp.Click += ribbonTabItem_Click;
			buttonItemClipartSalesGallery.Click += ribbonTabItem_Click;
			buttonItemClipartWebArt.Click += ribbonTabItem_Click;
			buttonItemDashboard.Click += ribbonTabItem_Click;
			buttonItemDashboardCleanslate.Click += ribbonTabItem_Click;
			buttonItemDashboardCover.Click += ribbonTabItem_Click;
			buttonItemDashboardHelp.Click += ribbonTabItem_Click;
			buttonItemPdfEmailPdf.Click += ribbonTabItem_Click;
			buttonItemPdfEmailPpt.Click += ribbonTabItem_Click;
			buttonItemPdfHelp.Click += ribbonTabItem_Click;
			buttonItemPdfSavePdf.Click += ribbonTabItem_Click;
			buttonItemPowerPointHelp.Click += ribbonTabItem_Click;
			buttonItemPowerPointLaunch.Click += ribbonTabItem_Click;
			buttonItemPowerPointSize1.Click += ribbonTabItem_Click;
			buttonItemPowerPointSize2.Click += ribbonTabItem_Click;
			buttonItemPowerPointSize3.Click += ribbonTabItem_Click;
			buttonItemPowerPointSize4.Click += ribbonTabItem_Click;
			buttonItemPowerPointSize5.Click += ribbonTabItem_Click;
			buttonItemPowerPointTemplateDisabled.Click += ribbonTabItem_Click;
			buttonItemPowerPointTemplateEnabled.Click += ribbonTabItem_Click;
			buttonItemSalesDepotHelp.Click += ribbonTabItem_Click;
			buttonItemSettingsDesktop.Click += ribbonTabItem_Click;
			buttonItemSettingsHelp.Click += ribbonTabItem_Click;
			buttonItemSettingsKillExcel.Click += ribbonTabItem_Click;
			buttonItemSettingsKilPowerPoint.Click += ribbonTabItem_Click;
			buttonItemSettingsMinibar.Click += ribbonTabItem_Click;
			buttonItemSettingsMonitor1.Click += ribbonTabItem_Click;
			buttonItemSettingsMonitor2.Click += ribbonTabItem_Click;
			buttonItemSettingsReset.Click += ribbonTabItem_Click;
			buttonItemSyncHelp.Click += ribbonTabItem_Click;
			buttonItemSyncHourlyOff.Click += ribbonTabItem_Click;
			buttonItemSyncHourlyOn.Click += ribbonTabItem_Click;
			buttonItemSyncStart.Click += ribbonTabItem_Click;
			buttonItemToolsContent.Click += ribbonTabItem_Click;
			buttonItemToolsHelp.Click += ribbonTabItem_Click;
			buttonItemToolsPageNumbers.Click += ribbonTabItem_Click;
			buttonItemToolsSave.Click += ribbonTabItem_Click;
			buttonItemToolsSlideHeader.Click += ribbonTabItem_Click;
			buttonItemTrainingHelp.Click += ribbonTabItem_Click;
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
			Point pos = MousePosition;
			bool inForm = pos.X >= Left && pos.Y >= Top && pos.X < Right && pos.Y < Bottom;
			if (!inForm && _expanded && !_comboOpened)
				ActivateRetractedForm();
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
			HelpManager.Instance.OpenHelpLink(ribbonControl.Items.IndexOf(ribbonControl.SelectedRibbonTabItem) + 1);
		}
		#endregion

		#region PowerPoint Methods
		private void ApplyPowerPointLogo()
		{
			buttonItemPowerPointLaunch.Image = Resources.PowerPointLaunch2010;
			string pptExecutableLocation = string.Format(@"{0}\Microsoft Office\OFFICE14\POWERPNT.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			if (File.Exists(pptExecutableLocation))
				buttonItemPowerPointLaunch.Image = Resources.PowerPointLaunch2010;
			else
			{
				pptExecutableLocation = string.Format(@"c:\Program Files\Microsoft Office\OFFICE14\POWERPNT.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
				if (File.Exists(pptExecutableLocation))
					buttonItemPowerPointLaunch.Image = Resources.PowerPointLaunch2010;
				else
				{
					pptExecutableLocation = string.Format(@"{0}\Microsoft Office\OFFICE12\POWERPNT.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
					if (File.Exists(pptExecutableLocation))
						buttonItemPowerPointLaunch.Image = Resources.PowerPointLaunch2007;
					else
					{
						pptExecutableLocation = string.Format(@"c:\Program Files\Microsoft Office\OFFICE12\POWERPNT.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
						if (File.Exists(pptExecutableLocation))
							buttonItemPowerPointLaunch.Image = Resources.PowerPointLaunch2007;
						else
						{
							pptExecutableLocation = string.Format(@"{0}\Microsoft Office\OFFICE11\POWERPNT.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
							if (File.Exists(pptExecutableLocation))
								buttonItemPowerPointLaunch.Image = Resources.PowerPointLaunch2003;
						}
					}
				}
			}
			ribbonBarPowerPointLaunch.RecalcLayout();
			ribbonPanelPowerPoint.PerformLayout();
		}


		private void RunPowerPoint()
		{
			AppManager.Instance.RunPowerPointLoader();
		}

		private void SetPresentationSettings()
		{
			if (SettingsManager.Instance.Orientation.Equals("Landscape"))
			{
				if (SettingsManager.Instance.SizeWidth == 10 && SettingsManager.Instance.SizeHeght == 7.5)
				{
					buttonItemPowerPointSize1.Checked = true;
					buttonItemPowerPointSize2.Checked = false;
					buttonItemPowerPointSize3.Checked = false;
					buttonItemPowerPointSize4.Checked = false;
					buttonItemPowerPointSize5.Checked = false;
				}
				else if (SettingsManager.Instance.SizeWidth == 10.75 && SettingsManager.Instance.SizeHeght == 8.25)
				{
					buttonItemPowerPointSize1.Checked = false;
					buttonItemPowerPointSize2.Checked = true;
					buttonItemPowerPointSize3.Checked = false;
					buttonItemPowerPointSize4.Checked = false;
					buttonItemPowerPointSize5.Checked = false;
				}
				else if (SettingsManager.Instance.SizeWidth == 10 && SettingsManager.Instance.SizeHeght == 5.63)
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
				if (SettingsManager.Instance.SizeWidth == 10 && SettingsManager.Instance.SizeHeght == 7.5)
				{
					buttonItemPowerPointSize1.Checked = false;
					buttonItemPowerPointSize2.Checked = false;
					buttonItemPowerPointSize3.Checked = false;
					buttonItemPowerPointSize4.Checked = true;
					buttonItemPowerPointSize5.Checked = false;
				}
				else if (SettingsManager.Instance.SizeWidth == 10.75 && SettingsManager.Instance.SizeHeght == 8.25)
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
			if (MasterWizardManager.Instance.SelectedWizard != null)
			{
				SettingsManager.Instance.SelectedWizard = masterWizard.Name;

				UpdateSlideSize();

				UpdateSlideTemplateStatus();

				UpdateApplicationsStatus();

				SettingsManager.Instance.SaveSharedSettings();
			}
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

		private void UpdateSlideTemplateStatus()
		{
			if (buttonItemPowerPointSize1.Checked)
			{
				if (!MasterWizardManager.Instance.SelectedWizard.HasSlideTemplate43)
				{
					_allowToSave = false;
					buttonItemPowerPointTemplateDisabled.Checked = true;
					_allowToSave = true;
					ribbonBarPowerPointSlideTemplate.Enabled = false;
					ribbonBarPowerPointSlideTemplate.Enabled = false;
				}
				else
				{
					_allowToSave = false;
					buttonItemPowerPointTemplateDisabled.Checked = !SettingsManager.Instance.SlideTemplateEnabled;
					_allowToSave = true;
					buttonItemPowerPointTemplateEnabled.Checked = SettingsManager.Instance.SlideTemplateEnabled;
					ribbonBarPowerPointSlideTemplate.Enabled = true;
				}
			}
			else if (buttonItemPowerPointSize2.Checked)
			{
				if (!MasterWizardManager.Instance.SelectedWizard.HasSlideTemplate54)
				{
					_allowToSave = false;
					buttonItemPowerPointTemplateDisabled.Checked = true;
					_allowToSave = true;
					ribbonBarPowerPointSlideTemplate.Enabled = false;
					ribbonBarPowerPointSlideTemplate.Enabled = false;
				}
				else
				{
					_allowToSave = false;
					buttonItemPowerPointTemplateDisabled.Checked = !SettingsManager.Instance.SlideTemplateEnabled;
					_allowToSave = true;
					buttonItemPowerPointTemplateEnabled.Checked = SettingsManager.Instance.SlideTemplateEnabled;
					ribbonBarPowerPointSlideTemplate.Enabled = true;
				}
			}
			else if (buttonItemPowerPointSize3.Checked)
			{
				if (!MasterWizardManager.Instance.SelectedWizard.HasSlideTemplate169)
				{
					_allowToSave = false;
					buttonItemPowerPointTemplateDisabled.Checked = true;
					_allowToSave = true;
					ribbonBarPowerPointSlideTemplate.Enabled = false;
					ribbonBarPowerPointSlideTemplate.Enabled = false;
				}
				else
				{
					_allowToSave = false;
					buttonItemPowerPointTemplateDisabled.Checked = !SettingsManager.Instance.SlideTemplateEnabled;
					_allowToSave = true;
					buttonItemPowerPointTemplateEnabled.Checked = SettingsManager.Instance.SlideTemplateEnabled;
					ribbonBarPowerPointSlideTemplate.Enabled = true;
				}
			}
			else if (buttonItemPowerPointSize4.Checked)
			{
				if (!MasterWizardManager.Instance.SelectedWizard.HasSlideTemplate34)
				{
					_allowToSave = false;
					buttonItemPowerPointTemplateDisabled.Checked = true;
					_allowToSave = true;
					ribbonBarPowerPointSlideTemplate.Enabled = false;
					ribbonBarPowerPointSlideTemplate.Enabled = false;
				}
				else
				{
					_allowToSave = false;
					buttonItemPowerPointTemplateDisabled.Checked = !SettingsManager.Instance.SlideTemplateEnabled;
					_allowToSave = true;
					buttonItemPowerPointTemplateEnabled.Checked = SettingsManager.Instance.SlideTemplateEnabled;
					ribbonBarPowerPointSlideTemplate.Enabled = true;
				}
			}
			else if (buttonItemPowerPointSize5.Checked)
			{
				if (!MasterWizardManager.Instance.SelectedWizard.HasSlideTemplate45)
				{
					_allowToSave = false;
					buttonItemPowerPointTemplateDisabled.Checked = true;
					_allowToSave = true;
					ribbonBarPowerPointSlideTemplate.Enabled = false;
					ribbonBarPowerPointSlideTemplate.Enabled = false;
				}
				else
				{
					_allowToSave = false;
					buttonItemPowerPointTemplateDisabled.Checked = !SettingsManager.Instance.SlideTemplateEnabled;
					_allowToSave = true;
					buttonItemPowerPointTemplateEnabled.Checked = SettingsManager.Instance.SlideTemplateEnabled;
					ribbonBarPowerPointSlideTemplate.Enabled = true;
				}
			}
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
			if (PowerPointHelper.Instance.PowerPointDetected())
			{
				using (var form = new FormFormatChangeNotification())
				{
					var buttonPressed = (sender as ButtonItem);
					string currentFormatText = SettingsManager.Instance.SlideSize;
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
			UpdateSlideTemplateStatus();
			SettingsManager.Instance.SaveSharedSettings();
			PowerPointHelper.Instance.Connect();
			PowerPointHelper.Instance.SetPresentationSettings();
		}

		private void buttonItemSize_CheckedChanged(object sender, EventArgs e)
		{
			if ((sender as ButtonItem).Checked)
			{
				if (buttonItemPowerPointSize1.Checked)
				{
					SettingsManager.Instance.SizeWidth = 10;
					SettingsManager.Instance.SizeHeght = 7.5;
					SettingsManager.Instance.Orientation = "Landscape";
				}
				else if (buttonItemPowerPointSize2.Checked)
				{
					SettingsManager.Instance.SizeWidth = 10.75;
					SettingsManager.Instance.SizeHeght = 8.25;
					SettingsManager.Instance.Orientation = "Landscape";
				}
				else if (buttonItemPowerPointSize3.Checked)
				{
					SettingsManager.Instance.SizeWidth = 10;
					SettingsManager.Instance.SizeHeght = 5.63;
					SettingsManager.Instance.Orientation = "Landscape";
				}
				else if (buttonItemPowerPointSize4.Checked)
				{
					SettingsManager.Instance.SizeWidth = 10;
					SettingsManager.Instance.SizeHeght = 7.5;
					SettingsManager.Instance.Orientation = "Portrait";
				}
				else if (buttonItemPowerPointSize5.Checked)
				{
					SettingsManager.Instance.SizeWidth = 10.75;
					SettingsManager.Instance.SizeHeght = 8.25;
					SettingsManager.Instance.Orientation = "Portrait";
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
			if (PowerPointHelper.Instance.PowerPointDetected())
			{
				using (var form = new FormFormatChangeNotification())
				{
					string currentFormatText = SettingsManager.Instance.SelectedWizard;
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

		private void buttonItemPowerPointTemplate_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				SettingsManager.Instance.SlideTemplateEnabled = buttonItemPowerPointTemplateEnabled.Checked;
				SettingsManager.Instance.SaveSharedSettings();
			}
		}

		private void buttonItemPowerPointTemplate_Click(object sender, EventArgs e)
		{
			if (AppManager.Instance.AplicationDetected())
			{
				if (AppManager.Instance.ShowWarningQuestion("All active applications will be closed before you change PowerPoint settings.\nDo you want to continue?") == DialogResult.Yes)
					AppManager.Instance.CloseActiveApplications();
				else
					return;
			}
			_allowToSave = false;
			_allowToSave = true;
			buttonItemPowerPointTemplateDisabled.Checked = false;
			buttonItemPowerPointTemplateEnabled.Checked = false;
			(sender as ButtonItem).Checked = true;
		}

		private void buttonItemPowerPointLaunch_Click(object sender, EventArgs e)
		{
			RunPowerPoint();
			AppManager.Instance.ActivateForm(Handle, false);
		}
		#endregion

		#region Dashboard Methods
		private void buttonItemDashboard_Click(object sender, EventArgs e)
		{
			AppManager.Instance.RunDashboard();
		}

		private void buttonItemDashboardCover_Click(object sender, EventArgs e)
		{
			using (var form = new FormAddCover())
			{
				DialogResult result = form.ShowDialog();
				if (result == DialogResult.Yes)
					AppManager.Instance.RunDashboard("showcover");
				else if (result == DialogResult.No)
				{
					if (!PowerPointHelper.Instance.PowerPointDetected())
					{
						if (AppManager.Instance.ShowWarningQuestion("You need to first open PowerPoint\nDo you want to do that now?") == DialogResult.Yes)
							RunPowerPoint();
						else
							return;
					}
					PowerPointHelper.Instance.Connect();
					PowerPointHelper.Instance.AppendGenericCover(true);
					AppManager.Instance.ActivateForm(Handle, false);
				}
			}
		}

		private void buttonItemDashboardCleanslate_Click(object sender, EventArgs e)
		{
			if (!PowerPointHelper.Instance.PowerPointDetected())
			{
				if (AppManager.Instance.ShowWarningQuestion("You need to first open PowerPoint\nDo you want to do that now?") == DialogResult.Yes)
					RunPowerPoint();
				else
					return;
			}
			PowerPointHelper.Instance.Connect();
			PowerPointHelper.Instance.AppendCleanslate();
			AppManager.Instance.ActivateForm(Handle, false);
		}
		#endregion

		#region SalesDepot
		private void buttonItemSalesDepot_Click(object sender, EventArgs e)
		{
			AppManager.Instance.RunLocalSalesDepot();
		}

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
				label.Click += (sender, args) => { AppManager.Instance.RunLocalSalesDepot(); ServiceDataManager.Instance.WriteActivity(); };
				var button = new ButtonItem();
				if (File.Exists(SettingsManager.Instance.SalesDepotSettings.LocalLogoPath) && File.Exists(SettingsManager.Instance.SalesDepotSettings.LocalIconPath))
					button.Image = new Bitmap(SettingsManager.Instance.SalesDepotSettings.LocalLogoPath);
				button.Click += (sender, args) => { AppManager.Instance.RunLocalSalesDepot(); ServiceDataManager.Instance.WriteActivity(); };
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
				label.Click += (sender, args) => { AppManager.Instance.RunWebSalesDepot(); ServiceDataManager.Instance.WriteActivity(); };
				button = new ButtonItem();
				if (File.Exists(SettingsManager.Instance.SalesDepotSettings.WebLogoPath) && File.Exists(SettingsManager.Instance.SalesDepotSettings.WebIconPath))
					button.Image = new Bitmap(SettingsManager.Instance.SalesDepotSettings.WebLogoPath);
				button.Click += (sender, args) => { AppManager.Instance.RunWebSalesDepot(); ServiceDataManager.Instance.WriteActivity(); };
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

			ribbonPanelSalesDepot.PerformLayout();
		}
		#endregion

		#region Apps Methods
		private void LoadNBWApplication()
		{
			if (NBWApplicationsManager.Instance.NBWApplications.Count > 0)
			{
				foreach (NBWApplication nbwApplication in NBWApplicationsManager.Instance.NBWApplications)
				{
					AddAppDefinition(nbwApplication);
				}
				ribbonBarApps.RecalcLayout();
				ribbonPanelApps.PerformLayout();
			}
		}

		private void UpdateApplicationsStatus()
		{
			foreach (NBWApplication nbwApplication in NBWApplicationsManager.Instance.NBWApplications)
			{
				if (nbwApplication.UseSlideTemplates && MasterWizardManager.Instance.SelectedWizard != null)
				{
					string slideTemplatesFolderPath = string.Empty;
					if (nbwApplication.UseWizard)
						slideTemplatesFolderPath = Path.Combine(MasterWizardManager.Instance.SelectedWizard.Folder.FullName, SettingsManager.Instance.SlideFolder, nbwApplication.SlideTemplatesPath);
					else
						slideTemplatesFolderPath = Path.Combine(MasterWizardManager.ScheduleBuildersFolder, SettingsManager.Instance.SlideFolder, nbwApplication.SlideTemplatesPath);
					if (Directory.Exists(slideTemplatesFolderPath))
					{
						nbwApplication.AppLabel.Enabled = true;
						nbwApplication.AppButton.Enabled = true;
					}
					else
					{
						nbwApplication.AppLabel.Enabled = false;
						nbwApplication.AppButton.Enabled = false;
					}
				}
			}
		}

		private void AddAppDefinition(NBWApplication nbwApplication)
		{
			var itemContainerApp = new ItemContainer();
			itemContainerApp.SubItems.AddRange(new BaseItem[]
			                                   {
				                                   nbwApplication.AppButton,
				                                   nbwApplication.DisabledButton,
				                                   nbwApplication.AppLabel
			                                   });
			galleryContainerApps.SubItems.AddRange(new BaseItem[] { itemContainerApp });
			superTooltip.SetSuperTooltip(nbwApplication.DisabledButton, new SuperTooltipInfo("This app is DISABLED", string.Empty, "Check your PowerPoint slide size on the first Tab of this minibar", null, null, eTooltipColor.Default, true, false, new Size(0, 0)));
		}

		private void buttonItemAppsTeamViewer_Click(object sender, EventArgs e)
		{
			if (File.Exists(SettingsManager.Instance.TeamViewerQSPath))
			{
				Process.Start(SettingsManager.Instance.TeamViewerQSPath);
				return;
			}
			AppManager.Instance.ShowWarning("TeamViewer was not found");
		}

		private void buttonItemAppsWebcast_Click(object sender, EventArgs e)
		{
			if (File.Exists(SettingsManager.Instance.TeamViewerQJPath))
			{
				Process.Start(SettingsManager.Instance.TeamViewerQJPath);
				return;
			}
			AppManager.Instance.ShowWarning("TeamViewer was not found");
		}

		private void buttonItemAppsJoinMe_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("http://saleswebcast.com/");
			}
			catch { }
		}

		private void buttonItemAppsWizSite_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("http://wiz10.com/");
			}
			catch { }
		}

		private void buttonItemAppsLiveMeeting_Click(object sender, EventArgs e)
		{
			if (File.Exists(@"C:\Program Files\Microsoft Office\Live Meeting 8\Console\PWConsole.exe"))
			{
				Process.Start(@"C:\Program Files\Microsoft Office\Live Meeting 8\Console\PWConsole.exe");
				return;
			}
			if (File.Exists(@"C:\Program Files (x86)\Microsoft Office\Live Meeting 8\Console\PWConsole.exe"))
			{
				Process.Start(@"C:\Program Files (x86)\Microsoft Office\Live Meeting 8\Console\PWConsole.exe");
				return;
			}
			AppManager.Instance.ShowWarning("LiveMeeting was not found");
		}

		private void buttonItemAppsLiveMeetingLocation_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(SettingsManager.Instance.Location);
			AppManager.Instance.ShowInformation("Location was copied to clipboard successfully");
		}
		#endregion

		#region Clipart Methods
		private void buttonItemClipartClientLogos_Click(object sender, EventArgs e)
		{
			AppManager.Instance.RunClientLogos();
		}

		private void buttonItemClipartSalesGallery_Click(object sender, EventArgs e)
		{
			AppManager.Instance.RunSalesGallery();
		}

		private void buttonItemClipartWebArt_Click(object sender, EventArgs e)
		{
			AppManager.Instance.RunWebArt();
		}
		#endregion

		#region PDF Methods
		private void buttonItemPdfSavePdf_Click(object sender, EventArgs e)
		{
			bool toContinue = false;
			string presentationName = string.Empty;
			if (PowerPointHelper.Instance.PowerPointDetected())
			{
				PowerPointHelper.Instance.Connect();
				if (PowerPointHelper.Instance.Is2003)
				{
					AppManager.Instance.ShowWarning("Your Version of PowerPoint will not Convert to PDF.\nPDF Converting  Only works with Office 2007 and 2010.");
					return;
				}
				if (PowerPointHelper.Instance.IsActive && PowerPointHelper.Instance.PowerPointObject.WindowState != PpWindowState.ppWindowMinimized)
				{
					presentationName = PowerPointHelper.Instance.ActiveFileName;
					if (!string.IsNullOrEmpty(presentationName))
						toContinue = true;
				}
			}

			if (toContinue)
			{
				if (AppManager.Instance.ShowWarningQuestion("Do you want to save " + presentationName + " as a PDF?") == DialogResult.Yes)
				{
					using (var dialog = new SaveFileDialog())
					{
						dialog.FileName = presentationName.Replace(".pptx", "").Replace(".ppt", "") + ".pdf";
						dialog.Title = "Save Presentation As PDF";
						dialog.Filter = "Adobe PDF Files|*.pdf";
						dialog.DefaultExt = "*.pdf";
						if (dialog.ShowDialog() == DialogResult.OK)
						{
							PowerPointHelper.Instance.SavePDF(dialog.FileName);
							if (File.Exists(dialog.FileName))
								Process.Start(dialog.FileName);
						}
					}
				}
			}
			else
				AppManager.Instance.ShowWarning("There is no active PowerPoint Presentation.\nOpen your presentation and try again.");
		}

		private void buttonItemPdfEmailPdf_Click(object sender, EventArgs e)
		{
			bool toContinue = false;
			string presentationName = string.Empty;
			if (PowerPointHelper.Instance.PowerPointDetected())
			{
				PowerPointHelper.Instance.Connect();
				if (PowerPointHelper.Instance.Is2003)
				{
					AppManager.Instance.ShowWarning("Your Version of PowerPoint will not Convert to PDF.\nPDF Converting  Only works with Office 2007 and 2010.");
					return;
				}
				if (PowerPointHelper.Instance.IsActive && PowerPointHelper.Instance.PowerPointObject.WindowState != PpWindowState.ppWindowMinimized)
				{
					presentationName = PowerPointHelper.Instance.ActiveFileName;
					if (!string.IsNullOrEmpty(presentationName))
						toContinue = true;
				}
			}

			if (toContinue)
			{
				if (AppManager.Instance.ShowWarningQuestion("Do you want to Email a PDF Version of " + presentationName + "?") == DialogResult.Yes)
				{
					string fileName = Path.Combine(Path.GetTempPath(), presentationName.Replace(".pptx", "").Replace(".ppt", "") + ".pdf");
					PowerPointHelper.Instance.SavePDF(fileName);
					if (OutlookHelper.Instance.Open())
					{
						OutlookHelper.Instance.CreateMessage(fileName);
						OutlookHelper.Instance.Close();
					}
					else
						AppManager.Instance.ShowWarning("Couldn't open Outlook");
				}
			}
			else
				AppManager.Instance.ShowWarning("There is no active PowerPoint Presentation.\nOpen your presentation and try again.");
		}

		private void buttonItemPdfEmailPpt_Click(object sender, EventArgs e)
		{
			bool toContinue = false;
			string presentationName = string.Empty;
			if (PowerPointHelper.Instance.PowerPointDetected())
			{
				PowerPointHelper.Instance.Connect();
				if (PowerPointHelper.Instance.IsActive && PowerPointHelper.Instance.PowerPointObject.WindowState != PpWindowState.ppWindowMinimized)
				{
					presentationName = PowerPointHelper.Instance.ActiveFileName;
					if (!string.IsNullOrEmpty(presentationName))
						toContinue = true;
				}
			}

			if (toContinue)
			{
				if (AppManager.Instance.ShowWarningQuestion("Do you want to Email " + presentationName + "?") == DialogResult.Yes)
				{
					string fileName = Path.Combine(Path.GetTempPath(), presentationName.Replace(".pptx", "").Replace(".ppt", "") + ".pdf");
					if (OutlookHelper.Instance.Open())
					{
						OutlookHelper.Instance.CreateMessage(PowerPointHelper.Instance.ActiveFilePath);
						OutlookHelper.Instance.Close();
					}
					else
						AppManager.Instance.ShowWarning("Couldn't open Outlook");
				}
			}
			else
				AppManager.Instance.ShowWarning("There is no active PowerPoint Presentation.\nOpen your presentation and try again.");
		}
		#endregion

		#region Tools Methods
		private void buttonItemToolsContent_Click(object sender, EventArgs e)
		{
			if (!PowerPointHelper.Instance.PowerPointDetected())
			{
				AppManager.Instance.ShowWarning("You have no Active PowerPoint Presentation.");
				return;
			}
			PowerPointHelper.Instance.Connect();
			AppManager.Instance.ActivateForm(Handle, false);
			using (var form = new FormSlideContentTools())
			{
				form.ShowDialog();
			}
			AppManager.Instance.ActivateForm(Handle, false);
		}

		private void buttonItemToolsPageNumbers_Click(object sender, EventArgs e)
		{
			if (!PowerPointHelper.Instance.PowerPointDetected())
			{
				AppManager.Instance.ShowWarning("You have no Active PowerPoint Presentation.");
				return;
			}
			PowerPointHelper.Instance.Connect();
			AppManager.Instance.ActivateForm(Handle, false);
			using (var form = new FormPageNumbersTools())
			{
				form.ShowDialog();
			}
			AppManager.Instance.ActivateForm(Handle, false);
		}

		private void buttonItemToolsSlideHeader_Click(object sender, EventArgs e)
		{
			if (!PowerPointHelper.Instance.PowerPointDetected())
			{
				AppManager.Instance.ShowWarning("You have no Active PowerPoint Presentation.");
				return;
			}
			PowerPointHelper.Instance.Connect();
			AppManager.Instance.ActivateForm(Handle, false);
			using (var form = new FormSlideHeadersTools())
			{
				form.ShowDialog();
			}
			AppManager.Instance.ActivateForm(Handle, false);
		}

		private void buttonItemToolsSave_Click(object sender, EventArgs e)
		{
			if (!PowerPointHelper.Instance.PowerPointDetected())
			{
				AppManager.Instance.ShowWarning("You have no Active PowerPoint Presentation.");
				return;
			}
			PowerPointHelper.Instance.Connect();
			AppManager.Instance.ActivateForm(Handle, false);
			PowerPointHelper.Instance.AddContents(true);
			PowerPointHelper.Instance.AddPageNumbers();
			AppManager.Instance.ActivateForm(Handle, false);
		}
		#endregion

		#region Settings Methods
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

		#region iPad Methods
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
			labelItemDashboard.Text = SettingsManager.Instance.DashboardName;
			if (File.Exists(SettingsManager.Instance.DashboardLogoPath) && File.Exists(SettingsManager.Instance.DashboardIconPath))
				buttonItemDashboard.Image = new Bitmap(SettingsManager.Instance.DashboardLogoPath);
			ribbonBarDashboard.RecalcLayout();
			ribbonPanelDashboard.PerformLayout();

			LoadSalesDepot();

			SetPresentationSettings();

			comboBoxEditPowerPointStyle.EditValueChanging -= comboBoxEditPowerPointStyle_EditValueChanging;
			comboBoxEditPowerPointStyle.Properties.Items.Clear();
			foreach (string masterWizard in MasterWizardManager.Instance.MasterWizards.Keys)
				comboBoxEditPowerPointStyle.Properties.Items.Add(masterWizard);

			int selectedIndex = comboBoxEditPowerPointStyle.Properties.Items.IndexOf(SettingsManager.Instance.SelectedWizard);
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

			ribbonBarSettingsMonitors.Visible = Screen.AllScreens.Length > 1;

			if (SettingsManager.Instance.AutoRunFloat)
				ShowFloater();
		}

		private void InitTabPages()
		{
			var tabPage = SettingsManager.Instance.TabPageSettings.TabPages.FirstOrDefault(x => x.Id == TabNamesEnum.PowerPoint);
			ribbonTabItemPowerPoint.Text = tabPage != null ? tabPage.Name : TabPageSettings.UndefinedName;
			ribbonTabItemPowerPoint.Enabled = tabPage != null && tabPage.Enabled;

			tabPage = SettingsManager.Instance.TabPageSettings.TabPages.FirstOrDefault(x => x.Id == TabNamesEnum.Dashboard);
			ribbonTabItemDashboard.Text = tabPage != null ? tabPage.Name : TabPageSettings.UndefinedName;
			ribbonTabItemDashboard.Enabled = tabPage != null && tabPage.Enabled;

			tabPage = SettingsManager.Instance.TabPageSettings.TabPages.FirstOrDefault(x => x.Id == TabNamesEnum.SalesDepot);
			ribbonTabItemSalesDepot.Text = tabPage != null ? tabPage.Name : TabPageSettings.UndefinedName;
			ribbonTabItemSalesDepot.Enabled = tabPage != null && tabPage.Enabled;

			tabPage = SettingsManager.Instance.TabPageSettings.TabPages.FirstOrDefault(x => x.Id == TabNamesEnum.Apps);
			ribbonTabItemApps.Text = tabPage != null ? tabPage.Name : TabPageSettings.UndefinedName;
			ribbonTabItemApps.Enabled = tabPage != null && tabPage.Enabled;

			tabPage = SettingsManager.Instance.TabPageSettings.TabPages.FirstOrDefault(x => x.Id == TabNamesEnum.Clipart);
			ribbonTabItemClipart.Text = tabPage != null ? tabPage.Name : TabPageSettings.UndefinedName;
			ribbonTabItemClipart.Enabled = tabPage != null && tabPage.Enabled;

			tabPage = SettingsManager.Instance.TabPageSettings.TabPages.FirstOrDefault(x => x.Id == TabNamesEnum.PDF);
			ribbonTabItemPDF.Text = tabPage != null ? tabPage.Name : TabPageSettings.UndefinedName;
			ribbonTabItemPDF.Enabled = tabPage != null && tabPage.Enabled;

			tabPage = SettingsManager.Instance.TabPageSettings.TabPages.FirstOrDefault(x => x.Id == TabNamesEnum.Tools);
			ribbonTabItemTools.Text = tabPage != null ? tabPage.Name : TabPageSettings.UndefinedName;
			ribbonTabItemTools.Enabled = tabPage != null && tabPage.Enabled;

			tabPage = SettingsManager.Instance.TabPageSettings.TabPages.FirstOrDefault(x => x.Id == TabNamesEnum.Settings);
			ribbonTabItemSettings.Text = tabPage != null ? tabPage.Name : TabPageSettings.UndefinedName;
			ribbonTabItemSettings.Enabled = tabPage != null && tabPage.Enabled;

			tabPage = SettingsManager.Instance.TabPageSettings.TabPages.FirstOrDefault(x => x.Id == TabNamesEnum.iPad);
			ribbonTabItemIPad.Text = tabPage != null ? tabPage.Name : TabPageSettings.UndefinedName;
			ribbonTabItemIPad.Enabled = tabPage != null && tabPage.Enabled;

			tabPage = SettingsManager.Instance.TabPageSettings.TabPages.FirstOrDefault(x => x.Id == TabNamesEnum.Sync);
			ribbonTabItemSync.Text = tabPage != null ? tabPage.Name : TabPageSettings.UndefinedName;
			ribbonTabItemSync.Enabled = tabPage != null && tabPage.Enabled;
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