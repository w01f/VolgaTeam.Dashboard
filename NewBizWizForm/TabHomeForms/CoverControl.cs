using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NewBizWizForm.TabHomeForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CoverControl : UserControl
    {
        private static CoverControl _instance;
        private BindingList<BusinessClasses.User> _users;
        private bool _allowToSave = false;
        public AppManager.SingleParameterDelegate EnableOutput { get; set; }
        public AppManager.SingleParameterDelegate EnableSavedFiles { get; set; }

        public bool SettingsNotSaved { get; set; }

        private CoverControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
                laAdvertiser.Font = new Font(laAdvertiser.Font.FontFamily, laAdvertiser.Font.Size - 2, laAdvertiser.Font.Style);
                laDecisionMaker.Font = new Font(laDecisionMaker.Font.FontFamily, laDecisionMaker.Font.Size - 2, laDecisionMaker.Font.Style);
                laSalesRepEmail.Font = new Font(laSalesRepEmail.Font.FontFamily, laSalesRepEmail.Font.Size - 2, laSalesRepEmail.Font.Style);
                laSalesRepPhone.Font = new Font(laSalesRepPhone.Font.FontFamily, laSalesRepPhone.Font.Size - 2, laSalesRepPhone.Font.Style);
                laSlideHeader.Font = new Font(laSlideHeader.Font.FontFamily, laSlideHeader.Font.Size - 2, laSlideHeader.Font.Style);
                checkEditFirstSlide.Font = new Font(checkEditFirstSlide.Font.FontFamily, checkEditFirstSlide.Font.Size - 2, checkEditFirstSlide.Font.Style);
                checkEditNewSolution.Font = new Font(checkEditNewSolution.Font.FontFamily, checkEditNewSolution.Font.Size - 2, checkEditNewSolution.Font.Style);
                checkEditPresentationDate.Font = new Font(checkEditPresentationDate.Font.FontFamily, checkEditPresentationDate.Font.Size - 2, checkEditPresentationDate.Font.Style);
                checkEditSalesRep.Font = new Font(checkEditSalesRep.Font.FontFamily, checkEditSalesRep.Font.Size - 2, checkEditSalesRep.Font.Style);
                checkEditUseEmptyCover.Font = new Font(checkEditUseEmptyCover.Font.FontFamily, checkEditUseEmptyCover.Font.Size - 2, checkEditUseEmptyCover.Font.Style);
                buttonXDeleteSalesQuote.Font = new Font(buttonXDeleteSalesQuote.Font.FontFamily, buttonXDeleteSalesQuote.Font.Size - 2, buttonXDeleteSalesQuote.Font.Style);
                buttonXSavedFiles.Font = new Font(buttonXSavedFiles.Font.FontFamily, buttonXSavedFiles.Font.Size - 2, buttonXSavedFiles.Font.Style);
                textEditSalesQuoteAuthor.Font = new Font(textEditSalesQuoteAuthor.Font.FontFamily, textEditSalesQuoteAuthor.Font.Size - 2, textEditSalesQuoteAuthor.Font.Style);
                memoEditSalesQuote.Font = new Font(memoEditSalesQuote.Font.FontFamily, memoEditSalesQuote.Font.Size - 2, memoEditSalesQuote.Font.Style);
            }
            comboBoxEditSlideHeader.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            comboBoxEditSlideHeader.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            comboBoxEditSlideHeader.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            comboBoxEditAdvertiser.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            comboBoxEditAdvertiser.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            comboBoxEditAdvertiser.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            comboBoxEditDecisionMaker.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            comboBoxEditDecisionMaker.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            comboBoxEditDecisionMaker.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
        }

        public static CoverControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CoverControl();
                return _instance;
            }
        }

        private void LoadSavedState()
        {
            _allowToSave = false;

            LoadSalesRep();

            if (string.IsNullOrEmpty(ConfigurationClasses.ViewSettingsManager.Instance.CoverState.SlideHeader))
            {
                if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
                    comboBoxEditSlideHeader.SelectedIndex = 0;
            }
            else
            {
                int index = comboBoxEditSlideHeader.Properties.Items.IndexOf(ConfigurationClasses.ViewSettingsManager.Instance.CoverState.SlideHeader);
                if (index >= 0)
                    comboBoxEditSlideHeader.SelectedIndex = index;
                else
                    comboBoxEditSlideHeader.SelectedIndex = 0;
            }

            checkEditNewSolution.Checked = ConfigurationClasses.ViewSettingsManager.Instance.CoverState.IsNewSolution;
            checkEditFirstSlide.Checked = ConfigurationClasses.ViewSettingsManager.Instance.CoverState.AddAsPageOne;
            checkEditPresentationDate.Checked = ConfigurationClasses.ViewSettingsManager.Instance.CoverState.ShowPresentationDate;
            dateEditPresentationDate.Enabled = checkEditPresentationDate.Checked;
            if (checkEditPresentationDate.Checked)
                dateEditPresentationDate.EditValue = ConfigurationClasses.ViewSettingsManager.Instance.CoverState.PresentationDate != DateTime.MinValue ? (object)ConfigurationClasses.ViewSettingsManager.Instance.CoverState.PresentationDate : null;
            else
                dateEditPresentationDate.EditValue = null;
            checkEditUseEmptyCover.Checked = ConfigurationClasses.ViewSettingsManager.Instance.CoverState.UseGenericCover;
            comboBoxEditAdvertiser.Enabled = !checkEditUseEmptyCover.Checked;
            comboBoxEditDecisionMaker.Enabled = !checkEditUseEmptyCover.Checked;
            comboBoxEditSlideHeader.Enabled = !checkEditUseEmptyCover.Checked;
            buttonXDeleteSalesQuote.Enabled = !checkEditUseEmptyCover.Checked;
            pbSalesQuotes.Enabled = !checkEditUseEmptyCover.Checked;
            dateEditPresentationDate.Enabled = !checkEditUseEmptyCover.Checked && checkEditPresentationDate.Checked;
            checkEditPresentationDate.Enabled = !checkEditUseEmptyCover.Checked;
            checkEditSalesRep.Enabled = !checkEditUseEmptyCover.Checked;
            lookUpEditSalesRep.Enabled = !checkEditUseEmptyCover.Checked && checkEditSalesRep.Checked;

            if (ConfigurationClasses.ViewSettingsManager.Instance.CoverState.Quote.IsSet)
            {
                textEditSalesQuoteAuthor.EditValue = ConfigurationClasses.ViewSettingsManager.Instance.CoverState.Quote.Author;
                memoEditSalesQuote.EditValue = ConfigurationClasses.ViewSettingsManager.Instance.CoverState.Quote.Text;
                buttonXDeleteSalesQuote.Visible = true;
            }
            else
            {
                textEditSalesQuoteAuthor.EditValue = null;
                memoEditSalesQuote.EditValue = null;
                buttonXDeleteSalesQuote.Visible = false;
            }

            comboBoxEditAdvertiser.EditValue = string.IsNullOrEmpty(ConfigurationClasses.ViewSettingsManager.Instance.CoverState.Advertiser) ? null : ConfigurationClasses.ViewSettingsManager.Instance.CoverState.Advertiser;
            comboBoxEditDecisionMaker.EditValue = string.IsNullOrEmpty(ConfigurationClasses.ViewSettingsManager.Instance.CoverState.DecisionMaker) ? null : ConfigurationClasses.ViewSettingsManager.Instance.CoverState.DecisionMaker;

            _allowToSave = true;
            this.SettingsNotSaved = false;

            UpdateOutputState();

            UpdateSavedFilesState();
        }

        private void SaveState()
        {
            ConfigurationClasses.ViewSettingsManager.Instance.CoverState.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : string.Empty;
            ConfigurationClasses.ViewSettingsManager.Instance.CoverState.IsNewSolution = checkEditNewSolution.Checked;
            ConfigurationClasses.ViewSettingsManager.Instance.CoverState.AddAsPageOne = checkEditFirstSlide.Checked;
            ConfigurationClasses.ViewSettingsManager.Instance.CoverState.ShowPresentationDate = checkEditPresentationDate.Checked;
            ConfigurationClasses.ViewSettingsManager.Instance.CoverState.PresentationDate = dateEditPresentationDate.DateTime;
            ConfigurationClasses.ViewSettingsManager.Instance.CoverState.UseGenericCover = checkEditUseEmptyCover.Checked;
            ConfigurationClasses.ViewSettingsManager.Instance.CoverState.Quote.Author = textEditSalesQuoteAuthor.EditValue != null ? textEditSalesQuoteAuthor.EditValue.ToString() : string.Empty;
            ConfigurationClasses.ViewSettingsManager.Instance.CoverState.Quote.Text = memoEditSalesQuote.EditValue != null ? memoEditSalesQuote.EditValue.ToString() : string.Empty;
            ConfigurationClasses.ViewSettingsManager.Instance.CoverState.Advertiser = comboBoxEditAdvertiser.EditValue != null ? comboBoxEditAdvertiser.EditValue.ToString() : string.Empty;
            ConfigurationClasses.ViewSettingsManager.Instance.CoverState.DecisionMaker = comboBoxEditDecisionMaker.EditValue != null ? comboBoxEditDecisionMaker.EditValue.ToString() : string.Empty;
            SaveSalesRep();
            this.SettingsNotSaved = false;
        }

        private void LoadSalesRep()
        {
            checkEditSalesRep.Checked = ConfigurationClasses.ViewSettingsManager.Instance.CoverState.ShowSalesRep;
            lookUpEditSalesRep.Enabled = checkEditSalesRep.Checked;
            BusinessClasses.User user = _users.Where(x => x.FullName.Equals(ConfigurationClasses.ViewSettingsManager.Instance.CoverState.SalesRep)).FirstOrDefault();
            if (user != null)
            {
                lookUpEditSalesRep.EditValue = ConfigurationClasses.ViewSettingsManager.Instance.CoverState.SalesRep;
                laSalesRepEmail.Text = user.Email;
                laSalesRepPhone.Text = user.Phone;
            }
            else
            {
                lookUpEditSalesRep.EditValue = null;
                laSalesRepEmail.Text = string.Empty;
                laSalesRepPhone.Text = string.Empty;
            }
        }

        private void SaveSalesRep()
        {
            ConfigurationClasses.ViewSettingsManager.Instance.CoverState.ShowSalesRep = checkEditSalesRep.Checked;
            BusinessClasses.User user = _users.Where(x => x.FullName.Equals(lookUpEditSalesRep.EditValue != null ? lookUpEditSalesRep.EditValue.ToString() : string.Empty)).FirstOrDefault();
            if (user != null)
            {
                laSalesRepEmail.Text = user.Email;
                laSalesRepPhone.Text = user.Phone;
                ConfigurationClasses.ViewSettingsManager.Instance.CoverState.SalesRep = user.FullName;
            }
            else
            {
                ConfigurationClasses.ViewSettingsManager.Instance.CoverState.SalesRep = string.Empty;
                laSalesRepEmail.Text = string.Empty;
                laSalesRepPhone.Text = string.Empty;
            }
            ConfigurationClasses.SettingsManager.Instance.SaveDashboardSettings();
        }

        private void CoverControl_Load(object sender, EventArgs e)
        {
            comboBoxEditSlideHeader.Properties.Items.Clear();
            comboBoxEditSlideHeader.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.CoverLists.Headers);

            comboBoxEditAdvertiser.Properties.Items.Clear();
            comboBoxEditAdvertiser.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.Advertisers.Titles);

            comboBoxEditDecisionMaker.Properties.Items.Clear();
            comboBoxEditDecisionMaker.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.DecisionMakers.Titles);

            _users = new BindingList<BusinessClasses.User>(new List<BusinessClasses.User>(BusinessClasses.ListManager.Instance.UsersList.GetUsersByStation(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.Name)));
            lookUpEditSalesRep.Properties.DataSource = _users;

            FormMain.Instance.FormClosed += new FormClosedEventHandler((sender1, e1) =>
            {
                if (this.SettingsNotSaved)
                {
                    SaveState();
                    ConfigurationClasses.ViewSettingsManager.Instance.CoverState.Save();
                }
            });

            LoadSavedState();
        }

        private void comboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                UpdateOutputState();
                this.SettingsNotSaved = true;
            }
        }

        private void checkEditSalesRep_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                SaveSalesRep();
                lookUpEditSalesRep.Enabled = checkEditSalesRep.Checked;
                this.SettingsNotSaved = true;
            }
        }

        private void lookUpEditSalesRep_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                SaveSalesRep();
                this.SettingsNotSaved = true;
            }
        }

        private void ckPresentationDate_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                dateEditPresentationDate.Enabled = checkEditPresentationDate.Checked;
                this.SettingsNotSaved = true;
            }
        }

        private void buttonXSalesQuotes_Click(object sender, EventArgs e)
        {
            using (FormQuotes form = new FormQuotes())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.SelectedQuote != null)
                    {
                        textEditSalesQuoteAuthor.EditValue = form.SelectedQuote.Author;
                        memoEditSalesQuote.EditValue = "\"" + form.SelectedQuote.Text + "\"";
                        buttonXDeleteSalesQuote.Visible = true;
                        this.SettingsNotSaved = true;
                    }
                }
            }
        }

        private void buttonXDeleteSalesQuote_Click(object sender, EventArgs e)
        {
            textEditSalesQuoteAuthor.EditValue = null;
            memoEditSalesQuote.EditValue = null;
            buttonXDeleteSalesQuote.Visible = false;
            this.SettingsNotSaved = true;
        }

        private void checkEditUseEmptyCover_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                comboBoxEditAdvertiser.Enabled = !checkEditUseEmptyCover.Checked;
                comboBoxEditDecisionMaker.Enabled = !checkEditUseEmptyCover.Checked;
                comboBoxEditSlideHeader.Enabled = !checkEditUseEmptyCover.Checked;
                lookUpEditSalesRep.Enabled = !checkEditUseEmptyCover.Checked;
                buttonXDeleteSalesQuote.Enabled = !checkEditUseEmptyCover.Checked;
                pbSalesQuotes.Enabled = !checkEditUseEmptyCover.Checked;
                dateEditPresentationDate.Enabled = !checkEditUseEmptyCover.Checked && checkEditPresentationDate.Checked;
                checkEditPresentationDate.Enabled = !checkEditUseEmptyCover.Checked;
                checkEditNewSolution.Enabled = !checkEditUseEmptyCover.Checked;
                UpdateOutputState();
                this.SettingsNotSaved = true;
            }
        }

        private void checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
                this.SettingsNotSaved = true;
        }

        private void buttonXSavedFiles_Click(object sender, EventArgs e)
        {
            using (FormSavedCover form = new FormSavedCover())
            {
                if (form.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(form.SelectedFile))
                {
                    ConfigurationClasses.ViewSettingsManager.Instance.CoverState.Load(form.SelectedFile);
                    LoadSavedState();
                }
            }
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

        #region Output Staff
        public string Title
        {
            get
            {
                return comboBoxEditSlideHeader.EditValue == null ? string.Empty : comboBoxEditSlideHeader.EditValue.ToString();
            }
        }

        public string Advertiser
        {
            get
            {
                return comboBoxEditAdvertiser.EditValue == null ? string.Empty : comboBoxEditAdvertiser.EditValue.ToString();
            }
        }

        public string DecisionMaker
        {
            get
            {
                return comboBoxEditDecisionMaker.EditValue == null ? string.Empty : comboBoxEditDecisionMaker.EditValue.ToString();
            }
        }

        public string SalesRep
        {
            get
            {
                BusinessClasses.User selectedUser = lookUpEditSalesRep.EditValue == null ? null : _users[lookUpEditSalesRep.ItemIndex];
                return selectedUser == null || !checkEditSalesRep.Checked ? string.Empty : selectedUser.FullName + "          " + selectedUser.Email + "          " + selectedUser.Phone;
            }
        }

        public string PresentationDate
        {
            get
            {
                return !checkEditPresentationDate.Checked || dateEditPresentationDate.EditValue == null ? string.Empty : dateEditPresentationDate.DateTime.ToString("MMMM d, yyyy");
            }
        }

        public string Quote
        {
            get
            {
                return (memoEditSalesQuote.EditValue == null ? string.Empty : memoEditSalesQuote.EditValue.ToString())
                    +(char)13 + (textEditSalesQuoteAuthor.EditValue == null ? string.Empty : textEditSalesQuoteAuthor.EditValue.ToString());
            }
        }

        public void UpdateOutputState()
        {
            bool result = false;
            if (comboBoxEditAdvertiser.EditValue != null && comboBoxEditDecisionMaker.EditValue != null)
                if (!string.IsNullOrEmpty(comboBoxEditAdvertiser.EditValue.ToString().Trim()) && !string.IsNullOrEmpty(comboBoxEditDecisionMaker.EditValue.ToString().Trim()))
                    result = true;
            result = result | checkEditUseEmptyCover.Checked;
            if (EnableOutput != null)
                EnableOutput(result);
        }

        public void UpdateSavedFilesState()
        {
            if (EnableSavedFiles != null)
                EnableSavedFiles(ConfigurationClasses.ViewSettingsManager.Instance.CoverState.AllowToLoad());
            buttonXSavedFiles.Enabled = ConfigurationClasses.ViewSettingsManager.Instance.CoverState.AllowToLoad();
        }

        public void Output()
        {
            if (!BusinessClasses.ListManager.Instance.Advertisers.Titles.Contains(this.Advertiser) && !string.IsNullOrEmpty(this.Advertiser))
            {
                BusinessClasses.ListManager.Instance.Advertisers.Titles.Add(this.Advertiser);
                BusinessClasses.ListManager.Instance.Advertisers.Save();
            }

            if (!BusinessClasses.ListManager.Instance.DecisionMakers.Titles.Contains(this.DecisionMaker) && !string.IsNullOrEmpty(this.DecisionMaker))
            {
                BusinessClasses.ListManager.Instance.DecisionMakers.Titles.Add(this.DecisionMaker);
                BusinessClasses.ListManager.Instance.DecisionMakers.Save();
            }

            if (this.SettingsNotSaved)
            {
                SaveState();
                ConfigurationClasses.ViewSettingsManager.Instance.CoverState.Save();
                UpdateSavedFilesState();
            }

            if (checkEditUseEmptyCover.Checked)
                InteropClasses.PowerPointHelper.Instance.AppendGenericCover(checkEditFirstSlide.Checked);
            else
                InteropClasses.PowerPointHelper.Instance.AppendCover(checkEditFirstSlide.Checked);
        }

        private void buttonXOutput_Click(object sender, EventArgs e)
        {
            Output();
        }
        #endregion
    }
}
