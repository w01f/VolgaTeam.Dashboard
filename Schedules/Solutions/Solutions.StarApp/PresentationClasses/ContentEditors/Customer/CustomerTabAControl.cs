using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Configuration.Customer;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Customer
{
	[ToolboxItem(false)]
	public sealed partial class CustomerTabAControl : ChildTabBaseControl
	{
		private CustomerTabAInfo CustomTabInfo => (CustomerTabAInfo)TabInfo;

		public CustomerTabAControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditTabACombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabACombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			comboBoxEditTabACombo1.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabACombo1.Properties.NullText =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(0)?.Value ??
				comboBoxEditTabACombo1.Properties.NullText;
			comboBoxEditTabACombo2.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabACombo2.Properties.NullText =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(1)?.Value ??
				comboBoxEditTabACombo2.Properties.NullText;
			comboBoxEditTabACombo3.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabACombo3.Properties.NullText =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(2)?.Value ??
				comboBoxEditTabACombo3.Properties.NullText;
			comboBoxEditTabACombo4.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabACombo4.Properties.NullText =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(3)?.Value ??
				comboBoxEditTabACombo4.Properties.NullText;

			clipartEditContainerTabA1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabA1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabA2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabA2.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainerTabA1.LoadData(SlideContainer.EditedContent.CustomerState.TabA.Clipart1);
			clipartEditContainerTabA2.LoadData(SlideContainer.EditedContent.CustomerState.TabA.Clipart2);
			comboBoxEditTabACombo1.EditValue = SlideContainer.EditedContent.CustomerState.TabA.Combo1 ??
											   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
												   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0);
			comboBoxEditTabACombo2.EditValue = SlideContainer.EditedContent.CustomerState.TabA.Combo2 ??
											   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
												   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1);
			comboBoxEditTabACombo3.EditValue = SlideContainer.EditedContent.CustomerState.TabA.Combo3 ??
											   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
												   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2);
			comboBoxEditTabACombo4.EditValue = SlideContainer.EditedContent.CustomerState.TabA.Combo4 ??
											   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
												   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.CustomerState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();
			SlideContainer.EditedContent.CustomerState.TabA.Clipart2 = clipartEditContainerTabA2.GetActiveClipartObject();

			SlideContainer.EditedContent.CustomerState.TabA.Combo1 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0) != comboBoxEditTabACombo1.EditValue ?
				comboBoxEditTabACombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.CustomerState.TabA.Combo2 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1) != comboBoxEditTabACombo2.EditValue ?
				comboBoxEditTabACombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.CustomerState.TabA.Combo3 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2) != comboBoxEditTabACombo3.EditValue ?
				comboBoxEditTabACombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo3.EditValue as String } :
				null;
			SlideContainer.EditedContent.CustomerState.TabA.Combo4 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3) != comboBoxEditTabACombo4.EditValue ?
				comboBoxEditTabACombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo4.EditValue as String } :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.CustomerState.TabA.SlideHeader ??
				   CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.CustomerState.TabA.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.CustomerState.TabA.SlideHeader =
				slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.CustomerState.TabA.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		#region Output
		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = TabPageContainer.ParentControl.SelectedTheme;

			var clipart1 = SlideContainer.EditedContent.CustomerState.TabA.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP04ACLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.CustomerState.TabA.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP04ACLIPART2", clipart2);

			var slideHeader = (SlideContainer.EditedContent.CustomerState.TabA.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var combos = new[]
				{
						(SlideContainer.EditedContent.CustomerState.TabA.Combo1 ?? SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0))?.Value,
						(SlideContainer.EditedContent.CustomerState.TabA.Combo2 ?? SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1))?.Value,
						(SlideContainer.EditedContent.CustomerState.TabA.Combo3 ?? SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2))?.Value,
						(SlideContainer.EditedContent.CustomerState.TabA.Combo4 ?? SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3))?.Value,
					}
				.Where(item => !String.IsNullOrWhiteSpace(item))
				.ToList();

			switch (combos.Count)
			{
				case 1:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04A-1.pptx" : (clipart1 != null ? "CP04A-5.pptx" : "CP04A-12.pptx"));
					break;
				case 2:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04A-2.pptx" : (clipart1 != null ? "CP04A-6.pptx" : "CP04A-11.pptx"));
					break;
				case 3:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04A-3.pptx" : (clipart1 != null ? "CP04A-7.pptx" : "CP04A-10.pptx"));
					break;
				case 4:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04A-4.pptx" : (clipart1 != null ? "CP04A-8.pptx" : "CP04A-9.pptx"));
					break;
				default:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04A-1.pptx" : (clipart1 != null ? "CP04A-5.pptx" : "CP04A-12.pptx"));
					break;
			}

			outputDataPackage.TextItems.Add("CP04AHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("CP04ACombo1".ToUpper(), combos.ElementAtOrDefault(0));
			outputDataPackage.TextItems.Add("CP04ACombo2".ToUpper(), combos.ElementAtOrDefault(1));
			outputDataPackage.TextItems.Add("CP04ACombo3".ToUpper(), combos.ElementAtOrDefault(2));
			outputDataPackage.TextItems.Add("CP04ACombo4".ToUpper(), combos.ElementAtOrDefault(3));

			return outputDataPackage;
		}
		#endregion
	}
}