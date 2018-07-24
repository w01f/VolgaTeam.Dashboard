using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using DevExpress.Skins;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class FishingControl : StarAppControl
	{
		public override SlideType SlideType => SlideType.StarAppFishing;

		public FishingControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab3SubATitle))
			{
				layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab3SubATitle;

				memoEditTabASubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				memoEditTabASubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

				memoEditTabASubheader1.Properties.NullText = SlideContainer.StarInfo.FishingConfiguration.PartASubHeader1Placeholder ?? memoEditTabASubheader1.Properties.NullText;
				memoEditTabASubheader2.Properties.NullText = SlideContainer.StarInfo.FishingConfiguration.PartASubHeader2Placeholder ?? memoEditTabASubheader2.Properties.NullText;

				clipartEditContainerTabA1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab3SubAClipart1Image), SlideContainer.StarInfo.FishingConfiguration.PartAClipart1Configuration, this);
				clipartEditContainerTabA1.EditValueChanged += OnEditValueChanged;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabA.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab3SubBTitle))
			{
				layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab3SubBTitle;

				comboBoxEditTabBCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				comboBoxEditTabBCombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				comboBoxEditTabBCombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				comboBoxEditTabBCombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

				comboBoxEditTabBCombo1.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditTabBCombo1.Properties.NullText =
					SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(0)?.Value ??
					comboBoxEditTabBCombo1.Properties.NullText;
				comboBoxEditTabBCombo2.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditTabBCombo2.Properties.NullText =
					SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(1)?.Value ??
					comboBoxEditTabBCombo2.Properties.NullText;
				comboBoxEditTabBCombo3.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditTabBCombo3.Properties.NullText =
					SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(2)?.Value ??
					comboBoxEditTabBCombo3.Properties.NullText;
				comboBoxEditTabBCombo4.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditTabBCombo4.Properties.NullText =
					SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(3)?.Value ??
					comboBoxEditTabBCombo4.Properties.NullText;

				clipartEditContainerTabB1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab3SubBClipart1Image), SlideContainer.StarInfo.FishingConfiguration.PartBClipart1Configuration, this);
				clipartEditContainerTabB1.EditValueChanged += OnEditValueChanged;
				clipartEditContainerTabB2.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab3SubBClipart2Image), SlideContainer.StarInfo.FishingConfiguration.PartBClipart2Configuration, this);
				clipartEditContainerTabB2.EditValueChanged += OnEditValueChanged;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabB.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab3SubCTitle))
			{
				layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab3SubCTitle;

				memoEditTabCSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				memoEditTabCSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				memoEditTabCSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

				memoEditTabCSubheader1.Properties.NullText = SlideContainer.StarInfo.FishingConfiguration.PartCSubHeader1Placeholder ?? memoEditTabCSubheader1.Properties.NullText;
				memoEditTabCSubheader2.Properties.NullText = SlideContainer.StarInfo.FishingConfiguration.PartCSubHeader2Placeholder ?? memoEditTabCSubheader2.Properties.NullText;
				memoEditTabCSubheader3.Properties.NullText = SlideContainer.StarInfo.FishingConfiguration.PartCSubHeader3Placeholder ?? memoEditTabCSubheader3.Properties.NullText;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabC.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab3SubUTitle))
			{
				layoutControlGroupTabU.Text = SlideContainer.StarInfo.Titles.Tab3SubUTitle;

				slidesEditContainerTabU.Init(SlideContainer.StarInfo.FishingConfiguration.PartUSlides);
				slidesEditContainerTabU.SlideOutput += SlideContainer.OnCustomSlideOutput;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabU.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab3SubVTitle))
			{
				layoutControlGroupTabV.Text = SlideContainer.StarInfo.Titles.Tab3SubVTitle;

				slidesEditContainerTabV.Init(SlideContainer.StarInfo.FishingConfiguration.PartVSlides);
				slidesEditContainerTabV.SlideOutput += SlideContainer.OnCustomSlideOutput;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabV.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab3SubWTitle))
			{
				layoutControlGroupTabW.Text = SlideContainer.StarInfo.Titles.Tab3SubWTitle;

				slidesEditContainerTabW.Init(SlideContainer.StarInfo.FishingConfiguration.PartWSlides);
				slidesEditContainerTabW.SlideOutput += SlideContainer.OnCustomSlideOutput;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabW.Visibility = LayoutVisibility.Never;

			_outputProcessors.AddRange(OutputProcessor.GetOutputProcessors(this));

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab3SubATitle))
			{
				clipartEditContainerTabA1.LoadData(SlideContainer.EditedContent.FishingState.TabA.Clipart1);
				memoEditTabASubheader1.EditValue = SlideContainer.EditedContent.FishingState.TabA.Subheader1 ??
												   SlideContainer.StarInfo.FishingConfiguration.PartASubHeader1DefaultValue;
				memoEditTabASubheader2.EditValue = SlideContainer.EditedContent.FishingState.TabA.Subheader2 ??
												   SlideContainer.StarInfo.FishingConfiguration.PartASubHeader2DefaultValue;
			}

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab3SubBTitle))
			{
				clipartEditContainerTabB1.LoadData(SlideContainer.EditedContent.FishingState.TabB.Clipart1);
				clipartEditContainerTabB2.LoadData(SlideContainer.EditedContent.FishingState.TabB.Clipart2);
				comboBoxEditTabBCombo1.EditValue = SlideContainer.EditedContent.FishingState.TabB.Combo1 ??
												   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
													   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0);
				comboBoxEditTabBCombo2.EditValue = SlideContainer.EditedContent.FishingState.TabB.Combo2 ??
												   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
													   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1);
				comboBoxEditTabBCombo3.EditValue = SlideContainer.EditedContent.FishingState.TabB.Combo3 ??
												   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
													   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2);
				comboBoxEditTabBCombo4.EditValue = SlideContainer.EditedContent.FishingState.TabB.Combo4 ??
												   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
													   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3);
			}

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab3SubCTitle))
			{
				memoEditTabCSubheader1.EditValue = SlideContainer.EditedContent.FishingState.TabC.Subheader1 ??
												   SlideContainer.StarInfo.FishingConfiguration.PartCSubHeader1DefaultValue;
				memoEditTabCSubheader2.EditValue = SlideContainer.EditedContent.FishingState.TabC.Subheader2 ??
												   SlideContainer.StarInfo.FishingConfiguration.PartCSubHeader2DefaultValue;
				memoEditTabCSubheader3.EditValue = SlideContainer.EditedContent.FishingState.TabC.Subheader3 ??
												   SlideContainer.StarInfo.FishingConfiguration.PartCSubHeader3DefaultValue;
			}

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab3SubUTitle))
			{
			}

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab3SubVTitle))
			{
			}

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab3SubWTitle))
			{
			}

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabA)
			{
				SlideContainer.EditedContent.FishingState.TabA.SlideHeader = SlideContainer.StarInfo.FishingConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					null;

				SlideContainer.EditedContent.FishingState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();

				SlideContainer.EditedContent.FishingState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.StarInfo.FishingConfiguration.PartASubHeader1DefaultValue ?
					memoEditTabASubheader1.EditValue as String ?? String.Empty :
					null;
				SlideContainer.EditedContent.FishingState.TabA.Subheader2 = memoEditTabASubheader2.EditValue as String != SlideContainer.StarInfo.FishingConfiguration.PartASubHeader2DefaultValue ?
					memoEditTabASubheader2.EditValue as String ?? String.Empty :
					null;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabB)
			{
				SlideContainer.EditedContent.FishingState.TabB.SlideHeader = SlideContainer.StarInfo.FishingConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

				SlideContainer.EditedContent.FishingState.TabB.Clipart1 = clipartEditContainerTabB1.GetActiveClipartObject();
				SlideContainer.EditedContent.FishingState.TabB.Clipart2 = clipartEditContainerTabB2.GetActiveClipartObject();

				SlideContainer.EditedContent.FishingState.TabB.Combo1 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0) != comboBoxEditTabBCombo1.EditValue ?
					comboBoxEditTabBCombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo1.EditValue as String } :
					null;
				SlideContainer.EditedContent.FishingState.TabB.Combo2 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1) != comboBoxEditTabBCombo2.EditValue ?
					comboBoxEditTabBCombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo2.EditValue as String } :
					null;
				SlideContainer.EditedContent.FishingState.TabB.Combo3 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2) != comboBoxEditTabBCombo3.EditValue ?
					comboBoxEditTabBCombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo3.EditValue as String } :
					null;
				SlideContainer.EditedContent.FishingState.TabB.Combo4 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3) != comboBoxEditTabBCombo4.EditValue ?
					comboBoxEditTabBCombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabBCombo4.EditValue as String } :
					null;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabC)
			{
				SlideContainer.EditedContent.FishingState.TabC.SlideHeader = SlideContainer.StarInfo.FishingConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					null;

				SlideContainer.EditedContent.FishingState.TabC.Subheader1 = memoEditTabCSubheader1.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader1DefaultValue ?
					memoEditTabCSubheader1.EditValue as String ?? String.Empty :
					null;
				SlideContainer.EditedContent.FishingState.TabC.Subheader2 = memoEditTabCSubheader2.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader2DefaultValue ?
					memoEditTabCSubheader2.EditValue as String ?? String.Empty :
					null;
				SlideContainer.EditedContent.FishingState.TabC.Subheader3 = memoEditTabCSubheader3.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader3DefaultValue ?
					memoEditTabCSubheader3.EditValue as String ?? String.Empty :
					null;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabU)
			{
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabV)
			{
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabW)
			{
			}

			_dataChanged = false;
		}

		private void LoadPartData()
		{
			_allowToSave = false;

			if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabA)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab3SubARightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab3SubAFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.FishingConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.FishingState.TabA.SlideHeader ??
													SlideContainer.StarInfo.FishingConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.FishingConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabB)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab3SubBRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab3SubBFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.FishingConfiguration.HeadersPartBItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.FishingState.TabB.SlideHeader ??
													SlideContainer.StarInfo.FishingConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.FishingConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabC)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab3SubCRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab3SubCFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.FishingConfiguration.HeadersPartCItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.FishingState.TabC.SlideHeader ??
													SlideContainer.StarInfo.FishingConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.FishingConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabU)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab3SubURightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab3SubUFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabV)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab3SubVRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab3SubVFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabW)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab3SubWRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab3SubWFooterLogo;
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