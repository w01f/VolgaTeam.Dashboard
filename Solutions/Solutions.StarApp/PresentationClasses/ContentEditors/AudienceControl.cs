﻿using System;
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
	public sealed partial class AudienceControl : StarAppControl, IStarAppSlide
	{
		private bool _allowToSave;

		public override SlideType SlideType => SlideType.StarAppAudience;
		public override string SlideName => SlideContainer.StarInfo.Titles.Tab9Title;

		public AudienceControl(BaseStarAppContainer slideContainer) : base(slideContainer)
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
			comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.AudienceLists.Headers);

			xtraTabPageA.Text = SlideContainer.StarInfo.Titles.Tab9SubATitle;
			xtraTabPageB.Text = SlideContainer.StarInfo.Titles.Tab9SubBTitle;
			xtraTabPageC.Text = SlideContainer.StarInfo.Titles.Tab9SubCTitle;
			OnSelectedPageChanged(null, new TabPageChangedEventArgs(null, xtraTabPageA));
		}

		public override void LoadData()
		{
			_allowToSave = false;
			comboBoxEditSlideHeader.EditValue =
					SlideContainer.StarInfo.AudienceLists.Headers.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.AudienceState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
					SlideContainer.StarInfo.AudienceLists.Headers.OrderByDescending(h => h.IsDefault).FirstOrDefault();

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.AudienceState.SlideHeader = comboBoxEditSlideHeader.EditValue as String;

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
					pbLogoRight.Image = SlideContainer.StarInfo.Tab9SubARightLogo;
					pbLogoFooter.Image = SlideContainer.StarInfo.Tab9SubAFooterLogo;
					break;
				case 1:
					pbLogoRight.Image = SlideContainer.StarInfo.Tab9SubBRightLogo;
					pbLogoFooter.Image = SlideContainer.StarInfo.Tab9SubBFooterLogo;
					break;
				case 2:
					pbLogoRight.Image = SlideContainer.StarInfo.Tab9SubCRightLogo;
					pbLogoFooter.Image = SlideContainer.StarInfo.Tab9SubCFooterLogo;
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