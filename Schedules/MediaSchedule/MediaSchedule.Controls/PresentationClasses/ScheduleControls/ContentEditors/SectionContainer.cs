using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.BusinessClasses.Output.DigitalInfo;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.Output;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	//public partial class SectionContainer : UserControl
	public partial class SectionContainer : XtraTabPage
	{
		public ScheduleSection SectionData { get; private set; }

		private SectionControl _sectionControl;
		private SectionDigitalInfoControl _digitalInfoControl;
		private SectionSummaryControl _customSummaryControl;
		private bool _sectionDataChanged;

		public event EventHandler<EventArgs> DataChanged;
		public event EventHandler<EventArgs> SectionEditorChanged;

		public ISectionEditorControl ActiveEditor => (ISectionEditorControl)xtraTabControl.SelectedTabPage;
		public ISectionItemCollectionControl ActiveItemCollection => ActiveEditor as ISectionItemCollectionControl;

		#region Totals Calculation
		public string TotalPeriodsValueFormatted => SectionData.TotalActivePeriods.ToString("#,##0");

		public string TotalSpotsValueFormatted => SectionData.TotalSpots.ToString("#,##0");

		public string TotalGRPValueFormatted => SectionData.TotalGRP.ToString(
			SectionData.ParentScheduleSettings.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0");

		public string TotalCPPValueFormatted => SectionData.TotalCPP.ToString("$#,###.00");

		public string AvgRateValueFormatted => SectionData.AvgRate.ToString("$#,###.00");

		public string TotalCostValuesFormatted => SectionData.TotalCost.ToString(SectionData.UseDecimalRates ? "$#,##0.00" : "$#,##0");

		public string NetRateValueFormatted => SectionData.NetRate.ToString("$#,###.00");

		public string TotalDiscountValueFormatted => SectionData.Discount.ToString("$#,###.00");
		#endregion

		public SectionContainer()
		{
			InitializeComponent();
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
				labelControlWarnings.Font = font;
				labelControlCollectionItemsInfo.Font = font;
			}
		}

		public void InitControls()
		{
			_sectionControl = new SectionControl(this);
			_digitalInfoControl = new SectionDigitalInfoControl(this);
			_customSummaryControl = new SectionSummaryControl(this);

			xtraTabControl.TabPages.AddRange(new XtraTabPage[]
			{
				_sectionControl,
				_digitalInfoControl,
				_customSummaryControl,
			});

			_sectionControl.InitControls();
			_digitalInfoControl.InitControls();
			_customSummaryControl.InitControls();

			xtraTabControl.SelectedPageChanged += OnSelectedSectionEditorChanged;
		}

		public void Release()
		{
			_sectionControl.Release();
			_sectionControl = null;

			_digitalInfoControl.Release();
			_digitalInfoControl = null;

			_customSummaryControl.Release();
			_customSummaryControl = null;

			DataChanged = null;
			SectionEditorChanged = null;
			SectionData = null;
		}

		#region Section Data Management
		public void LoadData(ScheduleSection sectionData, bool quickLoad = false)
		{
			SectionData = sectionData;
			Text = SectionData.Name;
			SectionData.DataChanged += OnSectionDataChanged;

			_sectionControl.LoadData();
			_digitalInfoControl.LoadData();
			_customSummaryControl.LoadData(quickLoad);

			UpdateCollectionItemsInfo();
			UpdateSummaryState();
			UpdateWarnings();
		}

		public void SaveData()
		{
			ActiveEditor?.SaveData();
		}

		public void UpdateAccordingSettings(SettingsChangedEventArgs args)
		{
			switch (args.ChangedSettingsType)
			{
				case ScheduleSettingsType.Columns:
				case ScheduleSettingsType.Totals:
				case ScheduleSettingsType.AdvancedColumns:
					_sectionDataChanged = true;
					_sectionControl.UpdateGridView();
					if (args.UpdateGridColums)
						_sectionControl.UpdateGridData(true);
					_sectionControl.UpdateSpotsByQuarter();
					UpdateSummaryState();
					UpdateWarnings();
					break;
				case ScheduleSettingsType.Quarters:
					_sectionControl.UpdateSpotsByQuarter();
					break;
				case ScheduleSettingsType.DigitalInfo:
					_digitalInfoControl.UpdateGridView();
					break;
			}
		}

		public void RaiseDataChanged()
		{
			_sectionDataChanged = true;
			UpdateCollectionItemsInfo();
			UpdateSummaryState();
			DataChanged?.Invoke(ActiveEditor, EventArgs.Empty);
		}

		private void OnSectionDataChanged(object sender, EventArgs e)
		{
			RaiseDataChanged();
		}

		private void OnSelectedSectionEditorChanged(object sender, TabPageChangedEventArgs e)
		{
			var previuseEditor = e.PrevPage as ISectionEditorControl;
			previuseEditor?.SaveData();
			if (_sectionDataChanged)
			{
				SectionData.Summary.SynchronizeSectionContent();
				_customSummaryControl.LoadData();
				_sectionDataChanged = false;
			}
			UpdateCollectionItemsInfo();
			UpdateWarnings();
			SectionEditorChanged?.Invoke(this, EventArgs.Empty);
		}

		private void UpdateCollectionItemsInfo()
		{
			switch (ActiveEditor?.EditorType)
			{
				case SectionEditorType.Schedule:
					labelControlCollectionItemsInfo.Visible = true;
					labelControlCollectionItemsInfo.Text = String.Format("<color=gray>Total Programs: {0}</color>", SectionData.Programs.Count);
					break;
				case SectionEditorType.DigitalInfo:
					labelControlCollectionItemsInfo.Visible = true;
					if (SectionData.DigitalInfo.Records.Count < BaseDigitalInfoOneSheetOutputModel.MaxRecords)
						labelControlCollectionItemsInfo.Text = String.Format("<color=gray>DIGITAL Marketing Products: {0}</color>", SectionData.DigitalInfo.Records.Count);
					else
						labelControlCollectionItemsInfo.Text = "<color=red>Maximum DIGITAL Marketing Products: <b><u>6</u></b></color>";
					break;
				case SectionEditorType.CustomSummary:
					labelControlCollectionItemsInfo.Visible = true;
					labelControlCollectionItemsInfo.Text = String.Format("<color=gray>Summary Items: {0}</color>", SectionData.Summary.CustomSummary.Items.Count);
					break;
				default:
					labelControlCollectionItemsInfo.Visible = false;
					break;
			}
		}

		private void UpdateSummaryState()
		{
			_customSummaryControl.PageEnabled =
				SectionData.Programs.Any(p => p.TotalSpots > 0) || SectionData.DigitalInfo.Records.Any();
		}

		private void UpdateWarnings()
		{
			var warnings = new List<string>();
			if (ActiveEditor == _sectionControl)
			{
				if (SectionData.UseGenericDateColumns)
					warnings.Add(String.Format("*Generic {0}s", SectionData.Parent.ScheduleSettings.SelectedSpotType));
				if (SectionData.UseDecimalRates)
					warnings.Add("*Decimals Enabled");
			}
			labelControlWarnings.Text = String.Join("    ", warnings);
		}
		#endregion

		#region Schedule Management
		public void AddItem()
		{
			ActiveItemCollection?.AddItem();
		}

		public void DeleteItem()
		{
			ActiveItemCollection?.DeleteItem();
		}
		#endregion

		#region Output Stuff
		public bool ReadyForOutput => GetAvailableOutputOptions().Any(option => option == ScheduleSectionOutputType.Program || option == ScheduleSectionOutputType.DigitalOneSheet);

		public void OutputPowerPoint()
		{
			var availableOptions = SelectedOutputOptions().ToList();
			if (!availableOptions.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				GenerateOutput(availableOptions);
				FormProgress.CloseProgress();
			});
		}

		public void OutputPdf()
		{
			var availableOptions = SelectedOutputOptions().ToList();
			if (!availableOptions.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var previewGroups = GeneratePreview(availableOptions);
				var pdfFileName = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					String.Format("{0}-{1}.pdf", SectionData.ParentSchedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
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

		public void Preview()
		{
			var availableOptions = SelectedOutputOptions().ToList();
			if (!availableOptions.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var previewGroups = GeneratePreview(availableOptions).ToList();
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

		public void Email()
		{
			var availableOptions = SelectedOutputOptions().ToList();
			if (!availableOptions.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var previewGroups = GeneratePreview(availableOptions).ToList();
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

		private IEnumerable<ScheduleSectionOutputType> GetAvailableOutputOptions()
		{
			return xtraTabControl.TabPages.OfType<ISectionOutputControl>()
				.SelectMany(sectionOutput => sectionOutput.GetAvailableOutputOptions())
				.Distinct();
		}

		private IEnumerable<ScheduleSectionOutputType> SelectedOutputOptions()
		{
			var availableOptions = GetAvailableOutputOptions();
			using (var form = new FormConfigureOutput(SectionData.Name, availableOptions))
			{
				if (form.ShowDialog(Controller.Instance.FormMain) == DialogResult.OK)
				{
					return form.GetSelectedOutputTypes();
				}
			}
			return new ScheduleSectionOutputType[] { };
		}

		private void GenerateOutput(IEnumerable<ScheduleSectionOutputType> selectedOutputOptions)
		{
			foreach (var outputOption in selectedOutputOptions)
			{
				switch (outputOption)
				{
					case ScheduleSectionOutputType.Program:
					case ScheduleSectionOutputType.ProgramAndDigital:
						_sectionControl.GenerateOutput(outputOption == ScheduleSectionOutputType.ProgramAndDigital);
						break;
					case ScheduleSectionOutputType.DigitalOneSheet:
						_digitalInfoControl.GenerateOneSheetOutput();
						break;
					case ScheduleSectionOutputType.DigitalStrategy:
						_digitalInfoControl.GenerateStrategyOutput();
						break;
					case ScheduleSectionOutputType.Summary:
						_customSummaryControl.GenerateOutput();
						break;
				}
			}
		}

		private IEnumerable<PreviewGroup> GeneratePreview(IEnumerable<ScheduleSectionOutputType> selectedOutputOptions)
		{
			var previewGroups = new List<PreviewGroup>();

			foreach (var outputOption in selectedOutputOptions)
			{
				switch (outputOption)
				{
					case ScheduleSectionOutputType.Program:
					case ScheduleSectionOutputType.ProgramAndDigital:
						previewGroups.Add(_sectionControl.GeneratePreview(outputOption == ScheduleSectionOutputType.ProgramAndDigital));
						break;
					case ScheduleSectionOutputType.DigitalOneSheet:
						previewGroups.Add(_digitalInfoControl.GenerateOneSheetPreview());
						break;
					case ScheduleSectionOutputType.DigitalStrategy:
						previewGroups.Add(_digitalInfoControl.GenerateStrategyPreview());
						break;
					case ScheduleSectionOutputType.Summary:
						previewGroups.Add(_customSummaryControl.GeneratePreview());
						break;
				}
			}

			return previewGroups;
		}
		#endregion
	}
}
