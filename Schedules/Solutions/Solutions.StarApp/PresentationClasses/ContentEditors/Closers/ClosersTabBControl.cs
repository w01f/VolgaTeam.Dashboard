using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Configuration.Closers;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Closers
{
	public partial class ClosersTabBControl : ChildTabBaseControl
	{
		private ClosersTabBInfo CustomTabInfo => (ClosersTabBInfo)TabInfo;

		public ClosersTabBControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditTabBCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabBCombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			Application.DoEvents();

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;
			clipartEditContainer2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainer2.EditValueChanged += OnEditValueChanged;

			Application.DoEvents();
			memoEditTabBSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditTabBSubheader1.Properties.NullText;
			memoEditTabBSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? memoEditTabBSubheader2.Properties.NullText;
			memoEditTabBSubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? memoEditTabBSubheader3.Properties.NullText;

			comboBoxEditTabBCombo1.Properties.Items.AddRange(CustomTabInfo.Combo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo1.Properties.NullText =
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabBCombo1.Properties.NullText;
			comboBoxEditTabBCombo2.Properties.Items.AddRange(CustomTabInfo.Combo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo2.Properties.NullText =
				CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabBCombo2.Properties.NullText;
			comboBoxEditTabBCombo3.Properties.Items.AddRange(CustomTabInfo.Combo3Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo3.Properties.NullText =
				CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabBCombo3.Properties.NullText;
			comboBoxEditTabBCombo4.Properties.Items.AddRange(CustomTabInfo.Combo4Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabBCombo4.Properties.NullText =
				CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabBCombo4.Properties.NullText;
			Application.DoEvents();
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.ClosersState.TabB.Clipart1);
			clipartEditContainer2.LoadData(SlideContainer.EditedContent.ClosersState.TabB.Clipart2);

			comboBoxEditTabBCombo1.EditValue = SlideContainer.EditedContent.ClosersState.TabB.Combo1 ??
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo2.EditValue = SlideContainer.EditedContent.ClosersState.TabB.Combo2 ??
				CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo3.EditValue = SlideContainer.EditedContent.ClosersState.TabB.Combo3 ??
				CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsDefault);
			comboBoxEditTabBCombo4.EditValue = SlideContainer.EditedContent.ClosersState.TabB.Combo4 ??
				CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsDefault);

			memoEditTabBSubheader1.EditValue = SlideContainer.EditedContent.ClosersState.TabB.Subheader1 ??
				CustomTabInfo.SubHeader1DefaultValue;
			memoEditTabBSubheader2.EditValue = SlideContainer.EditedContent.ClosersState.TabB.Subheader2 ??
				CustomTabInfo.SubHeader2DefaultValue;
			memoEditTabBSubheader3.EditValue = SlideContainer.EditedContent.ClosersState.TabB.Subheader3 ??
				CustomTabInfo.SubHeader3DefaultValue;
			Application.DoEvents();

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.ClosersState.TabB.Clipart1 = clipartEditContainer1.GetActiveClipartObject();
			SlideContainer.EditedContent.ClosersState.TabB.Clipart2 = clipartEditContainer2.GetActiveClipartObject();

			SlideContainer.EditedContent.ClosersState.TabB.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo1.EditValue ?
				comboBoxEditTabBCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo1.EditValue as String } :
				null;
			SlideContainer.EditedContent.ClosersState.TabB.Combo2 = CustomTabInfo.Combo2Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo2.EditValue ?
				comboBoxEditTabBCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo2.EditValue as String } :
				null;
			SlideContainer.EditedContent.ClosersState.TabB.Combo3 = CustomTabInfo.Combo3Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo3.EditValue ?
				comboBoxEditTabBCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo3.EditValue as String } :
				null;
			SlideContainer.EditedContent.ClosersState.TabB.Combo4 = CustomTabInfo.Combo4Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabBCombo4.EditValue ?
				comboBoxEditTabBCombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo4.EditValue as String } :
				null;

			SlideContainer.EditedContent.ClosersState.TabB.Subheader1 = memoEditTabBSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				memoEditTabBSubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ClosersState.TabB.Subheader2 = memoEditTabBSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue ?
				memoEditTabBSubheader2.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.ClosersState.TabB.Subheader3 = memoEditTabBSubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue ?
				memoEditTabBSubheader3.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.ClosersState.TabB.SlideHeader ??
				   CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.ClosersState.TabB.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.ClosersState.TabB.SlideHeader =
				slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.ClosersState.TabB.EnableOutput =
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

			var clipart1 = SlideContainer.EditedContent.ClosersState.TabB.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP11BCLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.ClosersState.TabB.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP11BCLIPART2", clipart2);

			var slideHeader = (SlideContainer.EditedContent.ClosersState.TabB.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;

			var optionalComboItemKeys = new[]
			{
				"CP11BSubHeader1",
				"CP11BSubHeader3",
				"CP11BSubHeader5"
			};
			var optionalComboItemValues = new List<string>();
			var combo1 = (SlideContainer.EditedContent.ClosersState.TabB.Combo1 ??
						 CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsDefault))?.Value;
			var combo2 = (SlideContainer.EditedContent.ClosersState.TabB.Combo2 ??
						 CustomTabInfo.Combo2Items.FirstOrDefault(item => item.IsDefault))?.Value;
			if (!String.IsNullOrWhiteSpace(combo2))
				optionalComboItemValues.Add(combo2);
			var combo3 = (SlideContainer.EditedContent.ClosersState.TabB.Combo3 ??
						 CustomTabInfo.Combo3Items.FirstOrDefault(item => item.IsDefault))?.Value;
			if (!String.IsNullOrWhiteSpace(combo3))
				optionalComboItemValues.Add(combo3);
			var combo4 = (SlideContainer.EditedContent.ClosersState.TabB.Combo4 ??
						 CustomTabInfo.Combo4Items.FirstOrDefault(item => item.IsDefault))?.Value;
			if (!String.IsNullOrWhiteSpace(combo4))
				optionalComboItemValues.Add(combo4);

			var subheader1 = SlideContainer.EditedContent.ClosersState.TabB.Subheader1 ??
							 CustomTabInfo.SubHeader1DefaultValue;
			var subheader2 = SlideContainer.EditedContent.ClosersState.TabB.Subheader2 ??
							 CustomTabInfo.SubHeader2DefaultValue;
			var subheader3 = SlideContainer.EditedContent.ClosersState.TabB.Subheader3 ??
							 CustomTabInfo.SubHeader3DefaultValue;

			outputDataPackage.TextItems.Add("CP11BHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);

			outputDataPackage.TextItems.Add("CP11BCombo1".ToUpper(), combo1);
			outputDataPackage.TextItems.Add("CP11BSubHeader2".ToUpper(), subheader1);
			outputDataPackage.TextItems.Add("CP11BSubHeader4".ToUpper(), subheader2);
			outputDataPackage.TextItems.Add("CP11BSubHeader6".ToUpper(), subheader3);

			switch (optionalComboItemValues.Count)
			{
				case 1:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11B-3.pptx");
					break;
				case 2:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11B-2.pptx");
					break;
				case 3:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11B-1.pptx");
					break;
				default:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarClosersFile("CP11B-3.pptx");
					break;
			}

			for (var i = 0; i < optionalComboItemKeys.Length; i++)
				outputDataPackage.TextItems.Add(optionalComboItemKeys[i].ToUpper(), optionalComboItemValues.ElementAtOrDefault(i) ?? String.Empty);

			return outputDataPackage;
		}
		#endregion
	}
}
