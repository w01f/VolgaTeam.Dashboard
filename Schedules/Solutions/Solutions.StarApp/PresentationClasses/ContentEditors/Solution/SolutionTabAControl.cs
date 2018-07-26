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
	public sealed partial class SolutionTabAControl : ChildTabBaseControl
	{
		private SolutionTabAInfo CustomTabInfo => (SolutionTabAInfo)TabInfo;

		public SolutionTabAControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			memoEditTabASubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			memoEditTabASubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditTabASubheader1.Properties.NullText;

			clipartEditContainerTabA1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabA1.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainerTabA1.LoadData(SlideContainer.EditedContent.SolutionState.TabA.Clipart1);
			memoEditTabASubheader1.EditValue = SlideContainer.EditedContent.SolutionState.TabA.Subheader1 ??
											   CustomTabInfo.SubHeader1DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.SolutionState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();

			SlideContainer.EditedContent.SolutionState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				memoEditTabASubheader1.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.SolutionState.TabA.SlideHeader ??
				   TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.SolutionState.TabA.SlideHeader =
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

			var clipart = SlideContainer.EditedContent.SolutionState.TabA.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart != null)
				outputDataPackage.ClipartItems.Add("CP10ACLIPART1", clipart);

			var slideHeader = (SlideContainer.EditedContent.SolutionState.TabA.SlideHeader ?? TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var subHeader1 = SlideContainer.EditedContent.SolutionState.TabA.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue;

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10A-1.pptx");

			outputDataPackage.TextItems.Add("CP10AHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("CP10ASubheader1".ToUpper(), subHeader1);

			return outputDataPackage;
		}
		#endregion
	}
}