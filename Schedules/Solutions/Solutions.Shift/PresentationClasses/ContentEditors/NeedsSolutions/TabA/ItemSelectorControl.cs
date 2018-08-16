using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Shift.Configuration.NeedsSolutions;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.NeedsSolutions.TabA
{
	public partial class ItemSelectorControl : UserControl
	{
		private const int ButtonHeight = 40;
		private const int VerticalPadding = 40;
		private const int HorizontalButtonPadding = 80;
		private const int HorizontalBorderPadding = 40;
		private const int ColumnsCount = 3;

		private bool _allowToSave;

		public event EventHandler<ItemChangedEventArgs> ItemStateChanged;

		public ItemSelectorControl()
		{
			InitializeComponent();
		}

		public void Init(IList<NeedsItemInfo> itemInfoList, int maxCheckedItems)
		{
			ItemStateChanged = null;
			if (!itemInfoList.Any()) return;
			xtraScrollableControl.Controls.Clear();
			var columOrder = 0;
			var rowOrder = 0;
			foreach (var itemInfo in itemInfoList)
			{
				var button = new ItemButton(itemInfo);
				button.ColumnOrder = columOrder;
				button.RowOrder = rowOrder;
				button.Text = itemInfo.Title;
				button.Height = (Int32)(ButtonHeight * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height);
				button.TextAlignment = eButtonTextAlignment.Center;
				button.ColorTable = eButtonColor.OrangeWithBackground;
				button.Style = eDotNetBarStyle.StyleManagerControlled;
				button.Click += (sender, e) =>
				{
					var clickedButton = (ItemButton)sender;
					if (clickedButton.Checked)
					{
						clickedButton.Checked = false;
					}
					else
					{
						var alreadyCheckedButtonsCount = xtraScrollableControl.Controls
							.OfType<ItemButton>()
							.Count(itemButton => itemButton.Checked);
						if (alreadyCheckedButtonsCount < maxCheckedItems)
							clickedButton.Checked = true;
					}
				};
				button.CheckedChanged += (sender, e) =>
				{
					if (!_allowToSave) return;
					var clickedButton = (ItemButton)sender;
					ItemStateChanged?.Invoke(sender,
						new ItemChangedEventArgs
						{
							Checked = clickedButton.Checked,
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

		public void LoadSavedState(IList<NeedsSolutionsState.NeedsItemState> itemStateList)
		{
			_allowToSave = false;

			foreach (var itemButton in xtraScrollableControl.Controls
				.OfType<ItemButton>()
				.ToList())
			{
				var itemState = itemStateList.FirstOrDefault(state =>
					String.Equals(state.Id, itemButton.ItemInfo.Id, StringComparison.OrdinalIgnoreCase));
				itemButton.Checked = itemState != null;
			}

			_allowToSave = true;
		}

		private void ResizeButtons()
		{
			xtraScrollableControl.SuspendLayout();
			var buttonWidth = (Width - HorizontalButtonPadding * (ColumnsCount - 1) - HorizontalBorderPadding) / ColumnsCount;
			foreach (var itemButton in xtraScrollableControl.Controls
				.OfType<ItemButton>()
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
