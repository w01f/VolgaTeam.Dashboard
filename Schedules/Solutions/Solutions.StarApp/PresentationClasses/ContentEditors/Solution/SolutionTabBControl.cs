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
	public sealed partial class SolutionTabBControl : ChildTabBaseControl
	{
		private SolutionTabBInfo CustomTabInfo => (SolutionTabBInfo)TabInfo;

		public SolutionTabBControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			memoEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			memoEditTabBSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditTabBSubheader1.Properties.NullText;

			clipartEditContainerTabB1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabB1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabB2.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabB3.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainerTabB1.LoadData(SlideContainer.EditedContent.SolutionState.TabB.Clipart1);
			clipartEditContainerTabB2.LoadData(SlideContainer.EditedContent.SolutionState.TabB.Clipart2);
			clipartEditContainerTabB3.LoadData(SlideContainer.EditedContent.SolutionState.TabB.Clipart3);
			memoEditTabBSubheader1.EditValue = SlideContainer.EditedContent.SolutionState.TabB.Subheader1 ??
											   CustomTabInfo.SubHeader1DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.SolutionState.TabB.Clipart1 = clipartEditContainerTabB1.GetActiveClipartObject();
			SlideContainer.EditedContent.SolutionState.TabB.Clipart2 = clipartEditContainerTabB2.GetActiveClipartObject();
			SlideContainer.EditedContent.SolutionState.TabB.Clipart3 = clipartEditContainerTabB3.GetActiveClipartObject();

			SlideContainer.EditedContent.SolutionState.TabB.Subheader1 = memoEditTabBSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				memoEditTabBSubheader1.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.SolutionState.TabB.SlideHeader ??
				   CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.SolutionState.TabB.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.SolutionState.TabB.SlideHeader =
				slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.SolutionState.TabB.EnableOutput =
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

			var clipart1 = SlideContainer.EditedContent.SolutionState.TabB.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP10BCLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.SolutionState.TabB.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP10BCLIPART2", clipart2);

			var clipart3 = SlideContainer.EditedContent.SolutionState.TabB.Clipart3 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP10BCLIPART3", clipart3);

			var slideHeader = (SlideContainer.EditedContent.SolutionState.TabB.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var subHeader1 = SlideContainer.EditedContent.SolutionState.TabB.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue;

			outputDataPackage.TextItems.Add("CP10BHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("CP10BSubheader1".ToUpper(), subHeader1);

			if (clipart1 != null &&
			   clipart2 != null &&
			   clipart3 != null)
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10B-3.pptx");
			else if (clipart1 != null &&
					 clipart2 != null)
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10B-2.pptx");
			else
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10B-1.pptx");

			return outputDataPackage;
		}
		#endregion
	}
}