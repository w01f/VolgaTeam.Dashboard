using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using DevExpress.Skins;
using DevExpress.XtraLayout;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class MarketControl : StarAppControl
	{
		public override SlideType SlideType => SlideType.StarAppMarket;
		public override string OutputName => SlideContainer.StarInfo.Titles.Tab7Title;

		public MarketControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabASubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab7SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab7SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab7SubCTitle;

			pictureEditTabAClipart1.Image = SlideContainer.StarInfo.Tab7SubAClipart1Image;
			pictureEditTabAClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabBClipart1.Image = SlideContainer.StarInfo.Tab7SubBClipart1Image;
			pictureEditTabBClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = SlideContainer.StarInfo.Tab7SubBClipart2Image;
			pictureEditTabBClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartBClipart2Configuration.Alignment;
			pictureEditTabBClipart3.Image = SlideContainer.StarInfo.Tab7SubBClipart3Image;
			pictureEditTabBClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartBClipart3Configuration.Alignment;
			pictureEditTabBClipart4.Image = SlideContainer.StarInfo.Tab7SubBClipart4Image;
			pictureEditTabBClipart4.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartBClipart4Configuration.Alignment;
			pictureEditTabBClipart5.Image = SlideContainer.StarInfo.Tab7SubBClipart5Image;
			pictureEditTabBClipart5.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartBClipart5Configuration.Alignment;
			pictureEditTabCClipart1.Image = SlideContainer.StarInfo.Tab7SubCClipart1Image;
			pictureEditTabCClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartCClipart1Configuration.Alignment;
			pictureEditTabCClipart2.Image = SlideContainer.StarInfo.Tab7SubCClipart2Image;
			pictureEditTabCClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartCClipart2Configuration.Alignment;
			pictureEditTabCClipart3.Image = SlideContainer.StarInfo.Tab7SubCClipart3Image;
			pictureEditTabCClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartCClipart3Configuration.Alignment;
			pictureEditTabCClipart4.Image = SlideContainer.StarInfo.Tab7SubCClipart4Image;
			pictureEditTabCClipart4.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartCClipart4Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabAClipart1,
				pictureEditTabBClipart1,
				pictureEditTabBClipart2,
				pictureEditTabBClipart3,
				pictureEditTabBClipart4,
				pictureEditTabBClipart5,
				pictureEditTabCClipart1,
				pictureEditTabCClipart2,
				pictureEditTabCClipart3,
				pictureEditTabCClipart4,
			});

			memoEditTabASubheader1.Properties.NullText = SlideContainer.StarInfo.MarketConfiguration.PartASubHeader1Placeholder ?? memoEditTabASubheader1.Properties.NullText;

			textEditTabBSubheader1.Properties.NullText = SlideContainer.StarInfo.MarketConfiguration.PartBSubHeader1Placeholder ?? textEditTabBSubheader1.Properties.NullText;
			memoEditTabBSubheader2.Properties.NullText = SlideContainer.StarInfo.MarketConfiguration.PartBSubHeader2Placeholder ?? memoEditTabBSubheader2.Properties.NullText;

			comboBoxEditTabCCombo1.Properties.Items.AddRange(SlideContainer.StarInfo.MarketConfiguration.PartCCombo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo1.Properties.NullText =
				SlideContainer.StarInfo.MarketConfiguration.PartCCombo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo1.Properties.NullText;

			_outputProcessors.AddRange(OutputProcessor.GetOutputProcessors(this));

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;
			pictureEditTabAClipart1.Image = SlideContainer.EditedContent.MarketState.TabA.Clipart1 ??
				pictureEditTabAClipart1.Image;
			memoEditTabASubheader1.EditValue = SlideContainer.EditedContent.MarketState.TabA.Subheader1 ??
				SlideContainer.StarInfo.MarketConfiguration.PartASubHeader1DefaultValue;

			pictureEditTabBClipart1.Image = SlideContainer.EditedContent.MarketState.TabB.Clipart1 ??
				pictureEditTabBClipart1.Image;
			pictureEditTabBClipart2.Image = SlideContainer.EditedContent.MarketState.TabB.Clipart2 ??
				pictureEditTabBClipart2.Image;
			pictureEditTabBClipart3.Image = SlideContainer.EditedContent.MarketState.TabB.Clipart3 ??
				pictureEditTabBClipart3.Image;
			pictureEditTabBClipart4.Image = SlideContainer.EditedContent.MarketState.TabB.Clipart4 ??
				pictureEditTabBClipart4.Image;
			pictureEditTabBClipart5.Image = SlideContainer.EditedContent.MarketState.TabB.Clipart5 ??
				pictureEditTabBClipart5.Image;
			textEditTabBSubheader1.EditValue = SlideContainer.EditedContent.MarketState.TabB.Subheader1 ??
				SlideContainer.StarInfo.MarketConfiguration.PartBSubHeader1DefaultValue;
			memoEditTabBSubheader2.EditValue = SlideContainer.EditedContent.MarketState.TabB.Subheader2 ??
				SlideContainer.StarInfo.MarketConfiguration.PartBSubHeader2DefaultValue;

			pictureEditTabCClipart1.Image = SlideContainer.EditedContent.MarketState.TabC.Clipart1 ??
				pictureEditTabCClipart1.Image;
			pictureEditTabCClipart2.Image = SlideContainer.EditedContent.MarketState.TabC.Clipart2 ??
				pictureEditTabCClipart2.Image;
			pictureEditTabCClipart3.Image = SlideContainer.EditedContent.MarketState.TabC.Clipart3 ??
				pictureEditTabCClipart3.Image;
			pictureEditTabCClipart4.Image = SlideContainer.EditedContent.MarketState.TabC.Clipart4 ??
				pictureEditTabCClipart4.Image;
			comboBoxEditTabCCombo1.EditValue = SlideContainer.EditedContent.MarketState.TabC.Combo1 ??
				SlideContainer.StarInfo.MarketConfiguration.PartCCombo1Items.FirstOrDefault(item => item.IsDefault);

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					SlideContainer.EditedContent.MarketState.TabA.SlideHeader = SlideContainer.StarInfo.MarketConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.MarketState.TabA.Clipart1 = pictureEditTabAClipart1.Image != SlideContainer.StarInfo.Tab7SubAClipart1Image ?
						pictureEditTabAClipart1.Image :
						null;

					SlideContainer.EditedContent.MarketState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.StarInfo.MarketConfiguration.PartASubHeader1DefaultValue ?
						memoEditTabASubheader1.EditValue as String ?? String.Empty :
						null;
					break;
				case 1:
					SlideContainer.EditedContent.MarketState.TabB.SlideHeader = SlideContainer.StarInfo.MarketConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.MarketState.TabB.Clipart1 = pictureEditTabBClipart1.Image != SlideContainer.StarInfo.Tab7SubBClipart1Image ?
						pictureEditTabBClipart1.Image :
						null;
					SlideContainer.EditedContent.MarketState.TabB.Clipart2 = pictureEditTabBClipart2.Image != SlideContainer.StarInfo.Tab7SubBClipart2Image ?
						pictureEditTabBClipart2.Image :
						null;
					SlideContainer.EditedContent.MarketState.TabB.Clipart3 = pictureEditTabBClipart3.Image != SlideContainer.StarInfo.Tab7SubBClipart3Image ?
						pictureEditTabBClipart3.Image :
						null;
					SlideContainer.EditedContent.MarketState.TabB.Clipart4 = pictureEditTabBClipart4.Image != SlideContainer.StarInfo.Tab7SubBClipart4Image ?
						pictureEditTabBClipart4.Image :
						null;
					SlideContainer.EditedContent.MarketState.TabB.Clipart5 = pictureEditTabBClipart5.Image != SlideContainer.StarInfo.Tab7SubBClipart5Image ?
						pictureEditTabBClipart5.Image :
						null;

					SlideContainer.EditedContent.MarketState.TabB.Subheader1 = textEditTabBSubheader1.EditValue as String != SlideContainer.StarInfo.MarketConfiguration.PartBSubHeader1DefaultValue ?
						textEditTabBSubheader1.EditValue as String ?? String.Empty :
						null;
					SlideContainer.EditedContent.MarketState.TabB.Subheader2 = memoEditTabBSubheader2.EditValue as String != SlideContainer.StarInfo.MarketConfiguration.PartBSubHeader2DefaultValue ?
						memoEditTabBSubheader2.EditValue as String ?? String.Empty :
						null;
					break;
				case 2:
					SlideContainer.EditedContent.MarketState.TabC.SlideHeader = SlideContainer.StarInfo.MarketConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.MarketState.TabC.Clipart1 = pictureEditTabCClipart1.Image != SlideContainer.StarInfo.Tab7SubCClipart1Image ?
						pictureEditTabCClipart1.Image :
						null;
					SlideContainer.EditedContent.MarketState.TabC.Clipart2 = pictureEditTabCClipart2.Image != SlideContainer.StarInfo.Tab7SubCClipart2Image ?
						pictureEditTabCClipart2.Image :
						null;
					SlideContainer.EditedContent.MarketState.TabC.Clipart3 = pictureEditTabCClipart3.Image != SlideContainer.StarInfo.Tab7SubCClipart3Image ?
						pictureEditTabCClipart3.Image :
						null;
					SlideContainer.EditedContent.MarketState.TabC.Clipart4 = pictureEditTabCClipart4.Image != SlideContainer.StarInfo.Tab7SubCClipart4Image ?
						pictureEditTabCClipart4.Image :
						null;

					SlideContainer.EditedContent.MarketState.TabC.Combo1 = SlideContainer.StarInfo.MarketConfiguration.PartCCombo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo1.EditValue ?
						comboBoxEditTabCCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabCCombo1.EditValue as String } :
						null;
					break;
			}

			_dataChanged = false;
		}

		private void LoadPartData()
		{
			_allowToSave = false;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab7SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab7SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.MarketConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.MarketState.TabA.SlideHeader ??
						SlideContainer.StarInfo.MarketConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.MarketConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab7SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab7SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.MarketConfiguration.HeadersPartBItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.MarketState.TabB.SlideHeader ??
						SlideContainer.StarInfo.MarketConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.MarketConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab7SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab7SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.MarketConfiguration.HeadersPartCItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.MarketState.TabC.SlideHeader ??
						SlideContainer.StarInfo.MarketConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.MarketConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
			}
			_allowToSave = true;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			_dataChanged = true;
			SlideContainer.RaiseDataChanged();
		}

		private void OnSelectedPageChanging(object sender, LayoutTabPageChangingEventArgs e)
		{
			if (_allowToSave)
				ApplyChanges();
		}

		private void OnSelectedPageChanged(object sender, LayoutTabPageChangedEventArgs e)
		{
			LoadPartData();
		}

		private void OnTabbedGroupClick(object sender, EventArgs e)
		{
			labelFocusFake.Focus();
		}

		private void OnResize(object sender, EventArgs e)
		{
			panelLogoRight.Visible = panelLogoBottom.Visible = Width > 1000;
		}
	}
}