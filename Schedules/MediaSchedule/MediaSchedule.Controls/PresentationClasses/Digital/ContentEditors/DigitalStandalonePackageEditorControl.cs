using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Online.Configuration;
using Asa.Business.Online.Dictionaries;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.PresentationClasses.Digital.Output;
using Asa.Media.Controls.PresentationClasses.Digital.Settings;
using Asa.Online.Controls.InteropClasses;
using Asa.Online.Controls.PresentationClasses.Packages;
using Asa.Online.Controls.PresentationClasses.Products;
using DevComponents.DotNetBar;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using ResourceManager = Asa.Common.Core.Configuration.ResourceManager;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	[ToolboxItem(false)]
	//public partial class DigitalStandalonePackageEditorControl:UserControl
	public partial class DigitalStandalonePackageEditorControl : XtraTabPage, IDigitalEditor, IDigitalOutputContainer, IDigitalOutputItem, IWebPackageOutput, IDigitalItemCollectionEditor
	{
		private bool _allowApplyValues;
		private bool _needToReload;
		private readonly DigitalEditorsContainer _container;
		private GridDragDropHelper _dragDropHelper;
		public DigitalSectionType SectionType => DigitalSectionType.StandalonePackage;
		public string HelpTag => "digitalpk";

		public event EventHandler<DataChangedEventArgs> DataChanged;

		private List<StandalonePackageRecord> PackageRecords => _container.EditedContent.StandalonePackage.Items;

		public DigitalPackageSettings PackageSettings => _container.EditedContent.StandalonePackage.DigitalPackageSettings;
		public bool HasItems => PackageRecords.Any();

		public DigitalStandalonePackageEditorControl(DigitalEditorsContainer container)
		{
			InitializeComponent();

			Text = ListManager.Instance.DefaultControlsConfiguration.SectionsStandalonePackageTitle ?? "Speed Builder";
			_container = container;

			repositoryItemComboBoxCategory.Items.Clear();
			repositoryItemComboBoxCategory.Items.AddRange(ListManager.Instance.ProductSources
				.Where(ps => ps.Category != null)
				.Select(ps => ps.Category.Name)
				.Distinct()
				.ToArray());
			repositoryItemComboBoxLocation.Items.Clear();
			repositoryItemComboBoxLocation.Items.AddRange(ListManager.Instance.ColumnPositions);

			bandedGridColumnCategory.Caption = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageColumnsCategoryTitle ?? bandedGridColumnCategory.Caption;
			bandedGridColumnGroup.Caption = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageColumnsSubCategoryTitle ?? bandedGridColumnGroup.Caption;
			bandedGridColumnProduct.Caption = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageColumnsProductTitle ?? bandedGridColumnProduct.Caption;
			bandedGridColumnInfo.Caption = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageColumnsInfoTitle ?? bandedGridColumnInfo.Caption;
			bandedGridColumnLocation.Caption = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageColumnsLocationTitle ?? bandedGridColumnLocation.Caption;
			bandedGridColumnRate.Caption = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageColumnsRateTitle ?? bandedGridColumnRate.Caption;
			bandedGridColumnInvestment.Caption = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageColumnsInvestmentTitle ?? bandedGridColumnInvestment.Caption;
			bandedGridColumnImpressions.Caption = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageColumnsImpressionsTitle ?? bandedGridColumnImpressions.Caption;
			bandedGridColumnCPM.Caption = ListManager.Instance.DefaultControlsConfiguration.StandalonePackageColumnsCPMTitle ?? bandedGridColumnCPM.Caption;

			pictureEditDefaultLogo.Image = BusinessObjects.Instance.ImageResourcesManager.DigitalStandalonePackageNoRecordsLogo ?? pictureEditDefaultLogo.Image;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			gridBandId.Width = (Int32)(gridBandId.Width * scaleFactor.Width);
			bandedGridColumnImpressions.Width = (Int32)(bandedGridColumnImpressions.Width * scaleFactor.Width);
			bandedGridColumnCPM.Width = (Int32)(bandedGridColumnCPM.Width * scaleFactor.Width);
			gridBandImpressions.Width = (Int32)(gridBandImpressions.Width * scaleFactor.Width);
			bandedGridColumnInvestment.Width = (Int32)(bandedGridColumnInvestment.Width * scaleFactor.Width);
			bandedGridColumnRate.Width = (Int32)(bandedGridColumnRate.Width * scaleFactor.Width);
			gridBandInvestment.Width = (Int32)(gridBandInvestment.Width * scaleFactor.Width);
			gridBandFormula.Width = (Int32)(gridBandFormula.Width * scaleFactor.Width);
		}

		public void LoadData()
		{
			if (!_needToReload) return;

			_allowApplyValues = false;

			gridControl.DataSource = null;
			UpdateGridColumns();
			gridControl.DataSource = PackageRecords;
			InitDargDropHelper();
			UpdateRecordsSplash();

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

		public void AddItem(object sender)
		{
			var category = (Category)((ButtonItem)sender).Tag;
			_container.EditedContent.StandalonePackage.AddItem(category);
			gridControl.DataSource = PackageRecords;
			advBandedGridView.RefreshData();
			UpdateRecordsSplash();
			InitDargDropHelper();
			RaiseDataChanged();
		}

		public void CloneItem()
		{
			var packageRecord = advBandedGridView.GetFocusedRow() as StandalonePackageRecord;
			if (packageRecord == null) return;
			_container.EditedContent.StandalonePackage.CloneItem(advBandedGridView.GetDataSourceRowIndex(advBandedGridView.FocusedRowHandle));
			gridControl.DataSource = PackageRecords;
			advBandedGridView.RefreshData();
			advBandedGridView.FocusedRowHandle = advBandedGridView.RowCount - 1;
			UpdateRecordsSplash();
			RaiseDataChanged();
		}

		public void DeleteItem()
		{
			var packageRecord = advBandedGridView.GetFocusedRow() as StandalonePackageRecord;
			if (packageRecord == null) return;
			var selectedRecordIndex = packageRecord.Index.ToString("#0)");
			if (PopupMessageHelper.Instance.ShowWarningQuestion(
				String.Format("Delete Line ID {0}?", selectedRecordIndex)) != DialogResult.Yes) return;
			_container.EditedContent.StandalonePackage.DeleteItem(advBandedGridView.GetDataSourceRowIndex(advBandedGridView.FocusedRowHandle));
			gridControl.DataSource = PackageRecords;
			advBandedGridView.RefreshData();
			UpdateRecordsSplash();
			RaiseDataChanged();
		}

		private void RaiseDataChanged()
		{
			DataChanged?.Invoke(this, new DataChangedEventArgs { ChangedSectionType = SectionType });
		}

		private void UpdateGridColumns()
		{
			gridBandCategory.Visible = PackageSettings.ShowCategory || PackageSettings.ShowGroup;
			if (PackageSettings.ShowCategory && PackageSettings.ShowGroup)
			{
				bandedGridColumnCategory.Visible = true;
				bandedGridColumnCategory.RowCount = 1;
				advBandedGridView.SetColumnPosition(bandedGridColumnCategory, 0, 0);
				bandedGridColumnGroup.Visible = true;
				bandedGridColumnGroup.RowCount = 1;
				advBandedGridView.SetColumnPosition(bandedGridColumnGroup, 1, 0);
			}
			else if (PackageSettings.ShowCategory)
			{
				bandedGridColumnGroup.Visible = false;
				bandedGridColumnCategory.Visible = true;
				bandedGridColumnCategory.RowCount = 2;
				advBandedGridView.SetColumnPosition(bandedGridColumnCategory, 0, 0);
			}
			else if (PackageSettings.ShowGroup)
			{
				bandedGridColumnCategory.Visible = false;
				bandedGridColumnGroup.Visible = true;
				bandedGridColumnGroup.RowCount = 2;
				advBandedGridView.SetColumnPosition(bandedGridColumnGroup, 0, 0);
			}

			gridBandProduct.Visible = PackageSettings.ShowProduct || PackageSettings.ShowLocation;
			if (PackageSettings.ShowProduct && PackageSettings.ShowLocation)
			{
				bandedGridColumnProduct.Visible = true;
				bandedGridColumnProduct.RowCount = 1;
				advBandedGridView.SetColumnPosition(bandedGridColumnProduct, 0, 0);
				bandedGridColumnLocation.Visible = true;
				bandedGridColumnLocation.RowCount = 1;
				advBandedGridView.SetColumnPosition(bandedGridColumnLocation, 1, 0);
			}
			else if (PackageSettings.ShowProduct)
			{
				bandedGridColumnLocation.Visible = false;
				bandedGridColumnProduct.Visible = true;
				bandedGridColumnProduct.RowCount = 2;
				advBandedGridView.SetColumnPosition(bandedGridColumnProduct, 0, 0);
			}
			else if (PackageSettings.ShowLocation)
			{
				bandedGridColumnProduct.Visible = false;
				bandedGridColumnLocation.Visible = true;
				bandedGridColumnLocation.RowCount = 2;
				advBandedGridView.SetColumnPosition(bandedGridColumnLocation, 0, 0);
			}

			gridBandInfo.Visible = PackageSettings.ShowInfo;

			gridBandImpressions.Visible = PackageSettings.ShowImpressions || PackageSettings.ShowCPM;
			if (PackageSettings.ShowImpressions && PackageSettings.ShowCPM)
			{
				bandedGridColumnImpressions.Visible = true;
				bandedGridColumnImpressions.RowCount = 1;
				advBandedGridView.SetColumnPosition(bandedGridColumnImpressions, 0, 0);
				bandedGridColumnCPM.Visible = true;
				bandedGridColumnCPM.RowCount = 1;
				advBandedGridView.SetColumnPosition(bandedGridColumnCPM, 1, 0);
			}
			else if (PackageSettings.ShowImpressions)
			{
				bandedGridColumnCPM.Visible = false;
				bandedGridColumnImpressions.Visible = true;
				bandedGridColumnImpressions.RowCount = 2;
				advBandedGridView.SetColumnPosition(bandedGridColumnImpressions, 0, 0);
			}
			else if (PackageSettings.ShowCPM)
			{
				bandedGridColumnImpressions.Visible = false;
				bandedGridColumnCPM.Visible = true;
				bandedGridColumnCPM.RowCount = 2;
				advBandedGridView.SetColumnPosition(bandedGridColumnCPM, 0, 0);
			}

			gridBandInvestment.Visible = PackageSettings.ShowInvestment || PackageSettings.ShowRate;
			if (PackageSettings.ShowInvestment && PackageSettings.ShowRate)
			{
				bandedGridColumnInvestment.Visible = true;
				bandedGridColumnInvestment.RowCount = 1;
				advBandedGridView.SetColumnPosition(bandedGridColumnInvestment, 0, 0);
				bandedGridColumnRate.Visible = true;
				bandedGridColumnRate.RowCount = 1;
				advBandedGridView.SetColumnPosition(bandedGridColumnRate, 1, 0);
			}
			else if (PackageSettings.ShowInvestment)
			{
				bandedGridColumnRate.Visible = false;
				bandedGridColumnInvestment.Visible = true;
				bandedGridColumnInvestment.RowCount = 2;
				advBandedGridView.SetColumnPosition(bandedGridColumnInvestment, 0, 0);
			}
			else if (PackageSettings.ShowRate)
			{
				bandedGridColumnInvestment.Visible = false;
				bandedGridColumnRate.Visible = true;
				bandedGridColumnRate.RowCount = 2;
				advBandedGridView.SetColumnPosition(bandedGridColumnRate, 0, 0);
			}

			gridBandFormula.Visible = PackageSettings.ShowInvestment && PackageSettings.ShowImpressions && PackageSettings.ShowCPM;
		}

		private void InitDargDropHelper()
		{
			if (_dragDropHelper != null || !PackageRecords.Any()) return;
			_dragDropHelper = new GridDragDropHelper(advBandedGridView, true, handledColumns: new[] { bandedGridColumnId });
			_dragDropHelper.AfterDrop += OnGridControlAfterDrop;
		}

		private void UpdateRecordsSplash()
		{
			if (PackageRecords.Any())
			{
				layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Never;
				layoutControlItemGrid.Visibility = LayoutVisibility.Always;
			}
			else
			{
				layoutControlItemGrid.Visibility = LayoutVisibility.Never;
				layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Always;
			}
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

		private void OnGridViewShowingEditor(object sender, CancelEventArgs e)
		{
			var focussedRecord = advBandedGridView.GetFocusedRow() as StandalonePackageRecord;
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
				repositoryItemComboBoxProduct.Items.AddRange(ListManager.Instance.ProductSources.Where(x => x.Category != null && (x.Category.Name.Equals(category) || String.IsNullOrEmpty(category)) && (x.SubCategory.Equals(subCategory) || String.IsNullOrEmpty(subCategory))).Select(x => x.Name).Distinct().ToArray());
			}
			else if (advBandedGridView.FocusedColumn == bandedGridColumnGroup)
			{
				var category = PackageSettings.ShowCategory ? focussedRecord.Category : null;
				var subCategories = ListManager.Instance.ProductSources.Where(x => x.Category != null && (x.Category.Name.Equals(category) || String.IsNullOrEmpty(category)) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray();
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

		private void OnGridControlAfterDrop(object sender, DragEventArgs e)
		{
			var grid = sender as GridControl;
			var view = grid.MainView as GridView;
			var hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
			var downHitInfo = e.Data.GetData(typeof(BandedGridHitInfo)) as BandedGridHitInfo;
			if (downHitInfo == null) return;
			var sourceRow = downHitInfo.RowHandle;
			var targetRow = hitInfo.HitTest == GridHitTest.EmptyRow ? view.DataRowCount : hitInfo.RowHandle;
			_container.EditedContent.StandalonePackage.ChangeItemPosition(sourceRow, targetRow);
			gridControl.DataSource = PackageRecords;
			advBandedGridView.RefreshData();
			if (advBandedGridView.RowCount > 0)
				advBandedGridView.FocusedRowHandle = targetRow;
			RaiseDataChanged();
		}

		private void OnTooltipGetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			if (e.SelectedControl != gridControl) return;
			var view = gridControl.GetViewAt(e.ControlMousePosition) as GridView;
			if (view == null) return;
			var hi = view.CalcHitInfo(e.ControlMousePosition);
			if (!hi.InRowCell) return;
			if (hi.Column != bandedGridColumnFormula) return;
			var record = view.GetRow(hi.RowHandle) as StandalonePackageRecord;
			if (record == null) return;
			e.Info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), record.UseFormula ? "Disable Delivery Formula" : "Enable Delivery Formula");
			e.Info.ImmediateToolTip = true;
			e.Info.Interval = 0;
		}
		#endregion

		#region Output Stuff
		public string SlideName => Text;
		public SlideType SlideType => SlideType.DigitalStandalonePackage;
		public Theme SelectedTheme
		{
			get
			{
				var selectedTheme = MediaMetaData.Instance.SettingsManager.GetSelectedThemeName(SlideType);
				return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType).FirstOrDefault(t => t.Name.Equals(selectedTheme) || String.IsNullOrEmpty(selectedTheme));
			}
		}
		public int SlidesCount => PackageRecords.Count / RowsPerSlide + (PackageRecords.Count % RowsPerSlide > 0 ? 1 : 0);
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
			var recordsCount = PackageRecords.Count;
			var rowsPerSlide = RowsPerSlide;
			OutputReplacementsLists = new List<Dictionary<string, string>>();
			for (var i = 0; i < recordsCount; i += rowsPerSlide)
			{
				var slideRows = new Dictionary<string, string>();
				slideRows.Add("Digital Media Campaign: AdvertiserNameHere", _container.EditedContent.ScheduleSettings.BusinessName);
				for (var j = 0; j < rowsPerSlide; j++)
				{
					if ((i + j) < recordsCount)
					{
						var packageRecord = PackageRecords.ElementAt(i + j);
						var mainInfo = new List<string>();
						if (PackageSettings.ShowCategory && !String.IsNullOrEmpty(packageRecord.Category))
							mainInfo.Add(packageRecord.Category);
						if (PackageSettings.ShowGroup && !String.IsNullOrEmpty(packageRecord.SubCategory))
							mainInfo.Add(packageRecord.SubCategory);
						if (PackageSettings.ShowProduct && !String.IsNullOrEmpty(packageRecord.Name))
							mainInfo.Add(packageRecord.Name);

						var additionalInfo = new List<string>();
						if (PackageSettings.ShowLocation && !String.IsNullOrEmpty(packageRecord.Location))
							additionalInfo.Add(packageRecord.Location);
						if (PackageSettings.ShowInfo && !String.IsNullOrEmpty(packageRecord.Info))
							additionalInfo.Add(packageRecord.Info);

						var pricingInfo = new List<string>();
						if (PackageSettings.ShowImpressions && packageRecord.ImpressionsCalculated.HasValue)
							pricingInfo.Add(String.Format("Impressions: {0}", packageRecord.ImpressionsCalculated.Value.ToString("#,##0")));
						if (PackageSettings.ShowInvestment && packageRecord.InvestmentCalculated.HasValue)
							pricingInfo.Add(String.Format("Investment: {0}", packageRecord.InvestmentCalculated.Value.ToString("$#,###.00")));
						if (PackageSettings.ShowCPM && packageRecord.CPMCalculated.HasValue)
							pricingInfo.Add(String.Format("CPM: {0}", packageRecord.CPMCalculated.Value.ToString("$#,###.00")));
						if (PackageSettings.ShowRate && packageRecord.Rate.HasValue)
							pricingInfo.Add(String.Format("Rate: {0}", packageRecord.Rate.Value.ToString("$#,###.00")));

						if (pricingInfo.Any())
							additionalInfo.Add(String.Join(",   ", pricingInfo));

						var mainInfoRow = mainInfo.Any() ?
							String.Join("  |  ", mainInfo.ToArray()) :
							String.Empty;
						var additionalInfoRow = additionalInfo.Any() ?
							String.Format("{0}{2}{2}{1}", additionalInfo.First(), String.Join("  |  ", additionalInfo.Skip(1)), (char)13) :
							String.Empty;


						if (!String.IsNullOrEmpty(mainInfoRow) && !String.IsNullOrEmpty(additionalInfoRow))
						{
							slideRows.Add(String.Format("Category{0}  |  Group{0}  |  Product{0}", j + 1), mainInfoRow);
							slideRows.Add(String.Format("ScheduleProductInfo{0}{1}{1}NotesandComments{0}", j + 1, (char)13), additionalInfoRow);
						}
						else if (!String.IsNullOrEmpty(mainInfoRow))
						{
							slideRows.Add(String.Format("Category{0}  |  Group{0}  |  Product{0}", j + 1), mainInfoRow);
							slideRows.Add(String.Format("ScheduleProductInfo{0}{1}{1}NotesandComments{0}", j + 1, (char)13), "DeleteRow");
						}
						else if (!String.IsNullOrEmpty(additionalInfoRow))
						{
							slideRows.Add(String.Format("Category{0}  |  Group{0}  |  Product{0}", j + 1), additionalInfoRow);
							slideRows.Add(String.Format("ScheduleProductInfo{0}{1}{1}NotesandComments{0}", j + 1, (char)13), "DeleteRow");
						}
					}
					else
					{
						slideRows.Add(String.Format("Category{0}  |  Group{0}  |  Product{0}", j + 1), "DeleteRow");
						slideRows.Add(String.Format("ScheduleProductInfo{0}{1}{1}NotesandComments{0}", j + 1, (char)13), "DeleteRow");
					}
				}
				OutputReplacementsLists.Add(slideRows);
			}
		}

		public OutputGroup GetOutputGroup()
		{
			return new OutputGroup
			{
				Name = SlideName,
				OutputItems = PackageRecords.Any() ?
					new List<IDigitalOutputItem> { this } :
					new List<IDigitalOutputItem>()
			};
		}

		public void GenerateOutput()
		{
			PopulateReplacementsList();
			BusinessObjects.Instance.PowerPointManager.Processor.AppendWebPackage(this);
		}

		public PreviewGroup GeneratePreview()
		{
			var previewGroup = new PreviewGroup
			{
				Name = Text,
				PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
			PopulateReplacementsList();
			BusinessObjects.Instance.PowerPointManager.Processor.PrepareWebPackageEmail(this, previewGroup.PresentationSourcePath);
			return previewGroup;
		}
		#endregion
	}
}