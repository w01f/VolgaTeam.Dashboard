using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;

namespace AdSalesAddIn
{
    public partial class ribbonAdSales
    {
        private bool _allowToSave = false;

        #region Common Methods and Event Handlers
        private void InitRibbon()
        {
            _allowToSave = false;
            bool appVisible = BusinessClasses.NBWApplicationsManager.Instance.NBWApplications.Count > 0;
            bool salesDepotVisisble = Directory.GetDirectories(ConfigurationClasses.SettingsManager.Instance.LibraryPath).Length > 0;
            bool clipartVisible = Directory.GetDirectories(ConfigurationClasses.SettingsManager.Instance.ClipartPath).Length > 0;

            groupDashboard.Label = ConfigurationClasses.SettingsManager.Instance.DashboardName;
            groupSalesDepot.Label = ConfigurationClasses.SettingsManager.Instance.SalesDepotName;
            groupSalesDepot.Visible = salesDepotVisisble;
            groupClipart.Visible = clipartVisible;
            groupApplications.Visible = appVisible;

            bool isHighDPI = BusinessClasses.CommonMethods.IsHighDPI();
            string logoPath = string.Empty;
            logoPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.SalesDepotLogoPath, isHighDPI ? ConfigurationClasses.SettingsManager.HighDPIRibbonImageFileName : ConfigurationClasses.SettingsManager.RegularDPIRibbonImageFileName);
            if (File.Exists(logoPath))
                buttonSalesDepot.Image = new Bitmap(logoPath);
            logoPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardLogoPath, isHighDPI ? ConfigurationClasses.SettingsManager.HighDPIRibbonImageFileName : ConfigurationClasses.SettingsManager.RegularDPIRibbonImageFileName);
            if (File.Exists(logoPath))
                buttonDashboard.Image = new Bitmap(logoPath);

            buttonAddCover.Image = isHighDPI ? Properties.Resources.AddCover120 : Properties.Resources.AddCover96;
            buttonAddSlide.Image = isHighDPI ? Properties.Resources.AddSlide120 : Properties.Resources.AddSlide96;
            buttonClientLogos.Image = isHighDPI ? Properties.Resources.ClientLogos120 : Properties.Resources.ClientLogos96;
            buttonMinibarKill.Image = isHighDPI ? Properties.Resources.KillMinibar120 : Properties.Resources.KillMinibar96;
            buttonMinibarLoad.Image = isHighDPI ? Properties.Resources.LoadMinibar120 : Properties.Resources.LoadMinibar96;
            buttonSalesGallery.Image = isHighDPI ? Properties.Resources.SalesGallery120 : Properties.Resources.SalesGallery96;
            buttonWebArt.Image = isHighDPI ? Properties.Resources.WebArt120 : Properties.Resources.WebArt96;

            LoadSlideSize();
            LoadMasterWizard();

            UpdateSlideSize();
            UpdateApplicationsStatus();

            InteropClasses.PowerPointHelper.Instance.SetPresentationSettings();

