using System.Collections.Generic;
using System.Windows.Forms;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	public partial class SimpleSummaryOutputContainer : UserControl
	{
		private readonly List<SimpleSummaryOutputControl> _itemsCollection = new List<SimpleSummaryOutputControl>();

		public SimpleSummaryOutputContainer()
		{
			InitializeComponent();
			AppManager.Instance.SetClickEventHandler(this);
		}

		public void AddItem(SimpleSummaryOutputControl item)
		{
			_itemsCollection.Add(item);
			pnMain.Controls.Add(item);
			RefreshItems();
		}

		public void DeleteItem(SimpleSummaryOutputControl item)
		{
			pnMain.Controls.Remove(item);
			_itemsCollection.Remove(item);
			_itemsCollection.Sort((x, y) => x.ItemNumber.CompareTo(y.ItemNumber));
			RefreshItems();
		}

		public void UpItem(int number)
		{
			int postion = number - 1;
			if (postion >= 1 && postion < _itemsCollection.Count)
			{
				SimpleSummaryOutputControl currentItem = _itemsCollection[postion];
				SimpleSummaryOutputControl upperItem = _itemsCollection[postion - 1];
				int ideaNumber = upperItem.ItemNumber;
				upperItem.ItemNumber = currentItem.ItemNumber;
				currentItem.ItemNumber = ideaNumber;
				_itemsCollection.Sort((x, y) => x.ItemNumber.CompareTo(y.ItemNumber));
				RefreshItems();
			}
		}

		public void DownItem(int number)
		{
			int postion = number - 1;
			if (postion >= 0 && postion < _itemsCollection.Count - 1)
			{
				SimpleSummaryOutputControl currentItem = _itemsCollection[postion];
				SimpleSummaryOutputControl lowerItem = _itemsCollection[postion + 1];
				int ideaNumber = lowerItem.ItemNumber;
				lowerItem.ItemNumber = currentItem.ItemNumber;
				currentItem.ItemNumber = ideaNumber;
				_itemsCollection.Sort((x, y) => x.ItemNumber.CompareTo(y.ItemNumber));
				RefreshItems();
			}
		}

		private void RefreshItems()
		{
			int index = _itemsCollection.Count - 1;
			foreach (SimpleSummaryOutputControl item in _itemsCollection)
			{
				pnMain.Controls.SetChildIndex(item, index);
				index--;
			}
		}
	}
}