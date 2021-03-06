﻿using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Configuration.Customer;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Customer
{
	[ToolboxItem(false)]
	public sealed partial class CustomerTabCControl : ChildTabBaseControl
	{
		private CustomerTabCInfo CustomTabInfo => (CustomerTabCInfo)TabInfo;

		public CustomerTabCControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			memoEditTabCSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabCSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabCSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			memoEditTabCSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditTabCSubheader1.Properties.NullText;
			memoEditTabCSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? memoEditTabCSubheader2.Properties.NullText;
			memoEditTabCSubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? memoEditTabCSubheader3.Properties.NullText;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			memoEditTabCSubheader1.EditValue = SlideContainer.EditedContent.CustomerState.TabC.Subheader1 ??
											   CustomTabInfo.SubHeader1DefaultValue;
			memoEditTabCSubheader2.EditValue = SlideContainer.EditedContent.CustomerState.TabC.Subheader2 ??
											   CustomTabInfo.SubHeader2DefaultValue;
			memoEditTabCSubheader3.EditValue = SlideContainer.EditedContent.CustomerState.TabC.Subheader3 ??
											   CustomTabInfo.SubHeader3DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.CustomerState.TabC.Subheader1 = memoEditTabCSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				memoEditTabCSubheader1.EditValue as String :
				null;
			SlideContainer.EditedContent.CustomerState.TabC.Subheader2 = memoEditTabCSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue ?
				memoEditTabCSubheader2.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.CustomerState.TabC.Subheader3 = memoEditTabCSubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue ?
				memoEditTabCSubheader3.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.CustomerState.TabC.SlideHeader ??
				   CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.CustomerState.TabC.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.CustomerState.TabC.SlideHeader =
				slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.CustomerState.TabC.EnableOutput =
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

			var slideHeader = (SlideContainer.EditedContent.CustomerState.TabC.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var subHeaders = new[]
				{
						SlideContainer.EditedContent.CustomerState.TabC.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue,
						SlideContainer.EditedContent.CustomerState.TabC.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue,
						SlideContainer.EditedContent.CustomerState.TabC.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue
					}
				.Where(item => !String.IsNullOrWhiteSpace(item))
				.ToList();

			switch (subHeaders.Count)
			{
				case 1:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile("CP04C-1.pptx");
					break;
				case 2:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile("CP04C-2.pptx");
					break;
				case 3:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile("CP04C-3.pptx");
					break;
				default:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile("CP04C-1.pptx");
					break;
			}

			outputDataPackage.TextItems.Add("CP04CHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("CP04CSubheader1".ToUpper(), subHeaders.ElementAtOrDefault(0));
			outputDataPackage.TextItems.Add("CP04CSubheader2".ToUpper(), subHeaders.ElementAtOrDefault(1));
			outputDataPackage.TextItems.Add("CP04CSubheader3".ToUpper(), subHeaders.ElementAtOrDefault(2));

			return outputDataPackage;
		}
		#endregion
	}
}