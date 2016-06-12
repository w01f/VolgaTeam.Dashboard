using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ContentEditors.Controls;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
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

		public SlideType SlideType => MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ?
			SlideType.TVProgramSchedule :
			SlideType.RadioProgramSchedule;
		#endregion

		public ScheduleContainer()
		{
			InitializeComponent();
		}

		#region BasePartitionEditControl Override
		public override void InitControl()
		{
			base.InitControl();
			pnSections.Dock = DockStyle.Fill;
			pnNoSections.Dock = DockStyle.Fill;

			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				labelControlScheduleInfo.Font = font;
				labelControlFlexFlightDatesWarning.Font = new Font(labelControlFlexFlightDatesWarning.Font.FontFamily,
					labelControlFlexFlightDatesWarning.Font.Size - 2, labelControlFlexFlightDatesWarning.Font.Style);

				font = new Font(laTotalPeriodsTitle.Font.FontFamily, laTotalPeriodsTitle.Font.Size - 2,
					laTotalPeriodsTitle.Font.Style);
				laTotalPeriodsTitle.Font = font;
				laTotalGRPTitle.Font = font;
				laTotalCPPTitle.Font = font;
				laAvgRateTitle.Font = font;
				laTotalCostTitle.Font = font;
				laNetRateTitle.Font = font;
				laAgencyDiscountTitle.Font = font;
				font = new Font(laTotalPeriodsValue.Font.FontFamily, laTotalPeriodsValue.Font.Size - 2,
					laTotalPeriodsValue.Font.Style);
				laTotalPeriodsValue.Font = font;
				laTotalGRPValue.Font = font;
				laTotalCPPValue.Font = font;
				laAvgRateValue.Font = font;
				laTotalCostValue.Font = font;
				laNetRateValue.Font = font;
				laAgencyDiscountValue.Font = font;
			}

			settingsContainer.InitControl();
			settingsContainer.SettingsChanged += OnSectionSettingsChanged;
			settingsContainer.SettingsControlsUpdated += OnSettingsControlsUpdated;

			quarterSelectorControl.QuarterSelected += OnQuarterChanged;

			retractableBarControl.Collapse(true);

			_tabDragDropHelper = new XtraTabDragDropHelper<SectionContainer>(xtraTabControlSections);
			_tabDragDropHelper.TabMoved += OnTabMoved;

			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) => OnOuterThemeChanged();
			BusinessObjects.Instance.OutputManager.ColorsChanged += OnSettingsControlsUpdated;

			Controller.Instance.ProgramScheduleNew.Click += OnAddSection;
			Controller.Instance.ProgramScheduleProgramAdd.Click += OnAddItem;
			Controller.Instance.ProgramScheduleProgramDelete.Click += OnDeleteItem;
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
			EditedContent = Schedule.ProgramSchedule.Clone<ProgramScheduleContent, ProgramScheduleContent>();

			labelControlScheduleInfo.Text = String.Format("{0}<br><color=gray><i>{1} ({2})</i></color>",
				ScheduleSettings.BusinessName,
				ScheduleSettings.FlightDates,
				String.Format("{0} {1}s", EditedContent.TotalPeriods, SpotTitle));

			laTotalPeriodsTitle.Text = String.Format("Total {0}s:", SpotTitle);
			if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed &&
				(ScheduleSettings.FlightDateStart != ScheduleSettings.UserFlightDateStart ||
					ScheduleSettings.FlightDateEnd != ScheduleSettings.UserFlightDateEnd))
				labelControlFlexFlightDatesWarning.Visible = true;
			else
				labelControlFlexFlightDatesWarning.Visible = false;

			laTotalCPPTitle.Text = ScheduleSettings.DemoType == DemoType.Rtg ? "Overall CPP:" : "Overall CPM:";
			laTotalGRPTitle.Text = ScheduleSettings.DemoType == DemoType.Rtg ? "Total GRPs:" : "Total Impr:";

			settingsContainer.LoadContent(EditedContent);

			quarterSelectorControl.InitControls(
				ScheduleSettings.Quarters,
				ScheduleSettings.Quarters.FirstOrDefault(q => q.DateAnchor == EditedContent.SelectedQuarter));
			quarterSelectorControl.Enabled = false;

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
			if (ActiveSection != null && ActiveSection.ActiveEditor.EditorType != SectionEditorType.ScheduleSection)
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
			FormThemeSelector.Link(Controller.Instance.ProgramScheduleTheme, BusinessObjects.Instance.ThemeManager.GetThemes(SlideType), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				IsThemeChanged = true;
			}));
			Controller.Instance.ProgramScheduleThemeBar.RecalcLayout();
			Controller.Instance.ProgramSchedulePanel.PerformLayout();
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
				UpdateQuarterSelectorControl();
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
			sectionTabControl.UpdateSpotsByQuarter(quarterSelectorControl.SelectedQuarter);
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

		private void UpdateQuarterSelectorControl()
		{
			if (ActiveSection == null) return;
			quarterSelectorControl.Enabled = ActiveSection.SectionData.ShowSpots;
		}

		private void UpdateTotalsVisibility()
		{
			if (ActiveSection == null) return;

			pnBottom.Visible = ActiveSection.SectionData.Programs.Any() &&
				(ActiveSection.SectionData.ShowTotalCPP ||
				ActiveSection.SectionData.ShowTotalGRP ||
				ActiveSection.SectionData.ShowTotalSpots ||
				ActiveSection.SectionData.ShowTotalPeriods ||
				ActiveSection.SectionData.ShowAverageRate ||
				ActiveSection.SectionData.ShowTotalRate ||
				ActiveSection.SectionData.ShowNetRate ||
				ActiveSection.SectionData.ShowDiscount);

			pnTotalCPP.Visible = ActiveSection.SectionData.ShowTotalCPP;
			pnTotalCPP.SendToBack();
			pnTotalGRP.Visible = ActiveSection.SectionData.ShowTotalGRP;
			pnTotalGRP.SendToBack();
			pnTotalSpots.Visible = ActiveSection.SectionData.ShowTotalSpots;
			pnTotalSpots.SendToBack();
			pnTotalPeriods.Visible = ActiveSection.SectionData.ShowTotalPeriods;
			pnTotalPeriods.SendToBack();
			pnAvgRate.Visible = ActiveSection.SectionData.ShowAverageRate;
			pnAvgRate.BringToFront();
			pnTotalCost.Visible = ActiveSection.SectionData.ShowTotalRate;
			pnTotalCost.BringToFront();
			pnNetRate.Visible = ActiveSection.SectionData.ShowNetRate;
			pnNetRate.BringToFront();
			pnAgencyDiscount.Visible = ActiveSection.SectionData.ShowDiscount;
			pnAgencyDiscount.BringToFront();
		}

		private void UpdateTotalsValues()
		{
			if (ActiveSection != null && ActiveSection.SectionData.Programs.Any())
			{
				laTotalPeriodsValue.Text = ActiveSection.TotalPeriodsValueFormatted;
				laTotalSpotsValue.Text = ActiveSection.TotalSpotsValueFormatted;
				laTotalGRPValue.Text = ActiveSection.TotalGRPValueFormatted;
				laTotalCPPValue.Text = ActiveSection.TotalCPPValueFormatted;
				laAvgRateValue.Text = ActiveSection.AvgRateValueFormatted;
				laTotalCostValue.Text = ActiveSection.TotalCostValuesFormatted;
				laNetRateValue.Text = ActiveSection.NetRateValueFormatted;
				laAgencyDiscountValue.Text = ActiveSection.TotalDiscountValueFormatted;
			}
			else
			{
				laTotalPeriodsValue.Text =
				laTotalSpotsValue.Text =
				laTotalGRPValue.Text =
				laTotalCPPValue.Text =
				laAvgRateValue.Text =
				laTotalCostValue.Text =
				laNetRateValue.Text =
					laAgencyDiscountValue.Text = String.Empty;
			}
		}

		private void UpdateOutputStatus()
		{
			Controller.Instance.ProgramSchedulePowerPoint.Enabled =
				Controller.Instance.ProgramSchedulePdf.Enabled =
				Controller.Instance.ProgramSchedulePreview.Enabled =
				Controller.Instance.ProgramScheduleEmail.Enabled = xtraTabControlSections.TabPages
				.OfType<SectionContainer>()
				.Any(sectionTabControl => sectionTabControl.ReadyForOutput);
		}

		private void OnSectionDataChanged(object sender, EventArgs e)
		{
			if (ActiveSection == null) return;
			if (!_allowToSave) return;
			settingsContainer.UpdateSettingsAccordingDataChanges(ActiveSection.ActiveEditor.EditorType);
			UpdateQuarterSelectorControl();
			UpdateTotalsVisibility();
			UpdateTotalsValues();
			UpdateOutputStatus();
			SettingsNotSaved = true;
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
			OnSectionDataChanged(sender, e);
		}

		private void OnSectionEditorChanged(object sender, EventArgs e)
		{
			if (ActiveSection == null) return;
			settingsContainer.UpdateSettingsAccordingSelectedSectionEditor(ActiveSection.ActiveEditor.EditorType);
			quarterSelectorControl.Visible =
				ActiveSection.ActiveEditor.EditorType == SectionEditorType.ScheduleSection;
			Controller.Instance.ProgramScheduleProgramAdd.Enabled =
				Controller.Instance.ProgramScheduleProgramDelete.Enabled = ActiveSection.ActiveItemCollection != null;
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
				pnSections.BringToFront();
				Controller.Instance.ProgramScheduleProgramAdd.Enabled = true;
				Controller.Instance.ProgramScheduleProgramDelete.Enabled = true;
			}
			else
			{
				pnNoSections.BringToFront();
				Controller.Instance.ProgramScheduleProgramAdd.Enabled = false;
				Controller.Instance.ProgramScheduleProgramDelete.Enabled = false;
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

		private void OnQuarterChanged(object sender, EventArgs e)
		{
			var selectedQuarter = quarterSelectorControl.SelectedQuarter;
			EditedContent.SelectedQuarter = selectedQuarter != null ? selectedQuarter.DateAnchor : (DateTime?)null;
			foreach (var sectionTabControl in xtraTabControlSections.TabPages.OfType<SectionContainer>())
				sectionTabControl.UpdateSpotsByQuarter(selectedQuarter);
			SettingsNotSaved = true;
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
		private IEnumerable<SectionContainer> SelectSectionsForOutput()
		{
			var tabPages = xtraTabControlSections.TabPages.OfType<SectionContainer>().Where(ss => ss.ReadyForOutput).ToList();
			var selectedSections = new List<SectionContainer>();
			if (tabPages.Count > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Schedules";
					var currentSection = (SectionContainer)xtraTabControlSections.SelectedTabPage;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.SectionData.Name);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (tabPage == currentSection)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedSections.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<SectionContainer>());
				}
			else
				selectedSections.AddRange(tabPages);
			return selectedSections;
		}

		public override void OutputPowerPoint()
		{
			var selectedSections = new List<SectionContainer>(SelectSectionsForOutput());
			if (!selectedSections.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				foreach (var sectionTabControl in selectedSections)
					sectionTabControl.GenerateOutput();
				FormProgress.CloseProgress();
			});
		}

		public override void OutputPdf()
		{
			var selectedSections = new List<SectionContainer>(SelectSectionsForOutput());
			if (!selectedSections.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var previewGroups = selectedSections.Select(summaryTab => summaryTab.GeneratePreview()).ToList();
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
			var selectedSections = new List<SectionContainer>(SelectSectionsForOutput());
			if (!selectedSections.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var previewGroups = selectedSections.Select(sectionControl => sectionControl.GeneratePreview()).ToList();
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();

			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;

			using (var formPreview = new FormPreview(
				Controller.Instance.FormMain,
				RegularMediaSchedulePowerPointHelper.Instance,
				BusinessObjects.Instance.HelpManager,
				Controller.Instance.ShowFloater))
			{
				formPreview.Text = "Preview Schedule";
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
			var selectedSections = new List<SectionContainer>(SelectSectionsForOutput());
			if (!selectedSections.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var previewGroups = selectedSections.Select(sectionControl => sectionControl.GeneratePreview()).ToList();
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();

			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
			{
				formEmail.Text = "Email this Schedule";
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