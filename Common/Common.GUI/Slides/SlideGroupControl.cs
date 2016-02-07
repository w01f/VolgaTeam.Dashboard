using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Asa.Common.Core.Objects.Slides;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraTab;

namespace Asa.Common.GUI.Slides
{
	[ToolboxItem(false)]
	//public partial class SlideGroupControl : UserControl
	public partial class SlideGroupControl : XtraTabPage
	{
		private readonly List<SlideMaster> _slides = new List<SlideMaster>();
		public string GroupName { get; private set; }
		public event EventHandler<SlideMasterEventArgs> SlideChanged;
		public event EventHandler<SlideMasterEventArgs> SlideSelected;
		public SlideMaster SelectedSlide
		{
			get { return layoutViewSlides.GetFocusedRow() as SlideMaster; }
		}

		public SlideGroupControl()
		{
			InitializeComponent();
		}

		public void LoadSlides(string groupName, IEnumerable<SlideMaster> slides)
		{
			GroupName = groupName;
			Text = GroupName;
			_slides.Clear();
			_slides.AddRange(slides);
			gridControlSlides.DataSource = _slides;
		}

		public void SelectSlide(string slideName)
		{
			var slideToSelect = _slides.FirstOrDefault(s => s.Name.Equals(slideName));
			if (slideToSelect == null) return;
			layoutViewSlides.FocusedRowHandle = _slides.IndexOf(slideToSelect);
			layoutViewSlides.VisibleRecordIndex = layoutViewSlides.FocusedRowHandle;
		}

		private void layoutViewSlides_DoubleClick(object sender, EventArgs e)
		{
			var layoutView = sender as LayoutView;
			var hitInfo = layoutView.CalcHitInfo(layoutView.GridControl.PointToClient(MousePosition));
			if (!hitInfo.InCard) return;
			var slideMaster = layoutView.GetRow(hitInfo.RowHandle) as SlideMaster;
			if (slideMaster == null) return;
			if (SlideSelected != null)
				SlideSelected(this, new SlideMasterEventArgs { SelectedSlide = slideMaster });
		}

		private void layoutViewSlides_CustomFieldValueStyle(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventArgs e)
		{
			var view = sender as LayoutView;
			if (view.FocusedRowHandle == e.RowHandle)
			{
				e.Appearance.BackColor = Color.NavajoWhite;
				e.Appearance.BackColor2 = Color.NavajoWhite;
			}
		}

		private void layoutViewSlides_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if (SlideChanged != null)
				SlideChanged(this, new SlideMasterEventArgs { SelectedSlide = SelectedSlide });
		}

		private void gridControlSlides_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			gridControlSlides.MainView.Focus();
		}
	}
}
