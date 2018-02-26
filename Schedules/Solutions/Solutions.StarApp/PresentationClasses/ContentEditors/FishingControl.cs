using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
		private bool _allowToSave;

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
			memoEditTabASubheader1.EditValue = SlideContainer.StarInfo.FishingConfiguration.PartASubHeader1DefaultValue;
			memoEditTabASubheader2.EditValue = SlideContainer.StarInfo.FishingConfiguration.PartASubHeader2DefaultValue;

			comboBoxEditTabBCombo1.EditValue =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0);
			comboBoxEditTabBCombo2.EditValue =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1);
			comboBoxEditTabBCombo3.EditValue =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2);
			comboBoxEditTabBCombo4.EditValue =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3);

			memoEditTabCSubheader1.EditValue = SlideContainer.StarInfo.FishingConfiguration.PartCSubHeader1DefaultValue;
			memoEditTabCSubheader2.EditValue = SlideContainer.StarInfo.FishingConfiguration.PartCSubHeader2DefaultValue;
			memoEditTabCSubheader3.EditValue = SlideContainer.StarInfo.FishingConfiguration.PartCSubHeader3DefaultValue;
			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.FishingState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;

			SlideContainer.SettingsContainer.SaveSettings();
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
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.FishingConfiguration.HeadersPartAItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.FishingState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.FishingConfiguration.HeadersPartAItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab3SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab3SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.FishingConfiguration.HeadersPartBItems);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.FishingConfiguration.HeadersPartBItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.FishingState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.FishingConfiguration.HeadersPartBItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab3SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab3SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.FishingConfiguration.HeadersPartCItems);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.FishingConfiguration.HeadersPartCItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.FishingState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.FishingConfiguration.HeadersPartCItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
			}
			_allowToSave = true;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SlideContainer.RaiseDataChanged();
		}

		private void OnSelectedPageChanged(object sender, LayoutTabPageChangedEventArgs e)
		{
			if (_allowToSave)
				ApplyChanges();
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