using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using Asa.Core.Common;

namespace Asa.CommonGUI.ToolForms
{
	public partial class FormNewSchedule : MetroForm
	{
		private readonly List<string> _busyNames = new List<string>();
		public FormNewSchedule(IEnumerable<string> busyNames)
		{
			InitializeComponent();
			_busyNames.AddRange(busyNames);
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
			if (_busyNames.All(name => name.ToLower() != ScheduleName.ToLower())) return;
			Utilities.Instance.ShowWarning(String.Format("A schedule already exists with this name.{0}Please save as a different file name", Environment.NewLine));
			e.Cancel = true;
		}
	}
}