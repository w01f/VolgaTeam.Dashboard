using System;
using System.Windows.Forms;
using Asa.Common.GUI.ToolForms;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class StarAppTabPageContainerControl<TStarAppControl> : XtraTabPage, IStarAppTabPageContainer where TStarAppControl : StarAppControl
	{
		private TStarAppControl _contentControl;
		private readonly BaseStarAppContainer _slideContainer;
		public StarAppControl ContentControl => _contentControl;

		public StarAppTabPageContainerControl(BaseStarAppContainer slideContainer)
		{
			_slideContainer = slideContainer;
			InitializeComponent();

			if (typeof(AudienceControl) == typeof(TStarAppControl))
				Text = _slideContainer.StarInfo.Titles.Tab9Title;
			else if (typeof(CleanslateControl) == typeof(TStarAppControl))
				Text = _slideContainer.StarInfo.Titles.Tab0Title;
			else if (typeof(ClosersControl) == typeof(TStarAppControl))
				Text = _slideContainer.StarInfo.Titles.Tab11Title;
			else if (typeof(CNAControl) == typeof(TStarAppControl))
				Text = _slideContainer.StarInfo.Titles.Tab2Title;
			else if (typeof(CoverControl) == typeof(TStarAppControl))
				Text = _slideContainer.StarInfo.Titles.Tab1Title;
			else if (typeof(CustomerControl) == typeof(TStarAppControl))
				Text = _slideContainer.StarInfo.Titles.Tab4Title;
			else if (typeof(FishingControl) == typeof(TStarAppControl))
				Text = _slideContainer.StarInfo.Titles.Tab3Title;
			else if (typeof(MarketControl) == typeof(TStarAppControl))
				Text = _slideContainer.StarInfo.Titles.Tab7Title;
			else if (typeof(MarketControl) == typeof(TStarAppControl))
				Text = _slideContainer.StarInfo.Titles.Tab7Title;
			else if (typeof(ROIControl) == typeof(TStarAppControl))
				Text = _slideContainer.StarInfo.Titles.Tab6Title;
			else if (typeof(ShareControl) == typeof(TStarAppControl))
				Text = _slideContainer.StarInfo.Titles.Tab5Title;
			else if (typeof(SolutionControl) == typeof(TStarAppControl))
				Text = _slideContainer.StarInfo.Titles.Tab10Title;
			else if (typeof(VideoControl) == typeof(TStarAppControl))
				Text = _slideContainer.StarInfo.Titles.Tab8Title;
		}

		public void LoadContent()
		{
			if (_contentControl != null) return;
			Application.DoEvents();
			_contentControl = (TStarAppControl)Activator.CreateInstance(typeof(TStarAppControl), _slideContainer);
			_contentControl.Dock = DockStyle.Fill;
			Controls.Add(_contentControl);
			_slideContainer.AssignCloseActiveEditorsOnOutsideClick(_contentControl);
			Application.DoEvents();
		}

	}
}
