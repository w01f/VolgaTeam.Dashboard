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
	public sealed partial class NextStepsTabGControl : ChildTabBaseControl
	{
		private NextStepsTabGInfo CustomTabInfo => (NextStepsTabGInfo)TabInfo;

		public NextStepsTabGControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);
			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo1Configuration);
			comboBoxEditCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo2Configuration);
			comboBoxEditCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo3Configuration);
			comboBoxEditCombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo4Configuration);
			comboBoxEditCombo5.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo5Configuration);
			memoEditSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader1Configuration);
			memoEditSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader2Configuration);
			memoEditSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader3Configuration);
			memoEditSubheader4.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader4Configuration);
			memoEditSubheader5.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader5Configuration);
			memoEditSubheader6.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader6Configuration);

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

			comboBoxEditCombo5.Properties.Items.Clear();
			comboBoxEditCombo5.Properties.Items.AddRange(CustomTabInfo.Combo5Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo5.Properties.NullText =
				CustomTabInfo.Combo5Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo5.Properties.NullText;

			memoEditSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditSubheader1.Properties.NullText;
			memoEditSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? memoEditSubheader2.Properties.NullText;
			memoEditSubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? memoEditSubheader3.Properties.NullText;
			memoEditSubheader4.Properties.NullText = CustomTabInfo.SubHeader4Placeholder ?? memoEditSubheader4.Properties.NullText;
			memoEditSubheader5.Properties.NullText = CustomTabInfo.SubHeader5Placeholder ?? memoEditSubheader5.Properties.NullText;
			memoEditSubheader6.Properties.NullText = CustomTabInfo.SubHeader6Placeholder ?? memoEditSubheader6.Properties.NullText;

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;

			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.NextStepsState.TabG.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.NextStepsState.TabG.Clipart2);

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.NextStepsState.TabG.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.NextStepsState.TabG.Combo1 ??
											   CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo2.EditValue = SlideContainer.EditedContent.NextStepsState.TabG.Combo2 ??
										   CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo3.EditValue = SlideContainer.EditedContent.NextStepsState.TabG.Combo3 ??
										   CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo4.EditValue = SlideContainer.EditedContent.NextStepsState.TabG.Combo4 ??
										   CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo5.EditValue = SlideContainer.EditedContent.NextStepsState.TabG.Combo5 ??
										   CustomTabInfo.Combo5Items.FirstOrDefault(item => item.IsDefault);

			memoEditSubheader1.EditValue = SlideContainer.EditedContent.NextStepsState.TabG.Subheader1 ??
										   CustomTabInfo.SubHeader1DefaultValue;
			memoEditSubheader2.EditValue = SlideContainer.EditedContent.NextStepsState.TabG.Subheader2 ??
										   CustomTabInfo.SubHeader2DefaultValue;
			memoEditSubheader3.EditValue = SlideContainer.EditedContent.NextStepsState.TabG.Subheader3 ??
										   CustomTabInfo.SubHeader3DefaultValue;
			memoEditSubheader4.EditValue = SlideContainer.EditedContent.NextStepsState.TabG.Subheader4 ??
										   CustomTabInfo.SubHeader4DefaultValue;
			memoEditSubheader5.EditValue = SlideContainer.EditedContent.NextStepsState.TabG.Subheader5 ??
										   CustomTabInfo.SubHeader5DefaultValue;
			memoEditSubheader6.EditValue = SlideContainer.EditedContent.NextStepsState.TabG.Subheader6 ??
										   CustomTabInfo.SubHeader6DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.NextStepsState.TabG.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.NextStepsState.TabG.Clipart2 = clipartEditContainer2.GetActiveClipartObject();

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.NextStepsState.TabG.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.NextStepsState.TabG.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo1.EditValue ?
				comboBoxEditCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.NextStepsState.TabG.Combo2 = CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo2.EditValue ?
				comboBoxEditCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.NextStepsState.TabG.Combo3 = CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo3.EditValue ?
				comboBoxEditCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo3.EditValue as String } :
				null;
			SlideContainer.EditedContent.NextStepsState.TabG.Combo4 = CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo4.EditValue ?
				comboBoxEditCombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo4.EditValue as String } :
				null;
			SlideContainer.EditedContent.NextStepsState.TabG.Combo5 = CustomTabInfo.Combo5Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo5.EditValue ?
				comboBoxEditCombo5.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo5.EditValue as String } :
				null;

			SlideContainer.EditedContent.NextStepsState.TabG.Subheader1 =
				memoEditSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue
					? memoEditSubheader1.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.NextStepsState.TabG.Subheader2 =
				memoEditSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue
					? memoEditSubheader2.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.NextStepsState.TabG.Subheader3 =
				memoEditSubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue
					? memoEditSubheader3.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.NextStepsState.TabG.Subheader4 =
				memoEditSubheader4.EditValue as String != CustomTabInfo.SubHeader4DefaultValue
					? memoEditSubheader4.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.NextStepsState.TabG.Subheader5 =
				memoEditSubheader5.EditValue as String != CustomTabInfo.SubHeader5DefaultValue
					? memoEditSubheader5.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.NextStepsState.TabG.Subheader6 =
				memoEditSubheader6.EditValue as String != CustomTabInfo.SubHeader6DefaultValue
					? memoEditSubheader6.EditValue as String ?? String.Empty
					: null;

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.NextStepsState.TabG.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(bool outputEnabled)
		{
			SlideContainer.EditedContent.NextStepsState.TabG.EnableOutput =
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

			var clipart1 = SlideContainer.EditedContent.NextStepsState.TabG.Clipart1 ??
						  ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("SHIFT14GCLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.NextStepsState.TabG.Clipart2 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("SHIFT14GCLIPART2", clipart2);

			var slideHeader = (SlideContainer.EditedContent.NextStepsState.TabG.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;

			var combos = new[]
				{
					(SlideContainer.EditedContent.NextStepsState.TabG.Combo1 ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.NextStepsState.TabG.Combo2 ?? CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.NextStepsState.TabG.Combo3 ?? CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.NextStepsState.TabG.Combo4 ?? CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.NextStepsState.TabG.Combo5 ?? CustomTabInfo.Combo5Items.FirstOrDefault(h => h.IsDefault))?.Value,
				}
				.Where(item => !String.IsNullOrWhiteSpace(item))
				.ToList();

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftNextStepsFile("g_next_steps.pptx");

			outputDataPackage.TextItems.Add("SHIFT14GHeader".ToUpper(), slideHeader);

			outputDataPackage.TextItems.Add("SHIFT14GCOMBOMERGE1".ToUpper(), String.Join(String.Format("{0}{0}", (char)13), combos.Select(item => String.Format("- {0}", item))));

			outputDataPackage.TextItems.Add("SHIFT14GTAB1SUBHEADER1".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabG.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT14GTAB2SUBHEADER1".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabG.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT14GTAB3SUBHEADER1".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabG.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT14GTAB4SUBHEADER1".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabG.Subheader4 ?? CustomTabInfo.SubHeader4DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT14GTAB5SUBHEADER1".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabG.Subheader5 ?? CustomTabInfo.SubHeader5DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT14GTAB6SUBHEADER1".ToUpper(), SlideContainer.EditedContent.NextStepsState.TabG.Subheader6 ?? CustomTabInfo.SubHeader6DefaultValue);

			return outputDataPackage;
		}
		#endregion
	}
}