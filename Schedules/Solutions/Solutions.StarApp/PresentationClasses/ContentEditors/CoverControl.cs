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

			Resize += OnResize;

			Text = SlideName;

			comboBoxEditSlideHeader.EnableSelectAll();
			memoEditSubheader1.EnableSelectAll();
			comboBoxEditCombo1.EnableSelectAll();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab1SubATitle;

			pictureEditClipart1.Image = SlideContainer.StarInfo.Tab1SubAClipart1Image;
			pictureEditClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.CoverConfiguration.PartAClipart1Configuration.Alignment;

			comboBoxEditCombo1.Properties.Items.AddRange(SlideContainer.StarInfo.UsersList.GetUsersByStation(MasterWizardManager.Instance.SelectedWizard.Name));

			dateEditCalendar1.DateTime = DateTime.Today;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);
			layoutControlItemCalendar1Toggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCalendar1Toggle.MaxSize, scaleFactor);
			layoutControlItemCalendar1Toggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemCalendar1Toggle.MinSize, scaleFactor);

			OnResize(this, EventArgs.Empty);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			memoEditSubheader1.EditValue = SlideContainer.StarInfo.CoverConfiguration.SubHeader1DefaultValue;

			checkEditAddAsPageOne.Checked = SlideContainer.EditedContent.CoverState.AddAsPageOne;
			comboBoxEditCombo1.EditValue =
				SlideContainer.StarInfo.UsersList.GetUsersByStation(MasterWizardManager.Instance.SelectedWizard.Name).FirstOrDefault();

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
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CoverConfiguration.HeaderPartAItems);

					comboBoxEditSlideHeader.EditValue =
					SlideContainer.StarInfo.CoverConfiguration.HeaderPartAItems.FirstOrDefault(h => String.Equals(h.Value, SlideContainer.EditedContent.CoverState.SlideHeader, StringComparison.OrdinalIgnoreCase)) ??
					SlideContainer.StarInfo.CoverConfiguration.HeaderPartAItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
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

		private void OnCalendar1ToggleCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemCalendar1Value.Enabled = checkEditCalendar1.Checked;
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