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
	public sealed partial class ClosersControl : BaseShiftControl
	{
		public override SlideType SlideType => SlideType.ShiftClosers;

		public ClosersControl(BaseShiftContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab8SubATitle))
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

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab8SubUTitle))
			{
				layoutControlGroupTabU.Text = SlideContainer.ShiftInfo.Titles.Tab8SubUTitle;

				slidesEditContainerTabU.Init(SlideContainer.ShiftInfo.ClosersConfiguration.PartUSlides);
				slidesEditContainerTabU.SlideOutput += SlideContainer.OnCustomSlideOutput;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabU.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab8SubVTitle))
			{
				layoutControlGroupTabV.Text = SlideContainer.ShiftInfo.Titles.Tab8SubVTitle;

				slidesEditContainerTabV.Init(SlideContainer.ShiftInfo.ClosersConfiguration.PartVSlides);
				slidesEditContainerTabV.SlideOutput += SlideContainer.OnCustomSlideOutput;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabV.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab8SubWTitle))
			{
				layoutControlGroupTabW.Text = SlideContainer.ShiftInfo.Titles.Tab8SubWTitle;

				slidesEditContainerTabW.Init(SlideContainer.ShiftInfo.ClosersConfiguration.PartWSlides);
				slidesEditContainerTabW.SlideOutput += SlideContainer.OnCustomSlideOutput;

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
			//SlideContainer.EditedContent.ClosersState.TabA.SlideHeader = SlideContainer.ShiftInfo.ClosersConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
			//	comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
			//	null;

			//SlideContainer.EditedContent.ClosersState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();

			//SlideContainer.EditedContent.ClosersState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.ShiftInfo.ClosersConfiguration.PartASubHeader1DefaultValue ?
			//	memoEditTabASubheader1.EditValue as String ?? String.Empty :
			//	null;
			//SlideContainer.EditedContent.ClosersState.TabA.Subheader2 = memoEditTabASubheader2.EditValue as String != SlideContainer.ShiftInfo.ClosersConfiguration.PartASubHeader2DefaultValue ?
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
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubARightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubAFooterLogo;

				//comboBoxEditSlideHeader.Properties.Items.Clear();
				//comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.ShiftInfo.ClosersConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
				//comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ClosersState.TabA.SlideHeader ??
				//	SlideContainer.ShiftInfo.ClosersConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
				//comboBoxEditSlideHeader.Properties.NullText = SlideContainer.ShiftInfo.ClosersConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				//	"Select or type";
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabB)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubBRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubBFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabC)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubCRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubCFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabD)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubDRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubDFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabE)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubERightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubEFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabF)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubFRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubFFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabG)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubGRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubGFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabH)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubHRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubHFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabI)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubIRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubIFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabJ)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubJRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubJFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabU)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubURightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubUFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabV)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubVRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubVFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabW)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab8SubWRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab8SubWFooterLogo;
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