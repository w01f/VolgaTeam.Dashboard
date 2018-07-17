﻿using System;
using System.ComponentModel;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using DevExpress.Skins;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class ClosersControl : BaseShiftControl
	{
		public override SlideType SlideType => SlideType.ShiftClosers;

		public ClosersControl(BaseShiftContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			if(!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab8SubATitle))
				layoutControlGroupTabA.Text = SlideContainer.ShiftInfo.Titles.Tab8SubATitle;
			else
				layoutControlGroupTabA.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab8SubBTitle))
				layoutControlGroupTabB.Text = SlideContainer.ShiftInfo.Titles.Tab8SubBTitle;
			else
				layoutControlGroupTabB.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab8SubCTitle))
				layoutControlGroupTabC.Text = SlideContainer.ShiftInfo.Titles.Tab8SubCTitle;
			else
				layoutControlGroupTabC.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab8SubDTitle))
				layoutControlGroupTabD.Text = SlideContainer.ShiftInfo.Titles.Tab8SubDTitle;
			else
				layoutControlGroupTabD.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab8SubETitle))
				layoutControlGroupTabE.Text = SlideContainer.ShiftInfo.Titles.Tab8SubETitle;
			else
				layoutControlGroupTabE.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab8SubFTitle))
				layoutControlGroupTabF.Text = SlideContainer.ShiftInfo.Titles.Tab8SubFTitle;
			else
				layoutControlGroupTabF.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab8SubGTitle))
				layoutControlGroupTabG.Text = SlideContainer.ShiftInfo.Titles.Tab8SubGTitle;
			else
				layoutControlGroupTabG.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab8SubHTitle))
				layoutControlGroupTabH.Text = SlideContainer.ShiftInfo.Titles.Tab8SubHTitle;
			else
				layoutControlGroupTabH.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab8SubITitle))
				layoutControlGroupTabI.Text = SlideContainer.ShiftInfo.Titles.Tab8SubITitle;
			else
				layoutControlGroupTabI.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab8SubJTitle))
				layoutControlGroupTabJ.Text = SlideContainer.ShiftInfo.Titles.Tab8SubJTitle;
			else
				layoutControlGroupTabJ.Visibility = LayoutVisibility.Never;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;
			//switch (tabbedControlGroupData.SelectedTabPageIndex)
			//{
			//	case 0:
					//SlideContainer.EditedContent.CNAState.TabA.SlideHeader = SlideContainer.StarInfo.CNAConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					//	comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					//	null;

					//SlideContainer.EditedContent.CNAState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();

					//SlideContainer.EditedContent.CNAState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.StarInfo.CNAConfiguration.PartASubHeader1DefaultValue ?
					//	memoEditTabASubheader1.EditValue as String ?? String.Empty :
					//	null;
					//SlideContainer.EditedContent.CNAState.TabA.Subheader2 = memoEditTabASubheader2.EditValue as String != SlideContainer.StarInfo.CNAConfiguration.PartASubHeader2DefaultValue ?
					//	memoEditTabASubheader2.EditValue as String ?? String.Empty :
					//	null;
			//		break;
			//}
			_dataChanged = false;
		}

		private void LoadPartData()
		{
			_allowToSave = false;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubAFooterLogo;

					//comboBoxEditSlideHeader.Properties.Items.Clear();
					//comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.ShiftInfo.CNAConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
					//comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CNAState.TabA.SlideHeader ??
					//	SlideContainer.StarInfo.CNAConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
					//comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.CNAConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
					//	"Select or type";
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubBFooterLogo;
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubCFooterLogo;
					break;
				case 3:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubDRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubDFooterLogo;
					break;
				case 4:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubERightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubEFooterLogo;
					break;
				case 5:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubFRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubFFooterLogo;
					break;
				case 6:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubGRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubGFooterLogo;
					break;
				case 7:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubHRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubHFooterLogo;
					break;
				case 8:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubIRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubIFooterLogo;
					break;
				case 9:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubJRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubJFooterLogo;
					break;
			}
			_allowToSave = true;

			_dataChanged = false;
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