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
	public sealed partial class MarketControl : BaseShiftControl
	{
		public override SlideType SlideType => SlideType.ShiftMarket;

		public MarketControl(BaseShiftContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab3SubATitle))
				layoutControlGroupTabA.Text = SlideContainer.ShiftInfo.Titles.Tab3SubATitle;
			else
				layoutControlGroupTabA.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab3SubBTitle))
				layoutControlGroupTabB.Text = SlideContainer.ShiftInfo.Titles.Tab3SubBTitle;
			else
				layoutControlGroupTabB.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab3SubCTitle))
				layoutControlGroupTabC.Text = SlideContainer.ShiftInfo.Titles.Tab3SubCTitle;
			else
				layoutControlGroupTabC.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab3SubDTitle))
				layoutControlGroupTabD.Text = SlideContainer.ShiftInfo.Titles.Tab3SubDTitle;
			else
				layoutControlGroupTabD.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab3SubETitle))
				layoutControlGroupTabE.Text = SlideContainer.ShiftInfo.Titles.Tab3SubETitle;
			else
				layoutControlGroupTabE.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab3SubFTitle))
				layoutControlGroupTabF.Text = SlideContainer.ShiftInfo.Titles.Tab3SubFTitle;
			else
				layoutControlGroupTabF.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab3SubGTitle))
				layoutControlGroupTabG.Text = SlideContainer.ShiftInfo.Titles.Tab3SubGTitle;
			else
				layoutControlGroupTabG.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab3SubHTitle))
				layoutControlGroupTabH.Text = SlideContainer.ShiftInfo.Titles.Tab3SubHTitle;
			else
				layoutControlGroupTabH.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab3SubITitle))
				layoutControlGroupTabI.Text = SlideContainer.ShiftInfo.Titles.Tab3SubITitle;
			else
				layoutControlGroupTabI.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab3SubJTitle))
				layoutControlGroupTabJ.Text = SlideContainer.ShiftInfo.Titles.Tab3SubJTitle;
			else
				layoutControlGroupTabJ.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab3SubUTitle))
			{
				layoutControlGroupTabU.Text = SlideContainer.ShiftInfo.Titles.Tab3SubUTitle;

				slidesEditContainerTabU.Init(SlideContainer.ShiftInfo.MarketConfiguration.PartUSlides);
				slidesEditContainerTabU.SlideOutput += SlideContainer.OnCustomSlideOutput;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabU.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab3SubVTitle))
			{
				layoutControlGroupTabV.Text = SlideContainer.ShiftInfo.Titles.Tab3SubVTitle;

				slidesEditContainerTabV.Init(SlideContainer.ShiftInfo.MarketConfiguration.PartVSlides);
				slidesEditContainerTabV.SlideOutput += SlideContainer.OnCustomSlideOutput;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabV.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab3SubWTitle))
			{
				layoutControlGroupTabW.Text = SlideContainer.ShiftInfo.Titles.Tab3SubWTitle;

				slidesEditContainerTabW.Init(SlideContainer.ShiftInfo.MarketConfiguration.PartWSlides);
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
			//SlideContainer.EditedContent.MarketState.TabA.SlideHeader = SlideContainer.ShiftInfo.MarketConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
			//	comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
			//	null;

			//SlideContainer.EditedContent.MarketState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();

			//SlideContainer.EditedContent.MarketState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.ShiftInfo.MarketConfiguration.PartASubHeader1DefaultValue ?
			//	memoEditTabASubheader1.EditValue as String ?? String.Empty :
			//	null;
			//SlideContainer.EditedContent.MarketState.TabA.Subheader2 = memoEditTabASubheader2.EditValue as String != SlideContainer.ShiftInfo.MarketConfiguration.PartASubHeader2DefaultValue ?
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
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab3SubARightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab3SubAFooterLogo;

				//comboBoxEditSlideHeader.Properties.Items.Clear();
				//comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.ShiftInfo.MarketConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
				//comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.MarketState.TabA.SlideHeader ??
				//	SlideContainer.ShiftInfo.MarketConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
				//comboBoxEditSlideHeader.Properties.NullText = SlideContainer.ShiftInfo.MarketConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				//	"Select or type";
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabB)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab3SubBRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab3SubBFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabC)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab3SubCRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab3SubCFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabD)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab3SubDRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab3SubDFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabE)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab3SubERightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab3SubEFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabF)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab3SubFRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab3SubFFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabG)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab3SubGRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab3SubGFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabH)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab3SubHRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab3SubHFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabI)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab3SubIRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab3SubIFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabJ)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab3SubJRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab3SubJFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabU)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab3SubURightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab3SubUFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabV)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab3SubVRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab3SubVFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabW)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab3SubWRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab3SubWFooterLogo;
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