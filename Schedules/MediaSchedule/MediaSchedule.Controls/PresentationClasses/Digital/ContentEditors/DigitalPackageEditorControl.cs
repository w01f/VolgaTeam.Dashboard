using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Online.Configuration;
using Asa.Business.Online.Dictionaries;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Enums;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.PresentationClasses.Digital.Output;
using Asa.Media.Controls.PresentationClasses.Digital.Settings;
using Asa.Online.Controls.InteropClasses;
using Asa.Online.Controls.PresentationClasses.Packages;
using Asa.Online.Controls.PresentationClasses.Products;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	[ToolboxItem(false)]
	//public partial class DigitalPackageEditorControl:UserControl
	public partial class DigitalPackageEditorControl : XtraTabPage, IDigitalEditor, IDigitalOutputContainer, IDigitalOutputItem, IWebPackageOutput
	{
		private bool _allowApplyValues;
		private bool _needToReload;
		private readonly DigitalEditorsContainer _container;
		public DigitalEditorType EditorType => DigitalEditorType.Package;
		public string HelpTag => "digitalpk";

		public event EventHandler<DataChangedEventArgs> DataChanged;

		private IEnumerable<ProductPackageRecord> PackageRecords
		{
			get
			{
				return _container.EditedContent.DigitalProducts
					.OrderBy(p => p.Index)
					.Select(p => p.PackageRecord)
					.ToList();
			}
		}

		public DigitalPackageSettings PackageSettings => _container.EditedContent.ScheduleSettings.DigitalPackageSettings;

		public DigitalPackageEditorControl(DigitalEditorsContainer container)
		{
			InitializeComponent();
			Text = ListManager.Instance.DefaultControlsConfiguration.SectionsPackageTitle ?? "Digital Package";
			_container = container;

			repositoryItemComboBoxCategory.Items.Clear();
			repositoryItemComboBoxCategory.Items.AddRange(ListManager.Instance.ProductSources
				.Where(ps => ps.Category != null)
				.Select(ps => ps.Category.Name)
				.Distinct()
				.ToArray());

			bandedGridColumnCategory.Caption = ListManager.Instance.DefaultControlsConfiguration.PackageColumnsCategoryTitle ?? bandedGridColumnCategory.Caption;
			bandedGridColumnGroup.Caption = ListManager.Instance.DefaultControlsConfiguration.PackageColumnsSubCategoryTitle ?? bandedGridColumnGroup.Caption;
			bandedGridColumnProduct.Caption = ListManager.Instance.DefaultControlsConfiguration.PackageColumnsProductTitle ?? bandedGridColumnProduct.Caption;
			bandedGridColumnInfo.Caption = ListManager.Instance.DefaultControlsConfiguration.PackageColumnsInfoTitle ?? bandedGridColumnInfo.Caption;
			bandedGridColumnComments.Caption = ListManager.Instance.DefaultControlsConfiguration.PackageColumnsCommentsTitle ?? bandedGridColumnComments.Caption;
			bandedGridColumnRate.Caption = ListManager.Instance.DefaultControlsConfiguration.PackageColumnsRateTitle ?? bandedGridColumnRate.Caption;
			bandedGridColumnInvestment.Caption = ListManager.Instance.DefaultControlsConfiguration.PackageColumnsInvestmentTitle ?? bandedGridColumnInvestment.Caption;
			bandedGridColumnImpressions.Caption = ListManager.Instance.DefaultControlsConfiguration.PackageColumnsImpressionsTitle ?? bandedGridColumnImpressions.Caption;
			bandedGridColumnCPM.Caption = ListManager.Instance.DefaultControlsConfiguration.PackageColumnsCPMTitle ?? bandedGridColumnCPM.Caption;

			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
				advBandedGridView.Appearance.BandPanel.Font = font;
				advBandedGridView.Appearance.EvenRow.Font = font;
				advBandedGridView.Appearance.FocusedCell.Font = font;
				advBandedGridView.Appearance.FocusedRow.Font = font;
				advBandedGridView.Appearance.HeaderPanel.Font = new Font(font.FontFamily, font.Size, FontStyle.Bold);
				advBandedGridView.Appearance.OddRow.Font = font;
				advBandedGridView.Appearance.Row.Font = font;
				advBandedGridView.Appearance.SelectedRow.Font = font;
			}
		}

		public void LoadData()
		{
			if (!_needToReload) return;

			_allowApplyValues = false;

			gridControl.DataSource = null;
			UpdateGridColumns();
			gridControl.DataSource = PackageRecords;

			_allowApplyValues = true;

			_needToReload = false;
		}

		public void RequestReload()
		{
			_needToReload = true;
		}

		public void SaveData()
		{
			advBandedGridView.PostEditor();
		}

		public void UpdateAccordingSettings(SettingsChangedEventArgs e)
		{
			advBandedGridView.PostEditor();
			UpdateGridColumns();
			advBandedGridView.RefreshData();
		}

		private void RaiseDataChanged()
		{
			DataChanged?.Invoke(this, new DataChangedEventArgs { ChangedEditorType = EditorType });
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

		#region Control Event Handlers

		private void OnGridViewCellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (_allowApplyValues)
			{
				RaiseDataChanged();
				if (e.Column == bandedGridColumnCategory)
				{
					advBandedGridView.PostEditor();
					advBandedGridView.CloseEditor();
					_allowApplyValues = false;
					advBandedGridView.SetRowCellValue(e.RowHandle, bandedGridColumnGroup, String.Empty);
					advBandedGridView.SetRowCellValue(e.RowHandle, bandedGridColumnProduct, String.Empty);
					_allowApplyValues = true;
				}
				else if (e.Column == bandedGridColumnGroup)
				{
					advBandedGridView.PostEditor();
					advBandedGridView.CloseEditor();
					_allowApplyValues = false;
					advBandedGridView.SetRowCellValue(e.RowHandle, bandedGridColumnProduct, String.Empty);
					_allowApplyValues = true;
				}
			}
		}

		private void OnResetClick(object sender, OpenLinkEventArgs e)
		{
			advBandedGridView.PostEditor();
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Do you want to reset your Categories and Products to the original selections on the HOME Tab") == DialogResult.Yes)
			{
				_allowApplyValues = false;

				PackageSettings.ResetToDefault();
				UpdateGridColumns();

				foreach (var packageRecord in PackageRecords)
					packageRecord.ResetToDefault();
				advBandedGridView.RefreshData();

				RaiseDataChanged();

				_allowApplyValues = true;
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
		#endregion

		#region Output Stuff
		public string SlideName => Text;
		public Theme SelectedTheme => _container.SelectedTheme;
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
		public List<Dictionary<string, string>> OutputReplacementsLists { get; set; }

		public void PopulateReplacementsList()
		{
			var recordsCount = PackageRecords.Count();
			var rowsPerSlide = RowsPerSlide;
			OutputReplacementsLists = new List<Dictionary<string, string>>();
			for (var i = 0; i < recordsCount; i += rowsPerSlide)
			{
				var slideRows = new Dictionary<string, string>();
				slideRows.Add("Digital Media Campaign: AdvertiserNameHere", String.Format("Digital Media Campaign: {0}", _container.EditedContent.ScheduleSettings.BusinessName));
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

		public IList<IDigitalOutputItem> GetOutputItems()
		{
			return new List<IDigitalOutputItem> { this };
		}

		public void GenerateOutput()
		{
			PopulateReplacementsList();
			OnlineSchedulePowerPointHelper.Instance.AppendWebPackage(this);
		}

		public PreviewGroup GeneratePreview()
		{
			var previewGroup = new PreviewGroup
			{
				Name = Text,
				PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
			PopulateReplacementsList();
			OnlineSchedulePowerPointHelper.Instance.PrepareWebPackageEmail(this, previewGroup.PresentationSourcePath);
			return previewGroup;
		}
		#endregion
	}
}