using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Configuration.Fishing;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Fishing
{
	[ToolboxItem(false)]
	public sealed partial class FishingTabAControl : ChildTabBaseControl
	{
		private FishingTabAInfo CustomTabInfo => (FishingTabAInfo)TabInfo;

		public FishingTabAControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			memoEditTabASubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabASubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			memoEditTabASubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditTabASubheader1.Properties.NullText;
			memoEditTabASubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? memoEditTabASubheader2.Properties.NullText;

			clipartEditContainerTabA1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabA1.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainerTabA1.LoadData(SlideContainer.EditedContent.FishingState.TabA.Clipart1);
			memoEditTabASubheader1.EditValue = SlideContainer.EditedContent.FishingState.TabA.Subheader1 ??
											   CustomTabInfo.SubHeader1DefaultValue;
			memoEditTabASubheader2.EditValue = SlideContainer.EditedContent.FishingState.TabA.Subheader2 ??
											   CustomTabInfo.SubHeader2DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.FishingState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();

			SlideContainer.EditedContent.FishingState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				memoEditTabASubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.FishingState.TabA.Subheader2 = memoEditTabASubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue ?
				memoEditTabASubheader2.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.FishingState.TabA.SlideHeader ??
				   TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.FishingState.TabA.SlideHeader =
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

			var clipart = SlideContainer.EditedContent.FishingState.TabA.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart != null)
				outputDataPackage.ClipartItems.Add("CP03ACLIPART1", clipart);

			var slideHeader = (SlideContainer.EditedContent.FishingState.TabA.SlideHeader ?? TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var subHeaders = new[]
				{
					SlideContainer.EditedContent.FishingState.TabA.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue,
					SlideContainer.EditedContent.FishingState.TabA.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue
				}
				.Where(item => !String.IsNullOrWhiteSpace(item))
				.ToList();

			switch (subHeaders.Count)
			{
				case 1:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile("CP03A-1.pptx");
					break;
				case 2:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile("CP03A-2.pptx");
					break;
				default:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile("CP03A-1.pptx");
					break;
			}

			outputDataPackage.TextItems.Add("CP03AHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("CP03ASubheader1".ToUpper(), subHeaders.ElementAtOrDefault(0));
			outputDataPackage.TextItems.Add("CP03ASubheader2".ToUpper(), subHeaders.ElementAtOrDefault(1));

			return outputDataPackage;
		}
		#endregion
	}
}