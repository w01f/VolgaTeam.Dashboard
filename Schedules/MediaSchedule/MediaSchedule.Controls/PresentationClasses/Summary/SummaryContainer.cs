using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.ContentEditors.Controls;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.RetractableBar;
using Asa.Common.GUI.Summary;
using Asa.Common.GUI.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.Properties;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Summary
{
	[ToolboxItem(false)]
	public partial class SummaryContainer : BasePartitionEditControl<ProgramScheduleContent, MediaSchedule, MediaScheduleSettings, MediaScheduleChangeInfo>
	{
		private bool _allowToSave;
		private SummaryTypeSettingsControl _summaryTypeTab;
		private ButtonInfo _summaryTypeButton;

		#region Properties
		private MediaSchedule Schedule
		{
			get { return BusinessObjects.Instance.ScheduleManager.ActiveSchedule; }
		}

		private MediaScheduleSettings ScheduleSettings
		{
			get { return Schedule.Settings; }
		}

		public override string Identifier
		{
			get { return ContentIdentifiers.Summary; }
		}

		public override RibbonTabItem TabPage
		{
			get { return Controller.Instance.TabSummary; }
		}

		public string SpotTitle
		{
			get { return ScheduleSettings.SelectedSpotType.ToString(); }
		}

		private SummaryTab ActiveSummary
		{
			get { return (SummaryTab)xtraTabControlSections.SelectedTabPage; }
		}
		#endregion

		public SummaryContainer()
		{
			InitializeComponent();
		}

		#region BasePartitionEditControl Override
		public override void InitControl()
		{
			base.InitControl();
			pnSections.Dock = DockStyle.Fill;
			_summaryTypeTab = new SummaryTypeSettingsControl();
			_summaryTypeTab.SummaryTypeChanged += OnSummaryTypeChanged;
			_summaryTypeTab.DataChanged += OnSummaryDataChanged;
			_summaryTypeButton = new ButtonInfo()
			{
				Tooltip = "Select Summary Type",
				Logo = Resources.SummaryOptionsType,
				Action = () => { xtraTabControlOptions.SelectedTabPage = _summaryTypeTab; }
			};
			retractableBarControl.Collapse(true);
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) => OnOuterThemeChanged();
			hyperLinkEditInfoContract.Enabled = BusinessObjects.Instance.OutputManager.ContractTemplateFolder.ExistsLocal();
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

			LoadSections(quickLoad);
			LoadActiveSectionData();
			UpdateOutputStatus();

			_allowToSave = true;
		}

		protected override void ApplyChanges()
		{
			xtraTabControlSections.TabPages
				.OfType<SummaryTab>()
				.ToList()
				.ForEach(summaryTab => summaryTab.SaveData());
			ChangeInfo.SummaryChanged = ChangeInfo.SummaryChanged || SettingsNotSaved;
		}

		protected override void SaveData()
		{
			Schedule.ProgramSchedule = EditedContent.Clone<ProgramScheduleContent, ProgramScheduleContent>();
		}

		public override void GetHelp()
		{
			var helpKey = String.Empty;
			switch (ActiveSummary.Section.Summary.SummaryType)
			{
				case SectionSummaryTypeEnum.Product:
					helpKey = "summary1";
					break;
				case SectionSummaryTypeEnum.Custom:
					helpKey = "summary2";
					break;
				case SectionSummaryTypeEnum.Strategy:
					helpKey = "strategy";
					break;
			}
			BusinessObjects.Instance.HelpManager.OpenHelpLink(helpKey);
		}

		protected override void LoadThemes()
		{
			base.LoadThemes();
			FormThemeSelector.Link(Controller.Instance.SummaryTheme, BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.Summaries), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Summaries), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType.Summaries, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				IsThemeChanged = true;
			}));
			Controller.Instance.SummaryThemeBar.RecalcLayout();
			Controller.Instance.SummaryPanel.PerformLayout();
		}
		#endregion

		#region Sections Management
		private void LoadSections(bool quickLoad)
		{
			if (!quickLoad)
			{
				xtraTabControlOptions.TabPages.Clear();
				xtraTabControlSections.TabPages
					.OfType<SummaryTab>()
					.ToList()
					.ForEach(sc => sc.Release());
				xtraTabControlSections.TabPages.Clear();
			}
			foreach (var section in EditedContent.Sections.OrderBy(s => s.Index))
			{
				if (quickLoad)
				{
					var sectionTabPage = xtraTabControlSections.TabPages
						.OfType<SummaryTab>()
						.FirstOrDefault(sc => sc.Section.UniqueID.Equals(section.UniqueID));
					if (sectionTabPage == null) continue;
					sectionTabPage.LoadData(section, true);
				}
				else
					AddSectionControl(section);
			}
			if (!quickLoad)
				xtraTabControlSections.SelectedTabPageIndex = 0;
		}

		private void LoadActiveSectionData()
		{
			_allowToSave = false;
			if (ActiveSummary != null)
			{
				_summaryTypeTab.LoadData(ActiveSummary.Section.Summary);
				UpdateSlideCount();
				LoadBarButtons();
				LoadSummarySettingsTabs();
			}
			_allowToSave = true;
		}

		private void AddSectionControl(ScheduleSection sectionData)
		{
			var summaryTab = new SummaryTab();
			summaryTab.LoadData(sectionData, false);
			summaryTab.SummaryTypeChanged += (o, e) =>
			{
				if (!_allowToSave) return;
				LoadActiveSectionData();
				SettingsNotSaved = true;
			};
			summaryTab.DataChanged += (o, e) =>
			{
				if (!_allowToSave) return;
				UpdateSlideCount();
				UpdateOutputStatus();
				SettingsNotSaved = true;
			};
			xtraTabControlSections.TabPages.Add(summaryTab);
		}

		private void UpdateOutputStatus()
		{
			Controller.Instance.SummaryPowerPoint.Enabled =
				Controller.Instance.SummaryPdf.Enabled =
				Controller.Instance.SummaryPreview.Enabled =
				Controller.Instance.SummaryEmail.Enabled = xtraTabControlSections.TabPages
				.OfType<SummaryTab>()
				.Any(sectionTabControl => sectionTabControl.Content.ReadyForOutput);
		}

		private void OnSummaryTypeChanged(object sender, EventArgs e)
		{
			ActiveSummary.ResetContent();
			SettingsNotSaved = true;
		}

		private void OnSummaryDataChanged(object sender, EventArgs e)
		{
			UpdateSlideCount();
			SettingsNotSaved = true;
		}

		private void UpdateSlideCount()
		{
			if (ActiveSummary != null &&
				(ActiveSummary.Section.Summary.SummaryType == SectionSummaryTypeEnum.Product ||
				ActiveSummary.Section.Summary.SummaryType == SectionSummaryTypeEnum.Custom))
				_summaryTypeTab.UpdateSlideCount(((ISummaryControl)ActiveSummary.Content).SlidesCount);
		}

		private void xtraTabControlSections_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if (!_allowToSave) return;
			LoadActiveSectionData();
		}
		#endregion

		#region Settings management
		private void LoadSummarySettingsTabs()
		{
			var currentTabIndex = xtraTabControlOptions.SelectedTabPageIndex;
			xtraTabControlOptions.TabPages.Clear();
			xtraTabControlOptions.TabPages.Add(_summaryTypeTab);
			xtraTabControlOptions.TabPages.AddRange(ActiveSummary.Content.SettingsPages.OfType<XtraTabPage>().ToArray());
			if (currentTabIndex < xtraTabControlOptions.TabPages.Count)
				xtraTabControlOptions.SelectedTabPageIndex = currentTabIndex;

		}

		private void LoadBarButtons()
		{
			var buttonInfos = new List<ButtonInfo>();
			buttonInfos.Add(_summaryTypeButton);
			buttonInfos.AddRange(ActiveSummary.Content.BarButtons);
			retractableBarControl.AddButtons(buttonInfos);
		}

		private void OnContractSettingsOpenLink(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			if (ActiveSummary == null) return;
			using (var form = new FormContractSettings())
			{
				form.checkEditShowSignatureLine.Checked = ActiveSummary.Section.ContractSettings.ShowSignatureLine;
				form.checkEditShowRatesExpiration.Checked = ActiveSummary.Section.ContractSettings.RateExpirationDate.HasValue;
				form.checkEditShowDisclaimer.Checked = ActiveSummary.Section.ContractSettings.ShowDisclaimer;
				form.dateEditRatesExpirationDate.EditValue = ActiveSummary.Section.ContractSettings.RateExpirationDate;
				if (form.ShowDialog() != DialogResult.OK) return;
				ActiveSummary.Section.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
				ActiveSummary.Section.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
				ActiveSummary.Section.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;
				SettingsNotSaved = true;
			}
		}
		#endregion

		#region Output Staff
		public Theme SelectedTheme
		{
			get
			{
				return BusinessObjects.Instance.ThemeManager
					.GetThemes(SlideType.Summaries)
					.FirstOrDefault(t =>
						t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Summaries)) ||
						String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Summaries)));
			}
		}

		private IEnumerable<SummaryTab> SelectSectionsForOutput()
		{
			var tabPages = xtraTabControlSections.TabPages.OfType<SummaryTab>().Where(ss => ss.Content.ReadyForOutput).ToList();
			var selectedSections = new List<SummaryTab>();
			if (tabPages.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Summaries";
					var currentSection = (SummaryTab)xtraTabControlSections.SelectedTabPage;
					foreach (var tabPage in tabPages)
					{
						var item = new CheckedListBoxItem(tabPage, tabPage.Section.Name);
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
							OfType<SummaryTab>());
				}
			else
				selectedSections.AddRange(tabPages);
			return selectedSections;
		}

		public override void OutputPowerPoint()
		{
			var selectedSections = new List<SummaryTab>(SelectSectionsForOutput());
			if (!selectedSections.Any()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				foreach (var selectedSection in selectedSections)
					selectedSection.Content.GeneratePowerPointOutput();
				FormProgress.CloseProgress();
			});
		}

		public override void OutputPdf()
		{
			var selectedSections = new List<SummaryTab>(SelectSectionsForOutput());

			if (!selectedSections.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var previewGroups = selectedSections.Select(summaryTab => summaryTab.Content.GeneratePreview()).ToList();
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
			var selectedSections = new List<SummaryTab>(SelectSectionsForOutput());

			if (!selectedSections.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var previewGroups = selectedSections.Select(summaryTab => summaryTab.Content.GeneratePreview()).ToList();
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			FormProgress.CloseProgress();

			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;

			using (var formPreview = new FormPreview(Controller.Instance.FormMain, RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater))
			{
				formPreview.Text = "Preview Summary";
				formPreview.LoadGroups(previewGroups);
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			}
		}

		public override void Email()
		{
			var selectedSections = new List<SummaryTab>(SelectSectionsForOutput());

			if (!selectedSections.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Email...");
			FormProgress.ShowProgress();
			var previewGroups = selectedSections.Select(summaryTab => summaryTab.Content.GeneratePreview()).ToList();
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			FormProgress.CloseProgress();

			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;

			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
			{
				formEmail.Text = "Email Summary";
				formEmail.LoadGroups(previewGroups);
				Utilities.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
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