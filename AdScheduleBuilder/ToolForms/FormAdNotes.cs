using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;


namespace AdScheduleBuilder.ToolForms
{
    public partial class FormAdNotes : Form
    {
        private int _selectedCommentRecordNumber = -1;
        private int _selectedSectionRecordNumber = -1;
        private int _selectedDeadlineRecordNumber = -1;
        private bool _useMultiLineComment = false;

        public DateTime Date { get; set; }

        public string CustomComment
        {
            get
            {
                return ckComment.Checked && textEditComment.EditValue != null ? textEditComment.EditValue.ToString() : string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    textEditComment.EditValue = value;
                    ckComment.Checked = true;
                }
                else
                {
                    textEditComment.EditValue = null;
                    ckComment.Checked = false;
                }
                UpdateCommentState();
            }
        }

        public BusinessClasses.NameCodePair[] Comments
        {
            get
            {
                List<BusinessClasses.NameCodePair> comments = new List<BusinessClasses.NameCodePair>();
                foreach (CheckedListBoxItem item in checkedListBoxControlComments.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        BusinessClasses.NameCodePair comment = new BusinessClasses.NameCodePair();
                        comment.Name = item.Description;
                        if (item.Value != null)
                            comment.Code = item.Value.ToString();
                        comments.Add(comment);
                    }
                }
                return comments.ToArray();
            }
            set
            {
                checkedListBoxControlComments.Items.Clear();
                _useMultiLineComment = BusinessClasses.ListManager.Instance.Notes.Where(x => !string.IsNullOrEmpty(x.Code)).Count() > 0;
                int i = 0;
                foreach (BusinessClasses.NameCodePair comment in BusinessClasses.ListManager.Instance.Notes)
                {
                    bool itemChecked = value.Where(x => x.Name.Equals(comment.Name) && x.Code.Equals(comment.Code)).Count() > 0;
                    checkedListBoxControlComments.Items.Add(comment.Code, comment.Name, itemChecked ? CheckState.Checked : CheckState.Unchecked, true);
                    if (itemChecked && _selectedCommentRecordNumber == -1)
                        _selectedCommentRecordNumber = i;
                    i++;
                }
                UpdateCommentState();
            }
        }

        public string Section
        {
            get
            {
                if (ckSection.Checked)
                    return textEditSection.Text;
                else
                    return checkedListBoxControlSections.CheckedIndices.Count > 0 ? (checkedListBoxControlSections.Items[checkedListBoxControlSections.CheckedIndices[0]] as CheckedListBoxItem).Description : string.Empty;
            }
            set
            {
                checkedListBoxControlSections.Items.Clear();
                foreach (BusinessClasses.Section comment in BusinessClasses.ListManager.Instance.Sections)
                    checkedListBoxControlSections.Items.Add(comment.Name, comment.Name, CheckState.Unchecked, true);

                textEditSection.Text = value;
                int i = 0;
                foreach (CheckedListBoxItem item in checkedListBoxControlSections.Items)
                {
                    if (item.Description.Equals(value))
                    {
                        item.CheckState = CheckState.Checked;
                        _selectedSectionRecordNumber = i;
                        textEditSection.Text = string.Empty;
                        ckSection.Checked = false;
                        break;
                    }
                    i++;
                }
            }
        }

        public string Deadline
        {
            get
            {
                if (ckDeadline.Checked)
                    return textEditDeadline.Text;
                else
                    return checkedListBoxControlDeadline.CheckedIndices.Count > 0 ? (checkedListBoxControlDeadline.Items[checkedListBoxControlDeadline.CheckedIndices[0]] as CheckedListBoxItem).Description : string.Empty;
            }
            set
            {
                checkedListBoxControlDeadline.Items.Clear();
                foreach (string deadline in BusinessClasses.ListManager.Instance.Deadlines)
                    checkedListBoxControlDeadline.Items.Add(deadline, deadline, CheckState.Unchecked, true);

                textEditDeadline.Text = value;
                int i = 0;
                foreach (CheckedListBoxItem item in checkedListBoxControlDeadline.Items)
                {
                    if (item.Description.Equals(value))
                    {
                        item.CheckState = CheckState.Checked;
                        _selectedDeadlineRecordNumber = i;
                        textEditComment.Text = string.Empty;
                        ckDeadline.Checked = false;
                        break;
                    }
                    i++;
                }
            }
        }

        public string Mechanicals
        {
            get
            {
                if (ckMechanicals.Checked)
                    return textEditMechanicals.Text;
                else
                    return null;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ckMechanicals.Checked = true;
                    textEditMechanicals.Text = value;
                }
            }
        }

        public FormAdNotes()
        {
            InitializeComponent();
            if (BusinessClasses.ListManager.Instance.Mechanicals.Count > 0)
            {
                xtraTabControlMechanicals.TabPages.Clear();
                foreach (var mechanicalType in BusinessClasses.ListManager.Instance.Mechanicals)
                {
                    CustomControls.MechanicalControl mechanicalPage = new CustomControls.MechanicalControl(mechanicalType);
                    xtraTabControlMechanicals.TabPages.Add(mechanicalPage);
                }
            }
            else
                tabItemMechanicals.Visible = false;
        }

        private void UpdateCommentState()
        {
            if (textEditComment.EditValue != null || checkedListBoxControlComments.CheckedItems.Count > 0)
                tabItemComments.Image = Properties.Resources.Selected;
            else
                tabItemComments.Image = Properties.Resources.Unselected;
        }

        private void UpdateSectionState()
        {
            if (!string.IsNullOrEmpty(textEditSection.Text) || checkedListBoxControlSections.CheckedItems.Count > 0)
                tabItemSections.Image = Properties.Resources.Selected;
            else
                tabItemSections.Image = Properties.Resources.Unselected;
        }

        private void UpdateMechanicalsState()
        {
            if (!string.IsNullOrEmpty(textEditMechanicals.Text))
                tabItemMechanicals.Image = Properties.Resources.Selected;
            else
                tabItemMechanicals.Image = Properties.Resources.Unselected;
        }

        private void UpdateDeadlineState()
        {
            if (!string.IsNullOrEmpty(textEditDeadline.Text) || checkedListBoxControlDeadline.CheckedItems.Count > 0)
                tabItemDeadlines.Image = Properties.Resources.Selected;
            else
                tabItemDeadlines.Image = Properties.Resources.Unselected;

            string deadline;
            if (ckDeadline.Checked)
                deadline = textEditDeadline.EditValue != null ? textEditDeadline.EditValue.ToString() : string.Empty;
            else
                deadline = checkedListBoxControlDeadline.CheckedIndices.Count > 0 ? CalculateDeadline((checkedListBoxControlDeadline.Items[checkedListBoxControlDeadline.CheckedIndices[0]] as CheckedListBoxItem).Description) : string.Empty;
            if (!string.IsNullOrEmpty(deadline))
                laDeadline.Text = "Deadline: " + deadline;
            else
                laDeadline.Text = "Deadline: Not Selected";
        }

        private string CalculateDeadline(string deadlineRange)
        {
            string result = string.Empty;
            Regex re = new Regex(@"\d+");
            Match m = re.Match(deadlineRange);
            if (m.Success)
            {
                int daysNumber = 0;
                if (int.TryParse(m.Value, out daysNumber))
                {
                    DateTime deadline = this.Date.AddDays(0 - daysNumber);
                    while (deadline.DayOfWeek == DayOfWeek.Saturday || deadline.DayOfWeek == DayOfWeek.Sunday)
                        deadline = deadline.AddDays(-1);
                    result = deadline.ToString("ddd, MM/dd/yy");
                }
            }
            return result;
        }

        private void ckComment_CheckedChanged(object sender, EventArgs e)
        {
            if (ckComment.Checked && !_useMultiLineComment)
                checkedListBoxControlComments.UnCheckAll();
            textEditComment.Enabled = ckComment.Checked;
            textEditComment.EditValue = ckComment.Checked ? textEditComment.EditValue : null;
        }

        private void textEditComment_EditValueChanged(object sender, EventArgs e)
        {
            UpdateCommentState();
        }

        private void checkedListBoxControlComments_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.State == CheckState.Checked && !_useMultiLineComment)
            {
                ckComment.Checked = false;
                foreach (CheckedListBoxItem item in checkedListBoxControlComments.Items)
                {
                    if (item != checkedListBoxControlComments.Items[e.Index] && item.CheckState == CheckState.Checked)
                    {
                        item.CheckState = CheckState.Unchecked;
                    }
                }
            }
            UpdateCommentState();
        }

        private void buttonXClearComment_Click(object sender, EventArgs e)
        {
            if (AppManager.ShowWarningQuestion("Are you sure you want to clear selected comment?") == System.Windows.Forms.DialogResult.Yes)
            {
                ckComment.Checked = false;
                textEditComment.EditValue = null;
                foreach (CheckedListBoxItem item in checkedListBoxControlComments.Items)
                    item.CheckState = CheckState.Unchecked;
            }
        }

        private void ckSection_CheckedChanged(object sender, EventArgs e)
        {
            if (ckSection.Checked)
                checkedListBoxControlSections.UnCheckAll();
            else
                textEditSection.EditValue = string.Empty;
            textEditSection.Enabled = ckSection.Checked;
        }

        private void textEditSection_EditValueChanged(object sender, EventArgs e)
        {
            UpdateSectionState();
        }

        private void checkedListBoxControlSections_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.State == CheckState.Checked)
            {
                ckSection.Checked = false;
                foreach (CheckedListBoxItem item in checkedListBoxControlSections.Items)
                {
                    if (item != checkedListBoxControlSections.Items[e.Index] && item.CheckState == CheckState.Checked)
                    {
                        item.CheckState = CheckState.Unchecked;
                    }
                }
            }
            UpdateSectionState();
        }

        private void buttonXClearSection_Click(object sender, EventArgs e)
        {
            if (AppManager.ShowWarningQuestion("Are you sure you want to clear selected section?") == System.Windows.Forms.DialogResult.Yes)
            {
                ckSection.Checked = true;
                textEditSection.EditValue = string.Empty;
            }
        }

        private void ckDeadline_CheckedChanged(object sender, EventArgs e)
        {
            if (ckDeadline.Checked)
                checkedListBoxControlDeadline.UnCheckAll();
            else
                textEditDeadline.EditValue = string.Empty;
            textEditDeadline.Enabled = ckDeadline.Checked;
        }

        private void textEditDeadline_EditValueChanged(object sender, EventArgs e)
        {
            UpdateDeadlineState();
        }

        private void checkedListBoxControlDeadline_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.State == CheckState.Checked)
            {
                ckDeadline.Checked = false;
                foreach (CheckedListBoxItem item in checkedListBoxControlDeadline.Items)
                {
                    if (item != checkedListBoxControlDeadline.Items[e.Index] && item.CheckState == CheckState.Checked)
                    {
                        item.CheckState = CheckState.Unchecked;
                    }
                }
            }
            UpdateDeadlineState();
        }

        private void buttonXClearDeadline_Click(object sender, EventArgs e)
        {
            if (AppManager.ShowWarningQuestion("Are you sure you want to clear selected deadline?") == System.Windows.Forms.DialogResult.Yes)
            {
                ckDeadline.Checked = true;
                textEditDeadline.EditValue = string.Empty;
            }
        }

        private void ckMechanicals_CheckedChanged(object sender, EventArgs e)
        {
            if (!ckMechanicals.Checked)
                textEditMechanicals.EditValue = string.Empty;
            textEditMechanicals.Enabled = ckMechanicals.Checked;
        }

        private void textEditMechanicals_EditValueChanged(object sender, EventArgs e)
        {
            UpdateMechanicalsState();
        }

        private void buttonXClearMechanicals_Click(object sender, EventArgs e)
        {
            if (AppManager.ShowWarningQuestion("Are you sure you want to clear mechanicals?") == System.Windows.Forms.DialogResult.Yes)
            {
                ckMechanicals.Checked = false;
                textEditMechanicals.EditValue = string.Empty;
            }
        }

        private void FormComment_Load(object sender, EventArgs e)
        {
            if (_selectedCommentRecordNumber != -1)
                checkedListBoxControlComments.MakeItemVisible(_selectedCommentRecordNumber);
            if (_selectedSectionRecordNumber != -1)
                checkedListBoxControlSections.MakeItemVisible(_selectedSectionRecordNumber);
            if (_selectedDeadlineRecordNumber != -1)
                checkedListBoxControlDeadline.MakeItemVisible(_selectedDeadlineRecordNumber);
        }

        private void tabControlAdNotes_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            FormComment_Load(null, null);
        }
    }
}