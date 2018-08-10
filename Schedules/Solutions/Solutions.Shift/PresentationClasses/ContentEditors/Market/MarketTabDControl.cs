using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Market;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Market
{
	[ToolboxItem(false)]
	public sealed partial class MarketTabDControl : ChildTabBaseControl
	{
		private MarketTabDInfo CustomTabInfo => (MarketTabDInfo)TabInfo;

		public MarketTabDControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
			clipartEditContainer3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;
			clipartEditContainer4.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart4Image), CustomTabInfo.Clipart4Configuration, TabPageContainer.ParentControl);
			clipartEditContainer4.EditValueChanged += OnEditValueChanged;
			clipartEditContainer5.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart5Image), CustomTabInfo.Clipart5Configuration, TabPageContainer.ParentControl);
			clipartEditContainer5.EditValueChanged += OnEditValueChanged;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);
			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo1Configuration);
			comboBoxEditCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo2Configuration);
			spinEditSubheader1.EnableSelectAll().AssignConfiguration(CustomTabInfo.SubHeader1Configuration);
			memoEditSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader2Configuration);
			spinEditSubheader3.EnableSelectAll().AssignConfiguration(CustomTabInfo.SubHeader3Configuration);
			spinEditSubheader4.EnableSelectAll().AssignConfiguration(CustomTabInfo.SubHeader4Configuration);
			spinEditSubheader5.EnableSelectAll().AssignConfiguration(CustomTabInfo.SubHeader5Configuration);

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

			memoEditSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? memoEditSubheader2.Properties.NullText;

			memoPopupEdit1.Init(SlideContainer.ShiftInfo.TargetCustomersLists.HHIs, CustomTabInfo.MemoPopup1DefaultItem, CustomTabInfo.MemoPopup1Configuration, SlideContainer.StyleConfiguration, SlideContainer.ResourceManager);
			memoPopupEdit1.EditValueChanged += OnEditValueChanged;

			memoPopupEdit2.Init(SlideContainer.ShiftInfo.TargetCustomersLists.Demos, CustomTabInfo.MemoPopup2DefaultItem, CustomTabInfo.MemoPopup2Configuration, SlideContainer.StyleConfiguration, SlideContainer.ResourceManager);
			memoPopupEdit2.EditValueChanged += OnEditValueChanged;

			memoPopupEdit3.Init(SlideContainer.ShiftInfo.TargetCustomersLists.Geographies, CustomTabInfo.MemoPopup3DefaultItem, CustomTabInfo.MemoPopup3Configuration, SlideContainer.StyleConfiguration, SlideContainer.ResourceManager);
			memoPopupEdit3.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.MarketState.TabD.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.MarketState.TabD.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.MarketState.TabD.Clipart3);
			clipartEditContainer4.LoadData(SlideContainer.EditedContent.MarketState.TabD.Clipart4);
			clipartEditContainer5.LoadData(SlideContainer.EditedContent.MarketState.TabD.Clipart5);

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.MarketState.TabD.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
			comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.MarketState.TabD.Combo1 ??
										   CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo2.EditValue = SlideContainer.EditedContent.MarketState.TabD.Combo2 ??
										   CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault);

			memoPopupEdit1.LoadData(SlideContainer.EditedContent.MarketState.TabD.MemoPopup1);
			memoPopupEdit2.LoadData(SlideContainer.EditedContent.MarketState.TabD.MemoPopup2);
			memoPopupEdit3.LoadData(SlideContainer.EditedContent.MarketState.TabD.MemoPopup3);

			spinEditSubheader1.EditValue = SlideContainer.EditedContent.MarketState.TabD.Subheader1 ??
											   CustomTabInfo.SubHeader1DefaultValue;
			memoEditSubheader2.EditValue = SlideContainer.EditedContent.MarketState.TabD.Subheader2 ??
											   CustomTabInfo.SubHeader2DefaultValue;
			spinEditSubheader3.EditValue = SlideContainer.EditedContent.MarketState.TabD.Subheader3 ??
										   CustomTabInfo.SubHeader3DefaultValue;
			spinEditSubheader4.EditValue = SlideContainer.EditedContent.MarketState.TabD.Subheader4 ??
										   CustomTabInfo.SubHeader4DefaultValue;
			spinEditSubheader5.EditValue = SlideContainer.EditedContent.MarketState.TabD.Subheader5 ??
										   CustomTabInfo.SubHeader5DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.MarketState.TabD.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.MarketState.TabD.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.MarketState.TabD.Clipart3 = clipartEditContainer3.GetActiveClipartObject();
			SlideContainer.EditedContent.MarketState.TabD.Clipart4 = clipartEditContainer4.GetActiveClipartObject();
			SlideContainer.EditedContent.MarketState.TabD.Clipart5 = clipartEditContainer5.GetActiveClipartObject();

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.MarketState.TabD.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.MarketState.TabD.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo1.EditValue ?
				comboBoxEditCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.MarketState.TabD.Combo2 = CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo2.EditValue ?
				comboBoxEditCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo2.EditValue as String } :
				null;

			SlideContainer.EditedContent.MarketState.TabD.MemoPopup1 = memoPopupEdit1.GetSelectedItem();
			SlideContainer.EditedContent.MarketState.TabD.MemoPopup2 = memoPopupEdit2.GetSelectedItem();
			SlideContainer.EditedContent.MarketState.TabD.MemoPopup3 = memoPopupEdit3.GetSelectedItem();

			SlideContainer.EditedContent.MarketState.TabD.Subheader1 = (decimal?)spinEditSubheader1.EditValue != CustomTabInfo.SubHeader1DefaultValue ?
				(decimal?)spinEditSubheader1.EditValue :
				null;
			SlideContainer.EditedContent.MarketState.TabD.Subheader2 =
				memoEditSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue
					? memoEditSubheader2.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.MarketState.TabD.Subheader3 = (decimal?)spinEditSubheader3.EditValue != CustomTabInfo.SubHeader3DefaultValue ?
				(decimal?)spinEditSubheader3.EditValue :
				null;
			SlideContainer.EditedContent.MarketState.TabD.Subheader4 = (decimal?)spinEditSubheader4.EditValue != CustomTabInfo.SubHeader4DefaultValue ?
				(decimal?)spinEditSubheader4.EditValue :
				null;
			SlideContainer.EditedContent.MarketState.TabD.Subheader5 = (decimal?)spinEditSubheader5.EditValue != CustomTabInfo.SubHeader5DefaultValue ?
				(decimal?)spinEditSubheader5.EditValue :
				null;

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.MarketState.TabD.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.MarketState.TabD.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftMarketD;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.MarketState.TabD.Clipart1 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
			{
				clipart1.OutputBackground = true;
				outputDataPackage.ClipartItems.Add("SHIFT05DCLIPART1", clipart1);
			}
			var clipart2 = SlideContainer.EditedContent.MarketState.TabA.Clipart2 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
			{
				clipart2.OutputBackground = true;
				outputDataPackage.ClipartItems.Add("SHIFT05DCLIPART2", clipart2);
			}
			var clipart3 = SlideContainer.EditedContent.MarketState.TabA.Clipart3 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
			{
				clipart3.OutputBackground = true;
				outputDataPackage.ClipartItems.Add("SHIFT05DCLIPART3", clipart3);
			}
			var clipart4 = SlideContainer.EditedContent.MarketState.TabD.Clipart4 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart4Image);
			if (clipart4 != null)
				outputDataPackage.ClipartItems.Add("SHIFT05DCLIPART4", clipart4);
			var clipart5 = SlideContainer.EditedContent.MarketState.TabD.Clipart5 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart5Image);
			if (clipart5 != null)
				outputDataPackage.ClipartItems.Add("SHIFT05DCLIPART5", clipart5);

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftMarketFile("020_market_opp_d.pptx");

			outputDataPackage.TextItems.Add("SHIFT05DHEADER".ToUpper(), (SlideContainer.EditedContent.MarketState.TabD.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);

			outputDataPackage.TextItems.Add("SHIFT05DCOMBO1".ToUpper(), (SlideContainer.EditedContent.MarketState.TabD.Combo1 ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT05DCOMBO2".ToUpper(), (SlideContainer.EditedContent.MarketState.TabD.Combo2 ?? CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault))?.Value);

			outputDataPackage.TextItems.Add("SHIFT05DMULTIBOX1".ToUpper(), (SlideContainer.EditedContent.MarketState.TabD.MemoPopup1 ?? CustomTabInfo.MemoPopup1DefaultItem)?.Value);
			outputDataPackage.TextItems.Add("SHIFT05DMULTIBOX2".ToUpper(), (SlideContainer.EditedContent.MarketState.TabD.MemoPopup2 ?? CustomTabInfo.MemoPopup2DefaultItem)?.Value);
			outputDataPackage.TextItems.Add("SHIFT05DMULTIBOX3".ToUpper(), (SlideContainer.EditedContent.MarketState.TabD.MemoPopup3 ?? CustomTabInfo.MemoPopup3DefaultItem)?.Value);

			outputDataPackage.TextItems.Add("SHIFT05DSUBHEADER1".ToUpper(), (SlideContainer.EditedContent.MarketState.TabD.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue ?? 0).ToString("##0'%'"));
			outputDataPackage.TextItems.Add("SHIFT05DSUBHEADER2".ToUpper(), SlideContainer.EditedContent.MarketState.TabD.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT05DSUBHEADER3".ToUpper(), (SlideContainer.EditedContent.MarketState.TabD.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue ?? 0).ToString("##0'%'"));
			outputDataPackage.TextItems.Add("SHIFT05DSUBHEADER4".ToUpper(), (SlideContainer.EditedContent.MarketState.TabD.Subheader4 ?? CustomTabInfo.SubHeader4DefaultValue ?? 0).ToString("##0'%'"));
			outputDataPackage.TextItems.Add("SHIFT05DSUBHEADER5".ToUpper(), (SlideContainer.EditedContent.MarketState.TabD.Subheader5 ?? CustomTabInfo.SubHeader5DefaultValue ?? 0).ToString("##0'%'"));

			return outputDataPackage;
		}
		#endregion
	}
}