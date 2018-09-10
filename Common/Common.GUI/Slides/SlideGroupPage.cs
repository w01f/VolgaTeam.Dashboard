using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Objects.Slides;
using DevExpress.Utils;
using DevExpress.XtraTab;
using Manina.Windows.Forms;

namespace Asa.Common.GUI.Slides
{
	//public partial class SlideGroupPage : UserControl
	public partial class SlideGroupPage : XtraTabPage
	{
		private readonly List<SlideMaster> _slideMasters = new List<SlideMaster>();
		private ImageListView.HitInfo _menuHitInfo;

		public event EventHandler<SlideMasterEventArgs> SlideOutput;
		public event EventHandler<EventArgs> SelectionChanged;

		public string SlideGroupName { get; }

		public SlideMaster SelectedSlide
		{
			get
			{
				return slidesListView.SelectedItems.Count > 0 ?
					slidesListView.SelectedItems.Select(item => item.Tag as SlideMaster).FirstOrDefault() :
					null;
			}
		}

		public SlideGroupPage(string groupName, IList<SlideMaster> slides, Size thumbnailSize) : this(groupName, slides)
		{
			if (!thumbnailSize.IsEmpty)
			{
				var persentWidth = thumbnailSize.Width > 0 ? (float)thumbnailSize.Width / slidesListView.ThumbnailSize.Width : 1;
				var persentHeight = thumbnailSize.Height > 0
					? (float)thumbnailSize.Height / slidesListView.ThumbnailSize.Height
					: 1;
				var percentFinal = new[] { persentWidth, persentHeight }.Min();
				var finalWidth = (Int32)(slidesListView.ThumbnailSize.Width * percentFinal);
				var finalHeight = (Int32)(slidesListView.ThumbnailSize.Height * percentFinal);

				slidesListView.ThumbnailSize = new Size(finalWidth, finalHeight);
			}
		}

		public SlideGroupPage(string groupName, IList<SlideMaster> slides)
		{
			InitializeComponent();
			SlideGroupName = groupName;
			Text = SlideGroupName;

			_slideMasters.AddRange(slides);

			if (_slideMasters.Any())
			{
				var minOrder = _slideMasters.Min(s => s.Order);

				slidesListView.Items.Clear();
				slidesListView.Items.AddRange(_slideMasters.Select(slideMaster => new ImageListViewItem(slideMaster.LogoFile.LocalPath)
				{
					Text = slideMaster.Name,
					Tag = slideMaster,
					Selected = slideMaster.Order == minOrder
				}).ToArray());
			}

			slidesListView.ClearSelection();
			slidesListView.SelectionChanged += OnListViewSelectionChanged;
		}

		private void OnListViewSelectionChanged(Object sender, EventArgs e)
		{
			SelectionChanged?.Invoke(this, EventArgs.Empty);
		}

		public void ResetSelection()
		{
			slidesListView.ClearSelection();
		}

		public void SelectSlide(SlideMaster slideMaster)
		{
			ResetSelection();
			var itemToSelect = slidesListView.Items.FirstOrDefault(item => item.Tag == slideMaster);
			if (itemToSelect != null)
				itemToSelect.Selected = true;
		}

		public void Release()
		{
			slidesListView.ClearSelection();
			slidesListView.Items.Clear();
			_slideMasters.Clear();

			SlideOutput = null;
			SelectionChanged = null;
		}

		private void OnListViewMouseMove(object sender, MouseEventArgs e)
		{
			slidesListView.Focus();

			slidesListView.HitTest(new Point(e.X, e.Y), out var hitInfo);
			if (!hitInfo.ItemHit)
				toolTipController.HideHint();
		}

		private void OnListViewItemDoubleClick(object sender, ItemClickEventArgs e)
		{
			slidesListView.ClearSelection();
			e.Item.Selected = true;
			SlideOutput?.Invoke(this, new SlideMasterEventArgs { SlideMaster = (SlideMaster)e.Item.Tag });
		}

		private void OnListViewItemHover(object sender, ItemHoverEventArgs e)
		{
			toolTipController.HideHint();
			var slideMaster = e.Item?.Tag as SlideMaster;
			if (String.IsNullOrEmpty(slideMaster?.ToolTipHeader) || String.IsNullOrEmpty(slideMaster.ToolTipBody)) return;

			var toolTipParameters = new ToolTipControllerShowEventArgs();
			var superTip = new SuperToolTip();
			var toolTipSetupArgs = new SuperToolTipSetupArgs();
			toolTipSetupArgs.AllowHtmlText = DefaultBoolean.True;
			toolTipSetupArgs.Title.Text = String.Format("<b>{0}</b>", slideMaster.ToolTipHeader);
			toolTipSetupArgs.Title.Font = new Font("Arial", 10);
			toolTipSetupArgs.Contents.Font = new Font("Arial", 9);
			toolTipSetupArgs.Contents.Text = String.Format("<color=gray>{0}</color>", slideMaster.ToolTipBody);
			superTip.Setup(toolTipSetupArgs);
			toolTipParameters.SuperTip = superTip;

			toolTipController.ShowHint(toolTipParameters, MousePosition);
		}

		private void OnListViewMouseLeave(object sender, EventArgs e)
		{
			toolTipController.HideHint();
		}

		private void OnListViewMouseDown(object sender, MouseEventArgs e)
		{
			_menuHitInfo = null;
			slidesListView.HitTest(new Point(e.X, e.Y), out var hitInfo);
			if (ModifierKeys != Keys.None) return;
			if (!hitInfo.InItemArea) return;
			switch (e.Button)
			{
				case MouseButtons.Right:
					_menuHitInfo = hitInfo;
					if (SlideOutput != null)
						contextMenuStrip.Show(MousePosition);
					break;
			}
		}

		private void OnContextMenuOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = SlideOutput == null ||
				!(_menuHitInfo != null && _menuHitInfo.InItemArea && _menuHitInfo.ItemIndex >= 0);
		}

		private void OnMenuItemOutputClick(object sender, EventArgs e)
		{
			var slideMaster = (SlideMaster)slidesListView.Items[_menuHitInfo.ItemIndex].Tag;
			SlideOutput?.Invoke(this, new SlideMasterEventArgs { SlideMaster = slideMaster });
		}
	}
}
