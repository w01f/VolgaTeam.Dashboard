using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Configuration.Fishing;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Fishing
{
	[ToolboxItem(false)]
	public sealed partial class FishingTabCControl : ChildTabBaseControl
	{
		private FishingTabCInfo CustomTabInfo => (FishingTabCInfo)TabInfo;

		public FishingTabCControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
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

			memoEditTabCSubheader1.EditValue = SlideContainer.EditedContent.FishingState.TabC.Subheader1 ??
											   CustomTabInfo.SubHeader1DefaultValue;
			memoEditTabCSubheader2.EditValue = SlideContainer.EditedContent.FishingState.TabC.Subheader2 ??
											   CustomTabInfo.SubHeader2DefaultValue;
			memoEditTabCSubheader3.EditValue = SlideContainer.EditedContent.FishingState.TabC.Subheader3 ??
											   CustomTabInfo.SubHeader3DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.FishingState.TabC.Subheader1 = memoEditTabCSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				memoEditTabCSubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.FishingState.TabC.Subheader2 = memoEditTabCSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue ?
				memoEditTabCSubheader2.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.FishingState.TabC.Subheader3 = memoEditTabCSubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue ?
				memoEditTabCSubheader3.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.FishingState.TabC.SlideHeader ??
			       TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.FishingState.TabC.SlideHeader =
				slideHeaderValue != TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
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

			var slideHeader = (SlideContainer.EditedContent.FishingState.TabC.SlideHeader ?? TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var subHeaders = new[]
				{
						SlideContainer.EditedContent.FishingState.TabC.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue,
						SlideContainer.EditedContent.FishingState.TabC.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue,
						SlideContainer.EditedContent.FishingState.TabC.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue
					}
				.Where(item => !String.IsNullOrWhiteSpace(item))
				.ToList();

			switch (subHeaders.Count)
			{
				case 1:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile("CP03C-1.pptx");
					break;
				case 2:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile("CP03C-2.pptx");
					break;
				case 3:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile("CP03C-3.pptx");
					break;
				default:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile("CP03C-1.pptx");
					break;
			}

			outputDataPackage.TextItems.Add("CP03CHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("CP03CSubheader1".ToUpper(), subHeaders.ElementAtOrDefault(0));
			outputDataPackage.TextItems.Add("CP03CSubheader2".ToUpper(), subHeaders.ElementAtOrDefault(1));
			outputDataPackage.TextItems.Add("CP03CSubheader3".ToUpper(), subHeaders.ElementAtOrDefault(2));

			return outputDataPackage;
		}
		#endregion
	}
}