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
	public sealed partial class InvestmentTabFControl : ChildTabBaseControl
	{
		private InvestmentTabFInfo CustomTabInfo => (InvestmentTabFInfo)TabInfo;

		public InvestmentTabFControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);
			memoEditSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SubHeader1Configuration);
			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo1Configuration);
			comboBoxEditCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo2Configuration);
			comboBoxEditCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo3Configuration);
			comboBoxEditCombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo4Configuration);
			comboBoxEditCombo5.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo5Configuration);
			comboBoxEditCombo6.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo6Configuration);
			comboBoxEditCombo7.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo7Configuration);
			comboBoxEditCombo8.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.Combo8Configuration);

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

			comboBoxEditCombo7.Properties.Items.Clear();
			comboBoxEditCombo7.Properties.Items.AddRange(CustomTabInfo.Combo7Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo7.Properties.NullText =
				CustomTabInfo.Combo7Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo7.Properties.NullText;

			comboBoxEditCombo8.Properties.Items.Clear();
			comboBoxEditCombo8.Properties.Items.AddRange(CustomTabInfo.Combo8Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditCombo8.Properties.NullText =
				CustomTabInfo.Combo8Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditCombo8.Properties.NullText;

			memoEditSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditSubheader1.Properties.NullText;

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;

			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;

			clipartEditContainer3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainer3.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.InvestmentState.TabF.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.InvestmentState.TabF.Clipart2);
			clipartEditContainer3.LoadData(SlideContainer.EditedContent.InvestmentState.TabF.Clipart3);

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.InvestmentState.TabF.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.InvestmentState.TabF.Combo1 ??
											   CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo2.EditValue = SlideContainer.EditedContent.InvestmentState.TabF.Combo2 ??
										   CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo3.EditValue = SlideContainer.EditedContent.InvestmentState.TabF.Combo3 ??
										   CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo4.EditValue = SlideContainer.EditedContent.InvestmentState.TabF.Combo4 ??
										   CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo5.EditValue = SlideContainer.EditedContent.InvestmentState.TabF.Combo5 ??
										   CustomTabInfo.Combo5Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo6.EditValue = SlideContainer.EditedContent.InvestmentState.TabF.Combo6 ??
										   CustomTabInfo.Combo6Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo7.EditValue = SlideContainer.EditedContent.InvestmentState.TabF.Combo7 ??
										   CustomTabInfo.Combo7Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditCombo8.EditValue = SlideContainer.EditedContent.InvestmentState.TabF.Combo8 ??
			                               CustomTabInfo.Combo8Items.FirstOrDefault(item => item.IsDefault);

			memoEditSubheader1.EditValue = SlideContainer.EditedContent.InvestmentState.TabF.Subheader1 ??
			                               CustomTabInfo.SubHeader1DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.InvestmentState.TabF.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.InvestmentState.TabF.Clipart2 = clipartEditContainer2.GetActiveClipartObject();
			SlideContainer.EditedContent.InvestmentState.TabF.Clipart3 = clipartEditContainer3.GetActiveClipartObject();

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.InvestmentState.TabF.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.InvestmentState.TabF.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo1.EditValue ?
				comboBoxEditCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.InvestmentState.TabF.Combo2 = CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo2.EditValue ?
				comboBoxEditCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.InvestmentState.TabF.Combo3 = CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo3.EditValue ?
				comboBoxEditCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo3.EditValue as String } :
				null;
			SlideContainer.EditedContent.InvestmentState.TabF.Combo4 = CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo4.EditValue ?
				comboBoxEditCombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo4.EditValue as String } :
				null;
			SlideContainer.EditedContent.InvestmentState.TabF.Combo5 = CustomTabInfo.Combo5Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo5.EditValue ?
				comboBoxEditCombo5.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo5.EditValue as String } :
				null;
			SlideContainer.EditedContent.InvestmentState.TabF.Combo6 = CustomTabInfo.Combo6Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo6.EditValue ?
				comboBoxEditCombo6.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo6.EditValue as String } :
				null;
			SlideContainer.EditedContent.InvestmentState.TabF.Combo7 = CustomTabInfo.Combo7Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo7.EditValue ?
				comboBoxEditCombo7.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo7.EditValue as String } :
				null;
			SlideContainer.EditedContent.InvestmentState.TabF.Combo8 = CustomTabInfo.Combo8Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditCombo8.EditValue ?
				comboBoxEditCombo8.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditCombo8.EditValue as String } :
				null;

			SlideContainer.EditedContent.InvestmentState.TabF.Subheader1 =
				memoEditSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue
					? memoEditSubheader1.EditValue as String ?? String.Empty
					: null;

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.InvestmentState.TabF.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(bool outputEnabled)
		{
			SlideContainer.EditedContent.InvestmentState.TabF.EnableOutput =
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

			var clipart1 = SlideContainer.EditedContent.InvestmentState.TabF.Clipart1 ??
						  ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
			{
				clipart1.OutputBackground = true;
				outputDataPackage.ClipartItems.Add("SHIFT12FCLIPART1", clipart1);
			}

			var clipart2 = SlideContainer.EditedContent.InvestmentState.TabF.Clipart2 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("SHIFT12FCLIPART2", clipart2);

			var clipart3 = SlideContainer.EditedContent.InvestmentState.TabF.Clipart3 ??
						   ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("SHIFT12FCLIPART3", clipart3);

			var slideHeader = (SlideContainer.EditedContent.InvestmentState.TabF.SlideHeader ??
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;

			var combos = new[]
				{
					(SlideContainer.EditedContent.InvestmentState.TabF.Combo1 ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.InvestmentState.TabF.Combo2 ?? CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.InvestmentState.TabF.Combo3 ?? CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.InvestmentState.TabF.Combo4 ?? CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.InvestmentState.TabF.Combo5 ?? CustomTabInfo.Combo5Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.InvestmentState.TabF.Combo6 ?? CustomTabInfo.Combo6Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.InvestmentState.TabF.Combo7 ?? CustomTabInfo.Combo7Items.FirstOrDefault(h => h.IsDefault))?.Value,
					(SlideContainer.EditedContent.InvestmentState.TabF.Combo8 ?? CustomTabInfo.Combo8Items.FirstOrDefault(h => h.IsDefault))?.Value,
				}
				.Where(item => !String.IsNullOrWhiteSpace(item))
				.ToList();

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftInvestmentFile("proof_f.pptx");

			outputDataPackage.TextItems.Add("SHIFT12FHeader".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("SHIFT12FSUBHEADER1".ToUpper(), SlideContainer.EditedContent.InvestmentState.TabF.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue);

			outputDataPackage.TextItems.Add("SHIFT12FCOMBOMERGE1".ToUpper(), String.Join(String.Format("{0}", (char)13), combos));

			return outputDataPackage;
		}
		#endregion
	}
}