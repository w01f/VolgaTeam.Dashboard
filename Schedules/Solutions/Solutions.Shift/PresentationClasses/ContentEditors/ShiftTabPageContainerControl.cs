using System;
using System.Windows.Forms;
using Asa.Business.Solutions.Shift.Configuration;
using DevExpress.XtraTab;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	//public partial class ShiftTabPageContainerControl<TShiftControl> : UserControl, IShiftTabPageContainer where TShiftControl : ShiftControl
	public partial class ShiftTabPageContainerControl<TShiftControl> : XtraTabPage, IShiftTabPageContainer where TShiftControl : BaseShiftControl
	{
		private TShiftControl _contentControl;
		private readonly BaseShiftContainer _slideContainer;
		private readonly ShiftTopTabInfo _tabInfo;
		public BaseShiftControl ContentControl => _contentControl;

		public ShiftTabPageContainerControl(BaseShiftContainer slideContainer, ShiftTopTabInfo tabInfo)
		{
			_slideContainer = slideContainer;
			_tabInfo = tabInfo;
			InitializeComponent();

			Text = _tabInfo.Title;
		}

		public void LoadContent()
		{
			if (_contentControl != null) return;
			TabControl.BeginUpdate();
			_contentControl = (TShiftControl)Activator.CreateInstance(typeof(TShiftControl), _slideContainer, _tabInfo);
			_contentControl.Dock = DockStyle.Fill;
			Controls.Add(_contentControl);
			_slideContainer.AssignCloseActiveEditorsOnOutsideClick(_contentControl);
			_contentControl.InitControls();
			_contentControl.BringToFront();
			TabControl.EndUpdate();
			Application.DoEvents();
		}
	}
}
