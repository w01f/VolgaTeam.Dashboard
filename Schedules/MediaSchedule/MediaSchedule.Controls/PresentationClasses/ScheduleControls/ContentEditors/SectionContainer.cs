﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Enums;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	//public partial class SectionContainer : UserControl
	public partial class SectionContainer : XtraTabPage
	{
		public ScheduleSection SectionData { get; private set; }

		private SectionControl _sectionControl;
		private CustomSummaryControl _customSummaryControl;
		private ProductSummaryControl _productSummaryControl;
		private StrategySummaryControl _strategySummaryControl;
		private bool _sectionDataChanged;

		public event EventHandler<EventArgs> DataChanged;
		public event EventHandler<EventArgs> SectionEditorChanged;

		public ISectionEditorControl ActiveEditor => (ISectionEditorControl)xtraTabControl.SelectedTabPage;

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
			}
		}

		public void InitControls()
		{
			_sectionControl = new SectionControl(this);
			_customSummaryControl = new CustomSummaryControl(this);
			_productSummaryControl = new ProductSummaryControl(this);
			_strategySummaryControl = new StrategySummaryControl(this);

			xtraTabControl.TabPages.AddRange(new XtraTabPage[]
			{
				_sectionControl,
				_customSummaryControl,
				_productSummaryControl,
				_strategySummaryControl
			});

			_sectionControl.InitControls();
			_customSummaryControl.InitControls();
			_productSummaryControl.InitControls();
			_strategySummaryControl.InitControls();

			xtraTabControl.SelectedPageChanged += OnSelectedSectionEditorChanged;
		}

		public void Release()
		{
			_sectionControl.Release();
			_sectionControl = null;

			_customSummaryControl.Release();
			_customSummaryControl = null;

			_productSummaryControl.Release();
			_productSummaryControl = null;

			_strategySummaryControl.Release();
			_strategySummaryControl = null;

			DataChanged = null;
			SectionEditorChanged = null;
			SectionData = null;
		}

		#region Section Data Management
		public void LoadData(ScheduleSection sectionData, bool quickLoad = false)
		{
			SectionData = sectionData;
			Text = SectionData.Name.Replace("&", "&&");
			SectionData.DataChanged += OnSectionDataChanged;

			_sectionControl.LoadData();
			_customSummaryControl.LoadData(quickLoad);
			_productSummaryControl.LoadData(quickLoad);
			_strategySummaryControl.LoadData();

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
					UpdateSummaryState();
					UpdateWarnings();
					break;
				case ScheduleSettingsType.CustomSummary:
					_customSummaryControl.UpdateTotalItems();
					break;
				case ScheduleSettingsType.ProductSummary:
					_productSummaryControl.UpdateTotalItems();
					break;
				case ScheduleSettingsType.Strategy:
					_strategySummaryControl.UpdateRows();
					break;
			}
		}

		public void RaiseDataChanged()
		{
			UpdateSummaryState();
			DataChanged?.Invoke(ActiveEditor, EventArgs.Empty);
		}

		private void OnSectionDataChanged(object sender, EventArgs e)
		{
			_sectionDataChanged = true;
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
				_productSummaryControl.LoadData();
				_strategySummaryControl.LoadData();
				_sectionDataChanged = false;
			}
			SectionEditorChanged?.Invoke(this, EventArgs.Empty);
			UpdateWarnings();
		}

		private void UpdateSummaryState()
		{
			_customSummaryControl.PageEnabled =
				_productSummaryControl.PageEnabled =
					SectionData.Programs.Any(p => p.TotalSpots > 0);
			_strategySummaryControl.PageEnabled =
				SectionData.ShowProgram &&
				SectionData.Programs.Any(p => p.TotalSpots > 0);
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
		public void UpdateSpotsByQuarter(Quarter selectedQuarter)
		{
			_sectionControl.UpdateSpotsByQuarter(selectedQuarter);
		}

		public void AddProgram()
		{
			_sectionControl.AddProgram();
		}

		public void DeleteProgram()
		{
			_sectionControl.DeleteProgram();
		}
		#endregion

		#region Output Stuff
		public ISectionOutputControl ActiveOutput => (ISectionOutputControl)xtraTabControl.SelectedTabPage;

		public bool ReadyForOutput => ActiveOutput.ReadyForOutput;

		public void GenerateOutput()
		{
			ActiveOutput.GenerateOutput();
		}

		public PreviewGroup GeneratePreview()
		{
			return ActiveOutput.GeneratePreview();
		}
		#endregion
	}
}
