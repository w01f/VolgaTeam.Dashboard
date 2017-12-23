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
	public sealed partial class MarketControl : StarAppControl, IStarAppSlide
	{
		private bool _allowToSave;

		public override SlideType SlideType => SlideType.StarAppMarket;
		public override string SlideName => SlideContainer.StarInfo.Titles.Tab7Title;

		public MarketControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			Text = SlideName;
			
			comboBoxEditSlideHeader.EnableSelectAll();
			memoEditTabASubheader1.EnableSelectAll();
			textEditTabBSubheader1.EnableSelectAll();
			memoEditTabBSubheader2.EnableSelectAll();
			comboBoxEditTabCCombo1.EnableSelectAll();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab7SubATitle;
			layoutControlGroupTabB.Text = SlideContainer.StarInfo.Titles.Tab7SubBTitle;
			layoutControlGroupTabC.Text = SlideContainer.StarInfo.Titles.Tab7SubCTitle;

			pictureEditTabAClipart1.Image = SlideContainer.StarInfo.Tab7SubAClipart1Image;
			pictureEditTabAClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartAClipart1Configuration.Alignment;
			pictureEditTabBClipart1.Image = SlideContainer.StarInfo.Tab7SubBClipart1Image;
			pictureEditTabBClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartBClipart1Configuration.Alignment;
			pictureEditTabBClipart2.Image = SlideContainer.StarInfo.Tab7SubBClipart2Image;
			pictureEditTabBClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartBClipart2Configuration.Alignment;
			pictureEditTabBClipart3.Image = SlideContainer.StarInfo.Tab7SubBClipart3Image;
			pictureEditTabBClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartBClipart3Configuration.Alignment;
			pictureEditTabBClipart4.Image = SlideContainer.StarInfo.Tab7SubBClipart4Image;
			pictureEditTabBClipart4.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartBClipart4Configuration.Alignment;
			pictureEditTabBClipart5.Image = SlideContainer.StarInfo.Tab7SubBClipart5Image;
			pictureEditTabBClipart5.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartBClipart5Configuration.Alignment;
			pictureEditTabCClipart1.Image = SlideContainer.StarInfo.Tab7SubCClipart1Image;
			pictureEditTabCClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartCClipart1Configuration.Alignment;
			pictureEditTabCClipart2.Image = SlideContainer.StarInfo.Tab7SubCClipart2Image;
			pictureEditTabCClipart2.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartCClipart2Configuration.Alignment;
			pictureEditTabCClipart3.Image = SlideContainer.StarInfo.Tab7SubCClipart3Image;
			pictureEditTabCClipart3.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartCClipart3Configuration.Alignment;
			pictureEditTabCClipart4.Image = SlideContainer.StarInfo.Tab7SubCClipart4Image;
			pictureEditTabCClipart4.Properties.PictureAlignment =
				SlideContainer.StarInfo.MarketConfiguration.PartCClipart4Configuration.Alignment;

			comboBoxEditTabCCombo1.Properties.Items.AddRange(SlideContainer.StarInfo.MarketConfiguration.PartCCombo1Items);

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			comboBoxEditTabCCombo1.EditValue =
				SlideContainer.StarInfo.MarketConfiguration.PartCCombo1Items.FirstOrDefault(item => item.IsDefault);

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.MarketState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;

			SlideContainer.SettingsContainer.SaveSettings();
		}

		private void LoadPartData()
		{
			_allowToSave = false;
			switch (tabbedControlGroupData.SelectedTabPageIndex)
			{
				case 0:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab7SubARightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab7SubAFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.MarketConfiguration.HeadersPartAItems);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.MarketConfiguration.HeadersPartAItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.MarketState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.MarketConfiguration.HeadersPartAItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 1:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab7SubBRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab7SubBFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.MarketConfiguration.HeadersPartBItems);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.MarketConfiguration.HeadersPartBItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.MarketState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.MarketConfiguration.HeadersPartBItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
					break;
				case 2:
					pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab7SubCRightLogo;
					pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab7SubCFooterLogo;

					comboBoxEditSlideHeader.Properties.Items.Clear();
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.MarketConfiguration.HeadersPartCItems);
					comboBoxEditSlideHeader.EditValue =
							SlideContainer.StarInfo.MarketConfiguration.HeadersPartCItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.MarketState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
							SlideContainer.StarInfo.MarketConfiguration.HeadersPartCItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
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