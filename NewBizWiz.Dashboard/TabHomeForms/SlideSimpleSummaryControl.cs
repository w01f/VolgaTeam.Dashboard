using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Dashboard;
using NewBizWiz.Dashboard.InteropClasses;
using ListManager = NewBizWiz.Core.Dashboard.ListManager;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public sealed partial class SlideSimpleSummaryControl : SlideBaseControl
	{
		private readonly SuperTooltipInfo _toolTip = new SuperTooltipInfo("HELP", "", "Help me with the Closing Summary Slide", null, null, eTooltipColor.Gray);

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
				checkEditMonthlyInvestment.Font = new Font(checkEditMonthlyInvestment.Font.FontFamily, checkEditMonthlyInvestment.Font.Size - 2, checkEditMonthlyInvestment.Font.Style);
				checkEditTotalInvestment.Font = new Font(checkEditTotalInvestment.Font.FontFamily, checkEditTotalInvestment.Font.Size - 2, checkEditTotalInvestment.Font.Style);
			}
			comboBoxEditAdvertiser.MouseUp += FormMain.Instance.Editor_MouseUp;
			comboBoxEditAdvertiser.MouseDown += FormMain.Instance.Editor_MouseDown;
			comboBoxEditAdvertiser.Enter += FormMain.Instance.Editor_Enter;
			comboBoxEditDecisionMaker.MouseUp += FormMain.Instance.Editor_MouseUp;
			comboBoxEditDecisionMaker.MouseDown += FormMain.Instance.Editor_MouseDown;
			comboBoxEditDecisionMaker.Enter += FormMain.Instance.Editor_Enter;
			spinEditMonthly.MouseUp += FormMain.Instance.Editor_MouseUp;
			spinEditMonthly.MouseDown += FormMain.Instance.Editor_MouseDown;
			spinEditMonthly.Enter += FormMain.Instance.Editor_Enter;
			spinEditTotal.MouseUp += FormMain.Instance.Editor_MouseUp;
			spinEditTotal.MouseDown += FormMain.Instance.Editor_MouseDown;
			spinEditTotal.Enter += FormMain.Instance.Editor_Enter;

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(ListManager.Instance.SimpleSummaryLists.Headers);
			if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
				comboBoxEditSlideHeader.SelectedIndex = 0;

			comboBoxEditAdvertiser.Properties.Items.Clear();
			comboBoxEditAdvertiser.Properties.Items.AddRange(Core.Common.ListManager.Instance.Advertisers);

			comboBoxEditDecisionMaker.Properties.Items.Clear();
			comboBoxEditDecisionMaker.Properties.Items.AddRange(Core.Common.ListManager.Instance.DecisionMakers);

			FormMain.Instance.FormClosed += (sender1, e1) =>
			{
				if (SettingsNotSaved)
				{
					SaveState();
					ViewSettingsManager.Instance.SimpleSummaryState.Save();
				}
			};

			LoadSavedState();
		}

		public bool AllowToSave { get; set; }
		public bool SettingsNotSaved { get; set; }

		public override string SlideName
		{
			get { return "Closing Summary"; }
		}

		public override SuperTooltipInfo Tooltip
		{
			get { return _toolTip; }
		}

		public override ButtonItem ThemeButton
		{
			get { return FormMain.Instance.buttonItemHomeThemeSimpleSummary; }
		}

		public void UpdateTotalItems()
		{
			laTotalItems.Text = string.Format("Total Items: {0}", simpleSummaryItemContainer.ItemsCount);
		}

		public void ResetTab()
		{
			xtraTabControl.SelectedTabPage = xtraTabPageBasicInfo;
			UpdateOutputState();
		}

		public void UpdateSavedFilesState()
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

			if (string.IsNullOrEmpty(ViewSettingsManager.Instance.SimpleSummaryState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
			{
				var index = comboBoxEditSlideHeader.Properties.Items.IndexOf(ViewSettingsManager.Instance.SimpleSummaryState.SlideHeader);
				comboBoxEditSlideHeader.SelectedIndex = index >= 0 ? index : 0;
			}

			ckAdvertiser.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowAdvertiser;
			comboBoxEditAdvertiser.EditValue = string.IsNullOrEmpty(ViewSettingsManager.Instance.SimpleSummaryState.Advertiser) ? null : ViewSettingsManager.Instance.SimpleSummaryState.Advertiser;

			ckDecisionMaker.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowDecisionMaker;
			comboBoxEditDecisionMaker.EditValue = string.IsNullOrEmpty(ViewSettingsManager.Instance.SimpleSummaryState.DecisionMaker) ? null : ViewSettingsManager.Instance.SimpleSummaryState.DecisionMaker;

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
			UpdateTotalValues();

			AllowToSave = true;
			SettingsNotSaved = false;

			UpdateTotalItems();
			UpdateSavedFilesState();
		}

		private void SaveState()
		{
			simpleSummaryItemContainer.SaveItems();

			ViewSettingsManager.Instance.SimpleSummaryState.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : null;
			ViewSettingsManager.Instance.SimpleSummaryState.ShowAdvertiser = ckAdvertiser.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.Advertiser = comboBoxEditAdvertiser.EditValue != null ? comboBoxEditAdvertiser.EditValue.ToString() : string.Empty;

			ViewSettingsManager.Instance.SimpleSummaryState.ShowDecisionMaker = ckDecisionMaker.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.DecisionMaker = comboBoxEditDecisionMaker.EditValue != null ? comboBoxEditDecisionMaker.EditValue.ToString() : string.Empty;
			ckDate.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowPresentationDate;

			ViewSettingsManager.Instance.SimpleSummaryState.ShowPresentationDate = ckDate.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.PresentationDate = dateEditDate.EditValue != null ? dateEditDate.DateTime : DateTime.MinValue;

			ViewSettingsManager.Instance.SimpleSummaryState.ShowFlightDates = ckFlightDates.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesStart = dateEditFligtDatesStart.EditValue != null ? dateEditFligtDatesStart.DateTime : DateTime.MinValue;
			ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesEnd = dateEditFligtDatesEnd.EditValue != null ? dateEditFligtDatesEnd.DateTime : DateTime.MinValue;

			ViewSettingsManager.Instance.SimpleSummaryState.ShowMonthly = checkEditMonthlyInvestment.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.ShowTotal = checkEditTotalInvestment.Checked;
			UpdateTotalValues();

			AllowToSave = true;
			SettingsNotSaved = false;
		}

		public void UpdateTotalValues()
		{
			spinEditMonthly.EditValue = ViewSettingsManager.Instance.SimpleSummaryState.MonthlyValue.HasValue ? ViewSettingsManager.Instance.SimpleSummaryState.MonthlyValue.Value : simpleSummaryItemContainer.TotalMonthlyValue;
			spinEditTotal.EditValue = ViewSettingsManager.Instance.SimpleSummaryState.TotalValue.HasValue ? ViewSettingsManager.Instance.SimpleSummaryState.TotalValue.Value : simpleSummaryItemContainer.TotalTotalValue;
		}

		protected override void SavedFiles_Click(object sender, EventArgs e)
		{
			using (var form = new FormSavedSimpleSummary())
			{
				if (form.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(form.SelectedFile)) return;
				ViewSettingsManager.Instance.SimpleSummaryState.Load(form.SelectedFile);
				LoadSavedState();
			}
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
			if (AllowToSave)
				SettingsNotSaved = true;
		}

		private void ckDecisionMaker_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditDecisionMaker.Enabled = ckDecisionMaker.Checked;
			if (AllowToSave)
				SettingsNotSaved = true;
		}

		private void ckDate_CheckedChanged(object sender, EventArgs e)
		{
			dateEditDate.Enabled = ckDate.Checked;
			if (AllowToSave)
				SettingsNotSaved = true;
		}

		private void ckFlightDates_CheckedChanged(object sender, EventArgs e)
		{
			dateEditFligtDatesStart.Enabled = ckFlightDates.Checked;
			dateEditFligtDatesEnd.Enabled = ckFlightDates.Checked;
			if (AllowToSave)
				SettingsNotSaved = true;
		}

		private void dateEditFligtDatesStart_EditValueChanged(object sender, EventArgs e)
		{
			dateEditFligtDatesEnd.Properties.NullDate = dateEditFligtDatesStart.DateTime;
			if (AllowToSave)
				SettingsNotSaved = true;
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
			if (AllowToSave)
				SettingsNotSaved = true;
		}

		private void edit_EditValueChanged(object sender, EventArgs e)
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
		public int ItemsCount
		{
			get { return simpleSummaryItemContainer.ItemTitles.Length; }
		}

		public string Title
		{
			get { return comboBoxEditSlideHeader.EditValue == null ? string.Empty : comboBoxEditSlideHeader.EditValue.ToString(); }
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

		public string[] ItemTitles
		{
			get { return simpleSummaryItemContainer.ItemTitles; }
		}

		public string[] ItemDetails
		{
			get { return simpleSummaryItemContainer.ItemDetails; }
		}

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

		public string TotalMonthlyValue
		{
			get { return checkEditMonthlyInvestment.Checked && spinEditMonthly.EditValue != null ? spinEditMonthly.Value.ToString("$#,##0.00") : String.Empty; }
		}

		public string TotalTotalValue
		{
			get { return checkEditTotalInvestment.Checked && spinEditTotal.EditValue != null ? spinEditTotal.Value.ToString("$#,##0.00") : String.Empty; }
		}

		public bool ShowMonthlyHeader
		{
			get { return simpleSummaryItemContainer.ShowMonthlyTotal; }
		}

		public bool ShowTotalHeader
		{
			get { return simpleSummaryItemContainer.ShowTotalTotal; }
		}

		private void SaveChanges()
		{
			if (!Core.Common.ListManager.Instance.Advertisers.Contains(Advertiser) && !string.IsNullOrEmpty(Advertiser))
			{
				Core.Common.ListManager.Instance.Advertisers.Add(Advertiser);
				Core.Common.ListManager.Instance.SaveAdvertisers();
			}

			if (!Core.Common.ListManager.Instance.DecisionMakers.Contains(DecisionMaker) && !string.IsNullOrEmpty(DecisionMaker))
			{
				Core.Common.ListManager.Instance.DecisionMakers.Add(DecisionMaker);
				Core.Common.ListManager.Instance.SaveDecisionMakers();
			}

			if (SettingsNotSaved)
			{
				SaveState();
				ViewSettingsManager.Instance.SimpleSummaryState.Save();
				UpdateSavedFilesState();
			}
		}

		public void Output()
		{
			SaveChanges();
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				form.TopMost = true;
				form.Show();
				AppManager.Instance.ShowFloater(null, () =>
				{
					DashboardPowerPointHelper.Instance.AppendSimpleSummary();
					form.Close();
				});
			}
		}

		public void Preview()
		{
			SaveChanges();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				DashboardPowerPointHelper.Instance.PrepareSimpleSummary(tempFileName);
				Utilities.Instance.ActivateForm(FormMain.Instance.Handle, false, false);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				using (var formPreview = new FormPreview(FormMain.Instance, DashboardPowerPointHelper.Instance, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater))
				{
					formPreview.Text = "Preview Slides";
					formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
					RegistryHelper.MainFormHandle = formPreview.Handle;
					RegistryHelper.MaximizeMainForm = false;
					var previewResult = formPreview.ShowDialog();
					RegistryHelper.MaximizeMainForm = false;
					RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
					if (previewResult != DialogResult.OK)
						AppManager.Instance.ActivateMainForm();
				}
			}
		}
		#endregion
	}
}