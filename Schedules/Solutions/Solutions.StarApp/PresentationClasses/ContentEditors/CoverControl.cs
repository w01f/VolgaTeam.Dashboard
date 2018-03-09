using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.StarApp.Configuration;
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
		private readonly List<User> _usersByStation = new List<User>();
		private readonly DateTime _defaultDate = DateTime.Today;

		public override SlideType SlideType => SlideType.StarAppCover;
		public override string SlideName => SlideContainer.StarInfo.Titles.Tab1Title;

		public CoverControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll();
			memoEditSubheader1.EnableSelectAll();
			comboBoxEditCombo1.EnableSelectAll();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab1SubATitle;

			pictureEditClipart1.Image = SlideContainer.StarInfo.Tab1SubAClipart1Image;
			pictureEditClipart1.Properties.PictureAlignment =
				SlideContainer.StarInfo.CoverConfiguration.PartAClipart1Configuration.Alignment;

			ImageEditorHelper.AssignImageEditors(new[]{
				pictureEditClipart1
			});

			_usersByStation.AddRange(SlideContainer.StarInfo.UsersList.GetUsersByStation(MasterWizardManager.Instance.SelectedWizard.Name));
			comboBoxEditCombo1.Properties.Items.AddRange(_usersByStation);

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

			pictureEditClipart1.Image = SlideContainer.EditedContent.CoverState.Clipart1 ??
				pictureEditClipart1.Image;

			memoEditSubheader1.EditValue = SlideContainer.EditedContent.CoverState.Subheader1 ??
				SlideContainer.StarInfo.CoverConfiguration.SubHeader1DefaultValue;

			checkEditAddAsPageOne.Checked = SlideContainer.EditedContent.CoverState.AddAsPageOne;

			dateEditCalendar1.EditValue = SlideContainer.EditedContent.CoverState.Calendar1 ?? _defaultDate;
			checkEditCalendar1.Checked = dateEditCalendar1.EditValue != null;

			comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.CoverState.Combo1 ??
				_usersByStation.FirstOrDefault();

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.CoverState.SlideHeader = SlideContainer.StarInfo.CoverConfiguration.HeaderPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
				comboBoxEditSlideHeader.EditValue as ListDataItem ?? (comboBoxEditSlideHeader.EditValue is String ? new ListDataItem { Value = (String)comboBoxEditSlideHeader.EditValue } : null) :
				null;

			SlideContainer.EditedContent.CoverState.AddAsPageOne = checkEditAddAsPageOne.Checked;

			SlideContainer.EditedContent.CoverState.Clipart1 = pictureEditClipart1.Image != SlideContainer.StarInfo.Tab1SubAClipart1Image ?
				pictureEditClipart1.Image :
				null;

			SlideContainer.EditedContent.CoverState.Subheader1 = memoEditSubheader1.EditValue as String != SlideContainer.StarInfo.CoverConfiguration.SubHeader1DefaultValue ?
				memoEditSubheader1.EditValue as String :
				null;

			SlideContainer.EditedContent.CoverState.Calendar1 = dateEditCalendar1.EditValue != null && (DateTime?)dateEditCalendar1.EditValue != _defaultDate ?
				(DateTime?)dateEditCalendar1.EditValue :
				null;

			SlideContainer.EditedContent.CoverState.Combo1 = _usersByStation.FirstOrDefault() != comboBoxEditCombo1.EditValue as User ?
				comboBoxEditCombo1.EditValue as User ?? (comboBoxEditCombo1.EditValue is String ? new User { FirstName = (String)comboBoxEditCombo1.EditValue } : null) :
				null;

			SlideContainer.SettingsContainer.SaveSettings();

			_dataChanged = false;
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

					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CoverState.SlideHeader ??
						SlideContainer.StarInfo.CoverConfiguration.HeaderPartAItems.OrderByDescending(h => h.IsDefault).FirstOrDefault();
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

		private void OnCombo1EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditCombo1.EditValue as User;
			simpleLabelItemCombo1Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			OnEditValueChanged(sender, e);
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