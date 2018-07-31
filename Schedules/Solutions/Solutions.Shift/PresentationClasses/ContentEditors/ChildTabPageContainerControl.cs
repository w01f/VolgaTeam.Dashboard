using System;
using System.Windows.Forms;
using Asa.Business.Solutions.Shift.Configuration;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	//public partial class ChildTabPageContainerControl<TChildTabControl> : UserControl, IChildTabPageContainer where TChildTabControl : ChildTabBaseControl
	public partial class ChildTabPageContainerControl<TChildTabControl> : XtraTabPage, IChildTabPageContainer where TChildTabControl : ChildTabBaseControl
	{
		private TChildTabControl _contentControl;
		private readonly ShiftChildTabInfo _tabInfo;
		public MultiTabControl ParentControl { get; }
		public ChildTabBaseControl ContentControl => _contentControl;

		public ChildTabPageContainerControl(MultiTabControl multiTabControl, ShiftChildTabInfo tabInfo)
		{
			ParentControl = multiTabControl;
			_tabInfo = tabInfo;
			InitializeComponent();

			Text = _tabInfo.Title;
		}

		public void LoadContent()
		{
			if (_contentControl != null) return;
			TabControl.BeginUpdate();
			_contentControl = (TChildTabControl)Activator.CreateInstance(typeof(TChildTabControl), this, _tabInfo);
			_contentControl.Dock = DockStyle.Fill;
			_contentControl.Visible = false;
			Controls.Add(_contentControl);
			ParentControl.SlideContainer.AssignCloseActiveEditorsOnOutsideClick(ContentControl);
			_contentControl.Visible = true;
			_contentControl.BringToFront();
			_contentControl.ApplyBackground();
			TabControl.EndUpdate();
			Application.DoEvents();
		}
	}
}
