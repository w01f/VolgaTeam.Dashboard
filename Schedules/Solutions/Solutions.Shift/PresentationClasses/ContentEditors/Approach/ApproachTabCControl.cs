using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Approach;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.PresentationClasses.Output;
using Asa.Solutions.Shift.PresentationClasses.ContentEditors.Approach.TabC;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Approach
{
	[ToolboxItem(false)]
	public sealed partial class ApproachTabCControl : ChildTabBaseControl
	{
		private readonly Item1Control _item1Control;
		private readonly Item2Control _item2Control;
		private readonly Item3Control _item3Control;
		private readonly Item4Control _item4Control;

		private readonly XtraTabDragDropHelper<ItemControl> _tabDragDropHelper;

		public ApproachTabCInfo CustomTabInfo => (ApproachTabCInfo)TabInfo;

		public ApproachTabCControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);
			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo1Configuration);

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

			_item1Control = new Item1Control();
			_item1Control.Init(
				CustomTabInfo.ApproachItems,
				CustomTabInfo.Tab1DefaultName,
				CustomTabInfo.SubHeader1Configuration,
				SlideContainer.StyleConfiguration,
				CustomTabInfo.FormListConfiguration);
			_item1Control.EditValueChanged += OnEditValueChanged;

			_item2Control = new Item2Control();
			_item2Control.Init(
				CustomTabInfo.ApproachItems,
				CustomTabInfo.Tab2DefaultName,
				CustomTabInfo.SubHeader2Configuration,
				SlideContainer.StyleConfiguration,
				CustomTabInfo.FormListConfiguration);
			_item2Control.EditValueChanged += OnEditValueChanged;

			_item3Control = new Item3Control();
			_item3Control.Init(
				CustomTabInfo.ApproachItems,
				CustomTabInfo.Tab3DefaultName,
				CustomTabInfo.SubHeader3Configuration,
				SlideContainer.StyleConfiguration,
				CustomTabInfo.FormListConfiguration);
			_item3Control.EditValueChanged += OnEditValueChanged;

			_item4Control = new Item4Control();
			_item4Control.Init(
				CustomTabInfo.ApproachItems,
				CustomTabInfo.Tab4DefaultName,
				CustomTabInfo.SubHeader4Configuration,
				SlideContainer.StyleConfiguration,
				CustomTabInfo.FormListConfiguration);
			_item4Control.EditValueChanged += OnEditValueChanged;

			_tabDragDropHelper = new XtraTabDragDropHelper<ItemControl>(xtraTabControl);
			_tabDragDropHelper.TabMoved += OnTabMoved;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ApproachState.TabC.SlideHeader ??
												CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.ApproachState.TabC.Combo1 ??
										   CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);

			_item1Control.LoadData(SlideContainer.EditedContent.ApproachState.TabC.ItemState1 ??
					ApproachState.ApproachItemState.FromItemInfo(CustomTabInfo.Tab1DefaultItem));
			_item2Control.LoadData(SlideContainer.EditedContent.ApproachState.TabC.ItemState2 ??
						  ApproachState.ApproachItemState.FromItemInfo(CustomTabInfo.Tab2DefaultItem));
			_item3Control.LoadData(SlideContainer.EditedContent.ApproachState.TabC.ItemState3 ??
						  ApproachState.ApproachItemState.FromItemInfo(CustomTabInfo.Tab3DefaultItem));
			_item4Control.LoadData(SlideContainer.EditedContent.ApproachState.TabC.ItemState4 ??
						  ApproachState.ApproachItemState.FromItemInfo(CustomTabInfo.Tab4DefaultItem));

			xtraTabControl.TabPages.Clear();
			xtraTabControl.TabPages.AddRange(
				new ItemControl[] { _item1Control, _item2Control, _item3Control, _item4Control }
					.OrderBy(item => item.ItemState.Index)
					.ToArray()
			);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.ApproachState.TabC.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.ApproachState.TabC.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo1.EditValue ?
				comboBoxEditCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo1.EditValue as String } :
				null;

			foreach (var itemControl in xtraTabControl.TabPages.OfType<ItemControl>().ToList())
				itemControl.ItemState.Index = xtraTabControl.TabPages.IndexOf(itemControl);

			SlideContainer.EditedContent.ApproachState.TabC.ItemState1 = _item1Control.ItemState;
			SlideContainer.EditedContent.ApproachState.TabC.ItemState2 = _item2Control.ItemState;
			SlideContainer.EditedContent.ApproachState.TabC.ItemState3 = _item3Control.ItemState;
			SlideContainer.EditedContent.ApproachState.TabC.ItemState4 = _item4Control.ItemState;

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.ApproachState.TabC.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(bool outputEnabled)
		{
			SlideContainer.EditedContent.ApproachState.TabC.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
			SlideContainer.RaiseSlideTypeChanged();
		}

		private void OnTabMoved(object sender, TabMoveEventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override bool ReadyForOutput => new[]
			{
				_item1Control.ItemState,
				_item2Control.ItemState,
				_item3Control.ItemState,
				_item4Control.ItemState,
			}
			.Any(item => item != null && !item.IsEmpty());

		public override SlideType SlideType => SlideType.ShiftApproachC;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			var activeItems = new[]
				{
					_item1Control.ItemState,
					_item2Control.ItemState,
					_item3Control.ItemState,
					_item4Control.ItemState,
				}.Where(item => item != null && !item.IsEmpty())
				.OrderBy(item => item.Index)
				.ToList();

			switch (activeItems.Count)
			{
				case 1:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftApproachFile("066_alacarte_approach_c4.pptx");
					break;
				case 2:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftApproachFile("065_alacarte_approach_c3.pptx");
					break;
				case 3:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftApproachFile("064_alacarte_approach_c2.pptx");
					break;
				case 4:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftApproachFile("063_alacarte_approach_c1.pptx");
					break;
				default:
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetShiftApproachFile("066_alacarte_approach_c4.pptx");
					break;
			}

			outputDataPackage.TextItems.Add("SHIFT09CHEADER".ToUpper(), (SlideContainer.EditedContent.ApproachState.TabC.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT09CCOMBO1".ToUpper(), (SlideContainer.EditedContent.ApproachState.TabC.Combo1 ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value);

			for (var i = 0; i < activeItems.Count; i++)
			{
				var clipart = activeItems[i].Clipart;
				clipart.OutputBackground = true;
				outputDataPackage.ClipartItems.Add(String.Format("SHIFT09CTAB{0}CLIPART{0}", i + 1).ToUpper(), clipart);

				outputDataPackage.TextItems.Add(String.Format("SHIFT09CTAB{0}SUBHEADER{0}", i + 1).ToUpper(), activeItems[i].Subheader);
			}

			return outputDataPackage;
		}
		#endregion
	}
}