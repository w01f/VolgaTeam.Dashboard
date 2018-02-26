﻿using System;
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
	public sealed partial class CustomerControl : StarAppControl, IStarAppSlide
	{
		private bool _allowToSave;

		public override SlideType SlideType => SlideType.StarAppCustomer;
		public override string SlideName => SlideContainer.StarInfo.Titles.Tab4Title;

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

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);
		
			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;
			comboBoxEditTabACombo1.EditValue =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0);
			comboBoxEditTabACombo2.EditValue =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1);
			comboBoxEditTabACombo3.EditValue =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2);
			comboBoxEditTabACombo4.EditValue =
				SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3);

			memoEditTabBSubheader1.EditValue = SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader1DefaultValue;
			memoEditTabBSubheader2.EditValue = SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader2DefaultValue;

			memoEditTabCSubheader1.EditValue = SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader1DefaultValue;
			memoEditTabCSubheader2.EditValue = SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader2DefaultValue;
			memoEditTabCSubheader3.EditValue = SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader3DefaultValue;

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.CustomerState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;

			SlideContainer.SettingsContainer.SaveSettings();
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
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.CustomerConfiguration.HeadersPartAItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.CustomerState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.CustomerConfiguration.HeadersPartAItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab4SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab4SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CustomerConfiguration.HeadersPartBItems);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.CustomerConfiguration.HeadersPartBItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.CustomerState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.CustomerConfiguration.HeadersPartBItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab4SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab4SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CustomerConfiguration.HeadersPartCItems);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.CustomerConfiguration.HeadersPartCItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.CustomerState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.CustomerConfiguration.HeadersPartCItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
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