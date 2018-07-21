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
	public sealed partial class InvestmentControl : BaseShiftControl
	{
		public override SlideType SlideType => SlideType.ShiftInvestment;

		public InvestmentControl(BaseShiftContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab7SubATitle))
				layoutControlGroupTabA.Text = SlideContainer.ShiftInfo.Titles.Tab7SubATitle;
			else
				layoutControlGroupTabA.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab7SubBTitle))
				layoutControlGroupTabB.Text = SlideContainer.ShiftInfo.Titles.Tab7SubBTitle;
			else
				layoutControlGroupTabB.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab7SubCTitle))
				layoutControlGroupTabC.Text = SlideContainer.ShiftInfo.Titles.Tab7SubCTitle;
			else
				layoutControlGroupTabC.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab7SubDTitle))
				layoutControlGroupTabD.Text = SlideContainer.ShiftInfo.Titles.Tab7SubDTitle;
			else
				layoutControlGroupTabD.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab7SubETitle))
				layoutControlGroupTabE.Text = SlideContainer.ShiftInfo.Titles.Tab7SubETitle;
			else
				layoutControlGroupTabE.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab7SubFTitle))
				layoutControlGroupTabF.Text = SlideContainer.ShiftInfo.Titles.Tab7SubFTitle;
			else
				layoutControlGroupTabF.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab7SubGTitle))
				layoutControlGroupTabG.Text = SlideContainer.ShiftInfo.Titles.Tab7SubGTitle;
			else
				layoutControlGroupTabG.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab7SubHTitle))
				layoutControlGroupTabH.Text = SlideContainer.ShiftInfo.Titles.Tab7SubHTitle;
			else
				layoutControlGroupTabH.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab7SubITitle))
				layoutControlGroupTabI.Text = SlideContainer.ShiftInfo.Titles.Tab7SubITitle;
			else
				layoutControlGroupTabI.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab7SubJTitle))
				layoutControlGroupTabJ.Text = SlideContainer.ShiftInfo.Titles.Tab7SubJTitle;
			else
				layoutControlGroupTabJ.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab7SubUTitle))
			{
				layoutControlGroupTabU.Text = SlideContainer.ShiftInfo.Titles.Tab7SubUTitle;

				slidesEditContainerTabU.Init(SlideContainer.ShiftInfo.InvestmentConfiguration.PartUSlides);
				slidesEditContainerTabU.SelectionChanged += OnEditValueChanged;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabU.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab7SubVTitle))
			{
				layoutControlGroupTabV.Text = SlideContainer.ShiftInfo.Titles.Tab7SubVTitle;

				slidesEditContainerTabV.Init(SlideContainer.ShiftInfo.InvestmentConfiguration.PartVSlides);
				slidesEditContainerTabV.SelectionChanged += OnEditValueChanged;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabV.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab7SubWTitle))
			{
				layoutControlGroupTabW.Text = SlideContainer.ShiftInfo.Titles.Tab7SubWTitle;

				slidesEditContainerTabW.Init(SlideContainer.ShiftInfo.InvestmentConfiguration.PartWSlides);
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
			//SlideContainer.EditedContent.InvestmentState.TabA.SlideHeader = SlideContainer.ShiftInfo.InvestmentConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
			//	comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
			//	null;

			//SlideContainer.EditedContent.InvestmentState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();

			//SlideContainer.EditedContent.InvestmentState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.ShiftInfo.InvestmentConfiguration.PartASubHeader1DefaultValue ?
			//	memoEditTabASubheader1.EditValue as String ?? String.Empty :
			//	null;
			//SlideContainer.EditedContent.InvestmentState.TabA.Subheader2 = memoEditTabASubheader2.EditValue as String != SlideContainer.ShiftInfo.InvestmentConfiguration.PartASubHeader2DefaultValue ?
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
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab7SubARightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab7SubAFooterLogo;

				//comboBoxEditSlideHeader.Properties.Items.Clear();
				//comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.ShiftInfo.InvestmentConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
				//comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.InvestmentState.TabA.SlideHeader ??
				//	SlideContainer.ShiftInfo.InvestmentConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
				//comboBoxEditSlideHeader.Properties.NullText = SlideContainer.ShiftInfo.InvestmentConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				//	"Select or type";
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabB)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab7SubBRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab7SubBFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabC)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab7SubCRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab7SubCFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabD)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab7SubDRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab7SubDFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabE)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab7SubERightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab7SubEFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabF)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab7SubFRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab7SubFFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabG)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab7SubGRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab7SubGFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabH)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab7SubHRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab7SubHFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabI)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab7SubIRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab7SubIFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabJ)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab7SubJRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab7SubJFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabU)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab7SubURightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab7SubUFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabV)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab7SubVRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab7SubVFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabW)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab7SubWRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab7SubWFooterLogo;
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