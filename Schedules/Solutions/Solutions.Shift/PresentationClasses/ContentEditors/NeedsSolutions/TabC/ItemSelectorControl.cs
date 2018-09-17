using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Shift.Configuration.NeedsSolutions;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.NeedsSolutions.TabC
{
	public partial class ItemSelectorControl : UserControl
	{
		private const int ButtonHeight = 90;
		private const int VerticalPadding = 40;
		private const int HorizontalButtonPadding = 80;
		private const int HorizontalBorderPadding = 40;
		private const int ColumnsCount = 3;

		private bool _allowToSave;
		private Int32 _maxCheckedItems;

		public event EventHandler<ItemChangedEventArgs> ItemStateChanged;

		public ItemSelectorControl()
		{
			InitializeComponent();
		}

		public void Init(IList<SolutionsItemInfo> itemInfoList, int maxCheckedItems)
		{
			_maxCheckedItems = maxCheckedItems;
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
				button.Text = String.Format("<div align=\"center\">{0}<br/><br/>{1}</div>",
					String.Format("<font color=\"{1}\" size=\"{2}\">{0}</font>",
						itemInfo.ButtonHeader?.Replace("&", "&amp;"),
						itemInfo.ButtonConfiguration.TopForeColor.ToHex(),
						itemInfo.ButtonConfiguration.TopFontSize),
					String.Format("<font color=\"{1}\" size=\"{2}\">{0}</font>",
						itemInfo.Title?.Replace("&", "&amp;"),
						itemInfo.ButtonConfiguration.BottomForeColor.ToHex(),
						itemInfo.ButtonConfiguration.BottomFontSize));
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
						if (alreadyCheckedButtonsCount < _maxCheckedItems)
							clickedButton.Checked = true;
						else
						{
							var maxCheckedItemsWord = String.Empty;
							switch (_maxCheckedItems)
							{
								case 1:
									maxCheckedItemsWord = "one";
									break;
								case 2:
									maxCheckedItemsWord = "two";
									break;
								case 3:
									maxCheckedItemsWord = "three";
									break;
								case 4:
									maxCheckedItemsWord = "four";
									break;
								case 5:
									maxCheckedItemsWord = "five";
									break;
								case 6:
									maxCheckedItemsWord = "six";
									break;
								case 7:
									maxCheckedItemsWord = "seven";
									break;
								case 8:
									maxCheckedItemsWord = "eight";
									break;
								case 9:
									maxCheckedItemsWord = "nine";
									break;
								case 10:
									maxCheckedItemsWord = "ten";
									break;

							}
							PopupMessageHelper.Instance.ShowWarning(String.Format("Only {0} ({1}) items are allowed.{2}{2}If you want to add another item, first remove one…", _maxCheckedItems, maxCheckedItemsWord, Environment.NewLine));
						}
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

					UpdateButtonsTooltips();
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

		public void LoadSavedState(IList<NeedsSolutionsState.SolutionsItemState> itemStateList)
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

			UpdateButtonsTooltips();

			_allowToSave = true;
		}

		private void UpdateButtonsTooltips()
		{
			var buttons = xtraScrollableControl.Controls.OfType<ItemButton>().ToList();
			if (buttons.Count(button => button.Checked) < _maxCheckedItems)
			{
				buttons
					.Where(button => !button.Checked)
					.ToList()
					.ForEach(button => button.Tooltip = "Add this one");
				buttons
					.Where(button => button.Checked)
					.ToList()
					.ForEach(button => button.Tooltip = "");
			}
			else
				buttons.ForEach(button => button.Tooltip = "");
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
