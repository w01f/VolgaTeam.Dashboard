using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms;
using NewBizWiz.OnlineSchedule.Controls.Properties;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
	public partial class WebPackageControl : UserControl
	{
		protected Form _formContainer;

		protected WebPackageControl()
		{
			InitializeComponent();
		}

		protected WebPackageControl(Form formContainer, bool placeFormulaBottom = false)
		{
			InitializeComponent();
			_formContainer = formContainer;
			Dock = DockStyle.Fill;
			AllowApplyValues = false;

			if (placeFormulaBottom)
			{
				pbFormualHelp.Visible = true;
				checkEditFormulaCPM.Left = labelControlFormula.Right + 50;
				checkEditFormulaCPM.Top = pbFormula.Top;
				checkEditFormulaCPM.Anchor = AnchorStyles.Top | AnchorStyles.Left;
				checkEditFormulaInvestment.Left = checkEditFormulaCPM.Right + 50;
				checkEditFormulaInvestment.Top = pbFormula.Top;
				checkEditFormulaInvestment.Anchor = AnchorStyles.Top | AnchorStyles.Left;
				checkEditFormulaImpressions.Left = checkEditFormulaInvestment.Right + 50;
				checkEditFormulaImpressions.Top = pbFormula.Top;
				checkEditFormulaImpressions.Anchor = AnchorStyles.Top | AnchorStyles.Left;
				pbFormualHelp.Visible = true;
				pbFormualHelp.Left = pnFormula.Right - pbFormualHelp.Width - 10;
				pbFormualHelp.Top = pbFormula.Top;
				pbFormualHelp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
				pnFormula.Parent = null;
				Controls.Add(pnFormula);
				pnFormula.Dock = DockStyle.Bottom;
				pnFormula.Height = pbFormula.Height + 20;
				pnFormula.SendToBack();
			}
			retractableBar.StateChanged += retractableBar_StateChanged;

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
				checkEditFormulaCPM.Font = font;
				checkEditFormulaImpressions.Font = font;
				checkEditFormulaInvestment.Font = font;
				labelControlFormula.Font = new Font(labelControlFormula.Font.FontFamily, labelControlFormula.Font.Size - 2, labelControlFormula.Font.Style);
				advBandedGridView.Appearance.BandPanel.Font = font;
				advBandedGridView.Appearance.EvenRow.Font = font;
				advBandedGridView.Appearance.FocusedCell.Font = font;
				advBandedGridView.Appearance.FocusedRow.Font = font;
				advBandedGridView.Appearance.HeaderPanel.Font = new Font(font.FontFamily, font.Size, FontStyle.Bold);
				advBandedGridView.Appearance.OddRow.Font = font;
				advBandedGridView.Appearance.Row.Font = font;
				advBandedGridView.Appearance.SelectedRow.Font = font;

				var buttonFont = new Font(buttonXCPM.Font.FontFamily, buttonXCPM.Font.Size - 2, buttonXCPM.Font.Style);
				buttonXCategory.Font = buttonFont;
				buttonXComments.Font = buttonFont;
				buttonXGroup.Font = buttonFont;
				buttonXImpressions.Font = buttonFont;
				buttonXInfo.Font = buttonFont;
				buttonXInvestment.Font = buttonFont;
				buttonXProduct.Font = buttonFont;
				buttonXRate.Font = buttonFont;
				buttonXScreenshot.Font = buttonFont;
			}
		}

		public bool SettingsNotSaved { get; set; }
		public bool AllowApplyValues { get; set; }
		public virtual Theme SelectedTheme { get; private set; }
		public virtual HelpManager HelpManager { get; private set; }
		public List<Dictionary<string, string>> OutputReplacementsLists { get; set; }

		public virtual ISchedule Schedule { get; private set; }
		public virtual DigitalPackageSettings Settings { get; private set; }
		public virtual IEnumerable<ProductPackageRecord> PackageRecords { get; private set; }
		public virtual ButtonItem Preview { get; private set; }
		public virtual ButtonItem PowerPoint { get; private set; }
		public virtual ButtonItem Email { get; private set; }
		public virtual ButtonItem Theme { get; private set; }


		public GridControl GridControl
		{
			get { return gridControl; }
		}

		public GridView GridView
		{
			get { return advBandedGridView; }
		}

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

		public bool SlidesAvailable
		{
			get { return Directory.Exists(BusinessWrapper.Instance.OutputManager.DigitalPackageTemplatesFolderPath); }
		}

		public virtual void LoadSchedule(bool quickLoad)
		{
			AllowApplyValues = false;
			if (!quickLoad)
			{
				repositoryItemComboBoxCategory.Items.Clear();
				repositoryItemComboBoxCategory.Items.AddRange(Core.OnlineSchedule.ListManager.Instance.ProductSources.Where(ps => ps.Category != null).Select(ps => ps.Category.Name).Distinct().ToArray());
				laAdvertiser.Text = Schedule.BusinessName + (!string.IsNullOrEmpty(Schedule.AccountNumber) ? (" - " + Schedule.AccountNumber) : string.Empty);
				LoadSettings();
				UpdateControls();
			}
			gridControl.DataSource = PackageRecords;
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
			Settings.ShowGroup &= PackageRecords.Any(r => !String.IsNullOrEmpty(r.SubCategory));

			if (Settings.ShowOptions)
				retractableBar.Expand(true);
			else
				retractableBar.Collapse(true);
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
			switch (Settings.Formula)
			{
				case FormulaType.CPM:
					checkEditFormulaCPM.Checked = true;
					checkEditFormulaInvestment.Checked = false;
					checkEditFormulaImpressions.Checked = false;
					break;
				case FormulaType.Investment:
					checkEditFormulaCPM.Checked = false;
					checkEditFormulaInvestment.Checked = true;
					checkEditFormulaImpressions.Checked = false;
					break;
				case FormulaType.Impressions:
					checkEditFormulaCPM.Checked = false;
					checkEditFormulaInvestment.Checked = false;
					checkEditFormulaImpressions.Checked = true;
					break;
			}
		}

		private void SaveSettings()
		{
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
			if (checkEditFormulaCPM.Checked)
				Settings.Formula = FormulaType.CPM;
			else if (checkEditFormulaInvestment.Checked)
				Settings.Formula = FormulaType.Investment;
			else if (checkEditFormulaImpressions.Checked)
				Settings.Formula = FormulaType.Impressions;
		}

		private void UpdateControls()
		{
			advBandedGridView.PostEditor();
			pnFormula.Enabled = Settings.ShowInvestment && Settings.ShowImpressions && Settings.ShowCPM;
			pbFormula.Image = pnFormula.Enabled ? Resources.FormulaLogo : Resources.FormulaLogoDisabled;
			UpdateGridColumns();
			UpdateOutputState();
			advBandedGridView.RefreshData();
		}

		private void UpdateGridColumns()
		{
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
			gridBandFormula.Visible = Settings.ShowInvestment && Settings.ShowImpressions && Settings.ShowCPM;
		}

		protected void UpdateOutputState()
		{
			var enableOutput = PackageRecords.Any() &&
				(Settings.ShowCategory || Settings.ShowGroup || Settings.ShowProduct || Settings.ShowComments || Settings.ShowInfo || Settings.ShowInvestment || Settings.ShowImpressions || Settings.ShowCPM || Settings.ShowRate);
			PowerPoint.Enabled = enableOutput;
			Email.Enabled = enableOutput;
			Preview.Enabled = enableOutput;
			Theme.Enabled = enableOutput;
			gridControl.Visible = enableOutput;
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
						else if (recordsCount > 1)
						{
							slideRows.Add(String.Format("Category{0}  |  Group{0}  |  Product{0}", j + 1), "DeleteRow");
							slideRows.Add(String.Format("ScheduleProductInfo{0}{1}{1}NotesandComments{0}", j + 1, (char)13), category.Any() ? String.Join("  |  ", category.ToArray()) : "DeleteRow");
						}
						else
						{
							slideRows.Add(String.Format("Category{0}  |  Group{0}  |  Product{0}", j + 1), category.Any() ? String.Join("  |  ", category.ToArray()) : "DeleteRow");
							slideRows.Add(String.Format("ScheduleProductInfo{0}{1}{1}NotesandComments{0}", j + 1, (char)13), "DeleteRow");
						}

						if (recordsCount > 1)
						{
							var investments = new List<string>();
							if (Settings.ShowImpressions && packageRecord.ImpressionsCalculated.HasValue)
								investments.Add(String.Format("Impressions: {0}", packageRecord.ImpressionsCalculated.Value.ToString("#,##0")));
							if (Settings.ShowCPM && packageRecord.CPMCalculated.HasValue)
								investments.Add(String.Format("CPM: {0}", packageRecord.CPMCalculated.Value.ToString("$#,###.00")));
							if (Settings.ShowRate && packageRecord.Rate.HasValue)
								investments.Add(String.Format("Rate: {0}", packageRecord.Rate.Value.ToString("$#,###.00")));
							if (Settings.ShowInvestment && packageRecord.InvestmentCalculated.HasValue)
								investments.Add(String.Format("Investment: {0}", packageRecord.InvestmentCalculated.Value.ToString("$#,###.00")));

							slideRows.Add(String.Format("Impressions{0},   CPM{0},   RATE{0},   Investment{0}", j + 1), investments.Any() ? String.Join(",   ", investments.ToArray()) : "DeleteColumn");
						}
						else
						{
							var impressions = new List<string>();
							if (Settings.ShowImpressions && packageRecord.ImpressionsCalculated.HasValue)
								impressions.Add(String.Format("Impressions: {0}", packageRecord.ImpressionsCalculated.Value.ToString("#,##0")));
							if (Settings.ShowCPM && packageRecord.CPMCalculated.HasValue)
								impressions.Add(String.Format("CPM: {0}", packageRecord.CPMCalculated.Value.ToString("$#,###.00")));
							if (Settings.ShowRate && packageRecord.Rate.HasValue)
								impressions.Add(String.Format("Rate: {0}", packageRecord.Rate.Value.ToString("$#,###.00")));

							var investments = new List<string>();
							if (Settings.ShowInvestment && packageRecord.InvestmentCalculated.HasValue)
								investments.Add(String.Format("Investment: {0}", packageRecord.InvestmentCalculated.Value.ToString("$#,###.00")));

							if (investments.Any())
							{
								slideRows.Add(String.Format("Impressions{0},   CPM{0},   RATE{0}", j + 1), impressions.Any() ? String.Join(",   ", impressions.ToArray()) : "DeleteRow");
								slideRows.Add(String.Format("Investment{0}", j + 1), String.Join(",   ", investments.ToArray()));
							}
							else
							{
								slideRows.Add(String.Format("Impressions{0},   CPM{0},   RATE{0}", j + 1), "DeleteRow");
								slideRows.Add(String.Format("Investment{0}", j + 1), impressions.Any() ? String.Join(",   ", impressions.ToArray()) : "DeleteRow");
							}
						}
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
			if (!AllowApplyValues) return;
			SaveSettings();
			UpdateControls();
			SettingsNotSaved = true;
		}

		private void retractableBar_StateChanged(object sender, CommonGUI.RetractableBar.StateChangedEventArgs e)
		{
			if (!AllowApplyValues) return;
			Settings.ShowOptions = e.Expaned;
			SettingsNotSaved = true;
		}

		private void advBandedGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			if (AllowApplyValues)
			{
				SettingsNotSaved = true;
				if (e.Column == bandedGridColumnCategory)
				{
					advBandedGridView.PostEditor();
					advBandedGridView.CloseEditor();
					AllowApplyValues = false;
					advBandedGridView.SetRowCellValue(e.RowHandle, bandedGridColumnGroup, String.Empty);
					advBandedGridView.SetRowCellValue(e.RowHandle, bandedGridColumnProduct, String.Empty);
					AllowApplyValues = true;
				}
				else if (e.Column == bandedGridColumnGroup)
				{
					advBandedGridView.PostEditor();
					advBandedGridView.CloseEditor();
					AllowApplyValues = false;
					advBandedGridView.SetRowCellValue(e.RowHandle, bandedGridColumnProduct, String.Empty);
					AllowApplyValues = true;
				}
			}
		}

		private void hyperLinkEditReset_OpenLink(object sender, OpenLinkEventArgs e)
		{
			advBandedGridView.PostEditor();
			if (Utilities.Instance.ShowWarningQuestion("Do you want to reset your Categories and Products to the original selections on the HOME Tab") == DialogResult.Yes)
			{
				AllowApplyValues = false;

				Settings.ResetToDefault();
				LoadSettings();
				UpdateControls();

				foreach (var packageRecord in PackageRecords)
					packageRecord.ResetToDefault();
				advBandedGridView.RefreshData();

				SettingsNotSaved = true;

				AllowApplyValues = true;
			}
			e.Handled = true;
		}

		private void advBandedGridView_ShowingEditor(object sender, CancelEventArgs e)
		{
			var focussedRecord = advBandedGridView.GetFocusedRow() as ProductPackageRecord;
			e.Cancel = focussedRecord != null && focussedRecord.UseFormula && (Settings.ShowInvestment && Settings.ShowImpressions && Settings.ShowCPM) &&
					   ((advBandedGridView.FocusedColumn == bandedGridColumnInvestment && Settings.Formula == FormulaType.Investment) ||
					   (advBandedGridView.FocusedColumn == bandedGridColumnImpressions && Settings.Formula == FormulaType.Impressions) ||
					   (advBandedGridView.FocusedColumn == bandedGridColumnCPM && Settings.Formula == FormulaType.CPM));
			if (e.Cancel || focussedRecord == null) return;
			if (advBandedGridView.FocusedColumn == bandedGridColumnProduct)
			{
				var category = Settings.ShowCategory ? focussedRecord.Category : null;
				var subCategory = Settings.ShowGroup ? focussedRecord.SubCategory : null;
				repositoryItemComboBoxProduct.Items.Clear();
				repositoryItemComboBoxProduct.Items.AddRange(Core.OnlineSchedule.ListManager.Instance.ProductSources.Where(x => (x.Category.Name.Equals(category) || String.IsNullOrEmpty(category)) && (x.SubCategory.Equals(subCategory) || String.IsNullOrEmpty(subCategory))).Select(x => x.Name).Distinct().ToArray());
			}
			else if (advBandedGridView.FocusedColumn == bandedGridColumnGroup)
			{
				var category = Settings.ShowCategory ? focussedRecord.Category : null;
				var subCategories = Core.OnlineSchedule.ListManager.Instance.ProductSources.Where(x => (x.Category.Name.Equals(category) || String.IsNullOrEmpty(category)) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray();
				repositoryItemComboBoxGroup.Items.Clear();
				repositoryItemComboBoxGroup.Items.AddRange(subCategories);
			}
		}

		private void repositoryItem_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (e.Button.Kind == ButtonPredefines.Delete)
			{
				advBandedGridView.PostEditor();
				advBandedGridView.CloseEditor();
				advBandedGridView.SetRowCellValue(advBandedGridView.FocusedRowHandle, advBandedGridView.FocusedColumn, String.Empty);
			}
		}

		private void repositoryItemComboBox_Closed(object sender, ClosedEventArgs e)
		{
			advBandedGridView.CloseEditor();
		}

		private void repositoryItemCheckEditFormula_CheckedChanged(object sender, EventArgs e)
		{
			advBandedGridView.PostEditor();
			advBandedGridView.RefreshData();
		}

		private void toolTipController_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			if (e.SelectedControl != gridControl) return;
			var view = gridControl.GetViewAt(e.ControlMousePosition) as GridView;
			if (view == null) return;
			var hi = view.CalcHitInfo(e.ControlMousePosition);
			if (!hi.InRowCell) return;
			if (hi.Column != bandedGridColumnFormula) return;
			var record = view.GetRow(hi.RowHandle) as ProductPackageRecord;
			if (record == null) return;
			e.Info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), record.UseFormula ? "Disable Delivery Formula" : "Enable Delivery Formula");
			e.Info.ImmediateToolTip = true;
			e.Info.Interval = 0;
		}

		public void Save_Click(object sender, EventArgs e)
		{
			if (Schedule.IsNew)
			{
				SaveAs_Click(sender, e);
				return;
			}
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
						Schedule.IsNew = false;
						SaveSchedule(from.ScheduleName);
					}
					else
					{
						Utilities.Instance.ShowWarning("Schedule Name can't be empty");
					}
				}
			}
		}

		public virtual void Help_Click(object sender, EventArgs e)
		{
			HelpManager.OpenHelpLink("dgpkg");
		}

		private void pbDisabledOutput_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("pkgdisabled");
		}

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			PopulateReplacementsList();
			OutputSlides();
		}

		public virtual void OutputSlides()
		{
		}

		public void Email_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			PopulateReplacementsList();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show(); var tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				OnlineSchedulePowerPointHelper.Instance.PrepareWebPackageEmail(this, tempFileName);
				formProgress.Close();
				if (File.Exists(tempFileName))
					using (var formEmail = new FormEmail(OnlineSchedulePowerPointHelper.Instance, HelpManager))
					{
						formEmail.Text = "Email this Online Schedule";
						formEmail.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
						Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
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
					ShowPreview(tempFileName);
			}
		}

		public virtual void ShowPreview(string tempFileName)
		{
		}

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion
	}
}