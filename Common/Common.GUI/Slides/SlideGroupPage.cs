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
		private SlideAdaptor _slideAdaptor;
		private ImageListView.HitInfo _menuHitInfo;

		public event EventHandler<SlideMasterEventArgs> SlideOutput;

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
			Text = groupName;
			_slideMasters.AddRange(slides);
			if (_slideMasters.Any())
			{
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
		}

		public void Release()
		{
			slidesListView.ClearSelection();
			slidesListView.Items.Clear();
			_slideAdaptor.Dispose();
			_slideAdaptor = null;
			_slideMasters.Clear();
		}

		private void imageListView_MouseMove(object sender, MouseEventArgs e)
		{
			slidesListView.Focus();

			ImageListView.HitInfo hitInfo;
			slidesListView.HitTest(new Point(e.X, e.Y), out hitInfo);
			if (!hitInfo.ItemHit)
				toolTipController.HideHint();
		}

		private void imageListView_ItemDoubleClick(object sender, ItemClickEventArgs e)
		{
			slidesListView.ClearSelection();
			e.Item.Selected = true;
			SlideOutput?.Invoke(this, new SlideMasterEventArgs { SlideMaster = (SlideMaster)e.Item.Tag });
		}

		private void slidesListView_ItemHover(object sender, ItemHoverEventArgs e)
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

		private void slidesListView_MouseLeave(object sender, EventArgs e)
		{
			toolTipController.HideHint();
		}

		private void imageListView_MouseDown(object sender, MouseEventArgs e)
		{
			_menuHitInfo = null;
			ImageListView.HitInfo hitInfo;
			slidesListView.HitTest(new Point(e.X, e.Y), out hitInfo);
			if (ModifierKeys != Keys.None) return;
			if (!hitInfo.InItemArea) return;
			switch (e.Button)
			{
				case MouseButtons.Right:
					_menuHitInfo = hitInfo;
					contextMenuStrip.Show(MousePosition);
					break;
			}
		}

		private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = !(_menuHitInfo != null && _menuHitInfo.InItemArea && _menuHitInfo.ItemIndex >= 0);
		}

		private void toolStripMenuItemOutput_Click(object sender, System.EventArgs e)
		{
			var slideMaster = (SlideMaster)slidesListView.Items[_menuHitInfo.ItemIndex].Tag;
			SlideOutput?.Invoke(this, new SlideMasterEventArgs { SlideMaster = slideMaster });
		}
	}
}
