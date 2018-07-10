﻿using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
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

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabASubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabCSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditTabDSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab8SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab8SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab8SubCTitle;
			layoutControlGroupTabD.Text = SlideContainer.StarInfo.Titles.Tab8SubDTitle;

			clipartEditContainerTabA1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab8SubAClipart1Image), SlideContainer.StarInfo.VideoConfiguration.PartAClipart1Configuration, this);
			clipartEditContainerTabA1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabB1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab8SubBClipart1Image), SlideContainer.StarInfo.VideoConfiguration.PartBClipart1Configuration, this);
			clipartEditContainerTabB1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabC1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab8SubCClipart1Image), SlideContainer.StarInfo.VideoConfiguration.PartCClipart1Configuration, this);
			clipartEditContainerTabC1.EditValueChanged += OnEditValueChanged;
			clipartEditContainerTabD1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab8SubDClipart1Image), SlideContainer.StarInfo.VideoConfiguration.PartDClipart1Configuration, this);
			clipartEditContainerTabD1.EditValueChanged += OnEditValueChanged;

			memoEditTabASubheader1.Properties.NullText = SlideContainer.StarInfo.VideoConfiguration.PartASubHeader1Placeholder ?? memoEditTabASubheader1.Properties.NullText;
			memoEditTabBSubheader1.Properties.NullText = SlideContainer.StarInfo.VideoConfiguration.PartBSubHeader1Placeholder ?? memoEditTabBSubheader1.Properties.NullText;
			memoEditTabCSubheader1.Properties.NullText = SlideContainer.StarInfo.VideoConfiguration.PartCSubHeader1Placeholder ?? memoEditTabCSubheader1.Properties.NullText;
			memoEditTabDSubheader1.Properties.NullText = SlideContainer.StarInfo.VideoConfiguration.PartDSubHeader1Placeholder ?? memoEditTabDSubheader1.Properties.NullText;

			_outputProcessors.AddRange(OutputProcessor.GetOutputProcessors(this));

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainerTabA1.LoadData(SlideContainer.EditedContent.VideoState.TabA.Clipart1);
			memoEditTabASubheader1.EditValue = SlideContainer.EditedContent.VideoState.TabA.Subheader1 ??
				SlideContainer.StarInfo.VideoConfiguration.PartASubHeader1DefaultValue;

			clipartEditContainerTabB1.LoadData(SlideContainer.EditedContent.VideoState.TabB.Clipart1);
			memoEditTabBSubheader1.EditValue = SlideContainer.EditedContent.VideoState.TabB.Subheader1 ??
				SlideContainer.StarInfo.VideoConfiguration.PartBSubHeader1DefaultValue;

			clipartEditContainerTabC1.LoadData(SlideContainer.EditedContent.VideoState.TabC.Clipart1);
			memoEditTabCSubheader1.EditValue = SlideContainer.EditedContent.VideoState.TabC.Subheader1 ??
				SlideContainer.StarInfo.VideoConfiguration.PartCSubHeader1DefaultValue;

			clipartEditContainerTabD1.LoadData(SlideContainer.EditedContent.VideoState.TabD.Clipart1);
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
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.VideoState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();

					SlideContainer.EditedContent.VideoState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.StarInfo.VideoConfiguration.PartASubHeader1DefaultValue ?
						memoEditTabASubheader1.EditValue as String ?? String.Empty :
						null;
					break;
				case 1:
					SlideContainer.EditedContent.VideoState.TabB.SlideHeader = SlideContainer.StarInfo.VideoConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.VideoState.TabB.Clipart1 = clipartEditContainerTabB1.GetActiveClipartObject();

					SlideContainer.EditedContent.VideoState.TabB.Subheader1 = memoEditTabBSubheader1.EditValue as String != SlideContainer.StarInfo.VideoConfiguration.PartBSubHeader1DefaultValue ?
						memoEditTabBSubheader1.EditValue as String ?? String.Empty :
						null;
					break;
				case 2:
					SlideContainer.EditedContent.VideoState.TabC.SlideHeader = SlideContainer.StarInfo.VideoConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.VideoState.TabC.Clipart1 = clipartEditContainerTabC1.GetActiveClipartObject();

					SlideContainer.EditedContent.VideoState.TabC.Subheader1 = memoEditTabCSubheader1.EditValue as String != SlideContainer.StarInfo.VideoConfiguration.PartCSubHeader1DefaultValue ?
						memoEditTabCSubheader1.EditValue as String ?? String.Empty :
						null;
					break;
				case 3:
					SlideContainer.EditedContent.VideoState.TabD.SlideHeader = SlideContainer.StarInfo.VideoConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

					SlideContainer.EditedContent.VideoState.TabD.Clipart1 = clipartEditContainerTabD1.GetActiveClipartObject();

					SlideContainer.EditedContent.VideoState.TabD.Subheader1 = memoEditTabDSubheader1.EditValue as String != SlideContainer.StarInfo.VideoConfiguration.PartDSubHeader1DefaultValue ?
						memoEditTabDSubheader1.EditValue as String ?? String.Empty :
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
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.VideoConfiguration.HeadersPartBItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.VideoState.TabA.SlideHeader ??
						SlideContainer.StarInfo.VideoConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.VideoConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab8SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab8SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.VideoConfiguration.HeadersPartBItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.VideoState.TabB.SlideHeader ??
						SlideContainer.StarInfo.VideoConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.VideoConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab8SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab8SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.VideoConfiguration.HeadersPartCItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.VideoState.TabC.SlideHeader ??
						SlideContainer.StarInfo.VideoConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.VideoConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
					break;
				case 3:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab8SubDRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab8SubDFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.VideoConfiguration.HeadersPartDItems.Where(item => !item.IsPlaceholder).ToArray());
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.VideoState.TabD.SlideHeader ??
						SlideContainer.StarInfo.VideoConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.VideoConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
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