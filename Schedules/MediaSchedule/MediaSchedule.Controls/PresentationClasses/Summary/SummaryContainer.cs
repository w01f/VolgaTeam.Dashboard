using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.CommonGUI.Preview;
using Asa.CommonGUI.RetractableBar;
using Asa.CommonGUI.Summary;
using Asa.CommonGUI.Themes;
using Asa.CommonGUI.ToolForms;
using Asa.Core.Common;
using Asa.Core.MediaSchedule;
using Asa.MediaSchedule.Controls.BusinessClasses;
using Asa.MediaSchedule.Controls.InteropClasses;
using Asa.MediaSchedule.Controls.Properties;
using DevExpress.XtraEditors.Controls;

namespace Asa.MediaSchedule.Controls.PresentationClasses.Summary
{
	[ToolboxItem(false)]
	public partial class SummaryContainer : UserControl
	{
		private bool _allowToSave;
		private RegularSchedule _localSchedule;
		private readonly SummaryTypeSettingsControl _summaryTypeTab;
		private readonly ButtonInfo _summaryTypeButton;

		#region Properties
		public bool SettingsNotSaved { get; set; }

		public bool AllowToLeaveControl
		{
			get
			{
				if (SettingsNotSaved)
					SaveSchedule();
				return true;
			}
		}

		private SummaryTab ActiveSummary
		{
			get { return (SummaryTab)xtraTabControlSections.SelectedTabPage; }
		}
		#endregion

		public SummaryContainer()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
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

			BusinessObjects.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) =>
			{
				InitThemeSelector();
				Controller.Instance.SummaryThemeBar.RecalcLayout();
				Controller.Instance.SummaryPanel.PerformLayout();
			};
		}

		#region Schedule Management

		public void LoadSchedule(bool quickLoad)
		{
			_allowToSave = false;

			_localSchedule = BusinessObjects.Instance.ScheduleManager.GetLocalSchedule();

			InitThemeSelector();

			LoadSections(quickLoad);

			hyperLinkEditInfoContract.Enabled = BusinessObjects.Instance.OutputManager.ContractTemplateFolder.ExistsLocal();

			LoadActiveSectionData();

			UpdateOutputStatus();

			_allowToSave = true;
			SettingsNotSaved = false;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			xtraTabControlSections.TabPages
				.OfType<SummaryTab>()
				.ToList()
				.ForEach(summaryTab => summaryTab.SaveData());
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (!SettingsNotSaved && !nameChanged) return true;
			if (nameChanged)
				_localSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(_localSchedule, nameChanged, true, false, false, this);
			SettingsNotSaved = false;
			return true;
		}
		#endregion

		#region Sections Management
		private void LoadSections(bool quickLoad)
		{
			if (!quickLoad)
				xtraTabControlSections.TabPages.Clear();
			foreach (var section in _localSchedule.ProgramSchedule.Sections.OrderBy(s => s.Index))
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
			if (ActiveSummary!= null && 
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
		private void InitThemeSelector()
		{
			FormThemeSelector.Link(Controller.Instance.SummaryTheme, BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.Summaries), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Summaries), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType.Summaries, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				SettingsNotSaved = true;
			}));
		}

		private void LoadSummarySettingsTabs()
		{
			var currentTabIndex = xtraTabControlOptions.SelectedTabPageIndex;
			xtraTabControlOptions.TabPages.Clear();
			xtraTabControlOptions.TabPages.Add(_summaryTypeTab);
			xtraTabControlOptions.TabPages.AddRange(ActiveSummary.Content.SettingsPages.ToArray());
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

		#region Ribbon Operations Events
		public void Help_Click(object sender, EventArgs e)
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

		public void Save_Click(object sender, EventArgs e)
		{
			if (SaveSchedule())
				Utilities.Instance.ShowInformation("Schedule Saved");
		}

		public void SaveAs_Click(object sender, EventArgs e)
		{
			using (var form = new FormNewSchedule(ScheduleManager.GetShortScheduleList().Select(s => s.ShortFileName)))
			{
				form.Text = "Save Schedule";
				form.laLogo.Text = "Please set a new name for your Schedule:";
				if (form.ShowDialog() != DialogResult.OK) return;
				if (!string.IsNullOrEmpty(form.ScheduleName))
				{
					if (SaveSchedule(form.ScheduleName))
						Utilities.Instance.ShowInformation("Schedule was saved");
				}
				else
					Utilities.Instance.ShowWarning("Schedule Name can't be empty");
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

		public void OnPowerPointOutput(object sender, EventArgs e)
		{
			SaveSchedule();
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

		public void OnOutputPreview(object sender, EventArgs e)
		{
			SaveSchedule();
			var selectedSections = new List<SummaryTab>(SelectSectionsForOutput());

			if (!selectedSections.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var previewGroups = selectedSections.Select(summaryTab => summaryTab.Content.GeneratePreview()).ToList();
			Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
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
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			}
		}

		public void OnEmailOutput(object sender, EventArgs e)
		{
			SaveSchedule();
			var selectedSections = new List<SummaryTab>(SelectSectionsForOutput());

			if (!selectedSections.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Email...");
			FormProgress.ShowProgress();
			var previewGroups = selectedSections.Select(summaryTab => summaryTab.Content.GeneratePreview()).ToList();
			Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			FormProgress.CloseProgress();

			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;

			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
			{
				formEmail.Text = "Email Summary";
				formEmail.LoadGroups(previewGroups);
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}
		}

		public void OnPdfOutput(object sender, EventArgs e)
		{
			SaveSchedule();
			var selectedSections = new List<SummaryTab>(SelectSectionsForOutput());

			if (!selectedSections.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var previewGroups = selectedSections.Select(summaryTab => summaryTab.Content.GeneratePreview()).ToList();
				var pdfFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1}.pdf", _localSchedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
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
		#endregion
	}
}