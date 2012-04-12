﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class SlideHeaderControl : UserControl
    {
        private OutputClasses.OutputControls.IGridOutputControl _settingsContainer = null;
        private bool _allowToSave = false;

        public SlideHeaderControl(OutputClasses.OutputControls.IGridOutputControl settingsContainer)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            _settingsContainer = settingsContainer;
            if ((base.CreateGraphics()).DpiX > 96)
            {
                checkEditEnableSlideHeader.Font = new System.Drawing.Font(checkEditEnableSlideHeader.Font.FontFamily, checkEditEnableSlideHeader.Font.Size - 3, checkEditEnableSlideHeader.Font.Style);
                Font font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
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
                checkEditPublicationLogo1.Font = font;
                checkEditPublicationLogo2.Font = font;
                checkEditPublicationLogo3.Font = font;
                checkEditPublicationLogo4.Font = font;
                checkEditPublicationName.Font = font;
                checkEditSlideTitle.Font = font;
            }
        }

        public void LoadSlideHeader()
        {
            _allowToSave = false;
            checkEditEnableSlideHeader.Checked = _settingsContainer.SlideHeaderState.EnableSlideHeader;
            checkEditAdvertiser.Checked =  _settingsContainer.SlideHeaderState.ShowAdvertiser;
            checkEditDecisionMaker.Checked = _settingsContainer.SlideHeaderState.ShowDecisionMaker;
            checkEditFlightDates.Checked = _settingsContainer.SlideHeaderState.ShowFlightDates;
            checkEditPresentationDate.Checked = _settingsContainer.SlideHeaderState.ShowPresentationDate;
            checkEditPublicationLogo1.Checked = _settingsContainer.SlideHeaderState.ShowLogo1;
            checkEditPublicationLogo2.Checked = _settingsContainer.SlideHeaderState.ShowLogo2;
            checkEditPublicationLogo3.Checked = _settingsContainer.SlideHeaderState.ShowLogo3;
            checkEditPublicationLogo4.Checked = _settingsContainer.SlideHeaderState.ShowLogo4;
            checkEditPublicationName.Checked = _settingsContainer.SlideHeaderState.ShowName;
            checkEditSlideTitle.Checked = _settingsContainer.SlideHeaderState.ShowSlideHeader;
            _settingsContainer.SetSlideHeader();
            _allowToSave = true;
        }

        private void SaveSlideHeader()
        {
            if (_allowToSave)
            {
                _settingsContainer.SlideHeaderState.EnableSlideHeader = checkEditEnableSlideHeader.Checked;
                _settingsContainer.SlideHeaderState.ShowAdvertiser = checkEditAdvertiser.Checked & checkEditEnableSlideHeader.Checked;
                _settingsContainer.SlideHeaderState.ShowDecisionMaker = checkEditDecisionMaker.Checked & checkEditEnableSlideHeader.Checked;
                _settingsContainer.SlideHeaderState.ShowFlightDates = checkEditFlightDates.Checked & checkEditEnableSlideHeader.Checked;
                _settingsContainer.SlideHeaderState.ShowPresentationDate = checkEditPresentationDate.Checked & checkEditEnableSlideHeader.Checked;
                _settingsContainer.SlideHeaderState.ShowLogo1 = checkEditPublicationLogo1.Checked & checkEditEnableSlideHeader.Checked;
                _settingsContainer.SlideHeaderState.ShowLogo2 = checkEditPublicationLogo2.Checked & checkEditEnableSlideHeader.Checked;
                _settingsContainer.SlideHeaderState.ShowLogo3 = checkEditPublicationLogo3.Checked & checkEditEnableSlideHeader.Checked;
                _settingsContainer.SlideHeaderState.ShowLogo4 = checkEditPublicationLogo4.Checked & checkEditEnableSlideHeader.Checked;
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
    }
}
