using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.ImageGallery;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.Output;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors
{
	[ToolboxItem(false)]
	//public sealed partial class SnapshotSummaryEditorControl : UserControl
	public sealed partial class SnapshotSummaryEditorControl : XtraTabPage,
		ISnapshotContentEditorControl,
		ISnapshotEditorControl,
		IOutputContainer,
		IOutputItem,
		ISnapshotSlideData
	{
		public SnapshotSummary Data { get; private set; }
		public SnapshotEditorType EditorType => SnapshotEditorType.Summary;
		public ISnapshotEditorControl ActiveEditor => this;
		public ISnapshotCollectionEditorControl ActiveItemCollection => null;
		public event EventHandler<EventArgs> DataChanged;

		public SnapshotSummaryEditorControl()
		{
			InitializeComponent();
			Text = String.Format("Summary ({0})", MediaMetaData.Instance.DataTypeString);
		}

		public void InitControls()
		{
			ShowCloseButton = DefaultBoolean.False;
		}

		public void LoadData(SnapshotSummary data)
		{
			Data = data;
			UpdateView();
		}

		public void SaveData()
		{
			advBandedGridView.CloseEditor();
		}

		public void Release()
		{
			gridControl.DataSource = null;
			DataChanged = null;
			Data = null;
		}

		public void UpdateAccordingSettings(SettingsChangedEventArgs eventArgs)
		{
			if (eventArgs.ChangedSettingsType != SnapshotSettingsType.Summary) return;
			UpdateView();
		}

		public void UpdateView(bool focus = false)
		{
			gridControl.DataSource = null;
			gridControl.DataSource = Data.Parent.Snapshots;
			gridControl.RefreshDataSource();
			advBandedGridView.RefreshData();
			if (focus)
				advBandedGridView.Focus();

			gridBandId.Visible = Data.ShowLineId;
			gridBandLogo.Visible = Data.ShowLogo;
			gridBandOtherColumns.Visible = Data.ShowCampaign || Data.ShowComments || Data.ShowSpots || Data.ShowCost || Data.ShowTotalWeeks || Data.ShowTotalCost;
			bandedGridColumnName.Visible = Data.ShowCampaign;
			bandedGridColumnComment.Visible = Data.ShowComments;
			bandedGridColumnSpots.Visible = Data.ShowSpots;
			bandedGridColumnRate.Visible = Data.ShowCost;
			bandedGridColumnTotalWeeks.Visible = Data.ShowTotalWeeks;
			bandedGridColumnCost.Visible = Data.ShowTotalCost;
			if (Data.UseDecimalRates)
			{
				bandedGridColumnRate.DisplayFormat.FormatString = "$#,##0.00";
				bandedGridColumnCost.DisplayFormat.FormatString = "$#,##0.00";
				repositoryItemSpinEditRate.DisplayFormat.FormatString = "$#,##0.00";
				repositoryItemSpinEditRate.EditFormat.FormatString = "$#,##0.00";
				repositoryItemSpinEditRate.IsFloatValue = true;
			}
			else
			{
				bandedGridColumnRate.DisplayFormat.FormatString = "$#,##0";
				bandedGridColumnCost.DisplayFormat.FormatString = "$#,##0";
				repositoryItemSpinEditRate.DisplayFormat.FormatString = "$#,##0";
				repositoryItemSpinEditRate.EditFormat.FormatString = "$#,##0";
				repositoryItemSpinEditRate.IsFloatValue = false;
			}
		}

		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			advBandedGridView.CloseEditor();
			advBandedGridView.FocusedColumn = null;
		}

		#region Grid Event Handlers
		private void OnGridViewCellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			advBandedGridView.CloseEditor();
			advBandedGridView.UpdateCurrentRow();
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnGridViewMouseDown(object sender, MouseEventArgs e)
		{
			var view = sender as AdvBandedGridView;
			if (view == null) return;
			var hInfo = view.CalcHitInfo(e.Location);
			if (hInfo.HitTest != BandedGridHitTest.RowCell)
				CloseActiveEditorsonOutSideClick(null, null);
		}

		private void OnGridViewRowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Column != bandedGridColumnLogo) return;
			if (e.Clicks < 2) return;
			var selectedProgram = advBandedGridView.GetFocusedRow() as Snapshot;
			if (selectedProgram == null) return;
			using (var form = new FormImageGallery(MediaMetaData.Instance.ListManager.Images))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (form.SelectedImageSource == null) return;
				selectedProgram.Logo = form.SelectedImageSource.Clone<ImageSource, ImageSource>();
				advBandedGridView.UpdateCurrentRow();
				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		private void OnTooltipGetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			if (e.SelectedControl != gridControl) return;
			var view = gridControl.GetViewAt(e.ControlMousePosition) as GridView;
			if (view == null) return;
			var hi = view.CalcHitInfo(e.ControlMousePosition);
			if (!hi.InRowCell) return;
			if (hi.Column != bandedGridColumnLogo) return;
			e.Info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), "Double-Click to change the logo…");
			e.Info.ImmediateToolTip = true;
			e.Info.Interval = 0;
		}
		#endregion

		#region Output
		public string OutputName => String.Format("Summary ({0})", MediaMetaData.Instance.DataTypeString);

		public SlideType SlideType => MediaMetaData.Instance.DataType == MediaDataType.TVSchedule
			? SlideType.TVSnapshotSummary
			: SlideType.RadioSnapshotSummary;

		private Theme SelectedTheme
		{
			get
			{
				var selectedTheme = MediaMetaData.Instance.SettingsManager.GetSelectedThemeName(SlideType);
				return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType).FirstOrDefault(t => t.Name.Equals(selectedTheme) || String.IsNullOrEmpty(selectedTheme));
			}
		}

		public string TemplateFilePath => BusinessObjects.Instance.OutputManager.GetSnapshotSummaryFile(
			MediaMetaData.Instance.SettingsManager.SelectedColor ?? BusinessObjects.Instance.OutputManager.ScheduleColors.Items.Select(ci => ci.Name).FirstOrDefault(),
			GetColumnInfo().Count());

		public string TotalRowValue => String.Empty;

		public string[][] Logos { get; set; }
		public ContractSettings ContractSettings => Data.ContractSettings;
		public List<Dictionary<string, string>> ReplacementsList { get; private set; }

		private int ProgramsPerSlide => 6;

		public OutputGroup GetOutputGroup()
		{
			return new OutputGroup(this)
			{
				Name = OutputName,
				IsCurrent = TabControl.SelectedTabPage == this,
				Configurations = GetOutputConfigurations().ToArray()
			};
		}

		public IList<OutputConfiguration> GetOutputConfigurations()
		{
			var outputConfigurations = new List<OutputConfiguration>();
			if (Data.Parent.Snapshots.Any(s => s.Programs.Any()))
				outputConfigurations.Add(new OutputConfiguration(
					SnapshotOutputType.Summary,
					Data.Parent.Snapshots.Count / ProgramsPerSlide + (Data.Parent.Snapshots.Count % ProgramsPerSlide > 0 ? 1 : 0)
					));
			return outputConfigurations;
		}

		private string[][] GetLogos()
		{
			var logos = new List<string[]>();
			var logosOnSlide = new List<string>();
			var progarmsCount = Data.Parent.Snapshots.Count;
			for (var i = 0; i < progarmsCount; i += ProgramsPerSlide)
			{
				logosOnSlide.Clear();
				for (int j = 0; j < ProgramsPerSlide; j++)
				{
					var fileName = String.Empty;
					if (Data.ShowLogo && (i + j) < progarmsCount)
					{
						var snapshot = Data.Parent.Snapshots[i + j];
						if (snapshot.Logo != null && snapshot.Logo.ContainsData)
						{
							fileName = Path.GetTempFileName();
							snapshot.Logo.SmallImage.Save(fileName);
						}
					}
					logosOnSlide.Add(fileName);
				}
				logos.Add(logosOnSlide.ToArray());
			}
			return logos.ToArray();
		}

		private IEnumerable<OutputColumnInfo> GetColumnInfo()
		{
			var columnInfoList = new List<OutputColumnInfo>();
			var i = 0;
			if (Data.ShowSpots)
			{
				columnInfoList.Add(new SpotsColumnInfo { Index = i });
				i++;
			}
			if (Data.ShowCost)
			{
				columnInfoList.Add(new RateColumnInfo { Index = i });
				i++;
			}
			if (Data.ShowTotalWeeks)
			{
				columnInfoList.Add(new TotalWeeksColumnInfo { Index = i });
				i++;
			}
			if (Data.ShowTotalCost)
			{
				columnInfoList.Add(new CostColumnInfo { Index = i });
				i++;
			}
			return columnInfoList;
		}

		private void PopulateReplacementsList()
		{
			var key = string.Empty;
			var value = string.Empty;
			var temp = new List<string>();
			ReplacementsList = new List<Dictionary<string, string>>();
			var snapshotCount = Data.Parent.Snapshots.Count;

			for (var i = 0; i < snapshotCount; i += ProgramsPerSlide)
			{
				var pageDictionary = new Dictionary<string, string>();
				key = "Flightdates";
				value = Data.Parent.ScheduleSettings.FlightDates;
				if (!pageDictionary.Keys.Contains(key))
					pageDictionary.Add(key, value);
				key = "Advertiser - Decisionmaker";
				value = String.Format("{0}  -  {1}", Data.Parent.ScheduleSettings.BusinessName, Data.Parent.ScheduleSettings.DecisionMaker);
				if (!pageDictionary.Keys.Contains(key))
					pageDictionary.Add(key, value);

				key = "tallyinfo";
				if (Data.ShowTallySpots || Data.ShowTotalCost || Data.ShowTallyCost)
				{
					temp.Clear();
					if (Data.ShowTallySpots)
						temp.Add(String.Format("Total Spots: {0}", String.Format("{0}{1}", Data.TotalSpots.ToString("#,###"), Data.ShowSpotsX ? "x" : String.Empty)));
					if (Data.ShowTotalCost)
						temp.Add(String.Format("Total Cost: {0}", Data.TotalCost.ToString(Data.UseDecimalRates ? "$#,##0.00" : "$#,##0")));
					value = String.Join("     ", temp);
				}
				else
					value = String.Empty;
				if (!pageDictionary.Keys.Contains(key))
					pageDictionary.Add(key, value);

				var dynamicColumnInfoList = GetColumnInfo().OrderBy(ci => ci.Index).ToList();
				var columHeaderTags = new[] { "upper_a lower_a", "upper_b lower_b", "upper_c lower_c", "upper_d lower_d" };
				var columValuesTags = new[] { "item{0}a", "item{0}b", "item{0}c", "item{0}d" };
				var columnIndex = 0;
				foreach (var outputColumnInfo in dynamicColumnInfoList)
				{
					key = columHeaderTags[columnIndex];
					value = outputColumnInfo.HeaderCaption;
					if (!pageDictionary.Keys.Contains(key))
						pageDictionary.Add(key, value);
					columnIndex++;
				}

				for (int j = 0; j < ProgramsPerSlide; j++)
				{
					key = (j + 1).ToString("00");
					if ((i + j) < snapshotCount)
					{
						var snapshot = Data.Parent.Snapshots[i + j];
						value = Data.ShowLineId ? snapshot.DisplayIndex.ToString("00") : "Delete Column";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("campaign{0}", j + 1);
						value = Data.ShowCampaign ? snapshot.Name : String.Empty;
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("comments{0}", j + 1);
						value = Data.ShowComments ? snapshot.Comment : String.Empty;
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						columnIndex = 0;
						foreach (var outputColumnInfo in dynamicColumnInfoList)
						{
							key = String.Format(columValuesTags[columnIndex], j + 1);
							value = outputColumnInfo.GetValue(snapshot);
							if (!pageDictionary.Keys.Contains(key))
								pageDictionary.Add(key, value);
							columnIndex++;
						}
					}
					else
					{
						value = "Delete Row";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);
					}
				}
				ReplacementsList.Add(pageDictionary);
			}
		}

		public void GenerateOutput(IList<OutputConfiguration> configurations)
		{
			if (!configurations.Any()) return;
			Logos = GetLogos();
			PopulateReplacementsList();
			RegularMediaSchedulePowerPointHelper.Instance.AppendSnapshot(new[] { this }, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
		}

		public IList<PreviewGroup> GeneratePreview(IList<OutputConfiguration> configurations)
		{
			var groupList = new List<PreviewGroup>();
			if (!configurations.Any())
				return groupList;

			Logos = GetLogos();
			PopulateReplacementsList();
			var previewGroup = new PreviewGroup
			{
				Name = OutputName,
				PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
			RegularMediaSchedulePowerPointHelper.Instance.PrepareSnapshotEmail(previewGroup.PresentationSourcePath, new[] { this }, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);

			groupList.Add(previewGroup);
			return groupList;
		}

		internal abstract class OutputColumnInfo
		{
			public abstract string HeaderCaption { get; }
			public int Index { get; set; }

			public abstract string GetValue(Snapshot snapshot);
		}

		internal class SpotsColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption => String.Format("Weekly{0}Spots", (char)13);

			public override string GetValue(Snapshot snapshot)
			{
				return String.Format("{0}{1}", snapshot.WeeklySpots.ToString("#,##0"), snapshot.Parent.SnapshotSummary.ShowSpotsX ? "x" : String.Empty);
			}
		}

		internal class RateColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption => String.Format("Weekly{0}Cost", (char)13);

			public override string GetValue(Snapshot snapshot)
			{
				return snapshot.WeeklyCost.ToString(snapshot.Parent.SnapshotSummary.UseDecimalRates ? "$#,##0.00" : "$#,##0");
			}
		}

		internal class TotalWeeksColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption => String.Format("Total{0}Weeks", (char)13);

			public override string GetValue(Snapshot snapshot)
			{
				return snapshot.TotalWeeks.ToString("#,##0");
			}
		}

		internal class CostColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption => "Cost";

			public override string GetValue(Snapshot snapshot)
			{
				return snapshot.TotalCost.ToString(snapshot.Parent.SnapshotSummary.UseDecimalRates ? "$#,##0.00" : "$#,##0");
			}
		}
		#endregion
	}
}
