using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using NewBizWiz.CommonGUI.ImageGallery;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.OptionsControls
{
	[ToolboxItem(false)]
	//public sealed partial class OptionsSummaryControl : UserControl
	public sealed partial class OptionsSummaryControl : XtraTabPage, IOptionsSlide
	{
		public OptionSummary Data { get; private set; }

		public event EventHandler<EventArgs> DataChanged;

		public OptionsSummaryControl(OptionSummary data)
		{
			InitializeComponent();
			ShowCloseButton = DefaultBoolean.False;
			Text = SlideName;
			LoadData(data);
		}

		public void LoadData(OptionSummary data)
		{
			Data = data;
			UpdateView();
		}

		public void SaveData()
		{
			advBandedGridView.CloseEditor();
		}

		public void UpdateView(bool focus = false)
		{
			Data.UpdateSpotType();
			
			gridControl.DataSource = null;
			gridControl.DataSource = Data.Parent.Options;
			gridControl.RefreshDataSource();
			advBandedGridView.RefreshData();
			if (focus)
				advBandedGridView.Focus();

			PageEnabled = Data.Enabled;

			gridBandId.Visible = Data.ShowLineId;
			gridBandLogo.Visible = Data.ShowLogo;
			gridBandOtherColumns.Visible = Data.ShowCampaign || Data.ShowComments || Data.ShowSpots || Data.ShowCost || Data.ShowTotalPeriods || Data.ShowTotalCost;
			bandedGridColumnName.Visible = Data.ShowCampaign;
			bandedGridColumnComment.Visible = Data.ShowComments;
			bandedGridColumnSpots.Visible = Data.ShowSpots;
			bandedGridColumnCost.Visible = Data.ShowCost;
			bandedGridColumnTotalPeriods.Visible = Data.ShowTotalPeriods;
			bandedGridColumnPeriodCost.Visible = Data.ShowTotalCost;
			switch (Data.SpotType)
			{
				case SpotType.Week:
					bandedGridColumnSpots.Caption = String.Format("Weekly{0}Spots", Environment.NewLine);
					bandedGridColumnCost.Caption = String.Format("Weekly{0}Cost", Environment.NewLine);
					bandedGridColumnTotalPeriods.Caption = String.Format("Total{0}Weeks", Environment.NewLine);
					break;
				case SpotType.Month:
					bandedGridColumnSpots.Caption = String.Format("Monthly{0}Spots", Environment.NewLine);
					bandedGridColumnCost.Caption = String.Format("Monthly{0}Cost", Environment.NewLine);
					bandedGridColumnTotalPeriods.Caption = String.Format("Total{0}Months", Environment.NewLine);
					break;
				case SpotType.Total:
					bandedGridColumnSpots.Caption = String.Format("Total{0}Spots", Environment.NewLine);
					break;
			}
			if (Data.UseDecimalRates)
			{
				bandedGridColumnCost.DisplayFormat.FormatString = "$#,##0.00";
				bandedGridColumnPeriodCost.DisplayFormat.FormatString = "$#,##0.00";
				repositoryItemSpinEditRate.DisplayFormat.FormatString = "$#,##0.00";
				repositoryItemSpinEditRate.EditFormat.FormatString = "$#,##0.00";
				repositoryItemSpinEditRate.IsFloatValue = true;
			}
			else
			{
				bandedGridColumnCost.DisplayFormat.FormatString = "$#,##0";
				bandedGridColumnPeriodCost.DisplayFormat.FormatString = "$#,##0";
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
		private void advBandedGridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			advBandedGridView.CloseEditor();
			advBandedGridView.UpdateCurrentRow();
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void advBandedGridView_MouseDown(object sender, MouseEventArgs e)
		{
			var view = sender as AdvBandedGridView;
			if (view == null) return;
			var hInfo = view.CalcHitInfo(e.Location);
			if (hInfo.HitTest != BandedGridHitTest.RowCell)
				CloseActiveEditorsonOutSideClick(null, null);
		}

		private void advBandedGridView_RowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Column != bandedGridColumnLogo) return;
			var selectedProgram = advBandedGridView.GetFocusedRow() as OptionSet;
			if (selectedProgram == null) return;
			using (var form = new FormImageGallery(MediaMetaData.Instance.ListManager.Images))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (form.SelectedImageSource == null) return;
				selectedProgram.Logo = form.SelectedImageSource;
				advBandedGridView.UpdateCurrentRow();
				if (DataChanged != null)
					DataChanged(this, EventArgs.Empty);
			}
		}
		#endregion

		#region Output
		public bool ReadyForOutput
		{
			get { return Data.Enabled && Data.Parent.Options.Any(s => s.Programs.Any()); }
		}

		public string SlideName
		{
			get { return "Summary Slide"; }
		}

		public string TemplateFileName
		{
			get
			{
				return String.Format(OutputManager.OptionsSummaryTemplateFileName,
						MediaMetaData.Instance.SettingsManager.SelectedColor,
						GetColumnInfo().Count());
			}
		}

		public string[][] Logos { get; set; }
		public float[] ColumnWidths { get; set; }
		public List<Dictionary<string, string>> ReplacementsList { get; private set; }

		private int ProgramsPerSlide
		{
			get { return 6; }
		}

		private string[][] GetLogos()
		{
			var logos = new List<string[]>();
			var logosOnSlide = new List<string>();
			var progarmsCount = Data.Parent.Options.Count;
			for (var i = 0; i < progarmsCount; i += ProgramsPerSlide)
			{
				logosOnSlide.Clear();
				for (int j = 0; j < ProgramsPerSlide; j++)
				{
					var fileName = String.Empty;
					if (Data.ShowLogo && (i + j) < progarmsCount)
					{
						var optionSet = Data.Parent.Options[i + j];
						if (optionSet.Logo != null && optionSet.Logo.ContainsData)
						{
							fileName = Path.GetTempFileName();
							optionSet.Logo.SmallImage.Save(fileName);
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
				columnInfoList.Add(new SpotsColumnInfo(Data.SpotType) { Index = i });
				i++;
			}
			if (Data.ShowCost)
			{
				columnInfoList.Add(new CostColumnInfo(Data.SpotType) { Index = i });
				i++;
			}
			if (Data.ShowTotalPeriods)
			{
				columnInfoList.Add(new TotalPeriodsColumnInfo(Data.SpotType) { Index = i });
				i++;
			}
			if (Data.ShowTotalCost)
			{
				columnInfoList.Add(new PeriodCostColumnInfo { Index = i });
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
			var optionsCount = Data.Parent.Options.Count;

			for (var i = 0; i < optionsCount; i += ProgramsPerSlide)
			{
				var pageDictionary = new Dictionary<string, string>();
				key = "Flightdates";
				value = Data.Parent.FlightDates;
				if (!pageDictionary.Keys.Contains(key))
					pageDictionary.Add(key, value);
				key = "Advertiser - Decisionmaker";
				value = String.Format("{0}  -  {1}", Data.Parent.BusinessName, Data.Parent.DecisionMaker);
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
					if ((i + j) < optionsCount)
					{
						var optionSet = Data.Parent.Options[i + j];
						value = Data.ShowLineId ? optionSet.DisplayIndex.ToString("00") : "Delete Column";
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("campaign{0}", j + 1);
						value = Data.ShowCampaign ? optionSet.Name : String.Empty;
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						key = String.Format("comments{0}", j + 1);
						value = Data.ShowComments ? optionSet.Comment : String.Empty;
						if (!pageDictionary.Keys.Contains(key))
							pageDictionary.Add(key, value);

						columnIndex = 0;
						foreach (var outputColumnInfo in dynamicColumnInfoList)
						{
							key = String.Format(columValuesTags[columnIndex], j + 1);
							value = outputColumnInfo.GetValue(optionSet);
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

		public PreviewGroup GetPreviewGroup(Theme selectedTheme)
		{
			Logos = GetLogos();
			PopulateReplacementsList();
			var previewGroup = new PreviewGroup
			{
				Name = SlideName,
				PresentationSourcePath = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()))
			};
			RegularMediaSchedulePowerPointHelper.Instance.PrepareOptionsEmail(previewGroup.PresentationSourcePath, new[] { this }, selectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
			return previewGroup;
		}

		public void Output(Theme selectedTheme)
		{
			Logos = GetLogos();
			PopulateReplacementsList();
			RegularMediaSchedulePowerPointHelper.Instance.AppendOptions(new[] { this }, selectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
		}

		internal abstract class OutputColumnInfo
		{
			public abstract string HeaderCaption { get; }
			public int Index { get; set; }

			public abstract string GetValue(OptionSet optionSet);
		}

		internal class SpotsColumnInfo : OutputColumnInfo
		{
			private readonly SpotType _spotType;
			public override string HeaderCaption
			{
				get
				{
					switch (_spotType)
					{
						case SpotType.Week:
							return String.Format("Weekly{0}Spots", (char)13);
						case SpotType.Month:
							return String.Format("Monthly{0}Spots", (char)13);
						case SpotType.Total:
							return String.Format("Total{0}Spots", (char)13);
						default:
							return String.Empty;
					}
				}
			}

			public SpotsColumnInfo(SpotType spotType)
			{
				_spotType = spotType;
			}

			public override string GetValue(OptionSet optionSet)
			{
				return String.Format("{0}{1}", optionSet.TotalSpots.ToString("#,##0"), optionSet.Parent.OptionsSummary.ShowSpotsX ? "x" : String.Empty);
			}
		}

		internal class CostColumnInfo : OutputColumnInfo
		{
			private readonly SpotType _spotType;
			public override string HeaderCaption
			{
				get
				{
					switch (_spotType)
					{
						case SpotType.Week:
							return String.Format("Weekly{0}Cost", (char)13);
						case SpotType.Month:
							return String.Format("Monthly{0}Cost", (char)13);
						case SpotType.Total:
							return String.Format("Total{0}Cost", (char)13);
						default:
							return String.Empty;
					}
				}
			}

			public CostColumnInfo(SpotType spotType)
			{
				_spotType = spotType;
			}

			public override string GetValue(OptionSet optionSet)
			{
				return optionSet.TotalCost.ToString(optionSet.Parent.OptionsSummary.UseDecimalRates ? "$#,##0.00" : "$#,##0");
			}
		}

		internal class TotalPeriodsColumnInfo : OutputColumnInfo
		{
			private readonly SpotType _spotType;
			public override string HeaderCaption
			{
				get
				{
					switch (_spotType)
					{
						case SpotType.Week:
							return String.Format("Total{0}Weeks", (char)13);
						case SpotType.Month:
							return String.Format("Total{0}Months", (char)13);
						default:
							return String.Empty;
					}
				}
			}

			public TotalPeriodsColumnInfo(SpotType spotType)
			{
				_spotType = spotType;
			}

			public override string GetValue(OptionSet optionSet)
			{
				return (optionSet.TotalPeriods.HasValue ? optionSet.TotalPeriods.Value : 0).ToString("#,##0");
			}
		}

		internal class PeriodCostColumnInfo : OutputColumnInfo
		{
			public override string HeaderCaption
			{
				get { return "Cost"; }
			}

			public override string GetValue(OptionSet optionSet)
			{
				return optionSet.TotalPeriodCost.ToString(optionSet.Parent.OptionsSummary.UseDecimalRates ? "$#,##0.00" : "$#,##0");
			}
		}
		#endregion
	}
}
