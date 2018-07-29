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
		private bool _allowHandleEvents;
		private readonly List<SlideMaster> _slideMasters = new List<SlideMaster>();
		private SlideAdaptor _slideAdaptor;
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

		public SlideGroupPage(string groupName, IEnumerable<SlideMaster> slides)
		{
			InitializeComponent();
			SlideGroupName = groupName;
			Text = SlideGroupName;

			_slideMasters.AddRange(slides);

			_allowHandleEvents = false;
			if (_slideMasters.Any())
			{
				var defaultSlideMaster = _slideMasters.First();
				slidesListView.ThumbnailSize = new Size(defaultSlideMaster.BrowseLogo.Width + 26, defaultSlideMaster.BrowseLogo.Height + 26);

				var minOrder = _slideMasters.Min(s => s.Order);
				_slideAdaptor = new SlideAdaptor(_slideMasters);
				slidesListView.Items.Clear();
				slidesListView.Items.AddRange(
					_slideMasters
						.Select(slideMaster => new ImageListViewItem(slideMaster.Identifier)
						{
							Text = slideMaster.Name,
							Tag = slideMaster,
							Selected = slideMaster.Order == minOrder,
						}).ToArray(),
					_slideAdaptor);
			}
			_allowHandleEvents = true;

			slidesListView.ClearSelection();
			var defaultItem = slidesListView.Items.FirstOrDefault();
			if (defaultItem != null)
				defaultItem.Selected = true;
			slidesListView.SelectionChanged += OnListViewSelectionChanged;
		}

		public void ResetSelection()
		{
			_allowHandleEvents = false;
			slidesListView.ClearSelection();
			_allowHandleEvents = true;
		}

		public void SelectSlide(SlideMaster slideMaster)
		{
			_allowHandleEvents = false;

			ResetSelection();
			var itemToSelect = slidesListView.Items.FirstOrDefault(item => item.Tag == slideMaster);
			if (itemToSelect != null)
				itemToSelect.Selected = true;

			_allowHandleEvents = true;
		}

		public void SetBackground(Image image)
		{
			if (image != null)
			{
				slidesListView.BackgroundImage = image;
				slidesListView.BackgroundImageLayout = ImageLayout.Stretch;
			}
		}

		public void Release()
		{
			slidesListView.ClearSelection();
			slidesListView.Items.Clear();
			_slideAdaptor.Dispose();
			_slideAdaptor = null;
			_slideMasters.Clear();

			SlideOutput = null;
			SelectionChanged = null;
		}

		private void OnListViewSelectionChanged(Object sender, EventArgs e)
		{
			SelectionChanged?.Invoke(this, EventArgs.Empty);
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
