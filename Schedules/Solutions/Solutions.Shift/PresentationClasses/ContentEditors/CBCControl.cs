using System;
using System.ComponentModel;
using System.Windows.Forms;
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

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab5SubATitle))
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

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab5SubUTitle))
			{
				layoutControlGroupTabU.Text = SlideContainer.ShiftInfo.Titles.Tab5SubUTitle;

				slidesEditContainerTabU.Init(SlideContainer.ShiftInfo.CBCConfiguration.PartUSlides);
				slidesEditContainerTabU.SelectionChanged += OnEditValueChanged;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabU.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab5SubVTitle))
			{
				layoutControlGroupTabV.Text = SlideContainer.ShiftInfo.Titles.Tab5SubVTitle;

				slidesEditContainerTabV.Init(SlideContainer.ShiftInfo.CBCConfiguration.PartVSlides);
				slidesEditContainerTabV.SelectionChanged += OnEditValueChanged;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabV.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab5SubWTitle))
			{
				layoutControlGroupTabW.Text = SlideContainer.ShiftInfo.Titles.Tab5SubWTitle;

				slidesEditContainerTabW.Init(SlideContainer.ShiftInfo.CBCConfiguration.PartWSlides);
				slidesEditContainerTabW.SelectionChanged += OnEditValueChanged;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabW.Visibility = LayoutVisibility.Never;

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
			//SlideContainer.EditedContent.CBCState.TabA.SlideHeader = SlideContainer.ShiftInfo.CBCConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
			//	comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
			//	null;

			//SlideContainer.EditedContent.CBCState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();

			//SlideContainer.EditedContent.CBCState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.ShiftInfo.CBCConfiguration.PartASubHeader1DefaultValue ?
			//	memoEditTabASubheader1.EditValue as String ?? String.Empty :
			//	null;
			//SlideContainer.EditedContent.CBCState.TabA.Subheader2 = memoEditTabASubheader2.EditValue as String != SlideContainer.ShiftInfo.CBCConfiguration.PartASubHeader2DefaultValue ?
			//	memoEditTabASubheader2.EditValue as String ?? String.Empty :
			//	null;
			//		break;
			//}
			_dataChanged = false;
		}

		private void LoadPartData()
		{
			_allowToSave = false;

			if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabA)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubARightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubAFooterLogo;

				//comboBoxEditSlideHeader.Properties.Items.Clear();
				//comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.ShiftInfo.CBCConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
				//comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CBCState.TabA.SlideHeader ??
				//	SlideContainer.ShiftInfo.CBCConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
				//comboBoxEditSlideHeader.Properties.NullText = SlideContainer.ShiftInfo.CBCConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				//	"Select or type";
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabB)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubBRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubBFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabC)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubCRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubCFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabD)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubDRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubDFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabE)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubERightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubEFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabF)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubFRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubFFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabG)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubGRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubGFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabH)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubHRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubHFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabI)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubIRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubIFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabJ)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubJRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubJFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabU)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubURightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubUFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabV)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubVRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubVFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabW)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab5SubWRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab5SubWFooterLogo;
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