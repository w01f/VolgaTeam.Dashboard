using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.CBC;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.CBC
{
	[ToolboxItem(false)]
	public sealed partial class CBCTabFControl : ChildTabBaseControl
	{
		private CBCTabFInfo CustomTabInfo => (CBCTabFInfo)TabInfo;

		public CBCTabFControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);
			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo1Configuration);
			memoEditSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader1Configuration);
			memoEditSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader2Configuration);
			memoEditSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader3Configuration);
			memoEditSubheader4.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader4Configuration);
			memoEditSubheader5.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader5Configuration);

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

			memoEditSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditSubheader1.Properties.NullText;
			memoEditSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? memoEditSubheader2.Properties.NullText;
			memoEditSubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? memoEditSubheader3.Properties.NullText;
			memoEditSubheader4.Properties.NullText = CustomTabInfo.SubHeader4Placeholder ?? memoEditSubheader4.Properties.NullText;
			memoEditSubheader5.Properties.NullText = CustomTabInfo.SubHeader5Placeholder ?? memoEditSubheader5.Properties.NullText;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.CBCState.TabF.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.CBCState.TabF.Clipart2);

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CBCState.TabF.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
			comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.CBCState.TabF.Combo1 ??
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);

			memoEditSubheader1.EditValue = SlideContainer.EditedContent.CBCState.TabF.Subheader1 ??
										   CustomTabInfo.SubHeader1DefaultValue;
			memoEditSubheader2.EditValue = SlideContainer.EditedContent.CBCState.TabF.Subheader2 ??
										   CustomTabInfo.SubHeader2DefaultValue;
			memoEditSubheader3.EditValue = SlideContainer.EditedContent.CBCState.TabF.Subheader3 ??
										   CustomTabInfo.SubHeader3DefaultValue;
			memoEditSubheader4.EditValue = SlideContainer.EditedContent.CBCState.TabF.Subheader4 ??
										   CustomTabInfo.SubHeader4DefaultValue;
			memoEditSubheader5.EditValue = SlideContainer.EditedContent.CBCState.TabF.Subheader5 ??
										   CustomTabInfo.SubHeader5DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.CBCState.TabF.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.CBCState.TabF.Clipart2 = clipartEditContainer2.GetActiveClipartObject();

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.CBCState.TabF.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.CBCState.TabF.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo1.EditValue ?
				comboBoxEditCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo1.EditValue as String } :
				null;

			SlideContainer.EditedContent.CBCState.TabF.Subheader1 =
				memoEditSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue
					? memoEditSubheader1.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.CBCState.TabF.Subheader2 =
				memoEditSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue
					? memoEditSubheader2.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.CBCState.TabF.Subheader3 =
				memoEditSubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue
					? memoEditSubheader3.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.CBCState.TabF.Subheader4 =
				memoEditSubheader4.EditValue as String != CustomTabInfo.SubHeader4DefaultValue
					? memoEditSubheader4.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.CBCState.TabF.Subheader5 =
				memoEditSubheader5.EditValue as String != CustomTabInfo.SubHeader5DefaultValue
					? memoEditSubheader5.EditValue as String ?? String.Empty
					: null;

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.CBCState.TabF.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.CBCState.TabF.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftCBC_F;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.CBCState.TabF.Clipart1 ??
						  ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("SHIFT08FCLIPART1", clipart1);
			var clipart2 = SlideContainer.EditedContent.CBCState.TabF.Clipart2 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("SHIFT08FCLIPART2", clipart2);

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftCBCFile("054a_cbc_west_palm.pptx");

			outputDataPackage.TextItems.Add("SHIFT08FHEADER".ToUpper(), (SlideContainer.EditedContent.CBCState.TabF.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT08FCOMBO1".ToUpper(), (SlideContainer.EditedContent.CBCState.TabF.Combo1 ?? CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault && !item.IsPlaceholder))?.Value);

			var subheaders = new[]
				{
					SlideContainer.EditedContent.CBCState.TabF.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue,
					SlideContainer.EditedContent.CBCState.TabF.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue,
					SlideContainer.EditedContent.CBCState.TabF.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue,
					SlideContainer.EditedContent.CBCState.TabF.Subheader4 ?? CustomTabInfo.SubHeader4DefaultValue,
					SlideContainer.EditedContent.CBCState.TabF.Subheader5 ?? CustomTabInfo.SubHeader5DefaultValue
				}
				.Where(item => !String.IsNullOrWhiteSpace(item))
				.ToList();

			for (int i = 0; i < 5; i++)
				outputDataPackage.TextItems.Add(String.Format("SHIFT08FSUBHEADER{0}", i + 1).ToUpper(), subheaders.ElementAtOrDefault(i));

			return outputDataPackage;
		}
		#endregion
	}
}