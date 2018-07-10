using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.InteropClasses;
using Asa.Solutions.StarApp.PresentationClasses.Output;
using DevExpress.Skins;
using DevExpress.XtraLayout;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class CoverControl : StarAppControl
	{
		private readonly List<User> _usersByStation = new List<User>();
		private readonly DateTime _defaultDate = DateTime.Today;

		public override SlideType SlideType => SlideType.StarAppCover;
		public override string OutputName => SlideContainer.StarInfo.Titles.Tab1Title;

		public CoverControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			memoEditSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab1SubATitle;

			clipartEditContainer1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab1SubAClipart1Image), SlideContainer.StarInfo.CoverConfiguration.PartAClipart1Configuration, this);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;

			memoEditSubheader1.Properties.NullText = SlideContainer.StarInfo.CoverConfiguration.SubHeader1Placeholder ?? memoEditSubheader1.Properties.NullText;

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

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.CoverState.Clipart1);

			memoEditSubheader1.EditValue = SlideContainer.EditedContent.CoverState.Subheader1 ??
				SlideContainer.StarInfo.CoverConfiguration.SubHeader1DefaultValue;

			checkEditAddAsPageOne.Checked = SlideContainer.EditedContent.CoverState.AddAsPageOne;

			dateEditCalendar1.EditValue = SlideContainer.EditedContent.CoverState.Calendar1 != DateTime.MinValue ? SlideContainer.EditedContent.CoverState.Calendar1 ?? _defaultDate : _defaultDate;
			checkEditCalendar1.Checked = SlideContainer.EditedContent.CoverState.Calendar1 != DateTime.MinValue;

			comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.CoverState.Combo1 ??
				_usersByStation.FirstOrDefault();

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.CoverState.SlideHeader = SlideContainer.StarInfo.CoverConfiguration.HeaderPartAItems.FirstOrDefault(h => h.IsDefault) != comboBoxEditSlideHeader.EditValue ?
				comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String } :
				null;

			SlideContainer.EditedContent.CoverState.AddAsPageOne = checkEditAddAsPageOne.Checked;

			SlideContainer.EditedContent.CoverState.Clipart1 = clipartEditContainer1.GetActiveClipartObject();

			SlideContainer.EditedContent.CoverState.Subheader1 = memoEditSubheader1.EditValue as String != SlideContainer.StarInfo.CoverConfiguration.SubHeader1DefaultValue ?
				memoEditSubheader1.EditValue as String ?? String.Empty :
				null;

			SlideContainer.EditedContent.CoverState.Calendar1 = checkEditCalendar1.Checked ?
				((DateTime?)dateEditCalendar1.EditValue == _defaultDate ? null : (DateTime?)dateEditCalendar1.EditValue) :
				DateTime.MinValue;

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
					comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CoverConfiguration.HeaderPartAItems.Where(item => !item.IsPlaceholder).ToArray());

					comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CoverState.SlideHeader ??
						SlideContainer.StarInfo.CoverConfiguration.HeaderPartAItems.FirstOrDefault(h => h.IsDefault);
					comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.CoverConfiguration.HeaderPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
						"Select or type";
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
			OnEditValueChanged(sender, e);
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
		public override bool ReadyForOutput => true;

		public override OutputGroup GetOutputGroup()
		{
			return new OutputGroup(this)
			{
				DisplayName = OutputName,
				IsCurrent = SlideContainer.ActiveSlideContent == this,
				Configurations = new[] { new OutputConfiguration(StarAppOutputType.Cover, OutputName, 1, SlideContainer.ActiveSlideContent == this) }
			};
		}

		private OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;
			outputDataPackage.AddAsFirtsPage = SlideContainer.EditedContent.CoverState.AddAsPageOne;

			var clipart = SlideContainer.EditedContent.CoverState.Clipart1 ?? ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab1SubAClipart1Image);
			if (clipart != null)
				outputDataPackage.ClipartItems.Add("CP01ACLIPART1", clipart);

			var slideHeader = (SlideContainer.EditedContent.CoverState.SlideHeader ?? SlideContainer.StarInfo.CoverConfiguration.HeaderPartAItems.FirstOrDefault(h => h.IsDefault))?.Value;
			var subHeader1 = SlideContainer.EditedContent.CoverState.Subheader1 ?? SlideContainer.StarInfo.CoverConfiguration.SubHeader1DefaultValue;
			var calendar1 = SlideContainer.EditedContent.CoverState.Calendar1 != DateTime.MinValue ? SlideContainer.EditedContent.CoverState.Calendar1 ?? _defaultDate : (DateTime?)null;
			var combo1 = SlideContainer.EditedContent.CoverState.Combo1 ?? _usersByStation.FirstOrDefault();

			if (!String.IsNullOrWhiteSpace(slideHeader) &&
				!String.IsNullOrWhiteSpace(subHeader1) &&
				calendar1.HasValue &&
				combo1 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-1.pptx" : "CP01A-8.pptx");

				outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP01ASubheader1".ToUpper(), subHeader1);
				outputDataPackage.TextItems.Add("CP01ACalendar1".ToUpper(), calendar1.Value.ToString("MMMM d, yyyy"));
				outputDataPackage.TextItems.Add("CP01ACombo1".ToUpper(), combo1.ToString());
				outputDataPackage.TextItems.Add("CP01ACombo1b".ToUpper(), String.Join("     ", combo1.Email, combo1.Phone));
			}
			else if (!String.IsNullOrWhiteSpace(slideHeader) &&
				   calendar1.HasValue &&
					 combo1 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-2.pptx" : "CP01A-9.pptx");

				outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP01ACalendar1".ToUpper(), calendar1.Value.ToString("MMMM d, yyyy"));
				outputDataPackage.TextItems.Add("CP01ACombo1".ToUpper(), combo1.ToString());
				outputDataPackage.TextItems.Add("CP01ACombo1b".ToUpper(), String.Join("     ", combo1.Email, combo1.Phone));
			}
			else if (!String.IsNullOrWhiteSpace(slideHeader) &&
					 !String.IsNullOrWhiteSpace(subHeader1) &&
					 calendar1.HasValue)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-3.pptx" : "CP01A-10.pptx");

				outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP01ASubheader1".ToUpper(), subHeader1);
				outputDataPackage.TextItems.Add("CP01ACalendar1".ToUpper(), calendar1.Value.ToString("MMMM d, yyyy"));
			}
			else if (!String.IsNullOrWhiteSpace(slideHeader) &&
					 !String.IsNullOrWhiteSpace(subHeader1) &&
					 combo1 != null)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-7.pptx" : "CP018A-14.pptx");

				outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP01ASubheader1".ToUpper(), subHeader1);
				outputDataPackage.TextItems.Add("CP01ACombo1".ToUpper(), combo1.ToString());
				outputDataPackage.TextItems.Add("CP01ACombo1b".ToUpper(), String.Join("     ", combo1.Email, combo1.Phone));
			}
			else if (!String.IsNullOrWhiteSpace(slideHeader) &&
					 !String.IsNullOrWhiteSpace(subHeader1))
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-4.pptx" : "CP01A-11.pptx");

				outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP01ASubheader1".ToUpper(), subHeader1);
			}
			else if (!String.IsNullOrWhiteSpace(slideHeader) &&
					 calendar1.HasValue)
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-5.pptx" : "CP01A-12.pptx");

				outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP01ACalendar1".ToUpper(), calendar1.Value.ToString("MMMM d, yyyy"));
			}
			else if (!String.IsNullOrWhiteSpace(slideHeader))
			{
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-6.pptx" : "CP01A-13.pptx");

				outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
			}

			return outputDataPackage;
		}

		public override void GenerateOutput(IList<OutputConfiguration> configurations)
		{
			var outputDataPackage = GetOutputData();
			SlideContainer.PowerPointProcessor.AppendStarCommonSlide(outputDataPackage);
		}

		public override IList<PreviewGroup> GeneratePreview(IList<OutputConfiguration> configurations)
		{
			var outputDataPackage = GetOutputData();
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			SlideContainer.PowerPointProcessor.PrepareStarCommonSlide(outputDataPackage, tempFileName);
			return new[] { new PreviewGroup { Name = OutputName, PresentationSourcePath = tempFileName, InsertOnTop = outputDataPackage.AddAsFirtsPage } };
		}
		#endregion
	}
}