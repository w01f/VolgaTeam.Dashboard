using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Media.Enums;
using Asa.Business.Online.Dictionaries;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Themes;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.Output;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings;
using Asa.Schedules.Common.Controls.ContentEditors.Controls;
using Asa.Schedules.Common.Controls.ContentEditors.Helpers;
using DevComponents.DotNetBar;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	[ToolboxItem(false)]
	public partial class ScheduleContainer : BasePartitionEditControl<ProgramScheduleContent, MediaSchedule, MediaScheduleSettings, MediaScheduleChangeInfo>
	//public partial class ScheduleContainer : UserControl
	{
		private bool _allowToSave;
		private XtraTabHitInfo _menuHitInfo;
		private XtraTabDragDropHelper<SectionContainer> _tabDragDropHelper;

		#region Properties
		private MediaSchedule Schedule => BusinessObjects.Instance.ScheduleManager.ActiveSchedule;

		private MediaScheduleSettings ScheduleSettings => Schedule.Settings;

		public override string Identifier => ContentIdentifiers.ProgramSchedule;

		public override RibbonTabItem TabPage => Controller.Instance.TabProgramSchedule;

		private SectionContainer ActiveSection => xtraTabControlSections.SelectedTabPage as SectionContainer;

		private string SpotTitle => ScheduleSettings.SelectedSpotType.ToString();
		#endregion

		public ScheduleContainer()
		{
			InitializeComponent();
		}

		#region BaseContentEditControl Override
		public override void InitControl()
		{
			base.InitControl();

			pictureEditDefaulLogo.Image = BusinessObjects.Instance.ImageResourcesManager.ProgramScheduleNoRecordsLogo ?? pictureEditDefaulLogo.Image;

			settingsContainer.InitControl();
			settingsContainer.SettingsChanged += OnSectionSettingsChanged;
			settingsContainer.SettingsControlsUpdated += OnSettingsControlsUpdated;

			retractableBarControl.ContentSize = retractableBarControl.Width;
			retractableBarControl.Collapse(true);

			_tabDragDropHelper = new XtraTabDragDropHelper<SectionContainer>(xtraTabControlSections);
			_tabDragDropHelper.TabMoved += OnTabMoved;

			BusinessObjects.Instance.OutputManager.ColorCollectionChanged += OnSettingsControlsUpdated;

			Controller.Instance.ProgramScheduleNew.Click += OnAddSection;
			Controller.Instance.ProgramScheduleProgramAdd.Click += OnAddItem;
			Controller.Instance.ProgramScheduleProgramDelete.Click += OnDeleteItem;
		}

		protected override void UpdateEditedContet()
		{
			_allowToSave = false;

			var quickLoad = EditedContent != null && !(ChangeInfo.WholeScheduleChanged ||
				ContentUpdateInfo.ChangeInfo.ScheduleDatesChanged ||
				ContentUpdateInfo.ChangeInfo.CalendarTypeChanged ||
				ContentUpdateInfo.ChangeInfo.SpotTypeChanged);

			EditedContent?.Dispose();

			EditedContent = Schedule.ProgramSchedule.Clone<ProgramScheduleContent, ProgramScheduleContent>();

			if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed &&
				(ScheduleSettings.FlightDateStart != ScheduleSettings.UserFlightDateStart ||
					ScheduleSettings.FlightDateEnd != ScheduleSettings.UserFlightDateEnd))
				layoutControlGroupFlexFlightDatesWarning.Visibility = LayoutVisibility.Always;
			else
				layoutControlGroupFlexFlightDatesWarning.Visibility = LayoutVisibility.Never;

			settingsContainer.LoadContent(EditedContent);

			LoadSections(quickLoad);

			LoadActiveSectionData();

			_allowToSave = true;
		}

		protected override void ApplyChanges()
		{
			xtraTabControlSections.TabPages
				.OfType<SectionContainer>()
				.ToList()
				.ForEach(sectionControl => sectionControl.SaveData());

			ChangeInfo.ProgramScheduleChanged = ChangeInfo.ProgramScheduleChanged || SettingsNotSaved;
		}

		protected override void SaveData()
		{
			Schedule.ProgramSchedule = EditedContent.Clone<ProgramScheduleContent, ProgramScheduleContent>();
		}

		public override void GetHelp()
		{
			var helpKey = String.Format("{0}ly", SpotTitle.ToLower());
			if (ActiveSection != null && ActiveSection.ActiveEditor.EditorType != SectionEditorType.Schedule)
				switch (ActiveSection.ActiveEditor.EditorType)
				{
					case SectionEditorType.CustomSummary:
						helpKey = "summary2";
						break;
				}
			BusinessObjects.Instance.HelpManager.OpenHelpLink(helpKey);
		}

		protected override void LoadThemes()
		{
			base.LoadThemes();

			FormThemeSelector.Link(
				Controller.Instance.ProgramScheduleTheme,
				Controller.Instance.FormMain,
				BusinessObjects.Instance.ThemeManager.GetThemes(SlideType),
				MediaMetaData.Instance.SettingsManager.GetSelectedThemeName(SlideType),
				MediaMetaData.Instance.SettingsManager,
				(theme, applyForAllSlideTypes) =>
				{
					MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType, theme.Name, applyForAllSlideTypes);
					MediaMetaData.Instance.SettingsManager.SaveSettings();
					if (applyForAllSlideTypes)
						Controller.Instance.ContentController.RaiseThemeChanged();
				}
			);
			Controller.Instance.ProgramScheduleThemeBar.RecalcLayout();
			Controller.Instance.ProgramSchedulePanel.PerformLayout();
		}

		protected override void UpdateMenuOutputButtons()
		{
			UpdateOutputStatus();
		}
		#endregion

		#region Sections Management
		private void LoadSections(bool quickLoad)
		{
			if (!quickLoad)
			{
				xtraTabControlSections.TabPages
					.OfType<SectionContainer>()
					.ToList()
					.ForEach(sc => sc.Release());
				xtraTabControlSections.TabPages.Clear();
			}
			foreach (var section in EditedContent.Sections.OrderBy(s => s.Index))
			{
				if (quickLoad)
				{
					var sectionTabControl = xtraTabControlSections.TabPages
						.OfType<SectionContainer>()
						.FirstOrDefault(sc => sc.SectionData.UniqueID.Equals(section.UniqueID));
					if (sectionTabControl == null) continue;
					sectionTabControl.LoadData(section, quickLoad);
				}
				else
					AddSectionControl(section);
			}
			if (!quickLoad)
				xtraTabControlSections.SelectedTabPageIndex = 0;
			UpdateSectionSplash();
		}

		private void LoadActiveSectionData()
		{
			_allowToSave = false;

			if (ActiveSection != null)
			{
				settingsContainer.LoadSection(ActiveSection.SectionData);
				UpdateStatusBar();
				OnSectionEditorChanged(ActiveSection, EventArgs.Empty);
			}
			UpdateOutputStatus();
			_allowToSave = true;
		}

		private SectionContainer AddSectionControl(ScheduleSection sectionData, int position = -1)
		{
			var sectionTabControl = new SectionContainer();
			sectionTabControl.InitControls();
			sectionTabControl.LoadData(sectionData);
			sectionTabControl.DataChanged += OnSectionDataChanged;
			sectionTabControl.SectionEditorChanged += OnSectionEditorChanged;
			position = position == -1 ? xtraTabControlSections.TabPages.OfType<SectionContainer>().Count() : position;
			xtraTabControlSections.TabPages.Insert(position, sectionTabControl);
			return sectionTabControl;
		}

		private void AddSection()
		{
			using (var form = new FormSectionName())
			{
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				var section = EditedContent.CreateSection();
				section.Name = form.SectionName;
				EditedContent.Sections.Add(section);
				EditedContent.RebuildSectionIndexes();
				var sectionControl = AddSectionControl(section);
				xtraTabControlSections.SelectedTabPage = sectionControl;
				settingsContainer.UpdateSettingsAccordingDataChanges(SectionEditorType.Schedule);
				UpdateSectionSplash();
				SettingsNotSaved = true;
			}
		}

		private void CloneSection(SectionContainer sectionControl)
		{
			using (var form = new FormSectionName())
			{
				form.SectionName = String.Format("{0} (Clone)", sectionControl.SectionData.Name);
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				var section = sectionControl.SectionData.Clone<ScheduleSection, ScheduleSection>();
				section.Name = form.SectionName;
				section.Index += 0.5;
				EditedContent.Sections.Add(section);
				EditedContent.RebuildSectionIndexes();
				var newControl = AddSectionControl(section, (Int32)section.Index);
				xtraTabControlSections.SelectedTabPage = newControl;
				settingsContainer.UpdateSettingsAccordingDataChanges(SectionEditorType.Schedule);
				SettingsNotSaved = true;
			}
		}

		private void DeleteSection(SectionContainer sectionControl)
		{
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Are you sure want to delete {0}?", sectionControl.SectionData.Name) != DialogResult.Yes) return;
			EditedContent.Sections.Remove(sectionControl.SectionData);
			EditedContent.RebuildSectionIndexes();
			xtraTabControlSections.TabPages.Remove(sectionControl);
			UpdateSectionSplash();
			settingsContainer.UpdateSettingsAccordingDataChanges(SectionEditorType.Schedule);
			SettingsNotSaved = true;
		}

		private void RenameSection(SectionContainer snapshotControl)
		{
			if (snapshotControl == null) return;
			using (var form = new FormSectionName())
			{
				form.SectionName = snapshotControl.SectionData.Name;
				if (form.ShowDialog(Controller.Instance.FormMain) != DialogResult.OK) return;
				snapshotControl.SectionData.Name = form.SectionName;
				snapshotControl.Text = form.SectionName;
				settingsContainer.UpdateSettingsAccordingDataChanges(SectionEditorType.Schedule);
				SettingsNotSaved = true;
			}
		}

		private void UpdateOutputStatus()
		{
			Controller.Instance.ProgramSchedulePowerPoint.Enabled =
				Controller.Instance.MenuOutputPdfButton.Enabled =
				Controller.Instance.ProgramSchedulePreview.Enabled =
				Controller.Instance.MenuEmailButton.Enabled = xtraTabControlSections.TabPages
				.OfType<SectionContainer>()
				.Any(sectionTabControl => sectionTabControl.ReadyForOutput);
		}

		private void OnSectionDataChanged(object sender, SectionDataChangedEventArgs e)
		{
			if (ActiveSection == null) return;
			if (!_allowToSave) return;
			settingsContainer.UpdateSettingsAccordingDataChanges(ActiveSection.ActiveEditor.EditorType);
			UpdateStatusBar();
			UpdateCollectionChangeButtons();
			UpdateOutputStatus();
			SettingsNotSaved = true;
			if (e.SnapshotsChanged)
				ChangeInfo.SnapshotsChanged = true;
			if (e.OptionsSetsChanged)
				ChangeInfo.OptionsChanged = true;
		}

		private void OnSectionSettingsChanged(object sender, SettingsChangedEventArgs e)
		{
			if (ActiveSection == null) return;
			if (!_allowToSave) return;
			if (EditedContent.ApplySettingsForAll)
			{
				switch (e.ChangedSettingsType)
				{
					case ScheduleSettingsType.Columns:
					case ScheduleSettingsType.Totals:
					case ScheduleSettingsType.AdvancedColumns:
						var templateContractSettings = ActiveSection.SectionData.ContractSettings;
						foreach (var sectionTabControl in xtraTabControlSections.TabPages
							.OfType<SectionContainer>()
							.Where(oc => oc.SectionData.UniqueID != ActiveSection.SectionData.UniqueID)
							.ToList()
						)
						{
							sectionTabControl.SectionData.ApplyFromTemplate(ActiveSection.SectionData);
							sectionTabControl.SectionData.ContractSettings.ShowSignatureLine = templateContractSettings.ShowSignatureLine;
							sectionTabControl.SectionData.ContractSettings.ShowDisclaimer = templateContractSettings.ShowDisclaimer;
							sectionTabControl.SectionData.ContractSettings.RateExpirationDate = templateContractSettings.RateExpirationDate;
						}
						break;
				}
			}
			foreach (var sectionTabControl in xtraTabControlSections.TabPages.OfType<SectionContainer>())
				sectionTabControl.UpdateAccordingSettings(e);
			OnSectionDataChanged(sender, new SectionDataChangedEventArgs());
		}

		private void OnSectionEditorChanged(object sender, EventArgs e)
		{
			if (ActiveSection == null) return;
			settingsContainer.UpdateSettingsAccordingSelectedSectionEditor(ActiveSection.ActiveEditor.EditorType);
			UpdateCollectionChangeButtons();
			LoadThemes();
			UpdateOutputStatus();
		}

		private void OnTabMoved(object sender, TabMoveEventArgs e)
		{
			EditedContent.ChangeSectionPosition(
				EditedContent.Sections.IndexOf(((SectionContainer)e.MovedPage).SectionData),
				EditedContent.Sections.IndexOf(((SectionContainer)e.TargetPage).SectionData) + (1 * e.Offset));
			SettingsNotSaved = true;
		}

		private void UpdateSectionSplash()
		{
			if (EditedContent.Sections.Any())
			{
				layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Never;
				layoutControlItemSectionsContainer.Visibility = LayoutVisibility.Always;
				Controller.Instance.ProgramScheduleProgramAdd.Enabled = true;
				Controller.Instance.ProgramScheduleProgramDelete.Enabled = true;
				Controller.Instance.ProgramScheduleSettings.Enabled = true;
			}
			else
			{
				layoutControlItemSectionsContainer.Visibility = LayoutVisibility.Never;
				layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Always;
				Controller.Instance.ProgramScheduleProgramAdd.Enabled = false;
				Controller.Instance.ProgramScheduleProgramDelete.Enabled = false;
				Controller.Instance.ProgramScheduleSettings.Enabled = false;
			}
		}

		private void UpdateCollectionChangeButtons()
		{
			var activeItemCollection = ActiveSection.ActiveItemCollection;
			if (activeItemCollection == null)
			{
				Controller.Instance.ProgramScheduleProgramAdd.Enabled =
					Controller.Instance.ProgramScheduleProgramDelete.Enabled = false;
				((RibbonBar)(Controller.Instance.ProgramScheduleProgramAdd.ContainerControl)).Text = "Program";
			}
			else
			{
				Controller.Instance.ProgramScheduleProgramAdd.Enabled = activeItemCollection.AllowToAddItem;
				Controller.Instance.ProgramScheduleProgramDelete.Enabled = activeItemCollection.AllowToDeleteItem;
				((RibbonBar)(Controller.Instance.ProgramScheduleProgramAdd.ContainerControl)).Text = activeItemCollection.CollectionTitle;

				var addProductTitle = String.Format("Add {0}", activeItemCollection.CollectionItemTitle);
				var addProductTooltip = String.Format("Add a {0} to your schedule", activeItemCollection.CollectionItemTitle);
				var deleteProductTitle = String.Format("Delete {0}", activeItemCollection.CollectionItemTitle);
				var deleteProductTooltip = String.Format("Delete the selected {0} from your schedule", activeItemCollection.CollectionItemTitle);

				if (ActiveSection.ActiveEditor.EditorType == SectionEditorType.DigitalInfo)
				{
					if (!String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalAddTitle))
						addProductTitle = ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalAddTitle;
					if (!String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalAddTooltip))
						addProductTooltip = ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalAddTooltip;
					if (!String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalDeleteTitle))
						deleteProductTitle = ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalDeleteTitle;
					if (!String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalDeleteTooltip))
						deleteProductTooltip = ListManager.Instance.DefaultControlsConfiguration.RibbonButtonMediaDigitalDeleteTooltip;
				}
				Controller.Instance.Supertip.SetSuperTooltip(
					Controller.Instance.ProgramScheduleProgramAdd,
					new SuperTooltipInfo(
						addProductTitle,
						"",
						addProductTooltip,
						null,
						null,
						eTooltipColor.Gray));
				Controller.Instance.Supertip.SetSuperTooltip(
					Controller.Instance.ProgramScheduleProgramDelete,
					new SuperTooltipInfo(
						deleteProductTitle,
						"",
						deleteProductTooltip,
						null,
						null,
						eTooltipColor.Gray));
			}
		}

		private void OnSelectedSectionChanged(object sender, TabPageChangedEventArgs e)
		{
			if (!_allowToSave) return;
			LoadActiveSectionData();
		}

		private void OnSectionTabCloseClick(object sender, EventArgs e)
		{
			var arg = (ClosePageButtonEventArgs)e;
			var sectionControl = (SectionContainer)arg.Page;
			DeleteSection(sectionControl);
		}

		private void OnSectionTabMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			_menuHitInfo = xtraTabControlSections.CalcHitInfo(new Point(e.X, e.Y));
			if (_menuHitInfo.HitTest != XtraTabHitTest.PageHeader) return;
			contextMenuStripSections.Show((Control)sender, e.Location);
		}

		private void OnRenameSectionClick(object sender, EventArgs e)
		{
			RenameSection((SectionContainer)_menuHitInfo.Page);
		}

		private void OnCloneSectionClick(object sender, EventArgs e)
		{
			CloneSection((SectionContainer)_menuHitInfo.Page);
		}
		#endregion

		#region Settings management
		public override void EditSettings()
		{
			using (var form = new FormOutputSettings())
			{
				form.checkEditEmptySports.Text = String.Format(form.checkEditEmptySports.Text,
					String.Format("{0}s:", ActiveSection.SectionData.Parent.ScheduleSettings.SelectedSpotType));
				form.layoutControlItemEmptySports.Enabled = ActiveSection.SectionData.ShowSpots;
				form.simpleLabelItemEmptySports.Enabled = ActiveSection.SectionData.ShowSpots;
				form.checkEditEmptySports.Checked = !ActiveSection.SectionData.ShowEmptySpots;
				form.checkEditOutputNoBrackets.Checked = ActiveSection.SectionData.OutputNoBrackets;
				form.checkEditUseGenericDates.Checked = ActiveSection.SectionData.UseGenericDateColumns;
				form.checkEditUseDecimalRate.Checked = ActiveSection.SectionData.UseDecimalRates;
				form.checkEditCloneLineToTheEnd.Checked = ActiveSection.SectionData.CloneLineToTheEnd;
				form.layoutControlItemOutputLimitQuarters.Enabled = ActiveSection.SectionData.Parent.ScheduleSettings.Quarters.Count > 1;
				form.simpleLabelItemOutputLimitQuarters.Enabled = ActiveSection.SectionData.Parent.ScheduleSettings.Quarters.Count > 1;
				form.checkEditOutputLimitQuarters.Checked = ActiveSection.SectionData.Parent.ScheduleSettings.Quarters.Count > 1 &&
															ActiveSection.SectionData.OutputPerQuater;
				form.checkEditOutputLimitPeriods.Checked = ActiveSection.SectionData.OutputMaxPeriods.HasValue;
				form.spinEditOutputLimitPeriods.EditValue = ActiveSection.SectionData.OutputMaxPeriods;
				form.checkEditOutputLimitPeriods.Text = String.Format(form.checkEditOutputLimitPeriods.Text,
					ActiveSection.SectionData.Parent.ScheduleSettings.SelectedSpotType);
				form.checkEditLockToMaster.Checked = MediaMetaData.Instance.SettingsManager.UseSlideMaster;

				form.checkEditShowSignatureLine.Checked = ActiveSection.SectionData.ContractSettings.ShowSignatureLine;
				form.checkEditShowRatesExpiration.Checked = ActiveSection.SectionData.ContractSettings.RateExpirationDate.HasValue;
				form.checkEditShowDisclaimer.Checked = ActiveSection.SectionData.ContractSettings.ShowDisclaimer;
				form.dateEditRatesExpirationDate.EditValue = ActiveSection.SectionData.ContractSettings.RateExpirationDate;

				if (form.ShowDialog() != DialogResult.OK) return;

				var updateColumns = ActiveSection.SectionData.UseGenericDateColumns != form.checkEditUseGenericDates.Checked;

				ActiveSection.SectionData.ShowEmptySpots = !form.checkEditEmptySports.Checked;
				ActiveSection.SectionData.OutputNoBrackets = form.checkEditOutputNoBrackets.Checked;
				ActiveSection.SectionData.UseDecimalRates = form.checkEditUseDecimalRate.Checked;
				ActiveSection.SectionData.CloneLineToTheEnd = form.checkEditCloneLineToTheEnd.Checked;
				ActiveSection.SectionData.UseGenericDateColumns = form.checkEditUseGenericDates.Checked;
				ActiveSection.SectionData.OutputPerQuater = form.checkEditOutputLimitQuarters.Checked;
				ActiveSection.SectionData.OutputMaxPeriods = form.spinEditOutputLimitPeriods.EditValue != null
					? (Int32?)form.spinEditOutputLimitPeriods.Value
					: null;

				ActiveSection.SectionData.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
				ActiveSection.SectionData.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
				ActiveSection.SectionData.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;

				MediaMetaData.Instance.SettingsManager.UseSlideMaster = form.checkEditLockToMaster.Checked;

				OnSectionSettingsChanged(this, new SettingsChangedEventArgs
				{
					UpdateGridColums = updateColumns,
					ChangedSettingsType = ScheduleSettingsType.AdvancedColumns
				});
			}
		}

		private void OnSettingsControlsUpdated(object sender, EventArgs e)
		{
			retractableBarControl.AddButtons(settingsContainer.GetSettingsButtons());
		}

		private void OnFlexFlightDatesWarningClick(object sender, EventArgs e)
		{
			using (var form = new FormFlexFlightDatesWarning())
			{
				form.ShowDialog();
			}
		}
		#endregion

		#region Ribbon Operations Events
		public void OnAddSection(object sender, EventArgs e)
		{
			AddSection();
		}

		public void OnAddItem(object sender, EventArgs e)
		{
			ActiveSection?.AddItem();
		}

		public void OnDeleteItem(object sender, EventArgs e)
		{
			ActiveSection?.DeleteItem();
		}
		#endregion

		#region Status Bar management

		private readonly LabelItem _statusBarTotalPeriodInfo = new LabelItem();
		private readonly LabelItem _statusBarTotalSpotsInfo = new LabelItem();
		private readonly LabelItem _statusBarTotalGRPInfo = new LabelItem();
		private readonly LabelItem _statusBarTotalCPPInfo = new LabelItem();
		private readonly LabelItem _statusBarAvgRateInfo = new LabelItem();
		private readonly LabelItem _statusBarTotalCostInfo = new LabelItem();
		private readonly LabelItem _statusBarNetRateInfo = new LabelItem();
		private readonly LabelItem _statusBarAgencyDiscountInfo = new LabelItem();

		protected override void UpdateStatusBar()
		{
			if (ActiveSection!= null && ActiveSection.SectionData.Programs.Any())
			{
				_statusBarTotalPeriodInfo.Text = String.Format("{0}s: {1}", SpotTitle, ActiveSection.TotalPeriodsValueFormatted);
				_statusBarTotalSpotsInfo.Text = String.Format("Spots: {0}", ActiveSection.TotalSpotsValueFormatted);
				_statusBarTotalGRPInfo.Text = String.Format("{0}: {1}", ScheduleSettings.DemoType == DemoType.Rtg ? "GRPs" : "Impressions", ActiveSection.TotalGRPValueFormatted);
				_statusBarTotalCPPInfo.Text = String.Format("{0}: {1}", ScheduleSettings.DemoType == DemoType.Rtg ? "CPP" : "CPM", ActiveSection.TotalCPPValueFormatted);
				_statusBarAvgRateInfo.Text = String.Format("Avg Rate: {0}", ActiveSection.AvgRateValueFormatted);
				_statusBarTotalCostInfo.Text = String.Format("Gross: {0}", ActiveSection.TotalCostValuesFormatted);
				_statusBarNetRateInfo.Text = String.Format("Net: {0}", ActiveSection.NetRateValueFormatted);
				_statusBarAgencyDiscountInfo.Text = String.Format("Agency: {0}", ActiveSection.TotalDiscountValueFormatted);

				var statusBarItems = new List<LabelItem>();

				statusBarItems.Add(_statusBarTotalPeriodInfo);
				if (ActiveSection.SectionData.ShowTotalSpots)
					statusBarItems.Add(_statusBarTotalSpotsInfo);
				if (ActiveSection.SectionData.ShowTotalGRP)
					statusBarItems.Add(_statusBarTotalGRPInfo);
				if (ActiveSection.SectionData.ShowTotalCPP)
					statusBarItems.Add(_statusBarTotalCPPInfo);
				if (ActiveSection.SectionData.ShowAverageRate)
					statusBarItems.Add(_statusBarAvgRateInfo);
				if (ActiveSection.SectionData.ShowTotalRate)
					statusBarItems.Add(_statusBarTotalCostInfo);
				if (ActiveSection.SectionData.ShowNetRate)
					statusBarItems.Add(_statusBarNetRateInfo);
				if (ActiveSection.SectionData.ShowDiscount)
					statusBarItems.Add(_statusBarAgencyDiscountInfo);

				ContentStatusBarManager.Instance.StatusBarItemsContainer.SubItems.Clear();
				ContentStatusBarManager.Instance.StatusBarItemsContainer.SubItems.AddRange(statusBarItems.ToArray());
				ContentStatusBarManager.Instance.StatusBar.RecalcLayout();
			}
			else
				base.UpdateStatusBar();
		}
		#endregion

		#region Output Staff
		private SlideType SlideType => ActiveSection?.ActiveEditor?.SlideType ??
									   (MediaMetaData.Instance.DataType == MediaDataType.TVSchedule
										   ? SlideType.TVSchedulePrograms
										   : SlideType.RadioSchedulePrograms);

		public override void OutputPowerPoint()
		{
			ActiveSection?.OutputPowerPoint();
		}

		public override void OutputPdf()
		{
			ActiveSection?.OutputPdf();
		}

		public override void Preview()
		{
			ActiveSection?.Preview();
		}

		public override void Email()
		{
			ActiveSection?.Email();
		}
		#endregion
	}
}