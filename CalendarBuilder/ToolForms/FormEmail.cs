using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CalendarBuilder.ToolForms
{
    public partial class FormEmail : Form
    {
        private List<Image> _previewImages = new List<Image>();
        public string PresentationFile { get; set; }

        public FormEmail()
        {
            InitializeComponent();
        }

        #region Form GUI Event Habdlers
        private void FormQuickView_Shown(object sender, EventArgs e)
        {
            barOperations.ItemLinks[2].Visible = !InteropClasses.PowerPointHelper.Instance.Is2003;
            if (!string.IsNullOrEmpty(this.PresentationFile))
            {
                laSlideSize.Text = string.Format("{0} {1} x {2}", new object[] { ConfigurationClasses.SettingsManager.Instance.Orientation, ConfigurationClasses.SettingsManager.Instance.SizeWidth.ToString("#.##"), ConfigurationClasses.SettingsManager.Instance.SizeHeght.ToString("#.##") });
                GetPreviewImages();
                comboBoxEditSlides.SelectedIndexChanged -= new EventHandler(comboBoxEditSlides_SelectedIndexChanged);
                comboBoxEditSlides.Properties.Items.Clear();
                for (int i = 1; i <= _previewImages.Count; i++)
                    comboBoxEditSlides.Properties.Items.Add(i.ToString());
                if (_previewImages.Count > 0)
                    comboBoxEditSlides.SelectedIndex = 0;
                comboBoxEditSlides_SelectedIndexChanged(null, null);
                comboBoxEditSlides.SelectedIndexChanged += new EventHandler(comboBoxEditSlides_SelectedIndexChanged);
            }
        }

        private void FormQuickView_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClearPreviewImages();
        }

        private void FormQuickView_Resize(object sender, EventArgs e)
        {
            comboBoxEditSlides.Left = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
        }
        #endregion

        #region Button Clicks
        private void barButtonItemRegularEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.PresentationFile))
            {
                using (FormEmailFileName form = new FormEmailFileName())
                {
                    ConfigurationClasses.RegistryHelper.MainFormHandle = form.Handle;
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string emailFile = Path.Combine(Path.GetFullPath(this.PresentationFile).Replace(Path.GetFileName(this.PresentationFile), string.Empty), form.FileName + ".ppt");
                        try
                        {
                            File.Copy(this.PresentationFile, emailFile, true);
                            if (InteropClasses.OutlookHelper.Instance.Open())
                            {
                                InteropClasses.OutlookHelper.Instance.CreateMessage("Advertising Schedule", emailFile);
                                InteropClasses.OutlookHelper.Instance.Close();
                            }
                            else
                                AppManager.ShowWarning("Cannot open Outlook");
                            File.Delete(emailFile);
                        }
                        catch
                        {
                        }
                    }
                    ConfigurationClasses.RegistryHelper.MainFormHandle = this.Handle;
                }
            }
        }

        private void barLargeButtonItemLockedEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.PresentationFile))
            {
                using (FormEmailFileName form = new FormEmailFileName())
                {
                    ConfigurationClasses.RegistryHelper.MainFormHandle = form.Handle;
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string emailFile = Path.Combine(Path.GetFullPath(this.PresentationFile).Replace(Path.GetFileName(this.PresentationFile), string.Empty), form.FileName + ".ppt");
                        try
                        {
                            InteropClasses.PowerPointHelper.Instance.CreateLockedPresentation(this.PresentationFile.Replace(Path.GetExtension(this.PresentationFile), string.Empty), emailFile);
                            if (File.Exists(emailFile))
                            {
                                if (InteropClasses.OutlookHelper.Instance.Open())
                                {
                                    InteropClasses.OutlookHelper.Instance.CreateMessage("Advertising Schedule", emailFile);
                                    InteropClasses.OutlookHelper.Instance.Close();
                                }
                                else
                                    AppManager.ShowWarning("Cannot open Outlook");
                                File.Delete(emailFile);
                            }
                        }
                        catch
                        {
                        }
                    }
                    ConfigurationClasses.RegistryHelper.MainFormHandle = this.Handle;
                }
            }
        }

        private void barLargeButtonItemPDFEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.PresentationFile))
            {
                using (FormEmailFileName form = new FormEmailFileName())
                {
                    ConfigurationClasses.RegistryHelper.MainFormHandle = form.Handle;
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string emailFile = Path.Combine(Path.GetFullPath(this.PresentationFile).Replace(Path.GetFileName(this.PresentationFile), string.Empty), form.FileName + ".pdf");
                        try
                        {
                            InteropClasses.PowerPointHelper.Instance.ConvertToPDF(this.PresentationFile, emailFile);
                            if (File.Exists(emailFile))
                            {
                                if (InteropClasses.OutlookHelper.Instance.Open())
                                {
                                    InteropClasses.OutlookHelper.Instance.CreateMessage("Advertising Schedule", emailFile);
                                    InteropClasses.OutlookHelper.Instance.Close();
                                }
                                else
                                    AppManager.ShowWarning("Cannot open Outlook");
                                File.Delete(emailFile);
                            }
                        }
                        catch
                        {
                        }
                    }
                    ConfigurationClasses.RegistryHelper.MainFormHandle = this.Handle;
                }
            }
        }

        private void barLargeButtonItemHelp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("email");
        }

        private void barLargeButtonItemExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Other Event Handlers
        private void comboBoxEditSlides_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.PresentationFile))
            {
                pictureBoxPreview.Image = _previewImages[comboBoxEditSlides.SelectedIndex];
                laSlideNumber.Text = string.Format("Slide {0} of {1}", new object[] { (comboBoxEditSlides.SelectedIndex + 1).ToString(), _previewImages.Count.ToString() });
            }
        }

        private void comboBoxEditSlides_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.PresentationFile))
            {
                if (e.Button.Index == 1)
                {
                    int selectedIndex = comboBoxEditSlides.SelectedIndex + 1;
                    if (selectedIndex >= _previewImages.Count)
                        selectedIndex = 0;
                    comboBoxEditSlides.SelectedIndex = selectedIndex;
                }
                else if (e.Button.Index == 2)
                {
                    int selectedIndex = comboBoxEditSlides.SelectedIndex - 1;
                    if (selectedIndex < 0)
                        selectedIndex = _previewImages.Count - 1;
                    comboBoxEditSlides.SelectedIndex = selectedIndex;
                }
            }
        }
        #endregion

        #region Common Methods
        private void GetPreviewImages()
        {
            if (!string.IsNullOrEmpty(this.PresentationFile))
            {
                string previewFolderPath = this.PresentationFile.Replace(Path.GetExtension(this.PresentationFile), string.Empty);
                if (Directory.Exists(previewFolderPath))
                {
                    _previewImages.Clear();
                    string[] previewImages = Directory.GetFiles(previewFolderPath, "*.png");
                    Array.Sort(previewImages, (x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));
                    for (int i = 0; i < previewImages.Length; i++)
                        _previewImages.Add(new System.Drawing.Bitmap(previewImages[i], true));
                }
            }
        }

        private void ClearPreviewImages()
        {
            try
            {
                foreach (Image image in _previewImages)
                    image.Dispose();
                _previewImages.Clear();
                File.Delete(this.PresentationFile);
                Directory.Delete(this.PresentationFile.Replace(Path.GetExtension(this.PresentationFile), string.Empty), true);
            }
            catch
            {
            }
        }
        #endregion
    }
}
