using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Enums;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Option;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ContentEditors.Controls;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.RetractableBar;
using Asa.Common.GUI.Themes;
using Asa.Common.GUI.ToolForms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab.ViewInfo;
using Asa.Media.Controls.BusinessClasses;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.Properties;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls
{
	[ToolboxItem(false)]
	public partial class OptionsContainer : BasePartitionEditControl<OptionsContent, MediaSchedule, MediaScheduleSettings, MediaScheduleChangeInfo>
	//public partial class OptionsContainer : UserControl
	{
		private bool _allowToSave;
		private XtraTabHitInfo _menuHitInfo;
		private XtraTabDragDropHelper<OptionsControl> _tabDragDropHelper;

		public override string Identifier
		{
			get { return ContentIdentifiers.Options; }
		}

		public override RibbonTabItem TabPage
		{
			get { return Controller.Instance.TabOptions; }
		}

		private MediaSchedule Schedule
		{
			get { return BusinessObjects.Instance.ScheduleManager.ActiveSchedule; }
		}

		private MediaScheduleSettings ScheduleSettings
		{
			get { return Schedule.Settings; }
		}

		public SlideType SlideType
		{
			get { return MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVOptions : SlideType.RadioOptions; }
		}

		private OptionsControl ActiveOptionControl
		{
			get { return xtraTabControlOptionSets.SelectedTabPage as OptionsControl; }
		}

		private OptionsSummaryControl ActiveSummary
		{
			get { return xtraTabControlOptionSets.SelectedTabPage as OptionsSummaryControl; }
		}

		private OptionsSummaryControl Summary
		{
			get { return xtraTabControlOptionSets.TabPages.OfType<OptionsSummaryControl>().Single(); }
		}

		public OptionsContainer()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				font = new Font(laAvgRateTitle.Font.FontFamily, laAvgRateTitle.Font.Size - 2, laAvgRateTitle.Font.Style);
				laTotalSpotsTitle.Font = font;
				laAvgRateTitle.Font = font;
				laTotalCostTitle.Font = font;
				font = new Font(laAvgRateValue.Font.FontFamily, laAvgRateValue.Font.Size - 2, laAvgRateValue.Font.Style);
				laTotalSpotsValue.Font = font;
				laAvgRateValue.Font = font;
				laTotalCostValue.Font = font;

				laColorsTitle.Font = new Font(laColorsTitle.Font.FontFamily, laColorsTitle.Font.Size - 2, laColorsTitle.Font.Style);
				labelControlScheduleInfo.Font = new Font(labelControlScheduleInfo.Font.FontFamily,
					labelControlScheduleInfo.Font.Size - 2, labelControlScheduleInfo.Font.Style);

				font = new Font(buttonXOptionAvgRate.Font.FontFamily, buttonXOptionAvgRate.Font.Size - 2,
					buttonXOptionAvgRate.Font.Style);
				buttonXOptionAvgRate.Font = font;
				buttonXOptionCost.Font = font;
				buttonXOptionDay.Font = font;
				buttonXOptionLength.Font = font;
				buttonXOptionLineId.Font = font;
				buttonXOptionLogo.Font = font;
				buttonXOptionProgram.Font = font;
				buttonXOptionRate.Font = font;
				buttonXOptionStation.Font = font;
				buttonXOptionTime.Font = font;
				buttonXOptionMonthlySpots.Font = font;
				buttonXOptionWeeklySpots.Font = font;
				buttonXOptionTotalSpots.Font = font;
				buttonXOptionTallySpots.Font = font;
				buttonXOptionTallyCost.Font = font;
				buttonXSummaryCampaign.Font = font;
				buttonXSummaryComments.Font = font;
				buttonXSummaryTallyCost.Font = font;
				buttonXSummaryTallySpots.Font = font;
				buttonXSummaryTotalCost.Font = font;
				buttonXSummaryTotalWeeks.Font = font;
				buttonXSummaryLineId.Font = font;
				buttonXSummaryLogo.Font = font;
				buttonXSummaryMonthlyCost.Font = font;
				buttonXSummaryMonthlySpots.Font = font;
				buttonXSummaryWeeklyCost.Font = font;
				buttonXSummaryWeeklySpots.Font = font;
				buttonXSummaryTotalMonths.Font = font;
				buttonXSummaryTotalSpots.Font = font;

				hyperLinkEditInfoAdvanced.Font = new Font(hyperLinkEditInfoAdvanced.Font.FontFamily,
					hyperLinkEditInfoAdvanced.Font.Size - 2, hyperLinkEditInfoAdvanced.Font.Style);
				hyperLinkEditInfoContract.Font = new Font(hyperLinkEditInfoContract.Font.FontFamily,
					hyperLinkEditInfoContract.Font.Size - 2, hyperLinkEditInfoContract.Font.Style);
			}
		}

		#region BasePartitionEditControl Override
		public override void InitControl()
		{
			base.InitControl();
			pnNoOptionSets.Dock = DockStyle.Fill;
			pnOptionSets.Dock = DockStyle.Fill;
			LoadBarButtons();
			retractableBarControl.Collapse(true);
			_tabDragDropHelper = new XtraTabDragDropHelper<OptionsControl>(xtraTabControlOptionSets);
			_tabDragDropHelper.TabMoved += OnTabMoved;
			InitColorControls();
			BusinessObjects.Instance.OutputManager.ColorsChanged += (o, e) =>
			{
				InitColorControls();
				LoadBarButtons();
			};
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) => OnOuterThemeChanged();
			hyperLinkEditInfoContract.Enabled = BusinessObjects.Instance.OutputManager.ContractTemplateFolder.ExistsLocal();

			Controller.Instance.OptionsNew.Click += OnAddOptionsSetClick;
			Controller.Instance.OptionsProgramAdd.Click += OnAddProgramClick;
			Controller.Instance.OptionsProgramDelete.Click += OnDeleteProgramClick;
		}

		protected override void UpdateEditedContet()
		{
			_allowToSave = false;

			var quickLoad = EditedContent != null && !(ContentUpdateInfo.ChangeInfo.WholeScheduleChanged ||
				ContentUpdateInfo.ChangeInfo.ScheduleDatesChanged ||
				ContentUpdateInfo.ChangeInfo.CalendarTypeChanged ||
				ContentUpdateInfo.ChangeInfo.SpotTypeChanged);

			if (EditedContent != null)
				EditedContent.Dispose();
			EditedContent = Schedule
				.GetSchedulePartitionContent<OptionsContent>(SchedulePartitionType.Options)
				.Clone<OptionsContent, OptionsContent>();

			labelControlScheduleInfo.Text = String.Format("{0}{3}<color=gray><i>{1} ({2})</i></color>",
				ScheduleSettings.BusinessName,
				ScheduleSettings.FlightDates,
				String.Format("{0} {1}s", ScheduleSettings.TotalWeeks, "week"),
				Environment.NewLine);

			LoadOptionSets(quickLoad);

			_allowToSave = true;

			LoadActiveOptionSetData();
		}

		protected override void ApplyChanges()
		{
			foreach (var optionsControl in xtraTabControlOptionSets.TabPages.OfType<OptionsControl>())
				optionsControl.SaveData();
			Summary.SaveData();
			SaveColors();
		}

		protected override void SaveData()
		{
			Schedule.ApplySchedulePartitionContent(SchedulePartitionType.Options, EditedContent.Clone<OptionsContent, OptionsContent>());
		}

		public override void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("options");
		}

		protected override void LoadThemes()
		{
			FormThemeSelector.Link(Controller.Instance.OptionsTheme, BusinessObjects.Instance.ThemeManager.GetThemes(SlideType), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				IsThemeChanged = true;
			}));
			Controller.Instance.OptionsThemeBar.RecalcLayout();
			Controller.Instance.OptionsPanel.PerformLayout();
		}
		#endregion

		#region Option Sets Processing
		private void LoadOptionSets(bool quickLoad)
		{
			if (quickLoad)
			{
				Summary.LoadData(EditedContent.OptionsSummary);
			}
			else
			{
				xtraTabControlOptionSets.TabPages
					.OfType<IOptionsSlideControl>()
					.ToList()
					.ForEach(sc => sc.Release());
				xtraTabControlOptionSets.TabPages.Clear();
				xtraTabControlOptionSets.TabPages.Add(new OptionsSummaryControl(EditedContent.OptionsSummary));
				Summary.DataChanged += (o, e) =>
				{
					if (!_allowToSave) return;
					UpdateTotalsValues();
					SettingsNotSaved = true;
				};
			}
			foreach (var optionSet in EditedContent.Options.OrderBy(s => s.Index))
			{
				if (quickLoad)
				{
					var optionsControl = xtraTabControlOptionSets.TabPages.OfType<OptionsControl>().FirstOrDefault(osc => osc.Data.UniqueID.Equals(optionSet.UniqueID));
					if (optionsControl == null) continue;
					optionsControl.LoadData(optionSet);
				}
				else
					AddOptionControl(optionSet);
			}
			if (!quickLoad)
				xtraTabControlOptionSets.SelectedTabPageIndex = 0;
			UpdateOptionsSplash();
		}

		private void LoadActiveOptionSetData(bool activate = false)
		{
			_allowToSave = false;

			pnApplyForAll.Visible = EditedContent.Options.Count > 1 && ActiveOptionControl != null;
			checkEditApplyForAll.Checked = EditedContent.OptionsSummary.ApplySettingsForAll;

			if (ActiveOptionControl != null)
			{
				pnSummaryInfo.Visible = false;
				pnOptionSetInfo.Visible = true;
				buttonXOptionStation.Checked = ActiveOptionControl.Data.ShowStation;
				buttonXOptionProgram.Checked = ActiveOptionControl.Data.ShowProgram;
				buttonXOptionDay.Checked = ActiveOptionControl.Data.ShowDay;
				buttonXOptionTime.Checked = ActiveOptionControl.Data.ShowTime;
				buttonXOptionRate.Checked = ActiveOptionControl.Data.ShowRate;
				buttonXOptionLength.Checked = ActiveOptionControl.Data.ShowLenght;
				buttonXOptionLogo.Checked = ActiveOptionControl.Data.ShowLogo;
				buttonXOptionWeeklySpots.Checked = false;
				buttonXOptionMonthlySpots.Checked = false;
				buttonXOptionTotalSpots.Checked = false;
				if (ActiveOptionControl.Data.ShowSpots)
				{
					switch (ActiveOptionControl.Data.SpotType)
					{
						case SpotType.Week:
							buttonXOptionWeeklySpots.Checked = true;
							break;
						case SpotType.Month:
							buttonXOptionMonthlySpots.Checked = true;
							break;
						case SpotType.Total:
							buttonXOptionTotalSpots.Checked = true;
							break;
					}
				}
				buttonXOptionLineId.Checked = ActiveOptionControl.Data.ShowLineId;
				buttonXOptionCost.Checked = ActiveOptionControl.Data.ShowCost;
				buttonXOptionTallySpots.Checked = ActiveOptionControl.Data.ShowTotalSpots;
				buttonXOptionTallyCost.Checked = ActiveOptionControl.Data.ShowTotalCost;
				buttonXOptionAvgRate.Checked = ActiveOptionControl.Data.ShowAverageRate;
			}
			else if (ActiveSummary != null)
			{
				pnSummaryInfo.Visible = true;
				pnOptionSetInfo.Visible = false;

				buttonXSummaryLineId.Checked = ActiveSummary.Data.ShowLineId;
				buttonXSummaryCampaign.Checked = ActiveSummary.Data.ShowCampaign;
				buttonXSummaryComments.Checked = ActiveSummary.Data.ShowComments;
				buttonXSummaryLogo.Checked = ActiveSummary.Data.ShowLogo;
				buttonXSummaryTotalCost.Checked = ActiveSummary.Data.ShowTotalCost;
				buttonXSummaryTallySpots.Checked = ActiveSummary.Data.ShowTallySpots;
				buttonXSummaryTallyCost.Checked = ActiveSummary.Data.ShowTallyCost;

				switch (ActiveSummary.Data.SpotType)
				{
					case SpotType.Week:
						buttonXSummaryWeeklySpots.Enabled = true;
						buttonXSummaryWeeklyCost.Enabled = true;
						buttonXSummaryTotalWeeks.Enabled = true;
						buttonXSummaryWeeklySpots.Checked = ActiveSummary.Data.ShowSpots;
						buttonXSummaryWeeklyCost.Checked = ActiveSummary.Data.ShowCost;
						buttonXSummaryTotalWeeks.Checked = ActiveSummary.Data.ShowTotalPeriods;

						buttonXSummaryMonthlySpots.Enabled = false;
						buttonXSummaryMonthlyCost.Enabled = false;
						buttonXSummaryTotalMonths.Enabled = false;
						buttonXSummaryTotalSpots.Enabled = false;

						buttonXSummaryMonthlySpots.Checked = false;
						buttonXSummaryMonthlyCost.Checked = false;
						buttonXSummaryTotalMonths.Checked = false;
						buttonXSummaryTotalSpots.Checked = false;
						break;
					case SpotType.Month:
						buttonXSummaryMonthlySpots.Enabled = true;
						buttonXSummaryMonthlyCost.Enabled = true;
						buttonXSummaryTotalMonths.Enabled = true;
						buttonXSummaryMonthlySpots.Checked = ActiveSummary.Data.ShowSpots;
						buttonXSummaryMonthlyCost.Checked = ActiveSummary.Data.ShowCost;
						buttonXSummaryTotalMonths.Checked = ActiveSummary.Data.ShowTotalPeriods;

						buttonXSummaryWeeklySpots.Enabled = false;
						buttonXSummaryWeeklyCost.Enabled = false;
						buttonXSummaryTotalWeeks.Enabled = false;
						buttonXSummaryTotalSpots.Enabled = false;

						buttonXSummaryWeeklySpots.Checked = false;
						buttonXSummaryWeeklyCost.Checked = false;
						buttonXSummaryTotalWeeks.Checked = false;
						buttonXSummaryTotalSpots.Checked = false;
						break;
					case SpotType.Total:
						buttonXSummaryTotalSpots.Enabled = true;
						buttonXSummaryTotalSpots.Checked = ActiveSummary.Data.ShowSpots;

						buttonXSummaryWeeklySpots.Enabled = false;
						buttonXSummaryWeeklyCost.Enabled = false;
						buttonXSummaryTotalWeeks.Enabled = false;
						buttonXSummaryMonthlySpots.Enabled = false;
						buttonXSummaryMonthlyCost.Enabled = false;
						buttonXSummaryTotalMonths.Enabled = false;

						buttonXSummaryWeeklySpots.Checked = false;
						buttonXSummaryWeeklyCost.Checked = false;
						buttonXSummaryTotalWeeks.Checked = false;
						buttonXSummaryMonthlySpots.Checked = false;
						buttonXSummaryMonthlyCost.Checked = false;
						buttonXSummaryTotalMonths.Checked = false;
						break;
				}

				if (activate)
					ActiveSummary.UpdateView(true);
			}
			else
			{
				pnSummaryInfo.Visible = false;
				pnOptionSetInfo.Visible = false;
			}
			UpdateTotalsValues();
			UpdateTotalsVisibility();
			UpdateOutputStatus();
			_allowToSave = true;
		}

		private void ApplySharedSettings(OptionsControl templateControl)
		{
			foreach (var optionsControl in xtraTabControlOptionSets.TabPages.OfType<OptionsControl>().Where(oc => oc.Data.UniqueID != templateControl.Data.UniqueID))
			{
				optionsControl.Data.ShowStation = templateControl.Data.ShowStation;
				optionsControl.Data.ShowProgram = templateControl.Data.ShowProgram;
				optionsControl.Data.ShowDay = templateControl.Data.ShowDay;
				optionsControl.Data.ShowTime = templateControl.Data.ShowTime;
				optionsControl.Data.ShowRate = templateControl.Data.ShowRate;
				optionsControl.Data.ShowLenght = templateControl.Data.ShowLenght;
				optionsControl.Data.ShowLogo = templateControl.Data.ShowLogo;
				optionsControl.Data.ShowSpots = templateControl.Data.ShowSpots;
				optionsControl.Data.ShowLineId = templateControl.Data.ShowLineId;
				optionsControl.Data.ShowCost = templateControl.Data.ShowCost;
				optionsControl.Data.ShowTotalSpots = templateControl.Data.ShowTotalSpots;
				optionsControl.Data.ShowTotalCost = templateControl.Data.ShowTotalCost;
				optionsControl.Data.ShowAverageRate = templateControl.Data.ShowAverageRate;
				optionsControl.Data.UseDecimalRates = templateControl.Data.UseDecimalRates;
				optionsControl.Data.CloneLineToTheEnd = templateControl.Data.CloneLineToTheEnd;
				optionsControl.Data.ShowSpotsX = templateControl.Data.ShowSpotsX;
				optionsControl.Data.SpotType = templateControl.Data.SpotType;

				optionsControl.Data.PositionStation = templateControl.Data.PositionStation;
				optionsControl.Data.PositionProgram = templateControl.Data.PositionProgram;
				optionsControl.Data.PositionDay = templateControl.Data.PositionDay;
				optionsControl.Data.PositionTime = templateControl.Data.PositionTime;
				optionsControl.Data.PositionRate = templateControl.Data.PositionRate;
				optionsControl.Data.PositionLenght = templateControl.Data.PositionLenght;
				optionsControl.Data.PositionSpots = templateControl.Data.PositionSpots;
				optionsControl.Data.PositionCost = templateControl.Data.PositionCost;
			}
		}

		private void ApplySharedContractSettings(ContractSettings templateSettings)
		{
			foreach (var optionsSlide in xtraTabControlOptionSets.TabPages.OfType<IOptionsSlideControl>())
			{
				optionsSlide.ContractSettings.ShowSignatureLine = templateSettings.ShowSignatureLine;
				optionsSlide.ContractSettings.ShowDisclaimer = templateSettings.ShowDisclaimer;
				optionsSlide.ContractSettings.RateExpirationDate = templateSettings.RateExpirationDate;
			}
		}

		private void AddOptionSet()
		{
			using (var form = new FormOptionSetName())
			{
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				var optionSet = new OptionSet(EditedContent);
				optionSet.Name = form.OptionSetName;
				EditedContent.Options.Add(optionSet);
				EditedContent.RebuildOptionSetIndexes();
				var optionControl = AddOptionControl(optionSet);
				xtraTabControlOptionSets.SelectedTabPage = optionControl;
				Summary.UpdateView();
			}
		}

		private void CloneOptionSet(OptionsControl optionControl)
		{
			using (var form = new FormOptionSetName())
			{
				form.OptionSetName = String.Format("{0} (Clone)", optionControl.Data.Name);
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				var optionSet = optionControl.Data.Clone<OptionSet, OptionSet>();
				optionSet.Name = form.OptionSetName;
				optionSet.Index += 0.5;
				EditedContent.Options.Add(optionSet);
				EditedContent.RebuildOptionSetIndexes();
				var newControl = AddOptionControl(optionSet, (Int32)optionSet.Index);
				xtraTabControlOptionSets.SelectedTabPage = newControl;
				Summary.UpdateView();
			}
		}

		private void DeleteOptionSet(OptionsControl optionControl)
		{
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Are you sure want to delete {0}?", optionControl.Data.Name) != DialogResult.Yes) return;
			EditedContent.Options.Remove(optionControl.Data);
			EditedContent.RebuildOptionSetIndexes();
			xtraTabControlOptionSets.TabPages.Remove(optionControl);
			Summary.UpdateView();
		}

		private void RenameOptionSet(OptionsControl optionControl)
		{
			if (optionControl == null) return;
			using (var form = new FormOptionSetName())
			{
				form.OptionSetName = optionControl.Data.Name;
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				optionControl.Data.Name = form.OptionSetName;
				optionControl.Text = form.OptionSetName;
				Summary.UpdateView();
			}
		}

		private OptionsControl AddOptionControl(OptionSet data, int position = -1)
		{
			var optionControl = new OptionsControl(data);
			optionControl.DataChanged += (o, e) =>
			{
				var sourceControl = o as OptionsControl;
				if (sourceControl == null) return;
				if (!_allowToSave) return;
				if (EditedContent.OptionsSummary.ApplySettingsForAll)
				{
					ApplySharedSettings(sourceControl);
					xtraTabControlOptionSets.TabPages.OfType<OptionsControl>().Where(oc => oc.Data.UniqueID != sourceControl.Data.UniqueID).ToList().ForEach(oc => oc.UpdateView());
				}
				UpdateTotalsValues();
				UpdateOutputStatus();
				Summary.UpdateView();
				SettingsNotSaved = true;
			};
			position = position == -1 ? xtraTabControlOptionSets.TabPages.OfType<OptionsControl>().Count() : position;
			xtraTabControlOptionSets.TabPages.Insert(position, optionControl);
			return optionControl;
		}

		private void UpdateTotalsValues()
		{
			if (ActiveOptionControl != null && ActiveOptionControl.Data.Programs.Any())
			{
				pnBottom.Visible = true;
				switch (ActiveOptionControl.Data.SpotType)
				{
					case SpotType.Week:
						laTotalSpotsTitle.Text = "Weekly Spots";
						laTotalCostTitle.Text = "Weekly Cost";
						break;
					case SpotType.Month:
						laTotalSpotsTitle.Text = "Monthly Spots";
						laTotalCostTitle.Text = "Monthly Cost";
						break;
					case SpotType.Total:
						laTotalSpotsTitle.Text = "Total Spots";
						laTotalCostTitle.Text = "Total Cost";
						break;
				}
				laTotalSpotsValue.Text = ActiveOptionControl.Data.TotalSpots.ToString("#,##0");
				laTotalCostValue.Text = ActiveOptionControl.Data.TotalCost.ToString(ActiveOptionControl.Data.UseDecimalRates ? "$#,##0.00" : "$#,##0");
				laAvgRateValue.Text = ActiveOptionControl.Data.AvgRate.ToString(ActiveOptionControl.Data.UseDecimalRates ? "$#,##0.00" : "$#,##0");
			}
			else if (ActiveSummary != null)
			{
				pnBottom.Visible = true;
				laTotalSpotsTitle.Text = "Total Spots";
				laTotalCostTitle.Text = "Total Cost";
				laTotalSpotsValue.Text = ActiveSummary.Data.TotalSpots.ToString("#,##0");
				laTotalCostValue.Text = ActiveSummary.Data.TotalCost.ToString(ActiveSummary.Data.UseDecimalRates ? "$#,##0.00" : "$#,##0");
				laAvgRateValue.Text = String.Empty;
			}
			else
			{
				pnBottom.Visible = false;
				laTotalSpotsValue.Text = laAvgRateValue.Text = String.Empty;
			}
		}

		private void UpdateTotalsVisibility()
		{

			if (ActiveOptionControl != null)
			{
				pnTotalSpots.Visible = ActiveOptionControl.Data.ShowTotalSpots;
				pnTotalSpots.BringToFront();
				pnTotalCost.Visible = ActiveOptionControl.Data.ShowTotalCost;
				pnTotalCost.BringToFront();
				pnAvgRate.Visible = ActiveOptionControl.Data.ShowAverageRate;
				pnAvgRate.BringToFront();
			}
			else if (ActiveSummary != null)
			{
				pnTotalSpots.Visible = ActiveSummary.Data.ShowTallySpots;
				pnTotalSpots.SendToBack();
				pnTotalCost.Visible = ActiveSummary.Data.ShowTallyCost;
				pnTotalCost.BringToFront();
				pnAvgRate.Visible = false;
			}
			else
			{
				pnTotalSpots.Visible = false;
				pnTotalCost.Visible = false;
				pnAvgRate.Visible = false;
			}
		}

		private void UpdateOptionsSplash()
		{
			if (EditedContent.Options.Any())
			{
				pnOptionSets.BringToFront();
				Controller.Instance.OptionsProgramAdd.Enabled = true;
				Controller.Instance.OptionsProgramDelete.Enabled = true;
			}
			else
			{
				pnNoOptionSets.BringToFront();
				Controller.Instance.OptionsProgramAdd.Enabled = false;
				Controller.Instance.OptionsProgramDelete.Enabled = false;
			}
		}

		private void InitColorControls()
		{
			xtraTabPageOptionsStyle.PageVisible = BusinessObjects.Instance.OutputManager.OptionsColors.Items.Count > 1;
			outputColorSelector.InitData(BusinessObjects.Instance.OutputManager.OptionsColors, MediaMetaData.Instance.SettingsManager.SelectedColor);
			outputColorSelector.ColorChanged += OnColorChanged;
		}

		private void LoadBarButtons()
		{
			var buttonInfos = new List<ButtonInfo>();
			buttonInfos.Add(new ButtonInfo { Logo = Resources.SectionSettingsInfo, Tooltip = "Open Schedule Info", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsInfo; } });
			if (BusinessObjects.Instance.OutputManager.OptionsColors.Items.Count > 1)
				buttonInfos.Add(new ButtonInfo { Logo = Resources.SectionSettingsOptions, Tooltip = "Open Options", Action = () => { xtraTabControlOptions.SelectedTabPage = xtraTabPageOptionsStyle; } });
			retractableBarControl.AddButtons(buttonInfos);
		}

		private void SaveColors()
		{
			MediaMetaData.Instance.SettingsManager.SelectedColor = outputColorSelector.SelectedColor ?? String.Empty;
			MediaMetaData.Instance.SettingsManager.SaveSettings();
		}
		#endregion

		#region Ribbon Button Clicks
		private void OnAddOptionsSetClick(object sender, EventArgs e)
		{
			AddOptionSet();
			UpdateOptionsSplash();
			SettingsNotSaved = true;
		}

		private void OnAddProgramClick(object sender, EventArgs e)
		{
			if (ActiveOptionControl == null) return;
			ActiveOptionControl.AddProgram();
		}

		private void OnDeleteProgramClick(object sender, EventArgs e)
		{
			if (ActiveOptionControl == null) return;
			ActiveOptionControl.DeleteProgram();
		}
		#endregion

		#region Options Controls Event Handlers
		private void xtraTabControlOptionSets_CloseButtonClick(object sender, EventArgs e)
		{
			var arg = (ClosePageButtonEventArgs)e;
			var optionControl = arg.Page as OptionsControl;
			if (optionControl == null) return;
			DeleteOptionSet(optionControl);
			UpdateOptionsSplash();
			SettingsNotSaved = true;
		}

		private void xtraTabControlOptionSets_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if (!_allowToSave) return;
			LoadActiveOptionSetData(true);
		}

		private void xtraTabControlOptionSets_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			_menuHitInfo = xtraTabControlOptionSets.CalcHitInfo(new Point(e.X, e.Y));
			if (_menuHitInfo.HitTest != XtraTabHitTest.PageHeader) return;
			if (!(_menuHitInfo.Page is OptionsControl)) return;
			contextMenuStripOptions.Show((Control)sender, e.Location);
		}

		private void OnTabMoved(object sender, TabMoveEventArgs e)
		{
			EditedContent.ChangeOptionSetPosition(
				EditedContent.Options.IndexOf(((OptionsControl)e.MovedPage).Data),
				EditedContent.Options.IndexOf(((OptionsControl)e.TargetPage).Data) + (1 * e.Offset));
			Summary.UpdateView();
			SettingsNotSaved = true;
		}

		private void toolStripMenuItemOptionSetsRename_Click(object sender, EventArgs e)
		{
			RenameOptionSet(_menuHitInfo.Page as OptionsControl);
			SettingsNotSaved = true;
		}

		private void toolStripMenuItemOptionSetsClone_Click(object sender, EventArgs e)
		{
			CloneOptionSet(_menuHitInfo.Page as OptionsControl);
			SettingsNotSaved = true;
		}

		private void OnSpotsClick(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (button.Checked)
			{
				button.Checked = false;
				return;
			}
			_allowToSave = false;
			buttonXOptionWeeklySpots.Checked = false;
			buttonXOptionMonthlySpots.Checked = false;
			buttonXOptionTotalSpots.Checked = false;
			_allowToSave = true;
			button.Checked = true;
			UpdateTotalsValues();
		}

		private void hyperLinkEditInfoAdvanced_OpenLink(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			using (var form = new FormOutputSettings())
			{
				if (ActiveOptionControl != null)
				{
					form.checkEditUseDecimalRate.Checked = ActiveOptionControl.Data.UseDecimalRates;
					form.checkEditShowSpotX.Checked = ActiveOptionControl.Data.ShowSpotsX;

					form.checkEditCloneLineToTheEnd.Enabled = true;
					form.labelControlDescriptionCloneLineToTheEnd.Enabled = true;
					form.checkEditCloneLineToTheEnd.Checked = ActiveOptionControl.Data.CloneLineToTheEnd;
				}
				else if (ActiveSummary != null)
				{
					form.checkEditUseDecimalRate.Checked = ActiveSummary.Data.UseDecimalRates;
					form.checkEditShowSpotX.Checked = ActiveSummary.Data.ShowSpotsX;

					form.checkEditCloneLineToTheEnd.Enabled = false;
					form.labelControlDescriptionCloneLineToTheEnd.Enabled = false;
					form.checkEditCloneLineToTheEnd.Checked = false;
				}
				else
					return;
				form.checkEditLockToMaster.Checked = MediaMetaData.Instance.SettingsManager.UseSlideMaster;
				if (form.ShowDialog() != DialogResult.OK) return;
				if (ActiveOptionControl != null)
				{
					ActiveOptionControl.Data.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
					ActiveOptionControl.Data.ShowSpotsX = form.checkEditShowSpotX.Checked;
					ActiveOptionControl.Data.CloneLineToTheEnd = form.checkEditCloneLineToTheEnd.Checked;
					if (EditedContent.OptionsSummary.ApplySettingsForAll)
					{
						ApplySharedSettings(ActiveOptionControl);
						xtraTabControlOptionSets.TabPages.OfType<OptionsControl>().ToList().ForEach(oc => oc.UpdateView());
					}
					else
						ActiveOptionControl.UpdateView();
				}
				else if (ActiveSummary != null)
				{
					ActiveSummary.Data.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
					ActiveSummary.Data.ShowSpotsX = form.checkEditShowSpotX.Checked;
					ActiveSummary.UpdateView();
				}
				MediaMetaData.Instance.SettingsManager.UseSlideMaster = form.checkEditLockToMaster.Checked;
				UpdateTotalsValues();
				SettingsNotSaved = true;
			}
		}

		private void hyperLinkEditInfoContract_OpenLink(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			using (var form = new FormContractSettings())
			{
				if (ActiveOptionControl != null)
				{
					form.checkEditShowSignatureLine.Checked = ActiveOptionControl.ContractSettings.ShowSignatureLine;
					form.checkEditShowRatesExpiration.Checked = ActiveOptionControl.ContractSettings.RateExpirationDate.HasValue;
					form.checkEditShowDisclaimer.Checked = ActiveOptionControl.ContractSettings.ShowDisclaimer;
					form.dateEditRatesExpirationDate.EditValue = ActiveOptionControl.ContractSettings.RateExpirationDate;
				}
				else if (ActiveSummary != null)
				{
					form.checkEditShowSignatureLine.Checked = ActiveSummary.ContractSettings.ShowSignatureLine;
					form.checkEditShowRatesExpiration.Checked = ActiveSummary.ContractSettings.RateExpirationDate.HasValue;
					form.checkEditShowDisclaimer.Checked = ActiveSummary.ContractSettings.ShowDisclaimer;
					form.dateEditRatesExpirationDate.EditValue = ActiveSummary.ContractSettings.RateExpirationDate;
				}
				else
					return;
				if (form.ShowDialog() != DialogResult.OK) return;
				if (ActiveOptionControl != null)
				{
					ActiveOptionControl.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
					ActiveOptionControl.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
					ActiveOptionControl.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;
					if (EditedContent.OptionsSummary.ApplySettingsForAll)
						ApplySharedContractSettings(ActiveOptionControl.ContractSettings);
				}
				else if (ActiveSummary != null)
				{
					ActiveSummary.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
					ActiveSummary.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
					ActiveSummary.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;

					if (EditedContent.OptionsSummary.ApplySettingsForAll)
						ApplySharedContractSettings(ActiveSummary.ContractSettings);
				}
				SettingsNotSaved = true;
			}
		}

		private void OnColorChanged(object sender, EventArgs e)
		{
			SettingsNotSaved = true;
		}

		private void OnInfoSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			EditedContent.OptionsSummary.ApplySettingsForAll = checkEditApplyForAll.Checked;

			if (ActiveOptionControl != null)
			{
				ActiveOptionControl.Data.ShowStation = buttonXOptionStation.Checked;
				ActiveOptionControl.Data.ShowProgram = buttonXOptionProgram.Checked;
				ActiveOptionControl.Data.ShowDay = buttonXOptionDay.Checked;
				ActiveOptionControl.Data.ShowTime = buttonXOptionTime.Checked;
				ActiveOptionControl.Data.ShowRate = buttonXOptionRate.Checked;
				ActiveOptionControl.Data.ShowLenght = buttonXOptionLength.Checked;
				ActiveOptionControl.Data.ShowLogo = buttonXOptionLogo.Checked;
				ActiveOptionControl.Data.ShowSpots = buttonXOptionWeeklySpots.Checked || buttonXOptionMonthlySpots.Checked || buttonXOptionTotalSpots.Checked;
				ActiveOptionControl.Data.ShowLineId = buttonXOptionLineId.Checked;
				ActiveOptionControl.Data.ShowCost = buttonXOptionCost.Checked;
				ActiveOptionControl.Data.ShowTotalSpots = buttonXOptionTallySpots.Checked;
				ActiveOptionControl.Data.ShowTotalCost = buttonXOptionTallyCost.Checked;
				ActiveOptionControl.Data.ShowAverageRate = buttonXOptionAvgRate.Checked;
				if (buttonXOptionWeeklySpots.Checked)
					ActiveOptionControl.Data.SpotType = SpotType.Week;
				else if (buttonXOptionMonthlySpots.Checked)
					ActiveOptionControl.Data.SpotType = SpotType.Month;
				else if (buttonXOptionTotalSpots.Checked)
					ActiveOptionControl.Data.SpotType = SpotType.Total;

				if (EditedContent.OptionsSummary.ApplySettingsForAll)
				{
					ApplySharedSettings(ActiveOptionControl);
					xtraTabControlOptionSets.TabPages.OfType<OptionsControl>().ToList().ForEach(oc => oc.UpdateView());
				}
				else
					ActiveOptionControl.UpdateView();
				UpdateOutputStatus();
			}
			else if (ActiveSummary != null)
			{
				ActiveSummary.Data.ShowLineId = buttonXSummaryLineId.Checked;
				ActiveSummary.Data.ShowCampaign = buttonXSummaryCampaign.Checked;
				ActiveSummary.Data.ShowComments = buttonXSummaryComments.Checked;
				ActiveSummary.Data.ShowSpots = buttonXSummaryWeeklySpots.Checked || buttonXSummaryMonthlySpots.Checked || buttonXSummaryTotalSpots.Checked;
				ActiveSummary.Data.ShowCost = buttonXSummaryWeeklyCost.Checked || buttonXSummaryMonthlyCost.Checked;
				ActiveSummary.Data.ShowLogo = buttonXSummaryLogo.Checked;
				ActiveSummary.Data.ShowTotalPeriods = buttonXSummaryTotalWeeks.Checked || buttonXSummaryTotalMonths.Checked;
				ActiveSummary.Data.ShowTotalCost = buttonXSummaryTotalCost.Checked;
				ActiveSummary.Data.ShowTallySpots = buttonXSummaryTallySpots.Checked;
				ActiveSummary.Data.ShowTallyCost = buttonXSummaryTallyCost.Checked;
			}
			Summary.UpdateView();
			UpdateTotalsVisibility();
			UpdateTotalsValues();
			SettingsNotSaved = true;
		}
		#endregion

		#region Output
		private Theme SelectedTheme
		{
			get { return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType).FirstOrDefault(t => t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType)) || String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType))); }
		}

		private void UpdateOutputStatus()
		{
			Controller.Instance.OptionsPowerPoint.Enabled =
				Controller.Instance.OptionsPdf.Enabled =
				Controller.Instance.OptionsPreview.Enabled =
				Controller.Instance.OptionsEmail.Enabled = xtraTabControlOptionSets.TabPages.OfType<IOptionsSlideControl>().Any(ss => ss.ReadyForOutput);
		}

		public override void OutputPowerPoint()
		{
			var tabPages = xtraTabControlOptionSets.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<IOptionsSlideControl>().Where(ss => ss.ReadyForOutput);
			var optionsSlides = new List<IOptionsSlideControl>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Options";
					var currentOptionControl = xtraTabControlOptionSets.SelectedTabPage as IOptionsSlideControl;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentOptionControl)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						optionsSlides.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<IOptionsSlideControl>());
				}
			else
				optionsSlides.AddRange(tabPages);
			if (!optionsSlides.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				foreach (var optionsSlide in optionsSlides)
					optionsSlide.Output(SelectedTheme);
				FormProgress.CloseProgress();
			});
		}

		public override void OutputPdf()
		{
			var tabPages = xtraTabControlOptionSets.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<IOptionsSlideControl>().Where(ss => ss.ReadyForOutput);
			var optionsSlides = new List<IOptionsSlideControl>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Options";
					var currentOptionControl = xtraTabControlOptionSets.SelectedTabPage as IOptionsSlideControl;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentOptionControl)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						optionsSlides.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<IOptionsSlideControl>());
				}
			else
				optionsSlides.AddRange(tabPages);
			if (!optionsSlides.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var previewGroups = new List<PreviewGroup>();
				previewGroups.AddRange(optionsSlides.Select(optionsSlide => optionsSlide.GetPreviewGroup(SelectedTheme)));
				var pdfFileName = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					String.Format("{0}-{1}.pdf", Schedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				RegularMediaSchedulePowerPointHelper.Instance.BuildPdf(pdfFileName, previewGroups.Select(pg => pg.PresentationSourcePath));
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }
				FormProgress.CloseProgress();
			});
		}

		public override void Preview()
		{
			var tabPages = xtraTabControlOptionSets.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<IOptionsSlideControl>().Where(ss => ss.ReadyForOutput);
			var optionsSlides = new List<IOptionsSlideControl>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Options";
					var currentOptionControl = xtraTabControlOptionSets.SelectedTabPage as IOptionsSlideControl;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentOptionControl)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						optionsSlides.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<IOptionsSlideControl>());
				}
			else
				optionsSlides.AddRange(tabPages);
			if (!optionsSlides.Any()) return;
			var previewGroups = new List<PreviewGroup>();

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			previewGroups.AddRange(optionsSlides.Select(optionsSlide => optionsSlide.GetPreviewGroup(SelectedTheme)));
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater))
			{
				formPreview.Text = "Preview Options";
				formPreview.LoadGroups(previewGroups);
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			}
		}

		public override void Email()
		{
			var tabPages = xtraTabControlOptionSets.TabPages.Where(tabPage => tabPage.PageEnabled).OfType<IOptionsSlideControl>().Where(ss => ss.ReadyForOutput);
			var optionsSlides = new List<IOptionsSlideControl>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Options";
					var currentOptionControl = xtraTabControlOptionSets.SelectedTabPage as IOptionsSlideControl;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SlideName);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentOptionControl)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						optionsSlides.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<IOptionsSlideControl>());
				}
			else
				optionsSlides.AddRange(tabPages);
			if (!optionsSlides.Any()) return;
			var previewGroups = new List<PreviewGroup>();

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			previewGroups.AddRange(optionsSlides.Select(optionsSlide => optionsSlide.GetPreviewGroup(SelectedTheme)));
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
			{
				formEmail.Text = "Email these Options";
				formEmail.LoadGroups(previewGroups);
				Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}
		}
		#endregion
	}
}
