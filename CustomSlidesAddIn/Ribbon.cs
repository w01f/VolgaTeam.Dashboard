using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace CustomSlidesAddIn
{
    public partial class Ribbon
    {
        private RibbonGallery galleryCustomSlides43 = null;
        private RibbonGallery galleryCustomSlides54 = null;
        private RibbonGallery galleryCustomSlides169 = null;
        private RibbonGallery galleryCustomSlides34 = null;
        private RibbonGallery galleryCustomSlides45 = null;

        #region Common Methods and Event Handlers
        private void InitRibbon()
        {
            groupCustomSlides.Label = ConfigurationClasses.SettingsManager.Instance.ApplicationName;
            galleryCustomSlides43.Image = ConfigurationClasses.SettingsManager.Instance.ApplicationLogo;
            galleryCustomSlides54.Image = ConfigurationClasses.SettingsManager.Instance.ApplicationLogo;
            galleryCustomSlides169.Image = ConfigurationClasses.SettingsManager.Instance.ApplicationLogo;
            galleryCustomSlides34.Image = ConfigurationClasses.SettingsManager.Instance.ApplicationLogo;
            galleryCustomSlides45.Image = ConfigurationClasses.SettingsManager.Instance.ApplicationLogo;

            LoadSlideSize();
            UpdateSlideSizeAccordingExistedCustomSlides();
        }

        private void Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
            InitRibbon();
        }
        #endregion

        #region Slide Format Methods and Event Handlers
        private void LoadSlideSize()
        {
            if (ConfigurationClasses.SettingsManager.Instance.Orientation.Equals("Landscape"))
            {
                if (ConfigurationClasses.SettingsManager.Instance.SizeWidth == 10 && ConfigurationClasses.SettingsManager.Instance.SizeHeght == 7.5)
                {
                    toggleButtonSlideFormat43.Checked = true;
                    toggleButtonSlideFormat54.Checked = false;
                    toggleButtonSlideFormat169.Checked = false;
                    toggleButtonSlideFormat34.Checked = false;
                    toggleButtonSlideFormat45.Checked = false;
                }
                else if (ConfigurationClasses.SettingsManager.Instance.SizeWidth == 10.75 && ConfigurationClasses.SettingsManager.Instance.SizeHeght == 8.25)
                {
                    toggleButtonSlideFormat43.Checked = false;
                    toggleButtonSlideFormat54.Checked = true;
                    toggleButtonSlideFormat169.Checked = false;
                    toggleButtonSlideFormat34.Checked = false;
                    toggleButtonSlideFormat45.Checked = false;
                }
                else if (ConfigurationClasses.SettingsManager.Instance.SizeWidth == 10 && ConfigurationClasses.SettingsManager.Instance.SizeHeght == 5.63)
                {
                    toggleButtonSlideFormat43.Checked = false;
                    toggleButtonSlideFormat54.Checked = false;
                    toggleButtonSlideFormat169.Checked = true;
                    toggleButtonSlideFormat34.Checked = false;
                    toggleButtonSlideFormat45.Checked = false;
                }
            }
            else
            {
                if (ConfigurationClasses.SettingsManager.Instance.SizeWidth == 10 && ConfigurationClasses.SettingsManager.Instance.SizeHeght == 7.5)
                {
                    toggleButtonSlideFormat43.Checked = false;
                    toggleButtonSlideFormat54.Checked = false;
                    toggleButtonSlideFormat169.Checked = false;
                    toggleButtonSlideFormat34.Checked = true;
                    toggleButtonSlideFormat45.Checked = false;
                }
                else if (ConfigurationClasses.SettingsManager.Instance.SizeWidth == 10.75 && ConfigurationClasses.SettingsManager.Instance.SizeHeght == 8.25)
                {
                    toggleButtonSlideFormat43.Checked = false;
                    toggleButtonSlideFormat54.Checked = false;
                    toggleButtonSlideFormat169.Checked = false;
                    toggleButtonSlideFormat34.Checked = false;
                    toggleButtonSlideFormat45.Checked = true;
                }
            }
        }

        private void SaveSlideSize()
        {
            if (toggleButtonSlideFormat43.Checked)
            {
                ConfigurationClasses.SettingsManager.Instance.SizeWidth = 10;
                ConfigurationClasses.SettingsManager.Instance.SizeHeght = 7.5;
                ConfigurationClasses.SettingsManager.Instance.Orientation = "Landscape";
            }
            else if (toggleButtonSlideFormat54.Checked)
            {
                ConfigurationClasses.SettingsManager.Instance.SizeWidth = 10.75;
                ConfigurationClasses.SettingsManager.Instance.SizeHeght = 8.25;
                ConfigurationClasses.SettingsManager.Instance.Orientation = "Landscape";
            }
            else if (toggleButtonSlideFormat169.Checked)
            {
                ConfigurationClasses.SettingsManager.Instance.SizeWidth = 10;
                ConfigurationClasses.SettingsManager.Instance.SizeHeght = 5.63;
                ConfigurationClasses.SettingsManager.Instance.Orientation = "Landscape";
            }
            else if (toggleButtonSlideFormat34.Checked)
            {
                ConfigurationClasses.SettingsManager.Instance.SizeWidth = 10;
                ConfigurationClasses.SettingsManager.Instance.SizeHeght = 7.5;
                ConfigurationClasses.SettingsManager.Instance.Orientation = "Portrait";
            }
            else if (toggleButtonSlideFormat45.Checked)
            {
                ConfigurationClasses.SettingsManager.Instance.SizeWidth = 10.75;
                ConfigurationClasses.SettingsManager.Instance.SizeHeght = 8.25;
                ConfigurationClasses.SettingsManager.Instance.Orientation = "Portrait";
            }

            if (ConfigurationClasses.SettingsManager.Instance.ChangeSizeAutomatically)
                InteropClasses.PowerPointHelper.Instance.SetPresentationSettings();

            groupCustomSlides.SuspendLayout();
            galleryCustomSlides34.Visible = toggleButtonSlideFormat34.Checked;
            galleryCustomSlides43.Visible = toggleButtonSlideFormat43.Checked;
            galleryCustomSlides45.Visible = toggleButtonSlideFormat45.Checked;
            galleryCustomSlides54.Visible = toggleButtonSlideFormat54.Checked;
            galleryCustomSlides169.Visible = toggleButtonSlideFormat169.Checked;
            groupCustomSlides.ResumeLayout(true);
        }

        private void UpdateSlideSizeAccordingExistedCustomSlides()
        {
            toggleButtonSlideFormat43.Enabled = BusinessClasses.CustomSlidesManager.Instance.CustomSlides43.Count > 0;
            if (toggleButtonSlideFormat43.Checked && !toggleButtonSlideFormat43.Enabled)
                toggleButtonSlideFormat43.Checked = false;
            toggleButtonSlideFormat54.Enabled = BusinessClasses.CustomSlidesManager.Instance.CustomSlides54.Count > 0;
            if (toggleButtonSlideFormat54.Checked && !toggleButtonSlideFormat54.Enabled)
                toggleButtonSlideFormat54.Checked = false;
            toggleButtonSlideFormat169.Enabled = BusinessClasses.CustomSlidesManager.Instance.CustomSlides169.Count > 0;
            if (toggleButtonSlideFormat169.Checked && !toggleButtonSlideFormat169.Enabled)
                toggleButtonSlideFormat169.Checked = false;
            toggleButtonSlideFormat34.Enabled = BusinessClasses.CustomSlidesManager.Instance.CustomSlides34.Count > 0;
            if (toggleButtonSlideFormat34.Checked && !toggleButtonSlideFormat34.Enabled)
                toggleButtonSlideFormat34.Checked = false;
            toggleButtonSlideFormat45.Enabled = BusinessClasses.CustomSlidesManager.Instance.CustomSlides45.Count > 0;
            if (toggleButtonSlideFormat45.Checked && !toggleButtonSlideFormat45.Enabled)
                toggleButtonSlideFormat45.Checked = false;

            if (!toggleButtonSlideFormat43.Checked && !toggleButtonSlideFormat54.Checked && !toggleButtonSlideFormat169.Checked && !toggleButtonSlideFormat34.Checked && !toggleButtonSlideFormat45.Checked)
            {
                if (toggleButtonSlideFormat43.Enabled)
                    toggleButtonSlideFormat43.Checked = true;
                else if (toggleButtonSlideFormat54.Enabled)
                    toggleButtonSlideFormat54.Checked = true;
                else if (toggleButtonSlideFormat169.Enabled)
                    toggleButtonSlideFormat169.Checked = true;
                else if (toggleButtonSlideFormat34.Enabled)
                    toggleButtonSlideFormat34.Checked = true;
                else if (toggleButtonSlideFormat45.Enabled)
                    toggleButtonSlideFormat45.Checked = true;
            }

            SaveSlideSize();
        }

        private void toggleButtonSlideFormat_Click(object sender, RibbonControlEventArgs e)
        {
            bool declineOperetion = false;
            if (BusinessClasses.CommonMethods.AplicationDetected() && ConfigurationClasses.SettingsManager.Instance.ChangeSizeAutomatically)
            {
                if (BusinessClasses.CommonMethods.ShowWarningQuestion("All active applications will be closed before you change PowerPoint settings.\nDo you want to continue?") == System.Windows.Forms.DialogResult.Yes)
                    BusinessClasses.CommonMethods.CloseActiveApplications();
                else
                    declineOperetion = true;
            }
            RibbonToggleButton buttonPressed = (sender as RibbonToggleButton);
            using (ToolForms.FormFormatChangeNotification form = new ToolForms.FormFormatChangeNotification())
            {
                string currentFormatText = ConfigurationClasses.SettingsManager.Instance.SlideSize;
                string futureFormatText = string.Empty;
                if (buttonPressed == toggleButtonSlideFormat43)
                    futureFormatText = "Landscape 4 x 3";
                else if (buttonPressed == toggleButtonSlideFormat54)
                    futureFormatText = "Landscape 5 x 4";
                else if (buttonPressed == toggleButtonSlideFormat169)
                    futureFormatText = "Landscape 16 x 9";
                else if (buttonPressed == toggleButtonSlideFormat34)
                    futureFormatText = "Portrait 3 x 4";
                else if (buttonPressed == toggleButtonSlideFormat45)
                    futureFormatText = "Portrait 4 x 5";
                form.labelControlCurrentState.Text = "Your curent presentation is: " + currentFormatText;
                form.labelControlFutureState.Text = "You want to change your presentation to: " + futureFormatText;
                if (form.ShowDialog() != System.Windows.Forms.DialogResult.Yes)
                    declineOperetion = true;
            }

            if (!declineOperetion)
            {
                toggleButtonSlideFormat43.Checked = false;
                toggleButtonSlideFormat54.Checked = false;
                toggleButtonSlideFormat169.Checked = false;
                toggleButtonSlideFormat34.Checked = false;
                toggleButtonSlideFormat45.Checked = false;
                buttonPressed.Checked = true;
                SaveSlideSize();
            }
        }
        #endregion

        #region Custom Slides Methods
        private void LoadCustomSlides()
        {
            galleryCustomSlides43 = this.Factory.CreateRibbonGallery();
            galleryCustomSlides43.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            galleryCustomSlides43.ItemImageSize = new System.Drawing.Size(150, 150);
            galleryCustomSlides43.ColumnCount = 1;
            galleryCustomSlides43.Visible = false;
            galleryCustomSlides43.Image = ConfigurationClasses.SettingsManager.Instance.ApplicationLogo;
            foreach (BusinessClasses.CustomSlide customSlide in BusinessClasses.CustomSlidesManager.Instance.CustomSlides43)
                galleryCustomSlides43.Items.Add(customSlide.CustomSlideButton);
            galleryCustomSlides43.Click += new RibbonControlEventHandler(gallery43Item_Click);
            groupCustomSlides.Items.Add(galleryCustomSlides43);

            galleryCustomSlides54 = this.Factory.CreateRibbonGallery();
            galleryCustomSlides54.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            galleryCustomSlides54.ItemImageSize = new System.Drawing.Size(150, 150);
            galleryCustomSlides54.ColumnCount = 1;
            galleryCustomSlides54.Visible = false;
            galleryCustomSlides54.Image = ConfigurationClasses.SettingsManager.Instance.ApplicationLogo;
            foreach (BusinessClasses.CustomSlide customSlide in BusinessClasses.CustomSlidesManager.Instance.CustomSlides54)
                galleryCustomSlides54.Items.Add(customSlide.CustomSlideButton);
            galleryCustomSlides54.Click += new RibbonControlEventHandler(gallery54Item_Click);
            groupCustomSlides.Items.Add(galleryCustomSlides54);

            galleryCustomSlides169 = this.Factory.CreateRibbonGallery();
            galleryCustomSlides169.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            galleryCustomSlides169.ItemImageSize = new System.Drawing.Size(150, 150);
            galleryCustomSlides169.ColumnCount = 1;
            galleryCustomSlides169.Visible = false;
            galleryCustomSlides169.Image = ConfigurationClasses.SettingsManager.Instance.ApplicationLogo;
            foreach (BusinessClasses.CustomSlide customSlide in BusinessClasses.CustomSlidesManager.Instance.CustomSlides169)
                galleryCustomSlides169.Items.Add(customSlide.CustomSlideButton);
            galleryCustomSlides169.Click += new RibbonControlEventHandler(gallery169Item_Click);
            groupCustomSlides.Items.Add(galleryCustomSlides169);

            galleryCustomSlides34 = this.Factory.CreateRibbonGallery();
            galleryCustomSlides34.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            galleryCustomSlides34.ItemImageSize = new System.Drawing.Size(150, 150);
            galleryCustomSlides34.ColumnCount = 1;
            galleryCustomSlides34.Visible = false;
            galleryCustomSlides34.Image = ConfigurationClasses.SettingsManager.Instance.ApplicationLogo;
            foreach (BusinessClasses.CustomSlide customSlide in BusinessClasses.CustomSlidesManager.Instance.CustomSlides34)
                galleryCustomSlides34.Items.Add(customSlide.CustomSlideButton);
            galleryCustomSlides34.Click += new RibbonControlEventHandler(gallery34Item_Click);
            groupCustomSlides.Items.Add(galleryCustomSlides34);

            galleryCustomSlides45 = this.Factory.CreateRibbonGallery();
            galleryCustomSlides45.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            galleryCustomSlides45.ItemImageSize = new System.Drawing.Size(150, 150);
            galleryCustomSlides45.ColumnCount = 1;
            galleryCustomSlides45.Visible = false;
            galleryCustomSlides45.Image = ConfigurationClasses.SettingsManager.Instance.ApplicationLogo;
            foreach (BusinessClasses.CustomSlide customSlide in BusinessClasses.CustomSlidesManager.Instance.CustomSlides45)
                galleryCustomSlides45.Items.Add(customSlide.CustomSlideButton);
            galleryCustomSlides45.Click += new RibbonControlEventHandler(gallery54Item_Click);
            groupCustomSlides.Items.Add(galleryCustomSlides45);

            groupCustomSlides.Items.Add(boxLandscape);
            groupCustomSlides.Items.Add(boxPortrait);
        }

        private void gallery43Item_Click(object sender, RibbonControlEventArgs e)
        {
            if (galleryCustomSlides43.SelectedItem != null)
            {
                string slideSourcePath = galleryCustomSlides43.SelectedItem.Tag as string;
                if (!string.IsNullOrEmpty(slideSourcePath))
                    InteropClasses.PowerPointHelper.Instance.AppendCustomSlide(slideSourcePath);
            }
        }

        private void gallery54Item_Click(object sender, RibbonControlEventArgs e)
        {
            if (galleryCustomSlides54.SelectedItem != null)
            {
                string slideSourcePath = galleryCustomSlides54.SelectedItem.Tag as string;
                if (!string.IsNullOrEmpty(slideSourcePath))
                    InteropClasses.PowerPointHelper.Instance.AppendCustomSlide(slideSourcePath);
            }
        }

        private void gallery169Item_Click(object sender, RibbonControlEventArgs e)
        {
            if (galleryCustomSlides169.SelectedItem != null)
            {
                string slideSourcePath = galleryCustomSlides169.SelectedItem.Tag as string;
                if (!string.IsNullOrEmpty(slideSourcePath))
                    InteropClasses.PowerPointHelper.Instance.AppendCustomSlide(slideSourcePath);
            }
        }

        private void gallery34Item_Click(object sender, RibbonControlEventArgs e)
        {
            if (galleryCustomSlides34.SelectedItem != null)
            {
                string slideSourcePath = galleryCustomSlides34.SelectedItem.Tag as string;
                if (!string.IsNullOrEmpty(slideSourcePath))
                    InteropClasses.PowerPointHelper.Instance.AppendCustomSlide(slideSourcePath);
            }
        }

        private void gallery45Item_Click(object sender, RibbonControlEventArgs e)
        {
            if (galleryCustomSlides45.SelectedItem != null)
            {
                string slideSourcePath = galleryCustomSlides45.SelectedItem.Tag as string;
                if (!string.IsNullOrEmpty(slideSourcePath))
                    InteropClasses.PowerPointHelper.Instance.AppendCustomSlide(slideSourcePath);
            }
        }
        #endregion
    }
}
