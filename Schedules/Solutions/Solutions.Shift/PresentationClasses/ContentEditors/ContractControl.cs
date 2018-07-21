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
	public sealed partial class ContractControl : BaseShiftControl
	{
		public override SlideType SlideType => SlideType.ShiftContract;

		public ContractControl(BaseShiftContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab10SubATitle))
				layoutControlGroupTabA.Text = SlideContainer.ShiftInfo.Titles.Tab10SubATitle;
			else
				layoutControlGroupTabA.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab10SubBTitle))
				layoutControlGroupTabB.Text = SlideContainer.ShiftInfo.Titles.Tab10SubBTitle;
			else
				layoutControlGroupTabB.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab10SubCTitle))
				layoutControlGroupTabC.Text = SlideContainer.ShiftInfo.Titles.Tab10SubCTitle;
			else
				layoutControlGroupTabC.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab10SubDTitle))
				layoutControlGroupTabD.Text = SlideContainer.ShiftInfo.Titles.Tab10SubDTitle;
			else
				layoutControlGroupTabD.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab10SubETitle))
				layoutControlGroupTabE.Text = SlideContainer.ShiftInfo.Titles.Tab10SubETitle;
			else
				layoutControlGroupTabE.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab10SubFTitle))
				layoutControlGroupTabF.Text = SlideContainer.ShiftInfo.Titles.Tab10SubFTitle;
			else
				layoutControlGroupTabF.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab10SubGTitle))
				layoutControlGroupTabG.Text = SlideContainer.ShiftInfo.Titles.Tab10SubGTitle;
			else
				layoutControlGroupTabG.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab10SubHTitle))
				layoutControlGroupTabH.Text = SlideContainer.ShiftInfo.Titles.Tab10SubHTitle;
			else
				layoutControlGroupTabH.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab10SubITitle))
				layoutControlGroupTabI.Text = SlideContainer.ShiftInfo.Titles.Tab10SubITitle;
			else
				layoutControlGroupTabI.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab10SubJTitle))
				layoutControlGroupTabJ.Text = SlideContainer.ShiftInfo.Titles.Tab10SubJTitle;
			else
				layoutControlGroupTabJ.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab10SubUTitle))
			{
				layoutControlGroupTabU.Text = SlideContainer.ShiftInfo.Titles.Tab10SubUTitle;

				slidesEditContainerTabU.Init(SlideContainer.ShiftInfo.ContractConfiguration.PartUSlides);
				slidesEditContainerTabU.SelectionChanged += OnEditValueChanged;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabU.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab10SubVTitle))
			{
				layoutControlGroupTabV.Text = SlideContainer.ShiftInfo.Titles.Tab10SubVTitle;

				slidesEditContainerTabV.Init(SlideContainer.ShiftInfo.ContractConfiguration.PartVSlides);
				slidesEditContainerTabV.SelectionChanged += OnEditValueChanged;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabV.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.ShiftInfo.Titles.Tab10SubWTitle))
			{
				layoutControlGroupTabW.Text = SlideContainer.ShiftInfo.Titles.Tab10SubWTitle;

				slidesEditContainerTabW.Init(SlideContainer.ShiftInfo.ContractConfiguration.PartWSlides);
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
			//SlideContainer.EditedContent.ContractState.TabA.SlideHeader = SlideContainer.ShiftInfo.ContractConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
			//	comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
			//	null;

			//SlideContainer.EditedContent.ContractState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();

			//SlideContainer.EditedContent.ContractState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.ShiftInfo.ContractConfiguration.PartASubHeader1DefaultValue ?
			//	memoEditTabASubheader1.EditValue as String ?? String.Empty :
			//	null;
			//SlideContainer.EditedContent.ContractState.TabA.Subheader2 = memoEditTabASubheader2.EditValue as String != SlideContainer.ShiftInfo.ContractConfiguration.PartASubHeader2DefaultValue ?
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
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab10SubARightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab10SubAFooterLogo;

				//comboBoxEditSlideHeader.Properties.Items.Clear();
				//comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.ShiftInfo.ContractConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
				//comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ContractState.TabA.SlideHeader ??
				//	SlideContainer.ShiftInfo.ContractConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
				//comboBoxEditSlideHeader.Properties.NullText = SlideContainer.ShiftInfo.ContractConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				//	"Select or type";
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabB)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab10SubBRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab10SubBFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabC)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab10SubCRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab10SubCFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabD)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab10SubDRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab10SubDFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabE)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab10SubERightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab10SubEFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabF)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab10SubFRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab10SubFFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabG)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab10SubGRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab10SubGFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabH)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab10SubHRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab10SubHFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabI)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab10SubIRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab10SubIFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabJ)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab10SubJRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab10SubJFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabU)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab10SubURightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab10SubUFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabV)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab10SubVRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab10SubVFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabW)
			{
				pictureEditLogoRight.Image = SlideContainer.ShiftInfo.Tab10SubWRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.ShiftInfo.Tab10SubWFooterLogo;
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