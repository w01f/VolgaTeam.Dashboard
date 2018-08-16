using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;

namespace Asa.Common.GUI.Common
{
	public class XtraTabDragDropHelper<TTabPage> where TTabPage : XtraTabPage
	{
		private readonly XtraTabControl _tabControl;
		private Point _point = Point.Empty;
		private XtraTabPage _movedPage;

		public event EventHandler<TabMoveEventArgs> TabMoved;

		public XtraTabDragDropHelper(XtraTabControl tabControl)
		{
			_tabControl = tabControl;
			SubscribeEvents();
		}

		private void SubscribeEvents()
		{
			_tabControl.AllowDrop = true;
			_tabControl.MouseDown += OnTabControlMouseDown;
			_tabControl.MouseMove += OnTabControlMouseMove;
			_tabControl.DragOver += OnTabControlDragOver;
		}

		private void OnTabControlMouseDown(object sender, MouseEventArgs e)
		{
			var point = new Point(e.X, e.Y);
			var hi = _tabControl.CalcHitInfo(point);
			if (hi.Page is TTabPage)
			{
				_movedPage = hi.Page;
				_point = point;
			}
			else
			{
				_movedPage = null;
				_point = Point.Empty;
			}
		}

		private void OnTabControlMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			if (_movedPage != null && _point != Point.Empty && (Math.Abs(e.X - _point.X) > SystemInformation.DragSize.Width || (Math.Abs(e.Y - _point.Y) > SystemInformation.DragSize.Height)))
				_tabControl.DoDragDrop(sender, DragDropEffects.Move);
		}

		private void OnTabControlDragOver(object sender, DragEventArgs e)
		{
			var hi = _tabControl.CalcHitInfo(_tabControl.PointToClient(new Point(e.X, e.Y)));
			if (hi.Page is TTabPage)
			{
				if (hi.Page != _movedPage)
				{
					if (_tabControl.TabPages.IndexOf(hi.Page) < _tabControl.TabPages.IndexOf(_movedPage))
					{
						_tabControl.TabPages.Move(_tabControl.TabPages.IndexOf(hi.Page), _movedPage);
						TabMoved?.Invoke(_tabControl, new TabMoveEventArgs { MovedPage = _movedPage, TargetPage = hi.Page });
					}
					else
					{
						_tabControl.TabPages.Move(_tabControl.TabPages.IndexOf(hi.Page) + 1, _movedPage);
						TabMoved?.Invoke(_tabControl, new TabMoveEventArgs { MovedPage = _movedPage, TargetPage = hi.Page, Offset = 1 });
					}
				}
				e.Effect = DragDropEffects.Move;
			}
			else
				e.Effect = DragDropEffects.None;
		}
	}

	public class TabMoveEventArgs : EventArgs
	{
		public XtraTabPage MovedPage { get; set; }
		public XtraTabPage TargetPage { get; set; }
		public int Offset { get; set; }
	}
}