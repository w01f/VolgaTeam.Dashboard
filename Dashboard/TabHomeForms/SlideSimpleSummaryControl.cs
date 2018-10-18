using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Dashboard.Configuration;
using Asa.Business.Dashboard.Entities.NonPersistent;
using Asa.Common.Core.Dictionaries;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Interop;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Summary;
using Asa.Common.GUI.ToolForms;
using DevComponents.DotNetBar;

namespace Asa.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public sealed partial class SlideSimpleSummaryControl : SlideBaseControl, ISummaryControl
	{
		private readonly SuperTooltipInfo _toolTipLoad = new SuperTooltipInfo("Summary Slides", "", "Open previously-saved Summary slide data files", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _toolTipHelp = new SuperTooltipInfo("HELP", "", "Help me with the Closing Summary Slide", null, null, eTooltipColor.Gray);

		public SlideSimpleSummaryControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
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
			comboBoxEditSlideHeader.Properties.Items.AddRange(ListManager.Instance.SimpleSummaryLists.Headers);
			if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
				comboBoxEditSlideHeader.SelectedIndex = 0;

			checkEditSolutionNew.EditValueChanged += EditValueChanged;

			FormMain.Instance.FormClosed += (sender1, e1) =>
			{
				if (!SettingsNotSaved) return;
				SaveState();
				ViewSettingsManager.Instance.SimpleSummaryState.Save();
			};

			LoadSavedState();
		}

		public bool AllowToSave { get; set; }

		public override string SlideName => "Closing Summary";

		public override SuperTooltipInfo TooltipLoad => _toolTipLoad;

		public override SuperTooltipInfo TooltipHelp => _toolTipHelp;

		public override ButtonItem ThemeButton => FormMain.Instance.buttonItemHomeThemeSimpleSummary;

		public void UpdateTotalItems()
		{
			buttonXAddItem.Text = String.Format("Add Item{0}", simpleSummaryItemContainer.ItemsCount > 0 ? String.Format(" ({0})", simpleSummaryItemContainer.ItemsCount) : String.Empty);
		}

		public void ResetTab()
		{
			xtraTabControl.SelectedTabPage = xtraTabPageBasicInfo;
			UpdateOutputState();
		}

		protected override void UpdateSavedFilesState()
		{
			SetLoadState(ViewSettingsManager.Instance.SimpleSummaryState.AllowToLoad());
		}

		public void UpdateOutputState()
		{
			SetOutputState(simpleSummaryItemContainer.ItemsComplited);
		}

		private void LoadSavedState()
		{
			AllowToSave = false;
			checkEditSolutionNew.Checked = ViewSettingsManager.Instance.SimpleSummaryState.IsNewSolution;
			if (string.IsNullOrEmpty(ViewSettingsManager.Instance.SimpleSummaryState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
				comboBoxEditSlideHeader.EditValue = ViewSettingsManager.Instance.SimpleSummaryState.SlideHeader;

			ckAdvertiser.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowAdvertiser;
			ckDecisionMaker.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowDecisionMaker;
			comboBoxEditAdvertiser.EditValue = String.IsNullOrEmpty(ViewSettingsManager.Instance.SimpleSummaryState.Advertiser) ? null : ViewSettingsManager.Instance.SimpleSummaryState.Advertiser;
			comboBoxEditDecisionMaker.EditValue = String.IsNullOrEmpty(ViewSettingsManager.Instance.SimpleSummaryState.DecisionMaker) ? null : ViewSettingsManager.Instance.SimpleSummaryState.DecisionMaker;

			ckDate.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowPresentationDate;
			if (ckDate.Checked)
				dateEditDate.EditValue = ViewSettingsManager.Instance.SimpleSummaryState.PresentationDate != DateTime.MinValue ? (object)ViewSettingsManager.Instance.SimpleSummaryState.PresentationDate : null;
			else
				dateEditDate.EditValue = null;

			ckFlightDates.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowFlightDates;
			if (ckFlightDates.Checked)
			{
				dateEditFligtDatesStart.EditValue = ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesStart != DateTime.MinValue ? (object)ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesStart : null;
				dateEditFligtDatesEnd.EditValue = ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesEnd != DateTime.MinValue ? (object)ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesEnd : null;
			}
			else
			{
				dateEditFligtDatesStart.EditValue = null;
				dateEditFligtDatesEnd.EditValue = null;
			}

			simpleSummaryItemContainer.LoadItems();

			checkEditMonthlyInvestment.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowMonthly;
			checkEditTotalInvestment.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowTotal;

			checkEditTableOutput.Checked = ViewSettingsManager.Instance.SimpleSummaryState.TableOutput;

			UpdateTotalValues();

			AllowToSave = true;
			SettingsNotSaved = false;

			UpdateFlightDatesWeeks();
			UpdateTotalItems();
			UpdateSavedFilesState();
			UpdateOutputState();
		}

		private void SaveState()
		{
			simpleSummaryItemContainer.SaveItems();
			ViewSettingsManager.Instance.SimpleSummaryState.IsNewSolution = checkEditSolutionNew.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : null;
			ViewSettingsManager.Instance.SimpleSummaryState.ShowAdvertiser = ckAdvertiser.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.Advertiser = comboBoxEditAdvertiser.EditValue != null ? comboBoxEditAdvertiser.EditValue.ToString() : string.Empty;

			ViewSettingsManager.Instance.SimpleSummaryState.ShowDecisionMaker = ckDecisionMaker.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.DecisionMaker = comboBoxEditDecisionMaker.EditValue != null ? comboBoxEditDecisionMaker.EditValue.ToString() : string.Empty;
			ViewSettingsManager.Instance.SimpleSummaryState.ShowPresentationDate = ckDate.Checked;

			ViewSettingsManager.Instance.SimpleSummaryState.ShowPresentationDate = ckDate.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.PresentationDate = dateEditDate.EditValue != null ? dateEditDate.DateTime : DateTime.MinValue;

			ViewSettingsManager.Instance.SimpleSummaryState.ShowFlightDates = ckFlightDates.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesStart = dateEditFligtDatesStart.EditValue != null ? dateEditFligtDatesStart.DateTime : DateTime.MinValue;
			ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesEnd = dateEditFligtDatesEnd.EditValue != null ? dateEditFligtDatesEnd.DateTime : DateTime.MinValue;

			ViewSettingsManager.Instance.SimpleSummaryState.ShowMonthly = checkEditMonthlyInvestment.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.ShowTotal = checkEditTotalInvestment.Checked;

			ViewSettingsManager.Instance.SimpleSummaryState.TableOutput = checkEditTableOutput.Checked;
			UpdateTotalValues();

			AllowToSave = true;
			SettingsNotSaved = false;
		}

		public void UpdateTotalValues()
		{
			spinEditMonthly.EditValue = ViewSettingsManager.Instance.SimpleSummaryState.MonthlyValue.HasValue ? ViewSettingsManager.Instance.SimpleSummaryState.MonthlyValue.Value : simpleSummaryItemContainer.TotalMonthlyValue;
			spinEditTotal.EditValue = ViewSettingsManager.Instance.SimpleSummaryState.TotalValue.HasValue ? ViewSettingsManager.Instance.SimpleSummaryState.TotalValue.Value : simpleSummaryItemContainer.TotalTotalValue;
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

		public override void LoadClick()
		{
			using (var form = new FormSavedSimpleSummary())
			{
				if (form.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(form.SelectedFile))
				{
					ViewSettingsManager.Instance.SimpleSummaryState.Load(form.SelectedFile);
					LoadSavedState();
				}
			}
			base.LoadClick();
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
			SettingsNotSaved = true;
		}

		private void ckAdvertiser_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditAdvertiser.Enabled = ckAdvertiser.Checked;
			pbAdvertiser.Image = ckAdvertiser.Checked ? Properties.Resources.SummaryBusinessName : Properties.Resources.SummaryBusinessName.MakeGrayscale();
			laAdvertiser.ForeColor = ckAdvertiser.Checked ? Color.Black : Color.Gray;
			if (AllowToSave)
				SettingsNotSaved = true;
		}

		private void ckDecisionMaker_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditDecisionMaker.Enabled = ckDecisionMaker.Checked;
			pbDecisionMaker.Image = ckDecisionMaker.Checked ? Properties.Resources.SummaryDecisionMaker : Properties.Resources.SummaryDecisionMaker.MakeGrayscale();
			laDecisionMaker.ForeColor = ckDecisionMaker.Checked ? Color.Black : Color.Gray;
			if (AllowToSave)
				SettingsNotSaved = true;
		}

		private void ckDate_CheckedChanged(object sender, EventArgs e)
		{
			dateEditDate.Enabled = ckDate.Checked;
			pbDate.Image = ckDate.Checked ? Properties.Resources.SummaryPresentationDate : Properties.Resources.SummaryPresentationDate.MakeGrayscale();
			pbDate.Enabled = ckDate.Checked;
			laDate.ForeColor = ckDate.Checked ? Color.Black : Color.Gray;
			if (AllowToSave)
				SettingsNotSaved = true;
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
				SettingsNotSaved = true;
		}

		private void dateEditFligtDatesStart_EditValueChanged(object sender, EventArgs e)
		{
			dateEditFligtDatesEnd.Properties.NullDate = dateEditFligtDatesStart.DateTime;
			UpdateFlightDatesWeeks();
			EditValueChanged(sender, e);
			if (AllowToSave)
				SettingsNotSaved = true;
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
				SettingsNotSaved = true;
		}

		private void EditValueChanged(object sender, EventArgs e)
		{
			if (AllowToSave)
				SettingsNotSaved = true;
		}

		private void spinEditTotals_EditValueChanged(object sender, EventArgs e)
		{
			if (!AllowToSave) return;
			ViewSettingsManager.Instance.SimpleSummaryState.MonthlyValue = simpleSummaryItemContainer.TotalMonthlyValue == (decimal?)spinEditMonthly.EditValue ? null : (decimal?)spinEditMonthly.EditValue;
			ViewSettingsManager.Instance.SimpleSummaryState.TotalValue = simpleSummaryItemContainer.TotalTotalValue == (decimal?)spinEditTotal.EditValue ? null : (decimal?)spinEditTotal.EditValue;
			SettingsNotSaved = true;
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

		public string Title => comboBoxEditSlideHeader.EditValue == null ? string.Empty : comboBoxEditSlideHeader.EditValue.ToString();

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
					return comboBoxEditAdvertiser.EditValue == null ? string.Empty : comboBoxEditAdvertiser.EditValue.ToString();
				return string.Empty;
			}
		}

		public string DecisionMaker
		{
			get
			{
				if (ckDecisionMaker.Checked)
					return comboBoxEditDecisionMaker.EditValue == null ? string.Empty : comboBoxEditDecisionMaker.EditValue.ToString();
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

		public ContractSettings ContractSettings => ViewSettingsManager.Instance.SimpleSummaryState.ContractSettings;

		public Theme SelectedTheme => SettingsManager.Instance.GetSelectedTheme(SlideType.SimpleSummary);

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

		protected override void SaveChanges(string fileName = "")
		{
			ListManager.Instance.Advertisers.Add(Advertiser);
			ListManager.Instance.Advertisers.Save();

			ListManager.Instance.DecisionMakers.Add(DecisionMaker);
			ListManager.Instance.DecisionMakers.Save();

			if (SettingsNotSaved)
			{
				SaveState();
				ViewSettingsManager.Instance.SimpleSummaryState.Save(fileName);
				UpdateSavedFilesState();
			}
		}

		public void Output()
		{
			SaveChanges();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowProgress();
			AppManager.Instance.ShowFloater(() =>
			{
				AppManager.Instance.PowerPointManager.Processor.AppendSummary(this, TableOutput);
				FormProgress.CloseProgress();
			});
		}

		public void Preview()
		{
			//SaveChanges();
			//FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			//FormProgress.ShowProgress();
			//var tempFileName = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			//AppManager.Instance.PowerPointManager.Processor.PrepareSummaryEmail(tempFileName, this, TableOutput);
			//Utilities.ActivateForm(FormMain.Instance.Handle, false, false);
			//FormProgress.CloseProgress();
			//if (!File.Exists(tempFileName)) return;
			//using (var formPreview = new FormPreview(FormMain.Instance, AppManager.Instance.PowerPointManager.Processor, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater, AppManager.Instance.CheckPowerPointRunning))
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