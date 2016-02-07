using System;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls
{
	public partial class FormSnapshotName : MetroForm
	{
		public FormSnapshotName()
		{
			InitializeComponent();
		}

		public string SnapshotName
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
			PopupMessageHelper.Instance.ShowWarning("You should set Snapshot name before continue");
		}
	}
}