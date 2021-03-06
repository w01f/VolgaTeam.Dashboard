﻿using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Configuration.Audience;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Audience
{
	[ToolboxItem(false)]
	public sealed partial class AudienceTabCControl : ChildTabBaseControl
	{
		private AudienceTabCInfo CustomTabInfo => (AudienceTabCInfo)TabInfo;

		public AudienceTabCControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			comboBoxEditTabCCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			comboBoxEditTabCCombo1.Properties.Items.AddRange(CustomTabInfo.Combo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo1.Properties.NullText =
				CustomTabInfo.Combo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo1.Properties.NullText;

			clipartEditContainerTabC1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabC1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabC2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabC2.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabC3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabC3.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabC4.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart4Image), CustomTabInfo.Clipart4Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabC4.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainerTabC1.LoadData(SlideContainer.EditedContent.AudienceState.TabC.Clipart1);
			clipartEditContainerTabC2.LoadData(SlideContainer.EditedContent.AudienceState.TabC.Clipart2);
			clipartEditContainerTabC3.LoadData(SlideContainer.EditedContent.AudienceState.TabC.Clipart3);
			clipartEditContainerTabC1.LoadData(SlideContainer.EditedContent.AudienceState.TabC.Clipart4);
			comboBoxEditTabCCombo1.EditValue = SlideContainer.EditedContent.AudienceState.TabC.Combo1 ??
											   CustomTabInfo.Combo1Items.FirstOrDefault(
												   item => item.IsDefault);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;


			SlideContainer.EditedContent.AudienceState.TabC.Clipart1 = clipartEditContainerTabC1.GetActiveClipartObject();
			SlideContainer.EditedContent.AudienceState.TabC.Clipart2 = clipartEditContainerTabC2.GetActiveClipartObject();
			SlideContainer.EditedContent.AudienceState.TabC.Clipart3 = clipartEditContainerTabC3.GetActiveClipartObject();
			SlideContainer.EditedContent.AudienceState.TabC.Clipart4 = clipartEditContainerTabC4.GetActiveClipartObject();

			SlideContainer.EditedContent.AudienceState.TabC.Combo1 = CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo1.EditValue ?
				comboBoxEditTabCCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo1.EditValue as String } :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.AudienceState.TabC.SlideHeader ??
				   CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.AudienceState.TabC.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.AudienceState.TabC.SlideHeader =
				slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.AudienceState.TabC.EnableOutput =
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

			var clipart1 = SlideContainer.EditedContent.AudienceState.TabC.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP09CCLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.AudienceState.TabC.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP09CCLIPART2", clipart2);

			var clipart3 = SlideContainer.EditedContent.AudienceState.TabC.Clipart3 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP09CCLIPART3", clipart3);

			var clipart4 = SlideContainer.EditedContent.AudienceState.TabC.Clipart4 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart4Image);
			if (clipart4 != null)
				outputDataPackage.ClipartItems.Add("CP09CCLIPART4", clipart4);

			var slideHeader = (SlideContainer.EditedContent.AudienceState.TabC.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var combo1 = (SlideContainer.EditedContent.AudienceState.TabC.Combo1 ?? CustomTabInfo.Combo1Items.FirstOrDefault(h => h.IsDefault))?.Value;

			outputDataPackage.TextItems.Add("CP09CHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("CP09CCombo1".ToUpper(), combo1);

			if (clipart1 != null &&
				clipart2 != null &&
				clipart3 != null &&
				clipart4 != null)
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09C-1.pptx");
			else if (clipart1 != null &&
					 clipart2 != null &&
					 clipart3 != null)
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09C-2.pptx");
			else if (clipart1 != null &&
					 clipart2 != null)
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09C-3.pptx");
			else if (clipart2 != null &&
					 clipart4 != null)
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09C-5.pptx");
			else if (clipart1 != null &&
					 clipart3 != null)
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09C-6.pptx");
			else if (clipart1 != null &&
					 clipart4 != null)
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09C-7.pptx");
			else if (clipart1 != null)
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09C-4.pptx");

			return outputDataPackage;
		}
		#endregion
	}
}