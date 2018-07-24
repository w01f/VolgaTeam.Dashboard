﻿using System;
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
	public sealed partial class CustomerControl : StarAppControl
	{
		public override SlideType SlideType => SlideType.StarAppCustomer;

		public CustomerControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab4SubATitle))
			{
				layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab4SubATitle;

				comboBoxEditTabACombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				comboBoxEditTabACombo2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				comboBoxEditTabACombo3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				comboBoxEditTabACombo4.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

				comboBoxEditTabACombo1.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditTabACombo1.Properties.NullText =
					SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(0)?.Value ??
					comboBoxEditTabACombo1.Properties.NullText;
				comboBoxEditTabACombo2.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditTabACombo2.Properties.NullText =
					SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(1)?.Value ??
					comboBoxEditTabACombo2.Properties.NullText;
				comboBoxEditTabACombo3.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditTabACombo3.Properties.NullText =
					SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(2)?.Value ??
					comboBoxEditTabACombo3.Properties.NullText;
				comboBoxEditTabACombo4.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditTabACombo4.Properties.NullText =
					SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(item => item.IsPlaceholder).ElementAtOrDefault(3)?.Value ??
					comboBoxEditTabACombo4.Properties.NullText;

				clipartEditContainerTabA1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab4SubAClipart1Image), SlideContainer.StarInfo.CustomerConfiguration.PartAClipart1Configuration, this);
				clipartEditContainerTabA1.EditValueChanged += OnEditValueChanged;
				clipartEditContainerTabA2.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab4SubAClipart2Image), SlideContainer.StarInfo.CustomerConfiguration.PartAClipart2Configuration, this);
				clipartEditContainerTabA2.EditValueChanged += OnEditValueChanged;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabA.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab4SubBTitle))
			{
				layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab4SubBTitle;

				memoEditTabBSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				memoEditTabBSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

				memoEditTabBSubheader1.Properties.NullText = SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader1Placeholder ?? memoEditTabBSubheader1.Properties.NullText;
				memoEditTabBSubheader2.Properties.NullText = SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader2Placeholder ?? memoEditTabBSubheader2.Properties.NullText;

				clipartEditContainerTabB1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab4SubBClipart1Image), SlideContainer.StarInfo.CustomerConfiguration.PartBClipart1Configuration, this);
				clipartEditContainerTabB1.EditValueChanged += OnEditValueChanged;
				clipartEditContainerTabB2.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab4SubBClipart2Image), SlideContainer.StarInfo.CustomerConfiguration.PartBClipart2Configuration, this);
				clipartEditContainerTabB2.EditValueChanged += OnEditValueChanged;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabB.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab4SubCTitle))
			{
				layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab4SubCTitle;

				memoEditTabCSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				memoEditTabCSubheader2.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				memoEditTabCSubheader3.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

				memoEditTabCSubheader1.Properties.NullText = SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader1Placeholder ?? memoEditTabCSubheader1.Properties.NullText;
				memoEditTabCSubheader2.Properties.NullText = SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader2Placeholder ?? memoEditTabCSubheader2.Properties.NullText;
				memoEditTabCSubheader3.Properties.NullText = SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader3Placeholder ?? memoEditTabCSubheader3.Properties.NullText;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabC.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab4SubUTitle))
			{
				layoutControlGroupTabU.Text = SlideContainer.StarInfo.Titles.Tab4SubUTitle;

				slidesEditContainerTabU.Init(SlideContainer.StarInfo.CustomerConfiguration.PartUSlides);
				slidesEditContainerTabU.SlideOutput += SlideContainer.OnCustomSlideOutput;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabU.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab4SubVTitle))
			{
				layoutControlGroupTabV.Text = SlideContainer.StarInfo.Titles.Tab4SubVTitle;

				slidesEditContainerTabV.Init(SlideContainer.StarInfo.CustomerConfiguration.PartVSlides);
				slidesEditContainerTabV.SlideOutput += SlideContainer.OnCustomSlideOutput;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabV.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab4SubWTitle))
			{
				layoutControlGroupTabW.Text = SlideContainer.StarInfo.Titles.Tab4SubWTitle;

				slidesEditContainerTabW.Init(SlideContainer.StarInfo.CustomerConfiguration.PartWSlides);
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

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab4SubATitle))
			{
				clipartEditContainerTabA1.LoadData(SlideContainer.EditedContent.CustomerState.TabA.Clipart1);
				clipartEditContainerTabA2.LoadData(SlideContainer.EditedContent.CustomerState.TabA.Clipart2);
				comboBoxEditTabACombo1.EditValue = SlideContainer.EditedContent.CustomerState.TabA.Combo1 ??
												   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
													   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0);
				comboBoxEditTabACombo2.EditValue = SlideContainer.EditedContent.CustomerState.TabA.Combo2 ??
												   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
													   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1);
				comboBoxEditTabACombo3.EditValue = SlideContainer.EditedContent.CustomerState.TabA.Combo3 ??
												   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
													   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2);
				comboBoxEditTabACombo4.EditValue = SlideContainer.EditedContent.CustomerState.TabA.Combo4 ??
												   SlideContainer.StarInfo.TargetCustomersLists.CombinedList
													   .Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3);
			}

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab4SubBTitle))
			{
				clipartEditContainerTabB1.LoadData(SlideContainer.EditedContent.CustomerState.TabB.Clipart1);
				clipartEditContainerTabB2.LoadData(SlideContainer.EditedContent.CustomerState.TabB.Clipart2);
				memoEditTabBSubheader1.EditValue = SlideContainer.EditedContent.CustomerState.TabB.Subheader1 ??
												   SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader1DefaultValue;
				memoEditTabBSubheader2.EditValue = SlideContainer.EditedContent.CustomerState.TabB.Subheader2 ??
												   SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader2DefaultValue;
			}

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab4SubCTitle))
			{
				memoEditTabCSubheader1.EditValue = SlideContainer.EditedContent.CustomerState.TabC.Subheader1 ??
												   SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader1DefaultValue;
				memoEditTabCSubheader2.EditValue = SlideContainer.EditedContent.CustomerState.TabC.Subheader2 ??
												   SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader2DefaultValue;
				memoEditTabCSubheader3.EditValue = SlideContainer.EditedContent.CustomerState.TabC.Subheader3 ??
												   SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader3DefaultValue;
			}

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab4SubUTitle))
			{
			}

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab4SubVTitle))
			{
			}

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab4SubWTitle))
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
				SlideContainer.EditedContent.CustomerState.TabA.SlideHeader = SlideContainer.StarInfo.CustomerConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
						null;

				SlideContainer.EditedContent.CustomerState.TabA.Clipart1 = clipartEditContainerTabA1.GetActiveClipartObject();
				SlideContainer.EditedContent.CustomerState.TabA.Clipart2 = clipartEditContainerTabA2.GetActiveClipartObject();

				SlideContainer.EditedContent.CustomerState.TabA.Combo1 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0) != comboBoxEditTabACombo1.EditValue ?
					comboBoxEditTabACombo1.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo1.EditValue as String } :
					null;
				SlideContainer.EditedContent.CustomerState.TabA.Combo2 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1) != comboBoxEditTabACombo2.EditValue ?
					comboBoxEditTabACombo2.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo2.EditValue as String } :
					null;
				SlideContainer.EditedContent.CustomerState.TabA.Combo3 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2) != comboBoxEditTabACombo3.EditValue ?
					comboBoxEditTabACombo3.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo3.EditValue as String } :
					null;
				SlideContainer.EditedContent.CustomerState.TabA.Combo4 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3) != comboBoxEditTabACombo4.EditValue ?
					comboBoxEditTabACombo4.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditTabACombo4.EditValue as String } :
					null;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabB)
			{
				SlideContainer.EditedContent.CustomerState.TabB.SlideHeader = SlideContainer.StarInfo.CustomerConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					null;

				SlideContainer.EditedContent.CustomerState.TabB.Clipart1 = clipartEditContainerTabB1.GetActiveClipartObject();
				SlideContainer.EditedContent.CustomerState.TabB.Clipart2 = clipartEditContainerTabB2.GetActiveClipartObject();

				SlideContainer.EditedContent.CustomerState.TabB.Subheader1 = memoEditTabBSubheader1.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader1DefaultValue ?
					memoEditTabBSubheader1.EditValue as String ?? String.Empty :
					null;
				SlideContainer.EditedContent.CustomerState.TabB.Subheader2 = memoEditTabBSubheader2.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader2DefaultValue ?
					memoEditTabBSubheader2.EditValue as String ?? String.Empty :
					null;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabC)
			{
				SlideContainer.EditedContent.CustomerState.TabC.SlideHeader = SlideContainer.StarInfo.CustomerConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
					comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
					null;

				SlideContainer.EditedContent.CustomerState.TabC.Subheader1 = memoEditTabCSubheader1.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader1DefaultValue ?
					memoEditTabCSubheader1.EditValue as String :
					null;
				SlideContainer.EditedContent.CustomerState.TabC.Subheader2 = memoEditTabCSubheader2.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader2DefaultValue ?
					memoEditTabCSubheader2.EditValue as String ?? String.Empty :
					null;
				SlideContainer.EditedContent.CustomerState.TabC.Subheader3 = memoEditTabCSubheader3.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader3DefaultValue ?
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
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab4SubARightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab4SubAFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CustomerConfiguration.HeadersPartAItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CustomerState.TabA.SlideHeader ??
													SlideContainer.StarInfo.CustomerConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.CustomerConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabB)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab4SubBRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab4SubBFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CustomerConfiguration.HeadersPartBItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CustomerState.TabB.SlideHeader ??
													SlideContainer.StarInfo.CustomerConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.CustomerConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabC)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab4SubCRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab4SubCFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CustomerConfiguration.HeadersPartCItems.Where(item => !item.IsPlaceholder).ToArray());
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CustomerState.TabC.SlideHeader ??
													SlideContainer.StarInfo.CustomerConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.CustomerConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
															  "Select or type";
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabU)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab4SubURightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab4SubUFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabV)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab4SubVRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab4SubVFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabW)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab4SubWRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab4SubWFooterLogo;
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