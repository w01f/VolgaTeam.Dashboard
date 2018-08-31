using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.IntegratedSolution;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ToolForms;
using Asa.Solutions.Common.PresentationClasses.Output;
using Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.TabA;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution
{
	[ToolboxItem(false)]
	public sealed partial class IntegratedSolutionTabAControl : ChildTabBaseControl
	{
		private ContentsItemControl Contents => xtraTabControl.TabPages.OfType<ContentsItemControl>().Single();
		private readonly XtraTabDragDropHelper<ProductItemControl> _tabDragDropHelper;

		public IntegratedSolutionTabAInfo CustomTabInfo => (IntegratedSolutionTabAInfo)TabInfo;

		public IntegratedSolutionTabAControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			xtraTabControl.TabPages.Add(new ContentsItemControl(this));
			Contents.ItemClicked += OnProductItemClicked;

			xtraTabControl.SelectedPageChanging += OnSelectedTabPageChanging;
			xtraTabControl.CloseButtonClick += OnTabClose;
			xtraTabControl.MouseWheel += OnTabControlMouseWheel;
			_tabDragDropHelper = new XtraTabDragDropHelper<ProductItemControl>(xtraTabControl);
			_tabDragDropHelper.TabMoved += OnTabMoved;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			foreach (var itemControl in xtraTabControl.TabPages.OfType<ProductItemControl>().ToList())
				xtraTabControl.TabPages.Remove(itemControl);

			foreach (var itemState in SlideContainer.EditedContent.IntegratedSolutionState.TabA.Products)
			{
				var itemInfo = CustomTabInfo.Products.FirstOrDefault(item =>
					String.Equals(item.ProductId, itemState.ProductId, StringComparison.OrdinalIgnoreCase));
				if (itemInfo != null)
				{
					var itemControl = new ProductItemControl(itemInfo, itemState, this);
					xtraTabControl.TabPages.Add(itemControl);
				}
			}

			var itemControls = xtraTabControl.TabPages.OfType<ProductItemControl>().ToList();

			itemControls.FirstOrDefault()?.InitControl();

			Contents.UpdateSlideCount(itemControls.Count);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.IntegratedSolutionState.TabA.Products.Clear();
			foreach (var itemControl in xtraTabControl.TabPages.OfType<ProductItemControl>().ToList())
			{
				itemControl.ApplyChanges();
				SlideContainer.EditedContent.IntegratedSolutionState.TabA.Products.Add(itemControl.ItemState);
			}

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.IntegratedSolutionState.TabA.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.IntegratedSolutionState.TabA.EnableOutput =
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

		private void OnTabClose(object sender, EventArgs e)
		{
			var arg = (ClosePageButtonEventArgs)e;
			if (arg.Page is ProductItemControl itemControl)
				xtraTabControl.TabPages.Remove(itemControl);

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

		#region Output
		public override bool ReadyForOutput => xtraTabControl.TabPages.Count > 1;

		//public override SlideType SlideType
		//{
		//	get
		//	{
		//		switch (xtraTabControl.TabPages.Count - 1)
		//		{
		//			case 1:
		//				return SlideType.ShiftIntegratedSolutionA4;
		//			case 2:
		//				return SlideType.ShiftIntegratedSolutionA3;
		//			case 3:
		//				return SlideType.ShiftIntegratedSolutionA2;
		//			case 4:
		//				return SlideType.ShiftIntegratedSolutionA1;
		//			default:
		//				return SlideType.ShiftIntegratedSolutionA4;
		//		}
		//	}
		//}

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			//outputDataPackage.Theme = SelectedTheme;

			//switch (SlideContainer.EditedContent.IntegratedSolutionState.TabA.Items.Count)
			//{
			//	case 1:
			//		outputDataPackage.TemplateName =
			//			MasterWizardManager.Instance.SelectedWizard.GetShiftIntegratedSolutionFile("029_marketing_needs_a4.pptx");
			//		break;
			//	case 2:
			//		outputDataPackage.TemplateName =
			//			MasterWizardManager.Instance.SelectedWizard.GetShiftIntegratedSolutionFile("028_marketing_needs_a3.pptx");
			//		break;
			//	case 3:
			//		outputDataPackage.TemplateName =
			//			MasterWizardManager.Instance.SelectedWizard.GetShiftIntegratedSolutionFile("027_marketing_needs_a2.pptx");
			//		break;
			//	case 4:
			//		outputDataPackage.TemplateName =
			//			MasterWizardManager.Instance.SelectedWizard.GetShiftIntegratedSolutionFile("026_marketing_needs_a1.pptx");
			//		break;
			//	default:
			//		outputDataPackage.TemplateName =
			//			MasterWizardManager.Instance.SelectedWizard.GetShiftIntegratedSolutionFile("029_marketing_needs_a4.pptx");
			//		break;
			//}

			//outputDataPackage.TextItems.Add("SHIFT09AHEADER".ToUpper(), (SlideContainer.EditedContent.IntegratedSolutionState.TabA.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);
			//outputDataPackage.TextItems.Add("SHIFT09ACOMBO1".ToUpper(), (SlideContainer.EditedContent.IntegratedSolutionState.TabA.Combo1 ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value);

			//var itemStateList = SlideContainer.EditedContent.IntegratedSolutionState.TabA.Items.OrderBy(itemState => itemState.Index).ToList();
			//for (var i = 0; i < itemStateList.Count; i++)
			//{
			//	var itemState = SlideContainer.EditedContent.IntegratedSolutionState.TabA.Items[i];
			//	var itemInfo = CustomTabInfo.NeedsList.FirstOrDefault(item =>
			//		String.Equals(item.Id, itemState.Id, StringComparison.OrdinalIgnoreCase));

			//	if (itemInfo == null) continue;

			//	var clipart = ImageClipartObject.FromImage(itemInfo.ClipartImage);
			//	clipart.OutputBackground = true;
			//	outputDataPackage.ClipartItems.Add(String.Format("SHIFT09ATAB{0}CLIPART{0}", i + 1).ToUpper(), clipart);

			//	outputDataPackage.TextItems.Add(String.Format("SHIFT09ATAB{0}SUBHEADER{0}", i + 1).ToUpper(), itemState.Subheader ?? itemInfo.SubHeaderDefaultValue);
			//}

			return outputDataPackage;
		}
		#endregion
	}
}