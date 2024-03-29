﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.IntegratedSolution;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.SubTab;
using DevExpress.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution
{
	[ToolboxItem(false)]
	public abstract partial class IntegratedSolutionSubTabControl : ChildTabBaseControl
	{
		private XtraTabHitInfo _menuHitInfo;
		private ContentsItemControl Contents => xtraTabControl.TabPages.OfType<ContentsItemControl>().Single();
		private readonly XtraTabDragDropHelper<ProductItemControl> _tabDragDropHelper;

		protected abstract IntegratedSolutionState.SubTabState TabState { get; }
		public IntegratedSolutionSubTabInfo CustomTabInfo => (IntegratedSolutionSubTabInfo)TabInfo;

		private ProductItemControl SelectedProduct => xtraTabControl.SelectedTabPage as ProductItemControl;

		protected IntegratedSolutionSubTabControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			xtraTabControl.TabPages.Add(new ContentsItemControl(this));
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

			xtraTabControl.SuspendLayout();

			var selectedItemId = (xtraTabControl.SelectedTabPage as ProductItemControl)?.ItemState?.ItemId;

			foreach (var itemControl in xtraTabControl.TabPages
				.OfType<ProductItemControl>()
				.Where(productControl => TabState.Products.All(product => product.ItemId != productControl.ItemState.ItemId))
				.ToList())
				xtraTabControl.TabPages.Remove(itemControl);

			foreach (var itemState in TabState.Products)
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

			Contents.UpdateSlideCount(itemControls.Count);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			TabState.Products.Clear();
			foreach (var itemControl in xtraTabControl.TabPages.OfType<ProductItemControl>().ToList())
			{
				itemControl.ApplyChanges();
				TabState.Products.Add(itemControl.ItemState);
			}

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return TabState.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			TabState.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnProductItemClicked(Object sender, ProductClickedEventArgs e)
		{
			if (!_allowToSave) return;

			var itemInfo = e.ItemInfo;

			var itemControl = new ProductItemControl(itemInfo,
				new IntegratedSolutionState.ProductItemState(itemInfo.ProductId),
				this);
			xtraTabControl.TabPages.Add(itemControl);
			itemControl.InitControl();

			Contents.UpdateSlideCount(xtraTabControl.TabPages.OfType<ProductItemControl>().Count());

			SlideContainer.RaiseSlideTypeChanged();
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

			Contents.UpdateSlideCount(xtraTabControl.TabPages.OfType<ProductItemControl>().Count());

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

			Contents.UpdateSlideCount(xtraTabControl.TabPages.OfType<ProductItemControl>().Count());

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

		public override SlideType SlideType => SelectedProduct?.SlideType ?? SlideType.CustomSlide;

		public override IList<OutputItem> GetOutputItems()
		{
			return xtraTabControl.TabPages
				.OfType<ProductItemControl>()
				.Where(item => item.ReadyForOutput)
				.Select(item => item.GetOutputItem())
				.Where(item => item != null)
				.ToList();
		}
		#endregion
	}
}