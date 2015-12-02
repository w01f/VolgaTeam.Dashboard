using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace Asa.Bar.App.Forms
{
	public partial class FormPassword : MetroForm
	{
		private readonly string _password;

		public FormPassword(string password, string title)
		{
			InitializeComponent();

			if (!String.IsNullOrEmpty(title))
				Text = title;
			_password = password + "0000";
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void textBoxPin_TextChanged(object sender, EventArgs e)
		{
			if (textBoxPin.Text.Length == 4)
				DialogResult = textBoxPin.Text == _password.Substring(0, 4) ? DialogResult.OK : DialogResult.No;
		}
	}
}
