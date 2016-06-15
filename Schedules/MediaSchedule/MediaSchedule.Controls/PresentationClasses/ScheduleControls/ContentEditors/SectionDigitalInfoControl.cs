using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Section.Digital;
using Asa.Business.Media.Enums;
using Asa.Business.Online.Dictionaries;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ImageGallery;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.BusinessClasses;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.Output;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	[ToolboxItem(false)]
	//public partial class SectionDigitalInfoControl : UserControl
	public partial class SectionDigitalInfoControl : XtraTabPage, ISectionEditorControl, ISectionOutputControl, ISectionItemCollectionControl
	{
		private bool _allowToSave;
		private SectionContainer _sectionContainer;
		private SectionDigitalInfo _digitalInfo;
		private GridDragDropHelper _dragDropHelper;
		public SectionEditorType EditorType => SectionEditorType.DigitalSection;

		public string CollectionTitle => "Digital";
		public string CollectionItemTitle => "Product";
		public bool AllowToAddItem => _digitalInfo != null && _digitalInfo.Products.Count < OutputScheduleData.MaxDigitalProducts;
		public bool AllowToDeleteItem => _digitalInfo != null && _digitalInfo.Products.Any();

		public SectionDigitalInfoControl(SectionContainer sectionContainer)
		{
			InitializeComponent();
			_sectionContainer = sectionContainer;
			Text = "Digital";
			pnContent.Dock = DockStyle.Fill;
			pbNoProducts.Dock = DockStyle.Fill;
		}

		public void InitControls()
		{
			if ((CreateGraphics()).DpiX > 96)
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

		#region Data Methods
		public void LoadData()
		{
			_digitalInfo = _sectionContainer.SectionData.DigitalInfo;

			_allowToSave = false;

			repositoryItemComboBoxCategory.Items.Clear();
			repositoryItemComboBoxCategory.Items.AddRange(ListManager.Instance.ProductSources
				.Where(ps => ps.Category != null)
				.Select(ps => ps.Category.Name)
				.Distinct()
				.ToArray());

			spinEditMonthlyInvestment.EditValue = _digitalInfo.MonthlyInvestment;
			spinEditTotalInvestment.EditValue = _digitalInfo.TotalInvestment;

			UpdateGridData();
			UpdateGridView();
			UpdateProductsSplash();
			InitDargDropHelper();
			_allowToSave = true;
		}

		public void SaveData()
		{
			_digitalInfo.MonthlyInvestment = _digitalInfo.ShowMonthlyInvestemt ? (Decimal?)spinEditMonthlyInvestment.EditValue : null;
			_digitalInfo.TotalInvestment = _digitalInfo.ShowTotalInvestemt ? (Decimal?)spinEditTotalInvestment.EditValue : null;
			advBandedGridView.CloseEditor();
		}

		public void AddItem()
		{
			_digitalInfo.AddProduct();
			UpdateGridData();
			UpdateProductsSplash();
			InitDargDropHelper();
			if (advBandedGridView.RowCount > 0)
				advBandedGridView.FocusedRowHandle = advBandedGridView.RowCount - 1;
			_sectionContainer.RaiseDataChanged();
		}

		public void DeleteItem()
		{
			var selectedProduct = advBandedGridView.GetFocusedRow() as SectionDigitalProduct;
			if (selectedProduct == null) return;
			if (PopupMessageHelper.Instance.ShowWarningQuestion(String.Format("Delete Product ID {0}?", selectedProduct.Index.ToString("# ##0"))) != DialogResult.Yes)
				return;
			_digitalInfo.DeleteProduct(selectedProduct);
			UpdateGridData();
			UpdateProductsSplash();
			InitDargDropHelper();
			_sectionContainer.RaiseDataChanged();
		}

		private void CloneItem(int sourceIndex)
		{
			_digitalInfo.CloneProduct(sourceIndex);
			UpdateGridData();
			UpdateProductsSplash();
			InitDargDropHelper();
			advBandedGridView.FocusedRowHandle = advBandedGridView.RowCount - 1;
			_sectionContainer.RaiseDataChanged();
		}

		private void UpdateGridData()
		{
			gridControl.DataSource = _digitalInfo.Products;
			advBandedGridView.RefreshData();
		}

		public void UpdateGridView()
		{
			bandedGridColumnCategory.Visible = _digitalInfo.ShowCategory;
			bandedGridColumnGroup.Visible = _digitalInfo.ShowSubCategory;
			bandedGridColumnProduct.Visible = _digitalInfo.ShowProduct;
			bandedGridColumnInfo.Visible = _digitalInfo.ShowInfo;
			bandedGridColumnLogo.Visible = _digitalInfo.ShowLogo;

			pnTotalInvestment.Visible = _digitalInfo.ShowTotalInvestemt;
			pnTotalInvestment.BringToFront();

			pnMonthlyInvestment.Visible = _digitalInfo.ShowMonthlyInvestemt;
			pnMonthlyInvestment.BringToFront();
		}

		private void UpdateProductsSplash()
		{
			if (_digitalInfo.Products.Any())
				pnContent.BringToFront();
			else
				pbNoProducts.BringToFront();
		}
		#endregion

		#region Common Methods
		public void Release()
		{
			gridControl.DataSource = null;
			_dragDropHelper.AfterDrop -= OnGridControlAfterDrop;
			_digitalInfo = null;
			_sectionContainer = null;
		}

		private IEnumerable<DXMenuItem> GetContextMenuItems(ColumnView targetView, int targetRowHandle)
		{
			var items = new List<DXMenuItem>();
			var sourceIndex = targetView.GetDataSourceRowIndex(targetRowHandle);
			if (AllowToAddItem)
				items.Add(new DXMenuItem("Clone this Line", (o, args) => CloneItem(sourceIndex)));
			if (AllowToDeleteItem)
				items.Add(new DXMenuItem("Delete this Line", (o, e) => DeleteItem()));
			return items;
		}

		private void InitDargDropHelper()
		{
			if (_dragDropHelper != null || !_digitalInfo.Products.Any()) return;
			_dragDropHelper = new GridDragDropHelper(advBandedGridView, true);
			_dragDropHelper.AfterDrop += OnGridControlAfterDrop;
		}
		#endregion

		#region Control Event Handlers
		private void OnGridControlAfterDrop(object sender, DragEventArgs e)
		{
			var grid = sender as GridControl;
			var view = grid.MainView as GridView;
			var hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
			var downHitInfo = e.Data.GetData(typeof(BandedGridHitInfo)) as BandedGridHitInfo;
			if (downHitInfo == null) return;
			var sourceRow = downHitInfo.RowHandle;
			var targetRow = hitInfo.HitTest == GridHitTest.EmptyRow ? view.DataRowCount : hitInfo.RowHandle;
			_digitalInfo.ChangeProductPosition(sourceRow, targetRow);
			UpdateGridData();
			if (advBandedGridView.RowCount > 0)
				advBandedGridView.FocusedRowHandle = targetRow;
			_sectionContainer.RaiseDataChanged();
		}

		private void OnGridViewCellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (!_allowToSave) return;
			if (e.Column == bandedGridColumnCategory)
			{
				advBandedGridView.PostEditor();
				advBandedGridView.CloseEditor();
				_allowToSave = false;
				advBandedGridView.SetRowCellValue(e.RowHandle, bandedGridColumnGroup, String.Empty);
				advBandedGridView.SetRowCellValue(e.RowHandle, bandedGridColumnProduct, String.Empty);
				_allowToSave = true;
			}
			else if (e.Column == bandedGridColumnGroup)
			{
				advBandedGridView.PostEditor();
				advBandedGridView.CloseEditor();
				_allowToSave = false;
				advBandedGridView.SetRowCellValue(e.RowHandle, bandedGridColumnProduct, String.Empty);
				_allowToSave = true;
			}
			_sectionContainer.RaiseDataChanged();
		}

		private void OnGridViewRowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Column != bandedGridColumnLogo) return;
			var digitalProduct = advBandedGridView.GetFocusedRow() as SectionDigitalProduct;
			if (digitalProduct == null) return;
			using (var form = new FormImageGallery(MediaMetaData.Instance.ListManager.Images))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (form.SelectedImageSource == null) return;
				digitalProduct.Logo = form.SelectedImageSource.Clone<ImageSource, ImageSource>();
				advBandedGridView.UpdateCurrentRow();
				_sectionContainer.RaiseDataChanged();
			}
		}

		private void OnGridViewShowingEditor(object sender, CancelEventArgs e)
		{
			var focussedRecord = advBandedGridView.GetFocusedRow() as SectionDigitalProduct;
			if (focussedRecord == null) return;
			if (advBandedGridView.FocusedColumn == bandedGridColumnProduct)
			{
				var category = _digitalInfo.ShowCategory ? focussedRecord.Category : null;
				var subCategory = _digitalInfo.ShowSubCategory ? focussedRecord.SubCategory : null;
				repositoryItemComboBoxProduct.Items.Clear();
				repositoryItemComboBoxProduct.Items.AddRange(ListManager.Instance.ProductSources.Where(x => (x.Category.Name.Equals(category) || String.IsNullOrEmpty(category)) && (x.SubCategory.Equals(subCategory) || String.IsNullOrEmpty(subCategory))).Select(x => x.Name).Distinct().ToArray());
			}
			else if (advBandedGridView.FocusedColumn == bandedGridColumnGroup)
			{
				var category = _digitalInfo.ShowCategory ? focussedRecord.Category : null;
				var subCategories = ListManager.Instance.ProductSources.Where(x => (x.Category.Name.Equals(category) || String.IsNullOrEmpty(category)) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray();
				repositoryItemComboBoxGroup.Items.Clear();
				repositoryItemComboBoxGroup.Items.AddRange(subCategories);
			}

			var view = sender as AdvBandedGridView;
			var edit = view.ActiveEditor as TextEdit;
			if (edit == null) return;
			edit.Properties.BeforeShowMenu += Properties_BeforeShowMenu;
		}

		private void OnGridViewPopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
		{
			if (!e.HitInfo.InRowCell) return;
			foreach (var menuItem in GetContextMenuItems(advBandedGridView, e.HitInfo.RowHandle))
				e.Menu.Items.Add(menuItem);
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

		private void Properties_BeforeShowMenu(object sender, BeforeShowMenuEventArgs e)
		{
			var items = GetContextMenuItems(advBandedGridView, advBandedGridView.FocusedRowHandle);
			if (!items.Any()) return;
			e.Menu.Items.Clear();
			foreach (var menuItem in items)
				e.Menu.Items.Add(menuItem);
		}

		private void OnInvestmentEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_sectionContainer.RaiseDataChanged();
		}
		#endregion

		#region Output Stuff
		private SlideType SlideType => MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ?
			SlideType.TVProgramSchedule :
			SlideType.RadioProgramSchedule;

		private Theme SelectedTheme
		{
			get
			{
				return BusinessObjects.Instance.ThemeManager
					.GetThemes(SlideType)
					.FirstOrDefault(t =>
						t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType)) ||
						String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType)));
			}
		}

		public IEnumerable<ScheduleSectionOutputType> GetAvailableOutputOptions()
		{
			return _digitalInfo != null && _digitalInfo.Products.Any() ?
				new[] { ScheduleSectionOutputType.Digital } :
				new ScheduleSectionOutputType[] { };
		}

		public IEnumerable<OutputDigitalData> PrepareOutput()
		{
			var outputPages = new List<OutputDigitalData>();
			var outputPage = new OutputDigitalData(_sectionContainer.SectionData);

			outputPage.Advertiser = _sectionContainer.SectionData.ParentScheduleSettings.BusinessName;
			outputPage.DecisionMaker = _sectionContainer.SectionData.ParentScheduleSettings.DecisionMaker;

			var temp = new List<string>();
			if (_sectionContainer.SectionData.DigitalInfo.ShowMonthlyInvestemt && _sectionContainer.SectionData.DigitalInfo.MonthlyInvestment.HasValue)
				temp.Add(String.Format("Monthly Digital Investment: {0}", _sectionContainer.SectionData.DigitalInfo.MonthlyInvestment.Value.ToString("$#,###.00")));
			if (_sectionContainer.SectionData.DigitalInfo.ShowTotalInvestemt && _sectionContainer.SectionData.DigitalInfo.TotalInvestment.HasValue)
				temp.Add(String.Format("Total Digital Investment: {0}", _sectionContainer.SectionData.DigitalInfo.TotalInvestment.Value.ToString("$#,###.00")));
			outputPage.DigitalInfo = temp.Any() ? String.Join("     ", temp) : String.Empty;

			outputPage.Color = MediaMetaData.Instance.SettingsManager.SelectedColor ??
				BusinessObjects.Instance.OutputManager.ScheduleColors.Items.Select(ci => ci.Name).FirstOrDefault();

			#region Set OutputDigitalProduct Values
			for (var j = 0; j < _sectionContainer.SectionData.DigitalInfo.Products.Count; j++)
			{
				var product = _sectionContainer.SectionData.DigitalInfo.Products[j];
				var outputProduct = new OutputDigitalProduct();
				outputProduct.Logo = _sectionContainer.SectionData.DigitalInfo.ShowLogo ?
					product.Logo?.Clone<ImageSource, ImageSource>() :
					null;
				outputProduct.Category = _sectionContainer.SectionData.DigitalInfo.ShowCategory ?
					product.Category :
					String.Empty;
				outputProduct.SubCategory = _sectionContainer.SectionData.DigitalInfo.ShowSubCategory ?
					product.SubCategory :
					String.Empty;
				outputProduct.Product = _sectionContainer.SectionData.DigitalInfo.ShowProduct ?
					product.Name :
					String.Empty;
				outputProduct.Info = _sectionContainer.SectionData.DigitalInfo.ShowInfo ?
					product.Info :
					String.Empty;
				outputPage.DigitalProducts.Add(outputProduct);
				Application.DoEvents();
			}
			#endregion

			outputPage.GetDigitalLogos();
			outputPage.PopulateReplacementsList();

			outputPages.Add(outputPage);
			return outputPages;
		}

		public void GenerateOutput()
		{
			var outputPages = PrepareOutput();
			RegularMediaSchedulePowerPointHelper.Instance.AppendDigitalOneSheet(outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
		}

		public PreviewGroup GeneratePreview()
		{
			var outputPages = PrepareOutput();
			var previewGroup = new PreviewGroup
			{
				Name = Text,
				PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
			RegularMediaSchedulePowerPointHelper.Instance.PrepareDigitalOneSheetEmail(previewGroup.PresentationSourcePath, outputPages, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster);
			return previewGroup;
		}
		#endregion
	}
}
