using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using DevExpress.XtraTab;
using Asa.Solutions.StarApp.PresentationClasses.Output;

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
			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
			}
			comboBoxEditSlideHeader.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.ShareLists.Headers);

			xtraTabPageA.Text = SlideContainer.StarInfo.Titles.Tab5SubATitle;
			xtraTabPageB.Text = SlideContainer.StarInfo.Titles.Tab5SubBTitle;
			xtraTabPageC.Text = SlideContainer.StarInfo.Titles.Tab5SubCTitle;
			xtraTabPageD.Text = SlideContainer.StarInfo.Titles.Tab5SubDTitle;
			xtraTabPageE.Text = SlideContainer.StarInfo.Titles.Tab5SubETitle;
			OnSelectedPageChanged(null, new TabPageChangedEventArgs(null, xtraTabPageA));
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

		private void EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SlideContainer.RaiseDataChanged();
		}

		private void OnSelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			switch (e.Page.TabIndex)
			{
				case 0:
					pbLogoRight.Image = SlideContainer.StarInfo.Tab5SubARightLogo;
					pbLogoFooter.Image = SlideContainer.StarInfo.Tab5SubAFooterLogo;
					break;
				case 1:
					pbLogoRight.Image = SlideContainer.StarInfo.Tab5SubBRightLogo;
					pbLogoFooter.Image = SlideContainer.StarInfo.Tab5SubBFooterLogo;
					break;
				case 2:
					pbLogoRight.Image = SlideContainer.StarInfo.Tab5SubCRightLogo;
					pbLogoFooter.Image = SlideContainer.StarInfo.Tab5SubCFooterLogo;
					break;
				case 3:
					pbLogoRight.Image = SlideContainer.StarInfo.Tab5SubDRightLogo;
					pbLogoFooter.Image = SlideContainer.StarInfo.Tab5SubDFooterLogo;
					break;
				case 4:
					pbLogoRight.Image = SlideContainer.StarInfo.Tab5SubERightLogo;
					pbLogoFooter.Image = SlideContainer.StarInfo.Tab5SubEFooterLogo;
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