using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.OnlineSchedule.Controls;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms;

namespace NewBizWiz.OnlineSchedule.Internal
{
	public partial class FormMain : Form
	{
		private static FormMain _instance;
		private Control _currentControl;

		private FormMain()
		{
			InitializeComponent();

			Controller.Instance.FormMain = this;
			Controller.Instance.Supertip = superTooltip;
			Controller.Instance.Ribbon = ribbonControl;
			Controller.Instance.TabScheduleSlides = ribbonTabItemBuildSchedules;
			Controller.Instance.TabWebPackage = ribbonTabItemWebPackage;
			Controller.Instance.TabWebSummary = ribbonTabItemProductSummary;
			Controller.Instance.TabWebBundle = ribbonTabItemProductBundle;

			#region Command Controls

			#region Home
			Controller.Instance.HomePanel = ribbonPanelScheduleSettings;
			Controller.Instance.HomeAdvertiserProfileBar = ribbonBarAdvertiser;
			Controller.Instance.HomeBusinessName = comboBoxEditBusinessName;
			Controller.Instance.HomeDecisionMaker = comboBoxEditDecisionMaker;
			Controller.Instance.HomePresentationDate = dateEditPresentationDate;
			Controller.Instance.HomeFlightDatesBar = ribbonBarFlightDates;
			Controller.Instance.HomeFlightDatesStart = dateEditFlightDatesStart;
			Controller.Instance.HomeFlightDatesEnd = dateEditFlightDatesEnd;
			Controller.Instance.HomeWeeks = labelItemFlightDatesWeeks;
			Controller.Instance.HomeSaveBar = ribbonBarScheduleSave;
			Controller.Instance.HomeSave = buttonItemSchedulesSave;
			Controller.Instance.HomeSaveAs = buttonItemSchedulesSaveAs;
			Controller.Instance.HomeHelpBar = ribbonBarSchedulesHelp;
			Controller.Instance.HomeHelp = buttonItemSchedulesHelp;
			Controller.Instance.HomeExitBar = ribbonBarHomeExit;
			#endregion

			#region Schedule Slides
			Controller.Instance.ScheduleSlidesHelp = buttonItemScheduleHelp;
			Controller.Instance.ScheduleSlidesSave = buttonItemScheduleSave;
			Controller.Instance.ScheduleSlidesSaveAs = buttonItemScheduleSaveAs;
			Controller.Instance.ScheduleSlidesEmail = buttonItemSchedulesEmail;
			Controller.Instance.ScheduleSlidesPowerPoint = buttonItemSchedulesPowerPoint;
			Controller.Instance.ScheduleSlidesOptions = buttonItemSchedulesOptions;
			#endregion

			#region Web Package
			Controller.Instance.WebPackageHelp = buttonItemWebPackageHelp;
			Controller.Instance.WebPackageSave = buttonItemWebPackageSave;
			Controller.Instance.WebPackageSaveAs = buttonItemWebPackageSaveAs;
			Controller.Instance.WebPackageEmail = buttonItemWebPackageEmail;
			Controller.Instance.WebPackagePowerPoint = buttonItemWebPackagePowerPoint;
			Controller.Instance.WebPackageOptions = buttonItemWebPackageOptions;
			#endregion

			#region Web Summary
			Controller.Instance.WebSummaryHelp = buttonItemProductSummaryHelp;
			Controller.Instance.WebSummarySave = buttonItemProductSummarySave;
			Controller.Instance.WebSummarySaveAs = buttonItemProductSummarySaveAs;
			Controller.Instance.WebSummaryEmail = buttonItemProductSummaryEmail;
			Controller.Instance.WebSummaryPowerPoint = buttonItemProductSummaryPowerPoint;
			Controller.Instance.WebSummaryWebsites = buttonItemProductSummaryWebsites;
			Controller.Instance.WebSummaryDimensions = buttonItemProductSummaryDimensions;
			Controller.Instance.WebSummaryProductImpressions = buttonItemProductSummaryImpressions;
			Controller.Instance.WebSummaryTotalAds = buttonItemProductSummaryTotalAds;
			Controller.Instance.WebSummaryActiveDays = buttonItemProductSummaryActiveDays;
			Controller.Instance.WebSummaryAdRate = buttonItemProductSummaryAdRate;
			Controller.Instance.WebSummaryProductInvestment = buttonItemProductSummaryInvestment;
			Controller.Instance.WebSummaryCPM = buttonItemProductSummaryCPM;
			Controller.Instance.WebSummaryMonthlyImpressions = buttonItemProductSummaryMonthlyImpressions;
			Controller.Instance.WebSummaryTotalImpressions = buttonItemProductSummaryTotalImpressions;
			Controller.Instance.WebSummaryMonthlyInvestment = buttonItemProductSummaryMonthlyInvestment;
			Controller.Instance.WebSummaryTotalInvestment = buttonItemProductSummaryTotalInvestment;
			#endregion

			#region Web Bundle
			Controller.Instance.WebBundleHelp = buttonItemProductBundleHelp;
			Controller.Instance.WebBundleSave = buttonItemProductBundleSave;
			Controller.Instance.WebBundleSaveAs = buttonItemProductBundleSaveAs;
			Controller.Instance.WebBundleEmail = buttonItemProductBundleEmail;
			Controller.Instance.WebBundlePowerPoint = buttonItemProductBundlePowerPoint;
			Controller.Instance.WebBundleWebsites = buttonItemProductBundleWebsites;
			Controller.Instance.WebBundleDimensions = buttonItemProductBundleDimensions;
			Controller.Instance.WebBundleProductImpressions = buttonItemProductBundleImpressions;
			Controller.Instance.WebBundleTotalAds = buttonItemProductBundleTotalAds;
			Controller.Instance.WebBundleActiveDays = buttonItemProductBundleActiveDays;
			Controller.Instance.WebBundleAdRate = buttonItemProductBundleAdRate;
			Controller.Instance.WebBundleProductInvestment = buttonItemProductBundleInvestment;
			Controller.Instance.WebBundleCPM = buttonItemProductBundleCPM;
			Controller.Instance.WebBundleMonthlyImpressions = buttonItemProductBundleMonthlyImpressions;
			Controller.Instance.WebBundleTotalImpressions = buttonItemProductBundleTotalImpressions;
			Controller.Instance.WebBundleMonthlyInvestment = buttonItemProductBundleMonthlyInvestment;
			Controller.Instance.WebBundleTotalInvestment = buttonItemProductBundleTotalInvestment;
			#endregion

			#endregion

			Controller.Instance.Init();

			if ((base.CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 1, styleController.Appearance.Font.Style);
				ribbonControl.Font = font;
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
				comboBoxEditBusinessName.Font = font;
				comboBoxEditDecisionMaker.Font = font;
				dateEditFlightDatesEnd.Font = font;
				dateEditFlightDatesStart.Font = font;
				dateEditPresentationDate.Font = font;
				ribbonBarAdvertiser.RecalcLayout();
				ribbonBarFlightDates.RecalcLayout();
				ribbonBarHomeExit.RecalcLayout();
				ribbonBarProductBundleCombinedTotals.RecalcLayout();
				ribbonBarProductBundleEmail.RecalcLayout();
				ribbonBarProductBundleExit.RecalcLayout();
				ribbonBarProductBundleHelp.RecalcLayout();
				ribbonBarProductBundleLogo.RecalcLayout();
				ribbonBarProductBundlePowerPoint.RecalcLayout();
				ribbonBarProductBundleProductDetails.RecalcLayout();
				ribbonBarProductBundleSave.RecalcLayout();
				ribbonBarProductSummaryEmail.RecalcLayout();
				ribbonBarProductSummaryExit.RecalcLayout();
				ribbonBarProductSummaryHelp.RecalcLayout();
				ribbonBarProductSummaryLogo.RecalcLayout();
				ribbonBarProductSummaryPowerPoint.RecalcLayout();
				ribbonBarProductSummaryProductDetails.RecalcLayout();
				ribbonBarProductSummarySave.RecalcLayout();
				ribbonBarRoductSummaryCombinedTotals.RecalcLayout();
				ribbonBarScheduleHelp.RecalcLayout();
				ribbonBarScheduleSave.RecalcLayout();
				ribbonBarSchedulesEmail.RecalcLayout();
				ribbonBarSchedulesExit.RecalcLayout();
				ribbonBarSchedulesHelp.RecalcLayout();
				ribbonBarSchedulesPowerPoint.RecalcLayout();
				ribbonBarSchedulesSaveAs.RecalcLayout();
				ribbonBarWebPackageEmail.RecalcLayout();
				ribbonBarWebPackageExit.RecalcLayout();
				ribbonBarWebPackageHelp.RecalcLayout();
				ribbonBarWebPackageOptions.RecalcLayout();
				ribbonBarWebPackagePowerPoint.RecalcLayout();
				ribbonBarWebPackageSaveAs.RecalcLayout();
				ribbonPanelBuildSchedules.PerformLayout();
				ribbonPanelScheduleSettings.PerformLayout();
				ribbonPanelProductBundle.PerformLayout();
				ribbonPanelProductSummary.PerformLayout();
				ribbonPanelWebPackage.PerformLayout();
			}
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

		public static void RemoveInstance()
		{
			_instance.Dispose();
			_instance = null;
		}

		private bool AllowToLeaveCurrentControl()
		{
			bool result = false;
			if ((_currentControl == Controller.Instance.ScheduleSettings))
			{
				if (Controller.Instance.ScheduleSettings.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemScheduleSettings;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == Controller.Instance.ScheduleSlides))
			{
				if (Controller.Instance.ScheduleSlides.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemBuildSchedules;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == Controller.Instance.WebPackage))
			{
				if (Controller.Instance.WebPackage.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemWebPackage;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == Controller.Instance.WebSummary))
			{
				if (Controller.Instance.WebSummary.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemProductSummary;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else if ((_currentControl == Controller.Instance.WebBundle))
			{
				if (Controller.Instance.WebBundle.AllowToLeaveControl)
					result = true;
				else
				{
					ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
					ribbonControl.SelectedRibbonTabItem = ribbonTabItemProductBundle;
					ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
				}
			}
			else
				result = true;
			return result;
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(SettingsManager.Instance.SelectedWizard))
				Instance.Text = "Online Schedule Builder - " + SettingsManager.Instance.SelectedWizard + " - " + SettingsManager.Instance.Size;
			ribbonControl.Enabled = false;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Online Schedule...";
				form.TopMost = true;
				form.Show();
				var thread = new Thread(delegate() { Invoke((MethodInvoker)delegate { Controller.Instance.LoadData(); }); });
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}

			ribbonControl.SelectedRibbonTabItem = ribbonTabItemScheduleSettings;
			ribbonControl_SelectedRibbonTabChanged(null, null);
			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
			ribbonControl.Enabled = true;
		}

		public void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemScheduleSettings)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.ScheduleSettings;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(Controller.Instance.ScheduleSettings);
				}
				_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemBuildSchedules)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.ScheduleSlides;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(Controller.Instance.ScheduleSlides);
				}
				_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemWebPackage)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.WebPackage;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(Controller.Instance.WebPackage);
				}
				_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProductSummary)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.WebSummary;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(Controller.Instance.WebSummary);
				}
				_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemProductBundle)
			{
				if (AllowToLeaveCurrentControl())
				{
					_currentControl = Controller.Instance.WebBundle;
					if (!pnMain.Controls.Contains(_currentControl))
						pnMain.Controls.Add(Controller.Instance.WebBundle);
				}
				_currentControl.BringToFront();
			}
			pnMain.BringToFront();
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool result = true;
			if (_currentControl == Controller.Instance.ScheduleSettings)
				result = Controller.Instance.ScheduleSettings.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.ScheduleSlides)
				result = Controller.Instance.ScheduleSlides.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.WebPackage)
				result = Controller.Instance.WebPackage.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.WebSummary)
				result = Controller.Instance.WebSummary.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.WebBundle)
				result = Controller.Instance.WebBundle.AllowToLeaveControl;
		}

		private void buttonItemHomeExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void pnMain_Click(object sender, EventArgs e)
		{
			if ((sender as Control) != null)
				(sender as Control).Focus();
		}
	}
}