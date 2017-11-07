using System;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;

namespace Asa.Common.GUI.ToolForms
{
	public partial class FormScheduleName : MetroForm
	{
		public FormScheduleName(bool saveForm = false)
		{
			InitializeComponent();
			if (saveForm)
			{
				Text = "Save Schedule";
				layoutControlItemScheduleName.Text = "<size=+2>Save a copy of this schedule:</size>";
				layoutControlItemSaveAsTemplate.Visibility = LayoutVisibility.Always;
				Height = (Int32)(210 * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height);
			}
			else
			{
				Text = "Build a New Schedule";
				layoutControlItemScheduleName.Text = "<size=+2>File Name:</size>";
				layoutControlItemSaveAsTemplate.Visibility = LayoutVisibility.Never;
				Height = (Int32)(190 * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height);
			}

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		public string ScheduleName
		{
			get
			{
				if (textEditScheduleName.EditValue != null)
					return textEditScheduleName.EditValue.ToString();
				return null;
			}
		}

		private void textEditScheduleName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter) return;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void FormNewSchedule_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (String.IsNullOrEmpty(ScheduleName))
			{
				PopupMessageHelper.Instance.ShowWarning("Schedule Name can't be empty");
				e.Cancel = true;
			}
		}
	}
}