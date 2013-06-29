using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using AdScheduleBuilder.BusinessClasses;
using AdScheduleBuilder.ConfigurationClasses;
using AdScheduleBuilder.CustomControls;
using AdScheduleBuilder.OutputClasses.OutputControls;
using AdScheduleBuilder.ToolForms;
using DevExpress.XtraEditors;

namespace AdScheduleBuilder
{
	public partial class FormMain : Form
	{
		private static FormMain _instance;
		private Control _currentControl;

		private FormMain()
		{
			InitializeComponent();

			#region Schedule Settings Events
			buttonItemHomeHelp.Click += ScheduleSettingsControl.Instance.buttonItemPrintScheduleettingsHelp_Click;
			buttonItemHomeProductAdd.Click += ScheduleSettingsControl.Instance.buttonItemPublicationsAdd_Click;
			buttonItemHomeProductClone.Click += ScheduleSettingsControl.Instance.buttonItemPublicationsClone_Click;
			buttonItemHomeProductDelete.Click += ScheduleSettingsControl.Instance.buttonItemPublicationsDelete_Click;
			buttonItemHomeSave.Click += ScheduleSettingsControl.Instance.buttonItemPrintScheduleettingsSave_Click;
			buttonItemHomeSaveAs.Click += ScheduleSettingsControl.Instance.buttonItemPrintScheduleettingsSaveAs_Click;
			comboBoxEditBusinessName.EditValueChanged += ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged;
			comboBoxEditDecisionMaker.EditValueChanged += ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged;
			comboBoxEditClientType.EditValueChanged += ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged;
			textEditAccountNumber.EditValueChanged += ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged;
			checkBoxItemHomeAccountNumber.CheckedChanged += ScheduleSettingsControl.Instance.checkBoxItemAccountNumber_CheckedChanged;
			dateEditPresentationDate.EditValueChanged += ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged;
			dateEditFlightDatesStart.EditValueChanged += ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged;
			dateEditFlightDatesStart.EditValueChanged += ScheduleSettingsControl.Instance.FlightDateStartEditValueChanged;
			dateEditFlightDatesStart.EditValueChanged += ScheduleSettingsControl.Instance.CalcWeeksOnFlightDatesChange;
			dateEditFlightDatesEnd.EditValueChanged += ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged;
			dateEditFlightDatesEnd.EditValueChanged += ScheduleSettingsControl.Instance.CalcWeeksOnFlightDatesChange;
			dateEditFlightDatesStart.CloseUp += ScheduleSettingsControl.Instance.dateEditFlightDatesStart_CloseUp;
			dateEditFlightDatesEnd.CloseUp += ScheduleSettingsControl.Instance.dateEditFlightDatesEnd_CloseUp;
			comboBoxEditBusinessName.Enter += Editor_Enter;
			comboBoxEditBusinessName.MouseDown += Editor_MouseDown;
			comboBoxEditBusinessName.MouseUp += Editor_MouseUp;
			comboBoxEditDecisionMaker.Enter += Editor_Enter;
			comboBoxEditDecisionMaker.MouseDown += Editor_MouseDown;
			comboBoxEditDecisionMaker.MouseUp += Editor_MouseUp;
			comboBoxEditClientType.Enter += Editor_Enter;
			comboBoxEditClientType.MouseDown += Editor_MouseDown;
			comboBoxEditClientType.MouseUp += Editor_MouseUp;
			textEditAccountNumber.Enter += Editor_Enter;
			textEditAccountNumber.MouseDown += Editor_MouseDown;
			textEditAccountNumber.MouseUp += Editor_MouseUp;
			#endregion

			#region Schedule Builder Events
			buttonItemPrintScheduleHelp.Click += ScheduleBuilderControl.Instance.buttonItemPrintScheduleHelp_Click;
			buttonItemPrintScheduleSave.Click += ScheduleBuilderControl.Instance.buttonItemPrintScheduleSave_Click;
			buttonItemPrintScheduleSaveAs.Click += ScheduleBuilderControl.Instance.buttonItemPrintScheduleSaveAs_Click;
			buttonItemPrintScheduleAdPricingColumnInches.Click += ScheduleBuilderControl.Instance.buttonItemAdPricingColumnInches_Click;
			buttonItemPrintScheduleAdPricingFlat.Click += ScheduleBuilderControl.Instance.buttonItemAdPricingColumnInches_Click;
			buttonItemPrintScheduleAdPricingPagePercent.Click += ScheduleBuilderControl.Instance.buttonItemAdPricingColumnInches_Click;
			buttonItemPrintScheduleAdPricingColumnInches.CheckedChanged += ScheduleBuilderControl.Instance.buttonItemAdPricing_CheckedChanged;
			buttonItemPrintScheduleAdPricingFlat.CheckedChanged += ScheduleBuilderControl.Instance.buttonItemAdPricing_CheckedChanged;
			buttonItemPrintScheduleAdPricingPagePercent.CheckedChanged += ScheduleBuilderControl.Instance.buttonItemAdPricing_CheckedChanged;
			checkBoxItemPrintScheduleAdSizeStandartSquare.CheckedChanged += ScheduleBuilderControl.Instance.checkBoxItemAdSizeStandartSquare_CheckedChanged;
			checkBoxItemPrintScheduleStandartPageSize.CheckedChanged += ScheduleBuilderControl.Instance.checkBoxItemSizeOptions_CheckedChanged;
			checkBoxItemPrintScheduleSharePagePageSize.CheckedChanged += ScheduleBuilderControl.Instance.checkBoxItemSizeOptions_CheckedChanged;
			spinEditStandartHeight.EditValueChanged += ScheduleBuilderControl.Instance.spinEditStandart_EditValueChanged;
			spinEditStandartWidth.EditValueChanged += ScheduleBuilderControl.Instance.spinEditStandart_EditValueChanged;
			comboBoxEditStandartPageSize.EditValueChanged += ScheduleBuilderControl.Instance.comboBoxEditSizeOptions_EditValueChanged;
			comboBoxEditSharePagePageSize.EditValueChanged += ScheduleBuilderControl.Instance.comboBoxEditSizeOptions_EditValueChanged;
			comboBoxEditRateCard.EditValueChanged += ScheduleBuilderControl.Instance.comboBoxEditRateCard_EditValueChanged;
			comboBoxEditPercentOfPage.EditValueChanged += ScheduleBuilderControl.Instance.comboBoxEditPercentOfPage_EditValueChanged;
			checkedListBoxControlSharePageSquare.ItemCheck += ScheduleBuilderControl.Instance.checkedListBoxControlSharePageSquare_ItemCheck;
			buttonItemPrintScheduleColorOptionsSingle.Click += ScheduleBuilderControl.Instance.ColorOptions_Click;
			buttonItemPrintScheduleColorOptionsSpot.Click += ScheduleBuilderControl.Instance.ColorOptions_Click;
			buttonItemPrintScheduleColorOptionsFull.Click += ScheduleBuilderControl.Instance.ColorOptions_Click;
			buttonItemPrintScheduleColorOptionsSingle.CheckedChanged += ScheduleBuilderControl.Instance.ColorOptions_CheckedChanged;
			buttonItemPrintScheduleColorOptionsSpot.CheckedChanged += ScheduleBuilderControl.Instance.ColorOptions_CheckedChanged;
			buttonItemPrintScheduleColorOptionsFull.CheckedChanged += ScheduleBuilderControl.Instance.ColorOptions_CheckedChanged;
			buttonItemPrintScheduleColorOptionsCostPerAd.Click += ScheduleBuilderControl.Instance.buttonItemColorOptions_Click;
			buttonItemPrintScheduleColorOptionsPercentOfAd.Click += ScheduleBuilderControl.Instance.buttonItemColorOptions_Click;
			buttonItemPrintScheduleColorOptionsIncluded.Click += ScheduleBuilderControl.Instance.buttonItemColorOptions_Click;
			buttonItemPrintScheduleColorOptionsPCI.Click += ScheduleBuilderControl.Instance.buttonItemColorOptions_Click;
			buttonItemPrintScheduleColorOptionsCostPerAd.CheckedChanged += ScheduleBuilderControl.Instance.buttonItemColorOptions_CheckedChanged;
			buttonItemPrintScheduleColorOptionsPercentOfAd.CheckedChanged += ScheduleBuilderControl.Instance.buttonItemColorOptions_CheckedChanged;
			buttonItemPrintScheduleColorOptionsIncluded.CheckedChanged += ScheduleBuilderControl.Instance.buttonItemColorOptions_CheckedChanged;
			buttonItemPrintScheduleColorOptionsPCI.CheckedChanged += ScheduleBuilderControl.Instance.buttonItemColorOptions_CheckedChanged;
			spinEditCostPerInch.EditValueChanged += ScheduleBuilderControl.Instance.spinEditCostPerInch_EditValueChanged;
			buttonItemPrintScheduleAdd.Click += ScheduleBuilderControl.Instance.buttonItemAddInsert_Click;
			buttonItemPrintScheduleCloneInsert.Click += ScheduleBuilderControl.Instance.buttonItemCloneInsert_Click;
			buttonItemPrintScheduleDeleteInsert.Click += ScheduleBuilderControl.Instance.buttonItemDeleteInsert_Click;
			spinEditCostPerInch.Enter += Editor_Enter;
			spinEditCostPerInch.MouseDown += Editor_MouseDown;
			spinEditCostPerInch.MouseUp += Editor_MouseUp;
			spinEditStandartHeight.Enter += Editor_Enter;
			spinEditStandartHeight.MouseDown += Editor_MouseDown;
			spinEditStandartHeight.MouseUp += Editor_MouseUp;
			spinEditStandartWidth.Enter += Editor_Enter;
			spinEditStandartWidth.MouseDown += Editor_MouseDown;
			spinEditStandartWidth.MouseUp += Editor_MouseUp;
			comboBoxEditStandartPageSize.Enter += Editor_Enter;
			comboBoxEditStandartPageSize.MouseDown += Editor_MouseDown;
			comboBoxEditStandartPageSize.MouseUp += Editor_MouseUp;
			comboBoxEditSharePagePageSize.Enter += Editor_Enter;
			comboBoxEditSharePagePageSize.MouseDown += Editor_MouseDown;
			comboBoxEditSharePagePageSize.MouseUp += Editor_MouseUp;
			#endregion

			#region Summaries Events
			buttonItemOverviewReset.Click += SummariesControl.Instance.buttonItemSummariesReset_Click;
			buttonItemOverviewPreview.Click += SummariesControl.Instance.buttonItemSummariesPreview_Click;
			buttonItemOverviewEmail.Click += SummariesControl.Instance.buttonItemSummariesEmail_Click;
			buttonItemOverviewHelp.Click += SummariesControl.Instance.buttonItemSummariesHelp_Click;
			buttonItemOverviewSave.Click += SummariesControl.Instance.buttonItemSummariesSave_Click;
			buttonItemOverviewSaveAs.Click += SummariesControl.Instance.buttonItemSummariesSaveAs_Click;
			buttonItemOverviewPowerPoint.Click += SummariesControl.Instance.buttonItemSummariesPowerPoint_Click;
			buttonItemMultiSummaryReset.Click += SummariesControl.Instance.buttonItemSummariesReset_Click;
			buttonItemMultiSummaryPreview.Click += SummariesControl.Instance.buttonItemSummariesPreview_Click;
			buttonItemMultiSummaryEmail.Click += SummariesControl.Instance.buttonItemSummariesEmail_Click;
			buttonItemMultiSummaryHelp.Click += SummariesControl.Instance.buttonItemSummariesHelp_Click;
			buttonItemMultiSummarySave.Click += SummariesControl.Instance.buttonItemSummariesSave_Click;
			buttonItemMultiSummarySaveAs.Click += SummariesControl.Instance.buttonItemSummariesSaveAs_Click;
			buttonItemMultiSummaryPowerPoint.Click += SummariesControl.Instance.buttonItemSummariesPowerPoint_Click;
			buttonItemSnapshotOptions.CheckedChanged += OutputSnapshotControl.Instance.buttonItemSnapshotOptions_CheckedChanged;
			buttonItemSnapshotReset.Click += SummariesControl.Instance.buttonItemSummariesReset_Click;
			buttonItemSnapshotPreview.Click += SummariesControl.Instance.buttonItemSummariesPreview_Click;
			buttonItemSnapshotEmail.Click += SummariesControl.Instance.buttonItemSummariesEmail_Click;
			buttonItemSnapshotHelp.Click += SummariesControl.Instance.buttonItemSummariesHelp_Click;
			buttonItemSnapshotSave.Click += SummariesControl.Instance.buttonItemSummariesSave_Click;
			buttonItemSnapshotSaveAs.Click += SummariesControl.Instance.buttonItemSummariesSaveAs_Click;
			buttonItemSnapshotPowerPoint.Click += SummariesControl.Instance.buttonItemSummariesPowerPoint_Click;
			#endregion

			#region Grids Events
			buttonItemDetailedGridReset.Click += GridsControl.Instance.buttonItemGridsReset_Click;
			buttonItemDetailedGridHelp.Click += GridsControl.Instance.buttonItemGridsHelp_Click;
			buttonItemDetailedGridSave.Click += GridsControl.Instance.buttonItemGridsSave_Click;
			buttonItemDetailedGridSaveAs.Click += GridsControl.Instance.buttonItemGridsSaveAs_Click;
			buttonItemDetailedGridDetails.CheckedChanged += GridsControl.Instance.buttonItemGridsDetails_CheckedChanged;
			buttonItemDetailedGridPowerPoint.Click += GridsControl.Instance.buttonItemGridsPowerPoint_Click;
			buttonItemDetailedGridEmail.Click += GridsControl.Instance.buttonItemGridsEmail_Click;
			buttonItemDetailedGridPreview.Click += GridsControl.Instance.buttonItemGridsPreview_Click;

			buttonItemMultiGridReset.Click += GridsControl.Instance.buttonItemGridsReset_Click;
			buttonItemMultiGridHelp.Click += GridsControl.Instance.buttonItemGridsHelp_Click;
			buttonItemMultiGridSave.Click += GridsControl.Instance.buttonItemGridsSave_Click;
			buttonItemMultiGridSaveAs.Click += GridsControl.Instance.buttonItemGridsSaveAs_Click;
			buttonItemMultiGridDetails.CheckedChanged += GridsControl.Instance.buttonItemGridsDetails_CheckedChanged;
			buttonItemMultiGridPowerPoint.Click += GridsControl.Instance.buttonItemGridsPowerPoint_Click;
			buttonItemMultiGridEmail.Click += GridsControl.Instance.buttonItemGridsEmail_Click;
			buttonItemMultiGridPreview.Click += GridsControl.Instance.buttonItemGridsPreview_Click;

			buttonItemChronoGridReset.Click += GridsControl.Instance.buttonItemGridsReset_Click;
			buttonItemChronoGridHelp.Click += GridsControl.Instance.buttonItemGridsHelp_Click;
			buttonItemChronoGridSave.Click += GridsControl.Instance.buttonItemGridsSave_Click;
			buttonItemChronoGridSaveAs.Click += GridsControl.Instance.buttonItemGridsSaveAs_Click;
			buttonItemChronoGridDetails.CheckedChanged += GridsControl.Instance.buttonItemGridsDetails_CheckedChanged;
			buttonItemChronoGridPowerPoint.Click += GridsControl.Instance.buttonItemGridsPowerPoint_Click;
			buttonItemChronoGridEmail.Click += GridsControl.Instance.buttonItemGridsEmail_Click;
			buttonItemChronoGridPreview.Click += GridsControl.Instance.buttonItemGridsPreview_Click;
			#endregion

			#region Calendars Events
			buttonItemCalendarsDetails.CheckedChanged += CalendarsControl.Instance.buttonItemCalendarsOptions_CheckedChanged;
			buttonItemCalendarsReset.Click += CalendarsControl.Instance.buttonItemCalendarsReset_Click;
			buttonItemCalendarsHelp.Click += CalendarsControl.Instance.buttonItemCalendarsHelp_Click;
			buttonItemCalendarsSave.Click += CalendarsControl.Instance.buttonItemCalendarSave_Click;
			buttonItemCalendarsSaveAs.Click += CalendarsControl.Instance.buttonItemCalendarSaveAs_Click;
			buttonItemCalendarsPowerPoint.Click += CalendarsControl.Instance.buttonItemCalendarsPowerPoint_Click;
			buttonItemCalendarsEmail.Click += CalendarsControl.Instance.buttonItemCalendarsEmail_Click;
			buttonItemCalendarsPreview.Click += CalendarsControl.Instance.buttonItemCalendarsPreview_Click;
			buttonItemCalendarsExport.Click += CalendarsControl.Instance.buttonItemCalendarsExport_Click;
			#endregion

			#region Rate Card Events
			buttonItemRateCardHelp.Click += RateCardControl.Instance.buttonItemRateCardHelp_Click;
			comboBoxEditRateCards.EditValueChanged += RateCardControl.Instance.comboBoxEditRateCards_EditValueChanged;
			#endregion

			#region Success Models Events
			buttonItemSuccessModelsHelp.Click += ModelsOfSuccessContainerControl.Instance.buttonItemSuccessModelsHelp_Click;
			#endregion

			ribbonTabItemSuccessModels.Enabled = false;

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
				ribbonBarModelsOfSuccess.RecalcLayout();
				ribbonBarRateCards.RecalcLayout();
				ribbonBarPrintScheduleLines.RecalcLayout();
				ribbonBarHomeHelp.RecalcLayout();
				ribbonBarHomeSave.RecalcLayout();
				ribbonBarPrintScheduleExit.RecalcLayout();
				ribbonBarPrintScheduleHelp.RecalcLayout();
				ribbonBarPrintScheduleSave.RecalcLayout();
				ribbonBarSuccessModelsExit.RecalcLayout();
				ribbonBarSuccessModelsHelp.RecalcLayout();
				ribbonBarSnapshotExit.RecalcLayout();
				ribbonBarSnapshotHelp.RecalcLayout();
				ribbonBarSnapshotPowerPoint.RecalcLayout();
				ribbonBarSnapshotSave.RecalcLayout();
				ribbonBarSnapshotOptions.RecalcLayout();
				ribbonPanelPrintSchedule.PerformLayout();
				ribbonPanelScheduleSettings.PerformLayout();
				ribbonPanelCalendars.PerformLayout();
				ribbonPanelSuccessModels.PerformLayout();
				ribbonPanelSnapshot.PerformLayout();
			}
		}

