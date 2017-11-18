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
	public sealed partial class VideoControl : StarAppControl, IStarAppSlide
	{
		private bool _allowToSave;

		public override SlideType SlideType => SlideType.StarAppVideo;
		public override string SlideName => SlideContainer.StarInfo.Titles.Tab8Title;

		public VideoControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			Text = SlideName;

			comboBoxEditSlideHeader.EnableSelectAll();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab8SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab8SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab8SubCTitle;
			layoutControlGroupTabD.Text = SlideContainer.StarInfo.Titles.Tab8SubDTitle;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);
			layoutControlItemLogoRight.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogoRight.MaxSize, scaleFactor);
			layoutControlItemLogoRight.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogoRight.MinSize, scaleFactor);
			layoutControlItemLogoFooter.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogoFooter.MaxSize, scaleFactor);
			layoutControlItemLogoFooter.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogoFooter.MinSize, scaleFactor);
		}

		public override void LoadData()
		{
			LoadPartData();
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.VideoState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;

			SlideContainer.SettingsContainer.SaveSettings();
		}

		private void LoadPartData()
		{
			_allowToSave = false;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab8SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab8SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.VideoLists.HeadersPartB);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.VideoLists.HeadersPartB.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.VideoState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.VideoLists.HeadersPartB.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab8SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab8SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.VideoLists.HeadersPartB);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.VideoLists.HeadersPartB.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.VideoState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.VideoLists.HeadersPartB.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab8SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab8SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.VideoLists.HeadersPartC);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.VideoLists.HeadersPartC.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.VideoState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.VideoLists.HeadersPartC.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 3:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab8SubDRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab8SubDFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.VideoLists.HeadersPartD);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.VideoLists.HeadersPartD.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.VideoState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.VideoLists.HeadersPartD.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
			}
			_allowToSave = true;
		}

		private void EditValueChanged(object sender, EventArgs e)
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