using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Digital;
using Asa.Business.Media.Interfaces;
using Asa.Business.Online.Dictionaries;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ImageGallery;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.BusinessClasses.Output.DigitalInfo;
using Asa.Media.Controls.InteropClasses;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Digital.DigitalInfo
{
	[ToolboxItem(false)]
	//public partial class BaseDigitalInfoEditControl : UserControl
	public abstract partial class BaseDigitalInfoEditControl : XtraTabPage
	{
		private bool _allowToSave;
		private GridDragDropHelper _dragDropHelper;
		protected IDigitalInfoContainer _dataContainer;
		protected MediaDigitalInfo _digitalInfo;

		public bool AllowToAddItem => _digitalInfo != null && _digitalInfo.Records.Count < BaseDigitalInfoOneSheetOutputModel.MaxRecords;
		public bool AllowToDeleteItem => _digitalInfo != null && _digitalInfo.Records.Any();

		protected BaseDigitalInfoEditControl()
		{
			InitializeComponent();
			Text = "Digital";
		}

		public virtual void InitControls()
		{
			bandedGridColumnCategory.Caption = ListManager.Instance.DefaultControlsConfiguration.DigitalInfoColumnsCategoryTitle ?? bandedGridColumnCategory.Caption;
			bandedGridColumnGroup.Caption = ListManager.Instance.DefaultControlsConfiguration.DigitalInfoColumnsSubCategoryTitle ?? bandedGridColumnGroup.Caption;
			bandedGridColumnProduct.Caption = ListManager.Instance.DefaultControlsConfiguration.DigitalInfoColumnsProductTitle ?? bandedGridColumnProduct.Caption;
			bandedGridColumnInfo.Caption = ListManager.Instance.DefaultControlsConfiguration.DigitalInfoColumnsInfoTitle ?? bandedGridColumnInfo.Caption;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);

			bandedGridColumnId.Width = (Int32)(bandedGridColumnId.Width * scaleFactor.Width);
			bandedGridColumnLogo.Width = (Int32)(bandedGridColumnLogo.Width * scaleFactor.Width);
			spinEditMonthlyInvestment.MaximumSize = RectangleHelper.ScaleSize(spinEditMonthlyInvestment.MaximumSize, scaleFactor);
			spinEditMonthlyInvestment.MinimumSize = RectangleHelper.ScaleSize(spinEditMonthlyInvestment.MinimumSize, scaleFactor);
			spinEditTotalInvestment.MaximumSize = RectangleHelper.ScaleSize(spinEditTotalInvestment.MaximumSize, scaleFactor);
			spinEditTotalInvestment.MinimumSize = RectangleHelper.ScaleSize(spinEditTotalInvestment.MinimumSize, scaleFactor);

			advBandedGridView.BestFitColumns(true);
		}

		#region Data Methods
		protected abstract void RaiseDataChanged();

		public virtual void LoadData()
		{
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
			RaiseDataChanged();
		}

		public void DeleteItem()
		{
			var selectedProduct = advBandedGridView.GetFocusedRow() as MediaDigitalInfoRecord;
			if (selectedProduct == null) return;
			if (PopupMessageHelper.Instance.ShowWarningQuestion(String.Format("Delete Product ID {0}?", selectedProduct.Index.ToString("# ##0"))) != DialogResult.Yes)
				return;
			_digitalInfo.DeleteProduct(selectedProduct);
			UpdateGridData();
			UpdateProductsSplash();
			InitDargDropHelper();
			RaiseDataChanged();
		}

		private void CloneItem(int sourceIndex)
		{
			_digitalInfo.CloneProduct(sourceIndex);
			UpdateGridData();
			UpdateProductsSplash();
			InitDargDropHelper();
			advBandedGridView.FocusedRowHandle = advBandedGridView.RowCount - 1;
			RaiseDataChanged();
		}

		private void UpdateGridData()
		{
			gridControl.DataSource = _digitalInfo.Records;
			advBandedGridView.RefreshData();
		}

		public void UpdateGridView()
		{
			bandedGridColumnCategory.Visible = _digitalInfo.ShowCategory;
			bandedGridColumnGroup.Visible = _digitalInfo.ShowSubCategory;
			bandedGridColumnProduct.Visible = _digitalInfo.ShowProduct;
			bandedGridColumnInfo.Visible = _digitalInfo.ShowInfo;
			bandedGridColumnLogo.Visible = _digitalInfo.ShowLogo;

			layoutControlItemTotalInvestment.Visibility = _digitalInfo.ShowTotalInvestemt
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			layoutControlItemMonthlyInvestment.Visibility = _digitalInfo.ShowMonthlyInvestemt
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
			emptySpaceItemInvestemt.Visibility = _digitalInfo.ShowTotalInvestemt && _digitalInfo.ShowMonthlyInvestemt
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
		}

		private void UpdateProductsSplash()
		{
			if (_digitalInfo.Records.Any())
			{
				layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Never;
				layoutControlGroupActiveContent.Visibility = LayoutVisibility.Always;
			}
			else
			{
				layoutControlGroupActiveContent.Visibility = LayoutVisibility.Never;
				layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Always;
			}
		}
		#endregion

		#region Common Methods
		public virtual void Release()
		{
			gridControl.DataSource = null;
			if (_dragDropHelper != null)
				_dragDropHelper.AfterDrop -= OnGridControlAfterDrop;
			_digitalInfo = null;
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
			if (_dragDropHelper != null || !_digitalInfo.Records.Any()) return;
			_dragDropHelper = new GridDragDropHelper(advBandedGridView, true, handledColumns: new[] { bandedGridColumnId, bandedGridColumnLogo });
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
			RaiseDataChanged();
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
			RaiseDataChanged();
		}

		private void OnGridViewRowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Column != bandedGridColumnLogo) return;
			if (e.Clicks < 2) return;
			var digitalProduct = advBandedGridView.GetFocusedRow() as MediaDigitalInfoRecord;
			if (digitalProduct == null) return;
			using (var form = new FormImageGallery(ListManager.Instance.Images))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (form.SelectedImageSource == null) return;
				digitalProduct.Logo = form.SelectedImageSource.Clone<ImageSource, ImageSource>();
				advBandedGridView.UpdateCurrentRow();
				RaiseDataChanged();
			}
		}

		private void OnGridViewShowingEditor(object sender, CancelEventArgs e)
		{
			var focussedRecord = advBandedGridView.GetFocusedRow() as MediaDigitalInfoRecord;
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

			var view = (AdvBandedGridView)sender;
			var edit = view.ActiveEditor as TextEdit;
			if (edit == null) return;
			edit.Properties.BeforeShowMenu += OnMenuBeforeShow;
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

		private void OnMenuBeforeShow(object sender, BeforeShowMenuEventArgs e)
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
			RaiseDataChanged();
		}

		private void OnTooltipGetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			if (e.SelectedControl != gridControl) return;
			var view = gridControl.GetViewAt(e.ControlMousePosition) as GridView;
			if (view == null) return;
			var hi = view.CalcHitInfo(e.ControlMousePosition);
			if (!hi.InRowCell) return;
			if (hi.Column != bandedGridColumnLogo) return;
			e.Info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), "Double-Click to change the logo…");
			e.Info.ImmediateToolTip = true;
			e.Info.Interval = 0;
		}
		#endregion

		#region Output Stuff
		public abstract SlideType SlideType { get; }

		private Theme SelectedTheme
		{
			get
			{
				return BusinessObjects.Instance.ThemeManager
					.GetThemes(SlideType)
					.FirstOrDefault(t =>
						t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedThemeName(SlideType)) ||
						String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedThemeName(SlideType)));
			}
		}

		public IList<OutputItem> GetOutputItems()
		{
			var outputItems = new List<OutputItem>();

			if (_digitalInfo != null && _digitalInfo.Records.Any())
				outputItems.AddRange(new[] {
					new OutputItem
					{
						Name = "Digital",
						IsCurrent = TabControl != null && TabControl.SelectedTabPage == this,
						PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath,
							Path.GetFileName(Path.GetTempFileName())),
						SlidesCount = BaseDigitalInfoOneSheetOutputModel.SlideCount,
						SlideGeneratingAction = (processor, destinationPresentation) =>
						{
							var outputPage = PrepareOneSheetOutput();
							processor.AppendDigitalOneSheet(outputPage, SelectedTheme, MediaMetaData.Instance.SettingsManager.UseSlideMaster,
								destinationPresentation);
						},
						PreviewGeneratingAction = (processor, presentationSourcePath) =>
						{
							var outputPage = PrepareOneSheetOutput();
							processor.PrepareDigitalOneSheetEmail(presentationSourcePath, outputPage, SelectedTheme,
								MediaMetaData.Instance.SettingsManager.UseSlideMaster);
						}
					},
					new OutputItem
					{
						Name = "Digital Strategies",
						IsCurrent = false,
						PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath,
							Path.GetFileName(Path.GetTempFileName())),
						SlidesCount = BaseDigitalInfoOneSheetOutputModel.SlideCount,
						SlideGeneratingAction = (processor, destinationPresentation) =>
						{
							var dataModel = PrepareStrategyOutput();
							processor.AppendStrategy(dataModel, SelectedTheme, destinationPresentation);
						},
						PreviewGeneratingAction = (processor, presentationSourcePath) =>
						{
							var dataModel = PrepareStrategyOutput();
							processor.PrepareStrategyEmail(presentationSourcePath, dataModel, SelectedTheme);
						}
					}
				});

			return outputItems;
		}

		#region OneSheet
		private StandaloneDigitalInfoOneSheetOutputModel PrepareOneSheetOutput()
		{
			var outputPage = new StandaloneDigitalInfoOneSheetOutputModel(_dataContainer);

			outputPage.Advertiser = _dataContainer.ParentScheduleSettings.BusinessName;
			outputPage.DecisionMaker = _dataContainer.ParentScheduleSettings.DecisionMaker;

			var temp = new List<string>();
			if (_digitalInfo.ShowMonthlyInvestemt && _digitalInfo.MonthlyInvestment.HasValue)
				temp.Add(String.Format("Monthly Digital Investment: {0}", _digitalInfo.MonthlyInvestment.Value.ToString("$#,###.00")));
			if (_digitalInfo.ShowTotalInvestemt && _digitalInfo.TotalInvestment.HasValue)
				temp.Add(String.Format("Total Digital Investment: {0}", _digitalInfo.TotalInvestment.Value.ToString("$#,###.00")));
			outputPage.SummaryInfo = temp.Any() ? String.Join("     ", temp) : String.Empty;

			outputPage.Color = MediaMetaData.Instance.SettingsManager.SelectedColor ??
				BusinessObjects.Instance.OutputManager.ScheduleColors.Items.Select(ci => ci.Name).FirstOrDefault();

			#region Set OutputDigitalProduct Values
			for (var j = 0; j < _digitalInfo.Records.Count; j++)
			{
				var digitalInfoRecord = _digitalInfo.Records[j];
				var outputProduct = new DigitalInfoRecordOutputModel();
				outputProduct.LineID = digitalInfoRecord.Index.ToString("00");
				outputProduct.Logo = _digitalInfo.ShowLogo ?
					digitalInfoRecord.Logo?.Clone<ImageSource, ImageSource>() :
					null;
				outputProduct.Details = digitalInfoRecord.OneSheetDetails;
				outputPage.Records.Add(outputProduct);
				Application.DoEvents();
			}
			#endregion

			outputPage.GetLogos();
			outputPage.PopulateReplacementsList();

			return outputPage;
		}
		#endregion

		#region Strategy

		private DigitalInfoStrategyOutputModel PrepareStrategyOutput()
		{
			var outputModel = new DigitalInfoStrategyOutputModel();

			outputModel.ShowLogos = _digitalInfo.ShowLogo;

			var totals = new List<string>();
			if (_digitalInfo.ShowTotalInvestemt && _digitalInfo.TotalInvestment.HasValue)
				totals.Add(String.Format("Total Investment:  {0}", _digitalInfo.TotalInvestment.Value.ToString("$#,##0.00")));
			if (_digitalInfo.ShowMonthlyInvestemt && _digitalInfo.MonthlyInvestment.HasValue)
				totals.Add(String.Format("Monthly Investment:  {0}", _digitalInfo.MonthlyInvestment.Value.ToString("$#,##0.00")));
			outputModel.Total1 = totals.ElementAtOrDefault(0);
			outputModel.Total2 = totals.ElementAtOrDefault(1);

			foreach (var digitalInfoRecord in _digitalInfo.Records)
			{
				var recordOutputModel = new DigitalInfoStrategyRecordOutputModel();
				recordOutputModel.Logo = digitalInfoRecord.Logo;

				var values = new List<string>();
				if (_digitalInfo.ShowCategory && !String.IsNullOrEmpty(digitalInfoRecord.Category))
					values.Add(digitalInfoRecord.Category);
				if (_digitalInfo.ShowSubCategory && !String.IsNullOrEmpty(digitalInfoRecord.SubCategory))
					values.Add(digitalInfoRecord.SubCategory);
				recordOutputModel.Text1 = String.Join("  -  ", values);

				values.Clear();
				if (_digitalInfo.ShowProduct && !String.IsNullOrEmpty(digitalInfoRecord.Name))
					values.Add(digitalInfoRecord.Name);
				if (_digitalInfo.ShowInfo && !String.IsNullOrEmpty(digitalInfoRecord.Info))
					values.Add(digitalInfoRecord.Info);
				recordOutputModel.Text2 = String.Join("   |   ", values);

				outputModel.Records.Add(recordOutputModel);
			}

			outputModel.GetLogos();
			return outputModel;
		}
		#endregion

		#endregion
	}
}
