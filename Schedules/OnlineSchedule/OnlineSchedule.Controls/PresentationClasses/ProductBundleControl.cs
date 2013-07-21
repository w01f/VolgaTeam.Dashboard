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
	public partial class ProductBundleControl : UserControl
	{
		private bool _allowToSave = true;
		private Schedule _localSchedule;

		public ProductBundleControl()
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

		private void ProductBundleControl_Load(object sender, EventArgs e)
		{
			spinEditMonthlyImpressions.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditMonthlyImpressions.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditMonthlyImpressions.Enter += Utilities.Instance.Editor_Enter;
			spinEditMonthlyInvestment.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditMonthlyInvestment.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditMonthlyInvestment.Enter += Utilities.Instance.Editor_Enter;
			spinEditTotalImpressions.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditTotalImpressions.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditTotalImpressions.Enter += Utilities.Instance.Editor_Enter;
			spinEditTotalInvestment.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditTotalInvestment.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditTotalInvestment.Enter += Utilities.Instance.Editor_Enter;
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

			gridControlProductBundle.DataSource = new BindingList<DigitalProduct>(_localSchedule.Products);

			_allowToSave = false;
			if (!string.IsNullOrEmpty(_localSchedule.ProductBundleSettings.SlideHeader))
				comboBoxEditSlideHeader.EditValue = _localSchedule.ProductBundleSettings.SlideHeader;
			else if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
				comboBoxEditSlideHeader.SelectedIndex = 0;

			spinEditMonthlyImpressions.EditValue = _localSchedule.ProductBundleSettings.TotalMonthlyImpressions;
			spinEditMonthlyInvestment.EditValue = _localSchedule.ProductBundleSettings.TotalMonthlyInvestments;
			spinEditTotalImpressions.EditValue = _localSchedule.ProductBundleSettings.TotalImpressions;
			spinEditTotalInvestment.EditValue = _localSchedule.ProductBundleSettings.TotalInvestments;
			var investment = (double)spinEditMonthlyInvestment.Value;
			var impressions = (double)spinEditMonthlyImpressions.Value;
			checkEditMonthlyCPM.Text = "CPM: " + (impressions != 0 ? ((investment / impressions) * 1000) : 0).ToString("$#,###.00");
			investment = (double)spinEditTotalInvestment.Value;
			impressions = (double)spinEditTotalImpressions.Value;
			checkEditTotalCPM.Text = "CPM: " + (impressions != 0 ? ((investment / impressions) * 1000) : 0).ToString("$#,###.00");
			checkEditMonthlyCPM.Checked = _localSchedule.ProductBundleSettings.ShowMonthlyCPM;
			checkEditTotalCPM.Checked = _localSchedule.ProductBundleSettings.ShowTotalCPM;

			checkEditShowTotalsLastSlideBundle.Checked = _localSchedule.ProductBundleSettings.ShowTotalsOnLastOnly;
			checkEditShowTotalsLastSlideBundle.Visible = _localSchedule.Products.Count > 5;

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
			Controller.Instance.WebBundleActiveDays.Checked = _localSchedule.ProductBundleSettings.ShowActiveDays;
			Controller.Instance.WebBundleAdRate.Checked = _localSchedule.ProductBundleSettings.ShowAdRate;
			Controller.Instance.WebBundleDimensions.Checked = _localSchedule.ProductBundleSettings.ShowDimensions;
			Controller.Instance.WebBundleMonthlyImpressions.Checked = _localSchedule.ProductBundleSettings.ShowMonthlyImpressions;
			Controller.Instance.WebBundleMonthlyInvestment.Checked = _localSchedule.ProductBundleSettings.ShowMonthlyInvestment;
			Controller.Instance.WebBundleTotalAds.Checked = _localSchedule.ProductBundleSettings.ShowTotalAds;
			Controller.Instance.WebBundleTotalImpressions.Checked = _localSchedule.ProductBundleSettings.ShowTotalImpressions;
			Controller.Instance.WebBundleTotalInvestment.Checked = _localSchedule.ProductBundleSettings.ShowTotalInvestment;
			Controller.Instance.WebBundleWebsites.Checked = _localSchedule.ProductBundleSettings.ShowWebsites;
			Controller.Instance.WebBundlePowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductBundleSettings.ShowWebsites |
			                                                                                          _localSchedule.ProductBundleSettings.ShowDimensions |
			                                                                                          _localSchedule.ProductBundleSettings.ShowActiveDays |
			                                                                                          _localSchedule.ProductBundleSettings.ShowAdRate |
			                                                                                          _localSchedule.ProductBundleSettings.ShowTotalAds);
			Controller.Instance.WebBundleEmail.Enabled = Controller.Instance.WebBundlePowerPoint.Enabled;
		}

		private void UpdateSlideFormat()
		{
			_allowToSave = false;
			Controller.Instance.WebBundleProductInvestment.Checked = false;
			Controller.Instance.WebBundleProductImpressions.Checked = false;
			_allowToSave = true;
			Controller.Instance.WebBundleCPM.Checked = false;
			Controller.Instance.WebBundlePowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductBundleSettings.ShowWebsites |
			                                                                                          _localSchedule.ProductBundleSettings.ShowDimensions |
			                                                                                          _localSchedule.ProductBundleSettings.ShowActiveDays |
			                                                                                          _localSchedule.ProductBundleSettings.ShowAdRate |
			                                                                                          _localSchedule.ProductBundleSettings.ShowTotalAds);
			Controller.Instance.WebBundleEmail.Enabled = Controller.Instance.WebBundlePowerPoint.Enabled;
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
			labelControlPackageBundleMonthlyImpressions.Visible = _localSchedule.ProductBundleSettings.ShowMonthlyImpressions;
			spinEditMonthlyImpressions.Visible = _localSchedule.ProductBundleSettings.ShowMonthlyImpressions;
			labelControlPackageBundleMonthlyInvestment.Visible = _localSchedule.ProductBundleSettings.ShowMonthlyInvestment;
			spinEditMonthlyInvestment.Visible = _localSchedule.ProductBundleSettings.ShowMonthlyInvestment;
			if (_localSchedule.ProductBundleSettings.ShowMonthlyImpressions && _localSchedule.ProductBundleSettings.ShowMonthlyInvestment)
			{
				checkEditMonthlyCPM.Visible = true;
			}
			else
			{
				checkEditMonthlyCPM.Checked = false;
				checkEditMonthlyCPM.Visible = false;
			}
			pnPackageBundleMonthly.Visible = _localSchedule.ProductBundleSettings.ShowMonthlyImpressions | _localSchedule.ProductBundleSettings.ShowMonthlyInvestment;

			labelControlPackageBundleTotalImpressions.Visible = _localSchedule.ProductBundleSettings.ShowTotalImpressions;
			spinEditTotalImpressions.Visible = _localSchedule.ProductBundleSettings.ShowTotalImpressions;
			labelControlPackageBundleTotalInvestment.Visible = _localSchedule.ProductBundleSettings.ShowTotalInvestment;
			spinEditTotalInvestment.Visible = _localSchedule.ProductBundleSettings.ShowTotalInvestment;
			if (_localSchedule.ProductBundleSettings.ShowTotalImpressions && _localSchedule.ProductBundleSettings.ShowTotalInvestment)
			{
				checkEditTotalCPM.Visible = true;
			}
			else
			{
				checkEditTotalCPM.Checked = false;
				checkEditTotalCPM.Visible = false;
			}
			pnPackageBundleTotal.Visible = _localSchedule.ProductBundleSettings.ShowTotalImpressions | _localSchedule.ProductBundleSettings.ShowTotalInvestment;
			pnPackageBundleTotal.BringToFront();
		}

		private void SaveTotals()
		{
			if (_allowToSave)
			{
				_localSchedule.ProductBundleSettings.ShowMonthlyImpressions = Controller.Instance.WebBundleMonthlyImpressions.Checked;
				_localSchedule.ProductBundleSettings.ShowMonthlyInvestment = Controller.Instance.WebBundleMonthlyInvestment.Checked;
				_localSchedule.ProductBundleSettings.ShowTotalImpressions = Controller.Instance.WebBundleTotalImpressions.Checked;
				_localSchedule.ProductBundleSettings.ShowTotalInvestment = Controller.Instance.WebBundleTotalInvestment.Checked;
				SettingsNotSaved = true;
				UpdateTotals();
			}
		}

		private void UpdateGridColumns()
		{
			Controller.Instance.WebBundlePowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductBundleSettings.ShowWebsites |
			                                                                                          _localSchedule.ProductBundleSettings.ShowDimensions |
			                                                                                          _localSchedule.ProductBundleSettings.ShowActiveDays |
			                                                                                          _localSchedule.ProductBundleSettings.ShowAdRate |
			                                                                                          _localSchedule.ProductBundleSettings.ShowTotalAds);
			Controller.Instance.WebBundleEmail.Enabled = Controller.Instance.WebBundlePowerPoint.Enabled;
		}

		private void SaveGridColumns()
		{
			if (_allowToSave)
			{
				SettingsNotSaved = true;
				UpdateGridColumns();
			}
		}

		private void SavePreviewOption()
		{
			if (_allowToSave)
			{
				_localSchedule.ProductBundleSettings.ShowActiveDays = Controller.Instance.WebBundleActiveDays.Checked;
				_localSchedule.ProductBundleSettings.ShowAdRate = Controller.Instance.WebBundleAdRate.Checked;
				_localSchedule.ProductBundleSettings.ShowDimensions = Controller.Instance.WebBundleDimensions.Checked;
				_localSchedule.ProductBundleSettings.ShowTotalAds = Controller.Instance.WebBundleTotalAds.Checked;
				_localSchedule.ProductBundleSettings.ShowWebsites = Controller.Instance.WebBundleWebsites.Checked;
				Controller.Instance.WebBundlePowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductBundleSettings.ShowWebsites |
				                                                                                          _localSchedule.ProductBundleSettings.ShowDimensions |
				                                                                                          _localSchedule.ProductBundleSettings.ShowActiveDays |
				                                                                                          _localSchedule.ProductBundleSettings.ShowAdRate |
				                                                                                          _localSchedule.ProductBundleSettings.ShowTotalAds);
				Controller.Instance.WebBundleEmail.Enabled = Controller.Instance.WebBundlePowerPoint.Enabled;
				SettingsNotSaved = true;
			}
		}

		private void PrepareOutput()
		{
			_localSchedule.ProductBundleSettings.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : (comboBoxEditSlideHeader.Properties.Items.Count > 0 ? comboBoxEditSlideHeader.Properties.Items[0].ToString() : string.Empty);
			var temp = new List<string>();
			temp.Clear();
			if (_localSchedule.ProductBundleSettings.ShowMonthlyImpressions)
				temp.Add("Monthly Impressions");
			if (_localSchedule.ProductBundleSettings.ShowTotalImpressions)
				temp.Add("Total Impressions");
			if (_localSchedule.ProductBundleSettings.ShowMonthlyInvestment)
				temp.Add("Monthly Investment");
			if (_localSchedule.ProductBundleSettings.ShowTotalInvestment)
				temp.Add("Total Investment");

			for (int i = 0; i < 4; i++)
			{
				switch (i)
				{
					case 0:
						if (temp.Count > 0)
							_localSchedule.ProductBundleSettings.TotalHeader1 = temp[0];
						else
							_localSchedule.ProductBundleSettings.TotalHeader1 = string.Empty;
						break;
					case 1:
						if (temp.Count > 1)
							_localSchedule.ProductBundleSettings.TotalHeader2 = temp[1];
						else
							_localSchedule.ProductBundleSettings.TotalHeader2 = string.Empty;
						break;
					case 2:
						if (temp.Count > 2)
							_localSchedule.ProductBundleSettings.TotalHeader3 = temp[2];
						else
							_localSchedule.ProductBundleSettings.TotalHeader3 = string.Empty;
						break;
					case 3:
						if (temp.Count > 3)
							_localSchedule.ProductBundleSettings.TotalHeader4 = temp[3];
						else
							_localSchedule.ProductBundleSettings.TotalHeader4 = string.Empty;
						break;
				}
			}
			temp.Clear();
			if (_localSchedule.ProductBundleSettings.ShowMonthlyImpressions)
				temp.Add(spinEditMonthlyImpressions.Value.ToString("#,##0"));
			if (_localSchedule.ProductBundleSettings.ShowMonthlyInvestment)
				temp.Add(spinEditMonthlyInvestment.Value.ToString("$#,###.00"));
			if (_localSchedule.ProductBundleSettings.ShowTotalImpressions)
				temp.Add(spinEditTotalImpressions.Value.ToString("#,##0"));
			if (_localSchedule.ProductBundleSettings.ShowTotalInvestment)
				temp.Add(spinEditTotalInvestment.Value.ToString("$#,###.00"));

			for (int i = 0; i < 4; i++)
			{
				switch (i)
				{
					case 0:
						if (temp.Count > 0)
							_localSchedule.ProductBundleSettings.TotalValue1 = temp[0];
						else
							_localSchedule.ProductBundleSettings.TotalValue1 = string.Empty;
						break;
					case 1:
						if (temp.Count > 1)
							_localSchedule.ProductBundleSettings.TotalValue2 = temp[1];
						else
							_localSchedule.ProductBundleSettings.TotalValue2 = string.Empty;
						break;
					case 2:
						if (temp.Count > 2)
							_localSchedule.ProductBundleSettings.TotalValue3 = temp[2];
						else
							_localSchedule.ProductBundleSettings.TotalValue3 = string.Empty;
						break;
					case 3:
						if (temp.Count > 3)
							_localSchedule.ProductBundleSettings.TotalValue4 = temp[3];
						else
							_localSchedule.ProductBundleSettings.TotalValue4 = string.Empty;
						break;
				}
			}

			temp.Clear();
			if (_localSchedule.ProductBundleSettings.ShowMonthlyImpressions && _localSchedule.ProductBundleSettings.ShowMonthlyInvestment)
			{
				temp.Add(checkEditMonthlyCPM.Checked ? checkEditMonthlyCPM.Text : string.Empty);
				temp.Add(string.Empty);
			}
			else if (_localSchedule.ProductBundleSettings.ShowMonthlyImpressions || _localSchedule.ProductBundleSettings.ShowMonthlyInvestment)
				temp.Add(string.Empty);
			if (_localSchedule.ProductBundleSettings.ShowTotalImpressions && _localSchedule.ProductBundleSettings.ShowTotalInvestment)
			{
				temp.Add(checkEditTotalCPM.Checked ? checkEditTotalCPM.Text : string.Empty);
				temp.Add(string.Empty);
			}
			else if (_localSchedule.ProductBundleSettings.ShowTotalImpressions || _localSchedule.ProductBundleSettings.ShowTotalInvestment)
				temp.Add(string.Empty);

			for (int i = 0; i < 4; i++)
			{
				switch (i)
				{
					case 0:
						if (temp.Count > 0)
							_localSchedule.ProductBundleSettings.TotalCPM1 = temp[0];
						else
							_localSchedule.ProductBundleSettings.TotalCPM1 = string.Empty;
						break;
					case 1:
						if (temp.Count > 1)
							_localSchedule.ProductBundleSettings.TotalCPM2 = temp[1];
						else
							_localSchedule.ProductBundleSettings.TotalCPM2 = string.Empty;
						break;
					case 2:
						if (temp.Count > 2)
							_localSchedule.ProductBundleSettings.TotalCPM3 = temp[2];
						else
							_localSchedule.ProductBundleSettings.TotalCPM3 = string.Empty;
						break;
					case 3:
						if (temp.Count > 3)
							_localSchedule.ProductBundleSettings.TotalCPM4 = temp[3];
						else
							_localSchedule.ProductBundleSettings.TotalCPM4 = string.Empty;
						break;
				}
			}
		}

		private void bandedGridViewProductBundle_CalcPreviewText(object sender, CalcPreviewTextEventArgs e)
		{
			var previewText = new List<string>();
			if (_localSchedule.ProductBundleSettings.ShowDimensions && !string.IsNullOrEmpty(_localSchedule.Products[e.RowHandle].Dimensions))
				previewText.Add("Ad Dimensions: " + _localSchedule.Products[e.RowHandle].Dimensions);
			if (_localSchedule.ProductBundleSettings.ShowWebsites && !string.IsNullOrEmpty(_localSchedule.Products[e.RowHandle].AllWebsites))
				previewText.Add("Websites: " + _localSchedule.Products[e.RowHandle].AllWebsites);
			if (_localSchedule.ProductBundleSettings.ShowTotalAds && _localSchedule.Products[e.RowHandle].TotalAds.HasValue)
				previewText.Add("Total Ads: " + _localSchedule.Products[e.RowHandle].TotalAds.Value.ToString("#,##0"));
			if (_localSchedule.ProductBundleSettings.ShowActiveDays && _localSchedule.Products[e.RowHandle].ActiveDays.HasValue)
				previewText.Add("Active Days: " + _localSchedule.Products[e.RowHandle].ActiveDays.Value.ToString("#,##0"));
			if (_localSchedule.ProductBundleSettings.ShowAdRate && _localSchedule.Products[e.RowHandle].AdRate.HasValue)
				previewText.Add("Ad Rate: " + _localSchedule.Products[e.RowHandle].AdRate.Value.ToString("$#,###.00"));
			e.PreviewText = string.Join(";  ", previewText.ToArray());
			if (string.IsNullOrEmpty(e.PreviewText))
				e.PreviewText = "            ";
		}

		public void PreviewOptions_CheckedChanged(object sender, EventArgs e)
		{
			SavePreviewOption();
			bandedGridViewProductBundle.RefreshData();
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
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("bundle");
		}

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			PrepareOutput();
			using (var formGridType = new FormGridType())
			{
				DialogResult result = formGridType.ShowDialog();
				if (result != DialogResult.Cancel)
				{
					using (var formProgress = new FormProgress())
					{
						formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
						formProgress.TopMost = true;
						formProgress.Show();
						OnlineSchedulePowerPointHelper.Instance.AppendProductBundle(_localSchedule, result == DialogResult.No);
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
			PrepareOutput();
			using (var formGridType = new FormGridType())
			{
				DialogResult result = formGridType.ShowDialog();
				if (result != DialogResult.Cancel)
				{
					using (var formProgress = new FormProgress())
					{
						formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
						formProgress.TopMost = true;
						formProgress.Show();
						string tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
						OnlineSchedulePowerPointHelper.Instance.PrepareBundleEmail(tempFileName, _localSchedule, result == DialogResult.No);
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

		private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				var investment = (double)spinEditMonthlyInvestment.Value;
				var impressions = (double)spinEditMonthlyImpressions.Value;
				checkEditMonthlyCPM.Text = "CPM: " + (impressions != 0 ? ((investment / impressions) * 1000) : 0).ToString("$#,###.00");

				_localSchedule.ProductBundleSettings.TotalMonthlyImpressions = spinEditMonthlyImpressions.EditValue != null ? (double?)spinEditMonthlyImpressions.Value : null;
				_localSchedule.ProductBundleSettings.TotalMonthlyInvestments = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
				SettingsNotSaved = true;
			}
		}

		private void spinEditTotalImpressions_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				var investment = (double)spinEditTotalInvestment.Value;
				var impressions = (double)spinEditTotalImpressions.Value;
				checkEditTotalCPM.Text = "CPM: " + (impressions != 0 ? ((investment / impressions) * 1000) : 0).ToString("$#,###.00");

				_localSchedule.ProductBundleSettings.TotalImpressions = spinEditTotalImpressions.EditValue != null ? (double?)spinEditTotalImpressions.Value : null;
				_localSchedule.ProductBundleSettings.TotalInvestments = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
				SettingsNotSaved = true;
			}
		}

		private void checkEditShowTotalsLastSlide_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				_localSchedule.ProductBundleSettings.ShowTotalsOnLastOnly = (sender as CheckEdit).Checked;
				SettingsNotSaved = true;
			}
		}

		private void comboBoxEditSlideHeader_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				_localSchedule.ProductBundleSettings.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : (comboBoxEditSlideHeader.Properties.Items.Count > 0 ? comboBoxEditSlideHeader.Properties.Items[0].ToString() : string.Empty);
				SettingsNotSaved = true;
			}
		}

		private void checkEditCPM_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				_localSchedule.ProductBundleSettings.ShowMonthlyCPM = checkEditMonthlyCPM.Checked;
				_localSchedule.ProductBundleSettings.ShowTotalCPM = checkEditTotalCPM.Checked;
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