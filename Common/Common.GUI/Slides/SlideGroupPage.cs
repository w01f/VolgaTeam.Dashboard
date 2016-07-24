using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Objects.Slides;
using DevExpress.XtraTab;
using Manina.Windows.Forms;

namespace Asa.Common.GUI.Slides
{
	//public partial class SlideGroupPage : UserControl
	public partial class SlideGroupPage : XtraTabPage
	{
		private readonly List<SlideMaster> _slideMasters = new List<SlideMaster>();
		private SlideAdaptor _slideAdaptor;

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
		}
	}
}
