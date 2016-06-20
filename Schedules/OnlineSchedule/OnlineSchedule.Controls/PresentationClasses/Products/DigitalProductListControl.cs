using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Online.Dictionaries;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Enums;
using Asa.Business.Online.Interfaces;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Online.Controls.Properties;
using Asa.Online.Controls.ToolForms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Asa.Online.Controls.PresentationClasses.Products
{
	public partial class DigitalProductListControl : UserControl
	{
		private Action _onDataChanged;
		private GridDragDropHelper _dragDropHelper;

		protected DigitalProductsContent Content { get; private set; }
		protected IDigitalScheduleSettings ScheduleSettings { get; private set; }

		public DigitalProductListControl()
		{
			InitializeComponent();

			gridBandType.Caption = ListManager.Instance.DefaultControlsConfiguration.ListColumnsGroupTitle ?? gridBandType.Caption;
			gridBandName.Caption = ListManager.Instance.DefaultControlsConfiguration.ListColumnsProductTitle ?? gridBandName.Caption;
			gridBandRate.Caption = ListManager.Instance.DefaultControlsConfiguration.ListColumnsPricingTitle ?? gridBandRate.Caption;
			gridBandOptions.Caption = ListManager.Instance.DefaultControlsConfiguration.ListColumnsOptionsTitle ?? gridBandOptions.Caption;

			repositoryItemComboBoxProductType.NullText = ListManager.Instance.DefaultControlsConfiguration.ListEditorsGroupTitle?? repositoryItemComboBoxProductType.NullText;
			repositoryItemComboBoxProductName.NullText = ListManager.Instance.DefaultControlsConfiguration.ListEditorsProductTitle ?? repositoryItemComboBoxProductName.NullText;
			repositoryItemComboBoxLocation.NullText = ListManager.Instance.DefaultControlsConfiguration.ListEditorsLocationTitle ?? repositoryItemComboBoxLocation.NullText;
			repositoryItemHyperLinkEditTargetEnabled.Caption = !String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.ListEditorsTargetingTitle)?
				String.Format("<i>{0}</i>", ListManager.Instance.DefaultControlsConfiguration.ListEditorsTargetingTitle):
				repositoryItemHyperLinkEditTargetEnabled.Caption;
			repositoryItemHyperLinkEditTargetDisabled.Caption = !String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.ListEditorsTargetingTitle) ?
				String.Format("<i>{0}</i>", ListManager.Instance.DefaultControlsConfiguration.ListEditorsTargetingTitle) :
				repositoryItemHyperLinkEditTargetDisabled.Caption;
			repositoryItemHyperLinkEditRichMediaEnabled.Caption = !String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.ListEditorsRichMediaTitle) ?
				String.Format("<i>{0}</i>", ListManager.Instance.DefaultControlsConfiguration.ListEditorsRichMediaTitle) :
				repositoryItemHyperLinkEditRichMediaEnabled.Caption;
			repositoryItemHyperLinkEditRichMediaDisabled.Caption = !String.IsNullOrEmpty(ListManager.Instance.DefaultControlsConfiguration.ListEditorsRichMediaTitle) ?
				String.Format("<i>{0}</i>", ListManager.Instance.DefaultControlsConfiguration.ListEditorsRichMediaTitle) :
				repositoryItemHyperLinkEditRichMediaDisabled.Caption;

			repositoryItemComboBoxProductType.EnableSelectAll();
			repositoryItemComboBoxProductName.EnableSelectAll();
			repositoryItemSpinEditSize.EnableSelectAll();
		}

		public void UpdateData(
			DigitalProductsContent content,
			IDigitalScheduleSettings scheduleSettings,
			Action onDataChanged)
		{
			Content = content;
			ScheduleSettings = scheduleSettings;
			_onDataChanged = onDataChanged;

			gridControl.DataSource = new BindingList<DigitalProduct>(Content.DigitalProducts);

			if (ListManager.Instance.ProductSources.All(productSource => String.IsNullOrEmpty(productSource.SubCategory)))
			{
				gridColumnCategory.RowCount = 2;
				gridColumnSubCategory.Visible = false;
			}

			if (ListManager.Instance.LockedMode)
			{
				gridColumnWidth.OptionsColumn.ReadOnly = true;
				gridColumnWidth.OptionsColumn.AllowEdit = false;
				gridColumnHeight.OptionsColumn.ReadOnly = true;
				gridColumnHeight.OptionsColumn.AllowEdit = false;
				repositoryItemComboBoxProductName.TextEditStyle = TextEditStyles.DisableTextEditor;
			}

			repositoryItemComboBoxRateType.Items.Clear();
			repositoryItemComboBoxRateType.Items.AddRange(ListManager.Instance.PricingStrategies);
			repositoryItemComboBoxLocation.Items.Clear();
			repositoryItemComboBoxLocation.Items.AddRange(ListManager.Instance.ColumnPositions);

			if (_dragDropHelper == null)
			{
				_dragDropHelper = new GridDragDropHelper(advBandedGridView, true);
				_dragDropHelper.AfterDrop += DigitalProductsAfterDrop;
			}
		}

		public void ApplyChanges()
		{
			advBandedGridView.CloseEditor();
		}

		public void UpdateView()
		{
			gridBandWidth.Visible = ScheduleSettings.DigitalProductListViewSettings.ShowDigitalDimensions;
			gridBandHeight.Visible = ScheduleSettings.DigitalProductListViewSettings.ShowDigitalDimensions;
			gridBandRate.Visible = ScheduleSettings.DigitalProductListViewSettings.ShowDigitalStrategy;
			if (ScheduleSettings.DigitalProductListViewSettings.ShowDigitalLocation)
			{
				gridColumnName.RowCount = 1;
				gridColumnLocation.Visible = true;
				advBandedGridView.SetColumnPosition(gridColumnLocation, 1, 0);
			}
			else
			{
				gridColumnLocation.Visible = false;
				gridColumnName.RowCount = 2;
			}

			gridBandOptions.Visible = ScheduleSettings.DigitalProductListViewSettings.ShowDigitalTargeting ||
									  ScheduleSettings.DigitalProductListViewSettings.ShowDigitalRichMedia;
			if (ScheduleSettings.DigitalProductListViewSettings.ShowDigitalTargeting &&
				ScheduleSettings.DigitalProductListViewSettings.ShowDigitalRichMedia)
			{
				gridColumnTarget.Visible = true;
				gridColumnTarget.RowCount = 1;
				advBandedGridView.SetColumnPosition(gridColumnTarget, 0, 0);
				gridColumnRichMedia.Visible = true;
				gridColumnRichMedia.RowCount = 1;
				advBandedGridView.SetColumnPosition(gridColumnRichMedia, 1, 0);
			}
			else if (ScheduleSettings.DigitalProductListViewSettings.ShowDigitalTargeting)
			{
				gridColumnTarget.Visible = true;
				gridColumnTarget.RowCount = 2;
				gridColumnRichMedia.Visible = false;
			}
			else if (ScheduleSettings.DigitalProductListViewSettings.ShowDigitalRichMedia)
			{
				gridColumnTarget.Visible = false;
				gridColumnRichMedia.Visible = true;
				gridColumnRichMedia.RowCount = 2;
			}
		}

		public void RefreshDigitalAfterAddProduct()
		{
			((BindingList<DigitalProduct>)advBandedGridView.DataSource).ResetBindings();
			advBandedGridView.FocusedRowHandle = advBandedGridView.RowCount - 1;
			_onDataChanged();
		}

		public void AddProduct(Category category)
		{
			Content.AddDigital(category.Name);
			RefreshDigitalAfterAddProduct();
		}

		public void DeleteProduct()
		{
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Are you sure you want to delete this line?") != DialogResult.Yes) return;
			advBandedGridView.DeleteSelectedRows();
			Content.RebuildDigitalProductIndexes();
			RefreshDigitalAfterAddProduct();
		}

		public void CloneProduct()
		{
			if (advBandedGridView.FocusedRowHandle == GridControl.InvalidRowHandle) return;
			var newRowHandle = advBandedGridView.FocusedRowHandle + 1;
			((BindingList<DigitalProduct>)advBandedGridView.DataSource)[advBandedGridView.GetDataSourceRowIndex(advBandedGridView.FocusedRowHandle)].Clone<DigitalProduct, DigitalProduct>();
			((BindingList<DigitalProduct>)advBandedGridView.DataSource).ResetBindings();
			advBandedGridView.FocusedRowHandle = newRowHandle;
			_onDataChanged();
		}

		private void CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			var product = advBandedGridView.GetFocusedRow() as DigitalProduct;
			if (product == null) return;

			if (e.Column == gridColumnName ||
				e.Column == gridColumnCategory ||
				e.Column == gridColumnSubCategory)
			{
				var productSource = ListManager.Instance.ProductSources.FirstOrDefault(x => x.Name.Equals(product.Name));
				if (productSource != null)
				{
					product.ApplyDefaultValues();
					advBandedGridView.RefreshData();
				}
			}
			_onDataChanged();
		}

		private void repositoryItemComboBoxProductName_Closed(object sender, ClosedEventArgs e)
		{
			advBandedGridView.CloseEditor();
		}

		private void OnShowingEditor(object sender, CancelEventArgs e)
		{
			e.Cancel = false;
			var digitalProduct = advBandedGridView.GetFocusedRow() as DigitalProduct;
			if (digitalProduct == null) return;
			if (advBandedGridView.FocusedColumn == gridColumnName)
			{
				var category = digitalProduct.Category;
				var subCategories = ListManager.Instance.ProductSources
					.Where(x => x.Category != null && x.Category.Name.Equals(category) && !string.IsNullOrEmpty(x.SubCategory))
					.Select(x => x.SubCategory)
					.Distinct()
					.ToArray();
				var subCategory = digitalProduct.SubCategory;
				if ((subCategories.Any() && !String.IsNullOrEmpty(subCategory)) || !subCategories.Any())
				{
					repositoryItemComboBoxProductName.Items.Clear();
					repositoryItemComboBoxProductName.Items.AddRange(
						ListManager.Instance.ProductSources.
							Where(x => x.Category != null && x.Category.Name.Equals(category) &&
									   (x.SubCategory.Equals(subCategory) || String.IsNullOrEmpty(subCategory))).
							Select(x => x.Name).Distinct().ToArray());
				}
				else
				{
					e.Cancel = true;
					PopupMessageHelper.Instance.ShowWarning("You need to select Digital Inventory Group First");
				}
			}
			else if (advBandedGridView.FocusedColumn == gridColumnSubCategory)
			{
				var category = digitalProduct.Category;
				var subCategories = ListManager.Instance.ProductSources.Where(x => x.Category != null && x.Category.Name.Equals(category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray();
				if (subCategories.Any())
				{
					repositoryItemComboBoxProductType.Items.Clear();
					repositoryItemComboBoxProductType.Items.AddRange(subCategories);
				}
				else
					e.Cancel = true;
			}
			else if (advBandedGridView.FocusedColumn == gridColumnRate)
			{
				e.Cancel = !digitalProduct.EnableRate;
			}
			else if (advBandedGridView.FocusedColumn == gridColumnLocation)
			{
				e.Cancel = !digitalProduct.EnableLocation;
			}
		}

		private void OnCustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			if (!(e.Column == gridColumnSubCategory ||
				e.Column == gridColumnLocation ||
				e.Column == gridColumnRichMedia ||
				e.Column == gridColumnTarget)) return;
			var product = advBandedGridView.GetRow(e.RowHandle) as DigitalProduct;
			if (e.Column == gridColumnSubCategory)
			{
				var availableSubcategories = ListManager.Instance.ProductSources.Where(x => x.Category != null && x.Category.Name.Equals(product.Category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct();
				if (availableSubcategories.Any())
					e.RepositoryItem = repositoryItemComboBoxProductType;
				else
					e.RepositoryItem = repositoryItemTextEditNotAvailable;
			}
			else if (e.Column == gridColumnLocation)
			{
				e.RepositoryItem = product.EnableLocation ? repositoryItemComboBoxLocation : repositoryItemTextEditNotAvailable;
			}
			else if (e.Column == gridColumnTarget)
			{
				e.RepositoryItem = product.EnableTarget && !String.IsNullOrEmpty(product.Name) ? repositoryItemHyperLinkEditTargetEnabled : repositoryItemHyperLinkEditTargetDisabled;
			}
			else if (e.Column == gridColumnRichMedia)
			{
				e.RepositoryItem = product.EnableRichMedia && !String.IsNullOrEmpty(product.Name) ? repositoryItemHyperLinkEditRichMediaEnabled : repositoryItemHyperLinkEditRichMediaDisabled;
			}
		}

		private void OnRowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			var product = (DigitalProduct)advBandedGridView.GetRow(e.RowHandle);
			if (e.Column == gridColumnWidth ||
				e.Column == gridColumnHeight)
			{
				if (e.CellValue == null)
				{
					e.Appearance.ForeColor = Color.Gray;
					e.Appearance.Font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Italic);
				}
				else
				{
					e.Appearance.ForeColor = Color.DodgerBlue;
					e.Appearance.Font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Regular);
				}
			}
			else if (e.Column == gridColumnCategory || e.Column == gridColumnSubCategory)
			{
				var availableSubcategories = ListManager.Instance.ProductSources.Where(x => x.Category != null && x.Category.Name.Equals(product.Category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct();
				if (e.Column == gridColumnCategory)
				{
					if (!availableSubcategories.Any() || !String.IsNullOrEmpty(product.SubCategory))
						e.Appearance.Font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Bold);
				}
				else if (e.Column == gridColumnSubCategory)
				{
					if (e.CellValue == null && availableSubcategories.Any())
					{
						e.Appearance.ForeColor = Color.Gray;
						e.Appearance.Font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Italic);
					}
				}
			}
			else if (e.Column == gridColumnName)
			{
				if (e.CellValue == null)
				{
					e.Appearance.ForeColor = Color.Gray;
					e.Appearance.Font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Italic);
				}
				else
				{
					e.Appearance.ForeColor = Color.DodgerBlue;
					e.Appearance.Font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Bold);
				}
			}
			else if (e.Column == gridColumnLocation && product.EnableLocation)
			{
				if (e.CellValue == null)
				{
					e.Appearance.ForeColor = Color.Gray;
					e.Appearance.Font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Italic);
				}
				else
				{
					e.Appearance.ForeColor = Color.DodgerBlue;
					e.Appearance.Font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Regular);
				}
			}
			else if (e.Column == gridColumnTarget)
			{
				if (String.IsNullOrEmpty(product.Name) || !product.EnableTarget)
				{
					e.Appearance.ForeColor = Color.Gray;
				}
				else
				{
					e.Appearance.ForeColor = Color.Black;
				}
			}
			else if (e.Column == gridColumnRichMedia)
			{
				if (String.IsNullOrEmpty(product.Name) || !product.EnableRichMedia)
				{
					e.Appearance.ForeColor = Color.Gray;
				}
				else
				{
					e.Appearance.ForeColor = Color.Black;
				}
			}
		}

		private void repositoryItemComboBoxProductType_CloseUp(object sender, CloseUpEventArgs e)
		{
			if (e.CloseMode != PopupCloseMode.Normal) return;
			e.AcceptValue = false;
			advBandedGridView.SetFocusedRowCellValue(gridColumnSubCategory, e.Value);
			advBandedGridView.CloseEditor();
		}

		private void DigitalProductsAfterDrop(object sender, DragEventArgs e)
		{
			var grid = (GridControl)sender;
			var view = (GridView)grid.MainView;
			var hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
			var downHitInfo = (BandedGridHitInfo)e.Data.GetData(typeof(BandedGridHitInfo));
			var sourceRow = downHitInfo.RowHandle;
			var targetRow = hitInfo.HitTest == GridHitTest.EmptyRow ? view.DataRowCount : hitInfo.RowHandle;
			view.CloseEditor();
			Content.ChangeDigitalProductPosition(sourceRow, targetRow);
			view.RefreshData();
			_onDataChanged();
		}

		private void repositoryItemHyperLinkEditTarget_OpenLink(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			var digitalProduct = advBandedGridView.GetFocusedRow() as DigitalProduct;
			if (digitalProduct == null) return;
			using (var form = new FormProductInfo(ProductInfoType.Targeting, digitalProduct))
			{
				if (form.ShowDialog() == DialogResult.OK)
					_onDataChanged();
			}
			advBandedGridView.CloseEditor();
		}

		private void repositoryItemHyperLinkEditRichMedia_OpenLink(object sender, OpenLinkEventArgs e)
		{
			e.Handled = true;
			var digitalProduct = advBandedGridView.GetFocusedRow() as DigitalProduct;
			if (digitalProduct == null) return;
			using (var form = new FormProductInfo(ProductInfoType.RichMedia, digitalProduct))
			{
				if (form.ShowDialog() == DialogResult.OK)
					_onDataChanged();
			}
			advBandedGridView.CloseEditor();
		}

		private void toolTipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			if (e.SelectedControl != gridControl) return;
			var view = gridControl.GetViewAt(e.ControlMousePosition) as GridView;
			if (view == null) return;
			var hitInfo = view.CalcHitInfo(e.ControlMousePosition);
			if (!hitInfo.InRowCell) return;
			var product = advBandedGridView.GetRow(hitInfo.RowHandle) as DigitalProduct;
			if (product == null) return;
			if (hitInfo.Column == gridColumnTarget)
			{
				if (String.IsNullOrEmpty(product.Name))
					e.Info = new ToolTipControlInfo(new CellToolTipInfo(hitInfo.RowHandle, hitInfo.Column, "cell"), "Digital Product Required");
				else if (product.EnableTarget)
				{
					var availableInfo = product.AddtionalInfo.Where(pi => pi.Type == ProductInfoType.Targeting && pi.Selected);
					if (availableInfo.Any())
						e.Info = new ToolTipControlInfo(new CellToolTipInfo(hitInfo.RowHandle, hitInfo.Column, "cell"), String.Join(Environment.NewLine, availableInfo.Select(pi => pi.EditValue))) { ToolTipImage = Resources.TargetButton };
					else
						e.Info = new ToolTipControlInfo(new CellToolTipInfo(hitInfo.RowHandle, hitInfo.Column, "cell"), "Click to add Targeting Options");
				}
				else
					e.Info = new ToolTipControlInfo(new CellToolTipInfo(hitInfo.RowHandle, hitInfo.Column, "cell"), "Targeting Options are not available for this product");
				e.Info.ToolTipPosition = MousePosition;
			}
			else if (hitInfo.Column == gridColumnRichMedia)
			{
				if (String.IsNullOrEmpty(product.Name))
					e.Info = new ToolTipControlInfo(new CellToolTipInfo(hitInfo.RowHandle, hitInfo.Column, "cell"), "Digital Product Required");
				else if (product.EnableRichMedia)
				{
					var availableInfo = product.AddtionalInfo.Where(pi => pi.Type == ProductInfoType.RichMedia && pi.Selected);
					if (availableInfo.Any())
						e.Info = new ToolTipControlInfo(new CellToolTipInfo(hitInfo.RowHandle, hitInfo.Column, "cell"), String.Join(Environment.NewLine, availableInfo.Select(pi => pi.EditValue))) { ToolTipImage = Resources.RichMediaButton };
					else
						e.Info = new ToolTipControlInfo(new CellToolTipInfo(hitInfo.RowHandle, hitInfo.Column, "cell"), "Click to add Rich Media Options");
				}
				else
					e.Info = new ToolTipControlInfo(new CellToolTipInfo(hitInfo.RowHandle, hitInfo.Column, "cell"), "Rich Media Options are not available for this product");
				e.Info.ToolTipPosition = MousePosition;
			}
		}

		private void advBandedGridView_MouseMove(object sender, MouseEventArgs e)
		{
			var hitInfo = ((GridView)sender).CalcHitInfo(new Point(e.X, e.Y));
			if (!hitInfo.InRowCell ||
				!(hitInfo.Column.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit)) return;
			var product = (DigitalProduct)advBandedGridView.GetRow(hitInfo.RowHandle);
			if (hitInfo.Column == gridColumnTarget && (String.IsNullOrEmpty(product.Name) || !product.EnableTarget))
			{
				Cursor = Cursors.Default;
				((DXMouseEventArgs)e).Handled = true;
			}
			if (hitInfo.Column == gridColumnRichMedia && (String.IsNullOrEmpty(product.Name) || !product.EnableRichMedia))
			{
				Cursor = Cursors.Default;
				((DXMouseEventArgs)e).Handled = true;
			}
		}
	}
}
