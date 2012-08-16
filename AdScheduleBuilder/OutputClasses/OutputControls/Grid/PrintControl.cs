﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class PrintControl : UserControl
    {
        private OutputClasses.OutputControls.IGridOutputControl _settingsContainer = null;
        private bool _allowToSave = false;

        public PrintControl(OutputClasses.OutputControls.IGridOutputControl settingsContainer)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            _settingsContainer = settingsContainer;
            if ((base.CreateGraphics()).DpiX > 96)
            {
                Font font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
                styleController.Appearance.Font = font;
                styleController.AppearanceDisabled.Font = font;
                styleController.AppearanceDropDown.Font = font;
                styleController.AppearanceDropDownHeader.Font = font;
                styleController.AppearanceFocused.Font = font;
                styleController.AppearanceReadOnly.Font = font;
            }
        }

        private bool AllowCheckColumnsButton(bool checkState)
        {
            int count = _settingsContainer.SelectedColumnsCount;
            if (checkState)
                return count > 4;
            else
                return count < 12;
        }

        public void LoadColumnsState()
        {
            _allowToSave = false;
            buttonXColor.Enabled = _settingsContainer.EnableColorHeader;
            buttonXCost.Enabled = _settingsContainer.EnableCostHeader;
            buttonXDate.Enabled = _settingsContainer.EnableDateHeader;
            buttonXDeadline.Enabled = _settingsContainer.EnableDeadlineHeader;
            buttonXDelivery.Enabled = _settingsContainer.EnableDeliveryHeader;
            buttonXDiscounts.Enabled = _settingsContainer.EnableDiscountHeader;
            buttonXTotalCost.Enabled = _settingsContainer.EnableFinalCostHeader;
            buttonXID.Enabled = _settingsContainer.EnableIDHeader;
            buttonXIndex.Enabled = _settingsContainer.EnableIndexHeader;
            buttonXMechanicals.Enabled = _settingsContainer.EnableMechanicalsHeader;
            buttonXPageSize.Enabled = _settingsContainer.EnablePageSizeHeader;
            buttonXPercentOfPage.Enabled = BusinessClasses.ListManager.Instance.ShareUnits.Count > 0 && _settingsContainer.EnablePercentOfPageHeader;
            buttonXPCI.Enabled = _settingsContainer.EnablePCIHeader;
            buttonXPublication.Enabled = _settingsContainer.EnablePublicationHeader;
            buttonXDimensions.Enabled = _settingsContainer.EnableDimensionsHeader;
            buttonXReadership.Enabled = _settingsContainer.EnableReadershipHeader;
            buttonXSection.Enabled = _settingsContainer.EnableSectionHeader;
            buttonXSquare.Enabled = _settingsContainer.EnableSquareHeader;

            buttonXColor.Checked = _settingsContainer.ShowColorHeader;
            buttonXCost.Checked = _settingsContainer.ShowCostHeader;
            buttonXDate.Checked = _settingsContainer.ShowDateHeader;
            buttonXDeadline.Checked = _settingsContainer.ShowDeadlineHeader;
            buttonXDelivery.Checked = _settingsContainer.ShowDeliveryHeader;
            buttonXDiscounts.Checked = _settingsContainer.ShowDiscountHeader;
            buttonXTotalCost.Checked = _settingsContainer.ShowFinalCostHeader;
            buttonXID.Checked = _settingsContainer.ShowIDHeader;
            buttonXIndex.Checked = _settingsContainer.ShowIndexHeader;
            buttonXMechanicals.Checked = _settingsContainer.ShowMechanicalsHeader;
            buttonXPageSize.Checked = _settingsContainer.ShowPageSizeHeader;
            buttonXPercentOfPage.Checked = _settingsContainer.ShowPercentOfPageHeader & buttonXPercentOfPage.Enabled;
            buttonXPCI.Checked = _settingsContainer.ShowPCIHeader;
            buttonXPublication.Checked = _settingsContainer.ShowPublicationHeader;
            buttonXDimensions.Checked = _settingsContainer.ShowDimensionsHeader;
            buttonXReadership.Checked = _settingsContainer.ShowReadershipHeader;
            buttonXSection.Checked = _settingsContainer.ShowSectionHeader;
            buttonXSquare.Checked = _settingsContainer.ShowSquareHeader;
            _allowToSave = true;
        }

        private void SaveColumnsState()
        {
            if (_allowToSave)
            {
                _settingsContainer.ShowColorHeader = buttonXColor.Checked;
                _settingsContainer.ShowCostHeader = buttonXCost.Checked;
                _settingsContainer.ShowDateHeader = buttonXDate.Checked;
                _settingsContainer.ShowDeadlineHeader = buttonXDeadline.Checked;
                _settingsContainer.ShowDeliveryHeader = buttonXDelivery.Checked;
                _settingsContainer.ShowDiscountHeader = buttonXDiscounts.Checked;
                _settingsContainer.ShowFinalCostHeader = buttonXTotalCost.Checked;
                _settingsContainer.ShowIDHeader = buttonXID.Checked;
                _settingsContainer.ShowIndexHeader = buttonXIndex.Checked;
                _settingsContainer.ShowMechanicalsHeader = buttonXMechanicals.Checked;
                _settingsContainer.ShowPageSizeHeader = buttonXPageSize.Checked;
                _settingsContainer.ShowPercentOfPageHeader = buttonXPercentOfPage.Checked;
                _settingsContainer.ShowPCIHeader = buttonXPCI.Checked;
                _settingsContainer.ShowPublicationHeader = buttonXPublication.Checked;
                _settingsContainer.ShowDimensionsHeader = buttonXDimensions.Checked;
                _settingsContainer.ShowReadershipHeader = buttonXReadership.Checked;
                _settingsContainer.ShowSectionHeader = buttonXSection.Checked;
                _settingsContainer.ShowSquareHeader = buttonXSquare.Checked;
                _settingsContainer.UpdateColumnsAccordingToggles();
                _settingsContainer.SettingsNotSaved = true;
            }
        }

        private void button_CheckedChanged(object sender, EventArgs e)
        {
            SaveColumnsState();
        }

        private void button_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonX button = sender as DevComponents.DotNetBar.ButtonX;
            if (button != null && AllowCheckColumnsButton(button.Checked))
                button.Checked = !button.Checked;
            else
                AppManager.ShowWarning("You may select between 4 and 12 Columns");
        }

        private void pbHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("printnavbar");
        }

        #region Picture Box Clicks Habdlers
        /// <summary>
        /// Buttonize the PictureBox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top += 1;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top -= 1;
        }
        #endregion
    }
}
