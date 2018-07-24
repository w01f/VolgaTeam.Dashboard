using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Common.InteropClasses;
using Asa.Solutions.Common.PresentationClasses.Output;
using DevExpress.Skins;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class CoverControl : StarAppControl
	{
		private readonly List<User> _usersByStation = new List<User>();
		private readonly DateTime _defaultDate = DateTime.Today;

		public override SlideType SlideType => SlideType.StarAppCover;

		public CoverControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();

			Resize += OnResize;

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab1SubATitle))
			{
				layoutControlGroupTabA.Text = SlideContainer.StarInfo.Titles.Tab1SubATitle;

				memoEditSubheader1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();
				comboBoxEditCombo1.EnableSelectAll().RaiseNullValueIfEditorEmpty().RaiseChangePlaceholderColor();

				memoEditSubheader1.Properties.NullText = SlideContainer.StarInfo.CoverConfiguration.PartASubHeader1Placeholder ?? memoEditSubheader1.Properties.NullText;
				_usersByStation.AddRange(SlideContainer.StarInfo.UsersList.GetUsersByStation(MasterWizardManager.Instance.SelectedWizard.Name));
				comboBoxEditCombo1.Properties.Items.AddRange(_usersByStation);

				clipartEditContainer1.Init(ImageClipartObject.FromImage(SlideContainer.StarInfo.Tab1SubAClipart1Image), SlideContainer.StarInfo.CoverConfiguration.PartAClipart1Configuration, this);
				clipartEditContainer1.EditValueChanged += OnEditValueChanged;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabA.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab1SubUTitle))
			{
				layoutControlGroupTabU.Text = SlideContainer.StarInfo.Titles.Tab1SubUTitle;

				slidesEditContainerTabU.Init(SlideContainer.StarInfo.CoverConfiguration.PartUSlides);
				slidesEditContainerTabU.SlideOutput += SlideContainer.OnCustomSlideOutput;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabU.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab1SubVTitle))
			{
				layoutControlGroupTabV.Text = SlideContainer.StarInfo.Titles.Tab1SubVTitle;

				slidesEditContainerTabV.Init(SlideContainer.StarInfo.CoverConfiguration.PartVSlides);
				slidesEditContainerTabV.SlideOutput += SlideContainer.OnCustomSlideOutput;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabV.Visibility = LayoutVisibility.Never;

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab1SubWTitle))
			{
				layoutControlGroupTabW.Text = SlideContainer.StarInfo.Titles.Tab1SubWTitle;

				slidesEditContainerTabW.Init(SlideContainer.StarInfo.CoverConfiguration.PartWSlides);
				slidesEditContainerTabW.SlideOutput += SlideContainer.OnCustomSlideOutput;

				Application.DoEvents();
			}
			else
				layoutControlGroupTabW.Visibility = LayoutVisibility.Never;

			_outputProcessors.AddRange(OutputProcessor.GetOutputProcessors(this));

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

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab1SubATitle))
			{
				clipartEditContainer1.LoadData(SlideContainer.EditedContent.CoverState.TabA.Clipart1);

				memoEditSubheader1.EditValue = SlideContainer.EditedContent.CoverState.TabA.Subheader1 ??
											   SlideContainer.StarInfo.CoverConfiguration.PartASubHeader1DefaultValue;

				checkEditAddAsPageOne.Checked = SlideContainer.EditedContent.CoverState.TabA.AddAsPageOne;

				dateEditCalendar1.EditValue = SlideContainer.EditedContent.CoverState.TabA.Calendar1 != DateTime.MinValue
					? SlideContainer.EditedContent.CoverState.TabA.Calendar1 ?? _defaultDate
					: _defaultDate;
				checkEditCalendar1.Checked = SlideContainer.EditedContent.CoverState.TabA.Calendar1 != DateTime.MinValue;

				comboBoxEditCombo1.EditValue = SlideContainer.EditedContent.CoverState.TabA.Combo1 ??
											   _usersByStation.FirstOrDefault();
			}

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab1SubUTitle))
			{
			}

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab1SubVTitle))
			{
			}

			if (!String.IsNullOrEmpty(SlideContainer.StarInfo.Titles.Tab1SubWTitle))
			{
			}

			_allowToSave = true;

			LoadPartData();
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabA)
			{
				SlideContainer.EditedContent.CoverState.TabA.SlideHeader =
					SlideContainer.StarInfo.CoverConfiguration.HeaderPartAItems.FirstOrDefault(h => h.IsDefault) !=
					comboBoxEditSlideHeader.EditValue
						? comboBoxEditSlideHeader.EditValue as ListDataItem ??
						  new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String }
						: null;

				SlideContainer.EditedContent.CoverState.TabA.AddAsPageOne = checkEditAddAsPageOne.Checked;

				SlideContainer.EditedContent.CoverState.TabA.Clipart1 = clipartEditContainer1.GetActiveClipartObject();

				SlideContainer.EditedContent.CoverState.TabA.Subheader1 =
					memoEditSubheader1.EditValue as String != SlideContainer.StarInfo.CoverConfiguration.PartASubHeader1DefaultValue
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
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabU)
			{
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabV)
			{
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabW)
			{
			}

			_dataChanged = false;
		}

		private void LoadPartData()
		{
			_allowToSave = false;
			if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabA)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab1SubARightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab1SubAFooterLogo;

				comboBoxEditSlideHeader.Properties.Items.Clear();
				comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.StarInfo.CoverConfiguration.HeaderPartAItems.Where(item => !item.IsPlaceholder).ToArray());

				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.CoverState.TabA.SlideHeader ??
					SlideContainer.StarInfo.CoverConfiguration.HeaderPartAItems.FirstOrDefault(h => h.IsDefault);
				comboBoxEditSlideHeader.Properties.NullText = SlideContainer.StarInfo.CoverConfiguration.HeaderPartAItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
					"Select or type";
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabU)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab1SubURightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab1SubUFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabV)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab1SubVRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab1SubVFooterLogo;
			}
			else if (tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabW)
			{
				pictureEditLogoRight.Image = SlideContainer.StarInfo.Tab1SubWRightLogo;
				pictureEditLogoFooter.Image = SlideContainer.StarInfo.Tab1SubWFooterLogo;
			}

			layoutControlItemAddAsPageOne.Visibility = tabbedControlGroupData.SelectedTabPage == layoutControlGroupTabA
				? LayoutVisibility.Always
				: LayoutVisibility.Never;

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
	}
}