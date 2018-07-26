using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Configuration.Solution;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Solution
{
	[ToolboxItem(false)]
	public sealed partial class SolutionTabCControl : ChildTabBaseControl
	{
		private SolutionTabCInfo CustomTabInfo => (SolutionTabCInfo)TabInfo;

		public SolutionTabCControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			memoEditTabCSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabCSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			memoEditTabCSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditTabCSubheader1.Properties.NullText;
			memoEditTabCSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? memoEditTabCSubheader2.Properties.NullText;

			clipartEditContainerTabC1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabC1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabC2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabC2.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainerTabC1.LoadData(SlideContainer.EditedContent.SolutionState.TabC.Clipart1);
			clipartEditContainerTabC2.LoadData(SlideContainer.EditedContent.SolutionState.TabC.Clipart2);
			memoEditTabCSubheader1.EditValue = SlideContainer.EditedContent.SolutionState.TabC.Subheader1 ??
											   CustomTabInfo.SubHeader1DefaultValue;
			memoEditTabCSubheader2.EditValue = SlideContainer.EditedContent.SolutionState.TabC.Subheader2 ??
											   CustomTabInfo.SubHeader2DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.SolutionState.TabC.Clipart1 = clipartEditContainerTabC1.GetActiveClipartObject();
			SlideContainer.EditedContent.SolutionState.TabC.Clipart2 = clipartEditContainerTabC2.GetActiveClipartObject();

			SlideContainer.EditedContent.SolutionState.TabC.Subheader1 = memoEditTabCSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				memoEditTabCSubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.SolutionState.TabC.Subheader2 = memoEditTabCSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue ?
				memoEditTabCSubheader2.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.SolutionState.TabC.SlideHeader ??
				   TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.SolutionState.TabC.SlideHeader =
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

			var clipart1 = SlideContainer.EditedContent.SolutionState.TabC.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP10CCLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.SolutionState.TabC.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP10CCLIPART2", clipart2);

			var slideHeader = (SlideContainer.EditedContent.SolutionState.TabC.SlideHeader ?? TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;

			var subHeaders = new[]
				{
						SlideContainer.EditedContent.SolutionState.TabC.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue,
						SlideContainer.EditedContent.SolutionState.TabC.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue
					}.Where(item => !String.IsNullOrWhiteSpace(item))
						.ToList();

			outputDataPackage.TextItems.Add("CP10CHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("CP10CSubheader1".ToUpper(), subHeaders.ElementAtOrDefault(0));
			outputDataPackage.TextItems.Add("CP10CSubheader2".ToUpper(), subHeaders.ElementAtOrDefault(1));

			if (clipart1 != null &&
				clipart2 != null)
				outputDataPackage.TemplateName = subHeaders.Count > 1 ?
					MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10C-1.pptx") :
					MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10C-2.pptx");
			else if (clipart1 != null)
				outputDataPackage.TemplateName = subHeaders.Count > 1 ?
					MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10C-3.pptx") :
					MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10C-4.pptx");
			else
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10C-5.pptx");
			return outputDataPackage;
		}
		#endregion
	}
}