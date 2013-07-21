using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms;
using ListManager = NewBizWiz.Core.OnlineSchedule.ListManager;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	public partial class ProductSummaryControl : UserControl
	{
		private bool _allowToSave = true;
		private Schedule _localSchedule;

		public ProductSummaryControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) =>
			{
				if (sender != this)
					LoadSchedule();
			};
			SettingsNotSaved = false;
		}

		public bool SettingsNotSaved { get; set; }

		public bool AllowToLeaveControl
		{
			get
			{
				bool result = false;
				if (SettingsNotSaved)
				{
					if (Utilities.Instance.ShowWarningQuestion("Schedule settings have changed.\nDo you want to save changes?") == DialogResult.Yes)
					{
						if (SaveSchedule())
							result = true;
					}
				}
				else
					result = true;
				return result;
			}
		}

		public void LoadSchedule()
		{
			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(ListManager.Instance.SlideHeaders.ToArray());

			labelControlPresentationDate.Text = "Presentation Date: " + _localSchedule.PresentationDate.ToString("MM/dd/yy");
			labelControlFlightDates.Text = "Online Campaign Dates: " + _localSchedule.FlightDates;
			labelControlAdvertiser.Text = "Prepared for: " + _localSchedule.BusinessName;
			labelControlDecisionMaker.Text = _localSchedule.DecisionMaker;

			labelControlMonthlyImpressions.Text = "Monthly Impressions: " + _localSchedule.MonthlyImpressions.ToString("#,##0");
			labelControlMonthlyInvestment.Text = "Monthly Investment: " + _localSchedule.MonthlyInvestment.ToString("$#,###.00");
			labelControlTotalImpressions.Text = "Total Impressions: " + _localSchedule.TotalImpressions.ToString("#,##0");
			labelControlTotalInvestment.Text = "Total Investment: " + _localSchedule.TotalInvestment.ToString("$#,###.00");

			gridControlProductSummary.DataSource = new BindingList<DigitalProduct>(_localSchedule.Products);

			_allowToSave = false;
			if (!string.IsNullOrEmpty(_localSchedule.ProductSummarySettings.SlideHeader))
				comboBoxEditSlideHeader.EditValue = _localSchedule.ProductSummarySettings.SlideHeader;
			else if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
				comboBoxEditSlideHeader.SelectedIndex = 0;

			checkEditShowTotalsLastSlideSummary.Checked = _localSchedule.ProductSummarySettings.ShowTotalsOnLastOnly;
			checkEditShowTotalsLastSlideSummary.Visible = _localSchedule.Products.Count > 5;

			UpdateToogledButtons();
			UpdateSlideFormat();
			UpdateTotals();
			UpdateGridColumns();
			_allowToSave = true;

			SettingsNotSaved = false;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			if (!string.IsNullOrEmpty(scheduleName))
				_localSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(_localSchedule, string.IsNullOrEmpty(scheduleName), this);
			SettingsNotSaved = false;
			return true;
		}

		private void UpdateToogledButtons()
		{
			Controller.Instance.WebSummaryActiveDays.Checked = _localSchedule.ProductSummarySettings.ShowActiveDays;
			Controller.Instance.WebSummaryAdRate.Checked = _localSchedule.ProductSummarySettings.ShowAdRate;
			Controller.Instance.WebSummaryCPM.Checked = _localSchedule.ProductSummarySettings.ShowCPM;
			Controller.Instance.WebSummaryDimensions.Checked = _localSchedule.ProductSummarySettings.ShowDimensions;
			Controller.Instance.WebSummaryProductImpressions.Checked = _localSchedule.ProductSummarySettings.ShowImpressions;
			Controller.Instance.WebSummaryProductInvestment.Checked = _localSchedule.ProductSummarySettings.ShowInvestment;
			Controller.Instance.WebSummaryMonthlyImpressions.Checked = _localSchedule.ProductSummarySettings.ShowMonthlyImpressions;
			Controller.Instance.WebSummaryMonthlyInvestment.Checked = _localSchedule.ProductSummarySettings.ShowMonthlyInvestment;
			Controller.Instance.WebSummaryTotalAds.Checked = _localSchedule.ProductSummarySettings.ShowTotalAds;
			Controller.Instance.WebSummaryTotalImpressions.Checked = _localSchedule.ProductSummarySettings.ShowTotalImpressions;
			Controller.Instance.WebSummaryTotalInvestment.Checked = _localSchedule.ProductSummarySettings.ShowTotalInvestment;
			Controller.Instance.WebSummaryWebsites.Checked = _localSchedule.ProductSummarySettings.ShowWebsites;
			Controller.Instance.WebSummaryPowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductSummarySettings.ShowWebsites |
			                                                                                           _localSchedule.ProductSummarySettings.ShowDimensions |
			                                                                                           _localSchedule.ProductSummarySettings.ShowActiveDays |
			                                                                                           _localSchedule.ProductSummarySettings.ShowAdRate |
			                                                                                           _localSchedule.ProductSummarySettings.ShowTotalAds |
			                                                                                           _localSchedule.ProductSummarySettings.ShowImpressions |
			                                                                                           _localSchedule.ProductSummarySettings.ShowInvestment |
			                                                                                           _localSchedule.ProductSummarySettings.ShowCPM);
			Controller.Instance.WebSummaryEmail.Enabled = Controller.Instance.WebSummaryPowerPoint.Enabled;
		}

		private void UpdateSlideFormat()
		{
			Controller.Instance.WebSummaryPowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductSummarySettings.ShowWebsites |
			                                                                                           _localSchedule.ProductSummarySettings.ShowDimensions |
			                                                                                           _localSchedule.ProductSummarySettings.ShowActiveDays |
			                                                                                           _localSchedule.ProductSummarySettings.ShowAdRate |
			                                                                                           _localSchedule.ProductSummarySettings.ShowTotalAds |
			                                                                                           _localSchedule.ProductSummarySettings.ShowImpressions |
			                                                                                           _localSchedule.ProductSummarySettings.ShowInvestment |
			                                                                                           _localSchedule.ProductSummarySettings.ShowCPM);
			Controller.Instance.WebSummaryEmail.Enabled = Controller.Instance.WebSummaryPowerPoint.Enabled;
		}

		private void SaveSlideFormat()
		{
			if (_allowToSave)
			{
				SettingsNotSaved = true;
				UpdateSlideFormat();
			}
		}

		private void UpdateTotals()
		{
			labelControlTotalImpressions.Visible = _localSchedule.ProductSummarySettings.ShowTotalImpressions;
			labelControlTotalInvestment.Visible = _localSchedule.ProductSummarySettings.ShowTotalInvestment;
			pnPackageSummaryTotal.Visible = _localSchedule.ProductSummarySettings.ShowTotalImpressions || _localSchedule.ProductSummarySettings.ShowTotalInvestment;
			if (_localSchedule.ProductSummarySettings.ShowTotalImpressions || _localSchedule.ProductSummarySettings.ShowTotalInvestment)
				pnPackageSummaryTotal.BringToFront();

			labelControlMonthlyImpressions.Visible = _localSchedule.ProductSummarySettings.ShowMonthlyImpressions;
			labelControlMonthlyInvestment.Visible = _localSchedule.ProductSummarySettings.ShowMonthlyInvestment;
			pnPackageSummaryMonthly.Visible = _localSchedule.ProductSummarySettings.ShowMonthlyImpressions || _localSchedule.ProductSummarySettings.ShowMonthlyInvestment;
			if (_localSchedule.ProductSummarySettings.ShowMonthlyImpressions || _localSchedule.ProductSummarySettings.ShowMonthlyInvestment)
				pnPackageSummaryMonthly.SendToBack();
		}

		private void SaveTotals()
		{
			if (_allowToSave)
			{
				_localSchedule.ProductSummarySettings.ShowMonthlyImpressions = Controller.Instance.WebSummaryMonthlyImpressions.Checked;
				_localSchedule.ProductSummarySettings.ShowMonthlyInvestment = Controller.Instance.WebSummaryMonthlyInvestment.Checked;
				_localSchedule.ProductSummarySettings.ShowTotalImpressions = Controller.Instance.WebSummaryTotalImpressions.Checked;
				_localSchedule.ProductSummarySettings.ShowTotalInvestment = Controller.Instance.WebSummaryTotalInvestment.Checked;
				SettingsNotSaved = true;
				UpdateTotals();
			}
		}

		private void UpdateGridColumns()
		{
			bandedGridColumnMonthlyCPM.Visible = _localSchedule.ProductSummarySettings.ShowCPM;
			bandedGridColumnMonthlyImpressions.Visible = _localSchedule.ProductSummarySettings.ShowImpressions;
			bandedGridColumnMonthlyInvestment.Visible = _localSchedule.ProductSummarySettings.ShowInvestment;
			gridBandMonthly.Visible = _localSchedule.ProductSummarySettings.ShowCPM | _localSchedule.ProductSummarySettings.ShowImpressions | _localSchedule.ProductSummarySettings.ShowInvestment;

			bandedGridColumnTotalCPM.Visible = _localSchedule.ProductSummarySettings.ShowCPM;
			bandedGridColumnTotalImpressions.Visible = _localSchedule.ProductSummarySettings.ShowImpressions;
			bandedGridColumnTotalInvestment.Visible = _localSchedule.ProductSummarySettings.ShowInvestment;
			gridBandTotal.Visible = _localSchedule.ProductSummarySettings.ShowCPM | _localSchedule.ProductSummarySettings.ShowImpressions | _localSchedule.ProductSummarySettings.ShowInvestment;
			Controller.Instance.WebSummaryPowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductSummarySettings.ShowWebsites |
			                                                                                           _localSchedule.ProductSummarySettings.ShowDimensions |
			                                                                                           _localSchedule.ProductSummarySettings.ShowActiveDays |
			                                                                                           _localSchedule.ProductSummarySettings.ShowAdRate |
			                                                                                           _localSchedule.ProductSummarySettings.ShowTotalAds |
			                                                                                           _localSchedule.ProductSummarySettings.ShowImpressions |
			                                                                                           _localSchedule.ProductSummarySettings.ShowInvestment |
			                                                                                           _localSchedule.ProductSummarySettings.ShowCPM);
			Controller.Instance.WebSummaryEmail.Enabled = Controller.Instance.WebSummaryPowerPoint.Enabled;
		}

		private void SaveGridColumns()
		{
			if (_allowToSave)
			{
				_localSchedule.ProductSummarySettings.ShowCPM = Controller.Instance.WebSummaryCPM.Checked;
				_localSchedule.ProductSummarySettings.ShowImpressions = Controller.Instance.WebSummaryProductImpressions.Checked;
				_localSchedule.ProductSummarySettings.ShowInvestment = Controller.Instance.WebSummaryProductInvestment.Checked;
				SettingsNotSaved = true;
				UpdateGridColumns();
			}
		}

		private void SavePreviewOption()
		{
			if (_allowToSave)
			{
				_localSchedule.ProductSummarySettings.ShowActiveDays = Controller.Instance.WebSummaryActiveDays.Checked;
				_localSchedule.ProductSummarySettings.ShowAdRate = Controller.Instance.WebSummaryAdRate.Checked;
				_localSchedule.ProductSummarySettings.ShowDimensions = Controller.Instance.WebSummaryDimensions.Checked;
				_localSchedule.ProductSummarySettings.ShowTotalAds = Controller.Instance.WebSummaryTotalAds.Checked;
				_localSchedule.ProductSummarySettings.ShowWebsites = Controller.Instance.WebSummaryWebsites.Checked;
				Controller.Instance.WebSummaryPowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductSummarySettings.ShowWebsites |
				                                                                                           _localSchedule.ProductSummarySettings.ShowDimensions |
				                                                                                           _localSchedule.ProductSummarySettings.ShowActiveDays |
				                                                                                           _localSchedule.ProductSummarySettings.ShowAdRate |
				                                                                                           _localSchedule.ProductSummarySettings.ShowTotalAds |
				                                                                                           _localSchedule.ProductSummarySettings.ShowImpressions |
				                                                                                           _localSchedule.ProductSummarySettings.ShowInvestment |
				                                                                                           _localSchedule.ProductSummarySettings.ShowCPM);
				Controller.Instance.WebSummaryEmail.Enabled = Controller.Instance.WebSummaryPowerPoint.Enabled;
				SettingsNotSaved = true;
			}
		}

		private void bandedGridViewProductSummary_CalcPreviewText(object sender, CalcPreviewTextEventArgs e)
		{
			var previewText = new List<string>();
			if (_localSchedule.ProductSummarySettings.ShowDimensions && !string.IsNullOrEmpty(_localSchedule.Products[e.RowHandle].Dimensions))
				previewText.Add("Ad Dimensions: " + _localSchedule.Products[e.RowHandle].Dimensions);
			if (_localSchedule.ProductSummarySettings.ShowWebsites && !string.IsNullOrEmpty(_localSchedule.Products[e.RowHandle].AllWebsites))
				previewText.Add("Websites: " + _localSchedule.Products[e.RowHandle].AllWebsites);
			if (_localSchedule.ProductSummarySettings.ShowTotalAds && _localSchedule.Products[e.RowHandle].TotalAds.HasValue)
				previewText.Add("Total Ads: " + _localSchedule.Products[e.RowHandle].TotalAds.Value.ToString("#,##0"));
			if (_localSchedule.ProductSummarySettings.ShowActiveDays && _localSchedule.Products[e.RowHandle].ActiveDays.HasValue)
				previewText.Add("Active Days: " + _localSchedule.Products[e.RowHandle].ActiveDays.Value.ToString("#,##0"));
			if (_localSchedule.ProductSummarySettings.ShowAdRate && _localSchedule.Products[e.RowHandle].AdRate.HasValue)
				previewText.Add("Ad Rate: " + _localSchedule.Products[e.RowHandle].AdRate.Value.ToString("$#,###.00"));
			e.PreviewText = string.Join(";  ", previewText.ToArray());
			if (string.IsNullOrEmpty(e.PreviewText))
				e.PreviewText = "            ";
		}

		private void PrepareOutput()
		{
			_localSchedule.ProductSummarySettings.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : (comboBoxEditSlideHeader.Properties.Items.Count > 0 ? comboBoxEditSlideHeader.Properties.Items[0].ToString() : string.Empty);
			var temp = new List<string>();
			temp.Clear();
			if (_localSchedule.ProductSummarySettings.ShowMonthlyImpressions)
				temp.Add("Monthly Impressions");
			if (_localSchedule.ProductSummarySettings.ShowTotalImpressions)
				temp.Add("Total Impressions");
			if (_localSchedule.ProductSummarySettings.ShowMonthlyInvestment)
				temp.Add("Monthly Investment");
			if (_localSchedule.ProductSummarySettings.ShowTotalInvestment)
				temp.Add("Total Investment");

			for (int i = 0; i < 4; i++)
			{
				switch (i)
				{
					case 0:
						if (temp.Count > 0)
							_localSchedule.ProductSummarySettings.TotalHeader1 = temp[0];
						else
							_localSchedule.ProductSummarySettings.TotalHeader1 = string.Empty;
						break;
					case 1:
						if (temp.Count > 1)
							_localSchedule.ProductSummarySettings.TotalHeader2 = temp[1];
						else
							_localSchedule.ProductSummarySettings.TotalHeader2 = string.Empty;
						break;
					case 2:
						if (temp.Count > 2)
							_localSchedule.ProductSummarySettings.TotalHeader3 = temp[2];
						else
							_localSchedule.ProductSummarySettings.TotalHeader3 = string.Empty;
						break;
					case 3:
						if (temp.Count > 3)
							_localSchedule.ProductSummarySettings.TotalHeader4 = temp[3];
						else
							_localSchedule.ProductSummarySettings.TotalHeader4 = string.Empty;
						break;
				}
			}
			temp.Clear();
			if (_localSchedule.ProductSummarySettings.ShowMonthlyImpressions)
				temp.Add(_localSchedule.MonthlyImpressions.ToString("#,##0"));
			if (_localSchedule.ProductSummarySettings.ShowTotalImpressions)
				temp.Add(_localSchedule.TotalImpressions.ToString("#,##0"));
			if (_localSchedule.ProductSummarySettings.ShowMonthlyInvestment)
				temp.Add(_localSchedule.MonthlyInvestment.ToString("$#,###.00"));
			if (_localSchedule.ProductSummarySettings.ShowTotalInvestment)
				temp.Add(_localSchedule.TotalInvestment.ToString("$#,###.00"));

			for (int i = 0; i < 4; i++)
			{
				switch (i)
				{
					case 0:
						if (temp.Count > 0)
							_localSchedule.ProductSummarySettings.TotalValue1 = temp[0];
						else
							_localSchedule.ProductSummarySettings.TotalValue1 = string.Empty;
						break;
					case 1:
						if (temp.Count > 1)
							_localSchedule.ProductSummarySettings.TotalValue2 = temp[1];
						else
							_localSchedule.ProductSummarySettings.TotalValue2 = string.Empty;
						break;
					case 2:
						if (temp.Count > 2)
							_localSchedule.ProductSummarySettings.TotalValue3 = temp[2];
						else
							_localSchedule.ProductSummarySettings.TotalValue3 = string.Empty;
						break;
					case 3:
						if (temp.Count > 3)
							_localSchedule.ProductSummarySettings.TotalValue4 = temp[3];
						else
							_localSchedule.ProductSummarySettings.TotalValue4 = string.Empty;
						break;
				}
			}
		}

		public void PreviewOptions_CheckedChanged(object sender, EventArgs e)
		{
			SavePreviewOption();
			bandedGridViewProductSummary.RefreshData();
		}

		public void ColumnsOptions_CheckedChanged(object sender, EventArgs e)
		{
			SaveGridColumns();
		}

		public void TotalsOptions_CheckedChanged(object sender, EventArgs e)
		{
			SaveTotals();
		}

		public void Help_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("summary");
		}

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			using (var formGridType = new FormGridType())
			{
				DialogResult result = formGridType.ShowDialog();
				if (result != DialogResult.Cancel)
				{
					PrepareOutput();
					using (var formProgress = new FormProgress())
					{
						formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
						formProgress.TopMost = true;
						formProgress.Show();
						OnlineSchedulePowerPointHelper.Instance.AppendProductSummary(_localSchedule, result == DialogResult.No);
						formProgress.Close();
					}
					using (var formOutput = new FormSlideOutput())
					{
						if (formOutput.ShowDialog() != DialogResult.OK)
							Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
					}
				}
			}
		}

		public void Email_Click(object sender, EventArgs e)
		{
			using (var formGridType = new FormGridType())
			{
				DialogResult result = formGridType.ShowDialog();
				if (result != DialogResult.Cancel)
				{
					PrepareOutput();
					using (var formProgress = new FormProgress())
					{
						formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
						formProgress.TopMost = true;
						formProgress.Show();
						string tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
						OnlineSchedulePowerPointHelper.Instance.PrepareSummaryEmail(tempFileName, _localSchedule, result == DialogResult.No);
						formProgress.Close();
						if (File.Exists(tempFileName))
							using (var formEmail = new FormEmail())
							{
								formEmail.Text = "Email this Online Schedule";
								formEmail.PresentationFile = tempFileName;
								RegistryHelper.MainFormHandle = formEmail.Handle;
								RegistryHelper.MaximizeMainForm = false;
								formEmail.ShowDialog();
								RegistryHelper.MaximizeMainForm = true;
								RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
							}
					}
				}
			}
		}

		private void checkEditShowTotalsLastSlide_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				_localSchedule.ProductSummarySettings.ShowTotalsOnLastOnly = (sender as CheckEdit).Checked;
				SettingsNotSaved = true;
			}
		}

		private void comboBoxEditSlideHeader_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				_localSchedule.ProductSummarySettings.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : (comboBoxEditSlideHeader.Properties.Items.Count > 0 ? comboBoxEditSlideHeader.Properties.Items[0].ToString() : string.Empty);
				SettingsNotSaved = true;
			}
		}

		public void Save_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			Utilities.Instance.ShowInformation("Schedule Saved");
		}

		public void SaveAs_Click(object sender, EventArgs e)
		{
			using (var from = new FormNewSchedule())
			{
				from.Text = "Save Schedule";
				from.laLogo.Text = "Please set a new name for your Schedule:";
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(from.ScheduleName))
					{
						if (SaveSchedule(from.ScheduleName))
							Utilities.Instance.ShowInformation("Schedule was saved");
					}
					else
					{
						Utilities.Instance.ShowWarning("Schedule Name can't be empty");
					}
				}
			}
		}
	}
}