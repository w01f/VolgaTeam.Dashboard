using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraTab;
using NewBizWiz.Core.Common;

namespace NewBizWiz.CommonGUI.Slides
{
	[ToolboxItem(false)]
	//public partial class SlideGroupControl : UserControl
	public partial class SlideGroupControl : XtraTabPage
	{
		private readonly List<SlideMaster> _slides = new List<SlideMaster>();
		public string GroupName { get; private set; }
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

		private void layoutViewSlides_DoubleClick(object sender, System.EventArgs e)
		{
			var layoutView = sender as LayoutView;
			var hitInfo = layoutView.CalcHitInfo(layoutView.GridControl.PointToClient(MousePosition));
			if (!hitInfo.InCard) return;
			var slideMaster = layoutView.GetRow(hitInfo.RowHandle) as SlideMaster;
			if (slideMaster == null) return;
			using (var form = new FormSlidePreview())
			{
				form.Text = slideMaster.Name;
				form.pictureBox.Image = slideMaster.Logo;
				form.ShowDialog();
			}
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
	}
}
