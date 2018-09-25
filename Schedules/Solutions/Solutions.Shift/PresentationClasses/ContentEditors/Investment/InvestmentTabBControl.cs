using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Investment;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Investment
{
	[ToolboxItem(false)]
	public sealed partial class InvestmentTabBControl : ChildTabBaseControl
	{
		private InvestmentTabBInfo CustomTabInfo => (InvestmentTabBInfo)TabInfo;

		public InvestmentTabBControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
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
			comboBoxEditCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo3Configuration);
			comboBoxEditCombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo4Configuration);
			comboBoxEditCombo5.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo5Configuration);
			comboBoxEditCombo6.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo6Configuration);
			textEditSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader1Configuration);
			memoEditSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader2Configuration);

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
			comboBoxEditCombo6.Properties.Items.Clear();
			comboBoxEditCombo6.Properties.Items.AddRange(CustomTabInfo.Combo6Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo6.Properties.NullText =
				CustomTabInfo.Combo6Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo6.Properties.NullText;

			textEditSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? textEditSubheader1.Properties.NullText;
			memoEditSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? memoEditSubheader2.Properties.NullText;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.InvestmentState.TabB.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.InvestmentState.TabB.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.InvestmentState.TabB.Clipart3);
			clipartEditContainer4.LoadData(SlideContainer.EditedContent.InvestmentState.TabB.Clipart4);
			clipartEditContainer5.LoadData(SlideContainer.EditedContent.InvestmentState.TabB.Clipart5);

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.InvestmentState.TabB.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
			comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.InvestmentState.TabB.Combo1 ??
										   CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo2.EditValue = SlideContainer.EditedContent.InvestmentState.TabB.Combo2 ??
										   CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo3.EditValue = SlideContainer.EditedContent.InvestmentState.TabB.Combo3 ??
										   CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo4.EditValue = SlideContainer.EditedContent.InvestmentState.TabB.Combo4 ??
										   CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo5.EditValue = SlideContainer.EditedContent.InvestmentState.TabB.Combo5 ??
										   CustomTabInfo.Combo5Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo6.EditValue = SlideContainer.EditedContent.InvestmentState.TabB.Combo6 ??
										   CustomTabInfo.Combo6Items.FirstOrDefault(item => item.IsDefault);

			textEditSubheader1.EditValue = SlideContainer.EditedContent.InvestmentState.TabB.Subheader1 ??
										   CustomTabInfo.SubHeader1DefaultValue;
			memoEditSubheader2.EditValue = SlideContainer.EditedContent.InvestmentState.TabB.Subheader2 ??
											   CustomTabInfo.SubHeader2DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.InvestmentState.TabB.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.InvestmentState.TabB.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.InvestmentState.TabB.Clipart3 = clipartEditContainer3.GetActiveClipartObject();
			SlideContainer.EditedContent.InvestmentState.TabB.Clipart4 = clipartEditContainer4.GetActiveClipartObject();
			SlideContainer.EditedContent.InvestmentState.TabB.Clipart5 = clipartEditContainer5.GetActiveClipartObject();

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.InvestmentState.TabB.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.InvestmentState.TabB.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo1.EditValue ?
				comboBoxEditCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.InvestmentState.TabB.Combo2 = CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo2.EditValue ?
				comboBoxEditCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.InvestmentState.TabB.Combo3 = CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo3.EditValue ?
				comboBoxEditCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo3.EditValue as String } :
				null;
			SlideContainer.EditedContent.InvestmentState.TabB.Combo4 = CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo4.EditValue ?
				comboBoxEditCombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo4.EditValue as String } :
				null;
			SlideContainer.EditedContent.InvestmentState.TabB.Combo5 = CustomTabInfo.Combo5Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo5.EditValue ?
				comboBoxEditCombo5.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo5.EditValue as String } :
				null;
			SlideContainer.EditedContent.InvestmentState.TabB.Combo6 = CustomTabInfo.Combo6Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo6.EditValue ?
				comboBoxEditCombo6.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo6.EditValue as String } :
				null;

			SlideContainer.EditedContent.InvestmentState.TabB.Subheader1 =
				textEditSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue
					? textEditSubheader1.EditValue as String ?? String.Empty
					: null;
			SlideContainer.EditedContent.InvestmentState.TabB.Subheader2 =
				memoEditSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue
					? memoEditSubheader2.EditValue as String ?? String.Empty
					: null;

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.InvestmentState.TabB.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.InvestmentState.TabB.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftInvestment;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.InvestmentState.TabB.Clipart1 ??
						  ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("SHIFT12BCLIPART1", clipart1);
			var clipart2 = SlideContainer.EditedContent.InvestmentState.TabB.Clipart2 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("SHIFT12BCLIPART2", clipart2);
			var clipart3 = SlideContainer.EditedContent.InvestmentState.TabB.Clipart3 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("SHIFT12BCLIPART3", clipart3);
			var clipart4 = SlideContainer.EditedContent.InvestmentState.TabB.Clipart4 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart4Image);
			if (clipart4 != null)
				outputDataPackage.ClipartItems.Add("SHIFT12BCLIPART4", clipart4);
			var clipart5 = SlideContainer.EditedContent.InvestmentState.TabB.Clipart5 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart5Image);
			if (clipart5 != null)
				outputDataPackage.ClipartItems.Add("SHIFT12BCLIPART5", clipart5);

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftInvestmentFile("investment_b.pptx");

			outputDataPackage.TextItems.Add("SHIFT12BHEADER".ToUpper(), (SlideContainer.EditedContent.InvestmentState.TabB.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);

			outputDataPackage.TextItems.Add("SHIFT12BCOMBO1".ToUpper(), (SlideContainer.EditedContent.InvestmentState.TabB.Combo1 ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT12BCOMBO2".ToUpper(), (SlideContainer.EditedContent.InvestmentState.TabB.Combo2 ?? CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT12BCOMBO3".ToUpper(), (SlideContainer.EditedContent.InvestmentState.TabB.Combo3 ?? CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT12BCOMBO4".ToUpper(), (SlideContainer.EditedContent.InvestmentState.TabB.Combo4 ?? CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT12BCOMBO5".ToUpper(), (SlideContainer.EditedContent.InvestmentState.TabB.Combo5 ?? CustomTabInfo.Combo5Items.FirstOrDefault(h => h.IsDefault))?.Value);
			outputDataPackage.TextItems.Add("SHIFT12BCOMBO6".ToUpper(), (SlideContainer.EditedContent.InvestmentState.TabB.Combo6 ?? CustomTabInfo.Combo6Items.FirstOrDefault(h => h.IsDefault))?.Value);

			outputDataPackage.TextItems.Add("SHIFT12BSUBHEADER1".ToUpper(), SlideContainer.EditedContent.InvestmentState.TabB.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue);
			outputDataPackage.TextItems.Add("SHIFT12BSUBHEADER2".ToUpper(), SlideContainer.EditedContent.InvestmentState.TabB.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue);

			return outputDataPackage;
		}
		#endregion
	}
}