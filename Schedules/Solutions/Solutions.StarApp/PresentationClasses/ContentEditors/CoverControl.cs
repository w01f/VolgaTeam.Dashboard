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
	public sealed partial class CoverControl : StarAppControl, IStarAppSlide
	{
		private bool _allowToSave;

		public override SlideType SlideType => SlideType.StarAppCover;
		public override string SlideName => SlideContainer.StarInfo.Titles.Tab1Title;

		public CoverControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			Text = SlideName;

			comboBoxEditSlideHeader.EnableSelectAll();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab1SubATitle;

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
			checkEditAddAsPageOne.Checked = SlideContainer.EditedContent.CoverState.AddAsPageOne;
			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.CoverState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;
			SlideContainer.EditedContent.CoverState.AddAsPageOne = checkEditAddAsPageOne.Checked;

			SlideContainer.SettingsContainer.SaveSettings();
		}

		private void LoadPartData()
		{
			_allowToSave = false;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab1SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab1SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CoverLists.HeadersPartA);

					comboBoxEditSlideHeader.EditValue =
					SlideContainer.StarInfo.CoverLists.HeadersPartA.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.CoverState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
					SlideContainer.StarInfo.CoverLists.HeadersPartA.OrderByDescending(h => h.IsDefault).FirstOrDefault();
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