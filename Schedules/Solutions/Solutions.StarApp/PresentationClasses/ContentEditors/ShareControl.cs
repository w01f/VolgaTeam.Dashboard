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
	public sealed partial class ShareControl : StarAppControl, IStarAppSlide
	{
		private bool _allowToSave;

		public override SlideType SlideType => SlideType.StarAppShare;
		public override string SlideName => SlideContainer.StarInfo.Titles.Tab5Title;

		public ShareControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			Text = SlideName;
			
			comboBoxEditSlideHeader.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareLists.Headers);

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab5SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab5SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab5SubCTitle;
			layoutControlGroupTabD.Text = SlideContainer.StarInfo.Titles.Tab5SubDTitle;
			layoutControlGroupTabE.Text = SlideContainer.StarInfo.Titles.Tab5SubETitle;
			OnSelectedPageChanged(null, new LayoutTabPageChangedEventArgs(null, layoutControlGroupTabA));

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
			_allowToSave = false;
			comboBoxEditSlideHeader.EditValue =
					SlideContainer.StarInfo.ShareLists.Headers.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.ShareState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
					SlideContainer.StarInfo.ShareLists.Headers.OrderByDescending(h => h.IsDefault).FirstOrDefault();

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.ShareState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;

			SlideContainer.SettingsContainer.SaveSettings();
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SlideContainer.RaiseDataChanged();
		}

		private void OnSelectedPageChanged(object sender, LayoutTabPageChangedEventArgs e)
		{
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubAFooterLogo;
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubBFooterLogo;
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubCFooterLogo;
					break;
				case 3:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubDRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubDFooterLogo;
					break;
				case 4:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubERightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubEFooterLogo;
					break;
			}
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