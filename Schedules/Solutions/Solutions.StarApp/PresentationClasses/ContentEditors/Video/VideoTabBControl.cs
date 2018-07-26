using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Configuration.Video;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Video
{
	[ToolboxItem(false)]
	public sealed partial class VideoTabBControl : ChildTabBaseControl
	{
		private VideoTabBInfo CustomTabInfo => (VideoTabBInfo)TabInfo;

		public VideoTabBControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			memoEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			memoEditTabBSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditTabBSubheader1.Properties.NullText;

			clipartEditContainerTabB1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabB1.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainerTabB1.LoadData(SlideContainer.EditedContent.VideoState.TabB.Clipart1);
			memoEditTabBSubheader1.EditValue = SlideContainer.EditedContent.VideoState.TabB.Subheader1 ??
											   CustomTabInfo.SubHeader1DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.VideoState.TabB.Clipart1 = clipartEditContainerTabB1.GetActiveClipartObject();

			SlideContainer.EditedContent.VideoState.TabB.Subheader1 = memoEditTabBSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				memoEditTabBSubheader1.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.VideoState.TabB.SlideHeader ??
				   TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.VideoState.TabB.SlideHeader =
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

			var clipart = SlideContainer.EditedContent.VideoState.TabB.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart != null)
				outputDataPackage.ClipartItems.Add("CP08BCLIPART1", clipart);

			var slideHeader = (SlideContainer.EditedContent.VideoState.TabB.SlideHeader ?? TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var subHeader1 = SlideContainer.EditedContent.VideoState.TabB.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue;

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarVideoFile("CP08B-1.pptx");

			outputDataPackage.TextItems.Add("CP08BHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("CP08BSubheader1".ToUpper(), subHeader1);

			return outputDataPackage;
		}
		#endregion
	}
}