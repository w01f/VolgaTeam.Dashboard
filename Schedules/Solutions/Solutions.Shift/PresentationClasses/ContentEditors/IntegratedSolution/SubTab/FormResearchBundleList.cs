using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Shift.Configuration.IntegratedSolution;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraGrid.Views.Grid;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.SubTab
{
	public partial class FormResearchBundleList : MetroForm
	{
		private List<ResearchInfo.BundleListItem> _items;
		private ResearchInfo.BundleListItem _defaultItem;

		public ResearchInfo.BundleListItem SelectedItem => (gridView.GetFocusedRow() as ListItemModel)?.Parent;

		public FormResearchBundleList()
		{
			InitializeComponent();

			Height = (Int32)(Screen.PrimaryScreen.Bounds.Height * 0.8);
			Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
			Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);

			gridView.RowHeight = (Int32)(gridView.RowHeight * scaleFactor.Height);

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, scaleFactor);
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, scaleFactor);
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, scaleFactor);
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, scaleFactor);

			Shown += OnShown;
		}

		public void LoadData(List<ResearchInfo.BundleListItem> items, ResearchInfo.BundleListItem defaultItem)
		{
			_items = items;
			_defaultItem = defaultItem;


			gridControl.DataSource = _items
				.Select(ListItemModel.FromParent)
				.ToList();
		}

		private void OnShown(object sender, EventArgs e)
		{
			var selectedListItem = _items.FirstOrDefault(item => ResearchInfo.BundleListItem.Equals(item, _defaultItem));
			gridView.FocusedRowHandle = _items.IndexOf(selectedListItem);
		}

		private void OnRowClick(object sender, RowClickEventArgs e)
		{
			if (e.Clicks == 2)
				DialogResult = DialogResult.OK;
		}

		internal class ListItemModel
		{
			public string Text { get; set; }
			public ResearchInfo.BundleListItem Parent { get; set; }

			public static ListItemModel FromParent(ResearchInfo.BundleListItem parentItem)
			{
				return new ListItemModel
				{
					Parent = parentItem,
					Text = String.Format("{0}{1}{2}",
						String.Format("<p style=\"color: #000000;\">{0}</p>", parentItem.Value1),
						String.Format("<p style=\"color: #000000;\">{0}</p>", parentItem.Value2),
						String.Format("<p style=\"color: #000000;\">{0}</p>", parentItem.Value3))
				};
			}
		}
	}
}