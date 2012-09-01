using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NewBizWizForm.TabHomeForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TargetCustomersControl : UserControl
    {
        private static TargetCustomersControl _instance;
        private bool _allowToSave = false;
        public AppManager.SingleParameterDelegate EnableOutput { get; set; }
        public AppManager.SingleParameterDelegate EnableSavedFiles { get; set; }

        public bool SettingsNotSaved { get; set; }

        private TargetCustomersControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
                laSlideHeader.Font = new Font(laSlideHeader.Font.FontFamily, laSlideHeader.Font.Size - 2, laSlideHeader.Font.Style);
                labelXDetail.Font = new Font(labelXDetail.Font.FontFamily, labelXDetail.Font.Size - 3, labelXDetail.Font.Style);
            }
            comboBoxEditSlideHeader.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            comboBoxEditSlideHeader.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            comboBoxEditSlideHeader.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
        }

        public static TargetCustomersControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TargetCustomersControl();
                return _instance;
            }
        }

        private void LoadSavedState()
        {
            _allowToSave = false;
            if (string.IsNullOrEmpty(ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.SlideHeader))
            {
                if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
                    comboBoxEditSlideHeader.SelectedIndex = 0;
            }
            else
            {
                int index = comboBoxEditSlideHeader.Properties.Items.IndexOf(ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.SlideHeader);
                if (index >= 0)
                    comboBoxEditSlideHeader.SelectedIndex = index;
                else
                    comboBoxEditSlideHeader.SelectedIndex = 0;
            }
            
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlTargetDemo.Items)
                if (ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.Demo.Contains(item.Value.ToString()))
                    item.CheckState = CheckState.Checked;
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlHouseholdIncome.Items)
                if (ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.Income.Contains(item.Value.ToString()))
                    item.CheckState = CheckState.Checked;
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGeographicResidence.Items)
                if (ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.Geographic.Contains(item.Value.ToString()))
                    item.CheckState = CheckState.Checked;
            
            _allowToSave = true;
            this.SettingsNotSaved = false;

            UpdateSavedFilesState();
            UpdateOutputState();
        }

        private void SaveState()
        {
            ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : string.Empty;
            
            ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.Demo.Clear();
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlTargetDemo.Items)
                if (item.CheckState == CheckState.Checked)
                    ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.Demo.Add(item.Value.ToString());
            ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.Income.Clear();
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlHouseholdIncome.Items)
                if (item.CheckState == CheckState.Checked)
                    ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.Income.Add(item.Value.ToString());
            ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.Geographic.Clear();
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGeographicResidence.Items)
                if (item.CheckState == CheckState.Checked)
                    ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.Geographic.Add(item.Value.ToString());
            this.SettingsNotSaved = false;
        }

        public void LoadFromFile()
        {
            using (FormSavedTargetCustomers form = new FormSavedTargetCustomers())
            {
                if (form.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(form.SelectedFile))
                {
                    ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.Load(form.SelectedFile);
                    LoadSavedState();
                }
            }
        }

        private void TargetCustomersControl_Load(object sender, EventArgs e)
        {
            comboBoxEditSlideHeader.Properties.Items.Clear();
            comboBoxEditSlideHeader.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.TargetCustomersLists.Headers);

            checkedListBoxControlTargetDemo.Items.Clear();
            checkedListBoxControlTargetDemo.Items.AddRange(BusinessClasses.ListManager.Instance.TargetCustomersLists.Demos.ToArray());
            
            checkedListBoxControlHouseholdIncome.Items.Clear();
            checkedListBoxControlHouseholdIncome.Items.AddRange(BusinessClasses.ListManager.Instance.TargetCustomersLists.HHIs.ToArray());

            checkedListBoxControlGeographicResidence.Items.Clear();
            checkedListBoxControlGeographicResidence.Items.AddRange(BusinessClasses.ListManager.Instance.TargetCustomersLists.Geographies.ToArray());

            FormMain.Instance.FormClosed += new FormClosedEventHandler((sender1, e1) =>
            {
                if (this.SettingsNotSaved)
                {
                    SaveState();
                    ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.Save();
                }
            });

            LoadSavedState();
        }

        private void checkedListBoxControl_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (_allowToSave)
            {
                UpdateOutputState();
                this.SettingsNotSaved = true;
            }
        }

        private void comboBoxEditSlideHeader_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
                this.SettingsNotSaved = true;
        }
        #region Output Staff
        public string Title
        {
            get
            {
                return comboBoxEditSlideHeader.EditValue == null ? string.Empty : comboBoxEditSlideHeader.EditValue.ToString();
            }
        }

        public string TargetDemo
        {
            get
            {
                string result = string.Empty;
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlTargetDemo.CheckedItems)
                    result += ", " + item.Value;
                if (!string.IsNullOrEmpty(result))
                    result = result.Substring(2);
                return result;
            }
        }

        public string HHI
        {
            get
            {
                string result = string.Empty;
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlHouseholdIncome.CheckedItems)
                    result += ", " + item.Value;
                if (!string.IsNullOrEmpty(result))
                    result = result.Substring(2);
                return result;
            }
        }

        public string Geography
        {
            get
            {
                string result = string.Empty;
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGeographicResidence.CheckedItems)
                    result += ", " + item.Value;
                if (!string.IsNullOrEmpty(result))
                    result = result.Substring(2);
                return result;
            }
        }

        public void UpdateOutputState()
        {
            if(EnableOutput != null)
                EnableOutput(checkedListBoxControlGeographicResidence.CheckedItems.Count > 0 && checkedListBoxControlHouseholdIncome.CheckedItems.Count > 0 && checkedListBoxControlTargetDemo.CheckedItems.Count > 0);
        }

        public void UpdateSavedFilesState()
        {
            if (EnableSavedFiles != null)
                EnableSavedFiles(ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.AllowToLoad());
        }

        public void Output()
        {
            if (this.SettingsNotSaved)
            {
                SaveState();
                ConfigurationClasses.ViewSettingsManager.Instance.TargetCustomersState.Save();
                UpdateSavedFilesState();
            }
            InteropClasses.PowerPointHelper.Instance.AppendTargetCustomers();
        }
        #endregion
    }
}
