using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Configuration.Market;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Market
{
	[ToolboxItem(false)]
	public sealed partial class MarketTabBControl : ChildTabBaseControl
	{
		private MarketTabBInfo CustomTabInfo => (MarketTabBInfo)TabInfo;

		public MarketTabBControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			textEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			textEditTabBSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? textEditTabBSubheader1.Properties.NullText;
			memoEditTabBSubheader2.Properties.NullText = CustomTabInfo.SubHeader2Placeholder ?? memoEditTabBSubheader2.Properties.NullText;

			clipartEditContainerTabB1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabB1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB2.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image), CustomTabInfo.Clipart2Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabB2.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB3.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image), CustomTabInfo.Clipart3Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabB3.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB4.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart4Image), CustomTabInfo.Clipart4Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabB4.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB5.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart5Image), CustomTabInfo.Clipart5Configuration, TabPageContainer.ParentControl);
			clipartEditContainerTabB5.EditValueChanged += OnEditValueChanged;
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainerTabB1.LoadData(SlideContainer.EditedContent.MarketState.TabB.Clipart1);
			clipartEditContainerTabB2.LoadData(SlideContainer.EditedContent.MarketState.TabB.Clipart2);
			clipartEditContainerTabB3.LoadData(SlideContainer.EditedContent.MarketState.TabB.Clipart3);
			clipartEditContainerTabB4.LoadData(SlideContainer.EditedContent.MarketState.TabB.Clipart4);
			clipartEditContainerTabB5.LoadData(SlideContainer.EditedContent.MarketState.TabB.Clipart5);
			textEditTabBSubheader1.EditValue = SlideContainer.EditedContent.MarketState.TabB.Subheader1 ??
											   CustomTabInfo.SubHeader1DefaultValue;
			memoEditTabBSubheader2.EditValue = SlideContainer.EditedContent.MarketState.TabB.Subheader2 ??
											   CustomTabInfo.SubHeader2DefaultValue;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.MarketState.TabB.Clipart1 = clipartEditContainerTabB1.GetActiveClipartObject();
			SlideContainer.EditedContent.MarketState.TabB.Clipart2 = clipartEditContainerTabB2.GetActiveClipartObject();
			SlideContainer.EditedContent.MarketState.TabB.Clipart3 = clipartEditContainerTabB3.GetActiveClipartObject();
			SlideContainer.EditedContent.MarketState.TabB.Clipart4 = clipartEditContainerTabB4.GetActiveClipartObject();
			SlideContainer.EditedContent.MarketState.TabB.Clipart5 = clipartEditContainerTabB5.GetActiveClipartObject();

			SlideContainer.EditedContent.MarketState.TabB.Subheader1 = textEditTabBSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue ?
				textEditTabBSubheader1.EditValue as String ?? String.Empty :
				null;
			SlideContainer.EditedContent.MarketState.TabB.Subheader2 = memoEditTabBSubheader2.EditValue as String != CustomTabInfo.SubHeader2DefaultValue ?
				memoEditTabBSubheader2.EditValue as String ?? String.Empty :
				null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.MarketState.TabB.SlideHeader ??
				   CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.MarketState.TabB.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.MarketState.TabB.SlideHeader =
				slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.MarketState.TabB.EnableOutput =
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

			var clipart1 = SlideContainer.EditedContent.MarketState.TabB.Clipart1 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart1 != null)
				outputDataPackage.ClipartItems.Add("CP07BCLIPART1", clipart1);

			var clipart2 = SlideContainer.EditedContent.MarketState.TabB.Clipart2 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart2Image);
			if (clipart2 != null)
				outputDataPackage.ClipartItems.Add("CP07BCLIPART2", clipart2);

			var clipart3 = SlideContainer.EditedContent.MarketState.TabB.Clipart3 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart3Image);
			if (clipart3 != null)
				outputDataPackage.ClipartItems.Add("CP07BCLIPART3", clipart3);

			var clipart4 = SlideContainer.EditedContent.MarketState.TabB.Clipart4 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart4Image);
			if (clipart4 != null)
				outputDataPackage.ClipartItems.Add("CP07BCLIPART4", clipart4);

			var clipart5 = SlideContainer.EditedContent.MarketState.TabB.Clipart5 ?? ImageClipartObject.FromImage(CustomTabInfo.Clipart5Image);
			if (clipart5 != null)
				outputDataPackage.ClipartItems.Add("CP07BCLIPART5", clipart5);

			var slideHeader = (SlideContainer.EditedContent.MarketState.TabB.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var subHeader1 = SlideContainer.EditedContent.MarketState.TabB.Subheader1 ?? CustomTabInfo.SubHeader1DefaultValue;
			var subHeader2 = SlideContainer.EditedContent.MarketState.TabB.Subheader2 ?? CustomTabInfo.SubHeader2DefaultValue;

			outputDataPackage.TextItems.Add("CP07BHEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
			outputDataPackage.TextItems.Add("CP07BSubHeader1".ToUpper(), subHeader1);
			outputDataPackage.TextItems.Add("CP07BSubHeader2".ToUpper(), subHeader2);

			if (clipart1 != null &&
				clipart2 != null &&
				clipart3 != null &&
				clipart4 != null &&
				clipart5 != null)
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile(!String.IsNullOrWhiteSpace(subHeader1) && !String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-1.pptx" : (!String.IsNullOrWhiteSpace(subHeader1) ? "CP07B-6.pptx" : (!String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-11.pptx" : "CP07B-16.pptx")));
			else if (clipart1 != null &&
					 clipart2 != null &&
					 clipart3 != null &&
					 clipart4 != null)
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile(!String.IsNullOrWhiteSpace(subHeader1) && !String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-2.pptx" : (!String.IsNullOrWhiteSpace(subHeader1) ? "CP07B-7.pptx" : (!String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-12.pptx" : "CP07B-17.pptx")));
			else if (clipart1 != null &&
					 clipart2 != null &&
					 clipart3 != null)
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile(!String.IsNullOrWhiteSpace(subHeader1) && !String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-3.pptx" : (!String.IsNullOrWhiteSpace(subHeader1) ? "CP07B-8.pptx" : (!String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-13.pptx" : "CP07B-18.pptx")));
			else if (clipart1 != null &&
					 clipart2 != null)
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile(!String.IsNullOrWhiteSpace(subHeader1) && !String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-4.pptx" : (!String.IsNullOrWhiteSpace(subHeader1) ? "CP07B-9.pptx" : (!String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-14.pptx" : "CP07B-19.pptx")));
			else if (clipart1 != null)
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile(!String.IsNullOrWhiteSpace(subHeader1) && !String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-5.pptx" : (!String.IsNullOrWhiteSpace(subHeader1) ? "CP07B-10.pptx" : (!String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-15.pptx" : "CP07B-20.pptx")));

			return outputDataPackage;
		}
		#endregion
	}
}