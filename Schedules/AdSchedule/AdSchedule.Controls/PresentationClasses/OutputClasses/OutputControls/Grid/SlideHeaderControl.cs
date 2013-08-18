﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class SlideHeaderControl : UserControl
	{
		private readonly IGridOutputControl _settingsContainer;
		private bool _allowToSave;

		public SlideHeaderControl(IGridOutputControl settingsContainer)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_settingsContainer = settingsContainer;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				checkEditEnableSlideHeader.Font = new Font(checkEditEnableSlideHeader.Font.FontFamily, checkEditEnableSlideHeader.Font.Size - 3, checkEditEnableSlideHeader.Font.Style);
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
				checkEditAdvertiser.Font = font;
				checkEditDecisionMaker.Font = font;
				checkEditFlightDates.Font = font;
				checkEditPresentationDate.Font = font;
				checkEditLogo1.Font = font;
				checkEditLogo2.Font = font;
				checkEditLogo3.Font = font;
				checkEditLogo4.Font = font;
				checkEditPublicationName.Font = font;
				checkEditSlideTitle.Font = font;
			}
		}

		public void LoadSlideHeader()
		{
			_allowToSave = false;

			checkEditEnableSlideHeader.Enabled = _settingsContainer.SlideHeaderState.EnableSlideInfo;
			checkEditAdvertiser.Enabled = _settingsContainer.SlideHeaderState.EnableAdvertiser;
			checkEditDecisionMaker.Enabled = _settingsContainer.SlideHeaderState.EnableDecisionMaker;
			checkEditFlightDates.Enabled = _settingsContainer.SlideHeaderState.EnableFlightDates;
			checkEditPresentationDate.Enabled = _settingsContainer.SlideHeaderState.EnablePresentationDate;
			checkEditLogo1.Enabled = _settingsContainer.SlideHeaderState.EnableLogo1;
			checkEditLogo2.Enabled = _settingsContainer.SlideHeaderState.EnableLogo2;
			checkEditLogo3.Enabled = _settingsContainer.SlideHeaderState.EnableLogo3;
			checkEditLogo4.Enabled = _settingsContainer.SlideHeaderState.EnableLogo4;
			checkEditPublicationName.Enabled = _settingsContainer.SlideHeaderState.EnableName;
			checkEditSlideTitle.Enabled = _settingsContainer.SlideHeaderState.EnableSlideHeader;

			checkEditEnableSlideHeader.Checked = _settingsContainer.SlideHeaderState.ShowSlideInfo;
			checkEditAdvertiser.Checked = _settingsContainer.SlideHeaderState.ShowAdvertiser;
			checkEditDecisionMaker.Checked = _settingsContainer.SlideHeaderState.ShowDecisionMaker;
			checkEditFlightDates.Checked = _settingsContainer.SlideHeaderState.ShowFlightDates;
			checkEditPresentationDate.Checked = _settingsContainer.SlideHeaderState.ShowPresentationDate;
			checkEditLogo1.Checked = _settingsContainer.SlideHeaderState.ShowLogo1;
			checkEditLogo2.Checked = _settingsContainer.SlideHeaderState.ShowLogo2;
			checkEditLogo3.Checked = _settingsContainer.SlideHeaderState.ShowLogo3;
			checkEditLogo4.Checked = _settingsContainer.SlideHeaderState.ShowLogo4;
			checkEditPublicationName.Checked = _settingsContainer.SlideHeaderState.ShowName;
			checkEditSlideTitle.Checked = _settingsContainer.SlideHeaderState.ShowSlideHeader;
			_settingsContainer.SetSlideHeader();
			_allowToSave = true;
		}

		private void SaveSlideHeader()
		{
			if (_allowToSave)
			{
				_settingsContainer.SlideHeaderState.ShowSlideInfo = checkEditEnableSlideHeader.Checked;
				_settingsContainer.SlideHeaderState.ShowAdvertiser = checkEditAdvertiser.Checked & checkEditEnableSlideHeader.Checked;
				_settingsContainer.SlideHeaderState.ShowDecisionMaker = checkEditDecisionMaker.Checked & checkEditEnableSlideHeader.Checked;
				_settingsContainer.SlideHeaderState.ShowFlightDates = checkEditFlightDates.Checked & checkEditEnableSlideHeader.Checked;
				_settingsContainer.SlideHeaderState.ShowPresentationDate = checkEditPresentationDate.Checked & checkEditEnableSlideHeader.Checked;
				_settingsContainer.SlideHeaderState.ShowLogo1 = checkEditLogo1.Checked & checkEditEnableSlideHeader.Checked;
				_settingsContainer.SlideHeaderState.ShowLogo2 = checkEditLogo2.Checked & checkEditEnableSlideHeader.Checked;
				_settingsContainer.SlideHeaderState.ShowLogo3 = checkEditLogo3.Checked & checkEditEnableSlideHeader.Checked;
				_settingsContainer.SlideHeaderState.ShowLogo4 = checkEditLogo4.Checked & checkEditEnableSlideHeader.Checked;
				_settingsContainer.SlideHeaderState.ShowName = checkEditPublicationName.Checked & checkEditEnableSlideHeader.Checked;
				_settingsContainer.SlideHeaderState.ShowSlideHeader = checkEditSlideTitle.Checked & checkEditEnableSlideHeader.Checked;
				_settingsContainer.SetSlideHeader();
				_settingsContainer.SettingsNotSaved = true;
			}
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			SaveSlideHeader();
		}

		private void checkEditEnableSlideHeader_CheckedChanged(object sender, EventArgs e)
		{
			pnMain.Enabled = checkEditEnableSlideHeader.Checked;
			SaveSlideHeader();
		}

		private void pbHelp_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("headersnavbar");
		}

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion
	}
}