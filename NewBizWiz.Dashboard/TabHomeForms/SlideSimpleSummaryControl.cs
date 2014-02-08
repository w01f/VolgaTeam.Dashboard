using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraTab;
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
			: base()
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
				ckPreparedFor.Font = new Font(ckPreparedFor.Font.FontFamily, ckPreparedFor.Font.Size - 3, ckPreparedFor.Font.Style);
				laAdvertiserTag.Font = new Font(laAdvertiserTag.Font.FontFamily, laAdvertiserTag.Font.Size - 2, laAdvertiserTag.Font.Style);
				ckCampaign.Font = new Font(ckCampaign.Font.FontFamily, ckCampaign.Font.Size - 2, ckCampaign.Font.Style);
				laCampaignTag.Font = new Font(laCampaignTag.Font.FontFamily, laCampaignTag.Font.Size - 2, laCampaignTag.Font.Style);
				ckMonthlyInvestment.Font = new Font(ckMonthlyInvestment.Font.FontFamily, ckMonthlyInvestment.Font.Size - 2, ckMonthlyInvestment.Font.Style);
				laMonthlyInvestment.Font = new Font(laMonthlyInvestment.Font.FontFamily, laMonthlyInvestment.Font.Size - 2, laMonthlyInvestment.Font.Style);
				laMonthlyInvestmentTag.Font = new Font(laMonthlyInvestmentTag.Font.FontFamily, laMonthlyInvestmentTag.Font.Size - 2, laMonthlyInvestmentTag.Font.Style);
				ckTotalInvestment.Font = new Font(ckTotalInvestment.Font.FontFamily, ckTotalInvestment.Font.Size - 2, ckTotalInvestment.Font.Style);
				laTotalInvestment.Font = new Font(laTotalInvestment.Font.FontFamily, laTotalInvestment.Font.Size - 2, laTotalInvestment.Font.Style);
				laTotalInvestmentTag.Font = new Font(laTotalInvestmentTag.Font.FontFamily, laTotalInvestmentTag.Font.Size - 2, laTotalInvestmentTag.Font.Style);
				ckEnableTotalsEdit.Font = new Font(ckEnableTotalsEdit.Font.FontFamily, ckEnableTotalsEdit.Font.Size - 2, ckEnableTotalsEdit.Font.Style);
				ckPresentationDate.Font = new Font(ckPresentationDate.Font.FontFamily, ckPresentationDate.Font.Size - 2, ckPresentationDate.Font.Style);
				laPresentationDateTag.Font = new Font(laPresentationDateTag.Font.FontFamily, laPresentationDateTag.Font.Size - 2, laPresentationDateTag.Font.Style);
				laDetails.Font = new Font(laDetails.Font.FontFamily, laDetails.Font.Size - 3, laDetails.Font.Style);
				laMonthly.Font = new Font(laMonthly.Font.FontFamily, laMonthly.Font.Size - 3, laMonthly.Font.Style);
				laTotal.Font = new Font(laTotal.Font.FontFamily, laTotal.Font.Size - 3, laTotal.Font.Style);
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

			simpleSummaryItemContainer.OutputContainer = simpleSummaryOutputContainer;

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

		public void UpdateTotalValues()
		{
			laMonthlyInvestmentTag.Text = (simpleSummaryItemContainer.TotalMonthlyValue.HasValue ? simpleSummaryItemContainer.TotalMonthlyValue.Value : 0).ToString("$#,##0.00");
			pnMonthInvestments.Visible = simpleSummaryItemContainer.ShowMonthlyTotal;
			laTotalInvestmentTag.Text = (simpleSummaryItemContainer.TotalTotalValue.HasValue ? simpleSummaryItemContainer.TotalTotalValue.Value : 0).ToString("$#,##0.00");
			pnTotalInvestments.Visible = simpleSummaryItemContainer.ShowTotalTotal;
		}

		public void UpdateOutputTotalValues()
		{
			laMonthly.Visible = simpleSummaryItemContainer.TotalMonthlyValue.HasValue;
			laTotal.Visible = simpleSummaryItemContainer.TotalTotalValue.HasValue;
			if (laMonthly.Visible && laTotal.Visible)
			{
				laMonthly.Width = 150;
				laMonthly.Text = "Monthly$:";
				laMonthly.TextAlign = ContentAlignment.MiddleLeft;
				laTotal.Width = 150;
				laTotal.Text = "Total$:";
				laTotal.TextAlign = ContentAlignment.MiddleLeft;
			}
			else if (laMonthly.Visible)
			{
				laMonthly.Width = 300;
				laMonthly.Text = "Monthly Investment:";
				laMonthly.TextAlign = ContentAlignment.MiddleRight;
			}
			else if (laTotal.Visible)
			{
				laTotal.Width = 300;
				laTotal.Text = "Total Investment:";
				laTotal.TextAlign = ContentAlignment.MiddleRight;
			}

			if (!ckEnableTotalsEdit.Checked)
			{
				spinEditMonthly.Value = (decimal)simpleSummaryItemContainer.OutputTotalMonthlyValue;
				spinEditTotal.Value = (decimal)simpleSummaryItemContainer.OutputTotalTotalValue;
			}
		}

		public void ResetTab()
		{
			xtraTabControl.SelectedTabPage = xtraTabPageAdvertiser;
		}

		public void UpdateSavedFilesState()
		{
			SetLoadState(ViewSettingsManager.Instance.SimpleSummaryState.AllowToLoad());
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
			ckPreparedFor.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowAdvertiser;
			comboBoxEditAdvertiser.EditValue = string.IsNullOrEmpty(ViewSettingsManager.Instance.SimpleSummaryState.Advertiser) ? null : ViewSettingsManager.Instance.SimpleSummaryState.Advertiser;

			ckDecisionMaker.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowDecisionMaker;
			comboBoxEditDecisionMaker.EditValue = string.IsNullOrEmpty(ViewSettingsManager.Instance.SimpleSummaryState.DecisionMaker) ? null : ViewSettingsManager.Instance.SimpleSummaryState.DecisionMaker;

			ckDate.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowPresentationDate;
			ckPresentationDate.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowPresentationDate;
			if (ckDate.Checked)
				dateEditDate.EditValue = ViewSettingsManager.Instance.SimpleSummaryState.PresentationDate != DateTime.MinValue ? (object)ViewSettingsManager.Instance.SimpleSummaryState.PresentationDate : null;
			else
				dateEditDate.EditValue = null;

			ckFlightDates.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowFlightDates;
			ckCampaign.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowFlightDates;
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

			ckMonthlyInvestment.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowMonthly;
			ckTotalInvestment.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowTotal;

			ckEnableTotalsEdit.Checked = ViewSettingsManager.Instance.SimpleSummaryState.EnableTotalsEdit;
			if (ckEnableTotalsEdit.Checked)
			{
				spinEditMonthly.Value = (decimal)ViewSettingsManager.Instance.SimpleSummaryState.MonthlyValue;
				spinEditTotal.Value = (decimal)ViewSettingsManager.Instance.SimpleSummaryState.TotalValue;
			}

			simpleSummaryItemContainer.LoadItems();

			AllowToSave = true;
			SettingsNotSaved = false;

			UpdateTotalItems();
			UpdateSavedFilesState();

			xtraTabControl_SelectedPageChanged(xtraTabControl, new TabPageChangedEventArgs(null, xtraTabControl.SelectedTabPage));
		}

		private void SaveState()
		{
			simpleSummaryItemContainer.SaveItems();

			ViewSettingsManager.Instance.SimpleSummaryState.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : null;
			ViewSettingsManager.Instance.SimpleSummaryState.ShowAdvertiser = ckAdvertiser.Checked & ckPreparedFor.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.Advertiser = comboBoxEditAdvertiser.EditValue != null ? comboBoxEditAdvertiser.EditValue.ToString() : string.Empty;

			ViewSettingsManager.Instance.SimpleSummaryState.ShowDecisionMaker = ckDecisionMaker.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.DecisionMaker = comboBoxEditDecisionMaker.EditValue != null ? comboBoxEditDecisionMaker.EditValue.ToString() : string.Empty;
			ckDate.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ShowPresentationDate;

			ViewSettingsManager.Instance.SimpleSummaryState.ShowPresentationDate = ckPresentationDate.Checked & ckDate.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.PresentationDate = dateEditDate.EditValue != null ? dateEditDate.DateTime : DateTime.MinValue;

			ViewSettingsManager.Instance.SimpleSummaryState.ShowFlightDates = ckFlightDates.Checked & ckCampaign.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesStart = dateEditFligtDatesStart.EditValue != null ? dateEditFligtDatesStart.DateTime : DateTime.MinValue;
			ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesEnd = dateEditFligtDatesEnd.EditValue != null ? dateEditFligtDatesEnd.DateTime : DateTime.MinValue;

			ViewSettingsManager.Instance.SimpleSummaryState.ShowMonthly = ckMonthlyInvestment.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.ShowTotal = ckTotalInvestment.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.EnableTotalsEdit = ckEnableTotalsEdit.Checked;
			ViewSettingsManager.Instance.SimpleSummaryState.MonthlyValue = (double)spinEditMonthly.Value;
			ViewSettingsManager.Instance.SimpleSummaryState.TotalValue = (double)spinEditTotal.Value;

			AllowToSave = true;
			SettingsNotSaved = false;
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

		private void xtraTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			laTotalItems.Visible = false;
			buttonXAddItem.Visible = false;

			ckPreparedFor.Visible = false;
			laAdvertiserTag.Visible = false;
			ckCampaign.Visible = false;
			laCampaignTag.Visible = false;
			pnTotals.Visible = false;
			ckPresentationDate.Visible = false;
			laPresentationDateTag.Visible = false;
			ckEnableTotalsEdit.Visible = false;
			ckMonthlyInvestment.Visible = false;
			spinEditMonthly.Visible = false;
			ckTotalInvestment.Visible = false;
			spinEditTotal.Visible = false;
			pnHeaderBorder.Visible = false;
			simpleSummaryOutputContainer.Visible = false;
			pnOutputWarning.Visible = false;

			SetOutputState(false);

			if (e.Page == xtraTabPagePaymentDetails)
			{
				laTotalItems.Visible = true;
				buttonXAddItem.Visible = true;
				pnTotals.Visible = true;
				UpdateTotalValues();
			}
			else if (e.Page == xtraTabPageOutput)
			{
				bool itemsComplited = simpleSummaryItemContainer.ItemsComplited;
				pnHeaderBorder.Visible = itemsComplited;
				simpleSummaryOutputContainer.Visible = itemsComplited;
				simpleSummaryOutputContainer.BringToFront();
				pnOutputWarning.Visible = !itemsComplited;
				ckPreparedFor.Visible = itemsComplited;
				laAdvertiserTag.Visible = itemsComplited;
				ckCampaign.Visible = itemsComplited;
				laCampaignTag.Visible = itemsComplited;
				ckPresentationDate.Visible = itemsComplited;
				laPresentationDateTag.Visible = itemsComplited;
				ckEnableTotalsEdit.Visible = itemsComplited;
				ckMonthlyInvestment.Visible = itemsComplited;
				spinEditMonthly.Visible = itemsComplited;
				ckTotalInvestment.Visible = itemsComplited;
				spinEditTotal.Visible = itemsComplited;
				UpdateOutputTotalValues();
				simpleSummaryItemContainer.HideTotals();
				simpleSummaryItemContainer.HideDescription();
				laAdvertiserTag.Text = Advertiser;
				laCampaignTag.Text = CampaignDates;
				laPresentationDateTag.Text = PresentationDate;

				SetOutputState(itemsComplited);
			}
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

		private void ckEnableTotals_CheckedChanged(object sender, EventArgs e)
		{
			spinEditMonthly.Properties.ReadOnly = !ckEnableTotalsEdit.Checked;
			spinEditTotal.Properties.ReadOnly = !ckEnableTotalsEdit.Checked;
			spinEditMonthly.Enabled = ckEnableTotalsEdit.Checked;
			spinEditTotal.Enabled = ckEnableTotalsEdit.Checked;
			UpdateOutputTotalValues();
			if (AllowToSave)
				SettingsNotSaved = true;
		}

		private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
		{
			if (AllowToSave && ckEnableTotalsEdit.Checked)
				SettingsNotSaved = true;
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			if (AllowToSave)
				SettingsNotSaved = true;
		}

		private void edit_EditValueChanged(object sender, EventArgs e)
		{
			if (AllowToSave)
				SettingsNotSaved = true;
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
				if (ckAdvertiser.Checked && ckPreparedFor.Checked)
					return comboBoxEditAdvertiser.EditValue == null ? string.Empty : comboBoxEditAdvertiser.EditValue.ToString();
				else
					return string.Empty;
			}
		}

		public string DecisionMaker
		{
			get
			{
				if (ckDecisionMaker.Checked)
					return comboBoxEditDecisionMaker.EditValue == null ? string.Empty : comboBoxEditDecisionMaker.EditValue.ToString();
				else
					return string.Empty;
			}
		}

		public string PresentationDate
		{
			get
			{
				if (ckDate.Checked && ckPresentationDate.Checked)
					return dateEditDate.EditValue != null ? !dateEditDate.DateTime.Equals(DateTime.MinValue) ? dateEditDate.DateTime.ToString("MMMM dd, yyyy") : string.Empty : string.Empty;
				else
					return string.Empty;
			}
		}

		public string CampaignDates
		{
			get
			{
				if (ckFlightDates.Checked && ckCampaign.Checked)
					return (dateEditFligtDatesStart.EditValue != null ? !dateEditFligtDatesStart.DateTime.Equals(DateTime.MinValue) ? dateEditFligtDatesStart.DateTime.ToString("M/d/yyyy") : string.Empty : string.Empty) +
						   (dateEditFligtDatesEnd.EditValue != null ? !dateEditFligtDatesEnd.DateTime.Equals(DateTime.MinValue) ? " - " + dateEditFligtDatesEnd.DateTime.ToString("M/d/yyyy") : string.Empty : string.Empty);
				else
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
			get { return ckMonthlyInvestment.Checked ? (ckEnableTotalsEdit.Checked ? (double)spinEditMonthly.Value : simpleSummaryItemContainer.OutputTotalMonthlyValue).ToString("$#,##0.00") : string.Empty; }
		}

		public string TotalTotalValue
		{
			get { return ckTotalInvestment.Checked ? (ckEnableTotalsEdit.Checked ? (double)spinEditTotal.Value : simpleSummaryItemContainer.OutputTotalTotalValue).ToString("$#,##0.00") : string.Empty; }
		}

		public bool ShowMonthlyHeader
		{
			get { return simpleSummaryItemContainer.TotalMonthlyValue.HasValue; }
		}

		public bool ShowTotalHeader
		{
			get { return simpleSummaryItemContainer.TotalTotalValue.HasValue; }
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