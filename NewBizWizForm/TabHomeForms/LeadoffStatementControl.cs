using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NewBizWizForm.TabHomeForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class LeadoffStatementControl : UserControl
    {
        private static LeadoffStatementControl _instance;
        private bool _allowToSave = false;
        public AppManager.SingleParameterDelegate EnableOutput { get; set; }
        public AppManager.SingleParameterDelegate EnableSavedFiles { get; set; }

        public bool SettingsNotSaved { get; set; }

        private LeadoffStatementControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
                laSlideHeader.Font = new Font(laSlideHeader.Font.FontFamily, laSlideHeader.Font.Size - 2, laSlideHeader.Font.Style);
                laDetail.Font = new Font(laDetail.Font.FontFamily, laDetail.Font.Size - 3, laDetail.Font.Style);
                ckA.Font = new Font(ckA.Font.FontFamily, ckA.Font.Size - 3, ckA.Font.Style);
                ckB.Font = new Font(ckB.Font.FontFamily, ckB.Font.Size - 3, ckB.Font.Style);
                ckC.Font = new Font(ckC.Font.FontFamily, ckC.Font.Size - 3, ckC.Font.Style);
            }
            UpdateEditState();
            comboBoxEditSlideHeader.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            comboBoxEditSlideHeader.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            comboBoxEditSlideHeader.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            memoEditA.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            memoEditA.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            memoEditA.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            memoEditB.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            memoEditB.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            memoEditB.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            memoEditC.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            memoEditC.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            memoEditC.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
        }

        public static LeadoffStatementControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LeadoffStatementControl();
                return _instance;
            }
        }

        private void UpdateEditState()
        {
            memoEditA.Enabled = ckA.Checked;
            memoEditB.Enabled = ckB.Checked;
            memoEditC.Enabled = ckC.Checked;
        }

        private void LoadSavedState()
        {
            _allowToSave = false;
            if (string.IsNullOrEmpty(ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.SlideHeader))
            {
                if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
                    comboBoxEditSlideHeader.SelectedIndex = 0;
            }
            else
            {
                int index = comboBoxEditSlideHeader.Properties.Items.IndexOf(ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.SlideHeader);
                if (index >= 0)
                    comboBoxEditSlideHeader.SelectedIndex = index;
                else
                    comboBoxEditSlideHeader.SelectedIndex = 0;
            }
            ckA.Checked = ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement1;
            ckB.Checked = ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement2;
            ckC.Checked = ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement3;
            memoEditA.EditValue = !string.IsNullOrEmpty(ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.Statement1) ? ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.Statement1 : (BusinessClasses.ListManager.Instance.LeadoffStatementLists.Statements.Count > 0 ? BusinessClasses.ListManager.Instance.LeadoffStatementLists.Statements[0] : string.Empty);
            memoEditB.EditValue = !string.IsNullOrEmpty(ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.Statement2) ? ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.Statement2 : (BusinessClasses.ListManager.Instance.LeadoffStatementLists.Statements.Count > 1 ? BusinessClasses.ListManager.Instance.LeadoffStatementLists.Statements[1] : string.Empty);
            memoEditC.EditValue = !string.IsNullOrEmpty(ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.Statement3) ? ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.Statement3 : (BusinessClasses.ListManager.Instance.LeadoffStatementLists.Statements.Count > 2 ? BusinessClasses.ListManager.Instance.LeadoffStatementLists.Statements[2] : string.Empty);

            _allowToSave = true;
            this.SettingsNotSaved = false;

            UpdateOutputState();
            UpdateSavedFilesState();
        }

        private void SaveState()
        {
            ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : string.Empty;
            ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement1 = ckA.Checked;
            ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement2 = ckB.Checked;
            ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.ShowStatement3 = ckC.Checked;
            ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.Statement1 = memoEditA.EditValue != null ? memoEditA.EditValue.ToString() : string.Empty;
            ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.Statement2 = memoEditB.EditValue != null ? memoEditB.EditValue.ToString() : string.Empty;
            ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.Statement3 = memoEditC.EditValue != null ? memoEditC.EditValue.ToString() : string.Empty;
            this.SettingsNotSaved = false;
        }

        public void LoadFromFile()
        {
            using (FormSavedLeadoffStatement form = new FormSavedLeadoffStatement())
            {
                if (form.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(form.SelectedFile))
                {
                    ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.Load(form.SelectedFile);
                    LoadSavedState();
                }
            }
        }

        private void LeadoffStatementControl_Load(object sender, EventArgs e)
        {
            comboBoxEditSlideHeader.Properties.Items.Clear();
            comboBoxEditSlideHeader.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.LeadoffStatementLists.Headers);

            FormMain.Instance.FormClosed += new FormClosedEventHandler((sender1, e1) =>
            {
                if (this.SettingsNotSaved)
                {
                    SaveState();
                    ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.Save();
                }
            });

            LoadSavedState();
        }

        private void checkBoxes_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEditState();
            UpdateOutputState();
            if (_allowToSave)
                this.SettingsNotSaved = true;
        }

        private void memoEditC_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
                this.SettingsNotSaved = true;
        }

        #region Output Staff
        public int StatementsCount
        {
            get
            {
                int result = 0;
                if (ckA.Checked && memoEditA.EditValue != null)
                    if (!string.IsNullOrEmpty(memoEditA.EditValue.ToString().Trim()))
                        result++;
                if (ckB.Checked && memoEditB.EditValue != null)
                    if (!string.IsNullOrEmpty(memoEditB.EditValue.ToString().Trim()))
                        result++;
                if (ckC.Checked && memoEditC.EditValue != null)
                    if (!string.IsNullOrEmpty(memoEditC.EditValue.ToString().Trim()))
                        result++;
                return result;
            }
        }

        public string Title
        {
            get
            {
                return comboBoxEditSlideHeader.EditValue == null ? string.Empty : comboBoxEditSlideHeader.EditValue.ToString();
            }
        }

        public string[] SelectedStatements
        {
            get
            {
                List<string> result = new List<string>();
                if (ckA.Checked && memoEditA.EditValue != null)
                    if (!string.IsNullOrEmpty(memoEditA.EditValue.ToString().Trim()))
                        result.Add(memoEditA.EditValue.ToString());
                if (ckB.Checked && memoEditB.EditValue != null)
                    if (!string.IsNullOrEmpty(memoEditB.EditValue.ToString().Trim()))
                        result.Add(memoEditB.EditValue.ToString());
                if (ckC.Checked && memoEditC.EditValue != null)
                    if (!string.IsNullOrEmpty(memoEditC.EditValue.ToString().Trim()))
                        result.Add(memoEditC.EditValue.ToString());
                return result.ToArray();
            }
        }

        public void UpdateOutputState()
        {
            if (EnableOutput != null)
                EnableOutput(ckA.Checked || ckB.Checked || ckC.Checked);
        }

        public void UpdateSavedFilesState()
        {
            if (EnableSavedFiles != null)
                EnableSavedFiles(ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.AllowToLoad());
        }

        public void Output()
        {
            if (this.SettingsNotSaved)
            {
                SaveState();
                ConfigurationClasses.ViewSettingsManager.Instance.LeadoffStatementState.Save();
                UpdateSavedFilesState();
            }
            InteropClasses.PowerPointHelper.Instance.AppendLeadoffStatements();
        }
        #endregion
    }
}
