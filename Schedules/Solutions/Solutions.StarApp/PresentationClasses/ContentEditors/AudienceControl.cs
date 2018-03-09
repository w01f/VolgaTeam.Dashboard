using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
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

			pictureEditTabAClipart1.Image = SlideContainer.EditedContent.AudienceState.TabA.Clipart1 ??
				pictureEditTabAClipart1.Image;
			pictureEditTabAClipart2.Image = SlideContainer.EditedContent.AudienceState.TabA.Clipart2 ??
				pictureEditTabAClipart2.Image;
			memoEditTabASubheader1.EditValue = SlideContainer.EditedContent.AudienceState.TabA.Subheader1 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader1DefaultValue;
			memoEditTabASubheader2.EditValue = SlideContainer.EditedContent.AudienceState.TabA.Subheader2 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader2DefaultValue;

			pictureEditTabBClipart1.Image = SlideContainer.EditedContent.AudienceState.TabB.Clipart1 ??
											pictureEditTabBClipart1.Image;
			pictureEditTabBClipart2.Image = SlideContainer.EditedContent.AudienceState.TabB.Clipart2 ??
											pictureEditTabBClipart2.Image;
			pictureEditTabBClipart3.Image = SlideContainer.EditedContent.AudienceState.TabB.Clipart3 ??
											pictureEditTabBClipart3.Image;
			textEditTabBSubheader1.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader1 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader1DefaultValue;
			textEditTabBSubheader2.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader2 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader2DefaultValue;
			textEditTabBSubheader3.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader3 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader3DefaultValue;
			memoEditTabBSubheader4.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader4 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader4DefaultValue;
			memoEditTabBSubheader5.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader5 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader5DefaultValue;
			memoEditTabBSubheader6.EditValue = SlideContainer.EditedContent.AudienceState.TabB.Subheader6 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader6DefaultValue;

			pictureEditTabCClipart1.Image = SlideContainer.EditedContent.AudienceState.TabC.Clipart1 ??
											pictureEditTabCClipart1.Image;
			pictureEditTabCClipart2.Image = SlideContainer.EditedContent.AudienceState.TabC.Clipart2 ??
											pictureEditTabCClipart2.Image;
			pictureEditTabCClipart3.Image = SlideContainer.EditedContent.AudienceState.TabC.Clipart3 ??
											pictureEditTabCClipart3.Image;
			pictureEditTabCClipart4.Image = SlideContainer.EditedContent.AudienceState.TabC.Clipart4 ??
											pictureEditTabCClipart4.Image;
			comboBoxEditTabCCombo1.EditValue = SlideContainer.EditedContent.AudienceState.TabC.Combo1 ??
				SlideContainer.StarInfo.AudienceConfiguration.PartCCombo1Items.FirstOrDefault(item => item.IsDefault);

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					SlideContainer.EditedContent.AudienceState.TabA.SlideHeader = SlideContainer.StarInfo.AudienceConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;

					SlideContainer.EditedContent.AudienceState.TabA.Clipart1 = pictureEditTabAClipart1.Image != SlideContainer.StarInfo.Tab9SubAClipart1Image ?
						pictureEditTabAClipart1.Image :
						null;
					SlideContainer.EditedContent.AudienceState.TabA.Clipart2 = pictureEditTabAClipart2.Image != SlideContainer.StarInfo.Tab9SubAClipart2Image ?
						pictureEditTabAClipart2.Image :
						null;

					SlideContainer.EditedContent.AudienceState.TabA.Subheader1 = memoEditTabASubheader1.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader1DefaultValue ?
						memoEditTabASubheader1.EditValue as String :
						null;
					SlideContainer.EditedContent.AudienceState.TabA.Subheader2 = memoEditTabASubheader2.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader2DefaultValue ?
						memoEditTabASubheader2.EditValue as String :
						null;
					break;
				case 1:
					SlideContainer.EditedContent.AudienceState.TabB.SlideHeader = SlideContainer.StarInfo.AudienceConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;

					SlideContainer.EditedContent.AudienceState.TabB.Clipart1 = pictureEditTabBClipart1.Image != SlideContainer.StarInfo.Tab9SubBClipart1Image ?
						pictureEditTabBClipart1.Image :
						null;
					SlideContainer.EditedContent.AudienceState.TabB.Clipart2 = pictureEditTabBClipart2.Image != SlideContainer.StarInfo.Tab9SubBClipart2Image ?
						pictureEditTabBClipart2.Image :
						null;
					SlideContainer.EditedContent.AudienceState.TabB.Clipart3 = pictureEditTabBClipart3.Image != SlideContainer.StarInfo.Tab9SubBClipart3Image ?
						pictureEditTabBClipart3.Image :
						null;

					SlideContainer.EditedContent.AudienceState.TabB.Subheader1 = textEditTabBSubheader1.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader1DefaultValue ?
						textEditTabBSubheader1.EditValue as String :
						null;
					SlideContainer.EditedContent.AudienceState.TabB.Subheader2 = textEditTabBSubheader2.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader2DefaultValue ?
						textEditTabBSubheader2.EditValue as String :
						null;
					SlideContainer.EditedContent.AudienceState.TabB.Subheader3 = textEditTabBSubheader3.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader3DefaultValue ?
						textEditTabBSubheader3.EditValue as String :
						null;
					SlideContainer.EditedContent.AudienceState.TabB.Subheader4 = memoEditTabBSubheader4.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader4DefaultValue ?
						memoEditTabBSubheader4.EditValue as String :
						null;
					SlideContainer.EditedContent.AudienceState.TabB.Subheader5 = memoEditTabBSubheader4.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader5DefaultValue ?
						memoEditTabBSubheader5.EditValue as String :
						null;
					SlideContainer.EditedContent.AudienceState.TabB.Subheader6 = memoEditTabBSubheader4.EditValue as String != SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader6DefaultValue ?
						memoEditTabBSubheader6.EditValue as String :
						null;
					break;
				case 2:
					SlideContainer.EditedContent.AudienceState.TabC.SlideHeader = SlideContainer.StarInfo.AudienceConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
						comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
						null;

					SlideContainer.EditedContent.AudienceState.TabC.Clipart1 = pictureEditTabCClipart1.Image != SlideContainer.StarInfo.Tab9SubCClipart1Image ?
						pictureEditTabCClipart1.Image :
						null;
					SlideContainer.EditedContent.AudienceState.TabC.Clipart2 = pictureEditTabCClipart2.Image != SlideContainer.StarInfo.Tab9SubCClipart2Image ?
						pictureEditTabCClipart2.Image :
						null;
					SlideContainer.EditedContent.AudienceState.TabC.Clipart3 = pictureEditTabCClipart3.Image != SlideContainer.StarInfo.Tab9SubCClipart3Image ?
						pictureEditTabCClipart3.Image :
						null;
					SlideContainer.EditedContent.AudienceState.TabC.Clipart4 = pictureEditTabCClipart4.Image != SlideContainer.StarInfo.Tab9SubCClipart4Image ?
						pictureEditTabCClipart4.Image :
						null;

					SlideContainer.EditedContent.AudienceState.TabC.Combo1 = SlideContainer.StarInfo.AudienceConfiguration.PartCCombo1Items.FirstOrDefault(h => h.IsDefault) != comboBoxEditTabCCombo1.EditValue ?
						comboBoxEditTabCCombo1.EditValue as ListDataItem ?? (comboBoxEditTabCCombo1.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditTabCCombo1.EditValue } : null) :
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
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab9SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab9SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceConfiguration.HeadersPartAItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.AudienceState.TabA.SlideHeader ??
						SlideContainer.StarInfo.AudienceConfiguration.HeadersPartAItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab9SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab9SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceConfiguration.HeadersPartBItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.AudienceState.TabB.SlideHeader ??
						SlideContainer.StarInfo.AudienceConfiguration.HeadersPartBItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab9SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab9SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceConfiguration.HeadersPartCItems);
					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.AudienceState.TabC.SlideHeader ??
						SlideContainer.StarInfo.AudienceConfiguration.HeadersPartCItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
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