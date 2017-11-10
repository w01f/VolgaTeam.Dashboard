using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;

namespace Asa.Calendar.Controls.ToolForms
{
	public partial class FormNoteColor : MetroForm
	{
		public FormNoteColor()
		{
			InitializeComponent();
			NoteColor = Color.LemonChiffon;
			ApplyForAll = false;

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		public Color NoteColor { get; set; }
		public bool ApplyForAll { get; set; }

		private void OnFormLoad(object sender, EventArgs e)
		{
			pnSelectedColor.BackColor = NoteColor;
		}

		private void OnSelectedColorPanelDoubleClick(object sender, EventArgs e)
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

		private void OnApplyForAllCheckedChanged(object sender, EventArgs e)
		{
			ApplyForAll = checkEditApplyForAll.Checked;
		}
	}
}