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
	public sealed partial class CustomerTabBControl : ChildTabBaseControl
	{
		private CustomerTabBInfo CustomTabInfo => (CustomerTabBInfo)TabInfo;

		public CustomerTabBControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			memoEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			memoEditTabBSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditTabBSubheader1.Properties.NullText;
			memoEditTabBSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? memoEditTabBSubheader2.Properties.NullText;

			clipartEditContainerTabB1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabB1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabB2.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainerTabB1.LoadData(SlideContainer.EditedContent.CustomerState.TabB.Clipart1);
			clipartEditContainerTabB2.LoadData(SlideContainer.EditedContent.CustomerState.TabB.Clipart2);
			memoEditTabBSubheader1.EditValue = SlideContainer.EditedContent.CustomerState.TabB.Subheader1 ??
											   CustomTabInfo.SubHeader1DefaultValue;
			memoEditTabBSubheader2.EditValue = SlideContainer.EditedContent.CustomerState.TabB.Subheader2 ??
											   CustomTabInfo.SubHeader2DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.CustomerState.TabB.Clipart1 = clipartEditContainerTabB1.GetActiveClipartObject();
			SlideContainer.EditedContent.CustomerState.TabB.Clipart2 = clipartEditContainerTabB2.GetActiveClipartObject();

			SlideContainer.EditedContent.CustomerState.TabB.Subheader1 = memoEditTabBSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				memoEditTabBSubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.CustomerState.TabB.Subheader2 = memoEditTabBSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue ?
				memoEditTabBSubheader2.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.CustomerState.TabB.SlideHeader ??
				   CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.CustomerState.TabB.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.CustomerState.TabB.SlideHeader =
				slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.CustomerState.TabB.EnableOutput =
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

			var clipart1 = SlideContainer.EditedContent.CustomerState.TabB.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP04BCLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.CustomerState.TabB.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP04BCLIPART2", clipart2);

			var slideHeader = (SlideContainer.EditedContent.CustomerState.TabB.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var subHeaders = new[]
				{
						SlideContainer.EditedContent.CustomerState.TabB.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue,
						SlideContainer.EditedContent.CustomerState.TabB.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue
					}
				.Where(item => !String.IsNullOrWhiteSpace(item))
				.ToList();

			switch (subHeaders.Count)
			{
				case 1:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04B-2.pptx" : (clipart1 != null ? "CP04B-4.pptx" : "CP04B-6.pptx"));
					break;
				case 2:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04B-1.pptx" : (clipart1 != null ? "CP04B-3.pptx" : "CP04B-5.pptx"));
					break;
				default:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04B-2.pptx" : (clipart1 != null ? "CP04B-4.pptx" : "CP04B-6.pptx"));
					break;
			}

			outputDataPackage.TextItems.Add("CP04BHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("CP04BSubheader1".ToUpper(), subHeaders.ElementAtOrDefault(0));
			outputDataPackage.TextItems.Add("CP04BSubheader2".ToUpper(), subHeaders.ElementAtOrDefault(1));

			return outputDataPackage;
		}
		#endregion
	}
}