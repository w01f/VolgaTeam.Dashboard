using System;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	//public partial class ChildTabPageContainerControl<TChildTabControl> : UserControl, IChildTabPageContainer where TChildTabControl : ChildTabBaseControl
	public partial class ChildTabPageContainerControl<TChildTabControl> : XtraTabPage, IChildTabPageContainer where TChildTabControl : ChildTabBaseControl
	{
		private TChildTabControl _contentControl;
		private readonly StarChildTabInfo _tabInfo;
		public MultiTabControl ParentControl { get; }
		public ChildTabBaseControl ContentControl => _contentControl;

		public ChildTabPageContainerControl(MultiTabControl multiTabControl, StarChildTabInfo tabInfo)
		{
			ParentControl = multiTabControl;
			_tabInfo = tabInfo;
			InitializeComponent();

			Text = _tabInfo.Title;
		}

		public void LoadContent()
		{
			if (_contentControl != null) return;
			Application.DoEvents();
			_contentControl = (TChildTabControl)Activator.CreateInstance(typeof(TChildTabControl), this, _tabInfo);
			_contentControl.Dock = DockStyle.Fill;
			Controls.Add(_contentControl);
			ParentControl.SlideContainer.AssignCloseActiveEditorsOnOutsideClick(ContentControl);
			_contentControl.BringToFront();
			Application.DoEvents();
		}
	}
}
