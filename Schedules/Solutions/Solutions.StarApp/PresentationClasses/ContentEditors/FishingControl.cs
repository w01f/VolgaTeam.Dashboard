using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.PresentationClasses.Output;
using DevExpress.Skins;
using DevExpress.XtraLayout;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class FishingControl : StarAppControl, IStarAppSlide
	{
		public override SlideType SlideType => SlideType.StarAppFishing;
		public override string SlideName => SlideContainer.StarInfo.Titles.Tab3Title;

		public FishingControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll();
			memoEditTabASubheader1.EnableSelectAll();
			memoEditTabASubheader2.EnableSelectAll();
			comboBoxEditTabBCombo1.EnableSelectAll();
			comboBoxEditTabBCombo2.EnableSelectAll();
			comboBoxEditTabBCombo3.EnableSelectAll();
			comboBoxEditTabBCombo4.EnableSelectAll();
			memoEditTabCSubheader1.EnableSelectAll();
			memoEditTabCSubheader2.EnableSelectAll();
			memoEditTabCSubheader3.EnableSelectAll();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab3SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab3SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab3SubCTitle;

			pictureEditTabAClipart1.Image = SlideContainer.StarInfo.Tab3SubAClipart1Image;
			pictureEditTabAClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.FishingConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabBClipart1.Image = SlideContainer.StarInfo.Tab3SubBClipart1Image;
			pictureEditTabBClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.FishingConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = SlideContainer.StarInfo.Tab3SubBClipart2Image;
			pictureEditTabBClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.FishingConfiguration.PartBClipart2Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabAClipart1,
				pictureEditTabBClipart1,
				pictureEditTabBClipart2,
			});

			comboBoxEditTabBCombo1.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList);
			comboBoxEditTabBCombo2.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList);
			comboBoxEditTabBCombo3.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList);
			comboBoxEditTabBCombo4.Properties.Items.AddRange(SlideContainer.StarInfo.TargetCustomersLists.CombinedList);

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			pictureEditTabAClipart1.Image = SlideContainer.EditedContent.FishingState.TabA.Clipart1 ??
				pictureEditTabAClipart1.Image;
			memoEditTabASubheader1.EditValue = SlideContainer.EditedContent.FishingState.TabA.Subheader1 ??
				SlideContainer.StarInfo.FishingConfiguration.PartASubHeader1DefaultValue;
			memoEditTabASubheader2.EditValue = SlideContainer.EditedContent.FishingState.TabA.Subheader2 ??
				SlideContainer.StarInfo.FishingConfiguration.PartASubHeader2DefaultValue;

			pictureEditTabBClipart1.Image = SlideContainer.EditedContent.FishingState.TabB.Clipart1 ??
				pictureEditTabBClipart1.Image;
			pictureEditTabBClipart2.Image = SlideContainer.EditedContent.FishingState.TabB.Clipart2 ??
				pictureEditTabBClipart2.Image;
			comboBoxEditTabBCombo1.EditValue = SlideContainer.EditedContent.FishingState.TabB.Combo1 ??
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0);
			comboBoxEditTabBCombo2.EditValue = SlideContainer.EditedContent.FishingState.TabB.Combo2 ??
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1);
			comboBoxEditTabBCombo3.EditValue = SlideContainer.EditedContent.FishingState.TabB.Combo3 ??
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2);
			comboBoxEditTabBCombo4.EditValue = SlideContainer.EditedContent.FishingState.TabB.Combo4 ??
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3);

			memoEditTabCSubheader1.EditValue = SlideContainer.EditedContent.FishingState.TabC.Subheader1 ??
				SlideContainer.StarInfo.FishingConfiguration.PartCSubHeader1DefaultValue;
			memoEditTabCSubheader2.EditValue = SlideContainer.EditedContent.FishingState.TabC.Subheader2 ??
				SlideContainer.StarInfo.FishingConfiguration.PartCSubHeader2DefaultValue;
			memoEditTabCSubheader3.EditValue = SlideContainer.EditedContent.FishingState.TabC.Subheader3 ??
				SlideContainer.StarInfo.FishingConfiguration.PartCSubHeader3DefaultValue;

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					SlideContainer.EditedContent.FishingState.TabA.SlideHeader = SlideContainer.StarInfo.FishingConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;

					SlideContainer.EditedContent.FishingState.TabA.Clipart1 = pictureEditTabAClipart1.Image != SlideContainer.StarInfo.Tab4SubAClipart1Image ?
						pictureEditTabAClipart1.Image :
						null;

					SlideContainer.EditedContent.FishingState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.StarInfo.FishingConfiguration.PartASubHeader1DefaultValue ?
						memoEditTabASubheader1.EditValue as String :
						null;
					SlideContainer.EditedContent.FishingState.TabA.Subheader2 = memoEditTabASubheader2.EditValue as String != SlideContainer.StarInfo.FishingConfiguration.PartASubHeader2DefaultValue ?
						memoEditTabASubheader2.EditValue as String :
						null;
					break;
				case 1:
					SlideContainer.EditedContent.FishingState.TabB.SlideHeader = SlideContainer.StarInfo.FishingConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;

					SlideContainer.EditedContent.FishingState.TabB.Clipart1 = pictureEditTabBClipart1.Image != SlideContainer.StarInfo.Tab4SubBClipart1Image ?
						pictureEditTabBClipart1.Image :
						null;
					SlideContainer.EditedContent.FishingState.TabB.Clipart2 = pictureEditTabBClipart2.Image != SlideContainer.StarInfo.Tab4SubBClipart2Image ?
						pictureEditTabBClipart2.Image :
						null;

					SlideContainer.EditedContent.FishingState.TabB.Combo1 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0) != comboBoxEditTabBCombo1.EditValue ?
						comboBoxEditTabBCombo1.EditValue as ListDataItem ?? (comboBoxEditTabBCombo1.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabBCombo1.EditValue } : null) :
						null;
					SlideContainer.EditedContent.FishingState.TabB.Combo2 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1) != comboBoxEditTabBCombo2.EditValue ?
						comboBoxEditTabBCombo2.EditValue as ListDataItem ?? (comboBoxEditTabBCombo2.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabBCombo2.EditValue } : null) :
						null;
					SlideContainer.EditedContent.FishingState.TabB.Combo3 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2) != comboBoxEditTabBCombo3.EditValue ?
						comboBoxEditTabBCombo3.EditValue as ListDataItem ?? (comboBoxEditTabBCombo3.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabBCombo3.EditValue } : null) :
						null;
					SlideContainer.EditedContent.FishingState.TabB.Combo4 = SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3) != comboBoxEditTabBCombo4.EditValue ?
						comboBoxEditTabBCombo4.EditValue as ListDataItem ?? (comboBoxEditTabBCombo4.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabBCombo4.EditValue } : null) :
						null;
					break;
				case 2:
					SlideContainer.EditedContent.FishingState.TabC.SlideHeader = SlideContainer.StarInfo.FishingConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;

					SlideContainer.EditedContent.FishingState.TabC.Subheader1 = memoEditTabCSubheader1.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader1DefaultValue ?
						memoEditTabCSubheader1.EditValue as String :
						null;
					SlideContainer.EditedContent.FishingState.TabC.Subheader2 = memoEditTabCSubheader2.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader2DefaultValue ?
						memoEditTabCSubheader2.EditValue as String :
						null;
					SlideContainer.EditedContent.FishingState.TabC.Subheader3 = memoEditTabCSubheader3.EditValue as String != SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader3DefaultValue ?
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
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab3SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab3SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.FishingConfiguration.HeadersPartAItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.FishingState.TabA.SlideHeader ??
						SlideContainer.StarInfo.FishingConfiguration.HeadersPartAItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab3SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab3SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.FishingConfiguration.HeadersPartBItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.FishingState.TabB.SlideHeader ??
						SlideContainer.StarInfo.FishingConfiguration.HeadersPartBItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab3SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab3SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.FishingConfiguration.HeadersPartCItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.FishingState.TabC.SlideHeader ??
						SlideContainer.StarInfo.FishingConfiguration.HeadersPartCItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
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

		#region Output Staff

		public override bool ReadyForOutput => false;

		public override void GenerateOutput()
		{
			//SolutionDashboardPowerPointHelper.Instance.AppendCover(this);
		}

		public override PreviewGroup GeneratePreview()
		{
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			//SolutionDashboardPowerPointHelper.Instance.PrepareCover(this, tempFileName);
			return new PreviewGroup { Name = SlideName, PresentationSourcePath = tempFileName };
		}
		#endregion
	}
}