using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Dictionaries;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Summary;

namespace Asa.Solutions.Dashboard.PresentationClasses
{
	[ToolboxItem(false)]
	public sealed partial class SimpleSummaryControl : DashboardSlideControl, ISummaryControl
	{
		public bool AllowToSave { get; set; }
		public override SlideType SlideType => SlideType.SimpleSummary;
		public override string SlideName => "Closing Summary";

		public SimpleSummaryControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				ckAdvertiser.Font = new Font(ckAdvertiser.Font.FontFamily, ckAdvertiser.Font.Size - 2, ckAdvertiser.Font.Style);
				comboBoxEditAdvertiser.Font = new Font(comboBoxEditAdvertiser.Font.FontFamily, comboBoxEditAdvertiser.Font.Size - 2, comboBoxEditAdvertiser.Font.Style);
				ckDecisionMaker.Font = new Font(ckDecisionMaker.Font.FontFamily, ckDecisionMaker.Font.Size - 2, ckDecisionMaker.Font.Style);
				comboBoxEditDecisionMaker.Font = new Font(comboBoxEditDecisionMaker.Font.FontFamily, comboBoxEditDecisionMaker.Font.Size - 2, comboBoxEditDecisionMaker.Font.Style);
				ckDate.Font = new Font(ckDate.Font.FontFamily, ckDate.Font.Size - 2, ckDate.Font.Style);
				dateEditDate.Font = new Font(dateEditDate.Font.FontFamily, dateEditDate.Font.Size - 2, dateEditDate.Font.Style);
				ckFlightDates.Font = new Font(ckFlightDates.Font.FontFamily, ckFlightDates.Font.Size - 2, ckFlightDates.Font.Style);
				dateEditFligtDatesStart.Font = new Font(dateEditFligtDatesStart.Font.FontFamily, dateEditFligtDatesStart.Font.Size - 2, dateEditFligtDatesStart.Font.Style);
				dateEditFligtDatesEnd.Font = new Font(dateEditFligtDatesEnd.Font.FontFamily, dateEditFligtDatesEnd.Font.Size - 2, dateEditFligtDatesEnd.Font.Style);
				laFlightDatesStart.Font = new Font(laFlightDatesStart.Font.FontFamily, laFlightDatesStart.Font.Size - 2, laFlightDatesStart.Font.Style);
				laFlightDatesEnd.Font = new Font(laFlightDatesEnd.Font.FontFamily, laFlightDatesEnd.Font.Size - 2, laFlightDatesEnd.Font.Style);
				labelControlFlightDatesWeeks.Font = new Font(labelControlFlightDatesWeeks.Font.FontFamily, labelControlFlightDatesWeeks.Font.Size - 2, labelControlFlightDatesWeeks.Font.Style);
				checkEditMonthlyInvestment.Font = new Font(checkEditMonthlyInvestment.Font.FontFamily, checkEditMonthlyInvestment.Font.Size - 2, checkEditMonthlyInvestment.Font.Style);
				checkEditTotalInvestment.Font = new Font(checkEditTotalInvestment.Font.FontFamily, checkEditTotalInvestment.Font.Size - 2, checkEditTotalInvestment.Font.Style);
				checkEditTableOutput.Font = new Font(checkEditTableOutput.Font.FontFamily, checkEditTableOutput.Font.Size - 2, checkEditTableOutput.Font.Style);
			}
			comboBoxEditAdvertiser.EnableSelectAll();
			comboBoxEditDecisionMaker.EnableSelectAll();
			spinEditMonthly.EnableSelectAll();
			spinEditTotal.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.DashboardInfo.SimpleSummaryLists.Headers);
			if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
				comboBoxEditSlideHeader.SelectedIndex = 0;

			checkEditSolutionNew.EditValueChanged += EditValueChanged;
			simpleSummaryItemContainer.ItemCollectionChanged += OnItemCollectionChanged;
		}

		private void OnItemCollectionChanged(object sender, EventArgs e)
		{
			SlideContainer.RaiseDataChanged();
			UpdateTotalItems();
			UpdateTotalValues();
		}

		public override void UpdateSelectedSlide(SlideType slideType)
		{
			base.UpdateSelectedSlide(slideType);
			if (slideType == SlideType)
				xtraTabControl.SelectedTabPage = xtraTabPageBasicInfo;
		}

		public void UpdateTotalItems()
		{
			buttonXAddItem.Text = String.Format("Add Item{0}", simpleSummaryItemContainer.ItemsCount > 0 ? String.Format(" ({0})", simpleSummaryItemContainer.ItemsCount) : String.Empty);
		}

		public override void LoadData()
		{
			AllowToSave = false;
			checkEditSolutionNew.Checked = SlideContainer.EditedContent.SimpleSummaryState.IsNewSolution;
			if (string.IsNullOrEmpty(SlideContainer.EditedContent.SimpleSummaryState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
			{
				var index = comboBoxEditSlideHeader.Properties.Items.IndexOf(SlideContainer.EditedContent.SimpleSummaryState.SlideHeader);
				comboBoxEditSlideHeader.SelectedIndex = index >= 0 ? index : 0;
			}

			ckAdvertiser.Checked = SlideContainer.EditedContent.SimpleSummaryState.ShowAdvertiser;
			ckDecisionMaker.Checked = SlideContainer.EditedContent.SimpleSummaryState.ShowDecisionMaker;
			comboBoxEditAdvertiser.EditValue = String.IsNullOrEmpty(SlideContainer.EditedContent.SimpleSummaryState.Advertiser) ? null : SlideContainer.EditedContent.SimpleSummaryState.Advertiser;
			comboBoxEditDecisionMaker.EditValue = String.IsNullOrEmpty(SlideContainer.EditedContent.SimpleSummaryState.DecisionMaker) ? null : SlideContainer.EditedContent.SimpleSummaryState.DecisionMaker;

			ckDate.Checked = SlideContainer.EditedContent.SimpleSummaryState.ShowPresentationDate;
			if (ckDate.Checked)
				dateEditDate.EditValue = SlideContainer.EditedContent.SimpleSummaryState.PresentationDate != DateTime.MinValue ? (object)SlideContainer.EditedContent.SimpleSummaryState.PresentationDate : null;
			else
				dateEditDate.EditValue = null;

			ckFlightDates.Checked = SlideContainer.EditedContent.SimpleSummaryState.ShowFlightDates;
			if (ckFlightDates.Checked)
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

			checkEditTableOutput.Checked = SlideContainer.EditedContent.SimpleSummaryState.TableOutput;

			UpdateTotalValues();

			AllowToSave = true;

			UpdateFlightDatesWeeks();
			UpdateTotalItems();
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.SimpleSummaryState.ItemsState.AddRange(simpleSummaryItemContainer.GetItems());
			SlideContainer.EditedContent.SimpleSummaryState.IsNewSolution = checkEditSolutionNew.Checked;
			SlideContainer.EditedContent.SimpleSummaryState.SlideHeader = comboBoxEditSlideHeader.EditValue?.ToString();
			SlideContainer.EditedContent.SimpleSummaryState.ShowAdvertiser = ckAdvertiser.Checked;
			SlideContainer.EditedContent.SimpleSummaryState.Advertiser = comboBoxEditAdvertiser.EditValue?.ToString() ?? string.Empty;

			SlideContainer.EditedContent.SimpleSummaryState.ShowDecisionMaker = ckDecisionMaker.Checked;
			SlideContainer.EditedContent.SimpleSummaryState.DecisionMaker = comboBoxEditDecisionMaker.EditValue?.ToString() ?? string.Empty;
			SlideContainer.EditedContent.SimpleSummaryState.ShowPresentationDate = ckDate.Checked;

			SlideContainer.EditedContent.SimpleSummaryState.ShowPresentationDate = ckDate.Checked;
			SlideContainer.EditedContent.SimpleSummaryState.PresentationDate = dateEditDate.EditValue != null ? dateEditDate.DateTime : DateTime.MinValue;

			SlideContainer.EditedContent.SimpleSummaryState.ShowFlightDates = ckFlightDates.Checked;
			SlideContainer.EditedContent.SimpleSummaryState.FlightDatesStart = dateEditFligtDatesStart.EditValue != null ? dateEditFligtDatesStart.DateTime : DateTime.MinValue;
			SlideContainer.EditedContent.SimpleSummaryState.FlightDatesEnd = dateEditFligtDatesEnd.EditValue != null ? dateEditFligtDatesEnd.DateTime : DateTime.MinValue;

			SlideContainer.EditedContent.SimpleSummaryState.ShowMonthly = checkEditMonthlyInvestment.Checked;
			SlideContainer.EditedContent.SimpleSummaryState.ShowTotal = checkEditTotalInvestment.Checked;

			SlideContainer.EditedContent.SimpleSummaryState.TableOutput = checkEditTableOutput.Checked;

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

		private void UpdateFlightDatesWeeks()
		{
			labelControlFlightDatesWeeks.Text = String.Empty;
			if (dateEditFligtDatesStart.EditValue == null || dateEditFligtDatesEnd.EditValue == null)
				return;
			var startDate = dateEditFligtDatesStart.DateTime;
			while (startDate.DayOfWeek != DayOfWeek.Monday)
				startDate = startDate.AddDays(-1);
			var endDate = dateEditFligtDatesEnd.DateTime;
			while (endDate.DayOfWeek != DayOfWeek.Sunday)
				endDate = endDate.AddDays(1);
			var datesRange = endDate - startDate;
			if (datesRange.Days <= 0) return;
			labelControlFlightDatesWeeks.Text = String.Format("<color=\"gray\">{0} weeks</color>", datesRange.Days / 7 + 1);
		}

		private void xtraTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			pnTotals.Visible = e.Page == xtraTabPagePaymentDetails;
		}

		private void buttonXAddItem_Click(object sender, EventArgs e)
		{
			simpleSummaryItemContainer.AddItem();
			if (simpleSummaryItemContainer.ItemsCount >= 20)
				buttonXAddItem.Enabled = false;
			UpdateTotalItems();
			SlideContainer.RaiseDataChanged();
		}

		private void ckAdvertiser_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditAdvertiser.Enabled = ckAdvertiser.Checked;
			pbAdvertiser.Image = ckAdvertiser.Checked ? Properties.Resources.SummaryBusinessName : Properties.Resources.SummaryBusinessName.MakeGrayscale();
			laAdvertiser.ForeColor = ckAdvertiser.Checked ? Color.Black : Color.Gray;
			if (AllowToSave)
				SlideContainer.RaiseDataChanged();
		}

		private void ckDecisionMaker_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditDecisionMaker.Enabled = ckDecisionMaker.Checked;
			pbDecisionMaker.Image = ckDecisionMaker.Checked ? Properties.Resources.SummaryDecisionMaker : Properties.Resources.SummaryDecisionMaker.MakeGrayscale();
			laDecisionMaker.ForeColor = ckDecisionMaker.Checked ? Color.Black : Color.Gray;
			if (AllowToSave)
				SlideContainer.RaiseDataChanged();
		}

		private void ckDate_CheckedChanged(object sender, EventArgs e)
		{
			dateEditDate.Enabled = ckDate.Checked;
			pbDate.Image = ckDate.Checked ? Properties.Resources.SummaryPresentationDate : Properties.Resources.SummaryPresentationDate.MakeGrayscale();
			pbDate.Enabled = ckDate.Checked;
			laDate.ForeColor = ckDate.Checked ? Color.Black : Color.Gray;
			if (AllowToSave)
				SlideContainer.RaiseDataChanged();
		}

		private void ckFlightDates_CheckedChanged(object sender, EventArgs e)
		{
			dateEditFligtDatesStart.Enabled = ckFlightDates.Checked;
			dateEditFligtDatesEnd.Enabled = ckFlightDates.Checked;
			pbFlightDates.Image = ckFlightDates.Checked ? Properties.Resources.SummaryFlightDates : Properties.Resources.SummaryFlightDates.MakeGrayscale();
			laFlightDates.ForeColor = ckFlightDates.Checked ? Color.Black : Color.Gray;
			laFlightDatesStart.ForeColor = ckFlightDates.Checked ? Color.Black : Color.Gray;
			laFlightDatesEnd.ForeColor = ckFlightDates.Checked ? Color.Black : Color.Gray;
			if (AllowToSave)
				SlideContainer.RaiseDataChanged();
		}

		private void dateEditFligtDatesStart_EditValueChanged(object sender, EventArgs e)
		{
			dateEditFligtDatesEnd.Properties.NullDate = dateEditFligtDatesStart.DateTime;
			UpdateFlightDatesWeeks();
			EditValueChanged(sender, e);
			if (AllowToSave)
				SlideContainer.RaiseDataChanged();
		}

		private void dateEditFligtDatesEnd_EditValueChanged(object sender, EventArgs e)
		{
			UpdateFlightDatesWeeks();
			EditValueChanged(sender, e);
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			checkEditMonthlyInvestment.ForeColor =
				checkEditMonthlyInvestment.Properties.Appearance.ForeColor =
				checkEditMonthlyInvestment.Properties.AppearanceFocused.ForeColor =
				 checkEditMonthlyInvestment.Checked ? Color.Black : Color.Gray;
			checkEditMonthlyInvestment.Refresh();
			checkEditTotalInvestment.ForeColor =
				checkEditTotalInvestment.Properties.Appearance.ForeColor =
				checkEditTotalInvestment.Properties.AppearanceFocused.ForeColor =
				 checkEditTotalInvestment.Checked ? Color.Black : Color.Gray;
			checkEditTotalInvestment.Refresh();
			spinEditMonthly.Enabled = checkEditMonthlyInvestment.Checked;
			spinEditTotal.Enabled = checkEditTotalInvestment.Checked;
			UpdateTotalItems();
			if (AllowToSave)
				SlideContainer.RaiseDataChanged();
		}

		private void EditValueChanged(object sender, EventArgs e)
		{
			if (AllowToSave)
				SlideContainer.RaiseDataChanged();
		}

		private void spinEditTotals_EditValueChanged(object sender, EventArgs e)
		{
			if (!AllowToSave) return;
			SlideContainer.EditedContent.SimpleSummaryState.MonthlyValue = simpleSummaryItemContainer.TotalMonthlyValue == (decimal?)spinEditMonthly.EditValue ? null : (decimal?)spinEditMonthly.EditValue;
			SlideContainer.EditedContent.SimpleSummaryState.TotalValue = simpleSummaryItemContainer.TotalTotalValue == (decimal?)spinEditTotal.EditValue ? null : (decimal?)spinEditTotal.EditValue;
			SlideContainer.RaiseDataChanged();
		}

		private void spinEditMonthly_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			spinEditMonthly.EditValue = simpleSummaryItemContainer.TotalMonthlyValue;
		}

		private void spinEditTotal_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			spinEditTotal.EditValue = simpleSummaryItemContainer.TotalTotalValue;
		}

		#region Output Staff
		public override bool ReadyForOutput => simpleSummaryItemContainer.ItemsComplited;

		public int ItemsCount => simpleSummaryItemContainer.ItemTitles.Length;

		public int SlidesCount
		{
			get
			{
				if (!TableOutput)
				{
					var main = ItemsCount / 5;
					var rest = ItemsCount % 5;
					return main + (rest > 0 ? 1 : 0);
				}
				else
				{
					var main = ItemsCount / 18;
					var rest = ItemsCount % 18;
					return main + (rest > 0 ? 1 : 0);
				}
			}
		}

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
				if (ckAdvertiser.Checked)
					return comboBoxEditAdvertiser.EditValue?.ToString() ?? string.Empty;
				return string.Empty;
			}
		}

		public string DecisionMaker
		{
			get
			{
				if (ckDecisionMaker.Checked)
					return comboBoxEditDecisionMaker.EditValue?.ToString() ?? string.Empty;
				return string.Empty;
			}
		}

		public string PresentationDate
		{
			get
			{
				if (ckDate.Checked)
					return dateEditDate.EditValue != null ? !dateEditDate.DateTime.Equals(DateTime.MinValue) ? dateEditDate.DateTime.ToString("MMMM dd, yyyy") : string.Empty : string.Empty;
				return string.Empty;
			}
		}

		public string CampaignDates
		{
			get
			{
				if (ckFlightDates.Checked)
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

		public Theme SelectedTheme => SlideContainer.GetSelectedTheme(SlideType.SimpleSummary);

		public bool TableOutput => checkEditTableOutput.Checked;

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

		public override void GenerateOutput()
		{
			throw new NotImplementedException();
			//SaveChanges();
			//FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			//FormProgress.ShowProgress();
			//AppManager.Instance.ShowFloater(() =>
			//{
			//	DashboardPowerPointHelper.Instance.AppendSummary(this);
			//	FormProgress.CloseProgress();
			//});
		}

		public override PreviewGroup GeneratePreview()
		{
			throw new NotImplementedException();
			//SaveChanges();
			//FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			//FormProgress.ShowProgress();
			//var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			//DashboardPowerPointHelper.Instance.PrepareSummaryEmail(tempFileName, this);
			//Utilities.ActivateForm(FormMain.Instance.Handle, false, false);
			//FormProgress.CloseProgress();
			//if (!File.Exists(tempFileName)) return;
			//using (var formPreview = new FormPreview(FormMain.Instance, DashboardPowerPointHelper.Instance, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater))
			//{
			//	formPreview.Text = "Preview Slides";
			//	formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
			//	RegistryHelper.MainFormHandle = formPreview.Handle;
			//	RegistryHelper.MaximizeMainForm = false;
			//	var previewResult = formPreview.ShowDialog();
			//	RegistryHelper.MaximizeMainForm = false;
			//	RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
			//	if (previewResult != DialogResult.OK)
			//		AppManager.Instance.ActivateMainForm();
			//}
		}
		#endregion
	}
}