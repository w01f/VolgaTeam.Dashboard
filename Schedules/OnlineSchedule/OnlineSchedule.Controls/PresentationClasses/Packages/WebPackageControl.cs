using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Online.Configuration;
using Asa.Business.Online.Dictionaries;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Enums;
using Asa.Business.Online.Interfaces;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ContentEditors.Controls;
using Asa.Common.GUI.RetractableBar;
using Asa.Common.GUI.ToolForms;
using Asa.Online.Controls.InteropClasses;
using Asa.Online.Controls.Properties;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Asa.Online.Controls.PresentationClasses.Packages
{
	[ToolboxItem(false)]
	[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
	//public partial class WebPackageControl:UserControl
	public abstract partial class WebPackageControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo> : BasePartitionEditControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>, IWebPackageOutput
		where TPartitionContet : BaseSchedulePartitionContent<TSchedule, TScheduleSettings>, IDigitalProductsContent
		where TSchedule : IDigitalSchedule<TScheduleSettings>
		where TScheduleSettings : IDigitalScheduleSettings
		where TChangeInfo : DigitalScheduleChangeInfo
	{
		protected abstract IDigitalSchedule<IDigitalScheduleSettings> Schedule { get; }

		protected IDigitalScheduleSettings ScheduleSettings
		{
			get { return Schedule.Settings; }
		}

		protected bool AllowApplyValues { get; set; }

		protected IEnumerable<ProductPackageRecord> PackageRecords
		{
			get
			{
				return EditedContent.DigitalProducts
					.OrderBy(p => p.Index)
					.Select(p => p.PackageRecord)
					.ToList();
			}
		}
		public abstract Form MainForm { get; }

		protected WebPackageControl()
		{
			InitializeComponent();
		}

		#region BaseContentEditControl Override
		public override void InitControl()
		{
			base.InitControl();
			retractableBar.StateChanged += OnRetractableBarStateChanged;
			pbFormualHelp.Buttonize();
			pbDisabledOutput.Buttonize();

			repositoryItemComboBoxCategory.Items.Clear();
			repositoryItemComboBoxCategory.Items.AddRange(ListManager.Instance.ProductSources
				.Where(ps => ps.Category != null)
				.Select(ps => ps.Category.Name)
				.Distinct()
				.ToArray());

			if ((MainForm.CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
				laAdvertiser.Font = font;
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

		protected override void UpdateEditedContet()
		{
			AllowApplyValues = false;
			laAdvertiser.Text = ScheduleSettings.BusinessName;
			gridControl.DataSource = null;
			LoadSettings();
			UpdateControls();
			gridControl.DataSource = PackageRecords;
			AllowApplyValues = true;
		}

		protected override void ApplyChanges()
		{
			advBandedGridView.PostEditor();
		}
		#endregion

		#region Common Methods
		protected abstract void GetDisabledOutputInfo();

		private void LoadSettings()
		{
			PackageSettings.ShowGroup &= PackageRecords.Any(r => !String.IsNullOrEmpty(r.SubCategory));

			if (PackageSettings.ShowOptions)
				retractableBar.Expand(true);
			else
				retractableBar.Collapse(true);
			buttonXCategory.Checked = PackageSettings.ShowCategory;
			buttonXGroup.Checked = PackageSettings.ShowGroup;
			buttonXProduct.Checked = PackageSettings.ShowProduct;
			buttonXImpressions.Checked = PackageSettings.ShowImpressions;
			buttonXCPM.Checked = PackageSettings.ShowCPM;
			buttonXRate.Checked = PackageSettings.ShowRate;
			buttonXInvestment.Checked = PackageSettings.ShowInvestment;
			buttonXInfo.Checked = PackageSettings.ShowInfo;
			buttonXComments.Checked = PackageSettings.ShowComments;
			buttonXScreenshot.Checked = PackageSettings.ShowScreenshot;
			switch (PackageSettings.Formula)
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
			PackageSettings.ShowCategory = buttonXCategory.Checked;
			PackageSettings.ShowGroup = buttonXGroup.Checked;
			PackageSettings.ShowProduct = buttonXProduct.Checked;
			PackageSettings.ShowImpressions = buttonXImpressions.Checked;
			PackageSettings.ShowCPM = buttonXCPM.Checked;
			PackageSettings.ShowRate = buttonXRate.Checked;
			PackageSettings.ShowInvestment = buttonXInvestment.Checked;
			PackageSettings.ShowInfo = buttonXInfo.Checked;
			PackageSettings.ShowComments = buttonXComments.Checked;
			PackageSettings.ShowScreenshot = buttonXScreenshot.Checked;
			if (checkEditFormulaCPM.Checked)
				PackageSettings.Formula = FormulaType.CPM;
			else if (checkEditFormulaInvestment.Checked)
				PackageSettings.Formula = FormulaType.Investment;
			else if (checkEditFormulaImpressions.Checked)
				PackageSettings.Formula = FormulaType.Impressions;
		}

		private void UpdateControls()
		{
			advBandedGridView.PostEditor();
			pnFormula.Enabled = PackageSettings.ShowInvestment && PackageSettings.ShowImpressions && PackageSettings.ShowCPM;
			pbFormula.Image = pnFormula.Enabled ? Resources.FormulaLogo : Resources.FormulaLogoDisabled;
			UpdateGridColumns();
			UpdateOutputState();
			advBandedGridView.RefreshData();
		}

		private void UpdateGridColumns()
		{
			if (PackageSettings.ShowCategory || PackageSettings.ShowGroup || PackageSettings.ShowProduct)
			{
				if (PackageSettings.ShowCategory && PackageSettings.ShowGroup && PackageSettings.ShowProduct)
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
				else if (PackageSettings.ShowCategory && PackageSettings.ShowGroup)
				{
					bandedGridColumnCategory.Visible = true;
					bandedGridColumnCategory.RowCount = 2;
					advBandedGridView.SetColumnPosition(bandedGridColumnCategory, 0, 0);
					bandedGridColumnGroup.Visible = true;
					bandedGridColumnGroup.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnGroup, 1, 0);
					bandedGridColumnProduct.Visible = false;
				}
				else if (PackageSettings.ShowCategory && PackageSettings.ShowProduct)
				{
					bandedGridColumnCategory.Visible = true;
					bandedGridColumnCategory.RowCount = 2;
					advBandedGridView.SetColumnPosition(bandedGridColumnCategory, 0, 0);
					bandedGridColumnGroup.Visible = false;
					bandedGridColumnProduct.Visible = true;
					bandedGridColumnProduct.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnProduct, 1, 0);
				}
				else if (PackageSettings.ShowGroup && PackageSettings.ShowProduct)
				{
					bandedGridColumnCategory.Visible = false;
					bandedGridColumnGroup.Visible = true;
					bandedGridColumnGroup.RowCount = 2;
					advBandedGridView.SetColumnPosition(bandedGridColumnGroup, 0, 0);
					bandedGridColumnProduct.Visible = true;
					bandedGridColumnProduct.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnProduct, 1, 0);
				}
				else if (PackageSettings.ShowCategory)
				{
					bandedGridColumnCategory.Visible = true;
					bandedGridColumnCategory.RowCount = 3;
					advBandedGridView.SetColumnPosition(bandedGridColumnCategory, 0, 0);
					bandedGridColumnGroup.Visible = false;
					bandedGridColumnProduct.Visible = false;
				}
				else if (PackageSettings.ShowGroup)
				{
					bandedGridColumnCategory.Visible = false;
					bandedGridColumnGroup.Visible = true;
					bandedGridColumnGroup.RowCount = 3;
					advBandedGridView.SetColumnPosition(bandedGridColumnGroup, 0, 0);
					bandedGridColumnProduct.Visible = false;
				}
				else if (PackageSettings.ShowProduct)
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
			gridBandInfo.Visible = PackageSettings.ShowInfo;
			gridBandComments.Visible = PackageSettings.ShowComments;
			if (PackageSettings.ShowImpressions || PackageSettings.ShowCPM || PackageSettings.ShowRate)
			{
				if (PackageSettings.ShowImpressions && PackageSettings.ShowCPM && PackageSettings.ShowRate)
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
				else if (PackageSettings.ShowImpressions && PackageSettings.ShowCPM)
				{
					bandedGridColumnImpressions.Visible = true;
					bandedGridColumnImpressions.RowCount = 2;
					advBandedGridView.SetColumnPosition(bandedGridColumnImpressions, 0, 0);
					bandedGridColumnCPM.Visible = true;
					bandedGridColumnCPM.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnCPM, 1, 0);
					bandedGridColumnRate.Visible = false;
				}
				else if (PackageSettings.ShowImpressions && PackageSettings.ShowRate)
				{
					bandedGridColumnImpressions.Visible = true;
					bandedGridColumnImpressions.RowCount = 2;
					advBandedGridView.SetColumnPosition(bandedGridColumnImpressions, 0, 0);
					bandedGridColumnCPM.Visible = false;
					bandedGridColumnRate.Visible = true;
					bandedGridColumnRate.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnRate, 1, 0);
				}
				else if (PackageSettings.ShowCPM && PackageSettings.ShowRate)
				{
					bandedGridColumnImpressions.Visible = false;
					bandedGridColumnCPM.Visible = true;
					bandedGridColumnCPM.RowCount = 2;
					advBandedGridView.SetColumnPosition(bandedGridColumnCPM, 0, 0);
					bandedGridColumnRate.Visible = true;
					bandedGridColumnRate.RowCount = 1;
					advBandedGridView.SetColumnPosition(bandedGridColumnRate, 1, 0);
				}
				else if (PackageSettings.ShowImpressions)
				{
					bandedGridColumnImpressions.Visible = true;
					bandedGridColumnImpressions.RowCount = 3;
					advBandedGridView.SetColumnPosition(bandedGridColumnImpressions, 0, 0);
					bandedGridColumnCPM.Visible = false;
					bandedGridColumnRate.Visible = false;
				}
				else if (PackageSettings.ShowCPM)
				{
					bandedGridColumnImpressions.Visible = false;
					bandedGridColumnCPM.Visible = true;
					bandedGridColumnCPM.RowCount = 3;
					advBandedGridView.SetColumnPosition(bandedGridColumnCPM, 0, 0);
					bandedGridColumnRate.Visible = false;
				}
				else if (PackageSettings.ShowRate)
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
			gridBandInvestment.Visible = PackageSettings.ShowInvestment;
			gridBandFormula.Visible = PackageSettings.ShowInvestment && PackageSettings.ShowImpressions && PackageSettings.ShowCPM;
		}
		#endregion

		#region Control Event Handlers
		public void OnToggleCheckedChanged(object sender, EventArgs e)
		{
			if (!AllowApplyValues) return;
			SaveSettings();
			UpdateControls();
			SettingsNotSaved = true;
		}

		private void OnRetractableBarStateChanged(object sender, StateChangedEventArgs e)
		{
			if (!AllowApplyValues) return;
			PackageSettings.ShowOptions = e.Expaned;
			SettingsNotSaved = true;
		}

		private void OnGridViewCellValueChanged(object sender, CellValueChangedEventArgs e)
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

		private void OnResetClick(object sender, OpenLinkEventArgs e)
		{
			advBandedGridView.PostEditor();
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Do you want to reset your Categories and Products to the original selections on the HOME Tab") == DialogResult.Yes)
			{
				AllowApplyValues = false;

				PackageSettings.ResetToDefault();
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

		private void OnGridViewShowingEditor(object sender, CancelEventArgs e)
		{
			var focussedRecord = advBandedGridView.GetFocusedRow() as ProductPackageRecord;
			e.Cancel = focussedRecord != null && focussedRecord.UseFormula && (PackageSettings.ShowInvestment && PackageSettings.ShowImpressions && PackageSettings.ShowCPM) &&
					   ((advBandedGridView.FocusedColumn == bandedGridColumnInvestment && PackageSettings.Formula == FormulaType.Investment) ||
					   (advBandedGridView.FocusedColumn == bandedGridColumnImpressions && PackageSettings.Formula == FormulaType.Impressions) ||
					   (advBandedGridView.FocusedColumn == bandedGridColumnCPM && PackageSettings.Formula == FormulaType.CPM));
			if (e.Cancel || focussedRecord == null) return;
			if (advBandedGridView.FocusedColumn == bandedGridColumnProduct)
			{
				var category = PackageSettings.ShowCategory ? focussedRecord.Category : null;
				var subCategory = PackageSettings.ShowGroup ? focussedRecord.SubCategory : null;
				repositoryItemComboBoxProduct.Items.Clear();
				repositoryItemComboBoxProduct.Items.AddRange(ListManager.Instance.ProductSources.Where(x => (x.Category.Name.Equals(category) || String.IsNullOrEmpty(category)) && (x.SubCategory.Equals(subCategory) || String.IsNullOrEmpty(subCategory))).Select(x => x.Name).Distinct().ToArray());
			}
			else if (advBandedGridView.FocusedColumn == bandedGridColumnGroup)
			{
				var category = PackageSettings.ShowCategory ? focussedRecord.Category : null;
				var subCategories = ListManager.Instance.ProductSources.Where(x => (x.Category.Name.Equals(category) || String.IsNullOrEmpty(category)) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray();
				repositoryItemComboBoxGroup.Items.Clear();
				repositoryItemComboBoxGroup.Items.AddRange(subCategories);
			}
		}

		private void OnButtonsRepositoryItemButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (e.Button.Kind != ButtonPredefines.Delete) return;
			advBandedGridView.PostEditor();
			advBandedGridView.CloseEditor();
			advBandedGridView.SetRowCellValue(advBandedGridView.FocusedRowHandle, advBandedGridView.FocusedColumn, String.Empty);
		}

		private void OnRepositoryItemComboBoxClosed(object sender, ClosedEventArgs e)
		{
			advBandedGridView.CloseEditor();
		}

		private void OnRepositoryItemCheckEditFormulaCheckedChanged(object sender, EventArgs e)
		{
			advBandedGridView.PostEditor();
			advBandedGridView.RefreshData();
		}

		private void OnTooltipGetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
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

		private void OnDisabledOutputClick(object sender, EventArgs e)
		{
			GetDisabledOutputInfo();
		}
		#endregion

		#region Output Stuff
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

		public abstract Theme SelectedTheme { get; }
		public List<Dictionary<string, string>> OutputReplacementsLists { get; set; }

		public DigitalPackageSettings PackageSettings
		{
			get { return ScheduleSettings.DigitalPackageSettings; }
		}

		protected bool IsOutputEnabled
		{
			get
			{
				return PackageRecords.Any() &&
					(PackageSettings.ShowCategory ||
						PackageSettings.ShowGroup ||
						PackageSettings.ShowProduct ||
						PackageSettings.ShowComments ||
						PackageSettings.ShowInfo ||
						PackageSettings.ShowInvestment ||
						PackageSettings.ShowImpressions ||
						PackageSettings.ShowCPM ||
						PackageSettings.ShowRate);
			}
		}

		protected abstract void UpdateOutputState();

		public void PopulateReplacementsList()
		{
			var recordsCount = PackageRecords.Count();
			var rowsPerSlide = RowsPerSlide;
			OutputReplacementsLists = new List<Dictionary<string, string>>();
			for (var i = 0; i < recordsCount; i += rowsPerSlide)
			{
				var slideRows = new Dictionary<string, string>();
				slideRows.Add("Digital Media Campaign: AdvertiserNameHere", String.Format("Digital Media Campaign: {0}", ScheduleSettings.BusinessName));
				for (var j = 0; j < rowsPerSlide; j++)
				{
					if ((i + j) < recordsCount)
					{
						var packageRecord = PackageRecords.ElementAt(i + j);
						var category = new List<string>();
						if (PackageSettings.ShowCategory && !String.IsNullOrEmpty(packageRecord.Category))
							category.Add(packageRecord.Category);
						if (PackageSettings.ShowGroup && !String.IsNullOrEmpty(packageRecord.SubCategory))
							category.Add(packageRecord.SubCategory);
						if (PackageSettings.ShowProduct && !String.IsNullOrEmpty(packageRecord.Name))
							category.Add(packageRecord.Name);

						var info = new List<string>();
						if (PackageSettings.ShowInfo && !String.IsNullOrEmpty(packageRecord.Info))
							info.Add(packageRecord.Info);
						if (PackageSettings.ShowComments && !String.IsNullOrEmpty(packageRecord.Comments))
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
							if (PackageSettings.ShowImpressions && packageRecord.ImpressionsCalculated.HasValue)
								investments.Add(String.Format("Impressions: {0}", packageRecord.ImpressionsCalculated.Value.ToString("#,##0")));
							if (PackageSettings.ShowCPM && packageRecord.CPMCalculated.HasValue)
								investments.Add(String.Format("CPM: {0}", packageRecord.CPMCalculated.Value.ToString("$#,###.00")));
							if (PackageSettings.ShowRate && packageRecord.Rate.HasValue)
								investments.Add(String.Format("Rate: {0}", packageRecord.Rate.Value.ToString("$#,###.00")));
							if (PackageSettings.ShowInvestment && packageRecord.InvestmentCalculated.HasValue)
								investments.Add(String.Format("Investment: {0}", packageRecord.InvestmentCalculated.Value.ToString("$#,###.00")));

							slideRows.Add(String.Format("Impressions{0},   CPM{0},   RATE{0},   Investment{0}", j + 1), investments.Any() ? String.Join(",   ", investments.ToArray()) : "DeleteColumn");
						}
						else
						{
							var impressions = new List<string>();
							if (PackageSettings.ShowImpressions && packageRecord.ImpressionsCalculated.HasValue)
								impressions.Add(String.Format("Impressions: {0}", packageRecord.ImpressionsCalculated.Value.ToString("#,##0")));
							if (PackageSettings.ShowCPM && packageRecord.CPMCalculated.HasValue)
								impressions.Add(String.Format("CPM: {0}", packageRecord.CPMCalculated.Value.ToString("$#,###.00")));
							if (PackageSettings.ShowRate && packageRecord.Rate.HasValue)
								impressions.Add(String.Format("Rate: {0}", packageRecord.Rate.Value.ToString("$#,###.00")));

							var investments = new List<string>();
							if (PackageSettings.ShowInvestment && packageRecord.InvestmentCalculated.HasValue)
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

		public override void OutputPowerPoint()
		{
			PopulateReplacementsList();
			OutputPowerPointSlides();
		}
		protected abstract void OutputPowerPointSlides();

		public override void OutputPdf()
		{
			PopulateReplacementsList();
			OutputPdfSlides();
		}

		protected abstract void OutputPdfSlides();

		public override void Preview()
		{
			PopulateReplacementsList();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Presentation for Preview...");
			FormProgress.ShowProgress();
			var tempFileName = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			OnlineSchedulePowerPointHelper.Instance.PrepareWebPackageEmail(this, tempFileName);
			Utilities.ActivateForm(MainForm.Handle, MainForm.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();
			if (File.Exists(tempFileName))
				PreviewSlides(tempFileName);
		}

		protected abstract void PreviewSlides(string tempFileName);

		public override void Email()
		{
			PopulateReplacementsList();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Presentation for Email...");
			FormProgress.ShowProgress();
			var tempFileName = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			OnlineSchedulePowerPointHelper.Instance.PrepareWebPackageEmail(this, tempFileName);
			FormProgress.CloseProgress();
			if (File.Exists(tempFileName))
				EmailSlides(tempFileName);
		}

		protected abstract void EmailSlides(string tempFileName);
		#endregion
	}
}