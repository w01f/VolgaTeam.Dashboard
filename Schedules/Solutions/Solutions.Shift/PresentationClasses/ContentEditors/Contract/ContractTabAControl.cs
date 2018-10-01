using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Contract;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ToolForms;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.PresentationClasses.Output;
using Asa.Solutions.Shift.PresentationClasses.ContentEditors.Contract.TabA;
using DevExpress.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Contract
{
	[ToolboxItem(false)]
	public partial class ContractTabAControl : ChildTabBaseControl
	{
		private XtraTabHitInfo _menuHitInfo;
		private ContentsItemControl Contents => xtraTabControl.TabPages.OfType<ContentsItemControl>().Single();
		private readonly XtraTabDragDropHelper<ProductItemControl> _tabDragDropHelper;

		public ContractTabAInfo CustomTabInfo => (ContractTabAInfo)TabInfo;

		public ContractTabAControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);
			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo1Configuration);
			comboBoxEditCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo2Configuration);

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(CustomTabInfo.HeadersItems
				.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditSlideHeader.Properties.NullText =
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				"Select or type";

			comboBoxEditCombo1.Properties.Items.Clear();
			comboBoxEditCombo1.Properties.Items.AddRange(CustomTabInfo.Combo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo1.Properties.NullText =
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo1.Properties.NullText;

			comboBoxEditCombo2.Properties.Items.Clear();
			comboBoxEditCombo2.Properties.Items.AddRange(CustomTabInfo.Combo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo2.Properties.NullText =
				CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo2.Properties.NullText;

			xtraTabControl.TabPages.Add(new ContentsItemControl(this));
			Contents.ItemClicking += OnProductItemClicking;
			Contents.ItemClicked += OnProductItemClicked;

			xtraTabControl.SelectedPageChanging += OnSelectedTabPageChanging;
			xtraTabControl.SelectedPageChanged += OnSelectedTabPageChanged;
			xtraTabControl.CloseButtonClick += OnTabClose;
			xtraTabControl.MouseDown += OnTabControlMouseDown;
			xtraTabControl.MouseWheel += OnTabControlMouseWheel;
			_tabDragDropHelper = new XtraTabDragDropHelper<ProductItemControl>(xtraTabControl);
			_tabDragDropHelper.TabMoved += OnTabMoved;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ContractState.TabA.SlideHeader ??
												CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.ContractState.TabA.Combo1 ??
										   CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo2.EditValue = SlideContainer.EditedContent.ContractState.TabA.Combo2 ??
										   CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault);

			xtraTabControl.SuspendLayout();

			var selectedItemId = (xtraTabControl.SelectedTabPage as ProductItemControl)?.ItemState?.ItemId;

			foreach (var itemControl in xtraTabControl.TabPages
				.OfType<ProductItemControl>()
				.Where(productControl => SlideContainer.EditedContent.ContractState.TabA.Products.All(product => product.ItemId != productControl.ItemState.ItemId))
				.ToList())
				xtraTabControl.TabPages.Remove(itemControl);

			foreach (var itemState in SlideContainer.EditedContent.ContractState.TabA.Products)
			{
				var itemControl = xtraTabControl.TabPages
					.OfType<ProductItemControl>()
					.FirstOrDefault(productControl => itemState.ItemId == productControl.ItemState.ItemId);
				if (itemControl == null)
				{
					var itemInfo = CustomTabInfo.Products.FirstOrDefault(item =>
						String.Equals(item.ProductId, itemState.ProductId, StringComparison.OrdinalIgnoreCase));
					if (itemInfo != null)
					{
						itemControl = new ProductItemControl(itemInfo, itemState, this);
						xtraTabControl.TabPages.Add(itemControl);
					}
				}
			}

			if (selectedItemId != null)
				xtraTabControl.SelectedTabPage = xtraTabControl.TabPages
													 .OfType<ProductItemControl>()
													 .FirstOrDefault(productTabControl =>
														 productTabControl.ItemState.ItemId == selectedItemId)
												 ?? xtraTabControl.SelectedTabPage;

			xtraTabControl.ResumeLayout();

			var itemControls = xtraTabControl.TabPages.OfType<ProductItemControl>().ToList();

			itemControls.FirstOrDefault()?.InitControl();

			UpdateSlideCount(itemControls.Count);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.ContractState.TabA.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.ContractState.TabA.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo1.EditValue ?
				comboBoxEditCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.ContractState.TabA.Combo2 = CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo2.EditValue ?
				comboBoxEditCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo2.EditValue as String } :
				null;

			SlideContainer.EditedContent.ContractState.TabA.Products.Clear();
			foreach (var itemControl in xtraTabControl.TabPages.OfType<ProductItemControl>().ToList())
			{
				itemControl.ApplyChanges();
				SlideContainer.EditedContent.ContractState.TabA.Products.Add(itemControl.ItemState);
			}

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.ContractState.TabA.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.ContractState.TabA.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		public void UpdateSlideCount(int slideCount)
		{
			simpleLabelItemItemsCount.Text = slideCount > 0 ?
				String.Format("<size=+2><color=gray>{0} ({1})</color></size>", CustomTabInfo.TabSelector.ContentsTabName, slideCount) :
				" ";
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		private void OnProductItemClicking(object sender, ProductClickingEventArgs e)
		{
			if (xtraTabControl.TabPages.OfType<ProductItemControl>().Count() >= CustomTabInfo.TabSelector.MaxSelectedItems)
			{
				e.Cancel = true;

				var maxCheckedItemsWord = String.Empty;
				switch (CustomTabInfo.TabSelector.MaxSelectedItems)
				{
					case 1:
						maxCheckedItemsWord = "one";
						break;
					case 2:
						maxCheckedItemsWord = "two";
						break;
					case 3:
						maxCheckedItemsWord = "three";
						break;
					case 4:
						maxCheckedItemsWord = "four";
						break;
					case 5:
						maxCheckedItemsWord = "five";
						break;
					case 6:
						maxCheckedItemsWord = "six";
						break;
					case 7:
						maxCheckedItemsWord = "seven";
						break;
					case 8:
						maxCheckedItemsWord = "eight";
						break;
					case 9:
						maxCheckedItemsWord = "nine";
						break;
					case 10:
						maxCheckedItemsWord = "ten";
						break;
				}

				PopupMessageHelper.Instance.ShowWarning(String.Format("Only {1} ({2}) {0} are allowed.{3}{3}If you want to add another item, first remove one…",
					CustomTabInfo.TabSelector.ContentsTabName,
					CustomTabInfo.TabSelector.MaxSelectedItems,
					maxCheckedItemsWord,
					Environment.NewLine));
			}
		}

		private void OnProductItemClicked(object sender, ProductClickedEventArgs e)
		{
			if (!_allowToSave) return;

			var itemInfo = e.ItemInfo;

			var itemControl = new ProductItemControl(itemInfo,
				new ContractState.ProductItemState(itemInfo.ProductId),
				this);
			xtraTabControl.TabPages.Add(itemControl);
			itemControl.InitControl();

			UpdateSlideCount(xtraTabControl.TabPages.OfType<ProductItemControl>().Count());

			SlideContainer.RaiseSlideTypeChanged();
			SlideContainer.RaiseOutputStatuesChanged();
			RaiseEditValueChanged();
		}

		private void OnSelectedTabPageChanging(object sender, TabPageChangingEventArgs e)
		{
			if (!(e.Page is ProductItemControl itemControl) || itemControl.Initialized) return;

			FormProgress.ShowProgress("Loading data...", () =>
			{
				itemControl.InitControl();
			});
		}

		private void OnSelectedTabPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (!_allowToSave) return;
			SlideContainer.RaiseSlideTypeChanged();
		}

		private void OnTabClose(object sender, EventArgs e)
		{
			var arg = (ClosePageButtonEventArgs)e;
			if (!(arg.Page is ProductItemControl itemControl)) return;

			if (PopupMessageHelper.Instance.ShowWarningQuestion(String.Format("Delete {0}?", itemControl.ItemInfo.Title)) != DialogResult.Yes)
				return;

			xtraTabControl.TabPages.Remove(itemControl);

			UpdateSlideCount(xtraTabControl.TabPages.OfType<ProductItemControl>().Count());

			SlideContainer.RaiseSlideTypeChanged();
			RaiseEditValueChanged();
		}

		private void OnTabControlMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			_menuHitInfo = xtraTabControl.CalcHitInfo(new Point(e.X, e.Y));
			if (!(_menuHitInfo.Page is ProductItemControl &&
				_menuHitInfo.HitTest == XtraTabHitTest.PageHeader)) return;
			contextMenuStrip.Show((Control)sender, e.Location);
		}

		private void OnMenuItemCloneClick(object sender, EventArgs e)
		{
			if (!(_menuHitInfo.Page is ProductItemControl productControl)) return;

			var itemControl = new ProductItemControl(productControl.ItemInfo,
				productControl.ItemState.Clone(),
				this);
			xtraTabControl.TabPages.Insert(xtraTabControl.TabPages.IndexOf(_menuHitInfo.Page), itemControl);
			itemControl.InitControl();

			UpdateSlideCount(xtraTabControl.TabPages.OfType<ProductItemControl>().Count());

			SlideContainer.RaiseSlideTypeChanged();
			RaiseEditValueChanged();
		}

		private void OnTabMoved(object sender, TabMoveEventArgs e)
		{
			RaiseEditValueChanged();
		}

		private void OnTabControlMouseWheel(object sender, MouseEventArgs e)
		{
			var point = new Point(e.X, e.Y);
			var hitInfo = xtraTabControl.CalcHitInfo(point);
			if (hitInfo?.Page == null)
				return;

			var currentPageIndex = xtraTabControl.TabPages.IndexOf(hitInfo.Page);

			if (e.Delta < 0 && currentPageIndex < xtraTabControl.TabPages.Count - 1)
			{
				var nextPageIndex = currentPageIndex + 1;
				var moveToPageIndex = nextPageIndex;
				do
				{
					xtraTabControl.MakePageVisible(xtraTabControl.TabPages[moveToPageIndex]);
					hitInfo = xtraTabControl.CalcHitInfo(point);
					if (hitInfo?.Page == null)
						break;
					currentPageIndex = xtraTabControl.TabPages.IndexOf(hitInfo.Page);
					moveToPageIndex++;

				} while (nextPageIndex > currentPageIndex && moveToPageIndex <= xtraTabControl.TabPages.Count - 1);
			}
			else if (currentPageIndex > 0)
			{
				var nextPageIndex = currentPageIndex - 1;
				var moveToPageIndex = nextPageIndex;
				do
				{
					xtraTabControl.MakePageVisible(xtraTabControl.TabPages[moveToPageIndex]);
					hitInfo = xtraTabControl.CalcHitInfo(point);
					if (hitInfo?.Page == null)
						break;
					currentPageIndex = xtraTabControl.TabPages.IndexOf(hitInfo.Page);
					moveToPageIndex--;

				} while (nextPageIndex < currentPageIndex && moveToPageIndex >= 0);
			}
		}

		private void OnToolTipControllerGetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			if (e.SelectedControl == xtraTabControl && String.Equals(e.Info?.Text, "Close", StringComparison.OrdinalIgnoreCase))
				e.Info.Text = "Delete";
		}

		#region Output
		public override bool ReadyForOutput => xtraTabControl.TabPages.OfType<ProductItemControl>().Any(item => item.ReadyForOutput);

		public override SlideType SlideType => SlideType.ShiftContract;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			var outputProductItems = SlideContainer.EditedContent.ContractState.TabA.Products;

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftContractFile(String.Format("a_agreement{0}.pptx", outputProductItems.Count));

			outputDataPackage.TextItems.Add("SHIFT15AHEADER".ToUpper(), (SlideContainer.EditedContent.ContractState.TabA.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);

			var subheader1 = String.Format("Investment: ${0} (net) monthly for {1} months",
				(SlideContainer.EditedContent.ContractState.TabA.Combo1 ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value,
				(SlideContainer.EditedContent.ContractState.TabA.Combo2 ?? CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT15ASUBHEADER1".ToUpper(), subheader1);

			for (var i = 0; i < outputProductItems.Count; i++)
			{
				var productInfo = CustomTabInfo.Products.FirstOrDefault(item =>
					String.Equals(item.ProductId, outputProductItems[i].ProductId, StringComparison.OrdinalIgnoreCase));

				outputDataPackage.TableItems.Add(String.Format("tactic{0}", i + 1), productInfo.Title);
				outputDataPackage.TableItems.Add(String.Format("descrip{0}", i + 1),
					(outputProductItems[i].MemoPopup1 ?? productInfo?.MemoPopup1Items.FirstOrDefault(h => h.IsDefault))?.Value);
				outputDataPackage.TableItems.Add(String.Format("size{0}", i + 1),
					(outputProductItems[i].Combo1 ?? productInfo?.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value);
				outputDataPackage.TableItems.Add(String.Format("qty{0}", i + 1),
					(outputProductItems[i].Combo2 ?? productInfo?.Combo2Items.FirstOrDefault(h => h.IsDefault))?.Value);
				outputDataPackage.TableItems.Add(String.Format("inv{0}", i + 1),
					(outputProductItems[i].Combo3 ?? productInfo?.Combo3Items.FirstOrDefault(h => h.IsDefault))?.Value);
			}

			return outputDataPackage;
		}
		#endregion
	}
}