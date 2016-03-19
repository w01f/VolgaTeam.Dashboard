using System;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;

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
				laLogo.Text = "Save a copy of this schedule:";
				checkEditSaveAsTemplate.Visible = true;
				Height = 210;
			}
			else
			{
				Text = "Build a New Schedule";
				laLogo.Text = "File Name:";
				checkEditSaveAsTemplate.Visible = false;
				Height = 190;
			}
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