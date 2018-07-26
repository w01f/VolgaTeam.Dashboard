﻿using System;
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
	public sealed partial class VideoTabDControl : ChildTabBaseControl
	{
		private VideoTabDInfo CustomTabInfo => (VideoTabDInfo)TabInfo;

		public VideoTabDControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			memoEditTabDSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			memoEditTabDSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditTabDSubheader1.Properties.NullText;

			clipartEditContainerTabD1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabD1.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainerTabD1.LoadData(SlideContainer.EditedContent.VideoState.TabD.Clipart1);
			memoEditTabDSubheader1.EditValue = SlideContainer.EditedContent.VideoState.TabD.Subheader1 ??
											   CustomTabInfo.SubHeader1DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.VideoState.TabD.Clipart1 = clipartEditContainerTabD1.GetActiveClipartObject();

			SlideContainer.EditedContent.VideoState.TabD.Subheader1 = memoEditTabDSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				memoEditTabDSubheader1.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.VideoState.TabD.SlideHeader ??
				   TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.VideoState.TabD.SlideHeader =
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

			var clipart = SlideContainer.EditedContent.VideoState.TabD.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart != null)
				outputDataPackage.ClipartItems.Add("CP08DCLIPART1", clipart);

			var slideHeader = (SlideContainer.EditedContent.VideoState.TabD.SlideHeader ?? TabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var subHeader1 = SlideContainer.EditedContent.VideoState.TabD.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue;

			outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarVideoFile(!String.IsNullOrEmpty(subHeader1) ? "CP08D-1.pptx" : "CP08D-2.pptx");

			outputDataPackage.TextItems.Add("CP08DHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("CP08DSubheader1".ToUpper(), subHeader1);

			return outputDataPackage;
		}
		#endregion
	}
}