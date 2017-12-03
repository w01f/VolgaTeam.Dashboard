using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Asa.Business.Common.Dictionaries;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Interop;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Summary;
using Asa.Solutions.Dashboard.PresentationClasses.Output;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;

namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class SimpleSummaryControl : DashboardSlideControl, IDashboardOutputData, IDashboardSlide, ISummaryControl
	{
		public bool AllowToSave { get; set; }
		public override SlideType SlideType => SlideType.SimpleSummary;
		public override string ControlName => SlideContainer.DashboardInfo.SimpleSummaryTitle;

		public SimpleSummaryControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			Text = ControlName;

			comboBoxEditSlideHeader.EnableSelectAll();
			comboBoxEditAdvertiser.EnableSelectAll();
			comboBoxEditDecisionMaker.EnableSelectAll();
			spinEditMonthly.EnableSelectAll();
			spinEditTotal.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.DashboardInfo.SimpleSummaryLists.Headers);
			if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
				comboBoxEditSlideHeader.SelectedIndex = 0;

			simpleSummaryItemContainer.ItemCollectionChanged += OnItemCollectionChanged;
			pictureEditSplash.Image = SlideContainer.DashboardInfo.SimpleSummarySplashLogo;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControl.MaximumSize = RectangleHelper.ScaleSize(layoutControl.MaximumSize, scaleFactor);
			layoutControl.MinimumSize = RectangleHelper.ScaleSize(layoutControl.MinimumSize, scaleFactor);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);
			layoutControlItemAdvertiserToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAdvertiserToggle.MaxSize, scaleFactor);
			layoutControlItemAdvertiserToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemAdvertiserToggle.MinSize, scaleFactor);
			layoutControlItemAdvertiserLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAdvertiserLogo.MaxSize, scaleFactor);
			layoutControlItemAdvertiserLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemAdvertiserLogo.MinSize, scaleFactor);
			simpleLabelItemAdvertiserTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemAdvertiserTitle.MaxSize, scaleFactor);
			simpleLabelItemAdvertiserTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemAdvertiserTitle.MinSize, scaleFactor);
			layoutControlItemAdvertiserValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAdvertiserValue.MaxSize, scaleFactor);
			layoutControlItemAdvertiserValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemAdvertiserValue.MinSize, scaleFactor);
			layoutControlItemDecisionMakerToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDecisionMakerToggle.MaxSize, scaleFactor);
			layoutControlItemDecisionMakerToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemDecisionMakerToggle.MinSize, scaleFactor);
			layoutControlItemDecisionMakerLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDecisionMakerLogo.MaxSize, scaleFactor);
			layoutControlItemDecisionMakerLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemDecisionMakerLogo.MinSize, scaleFactor);
			simpleLabelItemDecisionMakerTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemDecisionMakerTitle.MaxSize, scaleFactor);
			simpleLabelItemDecisionMakerTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemDecisionMakerTitle.MinSize, scaleFactor);
			layoutControlItemDecisionMakerValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDecisionMakerValue.MaxSize, scaleFactor);
			layoutControlItemDecisionMakerValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemDecisionMakerValue.MinSize, scaleFactor);
			layoutControlItemDateToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDateToggle.MaxSize, scaleFactor);
			layoutControlItemDateToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemDateToggle.MinSize, scaleFactor);
			layoutControlItemDateLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDateLogo.MaxSize, scaleFactor);
			layoutControlItemDateLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemDateLogo.MinSize, scaleFactor);
			simpleLabelItemDateTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemDateTitle.MaxSize, scaleFactor);
			simpleLabelItemDateTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemDateTitle.MinSize, scaleFactor);
			layoutControlItemDateValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDateValue.MaxSize, scaleFactor);
			layoutControlItemDateValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemDateValue.MinSize, scaleFactor);
			layoutControlItemFlightDatesToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemFlightDatesToggle.MaxSize, scaleFactor);
			layoutControlItemFlightDatesToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemFlightDatesToggle.MinSize, scaleFactor);
			layoutControlItemFlightDatesLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemFlightDatesLogo.MaxSize, scaleFactor);
			layoutControlItemFlightDatesLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemFlightDatesLogo.MinSize, scaleFactor);
			simpleLabelItemFlightDatesTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemFlightDatesTitle.MaxSize, scaleFactor);
			simpleLabelItemFlightDatesTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemFlightDatesTitle.MinSize, scaleFactor);
			layoutControlItemFlightDatesStart.MaxSize = RectangleHelper.ScaleSize(layoutControlItemFlightDatesStart.MaxSize, scaleFactor);
			layoutControlItemFlightDatesStart.MinSize = RectangleHelper.ScaleSize(layoutControlItemFlightDatesStart.MinSize, scaleFactor);
			layoutControlItemFlightDatesEnd.MaxSize = RectangleHelper.ScaleSize(layoutControlItemFlightDatesEnd.MaxSize, scaleFactor);
			layoutControlItemFlightDatesEnd.MinSize = RectangleHelper.ScaleSize(layoutControlItemFlightDatesEnd.MinSize, scaleFactor);
			layoutControlItemMonthlyInvestmentToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemMonthlyInvestmentToggle.MaxSize, scaleFactor);
			layoutControlItemMonthlyInvestmentToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemMonthlyInvestmentToggle.MinSize, scaleFactor);
			layoutControlItemMonthlyInvestmentValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemMonthlyInvestmentValue.MaxSize, scaleFactor);
			layoutControlItemMonthlyInvestmentValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemMonthlyInvestmentValue.MinSize, scaleFactor);
			layoutControlItemTotalInvestmentToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalInvestmentToggle.MaxSize, scaleFactor);
			layoutControlItemTotalInvestmentToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalInvestmentToggle.MinSize, scaleFactor);
			layoutControlItemTotalInvestementValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalInvestementValue.MaxSize, scaleFactor);
			layoutControlItemTotalInvestementValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalInvestementValue.MinSize, scaleFactor);
			layoutControlItemAddItem.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAddItem.MaxSize, scaleFactor);
			layoutControlItemAddItem.MinSize = RectangleHelper.ScaleSize(layoutControlItemAddItem.MinSize, scaleFactor);
		}

		private void OnItemCollectionChanged(object sender, EventArgs e)
		{
			SlideContainer.RaiseDataChanged();
			UpdateTotalItems();
			UpdateTotalValues();
		}

		public void UpdateTotalItems()
		{
			buttonXAddItem.Text = String.Format("Add Item{0}", simpleSummaryItemContainer.ItemsCount > 0 ? String.Format(" ({0})", simpleSummaryItemContainer.ItemsCount) : String.Empty);
			layoutControlItemAddItem.Enabled = simpleSummaryItemContainer.ItemsCount < 20;
		}

		public override void LoadData()
		{
			AllowToSave = false;
			if (string.IsNullOrEmpty(SlideContainer.EditedContent.SimpleSummaryState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.SimpleSummaryState.SlideHeader;

			checkEditAdvertiser.Checked = SlideContainer.EditedContent.SimpleSummaryState.ShowAdvertiser;
			checkEditDecisionMaker.Checked = SlideContainer.EditedContent.SimpleSummaryState.ShowDecisionMaker;

			comboBoxEditAdvertiser.EditValue = SlideContainer.EditedContent.SimpleSummaryState.Advertiser ?? SlideContainer.EditedContent.ScheduleSettings.BusinessName;
			comboBoxEditDecisionMaker.EditValue = SlideContainer.EditedContent.SimpleSummaryState.DecisionMaker ?? SlideContainer.EditedContent.ScheduleSettings.DecisionMaker;

			checkEditDate.Checked = SlideContainer.EditedContent.SimpleSummaryState.ShowPresentationDate;
			if (checkEditDate.Checked)
				dateEditDate.EditValue = SlideContainer.EditedContent.SimpleSummaryState.PresentationDate != DateTime.MinValue ?
					SlideContainer.EditedContent.SimpleSummaryState.PresentationDate :
					SlideContainer.EditedContent.ScheduleSettings.PresentationDate;
			else
				dateEditDate.EditValue = null;

			checkEditFlightDates.Checked = SlideContainer.EditedContent.SimpleSummaryState.ShowFlightDates;
			if (checkEditFlightDates.Checked)
			{
				dateEditFligtDatesStart.EditValue = SlideContainer.EditedContent.SimpleSummaryState.FlightDatesStart != DateTime.MinValue ? (object)SlideContainer.EditedContent.SimpleSummaryState.FlightDatesStart : null;
				dateEditFligtDatesEnd.EditValue = SlideContainer.EditedContent.SimpleSummaryState.FlightDatesEnd != DateTime.MinValue ? (object)SlideContainer.EditedContent.SimpleSummaryState.FlightDatesEnd : null;
			}
			else
			{
				dateEditFligtDatesStart.EditValue = null;
				dateEditFligtDatesEnd.EditValue = null;
			}

			simpleSummaryItemContainer.LoadItems(SlideContainer.EditedContent.SimpleSummaryState.ItemsState);

			checkEditMonthlyInvestment.Checked = SlideContainer.EditedContent.SimpleSummaryState.ShowMonthly;
			checkEditTotalInvestment.Checked = SlideContainer.EditedContent.SimpleSummaryState.ShowTotal;

			UpdateTotalValues();

			AllowToSave = true;

			UpdateTotalItems();
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.SimpleSummaryState.ItemsState.Clear();
			SlideContainer.EditedContent.SimpleSummaryState.ItemsState.AddRange(simpleSummaryItemContainer.GetItems());

			SlideContainer.EditedContent.SimpleSummaryState.SlideHeader = comboBoxEditSlideHeader.EditValue?.ToString();
			SlideContainer.EditedContent.SimpleSummaryState.ShowAdvertiser = checkEditAdvertiser.Checked;
			SlideContainer.EditedContent.SimpleSummaryState.Advertiser = comboBoxEditAdvertiser.EditValue?.ToString() ?? string.Empty;

			SlideContainer.EditedContent.SimpleSummaryState.ShowDecisionMaker = checkEditDecisionMaker.Checked;
			SlideContainer.EditedContent.SimpleSummaryState.DecisionMaker = comboBoxEditDecisionMaker.EditValue?.ToString() ?? string.Empty;
			SlideContainer.EditedContent.SimpleSummaryState.ShowPresentationDate = checkEditDate.Checked;

			SlideContainer.EditedContent.SimpleSummaryState.ShowPresentationDate = checkEditDate.Checked;
			SlideContainer.EditedContent.SimpleSummaryState.PresentationDate = dateEditDate.EditValue != null ? dateEditDate.DateTime : DateTime.MinValue;

			SlideContainer.EditedContent.SimpleSummaryState.ShowFlightDates = checkEditFlightDates.Checked;
			SlideContainer.EditedContent.SimpleSummaryState.FlightDatesStart = dateEditFligtDatesStart.EditValue != null ? dateEditFligtDatesStart.DateTime : DateTime.MinValue;
			SlideContainer.EditedContent.SimpleSummaryState.FlightDatesEnd = dateEditFligtDatesEnd.EditValue != null ? dateEditFligtDatesEnd.DateTime : DateTime.MinValue;

			SlideContainer.EditedContent.SimpleSummaryState.ShowMonthly = checkEditMonthlyInvestment.Checked;
			SlideContainer.EditedContent.SimpleSummaryState.ShowTotal = checkEditTotalInvestment.Checked;

			ListManager.Instance.Advertisers.Add(Advertiser);
			ListManager.Instance.Advertisers.Save();

			ListManager.Instance.DecisionMakers.Add(DecisionMaker);
			ListManager.Instance.DecisionMakers.Save();
		}

		public void UpdateTotalValues()
		{
			spinEditMonthly.EditValue = SlideContainer.EditedContent.SimpleSummaryState.MonthlyValue ?? simpleSummaryItemContainer.TotalMonthlyValue;
			spinEditTotal.EditValue = SlideContainer.EditedContent.SimpleSummaryState.TotalValue ?? simpleSummaryItemContainer.TotalTotalValue;
		}

		private void OnSelectedTabPageChanged(object sender, LayoutTabPageChangedEventArgs e)
		{
			layoutControlGroupInvestments.Visibility = e.Page == layoutControlGroupPaymentDetails ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void OnAddItemClick(object sender, EventArgs e)
		{
			simpleSummaryItemContainer.AddItem();
			UpdateTotalItems();
			SlideContainer.RaiseDataChanged();
		}

		private void OnAdvertiserCheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupAdvertiser.Enabled = checkEditAdvertiser.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnDecisionMakerCheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupDecisionMaker.Enabled = checkEditDecisionMaker.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnDateCheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupDate.Enabled = checkEditDate.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnFlightDatesCheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupFlightDates.Enabled = checkEditFlightDates.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnFlightDatesStartEditValueChanged(object sender, EventArgs e)
		{
			dateEditFligtDatesEnd.Properties.NullDate = dateEditFligtDatesStart.DateTime;
			OnEditValueChanged(sender, e);
		}

		private void OnFligtDatesEndEditValueChanged(object sender, EventArgs e)
		{
			OnEditValueChanged(sender, e);
		}

		private void OnInvestmentCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemMonthlyInvestmentValue.Enabled = checkEditMonthlyInvestment.Checked;
			layoutControlItemTotalInvestementValue.Enabled = checkEditTotalInvestment.Checked;
			UpdateTotalItems();
			OnEditValueChanged(sender, e);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (AllowToSave)
				SlideContainer.RaiseDataChanged();
		}

		private void OnInvestmentEditValueChanged(object sender, EventArgs e)
		{
			if (!AllowToSave) return;
			SlideContainer.EditedContent.SimpleSummaryState.MonthlyValue = simpleSummaryItemContainer.TotalMonthlyValue == (decimal?)spinEditMonthly.EditValue ? null : (decimal?)spinEditMonthly.EditValue;
			SlideContainer.EditedContent.SimpleSummaryState.TotalValue = simpleSummaryItemContainer.TotalTotalValue == (decimal?)spinEditTotal.EditValue ? null : (decimal?)spinEditTotal.EditValue;
			SlideContainer.RaiseDataChanged();
		}

		private void OnMonthlyInvestmentButtonClick(object sender, ButtonPressedEventArgs e)
		{
			spinEditMonthly.EditValue = simpleSummaryItemContainer.TotalMonthlyValue;
		}

		private void OnTotalInvestmentButtonClick(object sender, ButtonPressedEventArgs e)
		{
			spinEditTotal.EditValue = simpleSummaryItemContainer.TotalTotalValue;
		}

		#region Output Staff
		public override bool ReadyForOutput => simpleSummaryItemContainer.ItemsComplited;

		public int ItemsCount => simpleSummaryItemContainer.ItemTitles.Length;

		public string Title => comboBoxEditSlideHeader.EditValue?.ToString() ?? string.Empty;

		public string SummaryData
		{
			get
			{
				var values = new List<string>();
				if (!String.IsNullOrEmpty(PresentationDate))
					values.Add(PresentationDate);
				if (!String.IsNullOrEmpty(Advertiser))
					values.Add(Advertiser);
				if (!String.IsNullOrEmpty(DecisionMaker))
					values.Add(DecisionMaker);
				if (!String.IsNullOrEmpty(CampaignDates))
					values.Add(CampaignDates);
				if (!String.IsNullOrEmpty(TotalMonthlyValue))
					values.Add(String.Format("Monthly Investment: {0}", TotalMonthlyValue));
				if (!String.IsNullOrEmpty(TotalTotalValue))
					values.Add(String.Format("Total Investment: {0}", TotalTotalValue));
				return String.Join("   |   ", values);
			}
		}

		public string Advertiser
		{
			get
			{
				if (checkEditAdvertiser.Checked)
					return comboBoxEditAdvertiser.EditValue?.ToString() ?? string.Empty;
				return string.Empty;
			}
		}

		public string DecisionMaker
		{
			get
			{
				if (checkEditDecisionMaker.Checked)
					return comboBoxEditDecisionMaker.EditValue?.ToString() ?? string.Empty;
				return string.Empty;
			}
		}

		public string PresentationDate
		{
			get
			{
				if (checkEditDate.Checked)
					return dateEditDate.EditValue != null ? !dateEditDate.DateTime.Equals(DateTime.MinValue) ? dateEditDate.DateTime.ToString("MMMM dd, yyyy") : string.Empty : string.Empty;
				return string.Empty;
			}
		}

		public string CampaignDates
		{
			get
			{
				if (checkEditFlightDates.Checked)
					return (dateEditFligtDatesStart.EditValue != null ? !dateEditFligtDatesStart.DateTime.Equals(DateTime.MinValue) ? dateEditFligtDatesStart.DateTime.ToString("M/d/yyyy") : string.Empty : string.Empty) +
						   (dateEditFligtDatesEnd.EditValue != null ? !dateEditFligtDatesEnd.DateTime.Equals(DateTime.MinValue) ? " - " + dateEditFligtDatesEnd.DateTime.ToString("M/d/yyyy") : string.Empty : string.Empty);
				return string.Empty;
			}
		}

		public string[] ItemTitles => simpleSummaryItemContainer.ItemTitles;

		public string[] ItemDetails => simpleSummaryItemContainer.ItemDetails;

		public string[] MonthlyValues
		{
			get
			{
				if (ShowMonthlyHeader && !ShowTotalHeader)
					return null;
				return simpleSummaryItemContainer.OutputMonthlyValues;
			}
		}

		public string[] TotalValues
		{
			get
			{
				if (ShowMonthlyHeader && !ShowTotalHeader)
					return simpleSummaryItemContainer.OutputMonthlyValues;
				return simpleSummaryItemContainer.OutputTotalValues;
			}
		}

		public string TotalMonthlyValue => checkEditMonthlyInvestment.Checked && spinEditMonthly.EditValue != null ? spinEditMonthly.Value.ToString("$#,##0.00") : String.Empty;

		public string TotalTotalValue => checkEditTotalInvestment.Checked && spinEditTotal.EditValue != null ? spinEditTotal.Value.ToString("$#,##0.00") : String.Empty;

		public bool ShowMonthlyHeader => simpleSummaryItemContainer.ShowMonthlyTotal;

		public bool ShowTotalHeader => simpleSummaryItemContainer.ShowTotalTotal;

		public StorageDirectory ContractTemplateFolder => null;

		public ContractSettings ContractSettings => SlideContainer.EditedContent.SimpleSummaryState.ContractSettings;

		public int ItemsPerTable => ItemsCount > 18 ? 18 : ItemsCount;

		public bool ShowIcons => false;

		public string[] TableIcons => null;

		public List<Dictionary<string, string>> OutputReplacementsLists { get; private set; }

		public void PopulateReplacementsList()
		{
			if (OutputReplacementsLists == null)
				OutputReplacementsLists = new List<Dictionary<string, string>>();
			OutputReplacementsLists.Clear();
			var recordsCount = ItemsCount;
			OutputReplacementsLists = new List<Dictionary<string, string>>();
			var monthlyValues = simpleSummaryItemContainer.OutputMonthlyValues;
			var totalValues = simpleSummaryItemContainer.OutputTotalValues;
			for (var i = 0; i < recordsCount; i += ItemsPerTable)
			{
				var slideRows = new Dictionary<string, string>();
				for (var j = 0; j < ItemsPerTable; j++)
				{
					if ((i + j) < recordsCount)
					{
						slideRows.Add(String.Format("Product{0}", j + 1), ItemTitles[i + j]);
						var details = new List<string>();
						if (!String.IsNullOrEmpty(ItemDetails[i + j]))
							details.Add(ItemDetails[i + j]);
						if (monthlyValues.Any() && !String.IsNullOrEmpty(monthlyValues[i + j]))
							details.Add(String.Format("({0}/mo)", monthlyValues[i + j]));
						if (totalValues.Any() && !String.IsNullOrEmpty(totalValues[i + j]))
							details.Add(String.Format("({0} inv)", totalValues[i + j]));
						slideRows.Add(String.Format("Details{0}", j + 1), String.Join(" ", details));
					}
					else
					{
						slideRows.Add(String.Format("Product{0}", j + 1), "DeleteRow");
						slideRows.Add(String.Format("Details{0}", j + 1), "DeleteRow");
					}
				}
				OutputReplacementsLists.Add(slideRows);
			}
		}

		public IEnumerable<DashboardSlideInfo> GetSlideInfo()
		{
			return new[]
			{
				new SummarySlideInfo
				{
					SlideContainer = this,
					SlideName = String.Format("{0} (line - item)",ControlName),
					TableOutput = false
				},
				new SummarySlideInfo
				{
					SlideContainer = this,
					SlideName = String.Format("{0} (table - grid)",ControlName),
					TableOutput = true
				},
			};
		}

		public void GenerateOutput(DashboardSlideInfo slideInfo)
		{
			var sumarySlideInfo = (SummarySlideInfo)slideInfo;
			SlideContainer.PowerPointProcessor.AppendSummary(this, sumarySlideInfo.TableOutput);
		}

		public PreviewGroup GeneratePreview(DashboardSlideInfo slideInfo)
		{
			var sumarySlideInfo = (SummarySlideInfo)slideInfo;
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			SlideContainer.PowerPointProcessor.PrepareSummaryEmail(tempFileName, this, sumarySlideInfo.TableOutput);
			return new PreviewGroup { Name = ControlName, PresentationSourcePath = tempFileName };
		}
		#endregion
	}
}