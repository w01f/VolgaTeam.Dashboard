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
	public sealed partial class NextStepsTabDControl : ChildTabBaseControl
	{
		private NextStepsTabDInfo CustomTabInfo => (NextStepsTabDInfo)TabInfo;

		public NextStepsTabDControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);
			memoEditSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader1Configuration);
			memoEditSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader2Configuration);
			memoEditSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader3Configuration);
			memoEditSubheader4.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader4Configuration);
			memoEditSubheader5.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader5Configuration);
			memoEditSubheader6.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader6Configuration);
			memoEditSubheader7.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader7Configuration);
			memoEditSubheader8.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader8Configuration);

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(CustomTabInfo.HeadersItems
				.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditSlideHeader.Properties.NullText =
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				"Select or type";

			memoEditSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditSubheader1.Properties.NullText;
			memoEditSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? memoEditSubheader2.Properties.NullText;
			memoEditSubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? memoEditSubheader3.Properties.NullText;
			memoEditSubheader4.Properties.NullText = CustomTabInfo.SubHeader4Placeholder ?? memoEditSubheader4.Properties.NullText;
			memoEditSubheader5.Properties.NullText = CustomTabInfo.SubHeader5Placeholder ?? memoEditSubheader5.Properties.NullText;
			memoEditSubheader6.Properties.NullText = CustomTabInfo.SubHeader6Placeholder ?? memoEditSubheader6.Properties.NullText;
			memoEditSubheader7.Properties.NullText = CustomTabInfo.SubHeader7Placeholder ?? memoEditSubheader7.Properties.NullText;
			memoEditSubheader8.Properties.NullText = CustomTabInfo.SubHeader8Placeholder ?? memoEditSubheader8.Properties.NullText;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.NextStepsState.TabD.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.NextStepsState.TabD.Clipart2);

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.NextStepsState.TabD.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			memoEditSubheader1.EditValue = SlideContainer.EditedContent.NextStepsState.TabD.Subheader1 ??
											CustomTabInfo.SubHeader1DefaultValue;
			memoEditSubheader2.EditValue = SlideContainer.EditedContent.NextStepsState.TabD.Subheader2 ??
											CustomTabInfo.SubHeader2DefaultValue;
			memoEditSubheader3.EditValue = SlideContainer.EditedContent.NextStepsState.TabD.Subheader3 ??
											CustomTabInfo.SubHeader3DefaultValue;
			memoEditSubheader4.EditValue = SlideContainer.EditedContent.NextStepsState.TabD.Subheader4 ??
											CustomTabInfo.SubHeader4DefaultValue;
			memoEditSubheader5.EditValue = SlideContainer.EditedContent.NextStepsState.TabD.Subheader5 ??
										   CustomTabInfo.SubHeader5DefaultValue;
			memoEditSubheader6.EditValue = SlideContainer.EditedContent.NextStepsState.TabD.Subheader6 ??
										   CustomTabInfo.SubHeader6DefaultValue;
			memoEditSubheader7.EditValue = SlideContainer.EditedContent.NextStepsState.TabD.Subheader7 ??
										   CustomTabInfo.SubHeader7DefaultValue;
			memoEditSubheader8.EditValue = SlideContainer.EditedContent.NextStepsState.TabD.Subheader8 ??
										   CustomTabInfo.SubHeader8DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.NextStepsState.TabD.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.NextStepsState.TabD.Clipart2 = clipartEditContainer2.GetActiveClipartObject();

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.NextStepsState.TabD.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.NextStepsState.TabD.Subheader1 =
				memoEditSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue
					? memoEditSubheader1.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.NextStepsState.TabD.Subheader2 =
				memoEditSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue
					? memoEditSubheader2.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.NextStepsState.TabD.Subheader3 =
				memoEditSubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue
					? memoEditSubheader3.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.NextStepsState.TabD.Subheader4 =
				memoEditSubheader4.EditValue as String != CustomTabInfo.SubHeader4DefaultValue
					? memoEditSubheader4.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.NextStepsState.TabD.Subheader5 =
				memoEditSubheader5.EditValue as String != CustomTabInfo.SubHeader5DefaultValue
					? memoEditSubheader5.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.NextStepsState.TabD.Subheader6 =
				memoEditSubheader6.EditValue as String != CustomTabInfo.SubHeader6DefaultValue
					? memoEditSubheader6.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.NextStepsState.TabD.Subheader7 =
				memoEditSubheader7.EditValue as String != CustomTabInfo.SubHeader7DefaultValue
					? memoEditSubheader7.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.NextStepsState.TabD.Subheader8 =
				memoEditSubheader8.EditValue as String != CustomTabInfo.SubHeader8DefaultValue
					? memoEditSubheader8.EditValue as String ?? String.Empty
					: null;

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.NextStepsState.TabD.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.NextStepsState.TabD.EnableOutput =
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

			var clipart1 = SlideContainer.EditedContent.NextStepsState.TabD.Clipart1 ??
						  ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("SHIFT14DCLIPART1", clipart1);
			var clipart2 = SlideContainer.EditedContent.NextStepsState.TabD.Clipart2 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("SHIFT14DCLIPART2", clipart2);

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftNextStepsFile("d_grow_business.pptx");

			outputDataPackage.TextItems.Add("SHIFT14DHEADER".ToUpper(), (SlideContainer.EditedContent.NextStepsState.TabD.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);

			outputDataPackage.TextItems.Add("SHIFT14DSUBHEADER1".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabD.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT14DSUBHEADER2".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabD.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT14DSUBHEADER3".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabD.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT14DSUBHEADER4".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabD.Subheader4 ?? CustomTabInfo.SubHeader4DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT14DSUBHEADER5".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabD.Subheader5 ?? CustomTabInfo.SubHeader5DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT14DSUBHEADER6".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabD.Subheader6 ?? CustomTabInfo.SubHeader6DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT14DSUBHEADER7".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabD.Subheader7 ?? CustomTabInfo.SubHeader7DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT14DSUBHEADER8".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabD.Subheader8 ?? CustomTabInfo.SubHeader8DefaultValue);

			return outputDataPackage;
		}
		#endregion
	}
}