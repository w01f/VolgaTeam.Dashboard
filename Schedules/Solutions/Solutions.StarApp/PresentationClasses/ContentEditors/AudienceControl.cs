using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.StarApp.PresentationClasses.Output;
using DevExpress.Skins;
using DevExpress.XtraLayout;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class AudienceControl : StarAppControl, IStarAppSlide
	{
		private bool _allowToSave;

		public override SlideType SlideType => SlideType.StarAppAudience;
		public override string SlideName => SlideContainer.StarInfo.Titles.Tab9Title;

		public AudienceControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll();
			memoEditTabASubheader1.EnableSelectAll();
			memoEditTabASubheader2.EnableSelectAll();
			textEditTabBSubheader1.EnableSelectAll();
			textEditTabBSubheader2.EnableSelectAll();
			textEditTabBSubheader3.EnableSelectAll();
			memoEditTabBSubheader4.EnableSelectAll();
			memoEditTabBSubheader5.EnableSelectAll();
			memoEditTabBSubheader6.EnableSelectAll();
			Application.DoEvents();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab9SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab9SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab9SubCTitle;
			Application.DoEvents();

			pictureEditTabAClipart1.Image = SlideContainer.StarInfo.Tab9SubAClipart1Image;
			pictureEditTabAClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabAClipart2.Image = SlideContainer.StarInfo.Tab9SubAClipart2Image;
			pictureEditTabAClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartAClipart2Configuration.Alignment;
			pictureEditTabBClipart1.Image = SlideContainer.StarInfo.Tab9SubBClipart1Image;
			pictureEditTabBClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = SlideContainer.StarInfo.Tab9SubBClipart2Image;
			pictureEditTabBClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartBClipart2Configuration.Alignment;
			pictureEditTabBClipart3.Image = SlideContainer.StarInfo.Tab9SubBClipart3Image;
			pictureEditTabBClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartBClipart3Configuration.Alignment;
			pictureEditTabCClipart1.Image = SlideContainer.StarInfo.Tab9SubCClipart1Image;
			pictureEditTabCClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartCClipart1Configuration.Alignment;
			pictureEditTabCClipart2.Image = SlideContainer.StarInfo.Tab9SubCClipart2Image;
			pictureEditTabCClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartCClipart2Configuration.Alignment;
			pictureEditTabCClipart3.Image = SlideContainer.StarInfo.Tab9SubCClipart3Image;
			pictureEditTabCClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartCClipart3Configuration.Alignment;
			pictureEditTabCClipart4.Image = SlideContainer.StarInfo.Tab9SubCClipart4Image;
			pictureEditTabCClipart4.Properties.PictureAlignment =
				SlideContainer.StarInfo.AudienceConfiguration.PartCClipart4Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditTabAClipart1,
				pictureEditTabAClipart2,
				pictureEditTabBClipart1,
				pictureEditTabBClipart2,
				pictureEditTabBClipart3,
				pictureEditTabCClipart1,
				pictureEditTabCClipart2,
				pictureEditTabCClipart3,
				pictureEditTabCClipart4,
			});

			Application.DoEvents();

			comboBoxEditTabCCombo1.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceConfiguration.PartCCombo1Items);
			Application.DoEvents();

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			comboBoxEditTabCCombo1.EditValue =
				SlideContainer.StarInfo.AudienceConfiguration.PartCCombo1Items.FirstOrDefault(item => item.IsDefault);

			memoEditTabASubheader1.EditValue = SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader1DefaultValue;
			memoEditTabASubheader2.EditValue = SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader2DefaultValue;

			textEditTabBSubheader1.EditValue = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader1DefaultValue;
			textEditTabBSubheader2.EditValue = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader2DefaultValue;
			textEditTabBSubheader3.EditValue = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader3DefaultValue;
			memoEditTabBSubheader4.EditValue = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader4DefaultValue;
			memoEditTabBSubheader5.EditValue = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader5DefaultValue;
			memoEditTabBSubheader6.EditValue = SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader6DefaultValue;

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.AudienceState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;

			SlideContainer.SettingsContainer.SaveSettings();
		}

		private void LoadPartData()
		{
			_allowToSave = false;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab9SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab9SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceConfiguration.HeadersPartAItems);
					comboBoxEditSlideHeader.EditValue =
					SlideContainer.StarInfo.AudienceConfiguration.HeadersPartAItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.AudienceState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
					SlideContainer.StarInfo.AudienceConfiguration.HeadersPartAItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab9SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab9SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceConfiguration.HeadersPartBItems);
					comboBoxEditSlideHeader.EditValue =
					SlideContainer.StarInfo.AudienceConfiguration.HeadersPartBItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.AudienceState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
					SlideContainer.StarInfo.AudienceConfiguration.HeadersPartBItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab9SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab9SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceConfiguration.HeadersPartCItems);
					comboBoxEditSlideHeader.EditValue =
					SlideContainer.StarInfo.AudienceConfiguration.HeadersPartCItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.AudienceState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
					SlideContainer.StarInfo.AudienceConfiguration.HeadersPartCItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
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

		//public override bool ReadyForOutput => false;

		//public override void GenerateOutput()
		//{
		//	SolutionDashboardPowerPointHelper.Instance.AppendCover(this);
		//}

		//public override PreviewGroup GeneratePreview()
		//{
		//	var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
		//	SolutionDashboardPowerPointHelper.Instance.PrepareCover(this, tempFileName);
		//	return new PreviewGroup { Name = SlideName, PresentationSourcePath = tempFileName };
		//}
		#endregion
	}
}