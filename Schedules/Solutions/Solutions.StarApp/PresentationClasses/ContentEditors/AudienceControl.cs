﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using DevExpress.Skins;
using DevExpress.XtraLayout;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class AudienceControl : StarAppControl
	{
		public override SlideType SlideType => SlideType.StarAppAudience;
		public override string OutputName => SlideContainer.StarInfo.Titles.Tab9Title;

		public AudienceControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabASubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabASubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			textEditTabBSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader5.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader6.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditTabCCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			Application.DoEvents();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab9SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab9SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab9SubCTitle;
			Application.DoEvents();

			pictureEditTabAClipart1.Image = SlideContainer.StarInfo.Tab9SubAClipart1Image;
			pictureEditTabAClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabAClipart2.Image = SlideContainer.StarInfo.Tab9SubAClipart2Image;
			pictureEditTabAClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartAClipart2Configuration.Alignment;
			pictureEditTabBClipart1.Image = SlideContainer.StarInfo.Tab9SubBClipart1Image;
			pictureEditTabBClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = SlideContainer.StarInfo.Tab9SubBClipart2Image;
			pictureEditTabBClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartBClipart2Configuration.Alignment;
			pictureEditTabBClipart3.Image = SlideContainer.StarInfo.Tab9SubBClipart3Image;
			pictureEditTabBClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartBClipart3Configuration.Alignment;
			pictureEditTabCClipart1.Image = SlideContainer.StarInfo.Tab9SubCClipart1Image;
			pictureEditTabCClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartCClipart1Configuration.Alignment;
			pictureEditTabCClipart2.Image = SlideContainer.StarInfo.Tab9SubCClipart2Image;
			pictureEditTabCClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartCClipart2Configuration.Alignment;
			pictureEditTabCClipart3.Image = SlideContainer.StarInfo.Tab9SubCClipart3Image;
			pictureEditTabCClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartCClipart3Configuration.Alignment;
			pictureEditTabCClipart4.Image = SlideContainer.StarInfo.Tab9SubCClipart4Image;
			pictureEditTabCClipart4.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartCClipart4Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabAClipart1,
				pictureEditTabAClipart2,
				pictureEditTabBClipart1,
				pictureEditTabBClipart2,
				pictureEditTabBClipart3,
				pictureEditTabCClipart1,
				pictureEditTabCClipart2,
				pictureEditTabCClipart3,
				pictureEditTabCClipart4,
			});

			Application.DoEvents();
			memoEditTabASubheader1.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader1Placeholder ?? memoEditTabASubheader1.Properties.NullText;
			memoEditTabASubheader2.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader2Placeholder ?? memoEditTabASubheader2.Properties.NullText;
			textEditTabBSubheader1.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader1Placeholder ?? textEditTabBSubheader1.Properties.NullText;
			textEditTabBSubheader2.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader2Placeholder ?? textEditTabBSubheader2.Properties.NullText;
			textEditTabBSubheader3.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader3Placeholder ?? textEditTabBSubheader3.Properties.NullText;
			memoEditTabBSubheader4.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader4Placeholder ?? memoEditTabBSubheader4.Properties.NullText;
			memoEditTabBSubheader5.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader5Placeholder ?? memoEditTabBSubheader5.Properties.NullText;
			memoEditTabBSubheader6.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader6Placeholder ?? memoEditTabBSubheader6.Properties.NullText;

			comboBoxEditTabCCombo1.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceConfiguration.PartCCombo1Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditTabCCombo1.Properties.NullText =
				SlideContainer.StarInfo.AudienceConfiguration.PartCCombo1Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditTabCCombo1.Properties.NullText;
			Application.DoEvents();

			_outputProcessors.AddRange(OutputProcessor.GetOutputProcessors(this));

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabAClipart1.Image = SlideContainer.EditedContent.AudienceState.TabA.Clipart1 ??
				pictureEditTabAClipart1.Image;
			pictureEditTabAClipart2.Image = SlideContainer.EditedContent.AudienceState.TabA.Clipart2 ??
				pictureEditTabAClipart2.Image;
			memoEditTabASubheader1.EditValue = SlideContainer.EditedContent.AudienceState.TabA.Subheader1 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader1DefaultValue;
			memoEditTabASubheader2.EditValue = SlideContainer.EditedContent.AudienceState.TabA.Subheader2 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader2DefaultValue;

			pictureEditTabBClipart1.Image = SlideContainer.EditedContent.AudienceState.TabB.Clipart1 ??
											pictureEditTabBClipart1.Image;
			pictureEditTabBClipart2.Image = SlideContainer.EditedContent.AudienceState.TabB.Clipart2 ??
											pictureEditTabBClipart2.Image;
			pictureEditTabBClipart3.Image = SlideContainer.EditedContent.AudienceState.TabB.Clipart3 ??
											pictureEditTabBClipart3.Image;
			textEditTabBSubheader1.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader1 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader1DefaultValue;
			textEditTabBSubheader2.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader2 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader2DefaultValue;
			textEditTabBSubheader3.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader3 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader3DefaultValue;
			memoEditTabBSubheader4.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader4 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader4DefaultValue;
			memoEditTabBSubheader5.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader5 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader5DefaultValue;
			memoEditTabBSubheader6.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader6 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader6DefaultValue;

			pictureEditTabCClipart1.Image = SlideContainer.EditedContent.AudienceState.TabC.Clipart1 ??
											pictureEditTabCClipart1.Image;
			pictureEditTabCClipart2.Image = SlideContainer.EditedContent.AudienceState.TabC.Clipart2 ??
											pictureEditTabCClipart2.Image;
			pictureEditTabCClipart3.Image = SlideContainer.EditedContent.AudienceState.TabC.Clipart3 ??
											pictureEditTabCClipart3.Image;
			pictureEditTabCClipart4.Image = SlideContainer.EditedContent.AudienceState.TabC.Clipart4 ??
											pictureEditTabCClipart4.Image;
			comboBoxEditTabCCombo1.EditValue = SlideContainer.EditedContent.AudienceState.TabC.Combo1 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartCCombo1Items.FirstOrDefault(item => item.IsDefault);

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					SlideContainer.EditedContent.AudienceState.TabA.SlideHeader = SlideContainer.StarInfo.AudienceConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.AudienceState.TabA.Clipart1 = pictureEditTabAClipart1.Image != SlideContainer.StarInfo.Tab9SubAClipart1Image ?
						pictureEditTabAClipart1.Image :
						null;
					SlideContainer.EditedContent.AudienceState.TabA.Clipart2 = pictureEditTabAClipart2.Image != SlideContainer.StarInfo.Tab9SubAClipart2Image ?
						pictureEditTabAClipart2.Image :
						null;

					SlideContainer.EditedContent.AudienceState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader1DefaultValue ?
						memoEditTabASubheader1.EditValue as String ?? String.Empty :
						null;
					SlideContainer.EditedContent.AudienceState.TabA.Subheader2 = memoEditTabASubheader2.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader2DefaultValue ?
						memoEditTabASubheader2.EditValue as String ?? String.Empty :
						null;
					break;
				case 1:
					SlideContainer.EditedContent.AudienceState.TabB.SlideHeader = SlideContainer.StarInfo.AudienceConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.AudienceState.TabB.Clipart1 = pictureEditTabBClipart1.Image != SlideContainer.StarInfo.Tab9SubBClipart1Image ?
						pictureEditTabBClipart1.Image :
						null;
					SlideContainer.EditedContent.AudienceState.TabB.Clipart2 = pictureEditTabBClipart2.Image != SlideContainer.StarInfo.Tab9SubBClipart2Image ?
						pictureEditTabBClipart2.Image :
						null;
					SlideContainer.EditedContent.AudienceState.TabB.Clipart3 = pictureEditTabBClipart3.Image != SlideContainer.StarInfo.Tab9SubBClipart3Image ?
						pictureEditTabBClipart3.Image :
						null;

					SlideContainer.EditedContent.AudienceState.TabB.Subheader1 = textEditTabBSubheader1.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader1DefaultValue ?
						textEditTabBSubheader1.EditValue as String ?? String.Empty :
						null;
					SlideContainer.EditedContent.AudienceState.TabB.Subheader2 = textEditTabBSubheader2.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader2DefaultValue ?
						textEditTabBSubheader2.EditValue as String ?? String.Empty :
						null;
					SlideContainer.EditedContent.AudienceState.TabB.Subheader3 = textEditTabBSubheader3.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader3DefaultValue ?
						textEditTabBSubheader3.EditValue as String ?? String.Empty :
						null;
					SlideContainer.EditedContent.AudienceState.TabB.Subheader4 = memoEditTabBSubheader4.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader4DefaultValue ?
						memoEditTabBSubheader4.EditValue as String ?? String.Empty :
						null;
					SlideContainer.EditedContent.AudienceState.TabB.Subheader5 = memoEditTabBSubheader4.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader5DefaultValue ?
						memoEditTabBSubheader5.EditValue as String ?? String.Empty :
						null;
					SlideContainer.EditedContent.AudienceState.TabB.Subheader6 = memoEditTabBSubheader4.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader6DefaultValue ?
						memoEditTabBSubheader6.EditValue as String ?? String.Empty :
						null;
					break;
				case 2:
					SlideContainer.EditedContent.AudienceState.TabC.SlideHeader = SlideContainer.StarInfo.AudienceConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.AudienceState.TabC.Clipart1 = pictureEditTabCClipart1.Image != SlideContainer.StarInfo.Tab9SubCClipart1Image ?
						pictureEditTabCClipart1.Image :
						null;
					SlideContainer.EditedContent.AudienceState.TabC.Clipart2 = pictureEditTabCClipart2.Image != SlideContainer.StarInfo.Tab9SubCClipart2Image ?
						pictureEditTabCClipart2.Image :
						null;
					SlideContainer.EditedContent.AudienceState.TabC.Clipart3 = pictureEditTabCClipart3.Image != SlideContainer.StarInfo.Tab9SubCClipart3Image ?
						pictureEditTabCClipart3.Image :
						null;
					SlideContainer.EditedContent.AudienceState.TabC.Clipart4 = pictureEditTabCClipart4.Image != SlideContainer.StarInfo.Tab9SubCClipart4Image ?
						pictureEditTabCClipart4.Image :
						null;

					SlideContainer.EditedContent.AudienceState.TabC.Combo1 = SlideContainer.StarInfo.AudienceConfiguration.PartCCombo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo1.EditValue ?
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
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab9SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab9SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.AudienceState.TabA.SlideHeader ??
						SlideContainer.StarInfo.AudienceConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab9SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab9SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceConfiguration.HeadersPartBItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.AudienceState.TabB.SlideHeader ??
						SlideContainer.StarInfo.AudienceConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault);

					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab9SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab9SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceConfiguration.HeadersPartCItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.AudienceState.TabC.SlideHeader ??
						SlideContainer.StarInfo.AudienceConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault);

					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.AudienceConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
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