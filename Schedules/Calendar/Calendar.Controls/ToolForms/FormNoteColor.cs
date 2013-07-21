using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalendarBuilder.ToolForms
{
    public partial class FormNoteColor : Form
    {
        public Color NoteColor { get; set; }
        public bool ApplyForAll { get; set; }

        public FormNoteColor()
        {
            InitializeComponent();
            this.NoteColor = Color.LemonChiffon;
            this.ApplyForAll = false;
        }

        private void FormNoteColor_Load(object sender, EventArgs e)
        {
            pnSelectedColor.BackColor = this.NoteColor;
        }

        private void pnSelectedColor_DoubleClick(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.FullOpen = true;
                colorDialog.AllowFullOpen = true;
                colorDialog.AnyColor = true;
                colorDialog.SolidColorOnly = false;
                colorDialog.ShowHelp = false;
                colorDialog.Color = this.NoteColor;
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.NoteColor = colorDialog.Color;
                    pnSelectedColor.BackColor = this.NoteColor;
                }
            }
        }

        private void checkBoxApplyForAll_CheckedChanged(object sender, EventArgs e)
        {
            this.ApplyForAll = checkBoxApplyForAll.Checked;
        }
    }
}