            _allowToSave = true;
        }

        private void Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
            InitRibbon();
        }
        #endregion

        #region Slide Format Methods
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
            ConfigurationClasses.SettingsManager.Instance.SaveSharedSettings();
            InteropClasses.PowerPointHelper.Instance.SetPresentationSettings();
        }

        private void LoadMasterWizard()
        {
            dropDownWizards.Items.Clear();
            foreach (string masterWizard in BusinessClasses.MasterWizardManager.Instance.MasterWizards.Keys)
            {
                RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                item.Label = masterWizard;
                item.Tag = BusinessClasses.MasterWizardManager.Instance.MasterWizards[masterWizard];
                dropDownWizards.Items.Add(item);
            }
            int selectedIndex = BusinessClasses.MasterWizardManager.Instance.MasterWizards.Keys.ToList().IndexOf(ConfigurationClasses.SettingsManager.Instance.SelectedWizard);
            if (selectedIndex < 0)
            {
                selectedIndex = 0;
                BusinessClasses.MasterWizardManager.Instance.SelectedWizard = BusinessClasses.MasterWizardManager.Instance.MasterWizards.Values.FirstOrDefault();
            }
            else
            {
                BusinessClasses.MasterWizard masterWizard = null;
                BusinessClasses.MasterWizardManager.Instance.MasterWizards.TryGetValue(ConfigurationClasses.SettingsManager.Instance.SelectedWizard, out masterWizard);
                BusinessClasses.MasterWizardManager.Instance.SelectedWizard = masterWizard;
            }
            if (BusinessClasses.MasterWizardManager.Instance.MasterWizards.Count > 0)
                dropDownWizards.SelectedItemIndex = selectedIndex;
        }

        private void SaveMasterWizard()
        {
            ConfigurationClasses.SettingsManager.Instance.SelectedWizard = dropDownWizards.SelectedItem != null ? dropDownWizards.SelectedItem.Label : string.Empty;
            ConfigurationClasses.SettingsManager.Instance.SaveSharedSettings();
            BusinessClasses.MasterWizard masterWizard = null;
            BusinessClasses.MasterWizardManager.Instance.MasterWizards.TryGetValue(ConfigurationClasses.SettingsManager.Instance.SelectedWizard, out masterWizard);
            BusinessClasses.MasterWizardManager.Instance.SelectedWizard = masterWizard;
        }

        private void UpdateSlideSize()
        {
            toggleButtonSlideFormat43.Enabled = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.Has43;
            if (toggleButtonSlideFormat43.Checked && !toggleButtonSlideFormat43.Enabled)
                toggleButtonSlideFormat43.Checked = false;
            toggleButtonSlideFormat54.Enabled = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.Has54;
            if (toggleButtonSlideFormat54.Checked && !toggleButtonSlideFormat54.Enabled)
                toggleButtonSlideFormat54.Checked = false;
            toggleButtonSlideFormat169.Enabled = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.Has169;
            if (toggleButtonSlideFormat169.Checked && !toggleButtonSlideFormat169.Enabled)
                toggleButtonSlideFormat169.Checked = false;
            toggleButtonSlideFormat34.Enabled = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.Has34;
            if (toggleButtonSlideFormat34.Checked && !toggleButtonSlideFormat34.Enabled)
                toggleButtonSlideFormat34.Checked = false;
            toggleButtonSlideFormat45.Enabled = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.Has45;
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
        #endregion

        #region Applications Methods
        private void UpdateApplicationsStatus()
        {
            foreach (BusinessClasses.NBWApplication nbwApplication in BusinessClasses.NBWApplicationsManager.Instance.NBWApplications)
            {
                if (nbwApplication.UseSlideTemplates && BusinessClasses.MasterWizardManager.Instance.SelectedWizard != null)
                {
                    string slideTemplatesFolderPath = Path.Combine(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.Folder.FullName, ConfigurationClasses.SettingsManager.Instance.SlideFolder, nbwApplication.SlideTemplatesPath);
                    nbwApplication.AppButton.Enabled = Directory.Exists(slideTemplatesFolderPath);
                }
            }
        }

        private void LoadNBWApplications()
        {
            tabAdSales.SuspendLayout();
            groupApplications.SuspendLayout();
            int applicationsCount = BusinessClasses.NBWApplicationsManager.Instance.NBWApplications.Count;
            if (applicationsCount <= 3)
            {
                foreach (BusinessClasses.NBWApplication nbwApplication in BusinessClasses.NBWApplicationsManager.Instance.NBWApplications)
                    this.groupApplications.Items.Add(nbwApplication.AppButton);
            }
            else
            {
                RibbonSplitButton splitButton = this.Factory.CreateRibbonSplitButton();
                splitButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
                splitButton.ItemSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
                splitButton.ScreenTip = "Open adSALESapps";
                bool isHighDPI = BusinessClasses.CommonMethods.IsHighDPI();
                splitButton.Image = isHighDPI ? Properties.Resources.Applications120 : Properties.Resources.Applications96;
                foreach (BusinessClasses.NBWApplication nbwApplication in BusinessClasses.NBWApplicationsManager.Instance.NBWApplications)
                    splitButton.Items.Add(nbwApplication.AppButton);
                this.groupApplications.Items.Add(splitButton);
            }
            this.tabAdSales.ResumeLayout(false);
            groupApplications.ResumeLayout(false);
            groupApplications.PerformLayout();
        }
        #endregion

        #region Slide Format Event Handlers
        private void dropDownWizards_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            if (_allowToSave)
            {
                bool restorePrevItem = false;
                RibbonDropDownItem prevItem = dropDownWizards.Items.Where(x => x.Label.Equals(ConfigurationClasses.SettingsManager.Instance.SelectedWizard)).FirstOrDefault();
                if (BusinessClasses.CommonMethods.AplicationDetected())
                {
                    if (BusinessClasses.CommonMethods.ShowWarningQuestion("All active applications will be closed before you change PowerPoint settings.\nDo you want to continue?") == System.Windows.Forms.DialogResult.Yes)
                        BusinessClasses.CommonMethods.CloseActiveApplications();
                    else
                        restorePrevItem = true;
                }

                if (!restorePrevItem)
                    using (ToolForms.FormFormatChangeNotification form = new ToolForms.FormFormatChangeNotification())
                    {
                        string currentFormatText = ConfigurationClasses.SettingsManager.Instance.SelectedWizard;
                        string futureFormatText = dropDownWizards.SelectedItem != null ? dropDownWizards.SelectedItem.Label : string.Empty;
                        form.labelControlCurrentState.Text = "Your curent wizard is: " + currentFormatText;
                        form.labelControlFutureState.Text = "You want to change your wizard to: " + futureFormatText;
                        if (form.ShowDialog() != System.Windows.Forms.DialogResult.Yes)
                            restorePrevItem = true;
                    }

                if (restorePrevItem)
                    dropDownWizards.SelectedItem = prevItem;
                else
                {
                    SaveMasterWizard();
                    UpdateSlideSize();
                    UpdateApplicationsStatus();
                }
            }
        }

        private void toggleButtonSlideFormat45_Click(object sender, RibbonControlEventArgs e)
        {
            bool declineOperetion = false;
            if (BusinessClasses.CommonMethods.AplicationDetected())
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
            UpdateApplicationsStatus();
        }
        #endregion

        #region Dashboard Event Handlers
        private void buttonDashboard_Click(object sender, RibbonControlEventArgs e)
        {
            BusinessClasses.CommonMethods.RunDashboard();
        }

        private void buttonAddSlide_Click(object sender, RibbonControlEventArgs e)
        {
            InteropClasses.PowerPointHelper.Instance.AppendCleanslate();
        }

        private void buttonAddCover_Click(object sender, RibbonControlEventArgs e)
        {
            using (ToolForms.FormAddCover form = new ToolForms.FormAddCover())
            {
                DialogResult result = form.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Yes)
                    BusinessClasses.CommonMethods.RunDashboard("showcover");
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    InteropClasses.PowerPointHelper.Instance.AppendGenericCover(true);
                }
            }
        }
        #endregion

        #region Sales Depot Event Handlers
        private void buttonSalesDepot_Click(object sender, RibbonControlEventArgs e)
        {
            BusinessClasses.CommonMethods.RunSalesDepot();
        }
        #endregion

        #region Clipart Event Handlers
        private void buttonSalesGallery_Click(object sender, RibbonControlEventArgs e)
        {
            BusinessClasses.CommonMethods.RunSalesGallery();
        }

        private void buttonClientLogos_Click(object sender, RibbonControlEventArgs e)
        {
            BusinessClasses.CommonMethods.RunClientLogos();
        }

        private void buttonWebArt_Click(object sender, RibbonControlEventArgs e)
        {
            BusinessClasses.CommonMethods.RunWebArt();
        }
        #endregion

        #region Minibar Event Handlers
        private void buttonMinibar_Click(object sender, RibbonControlEventArgs e)
        {
            BusinessClasses.CommonMethods.RunMinibar();
        }

        private void buttonMinibarKill_Click(object sender, RibbonControlEventArgs e)
        {
            BusinessClasses.CommonMethods.KillMinibar();
        }
        #endregion

    }
}
