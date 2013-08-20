using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	public abstract partial class WebPackageControl : UserControl
	{
		protected Form _formContainer;
		protected WebPackageControl(Form formContainer)
		{
			InitializeComponent();
			_formContainer = formContainer;
			Dock = DockStyle.Fill;
			AllowApplyValues = false;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
				laAdvertiser.Font = font;
			}
		}

		public bool SettingsNotSaved { get; set; }
		public bool AllowApplyValues { get; set; }
		public List<Dictionary<string, string>> OutputReplacementsLists { get; set; }

		public abstract ISchedule Schedule { get; }
		public abstract DigitalPackageSettings Settings { get; }
		public abstract IEnumerable<ProductPackageRecord> PackageRecords { get; }
		public abstract ButtonItem OptionsButtons { get; }

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

		public virtual void LoadSchedule(bool quickLoad)
		{
			AllowApplyValues = false;
			if (!quickLoad)
			{
				laAdvertiser.Text = Schedule.BusinessName + (!string.IsNullOrEmpty(Schedule.AccountNumber) ? (" - " + Schedule.AccountNumber) : string.Empty);
				LoadSettings();
				UpdateControls();
				gridControl.DataSource = PackageRecords;
			}
			AllowApplyValues = true;
			SettingsNotSaved = false;
		}


		protected virtual bool SaveSchedule(string scheduleName = "")
		{
			advBandedGridView.PostEditor();
			SettingsNotSaved = false;
			return true;
		}

		private void LoadSettings()
		{
			OptionsButtons.Checked = Settings.ShowOptions;
			buttonXCategory.Checked = Settings.ShowCategory;
			buttonXGroup.Checked = Settings.ShowGroup;
			buttonXProduct.Checked = Settings.ShowProduct;
			buttonXImpressions.Checked = Settings.ShowImpressions;
			buttonXCPM.Checked = Settings.ShowCPM;
			buttonXRate.Checked = Settings.ShowRate;
			buttonXInvestment.Checked = Settings.ShowInvestment;
			buttonXInfo.Checked = Settings.ShowInfo;
			buttonXComments.Checked = Settings.ShowComments;
			buttonXScreenshot.Checked = Settings.ShowScreenshot;
		}

		private void SaveSettings()
		{
			Settings.ShowOptions = OptionsButtons.Checked;
			Settings.ShowCategory = buttonXCategory.Checked;
			Settings.ShowGroup = buttonXGroup.Checked;
			Settings.ShowProduct = buttonXProduct.Checked;
			Settings.ShowImpressions = buttonXImpressions.Checked;
			Settings.ShowCPM = buttonXCPM.Checked;
			Settings.ShowRate = buttonXRate.Checked;
			Settings.ShowInvestment = buttonXInvestment.Checked;
			Settings.ShowInfo = buttonXInfo.Checked;
			Settings.ShowComments = buttonXComments.Checked;
			Settings.ShowScreenshot = buttonXScreenshot.Checked;
		}

		private void UpdateControls()
		{
			splitContainerControl.PanelVisibility = Settings.ShowOptions ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel2;
			if (Settings.ShowCategory || Settings.ShowGroup || Settings.ShowProduct)
			{
				if (Settings.ShowCategory && Settings.ShowGroup && Settings.ShowProduct)
				{
					bandedGridColumnCategory.Visible = true;
					bandedGridColumnCategory.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnCategory, 0, 0);
					bandedGridColumnGroup.Visible = true;
					bandedGridColumnGroup.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnGroup, 1, 0);
					bandedGridColumnProduct.Visible = true;
					bandedGridColumnProduct.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnProduct, 2, 0);
				}
				else if (Settings.ShowCategory && Settings.ShowGroup)
				{
					bandedGridColumnCategory.Visible = true;
					bandedGridColumnCategory.RowCount = 2;
					advBandedGridView.SetColumnPosition(bandedGridColumnCategory, 0, 0);
					bandedGridColumnGroup.Visible = true;
					bandedGridColumnGroup.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnGroup, 1, 0);
					bandedGridColumnProduct.Visible = false;
				}
				else if (Settings.ShowCategory && Settings.ShowProduct)
				{
					bandedGridColumnCategory.Visible = true;
					bandedGridColumnCategory.RowCount = 2;
					advBandedGridView.SetColumnPosition(bandedGridColumnCategory, 0, 0);
					bandedGridColumnGroup.Visible = false;
					bandedGridColumnProduct.Visible = true;
					bandedGridColumnProduct.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnProduct, 1, 0);
				}
				else if (Settings.ShowGroup && Settings.ShowProduct)
				{
					bandedGridColumnCategory.Visible = false;
					bandedGridColumnGroup.Visible = true;
					bandedGridColumnGroup.RowCount = 2;
					advBandedGridView.SetColumnPosition(bandedGridColumnGroup, 0, 0);
					bandedGridColumnProduct.Visible = true;
					bandedGridColumnProduct.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnProduct, 1, 0);
				}
				else if (Settings.ShowCategory)
				{
					bandedGridColumnCategory.Visible = true;
					bandedGridColumnCategory.RowCount = 3;
					advBandedGridView.SetColumnPosition(bandedGridColumnCategory, 0, 0);
					bandedGridColumnGroup.Visible = false;
					bandedGridColumnProduct.Visible = false;
				}
				else if (Settings.ShowGroup)
				{
					bandedGridColumnCategory.Visible = false;
					bandedGridColumnGroup.Visible = true;
					bandedGridColumnGroup.RowCount = 3;
					advBandedGridView.SetColumnPosition(bandedGridColumnGroup, 0, 0);
					bandedGridColumnProduct.Visible = false;
				}
				else if (Settings.ShowProduct)
				{
					bandedGridColumnCategory.Visible = false;
					bandedGridColumnGroup.Visible = false;
					bandedGridColumnProduct.Visible = true;
					bandedGridColumnProduct.RowCount = 3;
					advBandedGridView.SetColumnPosition(bandedGridColumnProduct, 0, 0);
				}
				gridBandProduct.Visible = true;
			}
			else
				gridBandProduct.Visible = false;
			gridBandInfo.Visible = Settings.ShowInfo;
			gridBandComments.Visible = Settings.ShowComments;
			if (Settings.ShowImpressions || Settings.ShowCPM || Settings.ShowRate)
			{
				if (Settings.ShowImpressions && Settings.ShowCPM && Settings.ShowRate)
				{
					bandedGridColumnImpressions.Visible = true;
					bandedGridColumnImpressions.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnImpressions, 0, 0);
					bandedGridColumnCPM.Visible = true;
					bandedGridColumnCPM.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnCPM, 1, 0);
					bandedGridColumnRate.Visible = true;
					bandedGridColumnRate.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnRate, 2, 0);
				}
				else if (Settings.ShowImpressions && Settings.ShowCPM)
				{
					bandedGridColumnImpressions.Visible = true;
					bandedGridColumnImpressions.RowCount = 2;
					advBandedGridView.SetColumnPosition(bandedGridColumnImpressions, 0, 0);
					bandedGridColumnCPM.Visible = true;
					bandedGridColumnCPM.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnCPM, 1, 0);
					bandedGridColumnRate.Visible = false;
				}
				else if (Settings.ShowImpressions && Settings.ShowRate)
				{
					bandedGridColumnImpressions.Visible = true;
					bandedGridColumnImpressions.RowCount = 2;
					advBandedGridView.SetColumnPosition(bandedGridColumnImpressions, 0, 0);
					bandedGridColumnCPM.Visible = false;
					bandedGridColumnRate.Visible = true;
					bandedGridColumnRate.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnRate, 1, 0);
				}
				else if (Settings.ShowCPM && Settings.ShowRate)
				{
					bandedGridColumnImpressions.Visible = false;
					bandedGridColumnCPM.Visible = true;
					bandedGridColumnCPM.RowCount = 2;
					advBandedGridView.SetColumnPosition(bandedGridColumnCPM, 0, 0);
					bandedGridColumnRate.Visible = true;
					bandedGridColumnRate.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnRate, 1, 0);
				}
				else if (Settings.ShowImpressions)
				{
					bandedGridColumnImpressions.Visible = true;
					bandedGridColumnImpressions.RowCount = 3;
					advBandedGridView.SetColumnPosition(bandedGridColumnImpressions, 0, 0);
					bandedGridColumnCPM.Visible = false;
					bandedGridColumnRate.Visible = false;
				}
				else if (Settings.ShowCPM)
				{
					bandedGridColumnImpressions.Visible = false;
					bandedGridColumnCPM.Visible = true;
					bandedGridColumnCPM.RowCount = 3;
					advBandedGridView.SetColumnPosition(bandedGridColumnCPM, 0, 0);
					bandedGridColumnRate.Visible = false;
				}
				else if (Settings.ShowRate)
				{
					bandedGridColumnImpressions.Visible = false;
					bandedGridColumnCPM.Visible = false;
					bandedGridColumnRate.Visible = true;
					bandedGridColumnRate.RowCount = 3;
					advBandedGridView.SetColumnPosition(bandedGridColumnRate, 0, 0);
				}
				gridBandRate.Visible = true;
			}
			else
				gridBandRate.Visible = false;
			gridBandInvestment.Visible = Settings.ShowInvestment;
		}

		public int RowsPerSlide
		{
			get
			{
				var recordsCount = PackageRecords.Count();
				switch (recordsCount)
				{
					case 6:
						return 3;
					default:
						return recordsCount < 5 ? recordsCount : 4;
				}
			}
		}

		public void PopulateReplacementsList()
		{
			var recordsCount = PackageRecords.Count();
			var rowsPerSlide = RowsPerSlide;
			OutputReplacementsLists = new List<Dictionary<string, string>>();
			for (var i = 0; i < recordsCount; i += rowsPerSlide)
			{
				var slideRows = new Dictionary<string, string>();
				slideRows.Add("Digital Media Campaign: AdvertiserNameHere", String.Format("Digital Media Campaign: {0}", Schedule.BusinessName));
				for (var j = 0; j < rowsPerSlide; j++)
				{
					if ((i + j) < recordsCount)
					{
						var packageRecord = PackageRecords.ElementAt(i + j);
						var category = new List<string>();
						if (Settings.ShowCategory && !String.IsNullOrEmpty(packageRecord.Category))
							category.Add(packageRecord.Category);
						if (Settings.ShowGroup && !String.IsNullOrEmpty(packageRecord.SubCategory))
							category.Add(packageRecord.SubCategory);
						if (Settings.ShowProduct && !String.IsNullOrEmpty(packageRecord.Name))
							category.Add(packageRecord.Name);

						var info = new List<string>();
						if (Settings.ShowInfo && !String.IsNullOrEmpty(packageRecord.Info))
							info.Add(packageRecord.Info);
						if (Settings.ShowComments && !String.IsNullOrEmpty(packageRecord.Comments))
							info.Add(packageRecord.Comments);

						if (info.Any())
						{
							slideRows.Add(String.Format("Category{0}  |  Group{0}  |  Product{0}", j + 1), category.Any() ? String.Join("  |  ", category.ToArray()) : "DeleteRow");
							slideRows.Add(String.Format("ScheduleProductInfo{0}{1}{1}NotesandComments{0}", j + 1, (char)13), String.Join(String.Format("{0}{0}", (char)13), info.ToArray()));
						}
						else
						{
							slideRows.Add(String.Format("Category{0}  |  Group{0}  |  Product{0}", j + 1), "DeleteRow");
							slideRows.Add(String.Format("ScheduleProductInfo{0}{1}{1}NotesandComments{0}", j + 1, (char)13), category.Any() ? String.Join("  |  ", category.ToArray()) : "DeleteRow");
						}

						var investments = new List<string>();
						if (Settings.ShowImpressions && packageRecord.Impressions.HasValue)
							investments.Add(String.Format("Impressions: {0}", packageRecord.Impressions.Value.ToString("#,##0")));
						if (Settings.ShowCPM && packageRecord.CPM.HasValue)
							investments.Add(String.Format("CPM: {0}", packageRecord.CPM.Value.ToString("$#,###.00")));
						if (Settings.ShowRate && packageRecord.Rate.HasValue)
							investments.Add(String.Format("Rate: {0}", packageRecord.Rate.Value.ToString("$#,###.00")));
						if (Settings.ShowInvestment && packageRecord.Investment.HasValue)
							investments.Add(String.Format("Investment: {0}", packageRecord.Investment.Value.ToString("$#,###.00")));
						if (Settings.ShowImpressions || Settings.ShowCPM || Settings.ShowRate || Settings.ShowInvestment)
							slideRows.Add(String.Format("Impressions{0},   CPM{0},   RATE{0},   Investment{0}", j + 1), String.Join(",   ", investments.ToArray()));
						else
							slideRows.Add(String.Format("Impressions{0},   CPM{0},   RATE{0},   Investment{0}", j + 1), "DeleteColumn");
					}
					else
					{
						slideRows.Add(String.Format("Category{0}  |  Group{0}  |  Product{0}", j + 1), "DeleteRow");
						slideRows.Add(String.Format("ScheduleProductInfo{0}{1}{1}NotesandComments{0}", j + 1, (char)13), "DeleteRow");
						slideRows.Add(String.Format("Impressions{0},   CPM{0},   RATE{0},   Investment{0}", j + 1), String.Empty);
					}
				}
				OutputReplacementsLists.Add(slideRows);
			}
		}

		public void TogledButton_CheckedChanged(object sender, EventArgs e)
		{
			if (AllowApplyValues)
			{
				SaveSettings();
				UpdateControls();
				SettingsNotSaved = true;
			}
		}

		private void advBandedGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			if (AllowApplyValues)
				SettingsNotSaved = true;
		}

		private void hyperLinkEditReset_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Do you want to reset your Categories and Products to the original selections on the HOME Tab") == DialogResult.Yes)
				foreach (var packageRecord in PackageRecords)
					packageRecord.ResetToDefault();
			e.Handled = true;
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

		public abstract void Help_Click(object sender, EventArgs e);

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			PopulateReplacementsList();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				formProgress.Show();
				OnlineSchedulePowerPointHelper.Instance.AppendWebPackage(this);
				formProgress.Close();
			}
			using (var formOutput = new FormSlideOutput())
			{
				if (formOutput.ShowDialog() != DialogResult.OK)
					Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
			}
		}

		public void Email_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			PopulateReplacementsList();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				var tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				OnlineSchedulePowerPointHelper.Instance.PrepareWebPackageEmail(this, tempFileName);
				Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
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
						RegistryHelper.MainFormHandle = _formContainer.Handle;
					}
			}
		}

		public void Preview_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			PopulateReplacementsList();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				var tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				OnlineSchedulePowerPointHelper.Instance.PrepareWebPackageEmail(this, tempFileName);
				Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
				formProgress.Close();
				if (File.Exists(tempFileName))
					using (var formPreview = new FormPreview())
					{
						formPreview.Text = "Preview Digital Package";
						formPreview.PresentationFile = tempFileName;
						RegistryHelper.MainFormHandle = formPreview.Handle;
						RegistryHelper.MaximizeMainForm = false;
						DialogResult previewResult = formPreview.ShowDialog();
						RegistryHelper.MaximizeMainForm = _formContainer.WindowState == FormWindowState.Maximized;
						RegistryHelper.MainFormHandle = _formContainer.Handle;
						if (previewResult != DialogResult.OK)
							Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
						else
						{
							Utilities.Instance.ActivatePowerPoint(OnlineSchedulePowerPointHelper.Instance.PowerPointObject);
							Utilities.Instance.ActivateMiniBar();
						}
					}
			}
		}
	}
}