		public bool IsMaximized
		{
			get { return WindowState == FormWindowState.Normal ? false : true; }
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

		public event EventHandler<EventArgs> FloaterRequested;
		public event EventHandler<ExportEventArgs> ScheduleExported;

		public static void RemoveInstance()
		{
			_instance.Dispose();
			_instance = null;
		}

		private bool AllowToLeaveCurrentControl()
		{
			bool result = false;
			if ((_currentControl == ScheduleSettingsControl.Instance))
			{
				result = ScheduleSettingsControl.Instance.AllowToLeaveControl;
			}
			else if ((_currentControl == ScheduleBuilderControl.Instance))
			{
				result = ScheduleBuilderControl.Instance.AllowToLeaveControl;
			}
			else if ((_currentControl == SummariesControl.Instance))
			{
				result = SummariesControl.Instance.AllowToLeaveControl;
			}
			else if ((_currentControl == GridsControl.Instance))
			{
				result = GridsControl.Instance.AllowToLeaveControl;
			}
			else if ((_currentControl == CalendarsControl.Instance))
			{
				result = CalendarsControl.Instance.AllowToLeaveControl;
			}
			else
				result = true;
			return result;
		}

		public void UpdateScheduleTab(bool enable)
		{
			ribbonTabItemPrintSchedule.Enabled = enable;
		}

		public void UpdateOutputTabs(bool enable)
		{
			ribbonTabItemOverview.Enabled = enable;
			ribbonTabItemMultiSummary.Enabled = enable;
			ribbonTabItemSnapshot.Enabled = enable;
			ribbonTabItemDetailedGrid.Enabled = enable;
			ribbonTabItemMultiGrid.Enabled = enable;
			ribbonTabItemChronoGrid.Enabled = enable;
			ribbonTabItemCalendars.Enabled = enable;
		}

		public void Export(Schedule sourceSchedule, bool buildAdvanced, bool buildGraphic, bool buildSimple)
		{
			Opacity = 0;
			if (ScheduleExported != null)
				ScheduleExported(this, new ExportEventArgs(sourceSchedule, buildAdvanced, buildGraphic, buildSimple));
			Close();
		}

		private void FormMain_ClientSizeChanged(object sender, EventArgs e)
		{
			RegistryHelper.MaximizeMainForm = IsMaximized;
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(SettingsManager.Instance.SelectedWizard))
				Instance.Text = "Ad Schedule Builder - " + SettingsManager.Instance.SelectedWizard + " - " + SettingsManager.Instance.Size;
			ribbonControl.Enabled = false;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Ad Schedule...";
				form.TopMost = true;
				var thread = new Thread(delegate()
											{
												Invoke((MethodInvoker)delegate()
																		  {
																			  ScheduleSettingsControl.Instance.LoadSchedule(false);
																			  ScheduleBuilderControl.Instance.LoadSchedule(false);
																			  OutputBasicOverviewControl.Instance.UpdateOutput(false);
																			  OutputCalendarControl.Instance.UpdateOutput(false);
																			  OutputChronologicalControl.Instance.UpdateOutput(false);
																			  OutputDetailedGridControl.Instance.UpdateOutput(false);
																			  OutputMultiGridControl.Instance.UpdateOutput(false);
																			  OutputMultiSummaryControl.Instance.UpdateOutput(false);
																			  OutputSnapshotControl.Instance.UpdateOutput(false);
																		  });
											});
				thread.Start();

				form.Show();

				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}

