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
	public sealed partial class SolutionControl : StarAppControl, IStarAppSlide
	{
		private bool _allowToSave;

		public override SlideType SlideType => SlideType.StarAppSolution;
		public override string SlideName => SlideContainer.StarInfo.Titles.Tab10Title;

		public SolutionControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			Text = SlideName;
			
			comboBoxEditSlideHeader.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.SolutionLists.Headers);

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab10SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab10SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab10SubCTitle;
			layoutControlGroupTabD.Text = SlideContainer.StarInfo.Titles.Tab10SubDTitle;
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
					SlideContainer.StarInfo.SolutionLists.Headers.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.SolutionState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
					SlideContainer.StarInfo.SolutionLists.Headers.OrderByDescending(h => h.IsDefault).FirstOrDefault();

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.SolutionState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;

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
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab10SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab10SubAFooterLogo;
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab10SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab10SubBFooterLogo;
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab10SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab10SubCFooterLogo;
					break;
				case 3:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab10SubDRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab10SubDFooterLogo;
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