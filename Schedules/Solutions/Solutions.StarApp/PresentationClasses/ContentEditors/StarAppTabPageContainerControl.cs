using System;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	//public partial class StarAppTabPageContainerControl<TStarAppControl> : UserControl, IStarAppTabPageContainer where TStarAppControl : StarAppControl
	public partial class StarAppTabPageContainerControl<TStarAppControl> : XtraTabPage, IStarAppTabPageContainer where TStarAppControl : StarAppControl
	{
		private TStarAppControl _contentControl;
		private readonly BaseStarAppContainer _slideContainer;
		private readonly StarTopTabInfo _tabInfo;
		public StarAppControl ContentControl => _contentControl;

		public StarAppTabPageContainerControl(BaseStarAppContainer slideContainer, StarTopTabInfo tabInfo)
		{
			_slideContainer = slideContainer;
			_tabInfo = tabInfo;
			InitializeComponent();

			Text = _tabInfo.Title;
		}

		public void LoadContent()
		{
			if (_contentControl != null) return;
			Application.DoEvents();
			_contentControl = (TStarAppControl)Activator.CreateInstance(typeof(TStarAppControl), _slideContainer, _tabInfo);
			_contentControl.Dock = DockStyle.Fill;
			Controls.Add(_contentControl);
			_slideContainer.AssignCloseActiveEditorsOnOutsideClick(_contentControl);
			_contentControl.BringToFront();
			Application.DoEvents();
		}
	}
}
