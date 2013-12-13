using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.Core.Dashboard;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	public partial class SimpleSummaryItemContainer : UserControl
	{
		private readonly List<SimpleSummaryItemControl> _itemsCollection = new List<SimpleSummaryItemControl>();
		private int _selectedIndex = -1;

		public SimpleSummaryItemContainer()
		{
			InitializeComponent();
			AppManager.Instance.SetClickEventHandler(this);
		}

		public SimpleSummaryOutputContainer OutputContainer { get; set; }

		public int SelectedIndex
		{
			get { return _selectedIndex; }
			set
			{
				if (value >= 0 && value < _itemsCollection.Count)
				{
					_selectedIndex = value;
					_itemsCollection[_selectedIndex].Focus();
					pnMain.ScrollControlIntoView(_itemsCollection[_selectedIndex]);
				}
				else
					_selectedIndex = -1;
			}
		}

		public bool ItemsComplited
		{
			get { return _itemsCollection.Count(x => x.Complited) == _itemsCollection.Count; }
		}

		public void LoadItems()
		{
			foreach (SimpleSummaryItemControl item in _itemsCollection)
				OutputContainer.DeleteItem(item.OutputItem);
			_itemsCollection.Clear();
			pnMain.Controls.Clear();
			if (ViewSettingsManager.Instance.SimpleSummaryState.ItemsState.Count > 0)
			{
				foreach (SimpleSummaryItemState itemState in ViewSettingsManager.Instance.SimpleSummaryState.ItemsState)
				{
					var item = new SimpleSummaryItemControl(this);
					item.ItemNumber = _itemsCollection.Count() + 1;
					item.LoadSavedState();
					_itemsCollection.Add(item);
					pnMain.Controls.Add(item);
				}
			}
			else
			{
				var firstItem = new SimpleSummaryItemControl(this);
				firstItem.ItemNumber = _itemsCollection.Count() + 1;
				_itemsCollection.Add(firstItem);
				pnMain.Controls.Add(firstItem);
				var secondItem = new SimpleSummaryItemControl(this);
				secondItem.ItemNumber = _itemsCollection.Count() + 1;
				_itemsCollection.Add(secondItem);
				pnMain.Controls.Add(secondItem);
			}
			RefreshItems();
			pnMain.ScrollControlIntoView(_itemsCollection[0]);
		}

		public void SaveItems()
		{
			ViewSettingsManager.Instance.SimpleSummaryState.ItemsState.Clear();
			foreach (var item in _itemsCollection)
			{
				var itemState = new SimpleSummaryItemState();
				itemState.Order = item.ItemNumber;
				itemState.ShowValue = item.ShowValue;
				itemState.ShowDescription = item.ShowDescriptionOutput;
				itemState.ShowMonthly = item.ShowMonthlyOutput;
				itemState.ShowTotal = item.ShowTotalOutput;
				itemState.Value = item.ItemTitle;
				itemState.Description = item.ItemDetailOutput;
				itemState.Monthly = item.MonthlyValue.HasValue ? item.MonthlyValue.Value : 0;
				itemState.Total = item.TotalValue.HasValue ? item.TotalValue.Value : 0;
				ViewSettingsManager.Instance.SimpleSummaryState.ItemsState.Add(itemState);
			}
			ViewSettingsManager.Instance.SimpleSummaryState.ItemsState.Sort((x, y) => x.Order.CompareTo(y.Order));
		}

		public void AddItem()
		{
			var item = new SimpleSummaryItemControl(this);
			item.ItemNumber = _itemsCollection.Count() + 1;
			_itemsCollection.Add(item);
			pnMain.Controls.Add(item);
			RefreshItems();
			pnMain.ScrollControlIntoView(item);
		}

		public void DeleteItem(int number)
		{
			int postion = number - 1;
			if (postion >= 0 && postion < _itemsCollection.Count)
			{
				SimpleSummaryItemControl item = _itemsCollection[postion];
				postion = postion - 1;
				if (postion >= 0 && postion < _itemsCollection.Count)
				{
					SimpleSummaryItemControl previousItem = _itemsCollection[postion];
					previousItem.Focus();
					pnMain.ScrollControlIntoView(previousItem);
				}
				pnMain.Controls.Remove(item);
				_itemsCollection.Remove(item);
				_itemsCollection.Sort((x, y) => x.ItemNumber.CompareTo(y.ItemNumber));
				item.Dispose();
				RefreshItems();

				UpdateItemsNumbers();
				TabHomeMainPage.Instance.SlideSimpleSummary.buttonXAddItem.Enabled = true;
				pnMain.Focus();
				TabHomeMainPage.Instance.SlideSimpleSummary.UpdateTotalItems();

				TabHomeMainPage.Instance.SlideSimpleSummary.SettingsNotSaved = true;
			}
		}

		public void UpItem(int number)
		{
			int postion = number - 1;
			if (postion >= 1 && postion < _itemsCollection.Count)
			{
				SimpleSummaryItemControl currentItem = _itemsCollection[postion];
				SimpleSummaryItemControl upperItem = _itemsCollection[postion - 1];
				int ideaNumber = upperItem.ItemNumber;
				upperItem.ItemNumber = currentItem.ItemNumber;
				currentItem.ItemNumber = ideaNumber;
				_itemsCollection.Sort((x, y) => x.ItemNumber.CompareTo(y.ItemNumber));
				RefreshItems();
				pnMain.ScrollControlIntoView(currentItem);
				TabHomeMainPage.Instance.SlideSimpleSummary.SettingsNotSaved = true;
			}
		}

		public void DownItem(int number)
		{
			int postion = number - 1;
			if (postion >= 0 && postion < _itemsCollection.Count - 1)
			{
				SimpleSummaryItemControl currentItem = _itemsCollection[postion];
				SimpleSummaryItemControl lowerItem = _itemsCollection[postion + 1];
				int ideaNumber = lowerItem.ItemNumber;
				lowerItem.ItemNumber = currentItem.ItemNumber;
				currentItem.ItemNumber = ideaNumber;
				_itemsCollection.Sort((x, y) => x.ItemNumber.CompareTo(y.ItemNumber));
				RefreshItems();
				pnMain.ScrollControlIntoView(currentItem);
				TabHomeMainPage.Instance.SlideSimpleSummary.SettingsNotSaved = true;
			}
		}

		private void UpdateItemsNumbers()
		{
			int postion = 1;
			foreach (SimpleSummaryItemControl item in _itemsCollection)
			{
				item.ItemNumber = postion;
				postion++;
			}
		}

		public void RefreshItems()
		{
			int index = _itemsCollection.Count - 1;
			foreach (SimpleSummaryItemControl item in _itemsCollection)
			{
				pnMain.Controls.SetChildIndex(item, index);
				index--;
			}
		}

		public void HideTotals()
		{
			foreach (SimpleSummaryItemControl item in _itemsCollection)
			{
				item.OutputItem.pnMonthly.Visible = TotalMonthlyValue.HasValue;
				item.OutputItem.pnTotal.Visible = TotalTotalValue.HasValue;
			}
		}

		public void HideDescription()
		{
			foreach (SimpleSummaryItemControl item in _itemsCollection)
				item.OutputItem.Height = string.IsNullOrEmpty(item.ItemDetail) ? item.OutputItem.pnHeader.Height : (item.OutputItem.pnHeader.Height + item.OutputItem.pnDetails.Height);
		}

		#region Output Staff
		public int ItemsCount
		{
			get { return _itemsCollection.Count; }
		}

		public string[] ItemTitles
		{
			get
			{
				var result = new List<string>();
				foreach (var item in _itemsCollection)
					if (!string.IsNullOrEmpty(item.ItemTitle))
						result.Add(item.OutputItemTitle);
				return result.ToArray();
			}
		}

		public string[] ItemDetails
		{
			get
			{
				var result = new List<string>();
				foreach (SimpleSummaryItemControl item in _itemsCollection)
					if (!string.IsNullOrEmpty(item.ItemTitle))
						result.Add(item.ItemDetailOutput.Replace(Environment.NewLine, "; "));
				return result.ToArray();
			}
		}

		public string[] OutputMonthlyValues
		{
			get
			{
				var result = new List<string>();
				foreach (SimpleSummaryItemControl item in _itemsCollection)
					if (!string.IsNullOrEmpty(item.ItemTitle))
						result.Add(item.OutputMonthlyValue.HasValue ? item.OutputMonthlyValue.Value.ToString("$#,##0.00") : string.Empty);
				return result.ToArray();
			}
		}

		public string[] OutputTotalValues
		{
			get
			{
				var result = new List<string>();
				foreach (SimpleSummaryItemControl item in _itemsCollection)
					if (!string.IsNullOrEmpty(item.ItemTitle))
						result.Add(item.OutputTotalValue.HasValue ? item.OutputTotalValue.Value.ToString("$#,##0.00") : string.Empty);
				return result.ToArray();
			}
		}

		public bool ShowMonthlyTotal
		{
			get { return _itemsCollection.Where(x => x.MonthlyValue.HasValue).Count() > 0; }
		}

		public double? TotalMonthlyValue
		{
			get { return _itemsCollection.Where(x => x.ShowMonthly).Count() > 0 ? (_itemsCollection.Sum(x => !string.IsNullOrEmpty(x.ItemTitle) && x.ShowMonthly ? x.MonthlyValue : null)) : null; }
		}

		public bool ShowTotalTotal
		{
			get { return _itemsCollection.Where(x => x.TotalValue.HasValue).Count() > 0; }
		}

		public double? TotalTotalValue
		{
			get { return _itemsCollection.Where(x => x.ShowTotal).Count() > 0 ? (_itemsCollection.Sum(x => !string.IsNullOrEmpty(x.ItemTitle) && x.ShowTotal ? x.TotalValue : null)) : null; }
		}

		public double OutputTotalMonthlyValue
		{
			get { return _itemsCollection.Sum(x => !string.IsNullOrEmpty(x.OutputItemTitle) && x.OutputMonthlyValue.HasValue ? x.OutputMonthlyValue.Value : 0); }
		}

		public double OutputTotalTotalValue
		{
			get { return _itemsCollection.Sum(x => !string.IsNullOrEmpty(x.OutputItemTitle) && x.OutputTotalValue.HasValue ? x.OutputTotalValue.Value : 0); }
		}
		#endregion
	}
}