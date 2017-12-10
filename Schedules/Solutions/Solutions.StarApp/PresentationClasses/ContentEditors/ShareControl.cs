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
using DevExpress.XtraLayout.Utils;

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

			Resize += OnResize;
			
			Text = SlideName;
			
			comboBoxEditSlideHeader.EnableSelectAll();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab5SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab5SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab5SubCTitle;
			layoutControlGroupTabD.Text = SlideContainer.StarInfo.Titles.Tab5SubDTitle;
			layoutControlGroupTabE.Text = SlideContainer.StarInfo.Titles.Tab5SubETitle;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);
			layoutControlItemLogoRight.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogoRight.MaxSize, scaleFactor);
			layoutControlItemLogoRight.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogoRight.MinSize, scaleFactor);
			layoutControlItemLogoFooter.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogoFooter.MaxSize, scaleFactor);
			layoutControlItemLogoFooter.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogoFooter.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			LoadPartData();
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.ShareState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;

			SlideContainer.SettingsContainer.SaveSettings();
		}

		private void LoadPartData()
		{
			_allowToSave = false;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareLists.HeadersPartA);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.ShareLists.HeadersPartA.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.ShareState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.ShareLists.HeadersPartA.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareLists.HeadersPartB);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.ShareLists.HeadersPartB.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.ShareState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.ShareLists.HeadersPartB.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareLists.HeadersPartC);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.ShareLists.HeadersPartC.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.ShareState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.ShareLists.HeadersPartC.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 3:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubDRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubDFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareLists.HeadersPartD);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.ShareLists.HeadersPartD.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.ShareState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.ShareLists.HeadersPartD.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 4:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab5SubERightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab5SubEFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareLists.HeadersPartE);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.ShareLists.HeadersPartE.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.ShareState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.ShareLists.HeadersPartE.OrderByDescending(h => h.IsDefault).FirstOrDefault();
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
			if(_allowToSave)
				ApplyChanges();
			LoadPartData();
		}

		private void OnResize(object sender, EventArgs e)
		{
			var showLogos = Width > 1000;
			layoutControlItemLogoFooter.Visibility =
				layoutControlItemLogoRight.Visibility =
					showLogos ? LayoutVisibility.Always : LayoutVisibility.Never;
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