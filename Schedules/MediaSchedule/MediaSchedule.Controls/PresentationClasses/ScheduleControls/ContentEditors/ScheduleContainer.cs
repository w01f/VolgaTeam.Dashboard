using System;
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
using Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings;
using Asa.Schedules.Common.Controls.ContentEditors.Controls;
using DevComponents.DotNetBar;
using DevExpress.Skins;
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

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			simpleLabelItemScheduleInfo.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemScheduleInfo.MaxSize, scaleFactor);
			simpleLabelItemScheduleInfo.MinSize = RectangleHelper.ScaleSize(simpleLabelItemScheduleInfo.MinSize, scaleFactor);
			simpleLabelItemFlightDates.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemFlightDates.MaxSize, scaleFactor);
			simpleLabelItemFlightDates.MinSize = RectangleHelper.ScaleSize(simpleLabelItemFlightDates.MinSize, scaleFactor);
			simpleLabelItemTotalPeriodsTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTotalPeriodsTitle.MaxSize, scaleFactor);
			simpleLabelItemTotalPeriodsTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTotalPeriodsTitle.MinSize, scaleFactor);
			simpleLabelItemTotalPeriodsValue.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTotalPeriodsValue.MaxSize, scaleFactor);
			simpleLabelItemTotalPeriodsValue.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTotalPeriodsValue.MinSize, scaleFactor);
			simpleLabelItemTotalSpotsTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTotalSpotsTitle.MaxSize, scaleFactor);
			simpleLabelItemTotalSpotsTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTotalSpotsTitle.MinSize, scaleFactor);
			simpleLabelItemTotalSpotsValue.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTotalSpotsValue.MaxSize, scaleFactor);
			simpleLabelItemTotalSpotsValue.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTotalSpotsValue.MinSize, scaleFactor);
			simpleLabelItemTotalGRPTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTotalGRPTitle.MaxSize, scaleFactor);
			simpleLabelItemTotalGRPTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTotalGRPTitle.MinSize, scaleFactor);
			simpleLabelItemTotalGRPValue.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTotalGRPValue.MaxSize, scaleFactor);
			simpleLabelItemTotalGRPValue.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTotalGRPValue.MinSize, scaleFactor);
			simpleLabelItemTotalCPPTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTotalCPPTitle.MaxSize, scaleFactor);
			simpleLabelItemTotalCPPTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTotalCPPTitle.MinSize, scaleFactor);
			simpleLabelItemTotalCPPValue.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTotalCPPValue.MaxSize, scaleFactor);
			simpleLabelItemTotalCPPValue.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTotalCPPValue.MinSize, scaleFactor);
			simpleLabelItemAvgRateTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemAvgRateTitle.MaxSize, scaleFactor);
			simpleLabelItemAvgRateTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemAvgRateTitle.MinSize, scaleFactor);
			simpleLabelItemAvgRateValue.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemAvgRateValue.MaxSize, scaleFactor);
			simpleLabelItemAvgRateValue.MinSize = RectangleHelper.ScaleSize(simpleLabelItemAvgRateValue.MinSize, scaleFactor);
			simpleLabelItemTotalCostTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTotalCostTitle.MaxSize, scaleFactor);
			simpleLabelItemTotalCostTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTotalCostTitle.MinSize, scaleFactor);
			simpleLabelItemTotalCostValue.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTotalCostValue.MaxSize, scaleFactor);
			simpleLabelItemTotalCostValue.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTotalCostValue.MinSize, scaleFactor);
			simpleLabelItemNetRateTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemNetRateTitle.MaxSize, scaleFactor);
			simpleLabelItemNetRateTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemNetRateTitle.MinSize, scaleFactor);
			simpleLabelItemNetRateValue.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemNetRateValue.MaxSize, scaleFactor);
			simpleLabelItemNetRateValue.MinSize = RectangleHelper.ScaleSize(simpleLabelItemNetRateValue.MinSize, scaleFactor);
			simpleLabelItemAgencyDiscountTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemAgencyDiscountTitle.MaxSize, scaleFactor);
			simpleLabelItemAgencyDiscountTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemAgencyDiscountTitle.MinSize, scaleFactor);
			simpleLabelItemAgencyDiscountValue.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemAgencyDiscountValue.MaxSize, scaleFactor);
			simpleLabelItemAgencyDiscountValue.MinSize = RectangleHelper.ScaleSize(simpleLabelItemAgencyDiscountValue.MinSize, scaleFactor);
		}

		protected override void UpdateEditedContet()
		{
			_allowToSave = false;

			var quickLoad = EditedContent != null && !(ContentUpdateInfo.ChangeInfo.WholeScheduleChanged ||
				ContentUpdateInfo.ChangeInfo.ScheduleDatesChanged ||
				ContentUpdateInfo.ChangeInfo.CalendarTypeChanged ||
				ContentUpdateInfo.ChangeInfo.SpotTypeChanged);

			EditedContent?.Dispose();
			EditedContent = Schedule.ProgramSchedule.Clone<ProgramScheduleContent, ProgramScheduleContent>();

			simpleLabelItemScheduleInfo.Text = String.Format("<color=gray>{0}</color>", ScheduleSettings.BusinessName);

			simpleLabelItemFlightDates.Text = String.Format("<color=gray>{0} <i>({1})</i></color>",
				ScheduleSettings.FlightDates,
				String.Format("{0} {1}s", ScheduleSettings.TotalWeeks, "week"));

			simpleLabelItemTotalPeriodsTitle.Text = String.Format("<size=-1>Total {0}s:</size>", SpotTitle);
			if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed &&
				(ScheduleSettings.FlightDateStart != ScheduleSettings.UserFlightDateStart ||
					ScheduleSettings.FlightDateEnd != ScheduleSettings.UserFlightDateEnd))
				layoutControlGroupFlexFlightDatesWarning.Visibility = LayoutVisibility.Always;
			else
				layoutControlGroupFlexFlightDatesWarning.Visibility = LayoutVisibility.Never;

			simpleLabelItemTotalCPPTitle.Text = String.Format("<color=darkgray><size=-1>{0}</size></color>", ScheduleSettings.DemoType == DemoType.Rtg ? "Overall CPP:" : "Overall CPM:");
			simpleLabelItemTotalGRPTitle.Text = String.Format("<color=darkgray><size=-1>{0}</size></color>", ScheduleSettings.DemoType == DemoType.Rtg ? "Total GRPs:" : "Total Impr:");

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
				UpdateTotalsValues();
				UpdateTotalsVisibility();
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

		private void UpdateTotalsVisibility()
		{
			if (ActiveSection == null) return;

			layoutControlGroupTotals.Visibility = ActiveSection.SectionData.Programs.Any() &&
				(ActiveSection.SectionData.ShowTotalCPP ||
				ActiveSection.SectionData.ShowTotalGRP ||
				ActiveSection.SectionData.ShowTotalSpots ||
				ActiveSection.SectionData.ShowTotalPeriods ||
				ActiveSection.SectionData.ShowAverageRate ||
				ActiveSection.SectionData.ShowTotalRate ||
				ActiveSection.SectionData.ShowNetRate ||
				ActiveSection.SectionData.ShowDiscount) ? LayoutVisibility.Always : LayoutVisibility.Never;

			layoutControlGroupTotalCPP.Visibility = ActiveSection.SectionData.ShowTotalCPP ? LayoutVisibility.Always : LayoutVisibility.Never;
			layoutControlGroupTotalGRP.Visibility = ActiveSection.SectionData.ShowTotalGRP ? LayoutVisibility.Always : LayoutVisibility.Never;
			layoutControlGroupTotalSpots.Visibility = ActiveSection.SectionData.ShowTotalSpots ? LayoutVisibility.Always : LayoutVisibility.Never;
			layoutControlGroupTotalPeriods.Visibility = ActiveSection.SectionData.ShowTotalPeriods ? LayoutVisibility.Always : LayoutVisibility.Never;
			layoutControlGroupAvgRate.Visibility = ActiveSection.SectionData.ShowAverageRate ? LayoutVisibility.Always : LayoutVisibility.Never;
			layoutControlGroupTotalCost.Visibility = ActiveSection.SectionData.ShowTotalRate ? LayoutVisibility.Always : LayoutVisibility.Never;
			layoutControlGroupNetRate.Visibility = ActiveSection.SectionData.ShowNetRate ? LayoutVisibility.Always : LayoutVisibility.Never;
			layoutControlGroupAgencyDiscount.Visibility = ActiveSection.SectionData.ShowDiscount ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void UpdateTotalsValues()
		{
			if (ActiveSection != null && ActiveSection.SectionData.Programs.Any())
			{
				simpleLabelItemTotalPeriodsValue.Text = String.Format("<color=dimgray><size=-1>{0}</size></color>", ActiveSection.TotalPeriodsValueFormatted);
				simpleLabelItemTotalSpotsValue.Text = String.Format("<color=dimgray><size=-1>{0}</size></color>", ActiveSection.TotalSpotsValueFormatted);
				simpleLabelItemTotalGRPValue.Text = String.Format("<color=dimgray><size=-1>{0}</size></color>", ActiveSection.TotalGRPValueFormatted);
				simpleLabelItemTotalCPPValue.Text = String.Format("<color=dimgray><size=-1>{0}</size></color>", ActiveSection.TotalCPPValueFormatted);
				simpleLabelItemAvgRateValue.Text = String.Format("<color=dimgray><size=-1>{0}</size></color>", ActiveSection.AvgRateValueFormatted);
				simpleLabelItemTotalCostValue.Text = String.Format("<color=dimgray><size=-1>{0}</size></color>", ActiveSection.TotalCostValuesFormatted);
				simpleLabelItemNetRateValue.Text = String.Format("<color=dimgray><size=-1>{0}</size></color>", ActiveSection.NetRateValueFormatted);
				simpleLabelItemAgencyDiscountValue.Text = String.Format("<color=dimgray><size=-1>{0}</size></color>", ActiveSection.TotalDiscountValueFormatted);
			}
			else
			{
				simpleLabelItemTotalPeriodsValue.Text =
				simpleLabelItemTotalSpotsValue.Text =
				simpleLabelItemTotalGRPValue.Text =
				simpleLabelItemTotalCPPValue.Text =
				simpleLabelItemAvgRateValue.Text =
				simpleLabelItemTotalCostValue.Text =
				simpleLabelItemNetRateValue.Text =
					simpleLabelItemAgencyDiscountValue.Text = String.Empty;
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
			UpdateTotalsVisibility();
			UpdateTotalsValues();
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
						foreach (var sectionTabControl in xtraTabControlSections.TabPages
							.OfType<SectionContainer>()
							.Where(oc => oc.SectionData.UniqueID != ActiveSection.SectionData.UniqueID)
							)
							sectionTabControl.SectionData.ApplyFromTemplate(ActiveSection.SectionData);
						break;
					case ScheduleSettingsType.Contract:
						var templateSettings = ActiveSection.SectionData.ContractSettings;
						foreach (var sectionTabControl in xtraTabControlSections.TabPages.OfType<SectionContainer>())
						{
							sectionTabControl.SectionData.ContractSettings.ShowSignatureLine = templateSettings.ShowSignatureLine;
							sectionTabControl.SectionData.ContractSettings.ShowDisclaimer = templateSettings.ShowDisclaimer;
							sectionTabControl.SectionData.ContractSettings.RateExpirationDate = templateSettings.RateExpirationDate;
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
			}
			else
			{
				layoutControlItemSectionsContainer.Visibility = LayoutVisibility.Never;
				layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Always;
				Controller.Instance.ProgramScheduleProgramAdd.Enabled = false;
				Controller.Instance.ProgramScheduleProgramDelete.Enabled = false;
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