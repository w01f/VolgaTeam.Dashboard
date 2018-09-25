using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.NextSteps;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.NextSteps
{
	[ToolboxItem(false)]
	public sealed partial class NextStepsTabCControl : ChildTabBaseControl
	{
		private NextStepsTabCInfo CustomTabInfo => (NextStepsTabCInfo)TabInfo;

		public NextStepsTabCControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);
			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo1Configuration);
			comboBoxEditCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo2Configuration);
			comboBoxEditCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo3Configuration);
			comboBoxEditCombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo4Configuration);
			memoEditSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader1Configuration);
			memoEditSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader2Configuration);
			memoEditSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader3Configuration);
			textEditSubheader4.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader4Configuration);

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

			comboBoxEditCombo3.Properties.Items.Clear();
			comboBoxEditCombo3.Properties.Items.AddRange(CustomTabInfo.Combo3Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo3.Properties.NullText =
				CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo3.Properties.NullText;

			comboBoxEditCombo4.Properties.Items.Clear();
			comboBoxEditCombo4.Properties.Items.AddRange(CustomTabInfo.Combo4Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo4.Properties.NullText =
				CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo4.Properties.NullText;

			memoEditSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditSubheader1.Properties.NullText;
			memoEditSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? memoEditSubheader2.Properties.NullText;
			memoEditSubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? memoEditSubheader3.Properties.NullText;
			textEditSubheader4.Properties.NullText = CustomTabInfo.SubHeader4Placeholder ?? textEditSubheader4.Properties.NullText;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.NextStepsState.TabC.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.NextStepsState.TabC.Clipart2);

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.NextStepsState.TabC.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.NextStepsState.TabC.Combo1 ??
										   CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo2.EditValue = SlideContainer.EditedContent.NextStepsState.TabC.Combo2 ??
										   CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo3.EditValue = SlideContainer.EditedContent.NextStepsState.TabC.Combo3 ??
										   CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo4.EditValue = SlideContainer.EditedContent.NextStepsState.TabC.Combo4 ??
										   CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsDefault);

			memoEditSubheader1.EditValue = SlideContainer.EditedContent.NextStepsState.TabC.Subheader1 ??
											CustomTabInfo.SubHeader1DefaultValue;
			memoEditSubheader2.EditValue = SlideContainer.EditedContent.NextStepsState.TabC.Subheader2 ??
											CustomTabInfo.SubHeader2DefaultValue;
			memoEditSubheader3.EditValue = SlideContainer.EditedContent.NextStepsState.TabC.Subheader3 ??
											CustomTabInfo.SubHeader3DefaultValue;
			textEditSubheader4.EditValue = SlideContainer.EditedContent.NextStepsState.TabC.Subheader4 ??
											CustomTabInfo.SubHeader4DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.NextStepsState.TabC.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.NextStepsState.TabC.Clipart2 = clipartEditContainer2.GetActiveClipartObject();

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.NextStepsState.TabC.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.NextStepsState.TabC.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo1.EditValue ?
				comboBoxEditCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.NextStepsState.TabC.Combo2 = CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo2.EditValue ?
				comboBoxEditCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.NextStepsState.TabC.Combo3 = CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo3.EditValue ?
				comboBoxEditCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo3.EditValue as String } :
				null;
			SlideContainer.EditedContent.NextStepsState.TabC.Combo4 = CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo4.EditValue ?
				comboBoxEditCombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo4.EditValue as String } :
				null;

			SlideContainer.EditedContent.NextStepsState.TabC.Subheader1 =
				memoEditSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue
					? memoEditSubheader1.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.NextStepsState.TabC.Subheader2 =
				memoEditSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue
					? memoEditSubheader2.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.NextStepsState.TabC.Subheader3 =
				memoEditSubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue
					? memoEditSubheader3.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.NextStepsState.TabC.Subheader4 =
				textEditSubheader4.EditValue as String != CustomTabInfo.SubHeader4DefaultValue
					? textEditSubheader4.EditValue as String ?? String.Empty
					: null;

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.NextStepsState.TabC.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.NextStepsState.TabC.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftNextSteps;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.NextStepsState.TabC.Clipart1 ??
						  ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("SHIFT14CCLIPART1", clipart1);
			var clipart2 = SlideContainer.EditedContent.NextStepsState.TabC.Clipart2 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("SHIFT14CCLIPART2", clipart2);

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftNextStepsFile("c_partnership.pptx");

			outputDataPackage.TextItems.Add("SHIFT14CHEADER".ToUpper(), (SlideContainer.EditedContent.NextStepsState.TabC.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);

			outputDataPackage.TextItems.Add("SHIFT14CCOMBO1".ToUpper(), (SlideContainer.EditedContent.NextStepsState.TabC.Combo1 ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT14CCOMBO2".ToUpper(), (SlideContainer.EditedContent.NextStepsState.TabC.Combo2 ?? CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT14CCOMBO3".ToUpper(), (SlideContainer.EditedContent.NextStepsState.TabC.Combo3 ?? CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT14CCOMBO4".ToUpper(), (SlideContainer.EditedContent.NextStepsState.TabC.Combo4 ?? CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault))?.Value);

			outputDataPackage.TextItems.Add("SHIFT14CSUBHEADER1".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabC.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT14CSUBHEADER2".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabC.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT14CSUBHEADER3".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabC.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT14CSUBHEADER4".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabC.Subheader4 ?? CustomTabInfo.SubHeader4DefaultValue);

			return outputDataPackage;
		}
		#endregion
	}
}