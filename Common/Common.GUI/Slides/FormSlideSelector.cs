using System;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Slides;
using DevComponents.DotNetBar.Metro;

namespace Asa.Common.GUI.Slides
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
			laSlideSize.Text = String.Format(laSlideSize.Text, PowerPointManager.Instance.SlideSettings.SizeFormatted);
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
			AddSlide?.Invoke(this, new SlideMasterEventArgs { SelectedSlide = SelectedSlide });
		}
	}
}
