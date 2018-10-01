using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Shift.Configuration.Contract;
using Asa.Business.Solutions.Shift.Configuration.Contract.TabA;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Contract.TabA
{
	public partial class ProductSelectorControl : UserControl
	{
		private const int ButtonHeight = 60;
		private const int VerticalPadding = 40;
		private const int HorizontalButtonPadding = 80;
		private const int HorizontalBorderPadding = 40;
		private const int ColumnsCount = 3;

		public event EventHandler<ProductClickingEventArgs> ItemClicking;
		public event EventHandler<ProductClickedEventArgs> ItemClicked;

		public ProductSelectorControl()
		{
			InitializeComponent();
		}

		public void Init(IList<ProductInfo> itemInfoList)
		{
			ItemClicked = null;
			if (!itemInfoList.Any()) return;
			xtraScrollableControl.Controls.Clear();
			var columOrder = 0;
			var rowOrder = 0;
			foreach (var itemInfo in itemInfoList)
			{
				var button = new ProductItemButton(itemInfo);
				button.ColumnOrder = columOrder;
				button.RowOrder = rowOrder;
				button.Text = String.Format("<div align=\"center\"><font color=\"{1}\" size=\"{2}\">{0}</font></div>",
					itemInfo.Title?.Replace("&", "&amp;"),
					itemInfo.ButtonConfiguration.ForeColor.ToHex(),
					itemInfo.ButtonConfiguration.FontSize);
				if (!itemInfo.ButtonConfiguration.BackgroundColor.IsEmpty)
					button.BackColor = itemInfo.ButtonConfiguration.BackgroundColor;
				button.Height = (Int32)(ButtonHeight * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height);
				button.TextAlignment = eButtonTextAlignment.Center;
				button.ColorTable = eButtonColor.OrangeWithBackground;
				button.Style = eDotNetBarStyle.StyleManagerControlled;
				button.Click += (sender, e) =>
				{
					var clickedButton = (ProductItemButton)sender;
					var cllickingEventArgs = new ProductClickingEventArgs();
					ItemClicking?.Invoke(sender, cllickingEventArgs);
					if (!cllickingEventArgs.Cancel)
						ItemClicked?.Invoke(sender,
							new ProductClickedEventArgs
							{
								ItemInfo = clickedButton.ItemInfo
							});
				};
				xtraScrollableControl.Controls.Add(button);

				columOrder++;
				if (columOrder == ColumnsCount)
				{
					columOrder = 0;
					rowOrder++;
				}
			}
			ResizeButtons();
		}

		private void ResizeButtons()
		{
			xtraScrollableControl.SuspendLayout();
			var buttonWidth = (Width - HorizontalButtonPadding * (ColumnsCount - 1) - HorizontalBorderPadding) / ColumnsCount;
			foreach (var itemButton in xtraScrollableControl.Controls
				.OfType<ProductItemButton>()
				.OrderBy(b => b.RowOrder)
				.ThenBy(b => b.ColumnOrder)
				.ToList())
			{
				itemButton.Width = buttonWidth;
				var left = itemButton.ColumnOrder * (buttonWidth + HorizontalButtonPadding);
				var top = VerticalPadding + itemButton.RowOrder * ((Int32)(ButtonHeight * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height) + VerticalPadding);
				itemButton.Location = new Point(left, top);
			}
			xtraScrollableControl.ResumeLayout();
		}

		private void OnResize(object sender, EventArgs e)
		{
			ResizeButtons();
		}
	}
}
