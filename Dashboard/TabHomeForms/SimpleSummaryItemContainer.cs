using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Dashboard.Entities.NonPersistent;

namespace Asa.Dashboard.TabHomeForms
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
			_itemsCollection.Clear();
			pnMain.Controls.Clear();
			if (ViewSettingsManager.Instance.SimpleSummaryState.ItemsState.Count > 0)
			{
				foreach (var itemState in ViewSettingsManager.Instance.SimpleSummaryState.ItemsState)
				{
					var item = new SimpleSummaryItemControl(this);
					item.ItemNumber = _itemsCollection.Count() + 1;
					item.LoadSavedState(itemState);
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
				itemState.ShowDescription = item.ShowDescription;
				itemState.ShowMonthly = item.ShowMonthly;
				itemState.ShowTotal = item.ShowTotal;
				itemState.Value = item.ItemTitle;
				itemState.Description = item.ItemDetail;
				itemState.Monthly = item.MonthlyValue;
				itemState.Total = item.TotalValue;
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
			TabHomeMainPage.Instance.SlideSimpleSummary.UpdateTotalValues();
			TabHomeMainPage.Instance.SlideSimpleSummary.UpdateOutputState();
		}

		public void DeleteItem(int number)
		{
			var postion = number - 1;
			if (postion < 0 || postion >= _itemsCollection.Count) return;
			var item = _itemsCollection[postion];
			postion = postion - 1;
			if (postion >= 0 && postion < _itemsCollection.Count)
			{
				var previousItem = _itemsCollection[postion];
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
			TabHomeMainPage.Instance.SlideSimpleSummary.UpdateTotalValues();
			TabHomeMainPage.Instance.SlideSimpleSummary.UpdateOutputState();
		}

		public void UpItem(int number)
		{
			var postion = number - 1;
			if (postion < 1 || postion >= _itemsCollection.Count) return;
			var currentItem = _itemsCollection[postion];
			var upperItem = _itemsCollection[postion - 1];
			var ideaNumber = upperItem.ItemNumber;
			upperItem.ItemNumber = currentItem.ItemNumber;
			currentItem.ItemNumber = ideaNumber;
			_itemsCollection.Sort((x, y) => x.ItemNumber.CompareTo(y.ItemNumber));
			RefreshItems();
			pnMain.ScrollControlIntoView(currentItem);
			TabHomeMainPage.Instance.SlideSimpleSummary.SettingsNotSaved = true;
		}

		public void DownItem(int number)
		{
			var postion = number - 1;
			if (postion < 0 || postion >= _itemsCollection.Count - 1) return;
			var currentItem = _itemsCollection[postion];
			var lowerItem = _itemsCollection[postion + 1];
			var ideaNumber = lowerItem.ItemNumber;
			lowerItem.ItemNumber = currentItem.ItemNumber;
			currentItem.ItemNumber = ideaNumber;
			_itemsCollection.Sort((x, y) => x.ItemNumber.CompareTo(y.ItemNumber));
			RefreshItems();
			pnMain.ScrollControlIntoView(currentItem);
			TabHomeMainPage.Instance.SlideSimpleSummary.SettingsNotSaved = true;
		}

		private void UpdateItemsNumbers()
		{
			var postion = 1;
			foreach (var item in _itemsCollection)
			{
				item.ItemNumber = postion;
				postion++;
			}
		}

		public void RefreshItems()
		{
			var index = _itemsCollection.Count - 1;
			foreach (var item in _itemsCollection)
			{
				pnMain.Controls.SetChildIndex(item, index);
				index--;
			}
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
						result.Add(item.ItemTitle);
				return result.ToArray();
			}
		}

		public string[] ItemDetails
		{
			get
			{
				var result = new List<string>();
				foreach (var item in _itemsCollection)
					if (!string.IsNullOrEmpty(item.ItemTitle))
						result.Add(item.ItemDetail.Replace(Environment.NewLine, "; "));
				return result.ToArray();
			}
		}

		public string[] OutputMonthlyValues
		{
			get
			{
				var result = new List<string>();
				foreach (var item in _itemsCollection)
					if (!string.IsNullOrEmpty(item.ItemTitle))
						result.Add(item.MonthlyValue.HasValue && item.MonthlyValue.Value > 0 ? item.MonthlyValue.Value.ToString("$#,##0.00") : string.Empty);
				return result.ToArray();
			}
		}

		public string[] OutputTotalValues
		{
			get
			{
				var result = new List<string>();
				foreach (var item in _itemsCollection)
					if (!string.IsNullOrEmpty(item.ItemTitle))
						result.Add(item.TotalValue.HasValue && item.TotalValue.Value > 0 ? item.TotalValue.Value.ToString("$#,##0.00") : string.Empty);
				return result.ToArray();
			}
		}

		public bool ShowMonthlyTotal
		{
			get { return _itemsCollection.Any(x => x.MonthlyValue.HasValue && x.MonthlyValue.Value > 0); }
		}

		public decimal? TotalMonthlyValue
		{
			get { return _itemsCollection.Sum(x => x.MonthlyValue); }
		}

		public bool ShowTotalTotal
		{
			get { return _itemsCollection.Any(x => x.TotalValue.HasValue && x.TotalValue.Value > 0); }
		}

		public decimal? TotalTotalValue
		{
			get { return _itemsCollection.Sum(x => x.TotalValue); }
		}
		#endregion
	}
}