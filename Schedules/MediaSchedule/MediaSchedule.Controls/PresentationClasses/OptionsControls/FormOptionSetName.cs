using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using NewBizWiz.Core.Common;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.OptionsControls
{
	public partial class FormOptionSetName : MetroForm
	{
		public FormOptionSetName()
		{
			InitializeComponent();
		}

		public string OptionSetName
		{
			get
			{
				if (textEditScheduleName.EditValue != null)
					return textEditScheduleName.EditValue.ToString();
				return null;
			}
			set
			{
				textEditScheduleName.EditValue = value;
			}
		}

		private void textEditScheduleName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter) return;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void FormSnapshotName_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (!String.IsNullOrEmpty(textEditScheduleName.EditValue as String)) return;
			e.Cancel = true;
			Utilities.Instance.ShowWarning("You should set Schedule name before continue");
		}
	}
}