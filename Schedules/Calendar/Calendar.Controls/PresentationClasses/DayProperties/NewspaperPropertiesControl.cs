using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses.DayProperties
{
    public partial class NewspaperPropertiesControl : UserControl
    {
        private bool _alowToSave = false;
        private BusinessClasses.CalendarDay _day = null;

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler PropertiesChanged;

        public NewspaperPropertiesControl()
        {
            InitializeComponent();

            memoEditCustomNote.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            memoEditCustomNote.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            memoEditCustomNote.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            comboBoxEditQuickList.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            comboBoxEditQuickList.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            comboBoxEditQuickList.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            comboBoxEditSection.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            comboBoxEditSection.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            comboBoxEditSection.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            comboBoxEditPageSize.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            comboBoxEditPageSize.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            comboBoxEditPageSize.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditTotalCost.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditTotalCost.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditTotalCost.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
        }

        public void LoadData(BusinessClasses.CalendarDay day)
        {
            _day = day;
            _alowToSave = false;

            buttonXCustomNote.Checked = !string.IsNullOrEmpty(_day.Newspaper.CustomNote);
            memoEditCustomNote.EditValue = !string.IsNullOrEmpty(_day.Newspaper.CustomNote) ? _day.Newspaper.CustomNote : null;

            comboBoxEditQuickList.Properties.Items.Clear();
            comboBoxEditQuickList.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.PrintQuickList);
            buttonXQuickList.Enabled = comboBoxEditQuickList.Properties.Items.Count > 0;
            buttonXQuickList.Checked = !string.IsNullOrEmpty(_day.Newspaper.QuickListRecord) & comboBoxEditQuickList.Properties.Items.Count > 0;
            comboBoxEditQuickList.EditValue = !string.IsNullOrEmpty(_day.Newspaper.QuickListRecord) & comboBoxEditQuickList.Properties.Items.Count > 0 ? _day.Newspaper.QuickListRecord : null;

            comboBoxEditPublication.Properties.Items.Clear();
            comboBoxEditPublication.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.PrintSources.Select(x => x.Name).Distinct().ToArray());
            buttonXPublication.Checked = !string.IsNullOrEmpty(_day.Newspaper.PublicationName);
            comboBoxEditPublication.EditValue = !string.IsNullOrEmpty(_day.Newspaper.PublicationName) ? _day.Newspaper.PublicationName : null;
            labelControlPublicationAbbreviation.Text = !string.IsNullOrEmpty(_day.Newspaper.PublicationName) ? _day.Newspaper.PublicationAbbreviation : string.Empty;

            comboBoxEditSection.Properties.Items.Clear();
            comboBoxEditSection.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.PrintSections.Select(x => x.Name).ToArray());
            checkEditSection.Checked = !string.IsNullOrEmpty(_day.Newspaper.Section);
            comboBoxEditSection.EditValue = !string.IsNullOrEmpty(_day.Newspaper.Section) ? _day.Newspaper.Section : null;

            comboBoxEditPageSize.Properties.Items.Clear();
            comboBoxEditPageSize.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.PrintPageSizes);
            checkEditPageSize.Checked = !string.IsNullOrEmpty(_day.Newspaper.PageSize);
            comboBoxEditPageSize.EditValue = !string.IsNullOrEmpty(_day.Newspaper.PageSize) ? _day.Newspaper.PageSize : null;

            checkEditColor.Checked = !string.IsNullOrEmpty(_day.Newspaper.Color);
            comboBoxEditColor.EditValue = !string.IsNullOrEmpty(_day.Newspaper.Color) ? _day.Newspaper.Color : null;

            checkEditTotalCost.Checked = _day.Newspaper.TotalCost.HasValue;
            spinEditTotalCost.Value = _day.Newspaper.TotalCost.HasValue ? (decimal)_day.Newspaper.TotalCost.Value : 0;

            buttonXAdInfo.Checked = checkEditSection.Checked | checkEditPageSize.Checked | checkEditColor.Checked | checkEditTotalCost.Checked;

            _alowToSave = true;
        }

        public void SaveData()
        {
            if (_day != null)
            {
                _day.Newspaper.CustomNote = buttonXCustomNote.Checked && memoEditCustomNote.EditValue != null ? memoEditCustomNote.EditValue.ToString() : null;
                _day.Newspaper.QuickListRecord = (buttonXQuickList.Checked && comboBoxEditQuickList.EditValue != null) ? comboBoxEditQuickList.EditValue.ToString() : null;
                _day.Newspaper.PublicationName = buttonXPublication.Checked && comboBoxEditPublication.EditValue != null ? comboBoxEditPublication.EditValue.ToString() : null;
                _day.Newspaper.Section = checkEditSection.Checked && comboBoxEditSection.EditValue != null ? comboBoxEditSection.EditValue.ToString() : null;
                _day.Newspaper.PageSize = checkEditPageSize.Checked && comboBoxEditPageSize.EditValue != null ? comboBoxEditPageSize.EditValue.ToString() : null;
                _day.Newspaper.Color = checkEditColor.Checked && comboBoxEditColor.EditValue != null ? comboBoxEditColor.EditValue.ToString() : null;
                _day.Newspaper.TotalCost = checkEditTotalCost.Checked && spinEditTotalCost.Value != 0 ? (double?)spinEditTotalCost.Value : null;
            }
        }

        private void checkEditUseCustomNote_CheckedChanged(object sender, System.EventArgs e)
        {
            memoEditCustomNote.Enabled = buttonXCustomNote.Checked;
            memoEditCustomNote.EditValue = buttonXCustomNote.Checked ? memoEditCustomNote.EditValue : null;
        }

        private void Editor_EditValueChanged(object sender, EventArgs e)
        {
            if (_alowToSave)
                if (this.PropertiesChanged != null)
                    this.PropertiesChanged(sender, e);
        }

        private void checkEditQuickList_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditQuickList.Enabled = buttonXQuickList.Checked;
            comboBoxEditQuickList.EditValue = buttonXQuickList.Checked ? comboBoxEditQuickList.EditValue : null;
        }

        private void checkEditPublication_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditPublication.Enabled = buttonXPublication.Checked;
            comboBoxEditPublication.EditValue = buttonXPublication.Checked ? comboBoxEditPublication.EditValue : null;
            labelControlPublicationAbbreviation.Text = buttonXPublication.Checked ? labelControlPublicationAbbreviation.Text : string.Empty;
        }

        private void buttonXAdInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (!buttonXAdInfo.Checked)
            {
                checkEditSection.Checked = false;
                checkEditPageSize.Checked = false;
                checkEditColor.Checked = false;
                checkEditTotalCost.Checked = false;
                checkEditSection.Enabled = false;
                checkEditPageSize.Enabled = false;
                checkEditColor.Enabled = false;
                checkEditTotalCost.Enabled = false;
            }
            else
            {
                checkEditSection.Enabled = true;
                checkEditPageSize.Enabled = true;
                checkEditColor.Enabled = true;
                checkEditTotalCost.Enabled = true;
            }
        }

        private void checkEditSection_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditSection.Enabled = checkEditSection.Checked;
            comboBoxEditSection.EditValue = checkEditSection.Checked ? comboBoxEditSection.EditValue : null;
        }

        private void checkEditPageSize_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditPageSize.Enabled = checkEditPageSize.Checked;
            comboBoxEditPageSize.EditValue = checkEditPageSize.Checked ? comboBoxEditPageSize.EditValue : null;
        }

        private void checkEditColor_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditColor.Enabled = checkEditColor.Checked;
            comboBoxEditColor.EditValue = checkEditColor.Checked ? comboBoxEditColor.EditValue : null;
        }

        private void checkEditTotalCost_CheckedChanged(object sender, EventArgs e)
        {
            spinEditTotalCost.Enabled = checkEditTotalCost.Checked;
            spinEditTotalCost.Value = checkEditTotalCost.Checked ? spinEditTotalCost.Value : 0;
        }

        private void comboBoxEditPublication_EditValueChanged(object sender, EventArgs e)
        {
            if (_alowToSave)
            {
                BusinessClasses.PrintSource printSource = comboBoxEditPublication.EditValue != null ? BusinessClasses.ListManager.Instance.PrintSources.Where(x => x.Name.Equals(comboBoxEditPublication.EditValue.ToString())).FirstOrDefault() : null;
                if (printSource != null)
                    labelControlPublicationAbbreviation.Text = printSource.Abbreviation.ToUpper();
                else
                    labelControlPublicationAbbreviation.Text = string.Empty;
                Editor_EditValueChanged(sender, e);
            }
        }
    }
}
