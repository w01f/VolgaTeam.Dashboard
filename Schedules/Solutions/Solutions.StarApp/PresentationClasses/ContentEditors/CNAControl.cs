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
	public sealed partial class CNAControl : StarAppControl, IStarAppSlide
	{
		private bool _allowToSave;

		public override SlideType SlideType => SlideType.StarAppCNA;
		public override string SlideName => SlideContainer.StarInfo.Titles.Tab2Title;

		public CNAControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			Text = SlideName;
			
			comboBoxEditSlideHeader.EnableSelectAll();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab2SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab2SubBTitle;
			
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
			SlideContainer.EditedContent.CNAState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;

			SlideContainer.SettingsContainer.SaveSettings();
		}

		private void LoadPartData()
		{
			_allowToSave = false;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab2SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab2SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CNALists.HeadersPartA);

					comboBoxEditSlideHeader.EditValue =
						SlideContainer.StarInfo.CNALists.HeadersPartA.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.CNAState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
						SlideContainer.StarInfo.CNALists.HeadersPartA.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab2SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab2SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CNALists.HeadersPartB);

					comboBoxEditSlideHeader.EditValue =
						SlideContainer.StarInfo.CNALists.HeadersPartB.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.CNAState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
						SlideContainer.StarInfo.CNALists.HeadersPartB.OrderByDescending(h => h.IsDefault).FirstOrDefault();
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