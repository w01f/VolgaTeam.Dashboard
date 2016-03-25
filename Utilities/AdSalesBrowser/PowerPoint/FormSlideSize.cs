using System.Windows.Forms;
using Asa.Common.GUI.Common;
using DevComponents.DotNetBar.Metro;

namespace AdSalesBrowser.PowerPoint
{
	public partial class FormSlideSize : MetroForm
	{
		public FormSlideSize()
		{
			InitializeComponent();
			pb169.Buttonize();
			pb43.Buttonize();
			pb34.Buttonize();
		}

		private void OnSize169Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}

		private void OnSize43Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.No;
		}

		private void OnSize34Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Retry;
		}
	}
}
