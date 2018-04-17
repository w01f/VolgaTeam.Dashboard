using System;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using DevExpress.Skins;
using DevExpress.XtraLayout;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class CustomerControl : StarAppControl
	{
		public override SlideType SlideType => SlideType.StarAppCustomer;
		public override string OutputName => SlideContainer.StarInfo.Titles.Tab4Title;

		public CustomerControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll();
			comboBoxEditTabACombo1.EnableSelectAll();
			comboBoxEditTabACombo2.EnableSelectAll();
			comboBoxEditTabACombo3.EnableSelectAll();
			comboBoxEditTabACombo4.EnableSelectAll();
			memoEditTabBSubheader1.EnableSelectAll();
			memoEditTabBSubheader2.EnableSelectAll();
			memoEditTabCSubheader1.EnableSelectAll();
			memoEditTabCSubheader2.EnableSelectAll();
			memoEditTabCSubheader3.EnableSelectAll();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab4SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab4SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab4SubCTitle;

			pictureEditTabAClipart1.Image = SlideContainer.StarInfo.Tab4SubAClipart1Image;
			pictureEditTabAClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.CustomerConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabAClipart2.Image = SlideContainer.StarInfo.Tab4SubAClipart2Image;
			pictureEditTabAClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.CustomerConfiguration.PartAClipart2Configuration.Alignment;
			pictureEditTabBClipart1.Image = SlideContainer.StarInfo.Tab4SubBClipart1Image;
			pictureEditTabBClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.CustomerConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = SlideContainer.StarInfo.Tab4SubBClipart2Image;
			pictureEditTabBClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.CustomerConfiguration.PartBClipart2Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabAClipart1,
				pictureEditTabAClipart2,
				pictureEditTabBClipart1,
				pictureEditTabBClipart2,
			});

			comboBoxEditTabACombo1.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList);
			comboBoxEditTabACombo2.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList);
			comboBoxEditTabACombo3.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList);
			comboBoxEditTabACombo4.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList);

			_outputProcessors.AddRange(OutputProcessor.GetOutputProcessors(this));

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabAClipart1.Image = SlideContainer.EditedContent.CustomerState.TabA.Clipart1 ??
				pictureEditTabAClipart1.Image;
			pictureEditTabAClipart2.Image = SlideContainer.EditedContent.CustomerState.TabA.Clipart2 ??
				pictureEditTabAClipart2.Image;
			comboBoxEditTabACombo1.EditValue = SlideContainer.EditedContent.CustomerState.TabA.Combo1 ??
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0);
			comboBoxEditTabACombo2.EditValue = SlideContainer.EditedContent.CustomerState.TabA.Combo2 ??
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1);
			comboBoxEditTabACombo3.EditValue = SlideContainer.EditedContent.CustomerState.TabA.Combo3 ??
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2);
			comboBoxEditTabACombo4.EditValue = SlideContainer.EditedContent.CustomerState.TabA.Combo4 ??
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3);

			pictureEditTabBClipart1.Image = SlideContainer.EditedContent.CustomerState.TabB.Clipart1 ??
				pictureEditTabBClipart1.Image;
			pictureEditTabBClipart2.Image = SlideContainer.EditedContent.CustomerState.TabB.Clipart2 ??
				pictureEditTabBClipart2.Image;
			memoEditTabBSubheader1.EditValue = SlideContainer.EditedContent.CustomerState.TabB.Subheader1 ??
				SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader1DefaultValue;
			memoEditTabBSubheader2.EditValue = SlideContainer.EditedContent.CustomerState.TabB.Subheader2 ??
				SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader2DefaultValue;

			memoEditTabCSubheader1.EditValue = SlideContainer.EditedContent.CustomerState.TabC.Subheader1 ??
				SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader1DefaultValue;
			memoEditTabCSubheader2.EditValue = SlideContainer.EditedContent.CustomerState.TabC.Subheader2 ??
				SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader2DefaultValue;
			memoEditTabCSubheader3.EditValue = SlideContainer.EditedContent.CustomerState.TabC.Subheader3 ??
				SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader3DefaultValue;

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					SlideContainer.EditedContent.CustomerState.TabA.SlideHeader = SlideContainer.StarInfo.CustomerConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;

					SlideContainer.EditedContent.CustomerState.TabA.Clipart1 = pictureEditTabAClipart1.Image != SlideContainer.StarInfo.Tab4SubAClipart1Image ?
						pictureEditTabAClipart1.Image :
						null;
					SlideContainer.EditedContent.CustomerState.TabA.Clipart2 = pictureEditTabAClipart2.Image != SlideContainer.StarInfo.Tab4SubAClipart2Image ?
						pictureEditTabAClipart2.Image :
						null;

					SlideContainer.EditedContent.CustomerState.TabA.Combo1 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0) != comboBoxEditTabACombo1.EditValue ?
						comboBoxEditTabACombo1.EditValue as ListDataItem ?? (comboBoxEditTabACombo1.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabACombo1.EditValue } : null) :
						null;
					SlideContainer.EditedContent.CustomerState.TabA.Combo2 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1) != comboBoxEditTabACombo2.EditValue ?
						comboBoxEditTabACombo2.EditValue as ListDataItem ?? (comboBoxEditTabACombo2.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabACombo2.EditValue } : null) :
						null;
					SlideContainer.EditedContent.CustomerState.TabA.Combo3 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2) != comboBoxEditTabACombo3.EditValue ?
						comboBoxEditTabACombo3.EditValue as ListDataItem ?? (comboBoxEditTabACombo3.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabACombo3.EditValue } : null) :
						null;
					SlideContainer.EditedContent.CustomerState.TabA.Combo4 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3) != comboBoxEditTabACombo4.EditValue ?
						comboBoxEditTabACombo4.EditValue as ListDataItem ?? (comboBoxEditTabACombo4.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabACombo4.EditValue } : null) :
						null;
					break;
				case 1:
					SlideContainer.EditedContent.CustomerState.TabB.SlideHeader = SlideContainer.StarInfo.CustomerConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;

					SlideContainer.EditedContent.CustomerState.TabB.Clipart1 = pictureEditTabBClipart1.Image != SlideContainer.StarInfo.Tab4SubBClipart1Image ?
						pictureEditTabBClipart1.Image :
						null;
					SlideContainer.EditedContent.CustomerState.TabB.Clipart2 = pictureEditTabBClipart2.Image != SlideContainer.StarInfo.Tab4SubBClipart2Image ?
						pictureEditTabBClipart2.Image :
						null;

					SlideContainer.EditedContent.CustomerState.TabB.Subheader1 = memoEditTabBSubheader1.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader1DefaultValue ?
						memoEditTabBSubheader1.EditValue as String :
						null;
					SlideContainer.EditedContent.CustomerState.TabB.Subheader2 = memoEditTabBSubheader2.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader2DefaultValue ?
						memoEditTabBSubheader2.EditValue as String :
						null;
					break;
				case 2:
					SlideContainer.EditedContent.CustomerState.TabC.SlideHeader = SlideContainer.StarInfo.CustomerConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;

					SlideContainer.EditedContent.CustomerState.TabC.Subheader1 = memoEditTabCSubheader1.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader1DefaultValue ?
						memoEditTabCSubheader1.EditValue as String :
						null;
					SlideContainer.EditedContent.CustomerState.TabC.Subheader2 = memoEditTabCSubheader2.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader2DefaultValue ?
						memoEditTabCSubheader2.EditValue as String :
						null;
					SlideContainer.EditedContent.CustomerState.TabC.Subheader3 = memoEditTabCSubheader3.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader3DefaultValue ?
						memoEditTabCSubheader3.EditValue as String :
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
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab4SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab4SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CustomerConfiguration.HeadersPartAItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CustomerState.TabA.SlideHeader ??
						SlideContainer.StarInfo.CustomerConfiguration.HeadersPartAItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab4SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab4SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CustomerConfiguration.HeadersPartBItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CustomerState.TabB.SlideHeader ??
						SlideContainer.StarInfo.CustomerConfiguration.HeadersPartBItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab4SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab4SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CustomerConfiguration.HeadersPartCItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CustomerState.TabC.SlideHeader ??
						SlideContainer.StarInfo.CustomerConfiguration.HeadersPartCItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
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