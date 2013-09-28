using System;
using System.Windows.Forms;
using NewBizWiz.Core.Common;

namespace NewBizWiz.CommonGUI.Slides
{
	public partial class FormSlideSelector : Form
	{
		private SlidesContainerControl _slideContainer;

		public event EventHandler<SlideMasterEventArgs> AddSlide;

		public SlideMaster SelectedSlide
		{
			get { return _slideContainer.SelectedSlide; }
		}

		public FormSlideSelector()
		{
			InitializeComponent();
			laSlideSize.Text = String.Format(laSlideSize.Text, SettingsManager.Instance.Size);
		}

		public void LoadSlides(SlideManager slideManager)
		{
			_slideContainer = new SlidesContainerControl();
			_slideContainer.InitSlides(slideManager);
			pnMain.Controls.Add(_slideContainer);
			_slideContainer.BringToFront();
		}

		public void SetSelectedSlide(string selectedGroup, string selectedSlide)
		{
			_slideContainer.SetSelectedSlide(selectedGroup, selectedSlide);
		}

		private void buttonXAddSlide_Click(object sender, EventArgs e)
		{
			if (AddSlide != null)
				AddSlide(this, new SlideMasterEventArgs { SelectedSlide = SelectedSlide });
		}
	}
}
