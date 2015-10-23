using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using Asa.Core.OnlineSchedule;
using Asa.OnlineSchedule.Controls.PresentationClasses;
using Asa.OnlineSchedule.Controls.Properties;

namespace Asa.OnlineSchedule.Controls.ToolForms
{
	public partial class FormProductInfo : MetroForm
	{
		private readonly ProductInfoType _type;
		private readonly DigitalProduct _product;

		public FormProductInfo(ProductInfoType type, DigitalProduct product)
		{
			InitializeComponent();
			_type = type;
			_product = product;
			labelControlTitle.Text = product.FullName;
			switch (_type)
			{
				case ProductInfoType.Targeting:
					Text = "Targeting Options";
					pictureBoxInfoType.Image = Resources.TargetButton;
					break;
				case ProductInfoType.RichMedia:
					Text = "Rich Media Options";
					pictureBoxInfoType.Image = Resources.RichMediaButton;
					break;
			}
			foreach (var group in _product.AddtionalInfo.Where(pi => pi.Type == type).Select(pi => pi.Group).Distinct())
				xtraTabControlGroups.TabPages.Add(new DigitalProductInfoGroup(_product.AddtionalInfo.Where(pi => pi.Type == type && pi.Group == group)) { Text = group });
		}
		private void FormProductInfo_FormClosed(object sender, FormClosedEventArgs e)
		{
			var productInfoGroups = xtraTabControlGroups.TabPages.OfType<DigitalProductInfoGroup>();
			foreach (var productInfoGroup in productInfoGroups)
				productInfoGroup.CloseEditors();
			if (DialogResult != DialogResult.OK) return;
			_product.AddtionalInfo.RemoveAll(pi => pi.Type == _type);
			_product.AddtionalInfo.AddRange(productInfoGroups.SelectMany(pg => pg.DataSourse));
		}
	}
}
