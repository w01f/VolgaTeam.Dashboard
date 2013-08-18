using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.AdSchedule.Controls;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Internal
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
			Controller.Instance.TabPrintProduct = ribbonTabItemPrintSchedule;
			Controller.Instance.TabDigitalProduct = ribbonTabItemDigitalSchedule;
			Controller.Instance.TabDigitalPackage = ribbonTabItemDigitalPackage;
			Controller.Instance.TabBasicOverview = ribbonTabItemOverview;
			Controller.Instance.TabMultiSummary = ribbonTabItemMultiSummary;
			Controller.Instance.TabSnapshot = ribbonTabItemSnapshot;
			Controller.Instance.TabDetailedGrid = ribbonTabItemDetailedGrid;
			Controller.Instance.TabMultiGrid = ribbonTabItemMultiGrid;
			Controller.Instance.TabCalendar = ribbonTabItemCalendars;
			Controller.Instance.TabSummary = ribbonTabItemSummary;

			#region Command Controls

			#region Home
			Controller.Instance.HomePanel = ribbonPanelScheduleSettings;
			Controller.Instance.HomeHelp = buttonItemHomeHelp;
			Controller.Instance.HomeSave = buttonItemHomeSave;
			Controller.Instance.HomeSaveAs = buttonItemHomeSaveAs;
			Controller.Instance.HomeProduct = ribbonBarHomeProduct;
			Controller.Instance.HomeAdProduct = itemContainerHomePrintProduct;
			Controller.Instance.HomeAdProductAdd = buttonItemHomePrintProductAdd;
			Controller.Instance.HomeAdProductClone = buttonItemHomePrintProductClone;
			Controller.Instance.HomeAdProductDelete = buttonItemHomePrintProductDelete;
			Controller.Instance.HomeDigitalProduct = itemContainerHomeDigitalProduct;
			Controller.Instance.HomeDigitalProductAdd = buttonItemHomeDigitalProductAdd;
			Controller.Instance.HomeDigitalProductClone = buttonItemHomeDigitalProductClone;
			Controller.Instance.HomeDigitalProductDelete = buttonItemHomeDigitalProductDelete;
			Controller.Instance.HomeBusinessName = comboBoxEditBusinessName;
			Controller.Instance.HomeDecisionMaker = comboBoxEditDecisionMaker;
			Controller.Instance.HomeClientType = comboBoxEditClientType;
			Controller.Instance.HomeAccountNumberText = textEditAccountNumber;
			Controller.Instance.HomeAccountNumberCheck = checkBoxItemHomeAccountNumber;
			Controller.Instance.HomePresentationDate = dateEditPresentationDate;
			Controller.Instance.HomeFlightDatesStart = dateEditFlightDatesStart;
			Controller.Instance.HomeFlightDatesEnd = dateEditFlightDatesEnd;
			Controller.Instance.HomeWeeks = labelItemHomeFlightDatesWeeks;
			#endregion

			#region Print Product
			Controller.Instance.PrintProductHelp = buttonItemPrintScheduleHelp;
			Controller.Instance.PrintProductSave = buttonItemPrintScheduleSave;
			Controller.Instance.PrintProductSaveAs = buttonItemPrintScheduleSaveAs;
			Controller.Instance.PrintProductAdd = buttonItemPrintScheduleAdd;
			Controller.Instance.PrintProductClone = buttonItemPrintScheduleCloneInsert;
			Controller.Instance.PrintProductDelete = buttonItemPrintScheduleDeleteInsert;
			Controller.Instance.PrintProductAdPricingColumnInches = buttonItemPrintScheduleAdPricingColumnInches;
			Controller.Instance.PrintProductAdPricingFlat = buttonItemPrintScheduleAdPricingFlat;
			Controller.Instance.PrintProductAdPricingPagePercent = buttonItemPrintScheduleAdPricingPagePercent;
			Controller.Instance.PrintProductColorOptionsSingle = buttonItemPrintScheduleColorOptionsSingle;
			Controller.Instance.PrintProductColorOptionsSpot = buttonItemPrintScheduleColorOptionsSpot;
			Controller.Instance.PrintProductColorOptionsFull = buttonItemPrintScheduleColorOptionsFull;
			Controller.Instance.PrintProductColorOptionsCostPerAd = buttonItemPrintScheduleColorOptionsCostPerAd;
			Controller.Instance.PrintProductColorOptionsPercentOfAd = buttonItemPrintScheduleColorOptionsPercentOfAd;
			Controller.Instance.PrintProductColorOptionsIncluded = buttonItemPrintScheduleColorOptionsIncluded;
			Controller.Instance.PrintProductColorOptionsPCI = buttonItemPrintScheduleColorOptionsPCI;
			Controller.Instance.PrintProductAdSizeStandart = itemContainerSchedulesAdSizeStandart;
			Controller.Instance.PrintProductAdSizeSharePage = itemContainerSchedulesAdSizeSharePage;
			Controller.Instance.PrintProductStandartEqualSign = laStandartEqualSign;
			Controller.Instance.PrintProductStandartSquareMetric = laStandartSquareMetric;
			Controller.Instance.PrintProductStandartSquareValue = laStandartSquareValue;
			Controller.Instance.PrintProductStandartEqualSignContainer = controlContainerItemEqualSign;
			Controller.Instance.PrintProductStandartSquareMetricContainer = controlContainerItemSquareMetric;
			Controller.Instance.PrintProductStandartSquareValueContainer = controlContainerItemSquareValue;
			Controller.Instance.PrintProductAdSizeSharePagePercentOfPageLabel = labelItemPrintScheduleAdSizeSharePagePercentOfPage;
			Controller.Instance.PrintProductAdSizeSharePageDimensionsLabel = labelItemPrintScheduleAdSizeSharePageDimensions;
			Controller.Instance.PrintProductAdSizeStandartSquare = checkBoxItemPrintScheduleAdSizeStandartSquare;
			Controller.Instance.PrintProductStandartPageSizeCheck = checkBoxItemPrintScheduleStandartPageSize;
			Controller.Instance.PrintProductSharePagePageSizeCheck = checkBoxItemPrintScheduleSharePagePageSize;
			Controller.Instance.PrintProductStandartPageSizeCombo = comboBoxEditStandartPageSize;
			Controller.Instance.PrintProductSharePagePageSizeCombo = comboBoxEditSharePagePageSize;
			Controller.Instance.PrintProductRateCard = comboBoxEditRateCard;
			Controller.Instance.PrintProductPercentOfPage = comboBoxEditPercentOfPage;
			Controller.Instance.PrintProductStandartHeight = spinEditStandartHeight;
			Controller.Instance.PrintProductStandartWidth = spinEditStandartWidth;
			Controller.Instance.PrintProductSharePageSquare = checkedListBoxControlSharePageSquare;
			Controller.Instance.PrintProductCostPerInch = spinEditCostPerInch;
			Controller.Instance.PrintProductAdSizeRibbonBar = ribbonBarPrintScheduleAdSize;
			Controller.Instance.PrintProductPanel = ribbonPanelPrintSchedule;
			#endregion

			#region Digital Product
			Controller.Instance.DigitalProductOptions = buttonItemDigitalScheduleOptions;
			Controller.Instance.DigitalProductPreview = buttonItemDigitalSchedulePreview;
			Controller.Instance.DigitalProductPowerPoint = buttonItemDigitalSchedulePowerPoint;
			Controller.Instance.DigitalProductEmail = buttonItemDigitalScheduleEmail;
			Controller.Instance.DigitalProductSave = buttonItemDigitalScheduleSave;
			Controller.Instance.DigitalProductSaveAs = buttonItemDigitalScheduleSaveAs;
			Controller.Instance.DigitalProductHelp = buttonItemDigitalScheduleHelp;
			#endregion

			#region Digital Package
			Controller.Instance.DigitalPackageHelp = buttonItemDigitalPackageHelp;
			Controller.Instance.DigitalPackageSave = buttonItemDigitalPackageSave;
			Controller.Instance.DigitalPackageSaveAs = buttonItemDigitalPackageSaveAs;
			Controller.Instance.DigitalPackagePreview = buttonItemDigitalPackagePreview;
			Controller.Instance.DigitalPackageEmail = buttonItemDigitalPackageEmail;
			Controller.Instance.DigitalPackagePowerPoint = buttonItemDigitalPackagePowerPoint;
			Controller.Instance.DigitalPackageOptions = buttonItemDigitalPackageOptions;
			#endregion

			#region Basic Overview
			Controller.Instance.BasicOverviewHelp = buttonItemOverviewHelp;
			Controller.Instance.BasicOverviewSave = buttonItemOverviewSave;
			Controller.Instance.BasicOverviewSaveAs = buttonItemOverviewSaveAs;
			Controller.Instance.BasicOverviewPreview = buttonItemOverviewPreview;
			Controller.Instance.BasicOverviewEmail = buttonItemOverviewEmail;
			Controller.Instance.BasicOverviewPowerPoint = buttonItemOverviewPowerPoint;
			Controller.Instance.BasicOverviewDigitalLegend = buttonItemOverviewDigital;
			#endregion

			#region Multi Summary
			Controller.Instance.MultiSummaryHelp = buttonItemMultiSummaryHelp;
			Controller.Instance.MultiSummarySave = buttonItemMultiSummarySave;
			Controller.Instance.MultiSummarySaveAs = buttonItemMultiSummarySaveAs;
			Controller.Instance.MultiSummaryPreview = buttonItemMultiSummaryPreview;
			Controller.Instance.MultiSummaryEmail = buttonItemMultiSummaryEmail;
			Controller.Instance.MultiSummaryPowerPoint = buttonItemMultiSummaryPowerPoint;
			Controller.Instance.MultiSummaryDigitalLegend = buttonItemMultiSummaryDigital;
			#endregion

			#region Snapshot
			Controller.Instance.SnapshotOptions = buttonItemSnapshotOptions;
			Controller.Instance.SnapshotHelp = buttonItemSnapshotHelp;
			Controller.Instance.SnapshotSave = buttonItemSnapshotSave;
			Controller.Instance.SnapshotSaveAs = buttonItemSnapshotSaveAs;
			Controller.Instance.SnapshotPreview = buttonItemSnapshotPreview;
			Controller.Instance.SnapshotEmail = buttonItemSnapshotEmail;
			Controller.Instance.SnapshotPowerPoint = buttonItemSnapshotPowerPoint;
			Controller.Instance.SnapshotDigitalLegend = buttonItemSnapshotDigital;
			#endregion

			#region Detailed Grid
			Controller.Instance.DetailedGridOptions = buttonItemDetailedGridDetails;
			Controller.Instance.DetailedGridHelp = buttonItemDetailedGridHelp;
			Controller.Instance.DetailedGridSave = buttonItemDetailedGridSave;
			Controller.Instance.DetailedGridSaveAs = buttonItemDetailedGridSaveAs;
			Controller.Instance.DetailedGridPreview = buttonItemDetailedGridPreview;
			Controller.Instance.DetailedGridEmail = buttonItemDetailedGridEmail;
			Controller.Instance.DetailedGridPowerPoint = buttonItemDetailedGridPowerPoint;
			Controller.Instance.DetailedGridDigitalLegend = buttonItemDetailedGridDigital;
			#endregion

			#region Multi Grid
			Controller.Instance.MultiGridOptions = buttonItemMultiGridDetails;
			Controller.Instance.MultiGridHelp = buttonItemMultiGridHelp;
			Controller.Instance.MultiGridSave = buttonItemMultiGridSave;
			Controller.Instance.MultiGridSaveAs = buttonItemMultiGridSaveAs;
			Controller.Instance.MultiGridPreview = buttonItemMultiGridPreview;
			Controller.Instance.MultiGridEmail = buttonItemMultiGridEmail;
			Controller.Instance.MultiGridPowerPoint = buttonItemMultiGridPowerPoint;
			Controller.Instance.MultiGridDigitalLegend = buttonItemMultiGridDigital;
			#endregion

			#region Calendar
			Controller.Instance.CalendarOptions = buttonItemCalendarsDetails;
			Controller.Instance.CalendarHelp = buttonItemCalendarsHelp;
			Controller.Instance.CalendarSave = buttonItemCalendarsSave;
			Controller.Instance.CalendarSaveAs = buttonItemCalendarsSaveAs;
			Controller.Instance.CalendarPreview = buttonItemCalendarsPreview;
			Controller.Instance.CalendarEmail = buttonItemCalendarsEmail;
			Controller.Instance.CalendarPowerPoint = buttonItemCalendarsPowerPoint;
			Controller.Instance.CalendarMonthList = listBoxControlCalendar;
			#endregion

			#region Summary
			Controller.Instance.SummaryAddItem = buttonItemSummaryAdd;
			Controller.Instance.SummaryHelp = buttonItemSummaryHelp;
			Controller.Instance.SummarySave = buttonItemSummarySave;
			Controller.Instance.SummarySaveAs = buttonItemSummarySaveAs;
			Controller.Instance.SummaryPreview = buttonItemSummaryPreview;
			Controller.Instance.SummaryEmail = buttonItemSummaryEmail;
			Controller.Instance.SummaryPowerPoint = buttonItemSummaryPowerPoint;
			#endregion

			#region Rate Card
			Controller.Instance.RateCardHelp = buttonItemRateCardHelp;
			Controller.Instance.RateCardCombo = comboBoxEditRateCards;
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
				comboBoxEditClientType.Font = font;
				comboBoxEditDecisionMaker.Font = font;
				textEditAccountNumber.Font = font;
				spinEditCostPerInch.Font = font;
				spinEditStandartHeight.Font = font;
				spinEditStandartWidth.Font = font;
				comboBoxEditPercentOfPage.Font = font;
				comboBoxEditRateCard.Font = font;
				comboBoxEditRateCards.Font = font;
				comboBoxEditSharePagePageSize.Font = font;
				comboBoxEditStandartPageSize.Font = font;
				checkedListBoxControlSharePageSquare.Font = font;
				dateEditFlightDatesEnd.Font = font;
				dateEditFlightDatesStart.Font = font;
				dateEditPresentationDate.Font = font;

				laStandartEqualSign.Font = new Font(laStandartEqualSign.Font.FontFamily, laStandartEqualSign.Font.Size - 2, laStandartEqualSign.Font.Style);
				laRateCards.Font = new Font(laRateCards.Font.FontFamily, laRateCards.Font.Size - 3, laRateCards.Font.Style);
				laStandartSquareMetric.Font = new Font(laStandartSquareMetric.Font.FontFamily, laStandartSquareMetric.Font.Size - 2, laStandartSquareMetric.Font.Style);
				laStandartSquareValue.Font = new Font(laStandartSquareValue.Font.FontFamily, laStandartSquareValue.Font.Size - 2, laStandartSquareValue.Font.Style);

				ribbonBarPrintScheduleAdPricingStrategy.RecalcLayout();
				ribbonBarPrintScheduleAdSize.RecalcLayout();
				ribbonBarHomeBasicInfo.RecalcLayout();
				ribbonBarCalendarsExit.RecalcLayout();
				ribbonBarCalendarsHelp.RecalcLayout();
				ribbonBarCalendarsSave.RecalcLayout();
				ribbonBarPrintScheduleColorPricing.RecalcLayout();
				ribbonBarHomeFlightDates.RecalcLayout();
				ribbonBarHomeExit.RecalcLayout();
				ribbonBarHomeProduct.RecalcLayout();
				ribbonBarRateCards.RecalcLayout();
				ribbonBarPrintScheduleLines.RecalcLayout();
				ribbonBarHomeHelp.RecalcLayout();
				ribbonBarHomeSave.RecalcLayout();
				ribbonBarPrintScheduleExit.RecalcLayout();
				ribbonBarPrintScheduleHelp.RecalcLayout();
				ribbonBarPrintScheduleSave.RecalcLayout();
				ribbonBarSnapshotExit.RecalcLayout();
				ribbonBarSnapshotHelp.RecalcLayout();
				ribbonBarSnapshotPowerPoint.RecalcLayout();
				ribbonBarSnapshotSave.RecalcLayout();
				ribbonBarSnapshotOptions.RecalcLayout();
				ribbonPanelPrintSchedule.PerformLayout();
				ribbonPanelScheduleSettings.PerformLayout();
				ribbonPanelCalendars.PerformLayout();
				ribbonPanelSnapshot.PerformLayout();
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

		public event EventHandler<EventArgs> FloaterRequested;

		private bool AllowToLeaveCurrentControl()
		{
			bool result = false;
			if ((_currentControl == Controller.Instance.ScheduleSettings))
			{
				result = Controller.Instance.ScheduleSettings.AllowToLeaveControl;
			}
			else if ((_currentControl == Controller.Instance.PrintProductContainer))
			{
				result = Controller.Instance.PrintProductContainer.AllowToLeaveControl;
			}
			else if ((_currentControl == Controller.Instance.DigitalProductContainer))
			{
				result = Controller.Instance.DigitalProductContainer.AllowToLeaveControl;
			}
			else if ((_currentControl == Controller.Instance.DigitalPackage))
			{
				result = Controller.Instance.DigitalPackage.AllowToLeaveControl;
			}
			else if ((_currentControl == Controller.Instance.Summaries))
			{
				result = Controller.Instance.Summaries.AllowToLeaveControl;
			}
			else if ((_currentControl == Controller.Instance.Grids))
			{
				result = Controller.Instance.Grids.AllowToLeaveControl;
			}
			else if ((_currentControl == Controller.Instance.Calendars))
			{
				result = Controller.Instance.Calendars.AllowToLeaveControl;
			}
			else if ((_currentControl == Controller.Instance.Summary))
			{
				result = Controller.Instance.Summary.AllowToLeaveControl;
			}
			else
				result = true;
			return result;
		}

		private void FormMain_ClientSizeChanged(object sender, EventArgs e)
		{
			RegistryHelper.MaximizeMainForm = WindowState == FormWindowState.Maximized;
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(SettingsManager.Instance.SelectedWizard))
				Text = String.Format("SellerPoint Media Schedules - {0} - {1} ({2})", SettingsManager.Instance.SelectedWizard, SettingsManager.Instance.Size, BusinessWrapper.Instance.ScheduleManager.GetShortSchedule().ShortFileName);
			ribbonControl.Enabled = false;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Ad Schedule...";
				form.TopMost = true;
				form.Show();
				var thread = new Thread(delegate() { Invoke((MethodInvoker)delegate() { Controller.Instance.LoadData(); }); });
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
			ribbonTabItemRateCard.Enabled = BusinessWrapper.Instance.RateCardManager.RateCardFolders.Count > 0;
			ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
			ribbonControl.SelectedRibbonTabItem = ribbonTabItemScheduleSettings;
			ribbonControl_SelectedRibbonTabChanged(null, null);
			ribbonControl.SelectedRibbonTabChanged += ribbonControl_SelectedRibbonTabChanged;
			if (SettingsManager.Instance.SizeWidth == 10 && SettingsManager.Instance.SizeHeght == 5.63)
				buttonItemCalendarsPowerPoint.Enabled = false;
			else
				buttonItemCalendarsPowerPoint.Enabled = true;
			ribbonControl.Enabled = true;
		}

		private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemScheduleSettings)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					if (!pnMain.Controls.Contains(Controller.Instance.ScheduleSettings))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(Controller.Instance.ScheduleSettings);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					Controller.Instance.ScheduleSettings.BringToFront();
					_currentControl = Controller.Instance.ScheduleSettings;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemPrintSchedule)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					if (!pnMain.Controls.Contains(Controller.Instance.PrintProductContainer))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(Controller.Instance.PrintProductContainer);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					Controller.Instance.PrintProductContainer.BringToFront();
					_currentControl = Controller.Instance.PrintProductContainer;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemDigitalSchedule)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					if (!pnMain.Controls.Contains(Controller.Instance.DigitalProductContainer))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(Controller.Instance.DigitalProductContainer);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					Controller.Instance.DigitalProductContainer.BringToFront();
					_currentControl = Controller.Instance.DigitalProductContainer;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemDigitalPackage)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					if (!pnMain.Controls.Contains(Controller.Instance.DigitalPackage))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(Controller.Instance.DigitalPackage);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					Controller.Instance.DigitalPackage.BringToFront();
					_currentControl = Controller.Instance.DigitalPackage;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemOverview)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					Controller.Instance.Summaries.SelectSummary(SummaryType.Overview);
					if (!pnMain.Controls.Contains(Controller.Instance.Summaries))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(Controller.Instance.Summaries);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					Controller.Instance.Summaries.BringToFront();
					_currentControl = Controller.Instance.Summaries;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMultiSummary)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					Controller.Instance.Summaries.SelectSummary(SummaryType.MultiSummary);
					if (!pnMain.Controls.Contains(Controller.Instance.Summaries))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(Controller.Instance.Summaries);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					Controller.Instance.Summaries.BringToFront();
					_currentControl = Controller.Instance.Summaries;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSnapshot)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					Controller.Instance.Summaries.SelectSummary(SummaryType.Snapshot);
					if (!pnMain.Controls.Contains(Controller.Instance.Summaries))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(Controller.Instance.Summaries);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					Controller.Instance.Summaries.BringToFront();
					_currentControl = Controller.Instance.Summaries;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemDetailedGrid)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					Controller.Instance.Grids.SelectGrid(GridType.DetailedGrid);
					if (!pnMain.Controls.Contains(Controller.Instance.Grids))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(Controller.Instance.Grids);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					Controller.Instance.Grids.BringToFront();
					_currentControl = Controller.Instance.Grids;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMultiGrid)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					Controller.Instance.Grids.SelectGrid(GridType.MultiGrid);
					if (!pnMain.Controls.Contains(Controller.Instance.Grids))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(Controller.Instance.Grids);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					Controller.Instance.Grids.BringToFront();
					_currentControl = Controller.Instance.Grids;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendars)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					Controller.Instance.Calendars.UpdatePageAccordingToggledButton();
					if (!pnMain.Controls.Contains(Controller.Instance.Calendars))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(Controller.Instance.Calendars);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					Controller.Instance.Calendars.BringToFront();
					_currentControl = Controller.Instance.Calendars;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSummary)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					if (!pnMain.Controls.Contains(Controller.Instance.Summary))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(Controller.Instance.Summary);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					Controller.Instance.Summary.BringToFront();
					_currentControl = Controller.Instance.Summary;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemRateCard)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					if (!pnMain.Controls.Contains(Controller.Instance.RateCard))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						Controller.Instance.RateCard.LoadRateCards();
						pnMain.Controls.Add(Controller.Instance.RateCard);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					Controller.Instance.RateCard.BringToFront();
					_currentControl = Controller.Instance.RateCard;
				}
				else
					_currentControl.BringToFront();
			}
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool result = true;
			if (_currentControl == Controller.Instance.ScheduleSettings)
				result = Controller.Instance.ScheduleSettings.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.PrintProductContainer)
				result = Controller.Instance.PrintProductContainer.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.DigitalProductContainer)
				result = Controller.Instance.DigitalProductContainer.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.DigitalPackage)
				result = Controller.Instance.DigitalPackage.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.Summaries)
				result = Controller.Instance.Summaries.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.Grids)
				result = Controller.Instance.Grids.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.Calendars)
				result = Controller.Instance.Calendars.AllowToLeaveControl;
			else if (_currentControl == Controller.Instance.Summary)
				result = Controller.Instance.Summary.AllowToLeaveControl;
		}

		private void buttonItemFloater_Click(object sender, EventArgs e)
		{
			if (FloaterRequested != null)
				FloaterRequested(this, e);
		}

		private void buttonItemExit_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}