			Instance.ribbonTabItemRateCard.Enabled = RateCardManager.Instance.RateCardFolders.Count > 0;
			ribbonControl.SelectedRibbonTabItem = ribbonTabItemPrintScheduleettings;
			ribbonControl.SelectedRibbonTabChanged -= ribbonControl_SelectedRibbonTabChanged;
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
			if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemPrintScheduleettings)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					if (!pnMain.Controls.Contains(ScheduleSettingsControl.Instance))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(ScheduleSettingsControl.Instance);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					ScheduleSettingsControl.Instance.BringToFront();
					_currentControl = ScheduleSettingsControl.Instance;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemPrintSchedule)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					if (!pnMain.Controls.Contains(ScheduleBuilderControl.Instance))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(ScheduleBuilderControl.Instance);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					ScheduleBuilderControl.Instance.BringToFront();
					_currentControl = ScheduleBuilderControl.Instance;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemOverview)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					SummariesControl.Instance.SelectSummary(SummaryType.Overview);
					if (!pnMain.Controls.Contains(SummariesControl.Instance))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(SummariesControl.Instance);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					SummariesControl.Instance.BringToFront();
					_currentControl = SummariesControl.Instance;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMultiSummary)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					SummariesControl.Instance.SelectSummary(SummaryType.MultiSummary);
					if (!pnMain.Controls.Contains(SummariesControl.Instance))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(SummariesControl.Instance);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					SummariesControl.Instance.BringToFront();
					_currentControl = SummariesControl.Instance;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSnapshot)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					SummariesControl.Instance.SelectSummary(SummaryType.Snapshot);
					if (!pnMain.Controls.Contains(SummariesControl.Instance))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(SummariesControl.Instance);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					SummariesControl.Instance.BringToFront();
					_currentControl = SummariesControl.Instance;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemDetailedGrid)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					GridsControl.Instance.SelectGrid(GridType.DetailedGrid);
					if (!pnMain.Controls.Contains(GridsControl.Instance))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(GridsControl.Instance);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					GridsControl.Instance.BringToFront();
					_currentControl = GridsControl.Instance;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMultiGrid)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					GridsControl.Instance.SelectGrid(GridType.MultiGrid);
					if (!pnMain.Controls.Contains(GridsControl.Instance))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(GridsControl.Instance);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					GridsControl.Instance.BringToFront();
					_currentControl = GridsControl.Instance;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemChronoGrid)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					GridsControl.Instance.SelectGrid(GridType.ChronoGrid);
					if (!pnMain.Controls.Contains(GridsControl.Instance))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(GridsControl.Instance);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					GridsControl.Instance.BringToFront();
					_currentControl = GridsControl.Instance;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendars)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					CalendarsControl.Instance.UpdatePageAccordingToggledButton();
					if (!pnMain.Controls.Contains(CalendarsControl.Instance))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(CalendarsControl.Instance);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					CalendarsControl.Instance.BringToFront();
					_currentControl = CalendarsControl.Instance;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemRateCard)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					if (!pnMain.Controls.Contains(RateCardControl.Instance))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						RateCardControl.Instance.LoadRateCards();
						pnMain.Controls.Add(RateCardControl.Instance);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					RateCardControl.Instance.BringToFront();
					_currentControl = RateCardControl.Instance;
				}
				else
					_currentControl.BringToFront();
			}
			else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSuccessModels)
			{
				if (AllowToLeaveCurrentControl() || _currentControl == null)
				{
					if (!pnMain.Controls.Contains(ModelsOfSuccessContainerControl.Instance))
					{
						Application.DoEvents();
						pnEmpty.BringToFront();
						Application.DoEvents();
						pnMain.Controls.Add(ModelsOfSuccessContainerControl.Instance);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}
					ModelsOfSuccessContainerControl.Instance.BringToFront();
					ModelsOfSuccessContainerControl.Instance.UpdateSuccessModels();
					_currentControl = ModelsOfSuccessContainerControl.Instance;
				}
				else
					_currentControl.BringToFront();
			}
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool result = true;
			if (_currentControl == ScheduleSettingsControl.Instance)
				result = ScheduleSettingsControl.Instance.AllowToLeaveControl;
			else if (_currentControl == ScheduleBuilderControl.Instance)
				result = ScheduleBuilderControl.Instance.AllowToLeaveControl;
			else if (_currentControl == SummariesControl.Instance)
				result = SummariesControl.Instance.AllowToLeaveControl;
			else if (_currentControl == GridsControl.Instance)
				result = GridsControl.Instance.AllowToLeaveControl;
			else if (_currentControl == CalendarsControl.Instance)
				result = CalendarsControl.Instance.AllowToLeaveControl;
		}

		private void buttonItemDigital_Click(object sender, EventArgs e)
		{
			using (var form = new FormDigital())
			{
				form.ShowDialog();
			}
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