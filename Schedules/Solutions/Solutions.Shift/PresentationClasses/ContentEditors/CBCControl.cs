using System;
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
	public sealed partial class CBCControl : BaseShiftControl
	{
		public override SlideType SlideType => SlideType.ShiftCBC;

		public CBCControl(BaseShiftContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			if(!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab5SubATitle))
				layoutControlGroupTabA.Text = SlideContainer.ShiftInfo.Titles.Tab5SubATitle;
			else
				layoutControlGroupTabA.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab5SubBTitle))
				layoutControlGroupTabB.Text = SlideContainer.ShiftInfo.Titles.Tab5SubBTitle;
			else
				layoutControlGroupTabB.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab5SubCTitle))
				layoutControlGroupTabC.Text = SlideContainer.ShiftInfo.Titles.Tab5SubCTitle;
			else
				layoutControlGroupTabC.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab5SubDTitle))
				layoutControlGroupTabD.Text = SlideContainer.ShiftInfo.Titles.Tab5SubDTitle;
			else
				layoutControlGroupTabD.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab5SubETitle))
				layoutControlGroupTabE.Text = SlideContainer.ShiftInfo.Titles.Tab5SubETitle;
			else
				layoutControlGroupTabE.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab5SubFTitle))
				layoutControlGroupTabF.Text = SlideContainer.ShiftInfo.Titles.Tab5SubFTitle;
			else
				layoutControlGroupTabF.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab5SubGTitle))
				layoutControlGroupTabG.Text = SlideContainer.ShiftInfo.Titles.Tab5SubGTitle;
			else
				layoutControlGroupTabG.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab5SubHTitle))
				layoutControlGroupTabH.Text = SlideContainer.ShiftInfo.Titles.Tab5SubHTitle;
			else
				layoutControlGroupTabH.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab5SubITitle))
				layoutControlGroupTabI.Text = SlideContainer.ShiftInfo.Titles.Tab5SubITitle;
			else
				layoutControlGroupTabI.Visibility = LayoutVisibility.Never;
			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab5SubJTitle))
				layoutControlGroupTabJ.Text = SlideContainer.ShiftInfo.Titles.Tab5SubJTitle;
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
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubAFooterLogo;

					//comboBoxEditSlideHeader.Properties.Items.Clear();
					//comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.ShiftInfo.CNAConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
					//comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CNAState.TabA.SlideHeader ??
					//	SlideContainer.StarInfo.CNAConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
					//comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.CNAConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
					//	"Select or type";
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubBFooterLogo;
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubCFooterLogo;
					break;
				case 3:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubDRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubDFooterLogo;
					break;
				case 4:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubERightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubEFooterLogo;
					break;
				case 5:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubFRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubFFooterLogo;
					break;
				case 6:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubGRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubGFooterLogo;
					break;
				case 7:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubHRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubHFooterLogo;
					break;
				case 8:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubIRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubIFooterLogo;
					break;
				case 9:
					pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubJRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubJFooterLogo;
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