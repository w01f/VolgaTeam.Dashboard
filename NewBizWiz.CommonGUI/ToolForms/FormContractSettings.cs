namespace Asa.CommonGUI.ToolForms
{
	public partial class FormContractSettings : DevComponents.DotNetBar.Metro.MetroForm
	{
		public FormContractSettings()
		{
			InitializeComponent();
		}

		private void checkEditShowRatesExpiration_CheckedChanged(object sender, System.EventArgs e)
		{
			dateEditRatesExpirationDate.Enabled = checkEditShowRatesExpiration.Checked;
			if (!checkEditShowRatesExpiration.Checked)
				dateEditRatesExpirationDate.EditValue = null;
		}
	}
}