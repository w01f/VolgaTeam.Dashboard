using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Enums;
using Asa.Common.Core.Helpers;
using Asa.Online.Controls.PresentationClasses.Products;
using DevComponents.DotNetBar.Metro;
using Asa.Online.Controls.Properties;
using DevExpress.Skins;

namespace Asa.Online.Controls.ToolForms
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

			simpleLabelItemTitle.Text = String.Format("<size=+2><i>{0}</i></size>", product.FullName);

			switch (_type)
			{
				case ProductInfoType.Targeting:
					Text = "Targeting Options";
					pictureEditLogo.Image = Resources.TargetButton;
					break;
				case ProductInfoType.RichMedia:
					Text = "Rich Media Options";
					pictureEditLogo.Image = Resources.RichMediaButton;
					break;
			}
			foreach (var group in _product.AddtionalInfo.Where(pi => pi.Type == type).Select(pi => pi.Group).Distinct())
				xtraTabControlGroups.TabPages.Add(new DigitalProductInfoGroup(_product.AddtionalInfo.Where(pi => pi.Type == type && pi.Group == group)) { Text = group });

			layoutControlItemLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
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
