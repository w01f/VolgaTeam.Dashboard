using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace NewBizWiz.Calendar.Controls.ToolForms
{
	public partial class FormNoteColor : MetroForm
	{
		public FormNoteColor()
		{
			InitializeComponent();
			NoteColor = Color.LemonChiffon;
			ApplyForAll = false;
		}

		public Color NoteColor { get; set; }
		public bool ApplyForAll { get; set; }

		private void FormNoteColor_Load(object sender, EventArgs e)
		{
			pnSelectedColor.BackColor = NoteColor;
		}

		private void pnSelectedColor_DoubleClick(object sender, EventArgs e)
		{
			using (var colorDialog = new ColorDialog())
			{
				colorDialog.FullOpen = true;
				colorDialog.AllowFullOpen = true;
				colorDialog.AnyColor = true;
				colorDialog.SolidColorOnly = false;
				colorDialog.ShowHelp = false;
				colorDialog.Color = NoteColor;
				if (colorDialog.ShowDialog() == DialogResult.OK)
				{
					NoteColor = colorDialog.Color;
					pnSelectedColor.BackColor = NoteColor;
				}
			}
		}

		private void checkBoxApplyForAll_CheckedChanged(object sender, EventArgs e)
		{
			ApplyForAll = checkBoxApplyForAll.Checked;
		}
	}
}