using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Configuration.Cover;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.PresentationClasses.Output;
using DevExpress.Skins;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Cover
{
	[ToolboxItem(false)]
	public sealed partial class CoverTabAControl : ChildTabBaseControl
	{
		private readonly List<User> _usersByStation = new List<User>();
		private readonly DateTime _defaultDate = DateTime.Today;
		private CoverTabAInfo CustomTabInfo => (CoverTabAInfo)TabInfo;

		public CoverTabAControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			memoEditSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
			comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			memoEditSubheader1.Properties.NullText = CustomTabInfo.SubHeader1Placeholder ?? memoEditSubheader1.Properties.NullText;
			_usersByStation.AddRange(SlideContainer.StarInfo.UsersList.GetUsersByStation(MasterWizardManager.Instance.SelectedWizard.Name));
			comboBoxEditCombo1.Properties.Items.AddRange(_usersByStation);

			clipartEditContainer1.Init(ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image), CustomTabInfo.Clipart1Configuration, TabPageContainer.ParentControl);
			clipartEditContainer1.EditValueChanged += OnEditValueChanged;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemCalendar1Toggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCalendar1Toggle.MaxSize, scaleFactor);
			layoutControlItemCalendar1Toggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemCalendar1Toggle.MinSize, scaleFactor);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			clipartEditContainer1.LoadData(SlideContainer.EditedContent.CoverState.TabA.Clipart1);

			memoEditSubheader1.EditValue = SlideContainer.EditedContent.CoverState.TabA.Subheader1 ??
										   CustomTabInfo.SubHeader1DefaultValue;

			dateEditCalendar1.EditValue = SlideContainer.EditedContent.CoverState.TabA.Calendar1 != DateTime.MinValue
				? SlideContainer.EditedContent.CoverState.TabA.Calendar1 ?? _defaultDate
				: _defaultDate;
			checkEditCalendar1.Checked = SlideContainer.EditedContent.CoverState.TabA.Calendar1 != DateTime.MinValue;

			comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.CoverState.TabA.Combo1 ??
										   _usersByStation.FirstOrDefault();

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			SlideContainer.EditedContent.CoverState.TabA.Clipart1 = clipartEditContainer1.GetActiveClipartObject();

			SlideContainer.EditedContent.CoverState.TabA.Subheader1 =
				memoEditSubheader1.EditValue as String != CustomTabInfo.SubHeader1DefaultValue
					? memoEditSubheader1.EditValue as String ?? String.Empty
					: null;

			SlideContainer.EditedContent.CoverState.TabA.Calendar1 = checkEditCalendar1.Checked
				? ((DateTime?)dateEditCalendar1.EditValue == _defaultDate ? null : (DateTime?)dateEditCalendar1.EditValue)
				: DateTime.MinValue;

			SlideContainer.EditedContent.CoverState.TabA.Combo1 =
				_usersByStation.FirstOrDefault() != comboBoxEditCombo1.EditValue as User
					? comboBoxEditCombo1.EditValue as User ?? (comboBoxEditCombo1.EditValue is String
						  ? new User { FirstName = (String)comboBoxEditCombo1.EditValue }
						  : null)
					: null;

			_dataChanged = false;
		}

		public override ListDataItem GetSlideHeaderValue()
		{
			return SlideContainer.EditedContent.CoverState.TabA.SlideHeader ??
				   CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);
		}

		public override Boolean GetOutputEnableState()
		{
			return SlideContainer.EditedContent.CoverState.TabA.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplySlideHeaderValue(ListDataItem slideHeaderValue)
		{
			SlideContainer.EditedContent.CoverState.TabA.SlideHeader =
				slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ? slideHeaderValue : null;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.CoverState.TabA.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		private void OnCalendar1ToggleCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemCalendar1Value.Enabled = checkEditCalendar1.Checked;
			RaiseEditValueChanged();
		}

		private void OnCombo1EditValueChanged(object sender, EventArgs e)
		{
			var user = comboBoxEditCombo1.EditValue as User;
			simpleLabelItemCombo1Description1.Text = String.Format("<color=dimgray>{0}</color>", String.Join("        ", user?.Email, user?.Phone));
			RaiseEditValueChanged();
		}

		#region Output
		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = TabPageContainer.ParentControl.SelectedTheme;
			outputDataPackage.AddAsFirtsPage = SlideContainer.EditedContent.CoverState.TabA.AddAsPageOne;

			var clipart = SlideContainer.EditedContent.CoverState.TabA.Clipart1 ??
						  ImageClipartObject.FromImage(CustomTabInfo.Clipart1Image);
			if (clipart != null)
				outputDataPackage.ClipartItems.Add("CP01ACLIPART1", clipart);

			var slideHeader = (SlideContainer.EditedContent.CoverState.TabA.SlideHeader ??
							   CustomTabInfo.HeadersItems.FirstOrDefault(h =>
								   h.IsDefault))?.Value;
			var subHeader1 = SlideContainer.EditedContent.CoverState.TabA.Subheader1 ??
							 CustomTabInfo.SubHeader1DefaultValue;
			var calendar1 = SlideContainer.EditedContent.CoverState.TabA.Calendar1 != DateTime.MinValue
				? SlideContainer.EditedContent.CoverState.TabA.Calendar1 ?? _defaultDate
				: (DateTime?)null;
			var combo1 = SlideContainer.EditedContent.CoverState.TabA.Combo1 ??
						 _usersByStation.FirstOrDefault();

			if (!String.IsNullOrWhiteSpace(slideHeader) &&
				!String.IsNullOrWhiteSpace(subHeader1) &&
				calendar1.HasValue &&
				combo1 != null)
			{
				outputDataPackage.TemplateName =
					MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-1.pptx" : "CP01A-8.pptx");

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
				outputDataPackage.TemplateName =
					MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-2.pptx" : "CP01A-9.pptx");

				outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP01ACalendar1".ToUpper(), calendar1.Value.ToString("MMMM d, yyyy"));
				outputDataPackage.TextItems.Add("CP01ACombo1".ToUpper(), combo1.ToString());
				outputDataPackage.TextItems.Add("CP01ACombo1b".ToUpper(), String.Join("     ", combo1.Email, combo1.Phone));
			}
			else if (!String.IsNullOrWhiteSpace(slideHeader) &&
					 !String.IsNullOrWhiteSpace(subHeader1) &&
					 calendar1.HasValue)
			{
				outputDataPackage.TemplateName =
					MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-3.pptx" : "CP01A-10.pptx");

				outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP01ASubheader1".ToUpper(), subHeader1);
				outputDataPackage.TextItems.Add("CP01ACalendar1".ToUpper(), calendar1.Value.ToString("MMMM d, yyyy"));
			}
			else if (!String.IsNullOrWhiteSpace(slideHeader) &&
					 !String.IsNullOrWhiteSpace(subHeader1) &&
					 combo1 != null)
			{
				outputDataPackage.TemplateName =
					MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-7.pptx" : "CP018A-14.pptx");

				outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP01ASubheader1".ToUpper(), subHeader1);
				outputDataPackage.TextItems.Add("CP01ACombo1".ToUpper(), combo1.ToString());
				outputDataPackage.TextItems.Add("CP01ACombo1b".ToUpper(), String.Join("     ", combo1.Email, combo1.Phone));
			}
			else if (!String.IsNullOrWhiteSpace(slideHeader) &&
					 !String.IsNullOrWhiteSpace(subHeader1))
			{
				outputDataPackage.TemplateName =
					MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-4.pptx" : "CP01A-11.pptx");

				outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP01ASubheader1".ToUpper(), subHeader1);
			}
			else if (!String.IsNullOrWhiteSpace(slideHeader) &&
					 calendar1.HasValue)
			{
				outputDataPackage.TemplateName =
					MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-5.pptx" : "CP01A-12.pptx");

				outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP01ACalendar1".ToUpper(), calendar1.Value.ToString("MMMM d, yyyy"));
			}
			else if (!String.IsNullOrWhiteSpace(slideHeader))
			{
				outputDataPackage.TemplateName =
					MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-6.pptx" : "CP01A-13.pptx");

				outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
			}

			return outputDataPackage;
		}
		#endregion
	}
}