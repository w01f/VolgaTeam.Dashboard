using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Approach;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;
using Asa.Solutions.Shift.PresentationClasses.ContentEditors.Approach.TabA;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Approach
{
	[ToolboxItem(false)]
	public sealed partial class ApproachTabAControl : ChildTabBaseControl
	{
		private ContentsItemControl Contents => xtraTabControl.TabPages.OfType<ContentsItemControl>().Single();
		private readonly XtraTabDragDropHelper<ItemControl> _tabDragDropHelper;

		public ApproachTabAInfo CustomTabInfo => (ApproachTabAInfo)TabInfo;

		public ApproachTabAControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
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

			foreach (var itemState in SlideContainer.EditedContent.ApproachState.TabA.Items.OrderBy(item => item.Index))
			{
				var itemInfo = CustomTabInfo.ApproachItems.FirstOrDefault(item =>
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

			SlideContainer.EditedContent.ApproachState.TabA.Items.Clear();
			foreach (var itemControl in xtraTabControl.TabPages.OfType<ItemControl>().ToList())
				itemControl.ApplyChanges();

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.ApproachState.TabA.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.ApproachState.TabA.EnableOutput =
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
					SlideContainer.EditedContent.ApproachState.TabA.Items.RemoveAll(item =>
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

		public override SlideType SlideType => SlideType.ShiftApproachA;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			switch (SlideContainer.EditedContent.ApproachState.TabA.Items.Count)
			{
				case 1:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftApproachFile("058_our_approach_a4.pptx");
					break;
				case 2:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftApproachFile("057_our_approach_a3.pptx");
					break;
				case 3:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftApproachFile("056_our_approach_a2.pptx");
					break;
				case 4:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftApproachFile("055_our_approach_a1.pptx");
					break;
				default:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftApproachFile("058_our_approach_a4.pptx");
					break;
			}

			outputDataPackage.TextItems.Add("SHIFT09AHEADER".ToUpper(), (SlideContainer.EditedContent.ApproachState.TabA.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT09ACOMBO1".ToUpper(), (SlideContainer.EditedContent.ApproachState.TabA.Combo1 ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value);

			var itemStateList = SlideContainer.EditedContent.ApproachState.TabA.Items.OrderBy(itemState => itemState.Index).ToList();
			for (var i = 0; i < itemStateList.Count; i++)
			{
				var itemState = SlideContainer.EditedContent.ApproachState.TabA.Items[i];
				var itemInfo = CustomTabInfo.ApproachItems.FirstOrDefault(item =>
					String.Equals(item.Id, itemState.Id, StringComparison.OrdinalIgnoreCase));

				if (itemInfo == null) continue;

				var clipart = ImageClipartObject.FromImage(itemInfo.ClipartImage);
				clipart.OutputBackground = true;
				outputDataPackage.ClipartItems.Add(String.Format("SHIFT09ATAB{0}CLIPART{0}", i + 1).ToUpper(), clipart);

				outputDataPackage.TextItems.Add(String.Format("SHIFT09ATAB{0}SUBHEADER{0}", i + 1).ToUpper(), itemState.Subheader ?? itemInfo.SubHeaderDefaultValue);
			}

			return outputDataPackage;
		}
		#endregion
	}
}