using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using NewBizWiz.Core.Common;

namespace NewBizWiz.CommonGUI.Slides
{
	public partial class FormSlideSelector : MetroForm
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
			_slideContainer.SlideChanged += (o, e) =>
			{
				laSlideName.Text = e.SelectedSlide != null ? e.SelectedSlide.Name : String.Empty;
			};
			_slideContainer.SlideSelected += (o, e) =>
			{
				DialogResult = DialogResult.OK;
				Close();
			};
			_slideContainer.InitSlides(slideManager);
			pnMain.Controls.Add(_slideContainer);
			_slideContainer.BringToFront();
		}

		public void SetSelectedSlide(string selectedGroup, string selectedSlide)
		{
			_slideContainer.SetSelectedSlide(selectedGroup, selectedSlide);
			laSlideName.Text = selectedSlide;
		}

		private void buttonXAddSlide_Click(object sender, EventArgs e)
		{
			if (AddSlide != null)
				AddSlide(this, new SlideMasterEventArgs { SelectedSlide = SelectedSlide });
		}
	}
}
