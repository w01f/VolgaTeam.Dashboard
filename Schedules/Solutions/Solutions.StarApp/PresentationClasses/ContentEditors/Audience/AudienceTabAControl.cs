using System;
using System.Collections.Generic;
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
	public sealed partial class AudienceTabAControl : ChildTabBaseControl
	{
		private AudienceTabAInfo CustomTabInfo => (AudienceTabAInfo)TabInfo;

		public AudienceTabAControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			memoEditTabASubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabASubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			memoEditTabASubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditTabASubheader1.Properties.NullText;
			memoEditTabASubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? memoEditTabASubheader2.Properties.NullText;

			clipartEditContainerTabA1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabA1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabA2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabA2.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainerTabA1.LoadData(SlideContainer.EditedContent.AudienceState.TabA.Clipart1);
			clipartEditContainerTabA2.LoadData(SlideContainer.EditedContent.AudienceState.TabA.Clipart2);
			memoEditTabASubheader1.EditValue = SlideContainer.EditedContent.AudienceState.TabA.Subheader1 ??
											   CustomTabInfo.SubHeader1DefaultValue;
			memoEditTabASubheader2.EditValue = SlideContainer.EditedContent.AudienceState.TabA.Subheader2 ??
											   CustomTabInfo.SubHeader2DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.AudienceState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();
			SlideContainer.EditedContent.AudienceState.TabA.Clipart2 = clipartEditContainerTabA2.GetActiveClipartObject();

			SlideContainer.EditedContent.AudienceState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				memoEditTabASubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.AudienceState.TabA.Subheader2 = memoEditTabASubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue ?
				memoEditTabASubheader2.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.AudienceState.TabA.SlideHeader ??
				   TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.AudienceState.TabA.SlideHeader =
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

			var clipart1 = SlideContainer.EditedContent.AudienceState.TabA.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP09ACLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.AudienceState.TabA.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP09ACLIPART2", clipart2);

			var slideHeader = SlideContainer.EditedContent.AudienceState.TabA.SlideHeader?.Value ?? TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault)?.Value;
			outputDataPackage.TextItems.Add("CP09AHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);

			var textItemKeys = new[]
			{
					"CP09ASubheader1",
					"CP09ASubheader2"
				};

			var textItemValues = new List<string>();
			var subHeader1 = SlideContainer.EditedContent.AudienceState.TabA.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue;
			if (!String.IsNullOrWhiteSpace(subHeader1))
				textItemValues.Add(subHeader1);

			var subHeader2 = SlideContainer.EditedContent.AudienceState.TabA.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue;
			if (!String.IsNullOrWhiteSpace(subHeader2))
				textItemValues.Add(subHeader2);

			switch (textItemValues.Count)
			{
				case 1:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile(clipart1 != null ? "CP09A-2.pptx" : "CP09A-4.pptx");
					break;
				case 2:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile(clipart1 != null ? "CP09A-1.pptx" : "CP09A-3.pptx");
					break;
				default:
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile(clipart1 != null ? "CP09A-2.pptx" : "CP09A-4.pptx");
					break;
			}

			for (int i = 0; i < textItemKeys.Length; i++)
				outputDataPackage.TextItems.Add(textItemKeys[i].ToUpper(), textItemValues.ElementAtOrDefault(i) ?? String.Empty);

			return outputDataPackage;
		}
		#endregion
	}
}