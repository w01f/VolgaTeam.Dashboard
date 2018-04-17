﻿using System;
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
	public sealed partial class VideoControl : StarAppControl
	{
		public override SlideType SlideType => SlideType.StarAppVideo;
		public override string OutputName => SlideContainer.StarInfo.Titles.Tab8Title;

		public VideoControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll();
			memoEditTabASubheader1.EnableSelectAll();
			memoEditTabBSubheader1.EnableSelectAll();
			memoEditTabCSubheader1.EnableSelectAll();
			memoEditTabDSubheader1.EnableSelectAll();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab8SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab8SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab8SubCTitle;
			layoutControlGroupTabD.Text = SlideContainer.StarInfo.Titles.Tab8SubDTitle;

			pictureEditTabAClipart1.Image = SlideContainer.StarInfo.Tab8SubAClipart1Image;
			pictureEditTabAClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.VideoConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabBClipart1.Image = SlideContainer.StarInfo.Tab8SubBClipart1Image;
			pictureEditTabBClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.VideoConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabCClipart1.Image = SlideContainer.StarInfo.Tab8SubCClipart1Image;
			pictureEditTabCClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.VideoConfiguration.PartCClipart1Configuration.Alignment;
			pictureEditTabDClipart1.Image = SlideContainer.StarInfo.Tab8SubDClipart1Image;
			pictureEditTabDClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.VideoConfiguration.PartDClipart1Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabAClipart1,
				pictureEditTabBClipart1,
				pictureEditTabCClipart1,
				pictureEditTabDClipart1,
			});

			_outputProcessors.AddRange(OutputProcessor.GetOutputProcessors(this));

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabAClipart1.Image = SlideContainer.EditedContent.VideoState.TabA.Clipart1 ??
				pictureEditTabAClipart1.Image;
			memoEditTabASubheader1.EditValue = SlideContainer.EditedContent.VideoState.TabA.Subheader1 ??
				SlideContainer.StarInfo.VideoConfiguration.PartASubHeader1DefaultValue;

			pictureEditTabBClipart1.Image = SlideContainer.EditedContent.VideoState.TabB.Clipart1 ??
				pictureEditTabBClipart1.Image;
			memoEditTabBSubheader1.EditValue = SlideContainer.EditedContent.VideoState.TabB.Subheader1 ??
				SlideContainer.StarInfo.VideoConfiguration.PartBSubHeader1DefaultValue;

			pictureEditTabCClipart1.Image = SlideContainer.EditedContent.VideoState.TabC.Clipart1 ??
				pictureEditTabCClipart1.Image;
			memoEditTabCSubheader1.EditValue = SlideContainer.EditedContent.VideoState.TabC.Subheader1 ??
				SlideContainer.StarInfo.VideoConfiguration.PartCSubHeader1DefaultValue;

			pictureEditTabDClipart1.Image = SlideContainer.EditedContent.VideoState.TabD.Clipart1 ??
				pictureEditTabDClipart1.Image;
			memoEditTabDSubheader1.EditValue = SlideContainer.EditedContent.VideoState.TabD.Subheader1 ??
				SlideContainer.StarInfo.VideoConfiguration.PartDSubHeader1DefaultValue;

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					SlideContainer.EditedContent.VideoState.TabA.SlideHeader = SlideContainer.StarInfo.VideoConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;

					SlideContainer.EditedContent.VideoState.TabA.Clipart1 = pictureEditTabAClipart1.Image != SlideContainer.StarInfo.Tab8SubAClipart1Image ?
						pictureEditTabAClipart1.Image :
						null;

					SlideContainer.EditedContent.VideoState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.StarInfo.VideoConfiguration.PartASubHeader1DefaultValue ?
						memoEditTabASubheader1.EditValue as String :
						null;
					break;
				case 1:
					SlideContainer.EditedContent.VideoState.TabB.SlideHeader = SlideContainer.StarInfo.VideoConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;

					SlideContainer.EditedContent.VideoState.TabB.Clipart1 = pictureEditTabBClipart1.Image != SlideContainer.StarInfo.Tab8SubBClipart1Image ?
						pictureEditTabBClipart1.Image :
						null;

					SlideContainer.EditedContent.VideoState.TabB.Subheader1 = memoEditTabBSubheader1.EditValue as String != SlideContainer.StarInfo.VideoConfiguration.PartBSubHeader1DefaultValue ?
						memoEditTabBSubheader1.EditValue as String :
						null;
					break;
				case 2:
					SlideContainer.EditedContent.VideoState.TabC.SlideHeader = SlideContainer.StarInfo.VideoConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;

					SlideContainer.EditedContent.VideoState.TabC.Clipart1 = pictureEditTabCClipart1.Image != SlideContainer.StarInfo.Tab8SubCClipart1Image ?
						pictureEditTabCClipart1.Image :
						null;

					SlideContainer.EditedContent.VideoState.TabC.Subheader1 = memoEditTabCSubheader1.EditValue as String != SlideContainer.StarInfo.VideoConfiguration.PartCSubHeader1DefaultValue ?
						memoEditTabCSubheader1.EditValue as String :
						null;
					break;
				case 3:
					SlideContainer.EditedContent.VideoState.TabD.SlideHeader = SlideContainer.StarInfo.VideoConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;

					SlideContainer.EditedContent.VideoState.TabD.Clipart1 = pictureEditTabDClipart1.Image != SlideContainer.StarInfo.Tab8SubDClipart1Image ?
						pictureEditTabDClipart1.Image :
						null;

					SlideContainer.EditedContent.VideoState.TabD.Subheader1 = memoEditTabDSubheader1.EditValue as String != SlideContainer.StarInfo.VideoConfiguration.PartDSubHeader1DefaultValue ?
						memoEditTabDSubheader1.EditValue as String :
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
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab8SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab8SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.VideoConfiguration.HeadersPartBItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.VideoState.TabA.SlideHeader ??
						SlideContainer.StarInfo.VideoConfiguration.HeadersPartAItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab8SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab8SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.VideoConfiguration.HeadersPartBItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.VideoState.TabB.SlideHeader ??
						SlideContainer.StarInfo.VideoConfiguration.HeadersPartBItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab8SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab8SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.VideoConfiguration.HeadersPartCItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.VideoState.TabC.SlideHeader ??
						SlideContainer.StarInfo.VideoConfiguration.HeadersPartCItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 3:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab8SubDRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab8SubDFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.VideoConfiguration.HeadersPartDItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.VideoState.TabD.SlideHeader ??
						SlideContainer.StarInfo.VideoConfiguration.HeadersPartDItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
			}
			_allowToSave = true;
		}

		private void EditValueChanged(object sender, EventArgs e)
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