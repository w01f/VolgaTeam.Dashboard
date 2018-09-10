using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.NeedsSolutions;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;
using Asa.Solutions.Shift.PresentationClasses.ContentEditors.NeedsSolutions.TabA;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.NeedsSolutions
{
	[ToolboxItem(false)]
	public sealed partial class NeedsSolutionsTabAControl : ChildTabBaseControl
	{
		private ContentsItemControl Contents => xtraTabControl.TabPages.OfType<ContentsItemControl>().Single();
		private readonly XtraTabDragDropHelper<ItemControl> _tabDragDropHelper;

		public NeedsSolutionsTabAInfo CustomTabInfo => (NeedsSolutionsTabAInfo)TabInfo;

		public NeedsSolutionsTabAControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			xtraTabControl.TabPages.Add(new ContentsItemControl(this));
			Contents.ItemStateChanged += OnItemStateChanged;

			_tabDragDropHelper = new XtraTabDragDropHelper<ItemControl>(xtraTabControl);
			_tabDragDropHelper.TabMoved += OnTabMoved;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			Contents.LoadData();

			foreach (var itemControl in xtraTabControl.TabPages.OfType<ItemControl>().ToList())
				xtraTabControl.TabPages.Remove(itemControl);

			foreach (var itemState in SlideContainer.EditedContent.NeedsSolutionsState.TabA.Items.OrderBy(item => item.Index))
			{
				var itemInfo = CustomTabInfo.NeedsList.FirstOrDefault(item =>
					String.Equals(item.Id, itemState.Id, StringComparison.OrdinalIgnoreCase));
				if (itemInfo != null)
				{
					var itemControl = new ItemControl(itemInfo, this);
					xtraTabControl.TabPages.Add(itemControl);
					itemControl.LoadData();
				}
			}

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			Contents.ApplyChanges();

			SlideContainer.EditedContent.NeedsSolutionsState.TabA.Items.Clear();
			foreach (var itemControl in xtraTabControl.TabPages.OfType<ItemControl>().ToList())
				itemControl.ApplyChanges();

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.NeedsSolutionsState.TabA.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.NeedsSolutionsState.TabA.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnItemStateChanged(Object sender, ItemChangedEventArgs e)
		{
			if (!_allowToSave) return;

			var itemInfo = e.ItemInfo;

			if (e.Checked)
			{
				var itemControl = new ItemControl(itemInfo, this);
				xtraTabControl.TabPages.Add(itemControl);
				itemControl.LoadData();
			}
			else
			{
				var itemControl = xtraTabControl.TabPages
					.OfType<ItemControl>()
					.FirstOrDefault(control =>
						String.Equals(control.ItemInfo.Id, itemInfo.Id, StringComparison.OrdinalIgnoreCase));
				if (itemControl != null)
				{
					SlideContainer.EditedContent.NeedsSolutionsState.TabA.Items.RemoveAll(item =>
						String.Equals(item.Id, itemControl.ItemInfo.Id, StringComparison.OrdinalIgnoreCase));
					xtraTabControl.TabPages.Remove(itemControl);
				}
			}

			SlideContainer.RaiseSlideTypeChanged();
			RaiseEditValueChanged();
		}

		private void OnTabMoved(object sender, TabMoveEventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override bool ReadyForOutput => xtraTabControl.TabPages.Count > 1;

		public override SlideType SlideType
		{
			get
			{
				switch (xtraTabControl.TabPages.Count - 1)
				{
					case 1:
						return SlideType.ShiftNeedsSolutionsA4;
					case 2:
						return SlideType.ShiftNeedsSolutionsA3;
					case 3:
						return SlideType.ShiftNeedsSolutionsA2;
					case 4:
						return SlideType.ShiftNeedsSolutionsA1;
					default:
						return SlideType.ShiftNeedsSolutionsA4;
				}
			}
		}

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			switch (SlideContainer.EditedContent.NeedsSolutionsState.TabA.Items.Count)
			{
				case 1:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftNeedsSolutionsFile("029_marketing_needs_a4.pptx");
					break;
				case 2:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftNeedsSolutionsFile("028_marketing_needs_a3.pptx");
					break;
				case 3:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftNeedsSolutionsFile("027_marketing_needs_a2.pptx");
					break;
				case 4:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftNeedsSolutionsFile("026_marketing_needs_a1.pptx");
					break;
				default:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftNeedsSolutionsFile("029_marketing_needs_a4.pptx");
					break;
			}

			outputDataPackage.TextItems.Add("SHIFT07AHEADER".ToUpper(), (SlideContainer.EditedContent.NeedsSolutionsState.TabA.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT07ACOMBO1".ToUpper(), (SlideContainer.EditedContent.NeedsSolutionsState.TabA.Combo1 ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value);

			var itemStateList = SlideContainer.EditedContent.NeedsSolutionsState.TabA.Items.OrderBy(itemState => itemState.Index).ToList();
			for (var i = 0; i < itemStateList.Count; i++)
			{
				var itemState = SlideContainer.EditedContent.NeedsSolutionsState.TabA.Items[i];
				var itemInfo = CustomTabInfo.NeedsList.FirstOrDefault(item =>
					String.Equals(item.Id, itemState.Id, StringComparison.OrdinalIgnoreCase));

				if (itemInfo == null) continue;

				var clipart = ImageClipartObject.FromFile(itemInfo.ImagePath);
				clipart.OutputBackground = true;
				outputDataPackage.ClipartItems.Add(String.Format("SHIFT07ATAB{0}CLIPART{0}", i + 1).ToUpper(), clipart);

				outputDataPackage.TextItems.Add(String.Format("SHIFT07ATAB{0}SUBHEADER{0}", i + 1).ToUpper(), itemState.Subheader ?? itemInfo.SubHeaderDefaultValue);
			}

			return outputDataPackage;
		}
		#endregion
	}
}