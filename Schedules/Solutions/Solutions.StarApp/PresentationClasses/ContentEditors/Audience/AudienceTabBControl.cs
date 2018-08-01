using System;
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
	public sealed partial class AudienceTabBControl : ChildTabBaseControl
	{
		private AudienceTabBInfo CustomTabInfo => (AudienceTabBInfo)TabInfo;

		public AudienceTabBControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			textEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader6.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			textEditTabBSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? textEditTabBSubheader1.Properties.NullText;
			textEditTabBSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? textEditTabBSubheader2.Properties.NullText;
			textEditTabBSubheader3.Properties.NullText = CustomTabInfo.SubHeader3Placeholder ?? textEditTabBSubheader3.Properties.NullText;
			memoEditTabBSubheader4.Properties.NullText = CustomTabInfo.SubHeader4Placeholder ?? memoEditTabBSubheader4.Properties.NullText;
			memoEditTabBSubheader5.Properties.NullText = CustomTabInfo.SubHeader5Placeholder ?? memoEditTabBSubheader5.Properties.NullText;
			memoEditTabBSubheader6.Properties.NullText = CustomTabInfo.SubHeader6Placeholder ?? memoEditTabBSubheader6.Properties.NullText;

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

			clipartEditContainerTabB1.LoadData(SlideContainer.EditedContent.AudienceState.TabB.Clipart1);
			clipartEditContainerTabB2.LoadData(SlideContainer.EditedContent.AudienceState.TabB.Clipart2);
			clipartEditContainerTabB3.LoadData(SlideContainer.EditedContent.AudienceState.TabB.Clipart3);
			textEditTabBSubheader1.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader1 ??
											   CustomTabInfo.SubHeader1DefaultValue;
			textEditTabBSubheader2.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader2 ??
											   CustomTabInfo.SubHeader2DefaultValue;
			textEditTabBSubheader3.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader3 ??
											   CustomTabInfo.SubHeader3DefaultValue;
			memoEditTabBSubheader4.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader4 ??
											   CustomTabInfo.SubHeader4DefaultValue;
			memoEditTabBSubheader5.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader5 ??
											   CustomTabInfo.SubHeader5DefaultValue;
			memoEditTabBSubheader6.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader6 ??
											   CustomTabInfo.SubHeader6DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.AudienceState.TabB.Clipart1 = clipartEditContainerTabB1.GetActiveClipartObject();
			SlideContainer.EditedContent.AudienceState.TabB.Clipart2 = clipartEditContainerTabB2.GetActiveClipartObject();
			SlideContainer.EditedContent.AudienceState.TabB.Clipart3 = clipartEditContainerTabB3.GetActiveClipartObject();

			SlideContainer.EditedContent.AudienceState.TabB.Subheader1 = textEditTabBSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				textEditTabBSubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.AudienceState.TabB.Subheader2 = textEditTabBSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue ?
				textEditTabBSubheader2.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.AudienceState.TabB.Subheader3 = textEditTabBSubheader3.EditValue as String != CustomTabInfo.SubHeader3DefaultValue ?
				textEditTabBSubheader3.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.AudienceState.TabB.Subheader4 = memoEditTabBSubheader4.EditValue as String != CustomTabInfo.SubHeader4DefaultValue ?
				memoEditTabBSubheader4.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.AudienceState.TabB.Subheader5 = memoEditTabBSubheader4.EditValue as String != CustomTabInfo.SubHeader5DefaultValue ?
				memoEditTabBSubheader5.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.AudienceState.TabB.Subheader6 = memoEditTabBSubheader4.EditValue as String != CustomTabInfo.SubHeader6DefaultValue ?
				memoEditTabBSubheader6.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.AudienceState.TabB.SlideHeader ??
				   CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.AudienceState.TabB.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.AudienceState.TabB.SlideHeader =
				slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.AudienceState.TabB.EnableOutput =
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
			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09B-1.pptx");

			var clipart1 = SlideContainer.EditedContent.AudienceState.TabB.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP09BCLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.AudienceState.TabB.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP09BCLIPART2", clipart2);

			var clipart3 = SlideContainer.EditedContent.AudienceState.TabB.Clipart3 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP09BCLIPART3", clipart3);

			var slideHeader = (SlideContainer.EditedContent.AudienceState.TabB.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			outputDataPackage.TextItems.Add("CP09BHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);

			var subHeader1 = SlideContainer.EditedContent.AudienceState.TabB.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue;
			var subHeader2 = SlideContainer.EditedContent.AudienceState.TabB.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue;
			var subHeader3 = SlideContainer.EditedContent.AudienceState.TabB.Subheader3 ?? CustomTabInfo.SubHeader3DefaultValue;
			var subHeader4 = SlideContainer.EditedContent.AudienceState.TabB.Subheader4 ?? CustomTabInfo.SubHeader4DefaultValue;
			var subHeader5 = SlideContainer.EditedContent.AudienceState.TabB.Subheader5 ?? CustomTabInfo.SubHeader5DefaultValue;
			var subHeader6 = SlideContainer.EditedContent.AudienceState.TabB.Subheader6 ?? CustomTabInfo.SubHeader6DefaultValue;

			if (!String.IsNullOrWhiteSpace(subHeader1))
				outputDataPackage.TextItems.Add("CP09BSubHeader1".ToUpper(), subHeader1);
			if (!String.IsNullOrWhiteSpace(subHeader2))
				outputDataPackage.TextItems.Add("CP09BSubHeader2".ToUpper(), subHeader2);
			if (!String.IsNullOrWhiteSpace(subHeader3))
				outputDataPackage.TextItems.Add("CP09BSubHeader3".ToUpper(), subHeader3);
			if (!String.IsNullOrWhiteSpace(subHeader4))
				outputDataPackage.TextItems.Add("CP09BSubHeader4".ToUpper(), subHeader4);
			if (!String.IsNullOrWhiteSpace(subHeader5))
				outputDataPackage.TextItems.Add("CP09BSubHeader5".ToUpper(), subHeader5);
			if (!String.IsNullOrWhiteSpace(subHeader6))
				outputDataPackage.TextItems.Add("CP09BSubHeader6".ToUpper(), subHeader6);

			return outputDataPackage;
		}
		#endregion
	}
}