using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses.DayProperties
{
    public partial class CommentControl : UserControl
    {
        private bool _alowToSave = false;
        private BusinessClasses.CalendarDay _day = null;

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler PropertiesChanged;

        public CommentControl()
        {
            InitializeComponent();

            memoEditComment1.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            memoEditComment1.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            memoEditComment1.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            memoEditComment2.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            memoEditComment2.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            memoEditComment2.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
        }

        public void LoadData(BusinessClasses.CalendarDay day)
        {
            _day = day;
            _alowToSave = false;
            buttonXComment1.Checked = !string.IsNullOrEmpty(_day.Comment1);
            memoEditComment1.EditValue = !string.IsNullOrEmpty(_day.Comment1) ? _day.Comment1 : null;

            buttonXComment2.Checked = !string.IsNullOrEmpty(_day.Comment2);
            memoEditComment2.EditValue = !string.IsNullOrEmpty(_day.Comment2) ? _day.Comment2 : null;
            _alowToSave = true;
        }

        public void SaveData()
        {
            if (_day != null)
            {
                _day.Comment1 = buttonXComment1.Checked && memoEditComment1.EditValue != null ? memoEditComment1.EditValue.ToString() : null;
                _day.Comment2 = buttonXComment2.Checked && memoEditComment2.EditValue != null ? memoEditComment2.EditValue.ToString() : null;
            }
        }

        private void checkEditUseComment1_CheckedChanged(object sender, System.EventArgs e)
        {
            memoEditComment1.Enabled = buttonXComment1.Checked;
            memoEditComment1.EditValue = buttonXComment1.Checked ? memoEditComment1.EditValue : null;
        }

        private void checkEditUseComment2_CheckedChanged(object sender, System.EventArgs e)
        {
            memoEditComment2.Enabled = buttonXComment2.Checked;
            memoEditComment2.EditValue = buttonXComment2.Checked ? memoEditComment2.EditValue : null;
        }

        private void memoEditComment_EditValueChanged(object sender, EventArgs e)
        {
            if (_alowToSave)
                if (this.PropertiesChanged != null)
                    this.PropertiesChanged(sender, e);
        }
    }
}
